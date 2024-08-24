using System;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Web.Services.Description;
using System.Windows.Forms;
using Microsoft.Reporting.Map.WebForms.BingMaps;
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
                






                        {"@Version", txtVersion.Text},
                        {"@Rates", txtPrice.Text},
                        {"@Service", txtService.Text},
                       // {"@Ft", txtFt.Text},
                        {"@Discount", txtDiscount.Text},
                        {"@Vat", txtVat.Text},
                        {"@DueDate", txtDay.Text},
                        {"@CompanyName", txtCompany.Text},
                        {"@CompanyPhone", txtPhone.Text},
                        {"@Report", txtReport.Text},
                      
                 




                //{"@Version", Search? txtVersion.Text : ""},
                //{"@Rates", Search? txtRate.Text : "0.00"},
                //{"@Service", Search? txtService.Text : "0.00"},
                //{"@Discount", Search? txtDiscount.Text : ""},
                //{"@Vat", Search? txtVat.Text : ""},
                //{"@RawText", Search? txtRawText.Text : ""},
                //{"@RawValue", Search? txtRawValue.Text : "0.00"},
                //{"@DueDate", Search? txtDueDate.Text : ""},
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
            //GridView.DataSource = null;
            //Rows = dt.Rows.Count;

            //if (Rows != 0)
            //{
            //    GridView.DataSource = null;
            //    DataTable dtGrid = new DataTable();
            //    dtGrid = dt.DefaultView.ToTable(true, "SNo", "UserName", "Version", "Rates", "RawValue", "Service", "DueDates", "Id");

            //    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
            //    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
            //    DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

            //    Function.showGridViewFormatFromStore(dtGrid, GridView,
            //          "ลำดับ", 50, true, mc, mc
            //        , "ผู้ใช้น้ำประปา", 100, true, ml, ml
            //        , "เวอร์ชั่น", 200, true, ml, ml
            //        , "อัตราค่าน้ำ", 50, true, mr, mr
            //        , "ค่าน้ำดิบ", 50, true, mr, mr
            //        , "ค่าบริการ", 50, true, mr, mr
            //        , "จำนวนที่ต้องชำระ", 50, true, mc, mc
            //        , "", 0, false, mr, mr
            //        );

            //    GridView.Focus();
            //}

            //txtCount.Text = Function.ShowNumberOfData(Function.GetRows(dt));
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";
                strCondition += txtCode.Text != "" ? ", รหัส : " + txtCode.Text : "";
                //strCondition += txtVersion.Text != "" ? ", " + lblVersion.Text + " " + txtVersion.Text : "";
                //strCondition += txtRate.Text != "" ? ", " + lblRate.Text + " " + txtRate.Text : "";
                //strCondition += txtRawValue.Text != "" ? ", " + lblTapWater.Text + " " + txtRawValue.Text : "";
                //strCondition += txtRawText.Text != "" ? ", เลขน้ำประปา : " + txtRawText.Text : "";
                //strCondition += txtDiscount.Text != "" ? ", " + lblDiscount.Text + " " + txtDiscount.Text : "";
                //strCondition += txtVat.Text != "" ? ", " + lblVat.Text + " " + txtVat.Text : "";
                //strCondition += txtService.Text != "" ? ", " + lblFee.Text + " " + txtService.Text : "";
                //strCondition += txtDueDate.Text != "" ? ", " + lblDueDate.Text + " " + txtDueDate.Text : "";
                strCondition += cbbStatus.Text != ":: กรุณาเลือก ::" ? ", " + label41.Text + " " + cbbStatus.Text : "";

                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return "";
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.GridView.Rows[e.RowIndex];
                    DataTable dt = new DataTable();

                    Parameter = new string[,]
                    {
                        {"@Id", row.Cells["Id"].Value.ToString()},
                        {"@Code", ""},
                        {"@Version", ""},
                        {"@Rates", ""},
                        {"@Service", ""},
                        {"@Ft", ""},
                        {"@Discount", ""},
                        {"@Vat", ""},
                        {"@DueDate", ""},
                        {"@CompanyName", ""},
                        {"@CompanyPhone", ""},
                        {"@Report", ""},
                        {"@Status", "0"},
                        {"@User", ""},
                    };

                    //db.Get("Store.SelectVersionElectricity", Parameter, out strErr, out dt);

                    txtCode.Text = dt.Rows[0]["Code"].ToString();

                    txtVersion.Text = dt.Rows[0]["Version"].ToString();
                    txtReport.Text = dt.Rows[0]["Report"].ToString();
                    txtDay.Text = dt.Rows[0]["DueDate"].ToString();
                    txtVat.Text = dt.Rows[0]["Vat"].ToString();
                    cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();

                    txtPrice.Text = dt.Rows[0]["Rates"].ToString();
                    txtCompany.Text = dt.Rows[0]["CompanyName"].ToString();
                    txtPhone.Text = dt.Rows[0]["CompanyPhone"].ToString();
                    txtService.Text = dt.Rows[0]["Service"].ToString();

                    txtFt.Text = dt.Rows[0]["Ft"].ToString();
                    txtDiscount.Text = dt.Rows[0]["Discount"].ToString();

                    string strBarcode = dt.Rows[0]["Code"].ToString();
                    pbQrcode.Image = Barcode.QRCode(strBarcode, Color.Black, Color.White, "Q", 3, false);
                }
            }
            catch (Exception ex)
            {
                //Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        public void getDataGrid(DataTable dt)
        {
            try
            {
                //if (Fn.GetRows(dt) == 0)
                //{
                //    GridView.DataSource = null;
                //}
                //else
                //{
                //    DataTable dtGrid = new DataTable();
                //    dtGrid = dt.DefaultView.ToTable(true, "Version", "Rate", "Service", "Ft", "CompanyName", "Id");

                //    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                //    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                //    Fn.showGridViewFormatFromStore(dtGrid, GridView,
                //          "ลำดับ", 20, true, mc, mc
                //        , "Version", 80, true, ml, ml
                //        , "ค่าไฟฟ้าต่อหน่วย", 100, true, ml, ml
                //        , "ค่าบริการรายเดือน", 100, true, ml, ml
                //        , "Ft", 80, true, ml, ml
                //        , "บริษัทจดบันทึกค่าไฟฟ้า", 200, true, mc, mc
                //        , "", 0, false, mc, mc
                //        );

                //    lblResult.Text = Fn.ShowResult(dt.Rows.Count);
                //}
            }
            catch (Exception ex)
            {
                //Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
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
            if (!string.IsNullOrEmpty(txtVersion.Text))
            {
                //if (!Fn.IsDuplicates("MST_VersionElectricity", txtVersion.Text, Detail: "Version " + txtVersion.Text))
                //{
                //    txtCode.Text = Fn.GetCodes("139", "", "Generated");

                //    string[,] Parameter = new string[,]
                //    {
                //        {"@Id", ""},
                //        {"@Code", txtCode.Text},
                //        {"@Version", txtVersion.Text},
                //        {"@Rates", txtPrice.Text},
                //        {"@Service", txtService.Text},
                //        {"@Ft", txtFt.Text},
                //        {"@Discount", txtDiscount.Text},
                //        {"@Vat", txtVat.Text},
                //        {"@DueDate", txtDay.Text},
                //        {"@CompanyName", txtCompany.Text},
                //        {"@CompanyPhone", txtPhone.Text},
                //        {"@Report", txtReport.Text},
                //        {"@Status", Fn.getComboboxId(cbbStatus)},
                //        {"@User", strUserId},
                //    };

                //    if (Insert.Add(strAppCode, strAppName, strUserId, "Store.InsertVersionElectricity", Parameter, txtCode.Text, "Version " + txtVersion.Text))
                //    {
                //        Clear();
                //    }
                //}
            }
            else
            {
                Message.ShowRequestData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                //if (Delete.Drop(strAppCode, strAppName, strUserId, 0, Tb.VersionElectricity, txtCode, "Version " + txtVersion.Text))
                //{
                //    Clear();
                //}
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", txtCode.Text},
                    {"@Version", txtVersion.Text},
                    {"@Rates", txtPrice.Text},
                    {"@Service", txtService.Text},
                    {"@Ft", txtFt.Text},
                    {"@Discount", txtDiscount.Text},
                    {"@Vat", txtVat.Text},
                    {"@DueDate", txtDay.Text},
                    {"@CompanyName", txtCompany.Text},
                    {"@CompanyPhone", txtPhone.Text},
                    {"@Report", txtReport.Text},
                    //{"@Status", Fn.getComboboxId(cbbStatus)},
                    //{"@User", strUserId},
                };

                //if (Edit.Update(strAppCode, strAppName, strUserId, "Store.UpdateVersionElectricity", Parameter, txtCode.Text, "Version " + txtVersion.Text))
                //{
                //    Clear();
                //}
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", txtCode.Text},
                    {"@Version", txtVersion.Text},
                    {"@Rates", txtPrice.Text},
                    {"@Service", txtService.Text},
                    {"@Ft", txtFt.Text},
                    {"@Discount", txtDiscount.Text},
                    {"@Vat", txtVat.Text},
                    {"@DueDate", txtDay.Text},
                    {"@CompanyName", txtCompany.Text},
                    {"@CompanyPhone", txtPhone.Text},
                    {"@Report", txtReport.Text},
                    //{"@Status", Fn.getComboboxId(cbbStatus)},
                    //{"@User", strUserId},
                };

                //db.Get("Store.SelectVersionElectricity", Parameter, out strErr, out dt);
                //getDataGrid(dt);

                //lblCondition.Text = Fn.ShowConditons(GetCondition());
            }
            catch (Exception ex)
            {
                lblCondition.Text = "";
               // Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void FrmKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //string keyCode = Fn.keyPress(sender, e);

                //if (keyCode == "Ctrl+S")
                //{
                //    btnAdd_Click(sender, e);
                //}

                //if (keyCode == "Ctrl+E")
                //{
                //    btnEdit_Click(sender, e);
                //}

                //if (keyCode == "Ctrl+D")
                //{
                //    btnDelete_Click(sender, e);
                //}

                //if (keyCode == "Ctrl+X")
                //{
                //    btnExit_Click(sender, e);
                //}

                //if (keyCode == "Ctrl+F")
                //{
                //    btnSearch_Click(sender, e);
                //}

                //if (keyCode == "Alt+C")
                //{
                //    btnClear_Click(sender, e);
                //}
            }
            catch (Exception ex)
            {
               // Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }
    }
}