using System;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web.Services.Description;
using System.Windows.Forms;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.Reporting.WinForms;
using Microsoft.VisualBasic;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;
using Telerik.WinForms.UI.Barcode;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using static QRCoder.PayloadGenerator;

namespace SANSANG
{
    public partial class FrmElectricityRates : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "MEAMA00";
        public string AppName = "FrmElectricityRates";
        public string Error = "";
        public string Laguage;

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        private clsDate Date = new clsDate();
        private clsDelete Delete = new clsDelete();
        private clsEdit Edit = new clsEdit();
        private clsImage Images = new clsImage();
        private clsInsert Insert = new clsInsert();
        private clsFunction Function = new clsFunction();
        private clsSetting Setting = new clsSetting();
        private clsMessage Message = new clsMessage();
        private clsBarcode Barcode = new clsBarcode();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsHelpper Helper = new clsHelpper();
        private DataListConstant DataList = new DataListConstant();
        private clsLog Log = new clsLog();
        private clsEvent Event = new clsEvent();
        private TableConstant Table = new TableConstant();
        private StoreConstant Store = new StoreConstant();
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(20);
        private OperationConstant Operation = new OperationConstant();
        private Timer Timer = new Timer();
        public string[,] Parameter = new string[,] { };
        public bool IsStart = true;
        public string Account = "0";
        public int Rows = 0;

        public FrmElectricityRates(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
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
            Timer.Interval = (2000);
            Timer.Tick += new EventHandler(LoadList);
            Timer.Start();
        }

