using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmSettingPath : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string strAppCode = "SETPAT00";
        public string strAppName = "FrmSettingPath";
        public string strErr = "";
        public string strLaguage = "EN";
        public string strOpe = "";

        public string filePath = "-";
        public string fileName = "-";
        public string fileType = ".jpg";

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();
        private StoreConstant Store = new StoreConstant();
        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsEdit Edit = new clsEdit();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();
        private TableConstant Tb = new TableConstant();

        private Timer timer = new Timer();
        public bool start = true;
        public bool NewData = true;

        public string[,] Parameter = new string[,] { };

        public FrmSettingPath(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmSettingPath_Load(object sender, EventArgs e)
        {
            int sec = 1;
            timer.Interval = (sec * 1000);
            timer.Start();
            timer.Tick += new EventHandler(LoadList);
        }
        private void LoadList(object sender, EventArgs e)
        {
            start = true;

            List.GetList(cbbStatus, "0", "Status");

            gbFrm.Enabled = true;
            Clear();
            timer.Stop();
        }

        public void Clear()
        {
            txtName.Text = "";
            txtDetail.Text = "";
            txtLocations.Text = "";
            txtCode.Text = "";
            cbbStatus.SelectedValue = 0;

            Parameter = new string[,]
            {
                {"@Id",""},
                {"@Code", ""},
                {"@Name",""},
                {"@Location",""},
                {"@Detail",""},
                {"@Status","0"},
                {"@User",""},
            };

            db.Get("Store.SelectPath", Parameter, out strErr, out dt);
            GetDataGrid(dt);
            dataGridView.Focus();
        }

        public void GetDataGrid(DataTable dt)
        {
            if (Fn.GetRows(dt) == 0)
            {
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                txtNumberNow.Focus();
                txtNumberNow.Text = Fn.ShowNumberOfData(0);
                NewData = true;
            }
            else
            {
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "Code", "Detail", "Location", "PathDate", "StatusNameTh", "Id");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                  "ลำดับ", 30, true, mc, mc
                , "รหัสอ้างอิง", 120, true, ml, ml
                , "ชื่อแฟ้ม", 300, true, ml, ml
                , "ที่อยู่ไฟล์", 370, true, ml, ml
                , "ข้อมูล ณ วันที่", 130, true, mc, mc
                , "สถานะ", 100, true, mc, mc
                , "", 0, false, mc, mc
                );

                picExcel.Visible = true;
                NewData = false;
                txtNumberNow.Text = Fn.ShowNumberOfData(dt.Rows.Count);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtLocations.Text != "")
            {
                if (!Fn.IsDuplicates("MST_Path", txtName.Text, txtLocations.Text, Detail: "Path " + txtName.Text))
                {
                    txtCode.Text = txtCode.Text = Fn.GetCodes("84", "", "Generated");

                    Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", txtCode.Text},
                        {"@Name", txtName.Text},
                        {"@Location", txtLocations.Text},
                        {"@Detail", txtDetail.Text},
                        {"@Status", Fn.getComboBoxValue(cbbStatus)},
                        {"@User", strUserId},
                    };

                    if (Insert.Add(strAppCode, strAppName, strUserId, "Store.InsertPath", Parameter, txtCode.Text, "Path " + txtName.Text))
                    {
                        Clear();
                    }
                }
            }
            else
            {
                Message.ShowRequestData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", txtCode.Text},
                    {"@Name", txtName.Text},
                    {"@Location", txtLocations.Text},
                    {"@Detail", txtDetail.Text},
                    {"@Status", Fn.getComboBoxValue(cbbStatus)},
                    {"@User", strUserId},
                };

                if (Edit.Update(strAppCode, strAppName, strUserId, "Store.UpdatePath", Parameter, txtCode.Text, "Path " + txtName.Text))
                {
                    Clear();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                if (Delete.Drop(strAppCode, strAppName, strUserId, 0, Tb.Path, txtCode, "Path " + txtName.Text))
                {
                    Clear();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Open = new FolderBrowserDialog();

            if (Open.ShowDialog() == DialogResult.OK)
            {
                txtLocations.Text = Open.SelectedPath + "\\";
            }
        }

        private void cbbLocations_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void SearchData(bool Search)
        {
            Parameter = new string[,]
            {
                {"@Id", ""},
                {"@Code", Search ? txtCode.Text : ""},
                {"@Name", Search ? txtName.Text : ""},
                {"@Location", Search ? txtLocations.Text : ""},
                {"@Detail", Search ? txtDetail.Text : ""},
                {"@Status", Search ? Fn.getComboBoxValue(cbbStatus) : ""},
                {"@User", ""},
            };

            db.Get("Store.SelectPath", Parameter, out strErr, out dt);
            GetDataGrid(dt);
            ShowData(dt);
        }
        private void ShowData(DataTable dt)
        {
            try
            {
                if (Fn.GetRows(dt) > 0)
                {
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    txtDetail.Text = dt.Rows[0]["Detail"].ToString();
                    txtLocations.Text = dt.Rows[0]["Location"].ToString();
                    txtCode.Text = dt.Rows[0]["Code"].ToString();

                    cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                    dataGridView.Focus();
                }
            }
            catch (Exception)
            {

            }
        }


        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                    DataTable dt = new DataTable();

                    Parameter = new string[,]
                    {
                        {"@Id", row.Cells["Id"].Value.ToString()},
                        {"@Code", ""},
                        {"@Name",""},
                        {"@Location",""},
                        {"@Detail",""},
                        {"@Status","0"},
                        {"@User",""},
                    };

                    db.Get("Store.SelectPath", Parameter, out strErr, out dt);
                    ShowData(dt);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData(true);
        }
    }
}