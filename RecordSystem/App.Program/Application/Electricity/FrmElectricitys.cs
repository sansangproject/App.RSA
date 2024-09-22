using System;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Web;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;
using SANSANG.Utilites.App.Global;
using SANSANG.Utilites.App.Model;
using Telerik.WinControls.VirtualKeyboard;
using static QRCoder.PayloadGenerator;

namespace SANSANG
{
    public partial class FrmElectricitys : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "SAVEA00";
        public string AppName = "FrmElectricitys";
        public string Error = "";
        public string Laguage;

        private dbConnection db = new dbConnection();

        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private DataListConstant DataList = new DataListConstant();

        private clsDate Date = new clsDate();
        private clsDelete Delete = new clsDelete();
        private clsEdit Edit = new clsEdit();
        private clsImage Images = new clsImage();
        private clsInsert Insert = new clsInsert();
        private clsFunction Function = new clsFunction();
        private clsSetting Setting = new clsSetting();
        private clsMessage Message = new clsMessage();
        private clsBarcode Barcode = new clsBarcode();
        private clsDataList List = new clsDataList();
        private clsHelpper Helper = new clsHelpper();
        private clsLog Log = new clsLog();
        private clsEvent Event = new clsEvent();

        private TableConstant Table = new TableConstant();
        private StoreConstant Store = new StoreConstant();
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(20);
        private OperationConstant Operation = new OperationConstant();
        private CharacterConstant CharType = new CharacterConstant();
        private CultureConstant Cul = new CultureConstant();
        private Timer Timer = new Timer();

        public string[,] Parameter = new string[,] { };
        public bool IsStart = true;
        public string Account = "0";
        public int Rows = 0;
        public int CountRows = 0;
        public int Numbers = 0;
        public bool NewData = true;
        public string strBarcode = "";
        public string strInvoice = "";
        public string Rates = "0";
        public int AccountId = 0;
        public int CountEnter = 0;
        public int NumberOfPayment = 10;
        public int InvoiceDay = -7;
        public string Ft = "0.3972";
        public double First = 3.2484;
        public double Next = 4.2218;
        public double Over = 4.4217;

        public string qrHeader;
        public string qrLine2;
        public string qrLine3;
        public string qrLine4;
        public string strQR;

        private string PlaceholderText = "Scan Here...";
        private bool IsPlaceholderVisible;

        public FrmElectricitys(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
        {
            InitializeComponent();

            UserId = UserIdLogin;
            UserName = UserNameLogin;
            UserSurname = UserSurNameLogin;
            UserType = UserTypeLogin;

            SetPlaceholderVisibility();
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
            IsStart = true;

            List.GetLists(cbbPayment, DataList.MoneyId);
            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "1"));

            List.GetList(cbbAccount, DataList.ElectricityAccount);
            List.GetLists(cbbVersion, string.Format(DataList.ElectricityRatesId, "0"));

            List.GetMonthList(cbbMonth);
            List.GetYearList(cbbYear);

