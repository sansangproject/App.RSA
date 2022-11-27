using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Global;
using SANSANG.Utilites.App.Model;

namespace SANSANG
{
    public partial class FrmMembers : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "SAVME00";
        public string AppName = "FrmMembers";
        public string Error = "";

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        private StoreConstant Store = new StoreConstant();
        private OperationConstant Operation = new OperationConstant();
        private DataListConstant DataList = new DataListConstant();
        private clsDelete Delete = new clsDelete();
        private clsEdit Edit = new clsEdit();
        private clsInsert Insert = new clsInsert();
        private clsFunction Function = new clsFunction();
        private clsMessage Message = new clsMessage();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();
        private clsImage Images = new clsImage();
        private TableConstant Table = new TableConstant();
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(20);
        private Timer Timer = new Timer();

        public string[,] Parameter = new string[,] { };

        public FrmMembers(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
        {
            InitializeComponent();

            UserId = UserIdLogin;
            UserName = UserNameLogin;
            UserSurname = UserSurNameLogin;
            UserType = UserTypeLogin;
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            Loading.Show();
            Timer.Interval = (1000);
            Timer.Start();
            Timer.Tick += new EventHandler(LoadList);
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "0"));
            //List.GetLists(cbbType, string.Format(DataList.StatusId, "5"));

