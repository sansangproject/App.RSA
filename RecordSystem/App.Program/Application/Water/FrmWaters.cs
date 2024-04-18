﻿using System;
using System.Data;
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

namespace SANSANG
{
    public partial class FrmWaters : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "MWASA00";
        public string AppName = "FrmWaters";
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
        CharacterConstant CharType = new CharacterConstant();
        private Timer Timer = new Timer();
        CultureConstant Cul = new CultureConstant();

        public string[,] Parameter = new string[,] { };
        public bool IsStart = true;
        public string Account = "0";
        public int Rows = 0;
        public int CountRows = 0;
        public int Numbers = 0;
        public bool NewData = true;
        public string strBarcode = "";
        public string strInvoice = "";
        public string WaterRates = "0";
        public int AccountId = 0;
        public int CountEnter = 0;
        public int NumberOfPayment = -7;

        public string qrHeader;
        public string qrLine2;
        public string qrLine3;
        public string qrLine4;
        public string strQR;

        public FrmWaters(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
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
            IsStart = true;

            List.GetList(cbbAccount, DataList.WaterAccount);
            List.GetLists(cbbWaterRates, string.Format(DataList.WaterRatesId, "0"));
            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "1"));
            List.GetLists(cbbPayment, DataList.MoneyId);

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
                dtGrid = dt.DefaultView.ToTable(true, "SNo", "InvoiceNumber", "Dates", "Unit", "Pay", "StatusName", "DueDate", "Id");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
                DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                Function.showGridViewFormatFromStore(dtGrid, GridView
                        , "ลำดับ", 30, true, mc, mc
                        , "เลขที่ใบแจ้ง", 100, true, mc, mc
                        , "ประจำเดือน", 100, true, mc, ml
                        , "จำนวนหน่วย", 50, true, mr, mr
                        , "จำนวนเงิน (บาท)", 100, true, mr, mr
                        , "สถานะ", 100, true, mc, mc
                        , "กำหนดชำระ", 100, true, mc, ml
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
            this.Close();
        }

        private void Search(object sender, EventArgs e)
        {
            SearchData(true,out Numbers, "");
        }

        private void AddData(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtInvoiceNumber.Text) && Function.getComboBoxValue(cbbStatus) != "0" && !string.IsNullOrEmpty(txtMoneyPay.Text) && dtTime.Value.ToString("HH:mm:ss") != "00:00:00")
                {
                    if (!Function.IsDuplicates(Table.Waters, Function.getComboBoxValue(cbbMonth), Function.getComboBoxValue(cbbYear), Detail: GetDetails()))
                    {
                        txtCode.Text = Function.GetCodes(Table.WatersId, "", "Generated");
                        pbQrcode.Image = Barcode.QRCode(txtCode.Text, Color.Black, Color.White, "Q", 3, false);
                        strQR = qrHeader + "\r\n" + qrLine2 + "\r\n" + qrLine3 + "\r\n" + qrLine4 + "\r\n";

                        Parameter = new string[,]
                        {
                            {"@Id", ""},
                            {"@Code", txtCode.Text},
                            {"@User", UserId},
                            {"@IsActive", "1"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@AccountId", Function.getComboBoxValue(cbbAccount)},
                            {"@WaterRatesId", Function.getComboBoxValue(cbbWaterRates)},
                            {"@InvoiceNumber", txtInvoiceNumber.Text},
                            {"@InvoiceDate", Date.GetDate(dtp : dtDate, Format : 4)},
                            {"@DateBill", Date.GetDate(dtp : dtDateNow, Format : 4)},
                            {"@DateBefore", Date.GetDate(dtp : dtDateBefor, Format : 4)},
                            {"@Numeral", Function.MoveNumberStringComma(txtNumberNow.Text)},
                            {"@NumeralBefore", Function.MoveNumberStringComma(txtNumberBefor.Text)},
                            {"@Time", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                            {"@InvoiceMonth", Function.getComboBoxValue(cbbMonth)},
                            {"@InvoiceYear", Function.getComboBoxValue(cbbYear)},
                            {"@Unit", Function.MoveNumberStringComma(txtUnit.Text)},
                            {"@Raw", Function.MoveNumberStringComma(txtDipMoney.Text)},
                            {"@Money",  Function.MoveNumberStringComma(txtMoneyWater.Text)},
                            {"@Total", Function.MoveNumberStringComma(txtMoney.Text)},
                            {"@Vat", Function.MoveNumberStringComma(txtMoneyVat.Text)},
                            {"@Pay", Function.MoveNumberStringComma(txtMoneyAll.Text)},
                            {"@PayAll", Function.MoveNumberStringComma(txtMoneyPay.Text)},
                            {"@PaymentId", Function.getComboBoxValue(cbbPayment)},
                            {"@Discount", Function.MoveNumberStringComma(txtDiscount.Text)},
                            {"@PayDate", Date.GetDate(dtp : dtPay, Format : 4)},
                            {"@PaymentDate", Date.GetDate(dtp : dtDatePayment, Format : 4)},
                            {"@Status", Function.getComboBoxValue(cbbStatus)},
                            {"@Barcode", strBarcode},
                            {"@Remark", txtRemark.Text},
                        };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageWaters, Parameter, txtCode.Text, Details: GetDetails()))
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
                        {"@WaterRatesId", "0"},
                        {"@InvoiceNumber", ""},
                        {"@InvoiceDate", ""},
                        {"@DateBill", ""},
                        {"@DateBefore", ""},
                        {"@Numeral", ""},
                        {"@NumeralBefore", ""},
                        {"@Time", ""},
                        {"@InvoiceMonth", ""},
                        {"@InvoiceYear", ""},
                        {"@Unit", ""},
                        {"@Raw", ""},
                        {"@Money", ""},
                        {"@Total", ""},
                        {"@Vat", ""},
                        {"@Pay", ""},
                        {"@PayAll", ""},
                        {"@PaymentId", "0"},
                        {"@Discount", ""},
                        {"@PayDate", ""},
                        {"@PaymentDate", ""},
                        {"@Barcode", ""},
                        {"@Remark", ""},
                    };

                    db.Get(Store.ManageWaters, Parameter, out Error, out dt);
                    ShowData(dt);
                    GetBill(Operation.Overdue);
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
                        {"@User", UserId},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.UpdateAbbr},
                        {"@AccountId", Function.getComboBoxValue(cbbAccount)},
                        {"@WaterRatesId", Function.getComboBoxValue(cbbWaterRates)},
                        {"@InvoiceNumber", txtInvoiceNumber.Text},
                        {"@InvoiceDate", Date.GetDate(dtp : dtDate, Format : 4)},
                        {"@DateBill", Date.GetDate(dtp : dtDateNow, Format : 4)},
                        {"@DateBefore", Date.GetDate(dtp : dtDateBefor, Format : 4)},
                        {"@Numeral", Function.MoveNumberStringComma(txtNumberNow.Text)},
                        {"@NumeralBefore", Function.MoveNumberStringComma(txtNumberBefor.Text)},
                        {"@Time", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                        {"@InvoiceMonth", Function.getComboBoxValue(cbbMonth)},
                        {"@InvoiceYear", Function.getComboBoxValue(cbbYear)},
                        {"@Unit", Function.MoveNumberStringComma(txtUnit.Text)},
                        {"@Raw", Function.MoveNumberStringComma(txtDipMoney.Text)},
                        {"@Money",  Function.MoveNumberStringComma(txtMoneyWater.Text)},
                        {"@Total", Function.MoveNumberStringComma(txtMoney.Text)},
                        {"@Vat", Function.MoveNumberStringComma(txtMoneyVat.Text)},
                        {"@Pay", Function.MoveNumberStringComma(txtMoneyAll.Text)},
                        {"@PayAll", Function.MoveNumberStringComma(txtMoneyPay.Text)},
                        {"@PaymentId", Function.getComboBoxValue(cbbPayment)},
                        {"@Discount", Function.MoveNumberStringComma(txtDiscount.Text)},
                        {"@PayDate", Date.GetDate(dtp : dtPay, Format : 4)},
                        {"@PaymentDate", Date.GetDate(dtp : dtDatePayment, Format : 4)},
                        {"@Status", Function.getComboBoxValue(cbbStatus)},
                        {"@Barcode", strBarcode},
                        {"@Remark", txtRemark.Text},
                    };

                    if (Edit.Update(AppCode, AppName, UserId, Store.ManageWaters, Parameter, txtCode.Text, Details: GetDetails()))
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

                    if (Delete.Drop(AppCode, AppName, UserId, 0, Table.Waters, txtCode, Details: GetDetails(), true))
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
                    {"@Service", ""},
                    {"@Discount", ""},
                    {"@Vat", ""},
                    {"@RawText", ""},
                    {"@RawValue", ""},
                    {"@DueDate", ""},
                };

                db.Get(Store.ManageWaterRates, Parameter, out Error, out dt);

                if (string.IsNullOrEmpty(Error) && dt.Rows.Count > 0)
                {
                    txtDipValue.Text = dt.Rows[0]["RawValue"].ToString();
                    txtDipText.Text = dt.Rows[0]["RawText"].ToString();
                    txtDipCost.Text = dt.Rows[0]["Rates"].ToString();
                    txtFeeMonth.Text = dt.Rows[0]["Service"].ToString();
                    txtVat.Text = dt.Rows[0]["Vat"].ToString();
                    NumberOfPayment = Convert.ToInt32(dt.Rows[0]["DueDate"].ToString()) * (-1);
                    txtDiscount.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public void CalWaterValue()
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

        private void InvoiceInput(object sender, EventArgs e)
        {
            if (txtInvoiceNumber.Text != "")
            {
                dtDate.Enabled = true;
                dtTime.Enabled = true;
            }
            else
            {
                dtDate.Enabled = false;
                dtTime.Enabled = false;
            }
        }

        private void txtMoneyPay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtMoneyPay.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(txtMoneyPay.Text));
            }
            catch
            {

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
                    {"@WaterRatesId", "0"},
                    {"@InvoiceNumber", ""},
                    {"@InvoiceDate", ""},
                    {"@DateBill", Type == Operation.Overdue? Date.GetDate(dtp : dtDateNow, Format : 4) : ""},
                    {"@DateBefore", ""},
                    {"@Numeral", ""},
                    {"@NumeralBefore", ""},
                    {"@Time", ""},
                    {"@InvoiceMonth", DateOperation.Month.ToString()},
                    {"@InvoiceYear", DateOperation.ToString("yyyy", CultureInfo.GetCultureInfo("th-TH"))},
                    {"@Unit", ""},
                    {"@Raw", ""},
                    {"@Money", ""},
                    {"@Total", ""},
                    {"@Vat", ""},
                    {"@Pay", ""},
                    {"@PayAll", ""},
                    {"@PaymentId", "0"},
                    {"@Discount", ""},
                    {"@PayDate", ""},
                    {"@PaymentDate", ""},
                    {"@Barcode", ""},
                    {"@Remark", ""},
                };

                db.Get(Store.ManageWaters, Parameter, out Error, out dt);

                if (Type == Operation.Before)
                {
                    if (string.IsNullOrEmpty(Error) && dt.Rows.Count > 0)
                    {
                        txtNumberBefor.Text = dt.Rows[0]["Numeral"].ToString();
                        dtDateBefor.Value = Convert.ToDateTime(dt.Rows[0]["InvoiceDate"].ToString());
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
                    Function.GetWaterData(txtScan.Text, NumberOfPayment);
                    CountEnter = 0;

                    foreach (WaterModel Waters in GlobalVar.WaterDataList)
                    {
                        strInvoice = Waters.ReceiptId;
                        txtInvoiceNumber.Text = Waters.ReceiptId;
                        dtDate.Value = Waters.InvoiceDate;
                        dtTime.Text = "00:00:00";
                        dtDateNow.Value = Waters.ReadDate;
                        dtPay.Value = Waters.PayDate;
                        txtUnit.Text = Convert.ToString(Waters.Unit);
                        txtMoneyPay.Text = Convert.ToString(Waters.Amount);
                        break;
                    }

                    SearchData(false, out Numbers, strInvoice);

                    if (Numbers != 1)
                    {
                        SearchData(true, out Numbers);
                    }
                }
            }
            catch (Exception ex)
            {
                NewData = true;
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void Reader(object sender, EventArgs e)
        {
            if (CountEnter == 4)
            {
                Scan(sender, e);
                txtScan.Clear();
            }
        }

        private void NumberKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (!(char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8))
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    dtDateBefor.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void ShowData(DataTable dt)
        {
            try
            {
                cbbAccount.SelectedValue = dt.Rows[0]["AccountId"].ToString();
                cbbWaterRates.SelectedValue = dt.Rows[0]["WaterRatesId"].ToString();
                txtUnit.Text = dt.Rows[0]["Unit"].ToString();

                txtId.Text = dt.Rows[0]["Id"].ToString();
                txtCode.Text = dt.Rows[0]["Code"].ToString();
                string strBarcode = dt.Rows[0]["Code"].ToString();
                pbQrcode.Image = Barcode.QRCode(strBarcode, Color.Black, Color.White, "Q", 3, false);

                dtDateBefor.Text = dt.Rows[0]["DateBefore"].ToString();
                txtNumberBefor.Text = dt.Rows[0]["NumeralBefore"].ToString();
                txtInvoiceNumber.Text = dt.Rows[0]["InvoiceNumber"].ToString();

                cbbMonth.SelectedValue = dt.Rows[0]["InvoiceMonth"].ToString();
                cbbYear.SelectedValue = dt.Rows[0]["InvoiceYear"].ToString();

                dtDate.Text = dt.Rows[0]["InvoiceDate"].ToString();
                dtTime.Text = dt.Rows[0]["Time"].ToString();

                dtDateNow.Text = dt.Rows[0]["DateBill"].ToString();
                txtNumberNow.Text = dt.Rows[0]["Numeral"].ToString();

                txtDipMoney.Text = dt.Rows[0]["Raw"].ToString();
                txtMoneyWater.Text = dt.Rows[0]["Money"].ToString();
                txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
                txtMoney.Text = dt.Rows[0]["Total"].ToString();
                txtMoneyVat.Text = dt.Rows[0]["Vat"].ToString();
                txtMoneyAll.Text = dt.Rows[0]["Pay"].ToString();

                dtPay.Text = dt.Rows[0]["PayDate"].ToString();
                cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                cbbPayment.SelectedValue = dt.Rows[0]["PaymentId"].ToString();
                dtDatePayment.Text = dt.Rows[0]["PaymentDate"].ToString();
                txtMoneyPay.Text = dt.Rows[0]["PayAll"].ToString();
                txtRemark.Text = dt.Rows[0]["Remark"].ToString();

                txtDipValue.Text = dt.Rows[0]["RawValue"].ToString();
                txtDipText.Text = dt.Rows[0]["RawText"].ToString();
                txtVat.Text = dt.Rows[0]["RawVat"].ToString();
                txtFeeMonth.Text = dt.Rows[0]["Fee"].ToString();

                GridView.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void txtMoneyOverdue_TextChanged(object sender, EventArgs e)
        {
            if (txtMoneyOverdue.Text != "" && txtMoneyOverdue.Text != "0.00")
            {
                CalWaterValue();
            }
        }

        private void KeyEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CountEnter++;
            }
        }

        private void txtNumberNow_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    Calculator();
                    btnAdd.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public void Calculator()
        {
            try
            {
                string units = txtNumberNow.Text.Replace("\r\n", "");

                if (units == "")
                {
                    units = Convert.ToString(Convert.ToDouble(txtUnit.Text) + Convert.ToDouble(txtNumberBefor.Text));
                    txtNumberNow.Text = units;
                }

                txtUnit.Text = Convert.ToString(Math.Round(Convert.ToDouble(units) - Convert.ToDouble(txtNumberBefor.Text), 2));
                txtDipMoney.Text = string.Format("{0:#,##0.00}", Math.Round(Convert.ToDouble(txtUnit.Text) * Convert.ToDouble(txtDipValue.Text), 2));
                txtMoneyWater.Text = string.Format("{0:#,##0.00}", Math.Round(Convert.ToDouble(txtUnit.Text) * Convert.ToDouble(txtDipCost.Text), 4));

                txtMoney.Text = string.Format("{0:#,##0.00}", Math.Round((
                                Convert.ToDouble(txtDipMoney.Text) +
                                Convert.ToDouble(txtMoneyWater.Text) +
                                Convert.ToDouble(txtFeeMonth.Text)) -
                                Convert.ToDouble(txtDiscount.Text == "" ? "0" : txtDiscount.Text), 2));

                txtMoneyVat.Text = string.Format("{0:#,##0.00}", Math.Round(((Convert.ToDouble(txtMoney.Text) * (Convert.ToDouble(txtVat.Text)) / 100)), 3));
                txtMoneyAll.Text = string.Format("{0:#,##0.00}", Math.Round(Convert.ToDouble(txtMoney.Text) + Convert.ToDouble(txtMoneyVat.Text), 2));

                txtMoneyPay.Text = string.Format("{0:#,##0.00}", Math.Round(Convert.ToDouble(txtMoneyAll.Text), 2) + Math.Round(Convert.ToDouble(txtMoneyOverdue.Text), 2));

            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
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
                        {"@WaterRatesId", Search? Function.getComboBoxValue(cbbWaterRates) : "0"},
                        {"@InvoiceNumber", Search? txtInvoiceNumber.Text : ""},
                        {"@InvoiceDate", ""},
                        {"@DateBill", ""},
                        {"@DateBefore", ""},
                        {"@Numeral", ""},
                        {"@NumeralBefore", ""},
                        {"@Time", ""},
                        {"@InvoiceMonth", ""},
                        {"@InvoiceYear", ""},
                        {"@Unit", Search? Function.MoveNumberStringComma(txtUnit.Text) : ""},
                        {"@Raw", ""},
                        {"@Money", ""},
                        {"@Total", ""},
                        {"@Vat", ""},
                        {"@Pay", ""},
                        {"@PayAll", ""},
                        {"@PaymentId", Search? Function.getComboBoxValue(cbbPayment) : "0"},
                        {"@Discount", ""},
                        {"@PayDate", ""},
                        {"@PaymentDate", ""},
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
                        {"@WaterRatesId", "0"},
                        {"@InvoiceNumber", InvoiceNo},
                        {"@InvoiceDate", ""},
                        {"@DateBill", ""},
                        {"@DateBefore", ""},
                        {"@Numeral", ""},
                        {"@NumeralBefore", ""},
                        {"@Time", ""},
                        {"@InvoiceMonth", ""},
                        {"@InvoiceYear", ""},
                        {"@Unit", ""},
                        {"@Raw", ""},
                        {"@Money", ""},
                        {"@Total", ""},
                        {"@Vat", ""},
                        {"@Pay", ""},
                        {"@PayAll", ""},
                        {"@PaymentId", "0"},
                        {"@Discount", ""},
                        {"@PayDate", ""},
                        {"@PaymentDate", ""},
                        {"@Barcode", ""},
                        {"@Remark", ""},
                    };
                }

                db.Get(Store.ManageWaters, Parameter, out Error, out dt);

                if (string.IsNullOrEmpty(Error) && dt.Rows.Count > 0)
                {
                    CountRows = dt.Rows.Count;
                    ShowGridView(dt);

                    if (dt.Rows.Count == 1)
                    {
                        ShowData(dt);
                        GetBill(Operation.Overdue);
                    }
                }

                if (!string.IsNullOrEmpty(txtInvoiceNumber.Text) && dt.Rows.Count != 1)
                {
                    CountRows = dt.Rows.Count;
                    GetBill(Operation.Before);
                    GetBill(Operation.Overdue);
                }

                Number = CountRows;
            }
            catch (Exception ex)
            {
                Number = 0;
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public void Clear()
        {
            try
            {
                IsStart = false;
                Function.ClearAll(gbForm);
                CountEnter = 0;

                cbbStatus.SelectedValue = 0;
                cbbAccount.SelectedValue = 0;
                cbbWaterRates.SelectedValue = 0;
                cbbPayment.SelectedValue = 0;
                cbbMonth.SelectedIndex = 0;
                cbbYear.SelectedIndex = 0;

                dtDate.Enabled = false;
                dtDateBefor.Enabled = false;

                txtInvoiceNumber.Text = "";
                txtNumberBefor.Text = "";
                txtNumberNow.Text = "";
                txtUnit.Text = "";
                txtDipMoney.Text = "";
                txtMoneyWater.Text = "";
                txtMoney.Text = "";
                txtMoneyVat.Text = "";
                txtMoneyVat.Text = "";
                txtMoneyAll.Text = "";
                txtDipValue.Text = "";
                txtDipText.Text = "";
                txtDipCost.Text = "";
                txtVat.Text = "";
                txtCode.Text = "";
                txtMonthOverdue.Text = "";
                txtMoneyOverdue.Text = "";
                txtMoneyPay.Text = "";
                txtFeeMonth.Text = "";
                txtDiscount.Text = "";

                int year = Convert.ToInt32(DateTime.Now.ToString("yyyy", Function.SetFormatDate(Cul.EN)));
                int month = Convert.ToInt32(DateTime.Now.ToString("MM"));
                int days = Convert.ToInt32(DateTime.Now.ToString("dd"));
                dtTime.Value = new DateTime(2020, 02, 02, 0, 0, 0);
                dtDate.Value = new DateTime(year, month, days);

                dtDateNow.Value = dtDate.Value;
                dtDateBefor.Value = dtDate.Value.AddMonths(-1);
                dtPay.Value = dtDate.Value.AddDays(7);
                dtDatePayment.Value = new DateTime(year, month, days);

                pbQrcode.Image = null;
                txtScan.Text = "";
                GridView.Focus();

                SearchData(false, out Numbers);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void WaterRates_Selected(object sender, EventArgs e)
        {
            if (!IsStart)
            {
                WaterRates = Function.getComboBoxValue(cbbWaterRates);

                txtDipValue.Text = "";
                txtDipText.Text = "";
                txtDipCost.Text = "";
                txtFeeMonth.Text = "";
                txtVat.Text = "";
                NumberOfPayment = -7;
                txtDiscount.Text = "";

                if (WaterRates != "0")
                {
                    GetDataMaster(WaterRates);
                    btnScan.Enabled = true;
                    txtScan.Enabled = true;
                    txtInvoiceNumber.Enabled = true;
                    dtTime.Value = new DateTime(2020, 02, 02, 0, 0, 0);
                    txtScan.Focus();
                }
            }
        }

        private void DateChanged(object sender, EventArgs e)
        {
            try
            {
                int Month = Convert.ToInt32(dtDate.Value.ToString("MM"));
                int Year = Convert.ToInt32(dtDate.Value.ToString("yyyy"));

                if (Year <= 2500)
                {
                    Year = Year + 543;
                }

                cbbMonth.SelectedIndex = Month;
                cbbYear.SelectedValue = Year;
                dtDateBefor.Value = dtDate.Value.AddMonths(-1);
                dtDateNow.Value = dtDate.Value;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
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

        private void dtTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtUnit.Text == "")
                {
                    txtUnit.Focus();
                }
                else
                {
                    if (txtNumberBefor.Text != "")
                    {
                        btnAdd.Focus();
                    }
                    else
                    {
                        txtNumberBefor.Focus();
                    }
                }

                dtPay.Value = dtDate.Value.AddDays(NumberOfPayment);
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
                    Clear(sender, e);
                }
                if (keyCode == "Enter")
                {

                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void txtNumberBefor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.Focus();
            }
        }

        private void txtNumberBefor_TextChanged(object sender, EventArgs e)
        {
            if (txtNumberBefor.Text != "")
            {
                int Num = 0;
                Num = int.Parse(txtNumberBefor.Text) + int.Parse(txtUnit.Text == "" ? "0" : txtUnit.Text);
                txtNumberNow.Text = Convert.ToString(Num);
            }
        }

        private void txtUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNumberBefor.Text != "")
                {
                    btnAdd.Focus();
                }
                else
                {
                    txtNumberBefor.Focus();
                }
            }
        }

        private void txtInvoiceNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    SearchData(true, out Numbers);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void CalculateUnit(object sender, EventArgs e)
        {
            try
            {
                int sum = 0;
                sum = Convert.ToInt32(txtUnit.Text) + Convert.ToInt32(txtNumberBefor.Text);
                txtNumberNow.Text = sum.ToString();
            }
            catch
            {

            }
        }

        private void txtScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Function.IsCharacter(e.KeyChar, CharType.Barcode))
            {
                e.Handled = true;
            }
        }

        private void cbbAccount_Selected(object sender, EventArgs e)
        {
            if (!IsStart)
            {
                AccountId = Convert.ToInt32(Function.getComboBoxValue(cbbAccount));
                List.GetLists(cbbWaterRates, string.Format(DataList.WaterRatesId, AccountId));
            }
        }

        public string GetDetails()
        {
            return "Invoice No. " + txtInvoiceNumber.Text + " (฿" + txtMoneyPay.Text + ")";
        }
    }
}