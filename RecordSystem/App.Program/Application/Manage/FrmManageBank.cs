using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmManageBank : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string strAppCode = "MANBA00";
        public string strErr = "";
        public string strAppName = "FrmManageBank";
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
        private clsLog Log = new clsLog();
        private clsTextbox TSS = new clsTextbox();
        private Timer timer = new Timer();
        public string[,] Parameter = new string[,] { };

        public FrmManageBank(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmManageBank_Load(object sender, EventArgs e)
        {
            int sec = 2;
            timer.Interval = (sec * 1000);
            timer.Tick += new EventHandler(LoadList);
            timer.Start();
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetList(cbbStatus, "0", "Status");
            start = true;
            gbFrm.Enabled = true;
            Clear();
            timer.Stop();
        }

        public void Clear()
        {
            txtCode.Text = "";
            txtAddress.Text = "";
            txtNameEn.Text = "";
            txtNameTh.Text = "";
            txtShortCode.Text = "";
            txtSwiftCode.Text = "";
            txtPhone.Text = "";
            txtFax.Text = "";
            txtWebsite.Text = "";
            cbbStatus.SelectedValue = "0";
            btnSearch.Focus();

            string[,] Parameter = new string[,]
            {
                    {"@MsBankCode", ""},
                    {"@MsBankNameTh", ""},
                    {"@MsBankNameEn", ""},
                    {"@MsBankAddress", ""},
                    {"@MsBankStatus",  "0"},
                    {"@MsBankShort", ""},
                    {"@MsBankSwiftCode", ""},
                    {"@MsBankTel", ""},
                    {"@MsBankFax", ""},
                    {"@MsBankWebsite", ""},
            };

            db.Get("Spr_S_TblMasterBank", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        public void getDataGrid(DataTable dt)
        {
            int row = dt.Rows.Count;

            if (row == 0)
            {
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                txtCount.Text = "0";
            }
            else
            {
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "MsBankCode", "MsBankShort", "MsBankNameTh", "MsBankNameEn", "MsStatusNameTh", "Date", "MsBankId");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                      "ลำดับ", 50, true, mc, mc
                    , "รหัส", 50, true, mc, mc
                    , "ตัวย่อ", 50, true, mc, mc
                    , "ชื่อธนาคาร", 250, true, ml, ml
                    , "ชื่อภาษาอังกฤษ", 250, true, ml, ml
                    , "สถานะ", 50, true, mc, mc
                    , "ข้อมูล ณ วันที่", 150, true, mc, mc
                    , "", 0, false, mc, mc
                    );

                picExcel.Visible = true;
                txtCount.Text = row.ToString();
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
            if (txtCode.Text != "")
            {
                strOpe = "D";

                string[,] Parameter = new string[,]
                {
                {"@MsBankCode", txtCode.Text},
                {"@DeleteType", "1"},
                {"@User", strUserId},
                };

                Message.MessageConfirmation(strOpe, txtCode.Text, txtNameTh.Text + " (" + txtShortCode.Text + ")");

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
                        db.Operations("Spr_D_TblMasterBank", Parameter, out strErr);

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
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                strOpe = "U";

                string[,] Parameter = new string[,]
                {
                    {"@User", strUserId},
                    {"@MsBankCode", txtCode.Text},
                    {"@MsBankNameTh", txtNameTh.Text},
                    {"@MsBankNameEn", txtNameEn.Text},
                    {"@MsBankAddress", txtAddress.Text},
                    {"@MsBankStatus", Fn.getComboBoxValue(cbbStatus)},
                    {"@MsBankShort", txtShortCode.Text},
                    {"@MsBankSwiftCode", txtSwiftCode.Text},
                    {"@MsBankTel", txtPhone.Text},
                    {"@MsBankFax", txtFax.Text},
                    {"@MsBankWebsite", txtWebsite.Text},
                };

                Message.MessageConfirmation(strOpe, txtCode.Text, txtNameEn.Text + " (" + txtShortCode.Text + ")");

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
                        db.Operations("Spr_U_TblMasterBank", Parameter, out strErr);

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
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            string[,] Parameter = new string[,]
                {
                    {"@MsBankCode", txtCode.Text},
                    {"@MsBankNameTh", txtNameTh.Text},
                    {"@MsBankNameEn", txtNameEn.Text},
                    {"@MsBankAddress", txtAddress.Text},
                    {"@MsBankShort", txtShortCode.Text},
                    {"@MsBankSwiftCode", txtSwiftCode.Text},
                    {"@MsBankTel", txtPhone.Text},
                    {"@MsBankFax", txtFax.Text},
                    {"@MsBankWebsite", txtWebsite.Text},
                    {"@MsBankStatus", Fn.getComboBoxValue(cbbStatus)},
                };

            db.Get("Spr_S_TblMasterBank", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                txtCode.Text = Fn.GetCodes("70", "", "Generated");
                strOpe = "I";

                string[,] Parameter = new string[,]
                {
                    {"@User", strUserId},
                    {"@MsBankCode", txtCode.Text},
                    {"@MsBankNameTh", txtNameTh.Text},
                    {"@MsBankNameEn", txtNameEn.Text},
                    {"@MsBankAddress", txtAddress.Text},
                    {"@MsBankStatus", Fn.getComboBoxValue(cbbStatus)},
                    {"@MsBankShort", txtShortCode.Text},
                    {"@MsBankSwiftCode", txtSwiftCode.Text},
                    {"@MsBankTel", txtPhone.Text},
                    {"@MsBankFax", txtFax.Text},
                    {"@MsBankWebsite", txtWebsite.Text},
                };

                Message.MessageConfirmation(strOpe, txtCode.Text, txtNameEn.Text + " (" + txtShortCode.Text + ")");

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
                        db.Operations("Spr_I_TblMasterBank", Parameter, out strErr);

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
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
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
                    {"@MsBankCode", row.Cells["MsBankCode"].Value.ToString()},
                    {"@MsBankNameTh", ""},
                    {"@MsBankNameEn", ""},
                    {"@MsBankAddress", ""},
                    {"@MsBankStatus",  "0"},
                    {"@MsBankShort", ""},
                    {"@MsBankSwiftCode", ""},
                    {"@MsBankTel", ""},
                    {"@MsBankFax", ""},
                    {"@MsBankWebsite", ""},
                };

                db.Get("Spr_S_TblMasterBank", Parameter, out strErr, out dt);

                cbbStatus.SelectedValue = dt.Rows[0]["MsBankStatus"].ToString();

                txtCode.Text = dt.Rows[0]["MsBankCode"].ToString();
                txtNameTh.Text = dt.Rows[0]["MsBankNameTh"].ToString();
                txtNameEn.Text = dt.Rows[0]["MsBankNameEn"].ToString();
                txtAddress.Text = dt.Rows[0]["MsBankAddress"].ToString();
                txtShortCode.Text = dt.Rows[0]["MsBankShort"].ToString();
                txtSwiftCode.Text = dt.Rows[0]["MsBankSwiftCode"].ToString();
                txtPhone.Text = dt.Rows[0]["MsBankTel"].ToString();
                txtFax.Text = dt.Rows[0]["MsBankFax"].ToString();
                txtWebsite.Text = dt.Rows[0]["MsBankWebsite"].ToString();
                btnSearch.Focus();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
        }

        private void FrmManageBank_KeyDown(object sender, KeyEventArgs e)
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
    }
}