            pbMain.Enabled = true;
            Clear();
            Timer.Stop();
        }

        public void Clear()
        {
            Function.ClearAll(gbForm);
            Images.ShowDefault(pbImage);
            Search(false);
        }

        public void ShowGridView(DataTable dt)
        {
            try
            {
                if (Function.GetRows(dt) == 0)
                {
                    GridView.DataSource = null;
                    txtCount.Text = Function.ShowNumberOfData(0);
                }
                else
                {
                    GridView.DataSource = null;
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "SNo", "Code", "NameEn", "Web", "TypeName", "Dates", "Id");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                    Function.showGridViewFormatFromStore(dtGrid, GridView,
                          " ลำดับ", 50, true, mc, mc
                        , "รหัส", 100, true, ml, ml
                        , "โลโก้", 150, true, ml, ml
                        , "เว็บไซต์", 150, true, ml, ml
                        , "ประเภท", 100, true, ml, ml
                        , "ข้อมูล ณ วันที่", 150, true, mc, mc
                        , "", 0, false, mc, mc
                    );

                    txtCount.Text = Function.ShowNumberOfData(dt.Rows.Count);
                    GridView.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SearchData(object sender, EventArgs e)
        {
            Search(true);
        }

        public void Search(bool Search)
        {
            try
            {
                //Parameter = new string[,]
                //{
                //    {"@Id", Search ? txtId.Text : ""},
                   
                //};

                //string Condition = Function.ShowConditons(GetCondition());
                //lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;
                //db.Get(Store.ManageLogo, Parameter, out Error, out dt);
                //ShowGridView(dt);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";

                strCondition += txtCode.Text != "" ? ", รหัสอ้างอิง: " + txtCode.Text : "";
               
                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return "";
            }
        }

        private void CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow RowIndex = this.GridView.Rows[e.RowIndex];

                Parameter = new string[,]
                {
                    {"@Id", RowIndex.Cells["Id"].Value.ToString()},
                };

                db.Get(Store.ManageLogo, Parameter, out Error, out dt);
                ShowData(dt);
            }
        }

        private void ShowData(DataTable dt)
        {
            try
            {
                if (Function.GetRows(dt) > 0)
                {
                    cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                    txtId.Text = dt.Rows[0]["Id"].ToString();
                   
                    Image Picture = new Bitmap(txtLocation.Text);
                    pbImage.Image = Picture.GetThumbnailImage(150, 150, null, new IntPtr());

                    GridView.Focus();
                }
            }
            catch (Exception ex)
            {
                pbImage.Image = null;
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }
        private void AddData(object sender, EventArgs e)
        {
            try
            {
                if ((Function.GetComboId(cbbStatus) != "0" || Function.GetComboId(cbbStatus) != "0") && !string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtLocation.Text))
                {
                    if (!Function.IsDuplicates(Table.Logo, txtName.Text, Function.GetComboId(cbbStatus), Detail: "Logo " + txtName.Text))
                    {
                        txtCode.Text = Function.GetCodes(Table.ExpenseId, "1014", "Generated");

                        Parameter = new string[,]
                        {
                            {"@Id", ""},
                        };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageLogo, Parameter, txtCode.Text, Details: "Logo " + txtUser.Text))
                        {
                            Clear();
                        }
                    }
                }
                else
                {
                    Message.ShowRequestData();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void EditData(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text != "")
                {
                    Parameter = new string[,]
                    {
                        {"@Id", txtId.Text},
                    };

                    if (Edit.Update(AppCode, AppName, UserId, Store.ManageLogo, Parameter, txtCode.Text, Details: "Logo " + txtName.Text))
                    {
                        Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void DeleteData(object sender, EventArgs e)
        {
            try
            {
                if (Delete.Drop(AppCode, AppName, UserId, 0, Table.Logo, txtCode, Details: "Logo " + txtName.Text, true))
                {
                    Clear();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void ClearData(object sender, EventArgs e)
        {
            Clear();
        }

        private void Browse(object sender, EventArgs e)
        {
            Images.SelectImage();

            foreach (ImageModel Photo in GlobalVar.ImageDataList)
            {
                //txtFileName.Text = Photo.Name;
                //txtFileType.Text = Photo.Type;
                //txtLocation.Text = Photo.Path;
                //txtFolder.Text = "";
                //cbbStatus.SelectedValue = "1000";
                //cbbPath.SelectedValue = "1040";

                Image Picture = new Bitmap(Photo.Path);
                pbImage.Image = Picture.GetThumbnailImage(150, 150, null, new IntPtr());

                break;
            }

            btnAdd.Focus();
        }

        private void FrmKeyDown(object sender, KeyEventArgs e)
        {
            string keyCode = Function.KeyPress(sender, e);

            if (keyCode == "Ctrl+S")
            {
                AddData(sender, e);
            }
            if (keyCode == "Ctrl+E")
            {
                EditData(sender, e);
            }
            if (keyCode == "Ctrl+D")
            {
                DeleteData(sender, e);
            }
            if (keyCode == "Altl+C")
            {
                ClearData(sender, e);
            }
            if (keyCode == "Enter")
            {
                Search(true);
            }
        }

        //public void GetDataGrid(DataTable dt)
        //{
        //    try
        //    {
        //        if (Fn.GetRows(dt) == 0)
        //        {
        //            dataGridView.DataSource = null;
        //            txtCount.Text = Fn.ShowNumberOfData(0);
        //        }
        //        else
        //        {
        //            DataTable dtGrid = new DataTable();
        //            dataGridView.DataSource = null;

        //            dtGrid = dt.DefaultView.ToTable(true, "Code", "UserName", "CardNumber", "ShopName", "PointName", "Id");

        //            DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
        //            DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

        //            Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
        //              "ลำดับ", 30, true, mc, mc
        //            , "รหัสอ้างอิง", 80, true, mc, mc
        //            , "ชื่อ", 120, true, ml, ml
        //            , "หมายเลข", 100, true, ml, ml
        //            , "ห้างร้าน/บริการ", 150, true, ml, ml
        //            , "สะสมแต้ม", 50, true, mc, mc
        //            , "", 0, false, mc, mc
        //            );

        //            txtCount.Text = Fn.ShowNumberOfData(dt.Rows.Count);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
        //    }
        //}

        //private void btnExit_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        //public void SearchData(bool Search)
        //{
        //    try
        //    {
        //        Parameter = new string[,]
        //        {
        //            {"@Id", "0"},
        //            {"@Code", Search? txtCode.Text : ""},
        //            {"@ShopId", Search? txtCode.Text : "0"},
        //            {"@CardId", Search? txtCode.Text : "0"},
        //            {"@UserId", Search? txtCode.Text : "0"},
        //            {"@Website", Search? txtCode.Text : ""},
        //            {"@Phone", Search? txtCode.Text : ""},
        //            {"@StatusId", Search? txtCode.Text : "0"},
        //            {"@User", Search? txtCode.Text : "0"},
        //            {"@IsPoint", Search? txtCode.Text : "2"},
        //        };

        //        db.Get("Store.SelectMembers", Parameter, out strErr, out dt);
        //        GetDataGrid(dt);
        //        lblCondition.Text = Fn.ShowConditons(GetCondition());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
        //    }
        //}

        //private string GetCondition()
        //{
        //    try
        //    {
        //        string strCondition = "";
        //        strCondition += txtCode.Text != "" ? ", รหัส : " + txtCode.Text : "";
        //        return strCondition;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
        //        return "";
        //    }
        //}

        //private void btnAdd_Click(object sender, EventArgs e)
        //{

        //}

        //private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{

        //}

        //private void btnEdit_Click(object sender, EventArgs e)
        //{
        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{

        //}

        //private void btnClear_Click(object sender, EventArgs e)
        //{
        //    Clear();
        //}

        //private void picExcel_Click(object sender, EventArgs e)
        //{
        //}

        //private void txtEmail_Leave(object sender, EventArgs e)
        //{
        //    string stEmail = "";

        //    if (txtEmail.Text.Contains("@"))
        //    {
        //        Boolean d = txtEmail.Text.Contains("@");
        //    }
        //    else
        //    {
        //        if (txtEmail.Text != "" || txtEmail.Text != "-")
        //        {
        //            if (rdbHMail.Checked == true)
        //            {
        //                stEmail = txtEmail.Text;
        //                txtEmail.Text = stEmail + "@hotmail.com";
        //            }
        //            if (rdbGMail.Checked == true)
        //            {
        //                stEmail = txtEmail.Text;
        //                txtEmail.Text = stEmail + "@gmail.com";
        //            }
        //        }
        //    }
        //}

        //private void txtPhone_Leave(object sender, EventArgs e)
        //{
        //    if (txtPhone.Text != "")
        //    {
        //        txtPhone.Text = Fn.ConvertPhoneNumber(txtPhone.Text);
        //    }
        //}

        //public string phoneformat(string phnumber)
        //{
        //    String phone = phnumber;
        //    string countrycode = phone.Substring(0, 3);
        //    string Areacode = phone.Substring(3, 3);
        //    string number = phone.Substring(6, phone.Length - 6);

        //    string phnumberFormat = countrycode + "-" + Areacode + "-" + number;

        //    return phnumberFormat;
        //}

        //private void txtName_Leave(object sender, EventArgs e)
        //{
        //    string str = txtName.Text;
        //    string upperStr = str.ToUpper();
        //    txtName.Text = upperStr;
        //}

        //private void txtSurname_Leave(object sender, EventArgs e)
        //{
        //    string str = txtSurname.Text;
        //    string upperStr = str.ToUpper();
        //    txtSurname.Text = upperStr;
        //}

        //private void FrmSaveMember_KeyDown(object sender, KeyEventArgs e)
        //{
        //    string keyCode = Fn.keyPress(sender, e);

        //    if (keyCode == "Ctrl+S")
        //    {
        //        btnAdd_Click(sender, e);
        //    }
        //    if (keyCode == "Ctrl+E")
        //    {
        //        btnEdit_Click(sender, e);
        //    }
        //    if (keyCode == "Ctrl+D")
        //    {
        //        btnDelete_Click(sender, e);
        //    }
        //    if (keyCode == "Ctrl+F")
        //    {
        //        btnSearch_Click(sender, e);
        //    }
        //    if (keyCode == "Alt+S")
        //    {
        //    }
        //    if (keyCode == "Alt+C")
        //    {
        //        btnClear_Click(sender, e);
        //    }
        //}

        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    SearchData(true);
        //}

        //private void cbShowPass_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtPassword.PasswordChar = cbShowPass.Checked ? '\0' : '*';
        //}

        //private void txtPhone_TextChanged(object sender, EventArgs e)
        //{
        //}

        //private void cbbShop_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!Fn.IsDefault(cbbShop))
        //    {
        //        Images.ShowImage(pbImage, Id: cbbShop.SelectedValue.ToString());
        //    }
        //}
    }
}