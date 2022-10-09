using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmSearchSchool : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "SEASCH00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string filePath = "";
        public string fileName = "-";
        public string fileType = ".jpg";
        public string strAddress = "";

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

        public FrmSearchSchool(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmSearchSchool_Load(object sender, EventArgs e)
        {
            
            Clear();
        }

        public void Clear()
        {
            txtSearch.Text = "";
            txtCode.Text = "";
            txtNameTh.Text = "";
            txtNameEn.Text = "";
            txtType.Text = "";
            txtDepartment.Text = "";
            txtMinistry.Text = "";
            txtDateStart.Text = "";
            txtPresident.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            txtWeb.Text = "";

            lblCount.Text = "0";
            txtSearch.Text = "";
            txtSearch.Focus();

            dataGridView.DataSource = null;
        }

        public void getDataGrid(DataTable dt)
        {
            int row;

            try
            {
                row = dt.Rows.Count;
            }
            catch (Exception)
            {
                row = 0;
            }

            if (row == 0)
            {
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                txtSearch.Focus();
                lblCount.Text = "0";
            }
            else
            {
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "MsSchoolCode", "MsSchoolNameTh", "MsSchoolAmphurName", "MsSchoolProvinceName", "MsSchoolEstablishDate", "MsSchoolId");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                      "ลำดับ", 50, true, mc, mc
                    , "รหัสโรงเรียน", 100, true, ml, ml
                    , "ชื่อโรงเรียน", 150, true, ml, ml
                    , "อำเภอ", 150, true, ml, ml
                    , "จังหวัด", 100, true, ml, ml
                    , "วันก่อตั้ง", 100, true, mc, mc
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
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
                        {"@MsSchoolId", row.Cells["MsSchoolId"].Value.ToString()},
                        {"@MsSchoolBureauId", ""},
                        {"@MsSchoolBureauName", ""},
                        {"@MsSchoolDepartmentId", ""},
                        {"@MsSchoolDepartmentName", ""},
                        {"@MsSchoolMinistryId", ""},
                        {"@MsSchoolMinistryName", ""},
                        {"@MsSchoolCode", ""},
                        {"@MsSchoolNameTh", ""},
                        {"@MsSchoolNameEn", ""},
                        {"@MsSchoolHomeNo", ""},
                        {"@MsSchoolMoo", ""},
                        {"@MsSchoolStreet", ""},
                        {"@MsSchoolTumbolId", ""},
                        {"@MsSchooTumbolName", ""},
                        {"@MsSchoolAmphurId", ""},
                        {"@MsSchoolAmphurName", ""},
                        {"@MsSchoolProvinceId", ""},
                        {"@MsSchoolProvinceName", ""},
                        {"@MsSchoolPostCode", ""},
                        {"@MsSchoolTel", ""},
                        {"@MsSchoolFax", ""},
                        {"@MsSchoolEmail", ""},
                        {"@MsSchoolWebsite", ""},
                        {"@MsSchoolEstablishDate", ""},
                        {"@MsSchoolTypeId", ""},
                        {"@MsSchoolTypeName", ""},
                        {"@MsSchoolCampusId", ""},
                        {"@MsSchoolCampusName", ""},
                        {"@MsSchoolMunicipalId", ""},
                        {"@MsSchoolMunicipalName", ""},
                        {"@MsSchoolPresidentName", ""},
                        {"@MsSchoolStatus", ""},
                        {"@MsSchoolOfficeTypeId", ""},
                        {"@Find", ""},
                    };

                    db.Get("Spr_S_TblMasterSchool", Parameter, out strErr, out dt);

                    txtCode.Text = dt.Rows[0]["MsSchoolCode"].ToString();
                    txtNameTh.Text = dt.Rows[0]["MsSchoolNameTh"].ToString();
                    txtNameEn.Text = dt.Rows[0]["MsSchoolNameEn"].ToString();
                    txtType.Text = dt.Rows[0]["MsSchoolTypeName"].ToString();
                    txtDepartment.Text = dt.Rows[0]["MsSchoolDepartmentName"].ToString();
                    txtMinistry.Text = dt.Rows[0]["MsSchoolBureauName"].ToString();
                    txtDateStart.Text = dt.Rows[0]["MsSchoolEstablishDate"].ToString();
                    txtPresident.Text = dt.Rows[0]["MsSchoolPresidentName"].ToString();
                    txtAddress.Text = "อ." + dt.Rows[0]["MsSchoolAmphurName"].ToString() + " จ." + dt.Rows[0]["MsSchoolProvinceName"].ToString();
                    txtPhone.Text = dt.Rows[0]["MsSchoolTel"].ToString();
                    txtWeb.Text = dt.Rows[0]["MsSchoolWebsite"].ToString();

                    txtSearch.Text = "";
                    txtSearch.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
        }

        public void SearchData()
        {
            try
            {
                Parameter = new string[,]
                    {
                        {"@MsSchoolId", ""},
                        {"@MsSchoolBureauId", ""},
                        {"@MsSchoolBureauName", ""},
                        {"@MsSchoolDepartmentId", ""},
                        {"@MsSchoolDepartmentName", ""},
                        {"@MsSchoolMinistryId", ""},
                        {"@MsSchoolMinistryName", ""},
                        {"@MsSchoolCode", ""},
                        {"@MsSchoolNameTh", ""},
                        {"@MsSchoolNameEn", ""},
                        {"@MsSchoolHomeNo", ""},
                        {"@MsSchoolMoo", ""},
                        {"@MsSchoolStreet", ""},
                        {"@MsSchoolTumbolId", ""},
                        {"@MsSchooTumbolName", ""},
                        {"@MsSchoolAmphurId", ""},
                        {"@MsSchoolAmphurName", ""},
                        {"@MsSchoolProvinceId", ""},
                        {"@MsSchoolProvinceName", ""},
                        {"@MsSchoolPostCode", ""},
                        {"@MsSchoolTel", ""},
                        {"@MsSchoolFax", ""},
                        {"@MsSchoolEmail", ""},
                        {"@MsSchoolWebsite", ""},
                        {"@MsSchoolEstablishDate", ""},
                        {"@MsSchoolTypeId", ""},
                        {"@MsSchoolTypeName", ""},
                        {"@MsSchoolCampusId", ""},
                        {"@MsSchoolCampusName", ""},
                        {"@MsSchoolMunicipalId", ""},
                        {"@MsSchoolMunicipalName", ""},
                        {"@MsSchoolPresidentName", ""},
                        {"@MsSchoolStatus", ""},
                        {"@MsSchoolOfficeTypeId", ""},
                        {"@Find", txtSearch.Text},
                    };

                db.Get("Spr_S_TblMasterSchool", Parameter, out strErr, out dt);
                getDataGrid(dt);
                lblSearch.Text = "";
            }
            catch (Exception)
            {
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                txtSearch.Focus();
                lblCount.Text = "0";
                lblSearch.Text = "";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnSearch);
        }

        private void FrmSearchSchool_KeyDown(object sender, KeyEventArgs e)
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