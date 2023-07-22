using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmManageMenu : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "MANME00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";
        public string strGenerated = "Generated";
        public string strTableId = "82";

        public string filePath = "-";
        public string fileName = "-";
        public string fileType = ".jpg";

        private DataTable dt = new DataTable();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();

        public string[,] Parameters = new string[,] { };

        public FrmManageMenu(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmMangeMenu_Load(object sender, EventArgs e)
        {
            
            List.GetList(cbbStatus, "0", "Status");
            Clear();
        }

        public void Clear()
        {
            txtCode.Enabled = true;
            txtCode.Text = "";
            txtName.Text = "";
            txtDisplay.Text = "";
            txtNameEN.Text = "";
            txtShow.Text = "";
            txtType.Text = "";
            txtMain.Text = "";
            txtName.Text = "";
            txtCode.Focus();
            lblSearch.Text = "";
            txtMenuForm.Text = "";

            cbbStatus.SelectedValue = 0;

            string[,] Parameter = new string[,]
                {
                    {"@MsMenuCode",""},
                    {"@MsMenuForm", ""},
                    {"@MsMenuStatus", "0"},
                    {"@MsMenuId", ""},
                    {"@MsMenuNameEn", ""},
                    {"@MsMenuNameTh", ""},
                    {"@MsMenuDisplay", ""},
                    {"@MsMenuType", ""},
                    {"@MsMenuMain", ""},
                    {"@MsMenuSub", ""},
                };

            db.Get("Spr_S_TblMasterMenu", Parameter, out strErr, out dt);
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
                dtGrid = dt.DefaultView.ToTable(true, "MsMenuCode", "MsMenuNameTh", "MsMenuNameEn", "MsMenuDisplay", "MsStatusNameTh", "MsMenuId");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                    "ลำดับ", 50, true, mc, mc
                    , "รหัสอ้างอิง", 200, true, ml, ml
                    , "ภาษาไทย", 250, true, ml, ml
                    , "ภาษาอังกฤษ", 300, true, ml, ml
                    , "แสดงผล", 300, true, ml, ml
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
                   {"@MsMenuCode", txtCode.Text},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_D_TblMasterMenu", Parameter, out strErr);

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
                    {"@MsMenuCode", txtCode.Text},
                    {"@MsMenuForm", txtMenuForm.Text},
                    {"@MsMenuNameEn", txtNameEN.Text},
                    {"@MsMenuNameTh", txtName.Text},
                    {"@MsMenuDisplay",  txtShow.Text},
                    {"@MsMenuType", txtType.Text},
                    {"@MsMenuMain", txtMain.Text},
                    {"@MsMenuSub",txtDisplay.Text},
                    {"@MsMenuStatus", cbbStatus.SelectedValue.ToString()},
                    {"@User", strUserId},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_U_TblMasterMenu", Parameter, out strErr);

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

        public void Search()
        {
            string[,] Parameter = new string[,]
                {
                    {"@MsMenuCode", txtCode.Text},
                    {"@MsMenuForm", txtMenuForm.Text},
                    {"@MsMenuStatus", cbbStatus.SelectedValue.ToString()},
                    {"@MsMenuId", ""},
                    {"@MsMenuNameEn", txtNameEN.Text},
                    {"@MsMenuNameTh", txtName.Text},
                    {"@MsMenuDisplay",  txtShow.Text},
                    {"@MsMenuType", txtType.Text},
                    {"@MsMenuMain", txtMain.Text},
                    {"@MsMenuSub", txtDisplay.Text},
                };

            db.Get("Spr_S_TblMasterMenu", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                DataTable dt = new DataTable();

                string[,] Parameter = new string[,]
                    {
                    {"@MsMenuCode", row.Cells["MsMenuCode"].Value.ToString()},
                    {"@MsMenuForm", ""},
                    {"@MsMenuStatus", "0"},
                    {"@MsMenuId", ""},
                    {"@MsMenuNameEn", ""},
                    {"@MsMenuNameTh", ""},
                    {"@MsMenuDisplay", ""},
                    {"@MsMenuType", ""},
                    {"@MsMenuMain", ""},
                    {"@MsMenuSub", ""},
                    };

                db.Get("Spr_S_TblMasterMenu", Parameter, out strErr, out dt);

                txtCode.Text = dt.Rows[0]["MsMenuCode"].ToString();
                txtMenuForm.Text = dt.Rows[0]["MsMenuForm"].ToString();
                txtShow.Text = dt.Rows[0]["MsMenuDisplay"].ToString();
                txtDisplay.Text = dt.Rows[0]["MsMenuSub"].ToString();
                txtNameEN.Text = dt.Rows[0]["MsMenuNameEn"].ToString();
                txtType.Text = dt.Rows[0]["MsMenuType"].ToString();
                txtMain.Text = dt.Rows[0]["MsMenuMain"].ToString();
                txtName.Text = dt.Rows[0]["MsMenuNameTh"].ToString();
                cbbStatus.SelectedValue = dt.Rows[0]["MsMenuStatus"].ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                strOpe = "I";
                string[,] Parameter = new string[,]
                    {
                    {"@MsMenuCode", txtCode.Text},
                    {"@MsMenuForm", txtMenuForm.Text},
                    {"@MsMenuNameEn", txtNameEN.Text},
                    {"@MsMenuNameTh", txtName.Text},
                    {"@MsMenuDisplay", txtShow.Text},
                    {"@MsMenuType", txtType.Text},
                    {"@MsMenuMain", txtMain.Text},
                    {"@MsMenuSub",txtDisplay.Text},
                    {"@MsMenuStatus", cbbStatus.SelectedValue.ToString()},
                    {"@User", strUserId},
                    };

                bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

                if (Action == true)
                {
                    db.Operations("Spr_I_TblMasterMenu", Parameter, out strErr);

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

        private void FrmMangeMenu_KeyDown(object sender, KeyEventArgs e)
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

        private void txtType_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8) //&& !Char.IsControl(e.KeyChar))
            {
            }
            else
            {
                e.Handled = true;
                return;
            }
        }
    }
}