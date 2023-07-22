using System;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmManagePatient : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "PATMA00";
        public string AppName = "FrmManagePatient";
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
        private ColumnConstant Column = new ColumnConstant();
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(25);
        private clsHelpper Helper = new clsHelpper();
        private Timer Timer = new Timer();
        public string[,] Parameter = new string[,] { };

        public FrmManagePatient(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
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
            List.GetLists(cbbCategory, string.Format(DataList.CategoryId, "0"));
            List.GetList(cbbType, DataList.PayTypes);
            pb_Thai_True.Hide();
            pb_Thai_False.Show();
            gbForm.Enabled = true;
            Clear();
            Timer.Stop();
        }

        public void Clear()
        {
            Function.ClearAll(gbForm);
            Search(false);
        }

        public void Search(bool Search)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", Search ? txtId.Text : ""},
                    {"@Code", Search ? txtCode.Text : ""},
                    {"@Name", Search ? txtName.Text : ""},
                    {"@NameEn", Search ? txtNameEn.Text : ""},
                    {"@Status", Search ? Function.GetComboId(cbbStatus) : "0"},
                    {"@User", ""},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.SelectAbbr},
                };

                string Condition = Function.ShowConditons(GetCondition());
                lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;
                db.Get(Store.ManageItem, Parameter, out Error, out dt);
                ShowGridView(dt);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";

                strCondition += txtCode.Text != "" ? ", รหัสอ้างอิง: " + txtCode.Text : "";
                strCondition += cbbStatus.Text != ":: กรุณาเลือก ::" ? ", สถานะ: " + cbbStatus.Text : "";
                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return "";
            }
        }

        public void ShowGridView(DataTable dt)
        {
            try
            {
                if (Function.GetRows(dt) == 0)
                {
                    GridView.DataSource = null;
                    txtCount.Text = Function.ShowNumberOfData(0);
                }
                else
                {
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "SNo", "Code", "Display", "Categorys", "Types", "Dates", "Id");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                    Function.showGridViewFormatFromStore(dtGrid, GridView,
                          "ลำดับ", 50, true, mc, mc
                        , "รหัส", 150, true, ml, ml
                        , "ชื่อรายการ", 200, true, ml, ml
                        , "ประเภท", 200, true, ml, ml
                        , "สถานะ", 100, true, ml, ml
                        , "ข้อมูล ณ วันที่", 150, true, mc, mc
                        , "", 0, false, mc, mc
                        );

                    txtCount.Text = Function.ShowNumberOfData(dt.Rows.Count);
                    GridView.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }










        //private void FrmPATMA00_Load(object sender, EventArgs e)
        //{


        //    List.GetList(cbbStatus, "0", "Status");
        //    List.GetList(cbbHospital, "Y", "HospitalId");

        //    Clear();
        //}

        //public void Clear()
        //{
        //    lblSearch.Text = "";
        //    txtCode.Text = "";
        //    txtName.Text = "";
        //    txtNumber.Text = "";
        //    txtCode.Focus();

        //    cbbStatus.SelectedValue = 0;
        //    cbbHospital.SelectedValue = 0;

        //    Parameter = new string[,]
        //        {
        //            {"@MsHospitalCode", "0"},
        //            {"@MsPatientCode", ""},
        //            {"@MsPatientNumber", ""},
        //            {"@MsPatientName", ""},
        //            {"@MsPatientStatus", "0"},
        //        };

        //    db.Get("Spr_S_TblMasterPatient", Parameter, out strErr, out dt);
        //    getDataGrid(dt);
        //}

        //public void getDataGrid(DataTable dt)
        //{
        //    int row = dt.Rows.Count;

        //    if (row == 0)
        //    {
        //        //Fn.ShowImageNull();
        //        dataGridView.DataSource = null;
        //        picExcel.Visible = false;
        //        lblCount.Text = "0";
        //    }
        //    else
        //    {
        //        DataTable dtGrid = new DataTable();
        //        dtGrid = dt.DefaultView.ToTable(true, "MsPatientCode", "MsPatientName", "MsHospitalName", "MsHospitalLocationName", "MsStatusNameTh", "MsPatientId");

        //        DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
        //        DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

        //        Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
        //            "ลำดับ", 30, true, mc, mc
        //            , "เลขประจำตัวผู้ป่วย", 250, true, ml, ml
        //            , "ชื่อ - สกุล", 250, true, ml, ml
        //            , "สถาพยาบาล", 300, true, ml, ml
        //            , "ที่อยู่", 300, true, ml, ml
        //            , "สถานะ", 100, true, ml, ml
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

        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    SearchData();
        //}

        //public void SearchData()
        //{
        //    Parameter = new string[,]
        //        {
        //            {"@MsHospitalCode", cbbHospital.SelectedValue.ToString()},
        //            {"@MsPatientCode", txtCode.Text},
        //            {"@MsPatientNumber", txtNumber.Text},
        //            {"@MsPatientName", txtName.Text},
        //            {"@MsPatientStatus", cbbStatus.SelectedValue.ToString()},
        //        };

        //    db.Get("Spr_S_TblMasterPatient", Parameter, out strErr, out dt);
        //    getDataGrid(dt);
        //}

        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    txtCode.Text = Fn.GetCodes("85", "", "Generated");

        //    strOpe = "I";

        //    string[,] Parameter = new string[,]
        //        {
        //            {"@MsHospitalCode", cbbHospital.SelectedValue.ToString()},
        //            {"@MsPatientCode", txtCode.Text},
        //            {"@MsPatientNumber", txtNumber.Text},
        //            {"@MsPatientName", txtName.Text},
        //            {"@MsPatientStatus", cbbStatus.SelectedValue.ToString()},
        //            {"@User", strUserId},
        //        };

        //    bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

        //    if (Action == true)
        //    {
        //        db.Operations("Spr_I_TblMasterPatient", Parameter, out strErr);

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

        //        Parameter = new string[,]
        //        {
        //            {"@MsHospitalCode", "0"},
        //            {"@MsPatientCode", row.Cells["MsPatientCode"].Value.ToString()},
        //            {"@MsPatientNumber", ""},
        //            {"@MsPatientName", ""},
        //            {"@MsPatientStatus", "0"},
        //        };

        //        db.Get("Spr_S_TblMasterPatient", Parameter, out strErr, out dt);

        //        txtCode.Text = dt.Rows[0]["MsPatientCode"].ToString();
        //        txtName.Text = dt.Rows[0]["MsPatientName"].ToString();
        //        txtNumber.Text = dt.Rows[0]["MsPatientNumber"].ToString();
        //        cbbHospital.SelectedValue = dt.Rows[0]["MsHospitalCode"].ToString();
        //        cbbStatus.SelectedValue = dt.Rows[0]["MsPatientStatus"].ToString();
        //    }
        //}

        //private void btnEdit_Click(object sender, EventArgs e)
        //{
        //    strOpe = "U";

        //    string[,] Parameter = new string[,]
        //        {
        //            {"@MsHospitalCode", cbbHospital.SelectedValue.ToString()},
        //            {"@MsPatientCode", txtCode.Text},
        //            {"@MsPatientNumber", txtNumber.Text},
        //            {"@MsPatientName", txtName.Text},
        //            {"@MsPatientStatus", cbbStatus.SelectedValue.ToString()},
        //            {"@User", strUserId},
        //        };

        //    bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

        //    if (Action == true)
        //    {
        //        db.Operations("Spr_U_TblMasterPatient", Parameter, out strErr);

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

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    strOpe = "D";

        //    string[,] Parameter = new string[,]
        //        {
        //           {"@MsPatientCode", txtCode.Text},
        //        };

        //    bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

        //    if (Action == true)
        //    {
        //        db.Operations("Spr_D_TblMasterPatient", Parameter, out strErr);

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

        //private void btnClear_Click(object sender, EventArgs e)
        //{
        //    Clear();
        //}

        //private void picExcel_Click(object sender, EventArgs e)
        //{
        //}

        //private void FrmPATMA00_KeyDown(object sender, KeyEventArgs e)
        //{
        //    string keyCode = Fn.keyPress(sender, e);

        //    if (keyCode == "Ctrl+S")
        //    {
        //        btnAdd_Click(sender, e);
        //    }

        //    if (keyCode == "Ctrl+E")
        //    {
        //        btnEdit_Click(sender, e);
        //    }

        //    if (keyCode == "Ctrl+D")
        //    {
        //        btnDelete_Click(sender, e);
        //    }

        //    if (keyCode == "Ctrl+X")
        //    {
        //        btnExit_Click(sender, e);
        //    }

        //    if (keyCode == "Alt+F")
        //    {
        //        btnSearch_Click(sender, e);
        //    }

        //    if (keyCode == "Alt+C")
        //    {
        //        btnClear_Click(sender, e);
        //    }
        //}

        //private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == Convert.ToChar(Keys.Enter))
        //    {
        //        SearchData();
        //    }
        //}
    }
}