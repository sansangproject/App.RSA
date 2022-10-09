using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmSaveMember : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string strAppCode = "SAVMEM00";
        public string strAppName = "FrmSaveMember";
        public string strErr = "";
        public string errMes = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        private DataTable dt = new DataTable();

        private clsDate Date = new clsDate();
        private clsSearch Search = new clsSearch();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();
        private clsEvent Event = new clsEvent();
        private clsFormat Format = new clsFormat();
        private clsHelpper Help = new clsHelpper();
        private clsTime Time = new clsTime();
        private clsImage Images = new clsImage();
        private clsSetting Setting = new clsSetting();

        private dbConnection db = new dbConnection();

        private DataListConstant DataList = new DataListConstant();
        private OperationConstant Operations = new OperationConstant();
        private StoreConstant Store = new StoreConstant();

        private Timer Timer = new Timer();
        public string[,] Parameter = new string[,] { };

        public FrmSaveMember(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmSaveMember_Load(object sender, EventArgs e)
        {
            int sec = 2;
            Timer.Interval = (sec * 1000);
            Timer.Tick += new EventHandler(LoadList);
            Timer.Start();
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetListId(cbbShop, DataList.ShopId);
            List.GetListId(cbbCard, DataList.CardId);
            List.GetListId(cbbUser, DataList.UserId);
            List.GetListId(cbbStatus, DataList.StatusId, "0");
            gbMain.Enabled = true;
            Clear();
            Timer.Stop();
        }

        public void Clear()
        {
            cbbShop.SelectedValue = 0;
            cbbStatus.SelectedValue = 0;
            cbbCard.SelectedValue = 0;
            cbbUser.SelectedValue = 0;
            pbImage.Image = null;
            SearchData(false);
        }

        public void GetDataGrid(DataTable dt)
        {
            try
            {
                if (Fn.GetRows(dt) == 0)
                {
                    dataGridView.DataSource = null;
                    txtCount.Text = Fn.ShowNumberOfData(0);
                }
                else
                {
                    DataTable dtGrid = new DataTable();
                    dataGridView.DataSource = null;

                    dtGrid = dt.DefaultView.ToTable(true, "Code", "UserName", "CardNumber", "ShopName", "PointName", "Id");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                    Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                      "ลำดับ", 30, true, mc, mc
                    , "รหัสอ้างอิง", 80, true, mc, mc
                    , "ชื่อ", 120, true, ml, ml
                    , "หมายเลข", 100, true, ml, ml
                    , "ห้างร้าน/บริการ", 150, true, ml, ml
                    , "สะสมแต้ม", 50, true, mc, mc
                    , "", 0, false, mc, mc
                    );

                    txtCount.Text = Fn.ShowNumberOfData(dt.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SearchData(bool Search)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", "0"},
                    {"@Code", Search? txtCode.Text : ""},
                    {"@ShopId", Search? txtCode.Text : "0"},
                    {"@CardId", Search? txtCode.Text : "0"},
                    {"@UserId", Search? txtCode.Text : "0"},
                    {"@Website", Search? txtCode.Text : ""},
                    {"@Phone", Search? txtCode.Text : ""},
                    {"@StatusId", Search? txtCode.Text : "0"},
                    {"@User", Search? txtCode.Text : "0"},
                    {"@IsPoint", Search? txtCode.Text : "2"},
                };

                db.Get("Store.SelectMembers", Parameter, out strErr, out dt);
                GetDataGrid(dt);
                lblCondition.Text = Fn.ShowConditons(GetCondition());
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";
                strCondition += txtCode.Text != "" ? ", รหัส : " + txtCode.Text : "";
                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
                return "";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void picExcel_Click(object sender, EventArgs e)
        {
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            string stEmail = "";

            if (txtEmail.Text.Contains("@"))
            {
                Boolean d = txtEmail.Text.Contains("@");
            }
            else
            {
                if (txtEmail.Text != "" || txtEmail.Text != "-")
                {
                    if (rdbHMail.Checked == true)
                    {
                        stEmail = txtEmail.Text;
                        txtEmail.Text = stEmail + "@hotmail.com";
                    }
                    if (rdbGMail.Checked == true)
                    {
                        stEmail = txtEmail.Text;
                        txtEmail.Text = stEmail + "@gmail.com";
                    }
                }
            }
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (txtPhone.Text != "")
            {
                txtPhone.Text = Fn.ConvertPhoneNumber(txtPhone.Text);
            }
        }

        public string phoneformat(string phnumber)
        {
            String phone = phnumber;
            string countrycode = phone.Substring(0, 3);
            string Areacode = phone.Substring(3, 3);
            string number = phone.Substring(6, phone.Length - 6);

            string phnumberFormat = countrycode + "-" + Areacode + "-" + number;

            return phnumberFormat;
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            string str = txtName.Text;
            string upperStr = str.ToUpper();
            txtName.Text = upperStr;
        }

        private void txtSurname_Leave(object sender, EventArgs e)
        {
            string str = txtSurname.Text;
            string upperStr = str.ToUpper();
            txtSurname.Text = upperStr;
        }

        private void FrmSaveMember_KeyDown(object sender, KeyEventArgs e)
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
                btnSearch_Click(sender, e);
            }
            if (keyCode == "Alt+S")
            {
            }
            if (keyCode == "Alt+C")
            {
                btnClear_Click(sender, e);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData(true);
        }

        private void cbShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = cbShowPass.Checked ? '\0' : '*';
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
        }

        private void cbbShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Fn.IsDefault(cbbShop))
            {
                Images.ShowImage(pbImage, Id: cbbShop.SelectedValue.ToString());
            }
        }
    }
}