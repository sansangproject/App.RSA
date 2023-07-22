using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmManageMoney : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "MANMN00";
        public string AppName = "FrmManageMoney";
        public string Error = "";

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        private StoreConstant Store = new StoreConstant();
        private OperationConstant Operation = new OperationConstant();
        private DataListConstant DataList = new DataListConstant();
        private clsDelete Delete = new clsDelete();
        private clsEdit Edit = new clsEdit();
        private clsInsert Insert = new clsInsert();
        private clsFunction Function = new clsFunction();
        private clsMessage Message = new clsMessage();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();
        private clsImage Images = new clsImage();
        private TableConstant Table = new TableConstant();
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(25);
        private clsHelpper Helper = new clsHelpper();
        private Timer Timer = new Timer();
        public string[,] Parameter = new string[,] { };

        public FrmManageMoney(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
        {
            InitializeComponent();

            UserId = UserIdLogin;
            UserName = UserNameLogin;
            UserSurname = UserSurNameLogin;
            UserType = UserTypeLogin;
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            Loading.Show();
            Timer.Interval = (1000);
            Timer.Start();
            Timer.Tick += new EventHandler(LoadList);
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "0"));
            //pb_Thai_True.Hide();
            //pb_Thai_False.Show();
            //gbForm.Enabled = true;
            //Clear();
            Timer.Stop();
        }














        //public void Clear()
        //{
        //    txtCode.Enabled = true;
        //    txtCode.Text = "";
        //    txtCode.Focus();
        //    txtName.Text = "";
        //    txtEn.Text = "";
        //    cbbStatus.SelectedValue = "0";
        //    lblSearch.Text = "";

        //    string[,] Parameter = new string[,]
        //        {
        //            {"@MsMoneyCode", ""},
        //            {"@MsMoneyNameTh", ""},
        //            {"@MsMoneyNameEn", ""},
        //            {"@MsMoneyStatus", "0"},
        //        };

        //    db.Get("Spr_S_TblMasterMoney", Parameter, out strErr, out dt);
        //    getDataGrid(dt);
        //}

        //public void getDataGrid(DataTable dt)
        //{
        //    int row = dt.Rows.Count;

        //    if (row == 0)
        //    {
        //        dataGridView.DataSource = null;
        //        picExcel.Visible = false;
        //        lblCount.Text = "0";
        //    }
        //    else
        //    {
        //        DataTable dtGrid = new DataTable();
        //        dtGrid = dt.DefaultView.ToTable(true, "MsMoneyCode", "MsMoneyNameTh", "MsStatusNameTh", "MsMoneyCreateBy", "MsMoneyCreateDate", "MsMoneyId");

        //        DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
        //        DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

        //        Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
        //            "ลำดับ", 50, true, mc, mc
        //            , "รหัส", 200, true, ml, ml
        //            , "ชื่อรายการ", 250, true, ml, ml
        //            , "สถานะ", 250, true, ml, ml
        //            , "ผู้สร้าง", 100, true, ml, ml
        //            , "วันที่สร้าง", 100, true, mc, mc
        //            , "", 0, false, mc, mc
        //            );

        //        picExcel.Visible = true;
        //        lblCount.Text = row.ToString();
        //    }
        //}

        //private void btnExit_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        //private void btnClear_Click(object sender, EventArgs e)
        //{
        //    Clear();
        //}

        //private void picExcel_Click(object sender, EventArgs e)
        //{
        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    strOpe = "D";

        //    string[,] Parameter = new string[,]
        //        {
        //           {"@MsMoneyCode", txtCode.Text},
        //           {"@DeleteType", "0"},
        //           {"@User", strUserId},
        //        };

        //    bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

        //    if (Action == true)
        //    {
        //        db.Operations("Spr_D_TblMasterMoney", Parameter, out strErr);

        //        if (strErr == null)
        //        {
        //            Message.MessageResult(strOpe, "C", strErr);
        //            Clear();
        //        }
        //        else
        //        {
        //            Message.MessageResult(strOpe, "E", strErr);
        //        }
        //    }
        //}

        //private void btnEdit_Click(object sender, EventArgs e)
        //{
        //    strOpe = "U";

        //    string[,] Parameter = new string[,]
        //        {
        //            {"@MsMoneyCode", txtCode.Text},
        //            {"@MsMoneyNameTh", txtName.Text},
        //            {"@MsMoneyNameEn", txtEn.Text},
        //            {"@MsMoneyStatus",cbbStatus.SelectedValue.ToString()},
        //            {"@User",strUserId },
        //        };

        //    bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

        //    if (Action == true)
        //    {
        //        db.Operations("Spr_U_TblMasterMoney", Parameter, out strErr);

        //        if (strErr == null)
        //        {
        //            Message.MessageResult(strOpe, "C", strErr);
        //            Clear();
        //        }
        //        else
        //        {
        //            Message.MessageResult(strOpe, "E", strErr);
        //        }
        //    }
        //}

        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    SearchData();
        //}

        //public void SearchData()
        //{
        //    string[,] Parameter = new string[,]
        //        {
        //            {"@MsMoneyCode", txtCode.Text},
        //            {"@MsMoneyNameTh", txtName.Text},
        //            {"@MsMoneyNameEn", txtEn.Text},
        //            {"@MsMoneyStatus", cbbStatus.SelectedValue.ToString()},
        //        };

        //    db.Get("Spr_S_TblMasterMoney", Parameter, out strErr, out dt);
        //    getDataGrid(dt);
        //}

        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    strOpe = "I";

        //    string[,] Parameter = new string[,]
        //        {
        //            {"@MsMoneyCode", txtCode.Text},
        //            {"@MsMoneyNameTh", txtName.Text},
        //            {"@MsMoneyNameEn", txtEn.Text},
        //            {"@MsMoneyStatus",cbbStatus.SelectedValue.ToString()},
        //            {"@User",strUserId },
        //        };

        //    bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

        //    if (Action == true)
        //    {
        //        db.Operations("Spr_I_TblMasterMoney", Parameter, out strErr);

        //        if (strErr == null)
        //        {
        //            Message.MessageResult(strOpe, "C", strErr);
        //            Clear();
        //        }
        //        else
        //        {
        //            Message.MessageResult(strOpe, "E", strErr);
        //        }
        //    }
        //}

        //private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0)
        //    {
        //        DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
        //        DataTable dt = new DataTable();

        //        string[,] Parameter = new string[,]
        //        {
        //            {"@MsMoneyCode", row.Cells["MsMoneyCode"].Value.ToString()},
        //            {"@MsMoneyNameTh", ""},
        //            {"@MsMoneyNameEn",""},
        //            {"@MsMoneyStatus", "0"},
        //        };

        //        db.Get("Spr_S_TblMasterMoney", Parameter, out strErr, out dt);

        //        txtCode.Text = dt.Rows[0]["MsMoneyCode"].ToString();
        //        txtName.Text = dt.Rows[0]["MsMoneyNameTh"].ToString();
        //        cbbStatus.SelectedValue = dt.Rows[0]["MsMoneyStatus"].ToString();
        //        txtEn.Text = dt.Rows[0]["MsMoneyNameEn"].ToString();
        //    }
        //}
    }
}