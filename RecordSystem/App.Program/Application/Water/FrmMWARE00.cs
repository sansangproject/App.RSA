using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;
using SANSANG.Class;
using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmMWARE00 : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;
        public string programCode = "MWARE00";
        public string idTSS;
        public string strConditions;
        public string ReportName;
        public string strErr = "";
        public string ExportToPath = "";

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        private clsSearch Search = new clsSearch();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsDataList List = new clsDataList();
        private clsBarcode Barcode = new clsBarcode();
        private dbConnection db = new dbConnection();
        private clsMessage Message = new clsMessage();
        private clsDate Date = new clsDate();

        public FrmMWARE00(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmMWARE00_Load(object sender, EventArgs e)
        {

            Clear();
        }

        public void Clear()
        {
            txtRecId.Text = "";
            txtCode.Text = "WAB";
            txtDate.Text = "";

            txtRecId.Focus();

            List.GetMonthList(cbbMonth);
            List.GetYearList(cbbYear);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnExpenPDF_Click(object sender, EventArgs e)
        {
            //DownloadFile();
        }

        private void btnExpenEXCEL_Click(object sender, EventArgs e)
        {
            ExportToFile("XLS", sender, e);
        }

        public void DownloadFile()
        {
            if (cbbYear.SelectedIndex != 0)
            {
                for (int Month = 1; Month <= 12; Month++)
                {
                    //string Months = cbbMonth.SelectedValue.ToString().PadLeft(2, '0');
                    string Months = Month.ToString().PadLeft(2, '0');
                    string Years = cbbYear.Text;

                    string File = string.Format("D:\\MWAs\\INVOICEs\\MWA{0}{1}.pdf", Years, Months);

                    Uri uri = new Uri(string.Format("https://eservicesapp.mwa.co.th//reportsBH//rwservlet?hide_pass_keyCISWEB3=&destype=cache&desformat=pdf&report=HH_BILL_QR.rdf&desname=INVMWA.pdf&P_ACCOUNT=76943976&P_MONTH={0}&P_YEAR={1}", Months, Years));
                    string Links = uri.AbsoluteUri;

                    if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                    {
                        using (System.Net.WebClient client = new System.Net.WebClient())
                        {
                            client.DownloadFileAsync(new Uri(Links), File);
                        }
                    }
                }
            }
        }

        public void ExportToFile(string fileType, object sender, EventArgs e)
        {
            setCondition();
            idTSS = Fn.getTssFileName("MWARE");
            FrmAnimatedProgress frm = new FrmAnimatedProgress(10);
            strConditions = "";

            if (txtCode.Text == "" || txtCode.Text == "WAB")
            {
                txtCode.Text = "";
            }

            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@RcptWaterCode", txtCode.Text},
                    {"@RcptWaterNumber", txtRecId.Text},
                    {"@RcptWaterDateBill", txtDate.Text},
                    {"@RcptWaterMonth", Fn.getComboBoxValue(cbbMonth)},
                    {"@RcptWaterYear", Fn.getComboBoxValue(cbbYear)},
                };

                db.Get("Spr_R_TblRecieptWater", Parameter, out strErr, out dt);
                db.GetReport("Spr_R_TblRecieptWater", Parameter, out strErr, out ds);

                if (strErr == null && dt.Rows.Count > 0)
                {
                    string Path = Fn.getPath("App.Report");
                    ReportName = "RSA-R-MWARE00001";

                    string M3 = "0";
                    string M2 = "0";
                    string M1 = "0";
                    try
                    {
                        M3 = ds.Tables[1].Rows[0]["RcptWaterUnit"].ToString();
                    }
                    catch (Exception)
                    {
                        M3 = "0";
                    }
                    try
                    {
                        M2 = ds.Tables[1].Rows[1]["RcptWaterUnit"].ToString();
                    }
                    catch (Exception)
                    {
                        M2 = "0";
                    }
                    try
                    {
                        M1 = ds.Tables[1].Rows[2]["RcptWaterUnit"].ToString();
                    }
                    catch (Exception)
                    {
                        M1 = "0";
                    }

                    DialogResult result = folderBrowserDialog.ShowDialog();
                    frm.Show();

                    dt.Columns.Add(new DataColumn("BARCODEIMG", typeof(byte[])));
                    dt.Columns.Add(new DataColumn("QRCODEIMG", typeof(byte[])));

                    dt.Columns.Add(new DataColumn("UNITBEFOR3", typeof(string)));
                    dt.Columns.Add(new DataColumn("UNITBEFOR2", typeof(string)));
                    dt.Columns.Add(new DataColumn("UNITBEFOR1", typeof(string)));

                    foreach (DataRow row in dt.Rows)
                    {
                        Image imgBar;
                        Bitmap BitBar;
                        string strBar = "";

                        try
                        {
                            strBar = ds.Tables[1].Rows[3]["L1"].ToString() + System.Environment.NewLine
                                   + ds.Tables[1].Rows[3]["L2"].ToString() + System.Environment.NewLine
                                   + ds.Tables[1].Rows[3]["L3"].ToString() + System.Environment.NewLine
                                   + ds.Tables[1].Rows[3]["L4"].ToString();
                        }
                        catch (Exception)
                        {
                            strBar = ds.Tables[1].Rows[0]["L1"].ToString() + System.Environment.NewLine
                                   + ds.Tables[1].Rows[0]["L2"].ToString() + System.Environment.NewLine
                                   + ds.Tables[1].Rows[0]["L3"].ToString() + System.Environment.NewLine
                                   + ds.Tables[1].Rows[0]["L4"].ToString();
                        }

                        imgBar = Barcode.Code39(strBar, System.Drawing.Color.Black, System.Drawing.Color.White, 20);

                        BitBar = (Bitmap)imgBar;
                        var streamBar = new MemoryStream();
                        BitBar.Save(streamBar, System.Drawing.Imaging.ImageFormat.Png);

                        byte[] byteBar = streamBar.ToArray();

                        row["BARCODEIMG"] = byteBar;

                        Image imgQr;
                        Bitmap BitQr;
                        string strQr = "";

                        try
                        {
                            strQr = ds.Tables[1].Rows[3]["L1"].ToString() + System.Environment.NewLine
                                  + ds.Tables[1].Rows[3]["L2"].ToString() + System.Environment.NewLine
                                  + ds.Tables[1].Rows[3]["L3"].ToString() + System.Environment.NewLine
                                  + ds.Tables[1].Rows[3]["L4"].ToString();
                        }
                        catch (Exception)
                        {
                            strQr = ds.Tables[1].Rows[0]["L1"].ToString() + System.Environment.NewLine
                                  + ds.Tables[1].Rows[0]["L2"].ToString() + System.Environment.NewLine
                                  + ds.Tables[1].Rows[0]["L3"].ToString() + System.Environment.NewLine
                                  + ds.Tables[1].Rows[0]["L4"].ToString();
                        }

                        imgQr = Barcode.QRCode(strBar, System.Drawing.Color.Black, System.Drawing.Color.White, "M", 2);

                        BitQr = (Bitmap)imgQr;
                        var streamQr = new MemoryStream();
                        BitQr.Save(streamQr, System.Drawing.Imaging.ImageFormat.Png);

                        byte[] byteQr = streamQr.ToArray();
                        row["QRCODEIMG"] = byteQr;

                        row["UNITBEFOR3"] = M3;
                        row["UNITBEFOR2"] = M2;
                        row["UNITBEFOR1"] = M1;
                    }

                    if (result == DialogResult.OK)
                    {
                        ExportToPath = folderBrowserDialog.SelectedPath + "\\TSSRSA-MWARE00R" + Fn.getFileLastName();

                        if (fileType == "PDF")
                        {
                            System.Diagnostics.Process.Start(@"" + ExportToPath + ".pdf");
                        }
                        else
                        {
                            System.Diagnostics.Process.Start(@"" + ExportToPath + ".xls");
                        }

                        try
                        {
                            Insert.TblLogReport(idTSS, ReportName, strConditions, programCode, "MWA", "TSSRSA-MWARE00R" + Fn.getFileLastName() + "." + fileType, ExportToPath + "." + fileType, strUserName + " " + strUserSurname);
                        }
                        catch
                        {
                        }
                    }

                    strConditions = "";
                    Clear();
                }
                else
                {
                    Message.MessageResult("N", "SH", strErr);
                    txtRecId.Focus();
                }
            }
            catch (Exception)
            {
                Message.MessageResult("R", "E", strErr);
                strConditions = "";
                Clear();
            }
        }

        public void setCondition()
        {
            if (Fn.getComboBoxValue(cbbYear) != "0")
            {
                strConditions += " | ปี : " + cbbYear.Text;
            }
            if (Fn.getComboBoxValue(cbbMonth) != "0")
            {
                strConditions += " | เดือน : " + cbbMonth.Text;
            }
            if (txtCode.Text != "" || txtCode.Text != "WAB")
            {
                strConditions += " | รหัสอ้างอิง : " + txtCode.Text;
            }
            if (txtRecId.Text != "")
            {
                strConditions += " | เลขที่แจ้งค่าน้ำ : " + txtRecId.Text;
            }
            if (txtDate.Text != "")
            {
                txtDate.Text = Date.GetDate(dtp: dtDate, Format: 4);
                strConditions += " | วันที่แจ้งค่าน้ำ : " + txtDate.Text;
            }
        }

        private string getOverdue(string value, DateTime DateTime)
        {
            try
            {
                string[,] Parameters = new string[,]
                {
                   {"@RcptWaterDate", Date.GetDate(dt : DateTime.AddMonths(-1))},
                };

                db.Get("Spr_S_TblRecieptWater_Overdue", Parameters, out strErr, out dt);

                int numRows = 0;

                if (dt.Rows.Count >= 1)
                {
                    try
                    {
                        numRows = dt.Rows.Count;
                    }
                    catch (Exception)
                    {
                        numRows = 0;
                    }

                    if (value == "Month")
                    {
                        return "1";
                    }
                    else if (value == "Money")
                    {
                        return dt.Rows[0]["RcptWaterMoneyTotal"].ToString();
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }

        private void txtRecId_KeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnExpenPDF);
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnExpenPDF);
        }

        private void dtDate_ValueChanged(object sender, EventArgs e)
        {
            txtDate.Text = Date.GetDate(dtp: dtDate);
        }
    }
}