using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmSearchHospital : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "SEAHOS00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string filePath = "-";
        public string fileName = "-";
        public string fileType = ".jpg";

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();

        public string[,] Parameter = new string[,] { };

        public FrmSearchHospital(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmSearchHospital_Load(object sender, EventArgs e)
        {
            
            List.GetList(cbbStatus, "4", "Status");

            Clear();
        }

        public void Clear()
        {
            txtSearch.Text = "";
            txtSearch.Focus();
            txtId.Text = "";
            txtCode.Text = "";
            txtName.Text = "";
            txtUnderName.Text = "";
            txtUnderCode.Text = "";
            txtTypeCode.Text = "";
            txtTypeName.Text = "";
            txtAddress.Text = "";
            txtName.Text = "";
            txtPost.Text = "";
            txtPhone.Text = "";
            lblSearch.Text = "";
            txtNumber.Text = "";

            cbbStatus.SelectedValue = 0;

            string[,] Parameter = new string[,]
                    {
                        {"@SearchName", ""},
                        {"@MsHospitalId", ""},
                        {"@MsHospitalNumber", ""},
                        {"@MsHospitalName", ""},
                        {"@MsHospitalUnderId", ""},
                        {"@MsHospitalUnderName", ""},
                        {"@MsHospitalTypeId", ""},
                        {"@MsHospitalTypeName", ""},
                        {"@MsHospitalBed", ""},
                        {"@MsHospitalLocationId", ""},
                        {"@MsHospitalLocationName", ""},
                        {"@MsHospitalStatusId", "0"},
                        {"@MsHospitalStatusName", ""},
                        {"@MsHospitalAddress", ""},
                        {"@MsHospitalPostCode", ""},
                        {"@MsHospitalTel", ""},
                        {"@MsHospitalFax", ""},
                        {"@MsHospitalLevel", ""},
                    };

            db.Get("Spr_S_TblMasterHospital", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        public void getDataGrid(DataTable dt)
        {
            int row = dt.Rows.Count;

            if (row == 0)
            {
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                lblCount.Text = "0";
            }
            else
            {
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "MsHospitalId", "MsHospitalName", "MsHospitalUnderName", "MsHospitalTypeName", "MsStatusNameTh", "MsHospitalCode");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                    "ลำดับ", 50, true, mc, mc
                    , "รหัสโรงพยาบาล", 150, true, ml, ml
                    , "ชื่อโรงพยาบาล", 500, true, ml, ml
                    , "สังกัด", 300, false, ml, ml
                    , "ประเภท", 250, true, ml, ml
                    , "สถานะ", 100, true, ml, ml
                    , "", 0, false, mc, mc
                    );

                picExcel.Visible = true;
                lblCount.Text = Fn.setComma(dt.Rows[0]["DataRow"].ToString());
                txtSearch.Focus();
            }
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
            string[,] Parameter = new string[,]
                    {
                        {"@SearchName", txtSearch.Text},
                        {"@MsHospitalId", txtId.Text},
                        {"@MsHospitalNumber", txtCode.Text},
                        {"@MsHospitalName", txtName.Text},
                        {"@MsHospitalUnderId", txtUnderCode.Text},
                        {"@MsHospitalUnderName", txtUnderName.Text},
                        {"@MsHospitalTypeId", txtTypeCode.Text},
                        {"@MsHospitalTypeName",txtTypeName.Text},
                        {"@MsHospitalBed", ""},
                        {"@MsHospitalLocationId", ""},
                        {"@MsHospitalLocationName", txtAddress.Text},
                        {"@MsHospitalStatusId", cbbStatus.SelectedValue.ToString()},
                        {"@MsHospitalStatusName", ""},
                        {"@MsHospitalAddress", txtNumber.Text},
                        {"@MsHospitalPostCode", txtPost.Text},
                        {"@MsHospitalTel", txtPhone.Text},
                        {"@MsHospitalFax", ""},
                        {"@MsHospitalLevel", ""},
                    };

            db.Get("Spr_S_TblMasterHospital", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                txtId.Text = Fn.getId("Tbl_Master_Hospital", "MsHospitalId", "HOS");

                strOpe = "I";

                string[,] Parameter = new string[,]
                    {
                    {"@MsHospitalId", txtId.Text},
                    {"@MsHospitalNumber", txtCode.Text},
                    {"@MsHospitalName", txtName.Text},
                    {"@MsHospitalUnderId", txtUnderCode.Text},
                    {"@MsHospitalUnderName", txtUnderName.Text},
                    {"@MsHospitalTypeId", txtTypeCode.Text},
                    {"@MsHospitalTypeName", txtTypeName.Text},
                    {"@MsHospitalBed", ""},
                    {"@MsHospitalLocationId", ""},
                    {"@MsHospitalLocationName", txtAddress.Text},
                    {"@MsHospitalStatusId", cbbStatus.SelectedValue.ToString()},
                    {"@MsHospitalStatusName", cbbStatus.SelectedItem.ToString()},
                    {"@MsHospitalAddress ", txtNumber.Text},
                    {"@MsHospitalPostCode", txtPost.Text},
                    {"@MsHospitalTel", txtPhone.Text},
                    {"@MsHospitalFax", ""},
                    {"@MsHospitalLevel", ""},
                    {"@MsHospitalStatus", "Y"},
                    {"@User", strUserId},
                    };

                bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

                if (Action == true)
                {
                    db.Operations("Spr_I_TblMasterHospital", Parameter, out strErr);

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

                    string[,] Parameter = new string[,]
                        {
                        {"@SearchName", ""},
                        {"@MsHospitalId", row.Cells["MsHospitalId"].Value.ToString()},
                        {"@MsHospitalNumber", ""},
                        {"@MsHospitalName", ""},
                        {"@MsHospitalUnderId", ""},
                        {"@MsHospitalUnderName", ""},
                        {"@MsHospitalTypeId", ""},
                        {"@MsHospitalTypeName", ""},
                        {"@MsHospitalBed", ""},
                        {"@MsHospitalLocationId", ""},
                        {"@MsHospitalLocationName", ""},
                        {"@MsHospitalStatusId", "0"},
                        {"@MsHospitalStatusName", ""},
                        {"@MsHospitalAddress", ""},
                        {"@MsHospitalPostCode", ""},
                        {"@MsHospitalTel", ""},
                        {"@MsHospitalFax", ""},
                        {"@MsHospitalLevel", ""},
                        };

                    db.Get("Spr_S_TblMasterHospital", Parameter, out strErr, out dt);

                    txtId.Text = dt.Rows[0]["MsHospitalId"].ToString();
                    txtCode.Text = dt.Rows[0]["MsHospitalNumber"].ToString();
                    txtName.Text = dt.Rows[0]["MsHospitalName"].ToString();
                    txtUnderName.Text = dt.Rows[0]["MsHospitalUnderName"].ToString();
                    txtUnderCode.Text = dt.Rows[0]["MsHospitalUnderId"].ToString();
                    txtTypeCode.Text = dt.Rows[0]["MsHospitalTypeId"].ToString();
                    txtTypeName.Text = dt.Rows[0]["MsHospitalTypeName"].ToString();
                    txtNumber.Text = dt.Rows[0]["MsHospitalAddress"].ToString();
                    txtPost.Text = dt.Rows[0]["MsHospitalPostCode"].ToString();
                    txtPhone.Text = dt.Rows[0]["MsHospitalTel"].ToString();
                    cbbStatus.SelectedValue = dt.Rows[0]["MsHospitalStatusId"].ToString();
                    txtAddress.Text = dt.Rows[0]["MsHospitalLocationName"].ToString();
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
                   {"@MsHospitalId", txtId.Text},
                    {"@MsHospitalNumber", txtCode.Text},
                    {"@MsHospitalName", txtName.Text},
                    {"@MsHospitalUnderId", txtUnderCode.Text},
                    {"@MsHospitalUnderName", txtUnderName.Text},
                    {"@MsHospitalTypeId", txtTypeCode.Text},
                    {"@MsHospitalTypeName", txtTypeName.Text},
                    {"@MsHospitalBed", ""},
                    {"@MsHospitalLocationId", ""},
                    {"@MsHospitalLocationName", txtAddress.Text},
                    {"@MsHospitalStatusId", cbbStatus.SelectedValue.ToString()},
                    {"@MsHospitalStatusName", cbbStatus.SelectedItem.ToString()},
                    {"@MsHospitalAddress ", txtNumber.Text},
                    {"@MsHospitalPostCode", txtPost.Text},
                    {"@MsHospitalTel", txtPhone.Text},
                    {"@MsHospitalFax", ""},
                    {"@MsHospitalLevel", ""},
                    {"@MsHospitalStatus", "Y"},
                    {"@User", strUserId},
                    };

                    bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

                    if (Action == true)
                    {
                        db.Operations("Spr_U_TblMasterHospital", Parameter, out strErr);

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text != "")
                {
                    strOpe = "D";

                    string[,] Parameter = new string[,]
                        {
                   {"@MsHospitalId", txtId.Text},
                        };

                    bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

                    if (Action == true)
                    {
                        db.Operations("Spr_D_TblMasterHospital", Parameter, out strErr);

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void picExcel_Click(object sender, EventArgs e)
        {
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchData();
            }
        }

        private void FrmSearchHospital_KeyDown(object sender, KeyEventArgs e)
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
        }
    }
}