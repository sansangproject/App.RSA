using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmPATMA00 : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "PATMA00";
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

        public FrmPATMA00(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmPATMA00_Load(object sender, EventArgs e)
        {
            

            List.GetList(cbbStatus, "0", "Status");
            List.GetList(cbbHospital, "Y", "HospitalId");

            Clear();
        }

        public void Clear()
        {
            lblSearch.Text = "";
            txtCode.Text = "";
            txtName.Text = "";
            txtNumber.Text = "";
            txtCode.Focus();

            cbbStatus.SelectedValue = 0;
            cbbHospital.SelectedValue = 0;

            Parameter = new string[,]
                {
                    {"@MsHospitalCode", "0"},
                    {"@MsPatientCode", ""},
                    {"@MsPatientNumber", ""},
                    {"@MsPatientName", ""},
                    {"@MsPatientStatus", "0"},
                };

            db.Get("Spr_S_TblMasterPatient", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        public void getDataGrid(DataTable dt)
        {
            int row = dt.Rows.Count;

            if (row == 0)
            {
                //Fn.ShowImageNull();
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                lblCount.Text = "0";
            }
            else
            {
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "MsPatientCode", "MsPatientName", "MsHospitalName", "MsHospitalLocationName", "MsStatusNameTh", "MsPatientId");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                    "ลำดับ", 30, true, mc, mc
                    , "เลขประจำตัวผู้ป่วย", 250, true, ml, ml
                    , "ชื่อ - สกุล", 250, true, ml, ml
                    , "สถาพยาบาล", 300, true, ml, ml
                    , "ที่อยู่", 300, true, ml, ml
                    , "สถานะ", 100, true, ml, ml
                    , "", 0, false, mc, mc
                    );

                picExcel.Visible = true;
                lblCount.Text = row.ToString();
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
            Parameter = new string[,]
                {
                    {"@MsHospitalCode", cbbHospital.SelectedValue.ToString()},
                    {"@MsPatientCode", txtCode.Text},
                    {"@MsPatientNumber", txtNumber.Text},
                    {"@MsPatientName", txtName.Text},
                    {"@MsPatientStatus", cbbStatus.SelectedValue.ToString()},
                };

            db.Get("Spr_S_TblMasterPatient", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtCode.Text = Fn.GetCodes("85", "", "Generated");

            strOpe = "I";

            string[,] Parameter = new string[,]
                {
                    {"@MsHospitalCode", cbbHospital.SelectedValue.ToString()},
                    {"@MsPatientCode", txtCode.Text},
                    {"@MsPatientNumber", txtNumber.Text},
                    {"@MsPatientName", txtName.Text},
                    {"@MsPatientStatus", cbbStatus.SelectedValue.ToString()},
                    {"@User", strUserId},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_I_TblMasterPatient", Parameter, out strErr);

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

                Parameter = new string[,]
                {
                    {"@MsHospitalCode", "0"},
                    {"@MsPatientCode", row.Cells["MsPatientCode"].Value.ToString()},
                    {"@MsPatientNumber", ""},
                    {"@MsPatientName", ""},
                    {"@MsPatientStatus", "0"},
                };

                db.Get("Spr_S_TblMasterPatient", Parameter, out strErr, out dt);

                txtCode.Text = dt.Rows[0]["MsPatientCode"].ToString();
                txtName.Text = dt.Rows[0]["MsPatientName"].ToString();
                txtNumber.Text = dt.Rows[0]["MsPatientNumber"].ToString();
                cbbHospital.SelectedValue = dt.Rows[0]["MsHospitalCode"].ToString();
                cbbStatus.SelectedValue = dt.Rows[0]["MsPatientStatus"].ToString();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            strOpe = "U";

            string[,] Parameter = new string[,]
                {
                    {"@MsHospitalCode", cbbHospital.SelectedValue.ToString()},
                    {"@MsPatientCode", txtCode.Text},
                    {"@MsPatientNumber", txtNumber.Text},
                    {"@MsPatientName", txtName.Text},
                    {"@MsPatientStatus", cbbStatus.SelectedValue.ToString()},
                    {"@User", strUserId},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_U_TblMasterPatient", Parameter, out strErr);

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            strOpe = "D";

            string[,] Parameter = new string[,]
                {
                   {"@MsPatientCode", txtCode.Text},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_D_TblMasterPatient", Parameter, out strErr);

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void picExcel_Click(object sender, EventArgs e)
        {
        }

        private void FrmPATMA00_KeyDown(object sender, KeyEventArgs e)
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

            if (keyCode == "Alt+F")
            {
                btnSearch_Click(sender, e);
            }

            if (keyCode == "Alt+C")
            {
                btnClear_Click(sender, e);
            }
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchData();
            }
        }
    }
}