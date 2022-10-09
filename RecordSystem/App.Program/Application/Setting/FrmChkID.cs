using System;
using System.Windows.Forms;
using SANSANG.Class; 
using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmChkID : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        private clsSearch clsSearch = new clsSearch();
        private clsDelete clsDelete = new clsDelete();
        private clsInsert clsInsert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Mes = new clsMessage();

        public string strSql = "";

        public FrmChkID(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmChkID_Load(object sender, EventArgs e)
        {
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
        }

        private void picExcel_Click(object sender, EventArgs e)
        {
        }
    }
}