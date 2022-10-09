using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; 
using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmChkError : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "SETDAE00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string filePath = "-";
        public string fileName = "-";
        public string fileType = ".jpg";

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();

        private clsImage clsImage = new clsImage();
        private clsSearch Search = new clsSearch();
        private clsFunction Fn = new clsFunction(); private clsMessage Mes = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsMessage CallMessage = new clsMessage();
        private clsDataList List = new clsDataList();

        public string[,] Parameter = new string[,] { };

        public FrmChkError(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmChkError_Load(object sender, EventArgs e)
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //int round = 150;

            //for (int i = 1; i <= round; i++)
            //{
            //    string[,] Parameter = new string[,]
            //     {
            //    };

            //    db.Get("Spr_S_TblSaveExpenses", Parameter, out strErr, out dt);

            //        clsFunction Function = new clsFunction();
            //        string strCode  = "";
            //        string strTime  = Convert.ToString(DateTime.Now.ToString("HH:mm:ss"));
            //        DateTime dts    = Convert.ToDateTime(dt.Rows[0]["ExpenseDate"].ToString());
            //        string strDate  = Convert.ToString(dts.ToString("yyMMdd"));
            //        string strUserId    = dt.Rows[0]["ExpenseId"].ToString();

            //        String strYear = Convert.ToString(Convert.ToInt32(strDate.Substring(0, 2)) < 50 ?
            //                         Convert.ToInt32("20" + strDate.Substring(0, 2)) + 543 : Convert.ToInt32("25" + strDate.Substring(0, 2)));
            //        String strMonth = strDate.Substring(2, 2);
            //        String strDay = strDate.Substring(4, 2);

            //        var dateValue = Convert.ToDateTime(strDay + "/" + strMonth + "/" + strYear + " " + strTime);

            //        DateTime dte = Convert.ToDateTime(dateValue);

            //        strCode = Fn.getCode(i, "E", dte);
            //        Parameter = new string[,]
            //        {
            //            {"@ExpenseId",          strUserId},
            //            {"@ExpenseCode",        strCode},
            //            {"@User",               strUserId},

            //        };

            //        db.Get("Spr_U_TblSaveExpenses", Parameter, out strErr, out dt);
            //        Thread.Sleep(1000);

            //}
        }
    }
}