using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmManageBranch : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "MANBR00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string filePath = "-";
        public string fileName = "-";
        public string fileType = ".jpg";

        public bool start = false;

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsBarcode Barcode = new clsBarcode();
        private Timer timer = new Timer();

        public string[,] Parameter = new string[,] { };

        public FrmManageBranch(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmManageBranch_Load(object sender, EventArgs e)
        {
            int sec = 2;
            timer.Interval = (sec * 200);
            timer.Tick += new EventHandler(LoadList);
            timer.Start();
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetList(cbbStatus, "0", "Status");
            List.GetList(cbbBank, "Y", "Bank");
            List.getWhereList(cbbProvince, "", "", "Y", "ProvinceAll");
            start = true;
            gbFrm.Enabled = true;
            Clear();
            timer.Stop();
        }

        public void Clear()
        {
            txtCode.Text = "";
            txtCode.Focus();

            txtAddress.Text = "";
            txtNameEn.Text = "";
            txtNameTh.Text = "";
            txtTime.Text = "";
            txtDay.Text = "";

            cbbStatus.SelectedValue = "0";
            cbbBank.SelectedValue = "0";
            cbbProvince.SelectedValue = "0";
            pbBarcode.Image = null;

            string[,] Parameter = new string[,]
                {
                    {"@MsBranchId",""},
                    {"@MsBankCode",""},
                    {"@MsBranchCode",""},
                    {"@MsBranchNameTh",""},
                    {"@MsBranchNameEn",""},
                    {"@MsBranchPoint",""},
                    {"@MsBranchAddress",""},
                    {"@MsBranchNo",""},
                    {"@MsBranchBuilding",""},
                    {"@MsBranchMoo",""},
                    {"@MsBranchSoi",""},
                    {"@MsBranchRoad",""},
                    {"@MsBranchPostcode",""},
                    {"@MsBranchPhone",""},
                    {"@MsBranchStartDate",""},
                    {"@MsBranchWorkingDays",""},
                    {"@MsBranchWorkingTimes",""},
                    {"@MsBranchStatus", "0"},
                    {"@TambolId", "0"},
                    {"@AmphurId", "0"},
                    {"@ProvinceId", "0"},
                };

            db.Get("Spr_S_TblMasterBankBranch", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        public void getDataGrid(DataTable dt)
        {
            int row = dt.Rows.Count;

            if (row == 0)
            {
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                lblCount.Text = "0 รายการ";
            }
            else
            {
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "MsBranchCode", "MsBranchNameTh", "MsBankNameTh", "ProvinceNameTh", "MsStatusNameTh", "MsBranchId");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                      "ลำดับ", 50, true, mc, mc
                    , "รหัส", 100, true, mc, mc
                    , "ชื่อสาขา", 200, true, ml, ml
                    , "ธนาคาร", 200, true, ml, ml
                    , "จังหวัด", 100, true, mc, ml
                    , "สถานะ", 100, true, mc, mc
                    , "", 0, false, mc, mc
                    );

                picExcel.Visible = true;
                lblCount.Text = string.Format("{0:#,##0}", Convert.ToInt64(dt.Rows[0]["DataRow"].ToString())) + " รายการ";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void picExcel_Click(object sender, EventArgs e)
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text != "")
                {
                    strOpe = "D";

                    string[,] Parameter = new string[,]
                    {
                        {"@MsBranchId", txtId.Text},
                        {"@DeleteType", "1" },
                        {"@User", strUserId },
                    };

                    bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

                    if (Action == true)
                    {
                        db.Operations("Spr_D_TblMasterBankBranch", Parameter, out strErr);

                        if (strErr == null)
                        {
                            Message.MessageResult(strOpe, "C", strErr);
                            Clear();
                        }
                        else
                        {
                            Message.MessageResult(strOpe, "E", strErr);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text != "")
                {
                    strOpe = "U";

                    string[,] Parameter = new string[,]
                    {
                        {"@User", strUserId },
                        {"@MsBranchId", txtId.Text },
                        {"@MsBankCode", cbbBank.SelectedValue.ToString()},
                        {"@MsBranchCode", txtCode.Text },
                        {"@MsBranchNameTh", txtNameTh.Text },
                        {"@MsBranchNameEn", txtNameEn.Text },
                        {"@MsBranchPoint", "" },
                        {"@MsBranchAddress", txtAddress.Text },
                        {"@MsBranchNo", "" },
                        {"@MsBranchBuilding", "" },
                        {"@MsBranchMoo", "" },
                        {"@MsBranchSoi", "" },
                        {"@MsBranchRoad", "" },
                        {"@TambolId", "" },
                        {"@AmphurId", "" },
                        {"@ProvinceId", cbbProvince.SelectedValue.ToString() },
                        {"@MsBranchPostcode", "" },
                        {"@MsBranchPhone", "" },
                        {"@MsBranchStartDate", "" },
                        {"@MsBranchWorkingDays", txtDay.Text },
                        {"@MsBranchWorkingTimes", txtTime.Text },
                        {"@MsBranchStatus", cbbStatus.SelectedValue.ToString() },
                    };

                    bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

                    if (Action == true)
                    {
                        db.Operations("Spr_U_TblMasterBankBranch", Parameter, out strErr);

                        if (strErr == null)
                        {
                            Message.MessageResult(strOpe, "C", strErr);
                            Clear();
                        }
                        else
                        {
                            Message.MessageResult(strOpe, "E", strErr);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            string[,] Parameter = new string[,]
            {
                {"@MsBranchId", ""},
                {"@MsBankCode", cbbBank.SelectedValue.ToString()},
                {"@MsBranchCode", txtCode.Text},
                {"@MsBranchNameTh", txtNameTh.Text},
                {"@MsBranchNameEn", txtNameEn.Text},
                {"@MsBranchPoint",""},
                {"@MsBranchAddress", txtAddress.Text},
                {"@MsBranchNo",""},
                {"@MsBranchBuilding",""},
                {"@MsBranchMoo",""},
                {"@MsBranchSoi",""},
                {"@MsBranchRoad",""},
                {"@MsBranchPostcode",""},
                {"@MsBranchPhone",""},
                {"@MsBranchStartDate",""},
                {"@MsBranchWorkingDays", txtDay.Text},
                {"@MsBranchWorkingTimes", txtTime.Text},
                {"@MsBranchStatus", cbbStatus.SelectedValue.ToString()},
                {"@TambolId", "0"},
                {"@AmphurId", "0"},
                {"@ProvinceId", cbbProvince.SelectedValue.ToString()},
            };

            db.Get("Spr_S_TblMasterBankBranch", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtCode.Text = Fn.GetCodes("72", "", "Generated");
            strOpe = "I";

            string[,] Parameter = new string[,]
            {
                {"@User", strUserId },
                {"@MsBranchId", txtId.Text },
                {"@MsBankCode", cbbBank.SelectedValue.ToString() },
                {"@MsBranchCode", txtCode.Text },
                {"@MsBranchNameTh", txtNameTh.Text },
                {"@MsBranchNameEn", txtNameEn.Text },
                {"@MsBranchPoint", "" },
                {"@MsBranchAddress", txtAddress.Text },
                {"@MsBranchNo", "" },
                {"@MsBranchBuilding", "" },
                {"@MsBranchMoo", "" },
                {"@MsBranchSoi", "" },
                {"@MsBranchRoad", "" },
                {"@TambolId", "0" },
                {"@AmphurId", "0" },
                {"@ProvinceId", cbbProvince.SelectedValue.ToString()},
                {"@MsBranchPostcode", "" },
                {"@MsBranchPhone", "" },
                {"@MsBranchStartDate", "" },
                {"@MsBranchWorkingDays", txtDay.Text },
                {"@MsBranchWorkingTimes", txtTime.Text },
                {"@MsBranchStatus", cbbStatus.SelectedValue.ToString() },
            };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_I_TblMasterBankBranch", Parameter, out strErr);

                if (strErr == null)
                {
                    Message.MessageResult(strOpe, "C", strErr);
                    Clear();
                }
                else
                {
                    Message.MessageResult(strOpe, "E", strErr);
                }
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                DataTable dt = new DataTable();

                string[,] Parameter = new string[,]
                {
                    {"@MsBranchId", row.Cells["MsBranchId"].Value.ToString()},
                    {"@MsBankCode",""},
                    {"@MsBranchCode",""},
                    {"@MsBranchNameTh",""},
                    {"@MsBranchNameEn",""},
                    {"@MsBranchPoint",""},
                    {"@MsBranchAddress",""},
                    {"@MsBranchNo",""},
                    {"@MsBranchBuilding",""},
                    {"@MsBranchMoo",""},
                    {"@MsBranchSoi",""},
                    {"@MsBranchRoad",""},
                    {"@MsBranchPostcode",""},
                    {"@MsBranchPhone",""},
                    {"@MsBranchStartDate",""},
                    {"@MsBranchWorkingDays",""},
                    {"@MsBranchWorkingTimes",""},
                    {"@MsBranchStatus", "0"},
                    {"@TambolId", "0"},
                    {"@AmphurId", "0"},
                    {"@ProvinceId", cbbProvince.SelectedValue.ToString()},

                };

                db.Get("Spr_S_TblMasterBankBranch", Parameter, out strErr, out dt);

                txtId.Text = dt.Rows[0]["MsBranchId"].ToString();
                txtCode.Text = dt.Rows[0]["MsBankCode"].ToString();
                txtDay.Text = dt.Rows[0]["MsBranchWorkingDays"].ToString();
                txtTime.Text = dt.Rows[0]["MsBranchWorkingTimes"].ToString();
                txtNameEn.Text = dt.Rows[0]["MsBranchNameEn"].ToString();
                txtNameTh.Text = dt.Rows[0]["MsBranchNameTh"].ToString();
                txtAddress.Text = dt.Rows[0]["MsBranchAddress"].ToString();
                cbbBank.SelectedValue = dt.Rows[0]["MsBankCode"].ToString();
                cbbStatus.SelectedValue = dt.Rows[0]["MsBranchStatus"].ToString();
                cbbProvince.SelectedValue = dt.Rows[0]["ProvinceId"].ToString();
                pbBarcode.Image = Barcode.QRCode(dt.Rows[0]["MsBranchId"].ToString(), Color.Black, Color.White, "H", 4, true);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
        }

        private void FrmManageBranch_KeyDown(object sender, KeyEventArgs e)
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
            if (keyCode == "Altl+C")
            {
                btnClear_Click(sender, e);
            }
            if (keyCode == "Ctrl+P")
            {
                btnPrint_Click(sender, e);
            }
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchData();
            }
        }
    }
}