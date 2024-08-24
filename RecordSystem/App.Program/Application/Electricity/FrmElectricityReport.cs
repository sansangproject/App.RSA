using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmElectricityReport : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;
        public string programCode = "MEARE00";
        public string idTSS;
        public string strConditions;
        public string ReportName;
        public string strErr = "";
        public string ExportToPath = "";

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        private clsDate Date = new clsDate();
        private clsSearch Search = new clsSearch();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
        private clsDataList List = new clsDataList();
        private dbConnection db = new dbConnection();


        public FrmElectricityReport(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            
            Clear();
        }

        public void Clear()
        {
            txtRecId.Text = "";
            txtCode.Text = "ELB";
            txtDate.Text = "";

            txtRecId.Focus();

            List.GetMonthList(cbbMonth);
            List.GetYearList(cbbYear);
        }

        public void getDataGrid(DataTable dt)
        {
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
            ExportToFile("PDF", sender, e);
        }

        private void btnExpenEXCEL_Click(object sender, EventArgs e)
        {
            ExportToFile("XLS", sender, e);
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
            if (txtCode.Text != "" || txtCode.Text != "ELB")
            {
                strConditions += " | รหัสอ้างอิง : " + txtCode.Text;
            }
            if (txtRecId.Text != "")
            {
                strConditions += " | เลขที่แจ้งค่าไฟฟ้า : " + txtRecId.Text;
            }
            if (txtDate.Text != "")
            {
                txtDate.Text = Date.GetDate(dtp: dtDate, Format: 4);
                strConditions += " | วันที่แจ้งค่าไฟฟ้า : " + txtDate.Text;
            }
        }

        public void ExportToFile(string fileType, object sender, EventArgs e)
        {
            strConditions = "";
            setCondition();
            idTSS = Fn.getTssFileName("MEARE");
            FrmAnimatedProgress frm = new FrmAnimatedProgress(10);

            if (txtCode.Text == "" || txtCode.Text == "ELB")
            {
                txtCode.Text = "";
            }

            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@RcptElecCode", txtCode.Text},
                    {"@RcptElecNumber", txtRecId.Text},
                    {"@RcptElecDate", txtDate.Text},
                    {"@RcptElecMonth", Fn.getComboBoxValue(cbbMonth)},
                    {"@RcptElecYear", Fn.getComboBoxValue(cbbYear)},
                };

                db.Get("Spr_R_TblReceiptElectricity", Parameter, out strErr, out dt);

                if (strErr == null && dt.Rows.Count > 0)
                {
                    string Path = Fn.getPath("Report");
                    ReportName = "RSA-R-MEARE00001";

                    DialogResult result = folderBrowserDialog.ShowDialog();
                    frm.Show();

                    dt.Columns.Add(new DataColumn("QRCode", typeof(byte[])));
                    dt.Columns.Add(new DataColumn("BARCode", typeof(byte[])));
                    dt.Columns.Add(new DataColumn("RcptBarcode", typeof(byte[])));

                    foreach (DataRow row in dt.Rows)
                    {
                        Zen.Barcode.Code39BarcodeDraw bar = Zen.Barcode.BarcodeDrawFactory.Code39WithoutChecksum;
                        Image imgBar;
                        Bitmap BitBar;
                        string strBar = "";
                        var streamBar = new MemoryStream();
                        byte[] byteBar;

                        strBar = dt.Rows[0]["MsElecCARefNo1"].ToString();
                        imgBar = bar.Draw(strBar, 20);
                        BitBar = (Bitmap)imgBar;
                        BitBar.Save(streamBar, System.Drawing.Imaging.ImageFormat.Png);
                        byteBar = streamBar.ToArray();
                        row["BARCode"] = byteBar;

                        strBar = dt.Rows[0]["RcptElecBarcode"].ToString();
                        imgBar = bar.Draw(strBar, 20);
                        BitBar = (Bitmap)imgBar;
                        BitBar.Save(streamBar, System.Drawing.Imaging.ImageFormat.Png);
                        byteBar = streamBar.ToArray();
                        row["RcptBarcode"] = byteBar;

                        Zen.Barcode.CodeQrBarcodeDraw qr = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                        Image imgQr;
                        Bitmap BitQr;
                        string strQr = ""
                                        + dt.Rows[0]["RcptElecBarcode"].ToString().Substring(0, 16) + System.Environment.NewLine
                                        + dt.Rows[0]["RcptElecBarcode"].ToString().Substring(17, 20) + System.Environment.NewLine
                                        + dt.Rows[0]["RcptElecBarcode"].ToString().Substring(38, 15) + System.Environment.NewLine
                                        + dt.Rows[0]["RcptElecBarcode"].ToString().Substring(54, 5);

                        imgQr = qr.Draw(strQr, 15);

                        BitQr = (Bitmap)imgQr;
                        var streamQr = new MemoryStream();
                        BitQr.Save(streamQr, System.Drawing.Imaging.ImageFormat.Png);

                        byte[] byteQr = streamQr.ToArray();
                        row["QRCode"] = byteQr;
                    }

                

                    if (result == DialogResult.OK)
                    {
                        ExportToPath = folderBrowserDialog.SelectedPath + "\\TSSRSA-MEARE00R" + Fn.getFileLastName();

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
                            Insert.TblLogReport(idTSS, ReportName, strConditions, programCode, "MEA", "TSSRSA-MEARE00R" + Fn.getFileLastName() + "." + fileType, ExportToPath + "." + fileType, strUserName + " " + strUserSurname);
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

        private void dtDate_ValueChanged(object sender, EventArgs e)
        {
            txtDate.Text = Date.GetDate(dtp: dtDate);
        }

        private void txtRecId_KeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnExpenPDF);
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnExpenPDF);
        }
    }
}