            Clear();
            Timer.Stop();
        }

        public void ShowGridView(DataTable dt)
        {
            try
            {
                GridView.DataSource = null;
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "SNo", "InvoiceNumber", "Dates", "Unit", "Payment", "DueDate", "StatusName", "Id");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
                DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                Function.showGridViewFormatFromStore(dtGrid, GridView
                        , "ลำดับ", 30, true, mc, mc
                        , "เลขที่ใบแจ้ง", 100, true, ml, ml
                        , "ประจำเดือน", 100, true, mc, ml
                        , "จำนวนหน่วย", 50, true, mr, mr
                        , "จำนวนเงิน (บาท)", 100, true, mr, mr
                        , "กำหนดชำระ", 100, true, mc, mr
                         , "สถานะ", 100, true, mc, mc
                        , "", 0, false, mc, mc
                    );

                GridView.Focus();
                txtCount.Text = Function.ShowNumberOfData(Function.GetRows(dt));
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            Close();
        }

        private void Search(object sender, EventArgs e)
        {
            SearchData(true, out Numbers, "");
        }

        public void SearchData(bool Search, out int Number, string InvoiceNo = "")
        {
            try
            {
                if (string.IsNullOrEmpty(InvoiceNo))
                {
                    Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", Search? txtCode.Text : ""},
                        {"@Status", Search? Function.getComboBoxValue(cbbStatus) : "0"},
                        {"@User", ""},
                        {"@IsActive", ""},
                        {"@IsDelete", ""},
                        {"@Operation", Operation.SelectAbbr},
                        {"@AccountId", Search? Function.getComboBoxValue(cbbAccount) : "0"},
                        {"@VersionId", Search? Function.getComboBoxValue(cbbVersion) : "0"},
                        {"@InvoiceNumber", Search? txtInvoiceNumber.Text : ""},     
                        {"@Date", ""},
                        {"@Time", ""},
                        {"@Month", Search? Function.getComboBoxValue(cbbMonth) : "0"},
                        {"@Year", Search? Function.getComboBoxValue(cbbYear) : "0"},
                        {"@MeterNow", Search? txtNumberNow.Text : ""},
                        {"@MeterBefore", Search? txtNumberBefor.Text : ""},
                        {"@Unit", Search? txtUnit.Text : ""},
                        {"@Raw", Search? Function.SplitString(txtRaw.Text, ",", "") : ""},
                        {"@Ft", Search? Function.SplitString(txtMoneyFt.Text, ",", "") : ""},
                        {"@Discount", Search? Function.SplitString(txtDiscount.Text, ",", "") : ""},
                        {"@Detail", Search? txtDiscountDetail.Text : ""},
                        {"@Summary", Search? Function.SplitString(txtMoney.Text, ",", "") : ""},
                        {"@Vat", Search? Function.SplitString(txtMoneyVat.Text, ",", "") : ""},
                        {"@Total", Search? Function.SplitString(txtMoneyAll.Text, ",", "") : ""},
                        {"@Payment", Search? Function.SplitString(txtMoneyPay.Text, ",", "") : ""},
                        {"@MoneyId", Search?  Function.getComboBoxValue(cbbPayment)  : "0"},
                        {"@EndDate", ""},
                        {"@PayDate", ""},
                        {"@Barcode", ""},
                        {"@Remark", Search? txtRemark.Text : ""},
                    };
               }
                else
                {
                    Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", ""},
                        {"@Status", "0"},
                        {"@User", ""},
                        {"@IsActive", ""},
                        {"@IsDelete", ""},
                        {"@Operation", Operation.SelectAbbr},
                        {"@AccountId", "0"},
                        {"@VersionId", "0"},
                        {"@InvoiceNumber", InvoiceNo},
                        {"@Date", ""},
                        {"@Time", ""},
                        {"@Month", ""},
                        {"@Year", ""},
                        {"@MeterNow", ""},
                        {"@MeterBefore", ""},
                        {"@Unit", ""},
                        {"@Raw", ""},
                        {"@Ft", ""},
                        {"@Discount", ""},
                        {"@Detail", ""},
                        {"@Summary", ""},
                        {"@Vat", ""},
                        {"@Total", ""},
                        {"@Payment", ""},
                        {"@MoneyId", "0"},
                        {"@EndDate", ""},
                        {"@PayDate", ""},
                        {"@Barcode", ""},
                        {"@Remark", ""},
                    };
                }

                db.Get(Store.ManageElectricitys, Parameter, out Error, out dt);
                string Condition = Function.ShowConditons(GetCondition());
                lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;

                if (Search && dt.Rows.Count > 0)
                {
                    if (dt.Rows.Count == 1)
                    {
                        ShowGridView(dt);
                        ShowData(dt);
                        GetBill(Operation.Overdue);
                        GridView.Focus();
                    }
                }
                else
                {
                    if (dt.Rows.Count == 0)
                    {
                        ShowGridView(dt);
                        GetBill(Operation.Overdue);
                        GetBill(Operation.Before);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Error) && dt.Rows.Count > 0)
                        {
                            CountRows = dt.Rows.Count;
                            ShowGridView(dt);

                            if (dt.Rows.Count == 1)
                            {
                                ShowData(dt);
                                GetBill(Operation.Overdue);
                                GridView.Focus();
                            }
                        }

                        if (!string.IsNullOrEmpty(txtInvoiceNumber.Text) && dt.Rows.Count != 1)
                        {
                            CountRows = dt.Rows.Count;
                            GetBill(Operation.Before);
                            GetBill(Operation.Overdue);
                            GridView.Focus();
                        }
                    }
                }

                Number = CountRows;

                txtScan.Text = string.Empty;
                SetPlaceholderVisibility();
            }
            catch (Exception ex)
            {
                Number = 0;
                GridView.Focus();
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void AddData(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInvoiceNumber.Text) && Function.getComboBoxValue(cbbStatus) != "0" && !string.IsNullOrEmpty(txtMoneyPay.Text) && dtTime.Value.ToString("HH:mm:ss") != "00:00:00")
                {
                    if (!Function.IsDuplicates(Table.Electricitys, Function.getComboBoxValue(cbbMonth), Function.getComboBoxValue(cbbYear), Detail: GetDetails()))
                    {
                        txtCode.Text = Function.GetCodes(Table.ElectricityId, "", "Generated");
                        pbQrcode.Image = Barcode.QRCode(txtCode.Text, Color.Black, Color.White, "Q", 3, false);
                        strQR = qrHeader + "\r\n" + qrLine2 + "\r\n" + qrLine3 + "\r\n" + qrLine4 + "\r\n";

                        Parameter = new string[,]
                        {
                            {"@Id", ""},
                            {"@Code", txtCode.Text},
                            {"@Status", Function.getComboBoxValue(cbbStatus)},
                            {"@User", UserId},
                            {"@IsActive", "1"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@AccountId", Function.getComboBoxValue(cbbAccount)},
                            {"@VersionId", Function.getComboBoxValue(cbbVersion)},
                            {"@InvoiceNumber", txtInvoiceNumber.Text},
                            {"@Date", Date.GetDate(dtp : dtDate, Format : 4)},
                            {"@Time", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                            {"@Month", Function.getComboBoxValue(cbbMonth)},
                            {"@Year", Function.getComboBoxValue(cbbYear)},
                            {"@MeterNow", txtNumberNow.Text},
                            {"@MeterBefore", txtNumberBefor.Text},
                            {"@Unit", txtUnit.Text},
                            {"@Raw", Function.MoveNumberStringComma(txtRaw.Text)},
                            {"@Ft", Function.MoveNumberStringComma(txtMoneyFt.Text)},
                            {"@Discount", Function.MoveNumberStringComma(txtDiscount.Text)},
                            {"@Detail", txtDiscountDetail.Text},
                            {"@Summary", Function.MoveNumberStringComma(txtMoney.Text)},
                            {"@Vat", Function.MoveNumberStringComma(txtMoneyVat.Text)},
                            {"@Total", Function.MoveNumberStringComma(txtMoneyAll.Text)},
                            {"@Payment", Function.MoveNumberStringComma(txtMoneyPay.Text)},
                            {"@MoneyId", Function.getComboBoxValue(cbbPayment)},
                            {"@EndDate", Date.GetDate(dtp : dtPayEnd, Format : 4)},
                            {"@PayDate", Date.GetDate(dtp : dtPay, Format : 4)},
                            {"@Barcode", ""},
                            {"@Remark", txtRemark.Text},
                        };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageElectricitys, Parameter, txtCode.Text, Details: GetDetails()))
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

        private void GridViewClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = GridView.Rows[e.RowIndex];

                    Parameter = new string[,]
                    {
                        {"@Id", row.Cells["Id"].Value.ToString()},
                        {"@Code", ""},
                        {"@Status", "0"},
                        {"@User", ""},
                        {"@IsActive", ""},
                        {"@IsDelete", ""},
                        {"@Operation", Operation.SelectAbbr},
                        {"@AccountId", "0"},
                        {"@VersionId", "0"},
                        {"@InvoiceNumber", ""},
                        {"@Date", ""},
                        {"@Time", ""},
                        {"@Month", ""},
                        {"@Year", ""},
                        {"@MeterNow", ""},
                        {"@MeterBefore", ""},
                        {"@Unit", ""},
                        {"@Raw", ""},
                        {"@Ft", ""},
                        {"@Discount", ""},
                        {"@Detail", ""},
                        {"@Summary", ""},
                        {"@Vat", ""},
                        {"@Total", ""},
                        {"@Payment", ""},
                        {"@MoneyId", "0"},
                        {"@EndDate", ""},
                        {"@PayDate", ""},
                        {"@Barcode", ""},
                        {"@Remark", ""},
                    };

                    db.Get(Store.ManageElectricitys, Parameter, out Error, out dt);
                    ShowData(dt);
                    GetBill(Operation.Overdue);
                }

                SetPlaceholderVisibility();
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
                        {"@Status", Function.getComboBoxValue(cbbStatus)},
                        {"@User", UserId},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.UpdateAbbr},
                        {"@AccountId", Function.getComboBoxValue(cbbAccount)},
                        {"@VersionId", Function.getComboBoxValue(cbbVersion)},
                        {"@InvoiceNumber", txtInvoiceNumber.Text},
                        {"@Date", Date.GetDate(dtp : dtDate, Format : 4)},
                        {"@Time", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                        {"@Month", Function.getComboBoxValue(cbbMonth)},
                        {"@Year", Function.getComboBoxValue(cbbYear)},
                        {"@MeterNow", txtNumberNow.Text},
                        {"@MeterBefore", txtNumberBefor.Text},
                        {"@Unit", txtUnit.Text},
                        {"@Raw", Function.MoveNumberStringComma(txtRaw.Text)},
                        {"@Ft", Function.MoveNumberStringComma(txtMoneyFt.Text)},
                        {"@Discount", Function.MoveNumberStringComma(txtDiscount.Text)},
                        {"@Detail", txtDiscountDetail.Text},
                        {"@Summary", Function.MoveNumberStringComma(txtMoney.Text)},
                        {"@Vat", Function.MoveNumberStringComma(txtMoneyVat.Text)},
                        {"@Total", Function.MoveNumberStringComma(txtMoneyAll.Text)},
                        {"@Payment", Function.MoveNumberStringComma(txtMoneyPay.Text)},
                        {"@MoneyId", Function.getComboBoxValue(cbbPayment)},
                        {"@EndDate", Date.GetDate(dtp : dtPayEnd, Format : 4)},
                        {"@PayDate", Date.GetDate(dtp : dtPay, Format : 4)},
                        {"@Barcode", ""},
                        {"@Remark", txtRemark.Text},
                    };

                    if (Edit.Update(AppCode, AppName, UserId, Store.ManageElectricitys, Parameter, txtCode.Text, Details: GetDetails()))
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
                if (!string.IsNullOrEmpty(txtCode.Text))
                {
                    if (Delete.Drop(AppCode, AppName, UserId, 0, Table.Electricitys, txtCode, Details: GetDetails(), true))
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

        private void Clear(object sender, EventArgs e)
        {
            Clear();
        }

        public void GetDataMaster(string Id)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id ", Id},
                    {"@Code", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", ""},
                    {"@IsDelete", ""},
                    {"@Operation", Operation.SelectAbbr},
                    {"@AccountId", ""},
                    {"@Version", ""},
                    {"@Rates", ""},
                    {"@FirstRates", ""},
                    {"@NextRates", ""},
                    {"@OverRates", ""},
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

                if (string.IsNullOrEmpty(Error) && dt.Rows.Count > 0)
                {
                    txtVat.Text = dt.Rows[0]["Vat"].ToString();
                    NumberOfPayment = Convert.ToInt32(dt.Rows[0]["DueDate"].ToString());
                    txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
                    InvoiceDay = Convert.ToInt32(dt.Rows[0]["DueDate"].ToString()) * -1;
                    txtFee.Text = dt.Rows[0]["Service"].ToString();
                    txtFt.Text = dt.Rows[0]["Ft"].ToString();
                    NumberOfPayment = Convert.ToInt32(dt.Rows[0]["DueDate"].ToString());

                    txtPerUnit.Text = dt.Rows[0]["Rates"].ToString();

                    First = Convert.ToDouble(dt.Rows[0]["FirstRates"].ToString());
                    Next = Convert.ToDouble(dt.Rows[0]["NextRates"].ToString());
                    Over = Convert.ToDouble(dt.Rows[0]["OverRates"].ToString());
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public void CalElectValue()
        {
            try
            {
                txtMoneyPay.Text = string.Format("{0:#,##0.00}", Math.Round(Convert.ToDouble(txtMoneyAll.Text), 2) + Math.Round(Convert.ToDouble(txtMoneyOverdue.Text), 2));
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public void ShowData(DataTable dt)
        {
            try
            {
                cbbAccount.SelectedValue = dt.Rows[0]["AccountId"].ToString();
                cbbVersion.SelectedValue = dt.Rows[0]["VersionId"].ToString();

                txtInvoiceNumber.Text = dt.Rows[0]["InvoiceNumber"].ToString();

                dtDate.Text = dt.Rows[0]["Date"].ToString();
                dtTime.Text = dt.Rows[0]["Time"].ToString();
                cbbMonth.SelectedValue = dt.Rows[0]["Month"].ToString();
                cbbYear.SelectedValue = dt.Rows[0]["Year"].ToString();
                
                dtDateNow.Text = dt.Rows[0]["Date"].ToString();
                dtDateBefor.Value = dtDateNow.Value.AddMonths(-1);

                txtUnit.Text = dt.Rows[0]["Unit"].ToString();
                txtNumberBefor.Text = dt.Rows[0]["MeterBefore"].ToString();
                txtNumberNow.Text = dt.Rows[0]["MeterNow"].ToString();

                string strBarcode = dt.Rows[0]["Code"].ToString();
                pbQrcode.Image = Barcode.QRCode(strBarcode, Color.Black, Color.White, "Q", 3, false);

                txtId.Text = dt.Rows[0]["Id"].ToString();
                txtCode.Text = dt.Rows[0]["Code"].ToString();
                txtRaw.Text = dt.Rows[0]["Raw"].ToString();
                txtMoneyFt.Text = dt.Rows[0]["Ft"].ToString();
                txtDiscountDetail.Text = dt.Rows[0]["Detail"].ToString();
                txtDiscount.Text = dt.Rows[0]["Discount"].ToString();

                txtMoney.Text = dt.Rows[0]["Summary"].ToString();
                txtMoneyVat.Text = dt.Rows[0]["Vat"].ToString();
                txtMoneyAll.Text = dt.Rows[0]["Total"].ToString();
                txtRemark.Text = dt.Rows[0]["Remark"].ToString();

                txtMoneyPay.Text = dt.Rows[0]["Payment"].ToString();
                dtPayEnd.Text = dt.Rows[0]["EndDate"].ToString();
                dtPayStart.Value = dtDate.Value.AddDays(+1);

                cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                cbbPayment.SelectedValue = dt.Rows[0]["MoneyId"].ToString();

                dtPay.Text = dt.Rows[0]["Status"].ToString() == "1005" ? DateTime.Today.ToString() : dt.Rows[0]["PayDate"].ToString();

                txtScan.Text = "";

                GridView.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public void Clear()
        {
            try
            {
                IsStart = false;
                Function.ClearAlls(gbForm);
                CountEnter = 0;

                pbQrcode.Image = null;

                cbbMonth.Enabled = true;
                cbbYear.Enabled = true;
                cbbStatus.Enabled = true;

                int year = Convert.ToInt32(DateTime.Now.ToString("yyyy", Function.SetFormatDate(Cul.EN)));
                int month = Convert.ToInt32(DateTime.Now.ToString("MM"));
                int days = Convert.ToInt32(DateTime.Now.ToString("dd"));

                dtTime.Value = DateTime.Today;
                dtDate.Value = new DateTime(year, month, 18);

                dtDateNow.Value = dtDate.Value;
                dtDateBefor.Value = dtDate.Value.AddMonths(-1);
                dtPay.Value = DateTime.Today;

                dtPayStart.Value = dtDate.Value.AddDays(1);
                dtPayEnd.Value = dtPayStart.Value.AddDays(10);

                pbQrcode.Image = null;

                txtMonthOverdue.Text = "0";
                txtMoneyOverdue.Text = "0.00";

                SearchData(false, out Numbers);

                txtScan.Text = string.Empty;
                SetPlaceholderVisibility();

            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void GetBill(string Type = "")
        {
            if (!IsStart)
            {
                DateTime DateOperation = dtDate.Value.AddMonths(-1);

                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", ""},
                    {"@IsDelete", ""},
                    {"@Operation", Type == Operation.Before? Operation.BeforeAbbr : Operation.OverdueAbbr},
                    {"@AccountId", "0"},
                    {"@VersionId", "0"},
                    {"@InvoiceNumber", ""},
                    {"@Date", Type == Operation.Overdue? Date.GetDate(dtp : dtDateNow, Format : 4) : ""},
                    {"@Time", ""},
                    {"@Month", DateOperation.Month.ToString()},
                    {"@Year", DateOperation.ToString("yyyy", CultureInfo.GetCultureInfo("th-TH"))},
                    {"@MeterNow", ""},
                    {"@MeterBefore", ""},
                    {"@Unit", ""},
                    {"@Raw", ""},
                    {"@Ft", ""},
                    {"@Discount", ""},
                    {"@Detail", ""},
                    {"@Summary", ""},
                    {"@Vat", ""},
                    {"@Total", ""},
                    {"@Payment", ""},
                    {"@MoneyId", "0"},
                    {"@EndDate", ""},
                    {"@PayDate", ""},
                    {"@Barcode", ""},
                    {"@Remark", ""},
                };

                db.Get(Store.ManageElectricitys, Parameter, out Error, out dt);

                if (Type == Operation.Before)
                {
                    if (string.IsNullOrEmpty(Error) && dt.Rows.Count > 0)
                    {
                        string dates = dt.Rows[0]["DateBefor"].ToString();
                        txtNumberBefor.Text = dt.Rows[0]["MeterNow"].ToString();
                        dtDateBefor.Text = dates;
                        dtTime.Focus();
                    }
                    else
                    {
                        txtNumberBefor.Text = "";
                        txtNumberBefor.Focus();
                    }
                }

                if (Type == Operation.Overdue)
                {
                    if (string.IsNullOrEmpty(Error) && dt.Rows.Count > 0)
                    {
                        txtMonthOverdue.Text = dt.Rows[0]["Mounth"].ToString();
                        txtMoneyOverdue.Text = dt.Rows[0]["Amount"].ToString();
                    }
                    else
                    {
                        txtMonthOverdue.Text = "0";
                        txtMoneyOverdue.Text = "0.00";
                    }
                }
            }
        }

        private void Scan(object sender, EventArgs e)
        {
            try
            {
                if (txtScan.Text != "" && txtScan.Text != "\r\n\r\n\r\n\r\n")
                {
                    strBarcode = txtScan.Text;
                    strInvoice = "";
                    Function.getElectricData(txtScan.Text);
                    strInvoice = "";
                    CountEnter = 0;

                    foreach (ElectricModel Electric in GlobalVar.ElectricDataList)
                    {
                        strInvoice = Electric.ReceiptId;
                        txtInvoiceNumber.Text = Electric.ReceiptId;
                        dtDate.Value = Electric.ReadDate;
                        dtTime.Text = "00:00:00";
                        dtDateNow.Value = Electric.ReadDate;
                        dtPayEnd.Value = Electric.PayDate;
                        txtUnit.Text = Convert.ToString(Electric.Unit);
                        txtMoneyPay.Text = Convert.ToString(Electric.Amount);
                        break;
                    }

                    SearchData(false, out Numbers, txtInvoiceNumber.Text);

                    if (Numbers != 1)
                    {
                        SearchData(true, out Numbers);
                    }
                    else
                    {
                        btnEdit.Focus();
                    }
                }

                txtScan.Clear();
                SetPlaceholderVisibility();

            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void Reader(object sender, EventArgs e)
        {
            if (CountEnter == 4)
            {
                CountEnter = 0;
                Scan(sender, e);
            }
        }

        private void txtNumberNow_TextChanged(object sender, EventArgs e)
        {
            if (txtNumberNow.Text != "")
            {
                Calculator();
            }
            else
            {
                txtUnit.Text = "";
            }
        }

        private void txtRecId_TextChanged(object sender, EventArgs e)
        {
            if (txtInvoiceNumber.Text == "")
            {
                dtDate.Enabled = false;
                dtTime.Enabled = false;
                txtNumberNow.Enabled = false;
                cbbStatus.Enabled = false;
                dtPay.Enabled = false;
            }
            else
            {
                dtDate.Enabled = true;
                dtTime.Enabled = true;
                txtNumberNow.Enabled = true;
                cbbStatus.Enabled = true;
                dtPay.Enabled = true;
            }
        }

        private void txtNumberNow_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    Calculator();
                    txtRemark.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
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
                   Clear();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void btnAd_Click(object sender, EventArgs e)
        {
            dtPayEnd.Value = dtPayEnd.Value.AddDays(1);
        }

        private void btnRe_Click(object sender, EventArgs e)
        {
            dtPayEnd.Value = dtPayEnd.Value.AddDays(-1);
        }

        private void txtNumberBefor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8)
                {
                }
                else
                {
                    e.Handled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void KeyEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CountEnter++;
            }
        }
        
        private void txtNumberBefor_TextChanged(object sender, EventArgs e)
        {
            if (txtNumberBefor.Text != "" && txtUnit.Text != "")
            {
                int Number = 0;
                Number = int.Parse(txtNumberBefor.Text) + int.Parse(txtUnit.Text);
                txtNumberNow.Text = Convert.ToString(Number);
            }
        }

        private void dtTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    int theMonth = Convert.ToInt32(dtDate.Value.ToString("MM"));
                    int theYear = Convert.ToInt32(dtDate.Value.ToString("yyyy")) + 543;

                    cbbMonth.SelectedIndex = theMonth;
                    cbbYear.SelectedValue = theYear;
                    
                    dtDateNow.Text = dtDate.Text;
                    dtDateBefor.Value = dtDate.Value.AddMonths(-1);

                    dtPayStart.Value = dtDate.Value.AddDays(1);
                    dtPay.Value = DateTime.Today;

                    GetBill(Operation.Before);
                    GetBill(Operation.Overdue);

                    if (txtNumberBefor.Text == "")
                    {
                        txtNumberBefor.Focus();
                    }
                    else if (txtNumberBefor.Text != "" && txtNumberNow.Text == "")
                    {
                        txtNumberNow.Focus();
                    }
                    else
                    {
                        txtRemark.Focus();
                    }
                }
                catch (Exception)
                {
                    txtNumberBefor.Text = "0";
                    txtMoneyOverdue.Text = "0.00";
                    txtMonthOverdue.Text = "0";
                    cbbStatus.SelectedValue = 0;
                }
            }
        }

        private void TextBoxFormat(object sender, EventArgs e)
        {
            try
            {
                var tb = (TextBox)sender;
                tb.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(tb.Text));
            }
            catch
            {

            }
        }

        private void cbbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

        private void cbbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbVersion.SelectedValue.ToString() != "0" && cbbVersion.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                GetDataMaster(Function.getComboBoxValue(cbbVersion));
                btnScan.Enabled = true;
                txtScan.Enabled = true;
                txtInvoiceNumber.Enabled = true;
                dtTime.Value = new DateTime(2020, 02, 02, 0, 0, 0);
                btnScan.Focus();
            }
        }

        private void txtNumberBefor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNumberNow.Text == "")
                {
                    txtNumberNow.Focus();
                }
                else if (txtDiscountDetail.Text == "")
                {
                    txtDiscountDetail.Focus();
                }
                else 
                {
                    txtRemark.Focus();
                }
            }
        }

        public void Calculator()
        {
            try
            {
                int theMonth = Convert.ToInt32(dtDate.Value.ToString("MM"));
                int theYear = Convert.ToInt32(dtDate.Value.ToString("yyyy")) + 543;

                cbbMonth.SelectedIndex = theMonth;
                cbbYear.SelectedValue = theYear;

                int Units = 0;
                int Befor = 0;
                int Now = 0;

                string Fts = string.IsNullOrEmpty(txtFt.Text) ? Ft : txtFt.Text;

                Befor = string.IsNullOrEmpty(txtNumberBefor.Text) ? 0 : Convert.ToInt32(txtNumberBefor.Text);
                Now = string.IsNullOrEmpty(txtNumberNow.Text) ? 0 : Convert.ToInt32(txtNumberNow.Text);
                Units = Now - Befor;

                txtUnit.Text = Units.ToString();
                txtRaw.Text = string.Format("{0:#,##0.00}", Function.CalculateElectricity(First, Next, Over, Units));
                txtMoneyFt.Text = string.Format("{0:#,##0.00}", Math.Round(Convert.ToDouble(Units) * Convert.ToDouble(Fts), 2));

                txtMoney.Text = string.Format("{0:#,##0.00}", Math.Round((
                                Convert.ToDouble(txtRaw.Text) +
                                Convert.ToDouble(txtFee.Text) +
                                Convert.ToDouble(txtMoneyFt.Text)) -
                                Convert.ToDouble(txtDiscount.Text), 2));

                txtMoneyVat.Text = string.Format("{0:#,##0.00}", Math.Round(((Convert.ToDouble(txtMoney.Text) * (Convert.ToDouble(txtVat.Text)) / 100)), 2));
                txtMoneyAll.Text = string.Format("{0:#,##0.00}", Math.Round(Convert.ToDouble(txtMoney.Text) + Convert.ToDouble(txtMoneyVat.Text), 2));
                
                txtMoneyPay.Text = string.Format("{0:#,##0.00}", Math.Round(Convert.ToDouble(txtMoneyAll.Text), 2) + Math.Round(Convert.ToDouble(txtMoneyOverdue.Text), 2));
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void txtDiscountDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDiscount.Text == "" || txtDiscount.Text == "0.00")
                {
                    txtDiscount.Focus();
                }
                else
                {
                    txtRemark.Focus();
                }
            }
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtRemark.Text == "")
                {
                    txtRemark.Focus();
                }
                else
                {
                    if (Function.getComboboxId(cbbStatus) == "0")
                    {
                        cbbStatus.Focus();
                    }
                    else
                    {
                        btnAdd.Focus();
                    }
                }
            }
        }

        private void SumTotal()
        {
            try
            {
                double Sum = 0;
                double amount = Convert.ToDouble(txtMoneyPay.Text);
                double overdue = Convert.ToDouble(txtMoneyOverdue.Text);
                Sum = amount + overdue;

                txtMoneyPay.Text = string.Format("{0:#,##0.00}", Sum);
            }
            catch
            {

            }
        }

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Function.getComboboxId(cbbStatus) == "0")
                {
                    cbbStatus.Focus();
                }
                else
                {
                    btnAdd.Focus();
                }
            }
        }
        

        public string GetDetails()
        {
            return "Bill " + string.Concat(cbbMonth.Text, " ", cbbYear.Text) + " (฿" + txtMoneyPay.Text + ")";
        }

        private void SetPlaceholderVisibility()
        {
            IsPlaceholderVisible = string.IsNullOrEmpty(txtScan.Text);
            txtScan.Invalidate();
            txtScan.Text = IsPlaceholderVisible ? PlaceholderText : txtScan.Text;
            txtScan.ForeColor = IsPlaceholderVisible ? Color.Gray : Color.Black;
            txtScan.Font = IsPlaceholderVisible ? new Font(txtScan.Font.FontFamily, 8f, FontStyle.Italic) : new Font(txtScan.Font.FontFamily, 8f, FontStyle.Regular);

            if (IsPlaceholderVisible)
            {
                txtScan.Text = PlaceholderText;
                txtScan.SelectionStart = 0;
                txtScan.SelectionLength = 0;
            }
        }

        private new void Click(object sender, EventArgs e)
        {
            if (IsPlaceholderVisible)
            {
                txtScan.Text = "";
                IsPlaceholderVisible = false;
                txtScan.ForeColor = Color.Black;
                txtScan.Font = new Font(txtScan.Font.FontFamily, 9.75f, FontStyle.Regular);
            }
        }

        private new void Leave(object sender, EventArgs e)
        {
            SetPlaceholderVisibility();
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";

                strCondition += txtCode.Text != "" ? ", รหัส : " + txtCode.Text : "";
                strCondition += cbbAccount.Text != ":: กรุณาเลือก ::" ? ", " + lblName.Text + " " + cbbAccount.Text : "";
                strCondition += cbbVersion.Text != ":: กรุณาเลือก ::" ? ", " + lblVersion.Text + " " + cbbVersion.Text : "";
                strCondition += txtInvoiceNumber.Text != "" ? ", " + lblInvoiceNumber.Text + " " + txtInvoiceNumber.Text : "";
                strCondition += cbbMonth.Text != ":: กรุณาเลือก ::" ? ", " + lblbMonth.Text + " " + cbbMonth.Text : "";
                strCondition += cbbYear.Text != ":: กรุณาเลือก ::" ? ", " + "ปี :" + " " + cbbYear.Text : "";
                strCondition += txtNumberNow.Text != "" ? ", " + lblNumberNow.Text + "ครั้งนี้ :" + txtNumberNow.Text : "";
                strCondition += txtNumberBefor.Text != "" ? ", " + lblNumberBefor.Text + "ครั้งก่อน :" + txtNumberBefor.Text : "";
                strCondition += txtUnit.Text != "" ? ", " + lblUnit.Text + " " + txtUnit.Text : "";
                strCondition += txtRaw.Text != "" ? ", " + lblRaw.Text + " " + txtRaw.Text : "";
                strCondition += txtDiscountDetail.Text != "" ? ", " + lblDiscount.Text + " " + txtDiscountDetail.Text : "";
                strCondition += txtMoney.Text != "" ? ", " + lblMoney.Text + " " + txtMoney.Text : "";
                strCondition += txtMoneyAll.Text != "" ? ", " + lblMoneyAll.Text + " " + txtMoneyAll.Text : "";
                strCondition += txtMoneyPay.Text != "" ? ", ยอดที่ต้องชำระ :" + txtMoneyPay.Text : "";
                strCondition += txtDiscount.Text != "" ? ", " + lblDiscount.Text + " " + txtDiscount.Text : "";
                strCondition += txtVat.Text != "" ? ", " + lblVat.Text + " " + txtVat.Text : "";
                strCondition += txtFt.Text != "" ? ", " + lblFt.Text + " " + txtFt.Text : "";
                strCondition += txtRemark.Text != "" ? ", " + "หมายเหตุ :" + " " + txtRemark.Text : "";

                strCondition += cbbStatus.Text != ":: กรุณาเลือก ::" ? ", " + lblStatus.Text + " " + cbbStatus.Text : "";
                strCondition += cbbPayment.Text != ":: กรุณาเลือก ::" ? ", " + lblPayment.Text + " " + cbbPayment.Text : "";

                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return "";
            }
        }

        private void txtNumberNow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRemark.Focus();
            }
        }

        private void txtInvoiceNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchData(true, out Numbers, txtInvoiceNumber.Text);
            }
        }

        private void SearchInvoice(object sender, EventArgs e)
        {
            SearchData(true, out Numbers, txtInvoiceNumber.Text);
        }

        private void txtMoneyOverdue_TextChanged(object sender, EventArgs e)
        {
            if (txtMoneyOverdue.Text != "" && txtMoneyOverdue.Text != "0.00")
            {
                CalElectValue();
            }
        }
    }
}