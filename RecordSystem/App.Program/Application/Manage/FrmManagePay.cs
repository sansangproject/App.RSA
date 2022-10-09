using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmManagePay : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "MANPA00";
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

        public FrmManagePay(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmManagePay_Load(object sender, EventArgs e)
        {
            
            List.GetList(cbbStatus, "0", "Status");
            Clear();
        }

        public void Clear()
        {
            txtCode.Enabled = true;
            txtCode.Text = "";
            txtCode.Focus();
            txtName.Text = "";
            txtNameEn.Text = "";
            cbbStatus.SelectedValue = "0";
            lblSearch.Text = "";

            string[,] Parameter = new string[,]
                {
                    {"@MsPaymentCode",""},
                    {"@MsPaymentNameTh",""},
                    {"@MsPaymentNameEn",""},
                    {"@MsPaymentStatus","0"},
                };

            db.Get("Spr_S_TblMasterPayment", Parameter, out strErr, out dt);
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
                dtGrid = dt.DefaultView.ToTable(true, "MsPaymentCode", "MsPaymentNameTh", "MsPaymentNameEn", "MsStatusNameTh", "Date", "MsPaymentId");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                    "ลำดับ", 50, true, mc, mc
                    , "รหัส", 150, true, ml, ml
                    , "ชื่อรายการ (ไทย)", 200, true, ml, ml
                    , "ชื่อรายการ (อังกฤษ)", 200, true, ml, ml
                    , "สถานะ", 100, true, ml, ml
                    , "ข้อมูล ณ วันที่", 150, true, mc, mc
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void picExcel_Click(object sender, EventArgs e)
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            strOpe = "D";

            string[,] Parameter = new string[,]
                {
                   {"@MsPaymentCode", txtCode.Text},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_D_TblMasterPayment", Parameter, out strErr);

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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            strOpe = "U";

            string[,] Parameter = new string[,]
                {
                    {"@MsPaymentCode", txtCode.Text},
                    {"@MsPaymentNameTh", txtName.Text},
                    {"@MsPaymentNameEn", txtNameEn.Text},
                    {"@MsPaymentStatus",cbbStatus.SelectedValue.ToString()},
                    {"@User",strUserId },
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_U_TblMasterPayment", Parameter, out strErr);

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

        public void SearchData()
        {
            string[,] Parameter = new string[,]
                {
                    {"@MsPaymentCode", txtCode.Text},
                    {"@MsPaymentNameTh", txtName.Text},
                    {"@MsPaymentNameEn", txtNameEn.Text},
                    {"@MsPaymentStatus", cbbStatus.SelectedValue.ToString()},
                };

            db.Get("Spr_S_TblMasterPayment", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                DataTable dt = new DataTable();

                string[,] Parameter = new string[,]
                {
                    {"@MsPaymentCode", row.Cells["MsPaymentCode"].Value.ToString()},
                    {"@MsPaymentNameTh", ""},
                    {"@MsPaymentNameEn",""},
                    {"@MsPaymentStatus", "0"},
                };

                db.Get("Spr_S_TblMasterPayment", Parameter, out strErr, out dt);

                txtCode.Text = dt.Rows[0]["MsPaymentCode"].ToString();
                txtName.Text = dt.Rows[0]["MsPaymentNameTh"].ToString();
                cbbStatus.SelectedValue = dt.Rows[0]["MsPaymentStatus"].ToString();
                txtNameEn.Text = dt.Rows[0]["MsPaymentNameEn"].ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            strOpe = "I";

            string[,] Parameter = new string[,]
                {
                    {"@MsPaymentCode", txtCode.Text},
                    {"@MsPaymentNameTh", txtName.Text},
                    {"@MsPaymentNameEn", txtNameEn.Text},
                    {"@MsPaymentStatus",cbbStatus.SelectedValue.ToString()},
                    {"@User",strUserId },
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_I_TblMasterPayment", Parameter, out strErr);

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }
    }
}