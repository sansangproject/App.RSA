using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmImportImage : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserSurName;
        public string strUserType;

        public string programCode = "IMGMAN00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string filePath = "SPA591012028-5";
        public string fileName = "-";
        public string fileType = ".jpg";

        public string strDataCode, strImageCode, strprogramPath, strprogramName, strIdCode = "";

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsImage TSSImage = new clsImage();

        public string[,] Parameter = new string[,] { };

        public FrmImportImage(string programName, string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin, string dataCode, string imageCode, string programPath, string idCode)
        {
            InitializeComponent();
            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
            strDataCode = dataCode;
            strImageCode = imageCode;
            strprogramPath = programPath;
            strprogramName = programName;
            strIdCode = idCode;
            txtImageCode.Text = strImageCode;
        }

        private void FrmImportImage_Load(object sender, EventArgs e)
        {
            //List.GetList(cbbStatus, "0", "Status");
            //List.GetList(cbbImageTo, "Y", "Path");

            //if (strImageCode != "")
            //{
            //    txtImageToCode.Text = strImageCode;
            //    SearchImage(strImageCode);
            //}
            //else
            //{
            //    cbbImageTo.SelectedValue = strprogramPath == "" ? "P1-PROGRAM000-1" : strprogramPath;
            //}

            //txtImageToRef.Text = strDataCode;
            //btnExit.Focus();
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {

        }

        private void btnImageFromSubmit_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchImage(txtImageToCode.Text);
        }

        private void SearchImage(String imageCode)
        {
           
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static TextBox txtImageCode = new TextBox();

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtImageFromName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            txtImageToCode.Text = txtImageFromName.Text;
            txtImageToName.Text = txtImageFromName.Text;
            txtImageToType.Text = txtImageFromType.Text;
            txtImageToSize.Text = txtImageFromSize.Text;
            strImageCode = txtImageFromName.Text;
            pbImageEnd.Image = pbImageStart.Image;
            cbDeleteImageFrom.Checked = false;
        }

        private void txtImageToCode_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtImageToName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}