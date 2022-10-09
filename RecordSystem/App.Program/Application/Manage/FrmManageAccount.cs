using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmManageAccount : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;
        
        public string strAppCode = "MANAC00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string filePath = "SPA591012028-5";
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
        private clsImage Images = new clsImage();
        public FrmManageAccount(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmManageAccount_Load(object sender, EventArgs e)
        {
            List.GetList(cbbStatus, "0", "Status");
            List.GetList(cbbBank, "Y", "Bank");
            List.GetList(cbbType, "Y", "AccountType");

            Clear();
        }

        public void Clear()
        {
            txtCode.Text = "";
            txtCode.Focus();

            txtNumber.Text = "";
            txtName.Text = "";
            txtFileLocation.Text = "";
            txtDetail.Text = "";

            txtFileName.Text = "";
            txtFileType.Text = "";

            Images.ShowDefault(picFile);

            cbbStatus.SelectedValue = "0";
            cbbBank.SelectedValue = "0";
            cbbBranch.SelectedValue = "0";
            cbbType.SelectedValue = "0";

            string[,] Parameter = new string[,]
                {
                    {"@AccountId",""},
                    {"@AccountCode",""},
                    {"@AccountNumber",""},
                    {"@AccountName",""},
                    {"@AccountBank","0"},
                    {"@AccountBranch","0"},
                    {"@AccountType","0"},
                    {"@AccountDetail",""},
                    {"@AccountRemark",""},
                    {"@AccountFileLocation",""},
                    {"@AccountStatus","0"},
                };

            db.Get("Spr_S_TblSaveAccount", Parameter, out strErr, out dt);
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
                dtGrid = dt.DefaultView.ToTable(true, "AccountNumber", "AccountName", "MsBranchNameTh", "MsBankNameTh", "MsStatusNameTh", "AccountCode");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                    "ลำดับ", 50, true, mc, mc
                    , "เลขที่บัญชี", 150, true, mc, mc
                    , "ชื่อบัญชี", 150, true, ml, ml
                    , "สาขา", 200, true, ml, ml
                    , "ธนาคาร", 200, true, ml, ml
                    , "สถานะ", 100, true, mc, mc
                    , "รหัส", 100, false, mc, mc
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

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                txtFileLocation.Text = Fn.CopyImage(filePath, txtFileLocation.Text, strUserId, txtFileType.Text, strOpe);

                string[,] Parameter = new string[,]
                {
                   {"@AccountCode", txtCode.Text},
                   {"@DeleteType", "0"},
                   {"@User", strUserId}
                };

                Message.MessageConfirmation(strOpe, txtCode.Text, "Account No: " + txtNumber.Text);

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
                        db.Operations("Spr_D_TblSaveAccount", Parameter, out strErr);

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
            strOpe = "U";

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                txtFileLocation.Text = Fn.CopyImage(filePath, txtFileLocation.Text, strUserId, txtFileType.Text, strOpe);

                string[,] Parameter = new string[,]
                {
                    {"@AccountCode", txtCode.Text},
                    {"@AccountNumber",txtNumber.Text},
                    {"@AccountName",txtName.Text},
                    {"@AccountBank",cbbBank.SelectedValue.ToString()},
                    {"@AccountBranch",cbbBranch.SelectedValue.ToString()},
                    {"@AccountType",cbbType.SelectedValue.ToString()},
                    {"@AccountDetail",txtDetail.Text},
                    {"@AccountRemark",""},
                    {"@AccountFileLocation",txtFileLocation.Text},
                    {"@AccountFileType",txtFileType.Text},
                    {"@AccountStatus",cbbStatus.SelectedValue.ToString()},
                    {"@User",strUserId },
                };

                Message.MessageConfirmation(strOpe, txtCode.Text, "Account No: " + txtNumber.Text);

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
                        db.Operations("Spr_U_TblSaveAccount", Parameter, out strErr);

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
                    {"@AccountId",""},
                    {"@AccountCode",txtCode.Text},
                    {"@AccountNumber",txtNumber.Text},
                    {"@AccountName",txtName.Text},
                    {"@AccountBank",cbbBank.SelectedValue.ToString()},
                    {"@AccountBranch",cbbBranch.SelectedValue.ToString()},
                    {"@AccountType",cbbType.SelectedValue.ToString()},
                    {"@AccountDetail",txtDetail.Text},
                    {"@AccountRemark",""},
                    {"@AccountFileLocation",""},
                    {"@AccountStatus",cbbStatus.SelectedValue.ToString()},
                };

            db.Get("Spr_S_TblSaveAccount", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtCode.Text = Fn.GetCodes("113", "", "Generated");
            strOpe = "I";

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                txtFileLocation.Text = Fn.CopyImage(filePath, txtFileLocation.Text, strUserId, txtFileType.Text, strOpe);

                string[,] Parameter = new string[,]
                {
                    {"@AccountCode", txtCode.Text},
                    {"@AccountNumber", txtNumber.Text},
                    {"@AccountName", txtName.Text},
                    {"@AccountBank", cbbBank.SelectedValue.ToString()},
                    {"@AccountBranch", cbbBranch.SelectedValue.ToString()},
                    {"@AccountType", cbbType.SelectedValue.ToString()},
                    {"@AccountFileType", txtFileType.Text},
                    {"@AccountDetail", txtDetail.Text},
                    {"@AccountRemark", ""},
                    {"@AccountFileLocation", txtFileLocation.Text},
                    {"@AccountStatus", cbbStatus.SelectedValue.ToString()},
                    {"@User", strUserId },
                };

                Message.MessageConfirmation(strOpe, txtCode.Text, "Account No: " + txtNumber.Text);

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
                        db.Operations("Spr_I_TblSaveAccount", Parameter, out strErr);

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

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                DataTable dt = new DataTable();

                string[,] Parameter = new string[,]
                    {
                        {"@AccountId",""},
                        {"@AccountCode", row.Cells["AccountCode"].Value.ToString()},
                        {"@AccountNumber",""},
                        {"@AccountName",""},
                        {"@AccountBank","0"},
                        {"@AccountBranch","0"},
                        {"@AccountType","0"},
                        {"@AccountDetail",""},
                        {"@AccountRemark",""},
                        {"@AccountFileLocation",""},
                        {"@AccountStatus","0"},
                     };

                db.Get("Spr_S_TblSaveAccount", Parameter, out strErr, out dt);

                txtCode.Text = dt.Rows[0]["AccountCode"].ToString();

                txtNumber.Text = dt.Rows[0]["AccountNumber"].ToString();
                txtName.Text = dt.Rows[0]["AccountName"].ToString();
                txtDetail.Text = dt.Rows[0]["AccountDetail"].ToString();

                cbbStatus.SelectedValue = dt.Rows[0]["AccountStatus"].ToString();
                cbbBank.SelectedValue = dt.Rows[0]["AccountBank"].ToString();
                cbbBranch.SelectedValue = dt.Rows[0]["AccountBranch"].ToString();
                cbbType.SelectedValue = dt.Rows[0]["AccountType"].ToString();

                if (dt.Rows[0]["AccountFileLocation"].ToString() == "" || dt.Rows[0]["AccountFileLocation"].ToString() == "-")
                {
                    txtFileLocation.Text = "";
                }
                else
                {
                    txtFileLocation.Text = Fn.getImagePath(dt.Rows[0]["AccountFileLocation"].ToString(), dt.Rows[0]["AccountCode"].ToString(), dt.Rows[0]["AccountFileType"].ToString());
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
        }

        private void Frm_KeyDown(object sender, KeyEventArgs e)
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

        private void cbbBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.GetList(cbbBranch, cbbBank.SelectedValue.ToString() + ",Y", "Branch");
                cbbBranch.SelectedValue = "0";
            }
            catch (Exception)
            {
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = new Bitmap(open.FileName);
                txtFileName.Text = Path.GetFileNameWithoutExtension(open.FileName);
                txtFileType.Text = Path.GetExtension(open.FileName);
                txtFileLocation.Text = open.FileName;
                picFile.Image = img.GetThumbnailImage(160, 150, null, new IntPtr());
                open.RestoreDirectory = true;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (txtFileLocation.Text != "")
            {
                FrmShowImage FrmShowImage = new FrmShowImage(txtCode.Text);
                FrmShowImage.Show();
            }
        }

        private void txtFileLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Images.ShowImage(picFile, Code: txtFileLocation.Text);
            }
            catch (Exception)
            {
            }
        }
    }
}