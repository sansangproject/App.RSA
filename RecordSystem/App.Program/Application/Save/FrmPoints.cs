using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;
using SANSANG.Constant;
using SANSANG.Utilites.App.Model;
using RecordSystemApplication.App.Program.Application.Payment;

namespace SANSANG
{
    public partial class FrmPoints : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string strAppCode = "SAVPON00";
        public string strAppName = "FrmSavePoint";
        public string strErr = "";
        public string strLaguage;
        public string strOpe = "";
        public int row = 0;
        public double sum = 0;
        public bool SearchPress = false;
        public bool Start = true;

        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
        private clsDataList List = new clsDataList();
        private dbConnection db = new dbConnection();
        private clsDate Date = new clsDate();
        private clsLog Log = new clsLog();
        private clsSetting setting = new clsSetting();
        private clsHelpper Helper = new clsHelpper();
        private clsEvent Event = new clsEvent();

        private DataListConstant DataList = new DataListConstant();
        private StringConstant Str = new StringConstant();
        private PaymentConstant Payment = new PaymentConstant();

        private DataTable dt = new DataTable();
        private Timer timer = new Timer();

        public FrmPoints(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
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
            List.GetListId(cbbShop, DataList.ShopId);

            Clear(true);
            timer.Stop();
        }

        public void Clear(bool IsLoad)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }


    }
}