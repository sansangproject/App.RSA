using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmMaster : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string strAppCode = "FRMCO00";
        public string strAppName = "FormName";
        public string strLaguage = "th";
        public string strErr = "";

        private clsDataList List = new clsDataList();
        private DataListConstant DataList = new DataListConstant();
        private clsLog Log = new clsLog();
        private clsFunction Fn = new clsFunction();

        private Timer timer = new Timer();
        private DataTable dt = new DataTable();
        public string[,] Parameter = new string[,] { };
        
        public FrmMaster(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            try
            {
                InitializeComponent();
                strUserId = userIdLogin;
                strUserName = userNameLogin;
                strUserSurname = userSurNameLogin;
                strUserType = userTypeLogin;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            try
            {
                int sec = 2;
                timer.Interval = (sec * 1000);
                timer.Tick += new EventHandler(LoadList);
                timer.Start();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }
        private void LoadList(object sender, EventArgs e)
        {
            strLaguage = clsSetting.ReadLanguageSetting();
            //List.GetList(cbbName, "Y", DataList.Money);
            Clear();
            timer.Stop();
        }

        public void Clear()
        {
          
        }

        public void GetDataGrid(DataTable dt)
        {
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            
        }

        private string GetCondition()
        {
            return "";
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
            Clear();
        }

        private void picExcel_Click(object sender, EventArgs e)
        {

        }

        private void Frm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string keyCode = Fn.keyPress(sender, e);

                if (keyCode == "Ctrl+S")
                {
                    btnAdd_Click(sender, e);
                }

                if (keyCode == "Ctrl+E")
                {
                    btnEdit_Click(sender, e);
                }

                if (keyCode == "Ctrl+D")
                {
                    btnDelete_Click(sender, e);
                }

                if (keyCode == "Ctrl+X")
                {
                    btnExit_Click(sender, e);
                }

                if (keyCode == "Ctrl+F")
                {
                    btnSearch_Click(sender, e);
                }

                if (keyCode == "Alt+C")
                {
                    btnClear_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }
    }
}