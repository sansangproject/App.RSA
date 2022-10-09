using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmPrintAddress : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string programCode = "";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";
        public string strCode = "";
        public string filePath = "-";
        public string fileName = "-";
        public string fileType = ".jpg";

        public string Path = "";
        public string ReportName = "RSA-R-ADDRESS0000";
        public string strQr = "";

        private clsBarcode Barcode = new clsBarcode();
        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsCryption Cryption = new clsCryption();

        public string[,] Parameter = new string[,] { };

        public FrmPrintAddress(string Code)
        {
            InitializeComponent();
            strCode = Code;
        }

        private void FrmPrintAddress_Load(object sender, EventArgs e)
        {
            Path = Fn.getPath("App.Report");

            this.printDialog1.Document = this.printDocument1;
            DialogResult drs = this.printDialog1.ShowDialog();

            try
            {
                DataTable dt = Search.getAddressForPrint(strCode);

                if (drs == DialogResult.OK)
                {
                    int nCopy = this.printDocument1.PrinterSettings.Copies;
                    int sPage = this.printDocument1.PrinterSettings.FromPage;
                    int ePage = this.printDocument1.PrinterSettings.ToPage;
                    string PrinterName = this.printDocument1.PrinterSettings.PrinterName;

                    try
                    {

                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.ToString());
                    }
                }

                this.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}