using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using SANSANG.Class; 
using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmImageEditer : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string filePath = "-";
        public string fileName = "-";
        public string fileType = ".jpg";

        private DataTable dt = new DataTable();
        private clsSearch Search = new clsSearch();
        private clsFunction Fn = new clsFunction();
        private clsImage TSSImage = new clsImage();
        private clsInsert Insert = new clsInsert();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsMessage Message = new clsMessage();

        public string[,] Parameter = new string[,] { };
        private CultureInfo En = new CultureInfo("en-US");

        public FrmImageEditer(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            List.GetList(cbbPath, "Y", "Path");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
           
        }

        private void cbOriginal_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOriginal.Checked == true)
            {
                cbbPath.Enabled = true;
                cbbPath.SelectedValue = 0;
            }
            else
            {
                cbbPath.Enabled = false;
                cbbPath.SelectedValue = 0;
            }
        }

        private void btnAddEdit_Click(object sender, EventArgs e)
        {
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
        }
    }
}