        private void LoadList(object sender, EventArgs e)
        {
            Laguage = clsSetting.ReadLanguageSetting();

            IsStart = true;

            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "6"));
            List.GetList(cbbAccount, DataList.ElectricityAccount);
            gbForm.Enabled = true;
            Clear();
            Timer.Stop();
        }

        public void Clear()
        {
            Function.ClearAll(gbForm);
            pbQrcode.Image = null;
            Account = Function.GetComboId(cbbAccount);
            IsStart = false;
            Search(false);
        }

        public void Search(bool Search)
        {
            Parameter = new string[,]
            {
                {"@Id", Search? txtId.Text : ""},
                {"@Code", Search? txtCode.Text : ""},
                {"@Status", Search? Function.GetComboId(cbbStatus) : "0"},
                {"@User", ""},
                {"@IsActive", "1"},
                {"@IsDelete", "0"},
                {"@Operation", Operation.SelectAbbr},
                {"@AccountId", Search? Function.GetComboId(cbbAccount) : "0"},
                {"@Version", Search? txtVersion.Text : ""},
                {"@Rates", Search? txtRates.Text : ""},
                {"@Service", Search? txtService.Text : ""},
                {"@Discount", Search? txtDiscount.Text : ""},
                {"@Vat", Search? txtVat.Text : ""},
                {"@Ft", Search? txtFt.Text : ""},
                {"@Report", Search? txtReport.Text : ""},
                {"@DueDate", Search? txtDueDate.Text : ""},
                {"@Company", Search? txtCompany.Text : ""},
                {"@Phone", Search? txtPhone.Text : ""},
            };

            string Condition = Function.ShowConditons(GetCondition());
            lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;

            db.Get(Store.ManageElectricityRates, Parameter, out Error, out dt);

            if (string.IsNullOrEmpty(Error))
            {
                ShowGridView(dt);
            }
        }

        public void ShowGridView(DataTable dt)
        {
            GridView.DataSource = null;
            Rows = dt.Rows.Count;

            if (Rows != 0)
            {
                GridView.DataSource = null;
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "SNo", "UserName", "Version", "Rates", "Ft", "Service", "DueDates", "Id");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
                DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                Function.showGridViewFormatFromStore(dtGrid, GridView,
                      "ลำดับ", 50, true, mc, mc
                    , "ผู้ใช้น้ำประปา", 100, true, ml, ml
                    , "เวอร์ชั่น", 200, true, ml, ml
                    , "อัตราค่าน้ำ", 50, true, mr, mr
                    , "ค่าน้ำดิบ", 50, true, mr, mr
                    , "ค่าบริการ", 50, true, mr, mr
                    , "จำนวนที่ต้องชำระ", 50, true, mc, mc
                    , "", 0, false, mr, mr
                );

                GridView.Focus();
            }

            txtCount.Text = Function.ShowNumberOfData(Function.GetRows(dt));
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";
                strCondition += txtCode.Text != "" ? ", รหัส : " + txtCode.Text : "";
                strCondition += cbbAccount.Text != ":: กรุณาเลือก ::" ? ", " + lblName.Text + " " + cbbAccount.Text : "";
                strCondition += txtVersion.Text != "" ? ", " + lblVersion.Text + " " + txtVersion.Text : "";
                strCondition += txtReport.Text != "" ? ", " + lblReport.Text + " " + txtReport.Text : "";
                strCondition += txtRates.Text != "" ? ", " + lblRates.Text + " " + txtRates.Text : "";
                strCondition += txtService.Text != "" ? ", " + lblService.Text + " " + txtService.Text : "";
                strCondition += txtDiscount.Text != "" ? ", " + lblDiscount.Text + " " + txtDiscount.Text : "";
                strCondition += txtVat.Text != "" ? ", " + lblVat.Text + " " + txtVat.Text : "";
                strCondition += txtFt.Text != "" ? ", " + lblFt.Text + " " + txtFt.Text : "";
                strCondition += txtReport.Text != "" ? ", " + lblResult.Text + " " + txtReport.Text : "";
                strCondition += txtDueDate.Text != "" ? ", " + lblDuedate.Text + " " + txtDueDate.Text : "";
                strCondition += txtCompany.Text != "" ? ", " + lblCompany.Text + " " + txtCompany.Text : "";
                strCondition += txtPhone.Text != "" ? ", " + lblPhone.Text + " " + txtPhone.Text : "";
                strCondition += cbbStatus.Text != ":: กรุณาเลือก ::" ? ", " + lblStatus.Text + " " + label41.Text + " " + cbbStatus.Text : "";
                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return "";
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

        private void FrmKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string keyCode = Function.keyPress(sender, e);

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
                if (keyCode == "Ctrl+F")
                {
                    Search(sender, e);
                }
                if (keyCode == "Alt+C")
                {
                    Clear(sender, e);
                }
                if (keyCode == "Enter")
                {
                    Search(true);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void cbbAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Accounts = Function.selectedValue(cbbAccount);

            if (!IsStart)
            {
                Parameter = new string[,]
                 {
                    {"@Id", Accounts},
                    {"@Code", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.SelectAbbr},
                    {"@AccountNo", ""},
                    {"@AddressId", "0"},
                    {"@OfficeId", "0"},
                    {"@Branch", ""},
                    {"@Type", ""},   
                    {"@Mru", ""},
                    {"@Multiplier", ""},
                    {"@Installation", ""},
                 };

                db.Get(Store.ManageElectricityAccount, Parameter, out Error, out dt);

                if (string.IsNullOrEmpty(Error) && dt.Rows.Count > 0)
                {
                    txtBranch.Text = dt.Rows[0]["BranchName"].ToString();
                    txtType.Text = dt.Rows[0]["Type"].ToString();
                    txtPremise.Text = dt.Rows[0]["Address1"].ToString();
                    txtAccountNo.Text = dt.Rows[0]["AccountNo"].ToString();
                    txtInstallation.Text = dt.Rows[0]["Installation"].ToString();
                    txtMru.Text = dt.Rows[0]["Mru"].ToString();
                }
                else
                {
                    txtBranch.Text = "";
                    txtType.Text = "";
                    txtPremise.Text = "";
                    txtAccountNo.Text = "";
                    txtInstallation.Text = "";
                    txtMru.Text = "";
                }
            }
            if (Accounts == "0")
            {
                txtBranch.Text = "";
                txtType.Text = "";
                txtPremise.Text = "";
                txtAccountNo.Text = "";
                txtInstallation.Text = "";
                txtMru.Text = "";
            }

            GridView.Focus();
        }

        private void GridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow RowIndex = GridView.Rows[e.RowIndex];
                Parameter = new string[,]
                {
                    {"@Id", RowIndex.Cells["Id"].Value.ToString()},
                    {"@Code", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.SelectAbbr},
                    {"@AccountId", "0"},
                    {"@Version", ""},
                    {"@Rates", ""},
                    {"@Service", ""},
                    {"@Discount", ""},
                    {"@Vat", ""},
                    {"@Ft", ""},
                    {"@Report", ""},
                    {"@DueDate", ""},
                    {"@Company", ""},
                    {"@Phone", ""},
                };

                db.Get(Store.ManageElectricityRates, Parameter, out Error, out dt);
                ShowData(dt);
            }
        }

        public void ShowData(DataTable Data)
        {
            try
            {
                cbbAccount.SelectedValue = Data.Rows[0]["AccountId"].ToString();

                txtId.Text = Data.Rows[0]["Id"].ToString();
                txtCode.Text = Data.Rows[0]["Code"].ToString();

                txtVersion.Text = Data.Rows[0]["Version"].ToString();
                txtReport.Text = Data.Rows[0]["Report"].ToString();
                txtVat.Text = Data.Rows[0]["Vat"].ToString();

                txtRates.Text = Data.Rows[0]["Rates"].ToString();
                txtDueDate.Text = Data.Rows[0]["DueDate"].ToString();

                txtCompany.Text = Data.Rows[0]["Company"].ToString();
                txtPhone.Text = Data.Rows[0]["Phone"].ToString();

                txtService.Text = Data.Rows[0]["Service"].ToString();
                txtFt.Text = Data.Rows[0]["Ft"].ToString();
                txtDiscount.Text = Data.Rows[0]["Discount"].ToString();
                
                cbbStatus.SelectedValue = Data.Rows[0]["Status"].ToString();

                string strBarcode = Data.Rows[0]["Code"].ToString();
                pbQrcode.Image = Barcode.QRCode(strBarcode, Color.Black, Color.White, "Q", 3, false);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void Clear(object sender, EventArgs e)
        {
            Clear();
        }

        private void Search(object sender, EventArgs e)
        {
            Search(true);
        }

        private void AddData(object sender, EventArgs e)
        {
            try
            {
                if (Function.GetComboId(cbbAccount) != "0" && !string.IsNullOrEmpty(txtVersion.Text) && !string.IsNullOrEmpty(txtRates.Text))
                {
                    if (!Function.IsDuplicates(Table.ElectricityRates, Function.GetComboId(cbbAccount), txtVersion.Text, txtRates.Text,
                        Detail: txtPremise.Text + Environment.NewLine + txtVersion.Text + " (" + txtRates.Text + ")"))
                    {
                        txtCode.Text = Function.GetCodes(Table.ElectricityRatesId, "", "Generated");

                        Parameter = new string[,]
                        {
                            {"@Id", txtId.Text},
                            {"@Code", txtCode.Text},
                            {"@Status", Function.GetComboId(cbbStatus)},
                            {"@User", UserId},
                            {"@IsActive", "1"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@AccountId", Function.GetComboId(cbbAccount)},
                            {"@Version", txtVersion.Text},
                            {"@Rates", txtRates.Text},
                            {"@Service", txtService.Text},
                            {"@Discount", txtDiscount.Text},
                            {"@Vat", txtVat.Text},
                            {"@Ft", txtFt.Text},
                            {"@Report", txtReport.Text},
                            {"@DueDate", txtDueDate.Text},
                            {"@Company", txtCompany.Text},
                            {"@Phone", txtPhone.Text},
                        };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageElectricityRates, Parameter, txtCode.Text, Details: GetDetails()))
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
                        {"@Status", Function.GetComboId(cbbStatus)},
                        {"@User", UserId},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.InsertAbbr},
                        {"@AccountId", Function.GetComboId(cbbAccount)},
                        {"@Version", txtVersion.Text},
                        {"@Rates", txtRates.Text},
                        {"@Service", txtService.Text},
                        {"@Discount", txtDiscount.Text},
                        {"@Vat", txtVat.Text},
                        {"@Ft", txtFt.Text},
                        {"@Report", txtReport.Text},
                        {"@DueDate", txtDueDate.Text},
                        {"@Company", txtCompany.Text},
                        {"@Phone", txtPhone.Text},
                    };

                    if (Edit.Update(AppCode, AppName, UserId, Store.ManageElectricityRates, Parameter, txtCode.Text, Details: GetDetails()))
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
                if (Delete.Drop(AppCode, AppName, UserId, 0, Table.ElectricityRates, txtCode, Details: GetDetails(), true))
                {
                    Clear();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public string GetDetails()
        {
            return txtVersion.Text + " (" + txtRates.Text + ")";
        }
    }
}