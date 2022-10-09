using System;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmSeeMovie : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsImage TSSImage = new clsImage();
        private clsCryption Cryption = new clsCryption();

        public string strSql = "";

        public FrmSeeMovie(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmSeeMovie_Load(object sender, EventArgs e)
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
            string A = Cryption.Encrypt("thorst021232");
            string B = Cryption.Decrypt(A);

            Message.ShowMesInfo(A + " - " + B);
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