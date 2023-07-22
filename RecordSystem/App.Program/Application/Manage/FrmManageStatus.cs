using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmManageStatus : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "MANST00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string filePath = "-";
        public string fileName = "-";
        public string fileType = ".jpg";

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();

        private clsSearch Search = new clsSearch();
        private clsFunction Fn = new clsFunction();
       private dbConnection db = new dbConnection();
        private clsMessage Message = new clsMessage();
        private clsDataList List = new clsDataList();

        public string[,] Parameter = new string[,] { };

        public FrmManageStatus(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmMangeStatus_Load(object sender, EventArgs e)
        {
            List.GetList(cbbStatusType, "0", "StatusType");
            Clear();
        }

        public void Clear()
        {
            txtCode.Enabled = true;
            txtCode.Text = "";
            txtCode.Focus();

            txtName.Text = "";
            txtNameEn.Text = "";
            txtType.Text = "";
            lblSearch.Text = "";
            cbbStatusType.SelectedValue = 0;

            string[,] Parameter = new string[,]
                {
                    {"@MsStatusId",""},
                    {"@MsStatusCode",""},
                    {"@MsStatusNameEn",""},
                    {"@MsStatusNameTh",""},
                    {"@MsStatusType",""},
                };

            db.Get("Spr_S_TblMasterStatus", Parameter, out strErr, out dt);
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
                dtGrid = dt.DefaultView.ToTable(true, "MsStatusCode", "MsStatusNameTh", "MsStatusNameEn", "MsStatusType", "UpdateDate", "MsStatusId");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                    "ลำดับ", 100, true, mc, mc
                    , "รหัส", 200, true, ml, ml
                    , "ภาษาไทย", 150, true, ml, ml
                    , "ภาษาอังกฤษ", 150, true, ml, ml
                    , "กลุ่มสถานะ", 150, true, ml, ml
                    , "ข้อมูล ณ วันที่ ", 200, true, mc, mc
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
                   {"@MsStatusCode", txtCode.Text},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_D_TblMasterStatus", Parameter, out strErr);

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
                    {"@MsStatusCode", txtCode.Text},
                    {"@MsStatusNameEn", txtNameEn.Text},
                    {"@MsStatusNameTh", txtName.Text},
                    {"@MsStatusType", txtType.Text},
                    {"@User", strUserId},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_U_TblMasterStatus", Parameter, out strErr);

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
                    {"@MsStatusId", ""},
                    {"@MsStatusCode", txtCode.Text},
                    {"@MsStatusNameEn", txtNameEn.Text},
                    {"@MsStatusNameTh", txtName.Text},
                    {"@MsStatusType", txtType.Text},
                };

            db.Get("Spr_S_TblMasterStatus", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                DataTable dt = new DataTable();

                string[,] Parameter = new string[,]
                    {
                    {"@MsStatusId", ""},
                    {"@MsStatusCode", row.Cells["MsStatusCode"].Value.ToString()},
                    {"@MsStatusNameEn", ""},
                    {"@MsStatusNameTh", ""},
                    {"@MsStatusType", ""},
                    };

                db.Get("Spr_S_TblMasterStatus", Parameter, out strErr, out dt);

                txtCode.Text = dt.Rows[0]["MsStatusCode"].ToString();
                txtName.Text = dt.Rows[0]["MsStatusNameTh"].ToString();
                txtNameEn.Text = dt.Rows[0]["MsStatusNameEn"].ToString();
                cbbStatusType.SelectedValue = dt.Rows[0]["MsStatusType"].ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            strOpe = "I";

            string[,] Parameter = new string[,]
                {
                    {"@MsStatusCode", txtCode.Text},
                    {"@MsStatusNameEn", txtNameEn.Text},
                    {"@MsStatusNameTh", txtName.Text},
                    {"@MsStatusType", txtType.Text},
                    {"@User", strUserId},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_I_TblMasterStatus", Parameter, out strErr);

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

        private void FrmMangeStatus_KeyDown(object sender, KeyEventArgs e)
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
    }
}