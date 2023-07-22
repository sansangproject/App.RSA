using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmMangeUser : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string programCode = "MANUS00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        private DataTable dt = new DataTable();

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsCryption Cryption = new clsCryption();
        private clsSendMail TSSSendMail = new clsSendMail();

        public string[,] Parameter = new string[,] { };

        public FrmMangeUser(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmMangeUser_Load(object sender, EventArgs e)
        {
            

            List.GetList(cbbStatus, "0", "Status");
            List.GetList(cbbType, "Y", "UserType");

            Clear();
        }

        public void Clear()
        {
            cbbStatus.SelectedValue = 0;
            cbbType.SelectedValue = -1;

            txtCode.Enabled = true;
            txtCode.Text = "";
            txtCode.Focus();
            txtName.Text = "";
            txtSurname.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";

            lblSearch.Text = "";

            Parameter = new string[,]
                {
                    {"@MsUserCode", ""},
                    {"@MsUserPassword", ""},
                    {"@MsUserStatus", "0"},
                    {"@MsUserSurname", ""},
                    {"@MsUserName", ""},
                    {"@MsUserEmail", ""},
                    {"@MsUserType", ""},
                    {"@MsUserSex", ""},
                };

            db.Get("Spr_S_TblMasterUser", Parameter, out strErr, out dt);
            getDataGrid(dt);
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
                txtCode.Focus();
                lblCount.Text = "0";
            }
            else
            {
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "MsUserCode", "MsUserName", "MsUserSurname", "MsUserEmail", "MsStatusNameTh", "MsUserId");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                    "ลำดับ", 50, true, mc, mc
                    , "รหัสผู้ใช้งาน", 100, true, ml, ml
                    , "ชื่อ", 100, true, ml, ml
                    , "สกุล", 100, true, ml, ml
                    , "Email", 100, true, ml, ml
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                DataTable dt = new DataTable();

                string[,] Parameter = new string[,]
                {
                    {"@MsUserCode", row.Cells["MsUserCode"].Value.ToString()},
                    {"@MsUserPassword", ""},
                    {"@MsUserStatus", "0"},
                    {"@MsUserSurname", ""},
                    {"@MsUserEmail", ""},
                    {"@MsUserName", ""},
                    {"@MsUserType", ""},
                    {"@MsUserSex", ""},
                };

                db.Get("Spr_S_TblMasterUser", Parameter, out strErr, out dt);

                cbbStatus.SelectedValue = dt.Rows[0]["MsUserStatus"].ToString();
                cbbType.SelectedValue = dt.Rows[0]["MsUserType"].ToString();

                txtCode.Text = dt.Rows[0]["MsUserCode"].ToString();
                txtName.Text = dt.Rows[0]["MsUserName"].ToString();
                txtSurname.Text = dt.Rows[0]["MsUserSurname"].ToString();
                txtPassword.Text = Cryption.Decrypt(dt.Rows[0]["MsUserPassword"].ToString());
                txtEmail.Text = dt.Rows[0]["MsUserEmail"].ToString();
            }
        }

        public void SearchData()
        {
            string[,] Parameter = new string[,]
                {
                    {"@MsUserCode", txtCode.Text},
                    {"@MsUserPassword", txtPassword.Text},
                    {"@MsUserName", txtName.Text},
                    {"@MsUserSurname", txtSurname.Text},
                    {"@MsUserType", Fn.getComboboxId(cbbType)},
                    {"@MsUserStatus", Fn.getComboboxId(cbbStatus)},
                    {"@MsUserEmail", txtEmail.Text},
                    {"@MsUserSex", ""},
                };

            db.Get("Spr_S_TblMasterUser", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            strOpe = "D";

            string[,] Parameter = new string[,]
            {
                    {"@MsUserCode", txtCode.Text},
            };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_D_TblMasterUser", Parameter, out strErr);

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
            if (Fn.CheckEmail(txtEmail.Text) == false)
            {
                Message.ShowMesInfo("รูปแบบอีเมล์ไม่ถูกต้อง กรุณาตรวจสอบและลองใหม่อีกครั้ง");
                txtEmail.Focus();
                return;
            }
            else
            {
                strOpe = "U";

                string[,] Parameter = new string[,]
                {
                    {"@MsUserCode", txtCode.Text},
                    {"@MsUserPassword", Cryption.Encrypt(txtPassword.Text)},
                    {"@MsUserName", txtName.Text},
                    {"@MsUserSurname", txtSurname.Text},
                    {"@MsUserType", Fn.getComboboxId(cbbType)},
                    {"@MsUserStatus", Fn.getComboboxId(cbbStatus)},
                    {"@MsUserEmail", txtEmail.Text},
                    {"@User", strUserId},
                    {"@MsUserSex", ""},
                };

                bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

                if (Action == true)
                {
                    db.Operations("Spr_U_TblMasterUser", Parameter, out strErr);

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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Fn.CheckEmail(txtEmail.Text) == false)
            {
                Message.ShowMesInfo("รูปแบบอีเมล์ไม่ถูกต้อง กรุณาตรวจสอบและลองใหม่อีกครั้ง");
                txtEmail.Focus();
                return;
            }
            else
            {
                strOpe = "I";

                string[,] Parameter = new string[,]
                {
                    {"@MsUserCode", txtCode.Text},
                    {"@MsUserPassword", Cryption.Encrypt(txtPassword.Text)},
                    {"@MsUserName", txtName.Text},
                    {"@MsUserSex", ""},
                    {"@MsUserSurname", txtSurname.Text},
                    {"@MsUserType", Fn.getComboboxId(cbbType)},
                    {"@MsUserStatus", Fn.getComboboxId(cbbStatus)},
                    {"@MsUserEmail", txtEmail.Text},
                    {"@User", strUserId},
                };

                bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

                if (Action == true)
                {
                    db.Operations("Spr_I_TblMasterUser", Parameter, out strErr);

                    if (strErr == null)
                    {
                        try
                        {
                            TSSSendMail.newMember(txtEmail.Text, txtName.Text + " " + txtSurname.Text, txtCode.Text, txtPassword.Text);
                        }
                        catch (Exception)
                        {
                        }

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

        private void picExcel_Click(object sender, EventArgs e)
        {
        }

        private void cbShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (strUserId == "TUATHOR.ST")
            {
                txtPassword.PasswordChar = cbShowPass.Checked ? '\0' : '*';
            }
        }
    }
}