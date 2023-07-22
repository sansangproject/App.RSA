using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmManagePaySub : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "MANSU00";
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

        public FrmManagePaySub(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmManagePaySub_Load(object sender, EventArgs e)
        {
            

            List.GetList(cbbStatus, "0", "Status");
            List.GetList(cbbPay, "Y", "Payment");

            Clear();
        }

        public void Clear()
        {
            cbbStatus.SelectedValue = 0;
            cbbPay.SelectedValue = 0;

            txtCode.Enabled = true;
            txtCode.Text = "";
            txtName.Text = "";
            txtCodeRef.Text = "";
            rdbAdd.Checked = false;
            rdbMinus.Checked = false;
            rdbAll.Checked = true;
            txtDetail.Text = "";
            lblSearch.Text = "";
            txtName.Focus();

            string[,] Parameter = new string[,]
                {
                    {"@MsPaymentSubCode",""},
                    {"@MsPaymentSubNameTh",""},
                    {"@MsPaymentSubNameEn",""},
                    {"@MsPaymentSubDetail",""},
                    {"@MsPaymentCode","0"},
                    {"@MsPaymentSubType",""},
                    {"@MsPaymentSubStatus","0"},
                };

            db.Get("Spr_S_TblMasterPaymentSub", Parameter, out strErr, out dt);
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
                dtGrid = dt.DefaultView.ToTable(true, "MsPaymentSubCode", "MsPaymentSubNameTh", "MsPaymentNameTh", "MsPaymentSubTypeTxt", "MsStatusNameTh", "MsPaymentSubId");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                    "ลำดับ", 50, true, mc, mc
                    , "รหัส", 200, true, ml, ml
                    , "รายการ", 250, true, ml, ml
                    , "ประเภท", 250, true, ml, ml
                    , "รายรับ | รายจ่าย", 100, true, mc, mc
                    , "สถานะ", 100, true, mc, mc
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
                    {"@MsPaymentSubCode", txtCode.Text},
                    {"@DeleteType", "0"},
                    {"@User", strUserId},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_D_TblMasterPaymentSub", Parameter, out strErr);

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
                    {"@MsPaymentSubCode", txtCode.Text},
                    {"@MsPaymentSubNameTh", txtName.Text},
                    {"@MsPaymentSubNameEn", txtCodeRef.Text},
                    {"@MsPaymentSubDetail", txtDetail.Text},
                    {"@MsPaymentCode", cbbPay.SelectedValue.ToString()},
                    {"@MsPaymentSubType", rdbAdd.Checked == true? "1" : "0"},
                    {"@MsPaymentSubStatus",cbbStatus.SelectedValue.ToString()},
                    {"@User", strUserId},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_U_TblMasterPaymentSub", Parameter, out strErr);

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
                    {"@MsPaymentSubCode", txtCode.Text},
                    {"@MsPaymentSubNameTh", txtName.Text},
                    {"@MsPaymentSubNameEn", txtCodeRef.Text},
                    {"@MsPaymentSubDetail", txtDetail.Text},
                    {"@MsPaymentCode", cbbPay.SelectedValue.ToString()},
                    {"@MsPaymentSubType",  rdbAll.Checked == false ? rdbAdd.Checked == true? "1" : "0" : ""},
                    {"@MsPaymentSubStatus",cbbStatus.SelectedValue.ToString()},
                };

            db.Get("Spr_S_TblMasterPaymentSub", Parameter, out strErr, out dt);
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
                    {"@MsPaymentSubCode", row.Cells["MsPaymentSubCode"].Value.ToString()},
                    {"@MsPaymentSubNameTh",""},
                    {"@MsPaymentSubNameEn",""},
                    {"@MsPaymentSubDetail",""},
                    {"@MsPaymentCode","0"},
                    {"@MsPaymentSubType",""},
                    {"@MsPaymentSubStatus","0"},
                };

                db.Get("Spr_S_TblMasterPaymentSub", Parameter, out strErr, out dt);

                txtCode.Text = dt.Rows[0]["MsPaymentSubCode"].ToString();
                txtName.Text = dt.Rows[0]["MsPaymentSubNameTh"].ToString();
                txtDetail.Text = dt.Rows[0]["MsPaymentSubDetail"].ToString();
                cbbPay.SelectedValue = dt.Rows[0]["MsPaymentCode"].ToString();
                rdbAdd.Checked = dt.Rows[0]["MsPaymentSubType"].ToString() == "1" ? true : false;
                rdbMinus.Checked = dt.Rows[0]["MsPaymentSubType"].ToString() == "1" ? false : true;
                cbbStatus.SelectedValue = dt.Rows[0]["MsPaymentSubStatus"].ToString();
                txtCodeRef.Text = dt.Rows[0]["MsPaymentSubNameEn"].ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtCode.Text = Fn.getIdRunning("Tbl_Master_Payment_Sub", "MsPaymentSubId", (cbbPay.SelectedValue.ToString()).Substring(0, 3));
            strOpe = "I";

            string[,] Parameter = new string[,]
                {
                    {"@MsPaymentSubCode", txtCode.Text},
                    {"@MsPaymentSubNameTh", txtName.Text},
                    {"@MsPaymentSubNameEn", txtCodeRef.Text},
                    {"@MsPaymentSubDetail", txtDetail.Text},
                    {"@MsPaymentCode", cbbPay.SelectedValue.ToString()},
                    {"@MsPaymentSubType", rdbAdd.Checked == true? "1" : "0"},
                    {"@MsPaymentSubStatus",cbbStatus.SelectedValue.ToString()},
                    {"@User", strUserId},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_I_TblMasterPaymentSub", Parameter, out strErr);

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

        private void txtIDkeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnSearch);
        }

        private void txtNamekeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnSearch);
        }

        private void txtDetailkeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnSearch);
        }

        private void cbbPay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbPay.SelectedValue != null)
            {
                if (cbbPay.SelectedValue.ToString() != "0" & txtName.Text == "")
                {
                    txtDetail.Text = cbbPay.Text + " | ";
                }
            }
            else
            {
                cbbPay.SelectedValue = "0";
            }
        }

        private void txtType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8)
            {
            }
            else
            {
                e.Handled = true;
                return;
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text != "")
            {
                cbbStatus.SelectedValue = "Y";
            }
            else
            {
                cbbStatus.SelectedValue = "0";
            }
        }
    }
}