using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmReportPage : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string programCode = "RPTMAI00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";
        public string strPath = "";
        public string strValue = "11";
        public string strReportName = "";
        public string strReportType = "รายรับ";
        public string strExportType = "";
        public string strExportToPath = "";
        public string strExpenseIncome = "1";

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsBarcode Barcode = new clsBarcode();
        private clsDate Date = new clsDate();
        private DataTable dtr = new DataTable();
        private DataTable dtrs = new DataTable();

        private ReportDocument rpt;

        public FrmReportPage(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmReportPage_Load(object sender, EventArgs e)
        {
            
            List.GetList(cbbMoney, "Y", "Money");
            List.GetList(cbbPay, "Y", "Payment");
            Clear();
        }

        private void Clear()
        {
            try
            {
                dtDateStart.Text = DateTime.Today.ToString();
                dtDateStart.CustomFormat = "dd-MM-yyyy";
                DateTime dtStart = Convert.ToDateTime(dtDateStart.Text);
                dtDateEnd.Text = dtStart.AddDays(1 - dtStart.Day).AddMonths(1).AddDays(-1).Date.ToString();

                rdbTypeAll.Checked = true;
                rdbDay.Checked = true;

                txtDetail.Text = "";

                cbbMoney.SelectedValue = 0;
                cbbPay.SelectedValue = 0;
            }
            catch (Exception)
            {
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private string reportName(string formats)
        {
            if (formats == "รายวัน")
            {
                return "RSA-R-SAVEXD0001.rpt";
            }
            else if (formats == "รายเดือน")
            {
                return "RSA-R-SAVEXM0001.rpt";
            }
            else
            {
                return "";
            }
        }

        private void printReport(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            strExportType = button.Name;

            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@ExpenseCode", ""},
                    {"@ExpenseList", ""},
                    {"@ExpenseMoney", cbbMoney.SelectedValue.ToString()},
                    {"@ExpensePayment", cbbPay.SelectedValue.ToString()},
                    {"@ExpensePaymentSub", cbbPaySub.SelectedValue.ToString()},
                    {"@ExpenseIncome", strExpenseIncome},
                    {"@ExpenseDetail", txtDetail.Text},
                    {"@ExpenseAmount", ""},
                    {"@ExpenseDateStart", Date.GetDate(dtp : dtDateStart)},
                    {"@ExpenseDateEnd", Date.GetDate(dtp : dtDateEnd)},
                    {"@ExpenseStatus", ""},
                    {"@ExpenseReciept", ""},
                };

                db.Get("Spr_R_TblSaveExpense", Parameter, out strErr, out dtr);
                db.Get("Spr_R_TblSaveExpense_Sub", Parameter, out strErr, out dtrs);

                if (strErr == null && dtr.Rows.Count > 0 && chkShowBarcode.Checked == true)
                {
                    if (rdbQrcode.Checked == true)
                    {
                        dtr.Columns.Add(new DataColumn("ImgQrcode", typeof(byte[])));
                    }
                    else if (rdbBarcode.Checked == true)
                    {
                        dtr.Columns.Add(new DataColumn("ImgBarcode", typeof(byte[])));
                    }

                    int i = 0;

                    foreach (DataRow row in dtr.Rows)
                    {
                        Image imgBar;
                        Bitmap BitBar;
                        byte[] byteBar;
                        var streamBar = new MemoryStream();
                        string strText = dtr.Rows[i]["ExpenseCode"].ToString();

                        if (rdbQrcode.Checked == true)
                        {
                            imgBar = Barcode.QRCode(strText, Color.DarkOrange, Color.White, "M", 2);
                            BitBar = (Bitmap)imgBar;
                            BitBar.Save(streamBar, System.Drawing.Imaging.ImageFormat.Png);
                            byteBar = streamBar.ToArray();
                            row["ImgQrcode"] = byteBar;
                        }
                        else if (rdbBarcode.Checked == true)
                        {
                            imgBar = Barcode.Code128(strText, Color.DarkOrange, Color.White, 50);
                            BitBar = (Bitmap)imgBar;
                            BitBar.Save(streamBar, System.Drawing.Imaging.ImageFormat.Png);
                            byteBar = streamBar.ToArray();
                            row["ImgBarcode"] = byteBar;
                        }
                        else
                        {
                            imgBar = null;
                        }

                        i++;
                    }
                }

                rpt = new ReportDocument();
                rpt.Load(Fn.getPath("Report") + reportName(rdbDay.Text));

                if (rpt.Subreports.Count > 0)
                {
                    rpt.Subreports[0].SetDataSource(dtrs);
                }

                rpt.SetDataSource(dtr);

                strReportName = Fn.getTssFileName("REPORT") + "-" + strValue;
                rpt.SetParameterValue("UserName", strUserName + " " + strUserSurname);
                rpt.SetParameterValue("ReportName", strReportName);
                rpt.SetParameterValue("Type", strReportType);
                rpt.SetParameterValue("dtStart", dtDateStart.Value.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")));
                rpt.SetParameterValue("dtEnd", dtDateEnd.Value.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")));
                rpt.SetParameterValue("PrintDate", DateTime.Today.ToString("d MMMM yyyy", CultureInfo.CreateSpecificCulture("th-TH")));

                crystalReportViewer.ReportSource = rpt;
                crystalReportViewer.Refresh();

                strExportToPath = "";

                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    strExportToPath += folderBrowserDialog.SelectedPath + strReportName;

                    if (strExportType == "btnExportPdf")
                    {
                        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, strExportToPath + ".pdf");
                        rpt.Close();
                        System.Diagnostics.Process.Start(@"" + strExportToPath + ".pdf");
                    }
                    else if (strExportType == "btnExportXls")
                    {
                        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.Excel, strExportToPath + ".xls");
                        rpt.Close();
                        System.Diagnostics.Process.Start(@"" + strExportToPath + ".xls");
                    }
                    else if (strExportType == "btnExportDoc")
                    {
                        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, strExportToPath + ".doc");
                        rpt.Close();
                        System.Diagnostics.Process.Start(@"" + strExportToPath + ".doc");
                    }
                    else
                    {
                    }
                }

                Clear();
            }
            catch (Exception ex)
            {
                Message.ShowMesError("Generation Error : ", ex.ToString() + " " + strErr);
            }
        }

        private void rdbDay_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void rdbType1_CheckedChanged(object sender, EventArgs e)
        {
            strReportType = "รายรับ";
            strValue = "11";
            strExpenseIncome = "1";
        }

        private void rdbType0_CheckedChanged(object sender, EventArgs e)
        {
            strReportType = "รายจ่าย";
            strValue = "00";
            strExpenseIncome = "0";
        }

        private void rdbTypeAll_CheckedChanged(object sender, EventArgs e)
        {
            strReportType = "รายรับรายจ่าย";
            strValue = "10";
            strExpenseIncome = "";
        }

        private void cbbPay_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.GetList(cbbPaySub, cbbPay.SelectedValue.ToString() + ",Y", "PaymentSub");
                cbbPaySub.SelectedValue = "0";
            }
            catch (Exception)
            {
            }
        }

        private void dtDateStart_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                var dtStart = Convert.ToDateTime(dtDateStart.Text);
                dtDateEnd.Text = dtStart.AddDays(1 - dtStart.Day).AddMonths(1).AddDays(-1).Date.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void chkShowBarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowBarcode.Checked == true)
            {
                rdbQrcode.Visible = true;
                rdbBarcode.Visible = true;
            }
            else
            {
                rdbQrcode.Visible = false;
                rdbBarcode.Visible = false;
            }
        }
    }
}