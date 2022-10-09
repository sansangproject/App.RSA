using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmRebates : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "SAREB00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string CheckGreenCode = "IMG600218805-6";
        public string CrossRedCode = "IMG600218806-7";
        public string ExclamationYellowCode = "IMG600218807-8";

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

        public FrmRebates(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            

            List.GetList(cbbUsed, "9", "Status");
            List.getWhereList(cbbProvince, "", "", "Y", "ProvinceAll");

            Clear();
        }

        public int countRow()
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@RebateId", ""},
                    {"@RebateCode", ""},
                    {"@RebateName", ""},
                    {"@RebateCity", ""},
                    {"@RebateAmphur", "0"},
                    {"@RebateProvince", "0"},
                    {"@RebateRank", "0"},
                    {"@RebateGroup", "0"},
                    {"@RebateAmount", ""},
                    {"@RebateUsed", "0"},
                    {"@RebateJoin", "0"},
                    {"@RebateRemark", ""},
                    {"@RebateStatus", "0"},
                };

                db.Get("Spr_S_TblSaveRebate", Parameter, out strErr, out dt);
                return dt.Rows.Count + 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void Clear()
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtCity.Text = "";
            txtRemark.Text = "";
            txtAmount.Text = "";

            cbbProvince.SelectedValue = "0";
            cbbAmphur.SelectedValue = "0";
            cbbUsed.SelectedValue = 0;

            cbbJoin.DataSource = List.GetJoinList();
            cbbJoin.DisplayMember = "value";
            cbbJoin.ValueMember = "index";
            cbbJoin.SelectedIndex = 0;

            //cbbGroup.DataSource = List.getPersonalGroupList();
            cbbGroup.DisplayMember = "value";
            cbbGroup.ValueMember = "index";
            cbbGroup.SelectedIndex = 0;

            cbbRank.DataSource = List.getRankList();
            cbbRank.DisplayMember = "value";
            cbbRank.ValueMember = "index";
            cbbRank.SelectedIndex = 0;

            txtName.Focus();
            txtNumber.Text = Convert.ToString(countRow());
            SearchData();
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

            if (row != 0)
            {
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "RebateName", "RebateCity", "RebateAmount", "MsStatusNameTh", "RebateId", "RebateCode");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                      "ลำดับ", 50, true, mc, mc
                    , "ชื่อ", 150, true, ml, ml
                    , "ที่อยู่", 150, true, ml, ml
                    , "จำนวนเงิน", 150, true, ml, ml
                    , "สถานะ", 100, true, ml, ml
                    , "", 0, false, mc, mc
                    , "", 0, false, mc, mc
                    );

                picExcel.Visible = true;
                lblCount.Text = row.ToString();
            }
            else
            {
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                txtCode.Text = "";
                txtCode.Focus();
                lblCount.Text = "0";
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
            if (txtName.Text != "" && txtAmount.Text != "")
            {
                try
                {
                    txtCode.Text = Fn.GetCodes("123", "", "Generated");
                    strOpe = "I";

                    string[,] Parameter = new string[,]
                    {
                        {"@RebateCode", txtCode.Text},
                        {"@RebateName", txtName.Text},
                        {"@RebateCity", txtCity.Text},
                        {"@RebateAmphur", Fn.getComboBoxValue(cbbAmphur)},
                        {"@RebateProvince", Fn.getComboBoxValue(cbbProvince)},
                        {"@RebateRank", Fn.getComboBoxValue(cbbRank)},
                        {"@RebateGroup", Fn.getComboBoxValue(cbbGroup)},
                        {"@RebateAmount", txtAmount.Text},
                        {"@RebateUsed", Fn.getComboBoxValue(cbbUsed)},
                        {"@RebateJoin", Fn.getComboBoxValue(cbbJoin)},
                        {"@RebateRemark", txtRemark.Text},
                        {"@RebateStatus", "Y"},
                        {"@User", strUserId},
                    };

                    bool Action = Message.MessageConfirmation(strOpe, strUserId + "\n" + txtName.Text + " " + txtAmount.Text + " บาท", "");

                    if (Action == true)
                    {
                        db.Operations("Spr_I_TblSaveRebate", Parameter, out strErr);

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
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                DataTable dt = new DataTable();

                string[,] Parameter = new string[,]
                {
                    {"@RebateId", row.Cells["RebateId"].Value.ToString()},
                    {"@RebateCode", ""},
                    {"@RebateName", ""},
                    {"@RebateCity", ""},
                    {"@RebateAmphur", "0"},
                    {"@RebateProvince", "0"},
                    {"@RebateRank", "0"},
                    {"@RebateGroup", "0"},
                    {"@RebateAmount", ""},
                    {"@RebateUsed", "0"},
                    {"@RebateJoin", "0"},
                    {"@RebateRemark", ""},
                    {"@RebateStatus", "0"},
                };

                db.Get("Spr_S_TblSaveRebate", Parameter, out strErr, out dt);

                cbbProvince.SelectedValue = dt.Rows[0]["RebateProvince"].ToString();
                cbbAmphur.SelectedValue = dt.Rows[0]["RebateAmphur"].ToString();
                cbbGroup.SelectedValue = dt.Rows[0]["RebateGroup"].ToString();
                cbbRank.SelectedValue = dt.Rows[0]["RebateRank"].ToString();
                cbbJoin.SelectedValue = dt.Rows[0]["RebateJoin"].ToString();
                cbbUsed.SelectedValue = dt.Rows[0]["RebateUsed"].ToString();

                txtNumber.Text = dt.Rows[0]["RebateId"].ToString();
                txtCode.Text = dt.Rows[0]["RebateCode"].ToString();
                txtName.Text = dt.Rows[0]["RebateName"].ToString();
                txtCity.Text = dt.Rows[0]["RebateCity"].ToString();
                txtRemark.Text = dt.Rows[0]["RebateRemark"].ToString();
                txtAmount.Text = dt.Rows[0]["RebateAmount"].ToString();
                txtTotal.Text = dt.Rows[0]["RebateAmount"].ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            strOpe = "D";

            string[,] Parameter = new string[,]
                {
                    {"@RebateCode", txtCode.Text},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_D_TblSaveRebate", Parameter, out strErr);

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
            try
            {
                strUserId = txtCode.Text = Fn.GetCodes("123", "", "Generated");
                strOpe = "U";

                if (txtCode.Text != "")
                {
                    string[,] Parameter = new string[,]
                        {
                            {"@RebateId", txtNumber.Text},
                            {"@RebateCode", txtCode.Text},
                            {"@RebateName", txtName.Text},
                            {"@RebateCity", txtCity.Text},
                            {"@RebateAmphur", cbbAmphur.SelectedValue.ToString()},
                            {"@RebateProvince", cbbProvince.SelectedValue.ToString()},
                            {"@RebateRank", cbbRank.SelectedValue.ToString()},
                            {"@RebateGroup", cbbGroup.SelectedValue.ToString()},
                            {"@RebateAmount", txtAmount.Text},
                            {"@RebateUsed", cbbUsed.SelectedValue.ToString()},
                            {"@RebateJoin", cbbJoin.SelectedValue.ToString()},
                            {"@RebateRemark", txtRemark.Text},
                            {"@RebateStatus", "E"},
                            {"@User", strUserId},
                        };

                    bool Action = Message.MessageConfirmation(strOpe, strUserId + "\n" + txtName.Text + " " + txtAmount.Text + " บาท", "");

                    if (Action == true)
                    {
                        db.Operations("Spr_U_TblSaveRebate", Parameter, out strErr);

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

        public void SearchData()
        {
            try
            {
                Parameter = new string[,]
                    {
                        {"@RebateId", ""},
                        {"@RebateCode", txtCode.Text},
                        {"@RebateName", txtName.Text},
                        {"@RebateCity", txtCity.Text},
                        {"@RebateProvince", cbbProvince.SelectedValue.ToString()},
                        {"@RebateAmphur", cbbAmphur.Items.Count == 0 ? "0": cbbAmphur.SelectedValue.ToString()},
                        {"@RebateRank", cbbRank.SelectedValue.ToString()},
                        {"@RebateGroup", cbbGroup.SelectedValue.ToString()},
                        {"@RebateAmount", txtAmount.Text},
                        {"@RebateUsed", cbbUsed.SelectedValue.ToString()},
                        {"@RebateJoin", cbbJoin.SelectedValue.ToString()},
                        {"@RebateRemark", txtRemark.Text},
                        {"@RebateStatus", "0"},
                    };

                db.Get("Spr_S_TblSaveRebate", Parameter, out strErr, out dt);

                Double sumAmount = 0;
                int sumRow = 0;
                sumRow = dt.Rows.Count - 1;

                for (int i = 0; i <= sumRow; i++)
                {
                    sumAmount += Convert.ToDouble(dt.Rows[i]["RebateAmount"].ToString());
                }

                txtTotal.Text = sumAmount.ToString("###,##0");

                getDataGrid(dt);
            }
            catch (Exception)
            {
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtName.Focus();
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtAmount.Focus();
            }
        }

        private void txtCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cbbProvince.Focus();
            }
        }

        private void cbbProvince_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cbbAmphur.Focus();
            }
        }

        private void cbbAmphur_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cbbUsed.Focus();
            }
        }

        private void txtRemark_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                btnAdd_Click(sender, e);
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtCity.Focus();
            }
            if (Char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8) //&& !Char.IsControl(e.KeyChar))
            {
            }
            else
            {
                e.Handled = true;
                return;
            }
        }

        private void cbbStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cbbGroup.Focus();
            }
        }

        private void cbbGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cbbRank.Focus();
            }
        }

        private void cbbRank_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                cbbJoin.Focus();
            }
        }

        private void cbbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.getWhereList(cbbAmphur, "", cbbProvince.SelectedValue.ToString(), "Y", "Amphur");
            }
            catch (Exception)
            {
            }
        }

        private void FrmSaveRebate_KeyDown(object sender, KeyEventArgs e)
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
            if (keyCode == "Ctrl+F")
            {
                btnFind_Click(sender, e);
            }
            if (keyCode == "Alt+C")
            {
                btnClear_Click(sender, e);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void cbbJoin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtRemark.Focus();
            }
        }
    }
}