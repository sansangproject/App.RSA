using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmMangeShop : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "MANSH00";
        public string AppName = "FrmMangeShop";
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
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(25);
        private Timer Timer = new Timer();
        private bool Start = false;
        public string[,] Parameter = new string[,] { };

        public FrmMangeShop(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
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
            Start = true;
            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "0"));
            List.GetLists(cbbProvince, string.Format(DataList.ProvinceId, "status", "1000"));
            List.GetLists(cbbAmphoe, string.Format(DataList.AmphoeId, "status", "1000"));
            List.GetLists(cbbTambol, string.Format(DataList.TambolId, "status", "1000"));
            List.GetLists(cbbLogo, DataList.LogoId);
            List.GetLists(cbbType, string.Format(DataList.StatusId, "5"));

            gbForm.Enabled = true;
            Clear();
            Timer.Stop();
        }

        public void Clear()
        {
            Function.ClearAll(gbForm);
            Start = false;
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
                    dtGrid = dt.DefaultView.ToTable(true, "SNo", "Code", "Display", "AmphoeName", "ProvinceName", "Category", "Id");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                    Function.showGridViewFormatFromStore(dtGrid, GridView,
                        " ลำดับ", 50, true, mc, mc
                        , "รหัส", 150, true, ml, ml
                        , "ห้างร้าน", 300, true, ml, ml
                        , "อำเภอ/เขต", 100, true, ml, ml
                        , "จังหวัด", 100, true, ml, ml
                        , "ประเภท", 100, true, mc, mc
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
                Parameter = new string[,]
                {
                    {"@Id", Search ? txtId.Text : ""},
                    {"@Code", Search ? txtCode.Text : ""},
                    {"@Name", Search ? txtName.Text : ""},
                    {"@Status", Search ? Function.GetComboId(cbbStatus) : "0"},
                    {"@User", ""},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.SelectAbbr},
                    {"@NameEn", Search ? txtNameEn.Text : ""},
                    {"@Display", Search ? txtDisplay.Text : ""},
                    {"@Detail", ""},
                    {"@Address", Search ? txtAddress.Text : ""},
                    {"@TambolId", Search ? Function.GetComboId(cbbTambol) : "0"},
                    {"@AmphoeId", Search ? Function.GetComboId(cbbAmphoe) : "0"},
                    {"@ProvinceId", Search ? Function.GetComboId(cbbProvince) : "0"},
                    {"@Postcode", Search ? txtPostcode.Text : ""},
                    {"@Phone", Search ? txtPhone.Text : ""},
                    {"@Mobile", Search ? txtMobile.Text : ""},
                    {"@Fax", Search ? txtFax.Text : ""},
                    {"@Email", Search ? txtEmail.Text : ""},
                    {"@Line", Search ? txtLine.Text : ""},
                    {"@Web", Search ? txtWeb.Text : ""},
                    {"@Manager", Search ? txtManager.Text : ""},
                    {"@OfficeHours", Search ? txtOfficeHours.Text : ""},
                    {"@LogoId", Search ? Function.GetComboId(cbbLogo) : "0"},
                    {"@TypeId", Search ? Function.GetComboId(cbbType) : "0"},
                    {"@Remark", Search ? txtRemark.Text : ""},
                };

                string Condition = Function.ShowConditons(GetCondition());
                lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;
                db.Get(Store.ManageShop, Parameter, out Error, out dt);
                ShowGridView(dt);
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
                strCondition += txtName.Text != "" ? ", ชื่อ: " + txtName.Text : "";
                strCondition += txtNameEn.Text != "" ? ", ชื่อ (อังกฤษ): " + txtNameEn.Text : "";
                strCondition += cbbType.Text != ":: กรุณาเลือก ::" ? ", ประเภท: " + cbbType.Text : "";
                strCondition += txtPhone.Text != "" ? ", เบอร์: " + txtPhone.Text : "";
                strCondition += txtMobile.Text != "" ? ", มือถือ: " + txtMobile.Text : "";
                strCondition += txtFax.Text != "" ? ", แฟกซ์: " + txtFax.Text : "";
                strCondition += txtEmail.Text != "" ? ", อีเมล: " + txtEmail.Text : "";
                strCondition += txtManager.Text != "" ? ", ผู้จัดการ: " + txtManager.Text : "";
                strCondition += txtOfficeHours.Text != "" ? ", เวลาทำการ: " + txtOfficeHours.Text : "";
                strCondition += txtLine.Text != "" ? ", ไลน์: " + txtLine.Text : "";
                strCondition += txtAddress.Text != "" ? ", ที่ตั้ง: " + txtAddress.Text : "";
                strCondition += cbbTambol.Text != ":: กรุณาเลือก ::" ? ", ตำบล: " + cbbTambol.Text : "";
                strCondition += cbbAmphoe.Text != ":: กรุณาเลือก ::" ? ", อำเภอ: " + cbbAmphoe.Text : "";
                strCondition += cbbProvince.Text != ":: กรุณาเลือก ::" ? ", จังหวัด: " + cbbProvince.Text : "";
                strCondition += txtPostcode.Text != "" ? ", รหัสไปรษณีย์: " + txtPostcode.Text : "";
                strCondition += txtRemark.Text != "" ? ", หมายเหตุ: " + txtRemark.Text : "";
                strCondition += cbbStatus.Text != ":: กรุณาเลือก ::" ? ", สถานะ: " + cbbStatus.Text : "";
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
                    {"@Code", ""},
                    {"@Name", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.SelectAbbr},
                    {"@NameEn", ""},
                    {"@Display", ""},
                    {"@Detail", ""},
                    {"@Address", ""},
                    {"@TambolId", "0"},
                    {"@AmphoeId", "0"},
                    {"@ProvinceId", "0"},
                    {"@Postcode", ""},
                    {"@Phone", ""},
                    {"@Mobile", ""},
                    {"@Fax", ""},
                    {"@Email", ""},
                    {"@Line", ""},
                    {"@Web", ""},
                    {"@Manager", ""},
                    {"@OfficeHours",  ""},
                    {"@LogoId", "0"},
                    {"@Remark",""},
                    {"@TypeId", "0"},
                };

                db.Get(Store.ManageShop, Parameter, out Error, out dt);
                ShowData(dt);
            }
        }

        private void ShowData(DataTable dt)
        {
            try
            {
                if (Function.GetRows(dt) > 0)
                {
                    txtId.Text = dt.Rows[0]["Id"].ToString();
                    txtCode.Text = dt.Rows[0]["Code"].ToString();

                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    txtNameEn.Text = dt.Rows[0]["NameEn"].ToString();
                    txtDisplay.Text = dt.Rows[0]["Display"].ToString();
                    txtAddress.Text = dt.Rows[0]["Address"].ToString();
                    
                    txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                    txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                    txtFax.Text = dt.Rows[0]["Fax"].ToString();
                    txtEmail.Text = dt.Rows[0]["Email"].ToString();
                    txtRemark.Text = dt.Rows[0]["Remark"].ToString();
                    txtWeb.Text = dt.Rows[0]["Web"].ToString();

                    txtManager.Text = dt.Rows[0]["Manager"].ToString();
                    txtOfficeHours.Text = dt.Rows[0]["OfficeHours"].ToString();
                    txtLine.Text = dt.Rows[0]["Line"].ToString();

                    cbbLogo.SelectedValue = dt.Rows[0]["LogoId"].ToString();

                    cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                    cbbProvince.SelectedValue = dt.Rows[0]["ProvinceId"].ToString();
                    cbbAmphoe.SelectedValue = dt.Rows[0]["AmphoeId"].ToString();
                    cbbTambol.SelectedValue = dt.Rows[0]["TambolId"].ToString();
                    cbbType.SelectedValue = dt.Rows[0]["TypeId"].ToString();

                    txtPostcode.Text = dt.Rows[0]["Postcode"].ToString();

                    GridView.Focus();
                }
            }
            catch
            {

            }
        }

        private void AddData(object sender, EventArgs e)
        {
            try
            {
                if (Function.GetComboId(cbbLogo) != "0" && Function.GetComboId(cbbStatus) != "0" && !string.IsNullOrEmpty(txtNameEn.Text) && !string.IsNullOrEmpty(txtDisplay.Text))
                {
                    if (!Function.IsDuplicates(Table.Shop, txtNameEn.Text, Function.GetComboId(cbbLogo), Detail: txtDisplay.Text))
                    {
                        txtCode.Text = Function.GetCodes(Table.ExpenseId, "1025", "Generated");

                        Parameter = new string[,]
                        {
                            {"@Id", ""},
                            {"@Code", txtCode.Text},
                            {"@Name", txtName.Text},
                            {"@Status", Function.GetComboId(cbbStatus)},
                            {"@User", UserId},
                            {"@IsActive", Function.GetComboId(cbbStatus) == "1000"? "1" : "0"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@NameEn", txtNameEn.Text},
                            {"@Display", txtDisplay.Text},
                            {"@Detail", ""},
                            {"@Address", txtAddress.Text},
                            {"@TambolId", Function.GetComboId(cbbTambol)},
                            {"@AmphoeId", Function.GetComboId(cbbAmphoe)},
                            {"@ProvinceId", Function.GetComboId(cbbProvince)},
                            {"@Postcode", txtPostcode.Text},
                            {"@Phone", txtPhone.Text},
                            {"@Mobile", txtMobile.Text},
                            {"@Fax", txtFax.Text},
                            {"@Email", txtEmail.Text},
                            {"@Line", txtLine.Text},
                            {"@Web", txtWeb.Text},
                            {"@Manager", txtManager.Text},
                            {"@OfficeHours", txtOfficeHours.Text},
                            {"@LogoId", Function.GetComboId(cbbLogo)},
                            {"@Remark", txtRemark.Text},
                            {"@TypeId", Function.GetComboId(cbbType)},
                        };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageShop, Parameter, txtCode.Text, Details: txtDisplay.Text))
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
                        {"@Code", txtCode.Text},
                        {"@Name", txtName.Text},
                        {"@Status", Function.GetComboId(cbbStatus)},
                        {"@User", UserId},
                        {"@IsActive", Function.GetComboId(cbbStatus) == "1000"? "1" : "0"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.UpdateAbbr},
                        {"@NameEn", txtNameEn.Text},
                        {"@Display", txtDisplay.Text},
                        {"@Detail", ""},
                        {"@Address", txtAddress.Text},
                        {"@TambolId", Function.GetComboId(cbbTambol)},
                        {"@AmphoeId", Function.GetComboId(cbbAmphoe)},
                        {"@ProvinceId", Function.GetComboId(cbbProvince)},
                        {"@Postcode", txtPostcode.Text},
                        {"@Phone", txtPhone.Text},
                        {"@Mobile", txtMobile.Text},
                        {"@Fax", txtFax.Text},
                        {"@Email", txtEmail.Text},
                        {"@Line", txtLine.Text},
                        {"@Web", txtWeb.Text},
                        {"@Manager", txtManager.Text},
                        {"@OfficeHours", txtOfficeHours.Text},
                        {"@LogoId", Function.GetComboId(cbbLogo)},
                        {"@Remark", ""},
                        {"@TypeId", Function.GetComboId(cbbType)},
                    };

                    if (Edit.Update(AppCode, AppName, UserId, Store.ManageShop, Parameter, txtCode.Text, Details: txtDisplay.Text))
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
                if (Delete.Drop(AppCode, AppName, UserId, 0, Table.Shop, txtCode, Details: txtDisplay.Text, true))
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

        private void ProvinceIndexChanged(object sender, EventArgs e)
        {
            if (!Start)
            {
                List.GetLists(cbbAmphoe, string.Format(DataList.AmphoeId, "ProvinceId", cbbProvince.SelectedValue.ToString()));
                cbbAmphoe.SelectedValue = "0";
                cbbAmphoe.Enabled = true;
                txtPostcode.Text = "";
            }
        }

        private void AmphoeIndexChanged(object sender, EventArgs e)
        {
            if (!Start)
            {
                List.GetLists(cbbTambol, string.Format(DataList.TambolId, "AmphoeId", cbbAmphoe.SelectedValue.ToString()));
                txtPostcode.Text = "";
                cbbTambol.SelectedValue = "0";
                cbbTambol.Enabled = true;
            }
        }

        private void TambolIndexChanged(object sender, EventArgs e)
        {
            if (!Start && Function.GetComboId(cbbTambol) != "0")
            {
                Parameter = new string[,]
                {
                        {"@Id", ""},
                        {"@User", ""},
                        {"@IsActive", ""},
                        {"@IsDelete", ""},
                        {"@Operation", Operation.SelectAbbr},
                        {"@TambolId", Function.GetComboId(cbbTambol)},
                        {"@TambolCode", ""},
                        {"@AmphoeId", "0"},
                        {"@ProvinceId", "0"},
                        {"@Zipcode", ""},
                        {"@Search", ""},
                };

                db.Get(Store.ManagePostcode, Parameter, out Error, out dt);

                if (Function.GetRows(dt) != 0)
                {
                    string Zipcode = dt.Rows[0]["Zipcode"].ToString();
                    txtPostcode.Text = Zipcode;
                }

                btnExit.Focus();
            }
        }

        private void txtMobile_Leave(object sender, EventArgs e)
        {
            if (txtMobile.Text != "")
            {
                txtMobile.Text = Function.ConvertPhoneNumber(txtMobile.Text);
            }
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (txtPhone.Text != "")
            {
                txtPhone.Text = Function.ConvertPhoneNumber(txtPhone.Text);
            }
        }
    }
}