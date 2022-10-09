using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;
using SANSANG.Utilites.App.Global;
using SANSANG.Utilites.App.Model;

namespace SANSANG
{
    public partial class FrmMWASA00 : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string strAppCode = "MWASA00";
        public string strAppName = "FrmMWASA00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string Green = "IMG600218805-6";
        public string Red = "IMG600218806-7";
        public string YellowC = "IMG600218807-8";

        public string strPath = "SPA591012026-3";
        public string fileName = "-";
        public string fileType = ".jpg";
        public string strImageCode = "";

        public int EnterNumber = 0;
        public int Character = 0;
        public int CountEnter = 0;
        public bool Selected = false;

        public int dtBefore = 0;
        public int dtOverdue = 0;

        private StoreConstant Store = new StoreConstant();
        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();
        private DataTable dtBill = new DataTable();
        private clsFunction Fn = new clsFunction();
        private clsInsert Insert = new clsInsert();
        private clsDelete Delete = new clsDelete();
        private clsEdit Edit = new clsEdit();
        private clsMessage Message = new clsMessage();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();
        private clsDate Date = new clsDate();
        private clsBarcode Barcode = new clsBarcode();
        private TableConstant Tb = new TableConstant();
        private CultureConstant Cul = new CultureConstant();
        private CharacterConstant CharType = new CharacterConstant();
        private Timer timer = new Timer();
        public string[,] Parameter = new string[,] { };
        public bool NewData = true;
        public string strBarcode = "";

        public string qrHeader;
        public string qrLine2;
        public string qrLine3;
        public string qrLine4;
        public string strQR;

        public FrmMWASA00(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            int sec = 2;
            timer.Interval = (sec * 1000);
            timer.Tick += new EventHandler(LoadList);
            timer.Start();
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetList(cbbUser, "Address", "Water");
            List.GetList(cbbVersion, "Version", "Water");
            List.GetList(cbbStatus, "1", "Status");
            List.GetList(cbbPayment, "Y", "Money");

            List.GetMonthList(cbbMonth);
            List.GetYearList(cbbYear);

            Clear();
            timer.Stop();
        }

        public void GetDataGrid(DataTable dt)
        {
            try
            {
                if (Fn.GetRows(dt) == 0)
                {
                    dataGridView.DataSource = null;
                    picExcel.Visible = false;
                    txtNumberNow.Focus();
                    txtCount.Text = Fn.ShowNumberOfData(0);
                    NewData = true;
                }
                else
                {
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "InvoiceNumber", "Dates", "Unit", "Amount", "StatusNameTh", "Code");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
                    DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                    Fn.showGridViewFormatFromStore(dtGrid, dataGridView
                            , "ลำดับ", 30, true, mc, mc
                            , "เลขที่ใบแจ้ง", 70, true, ml, ml
                            , "ประจำเดือน", 80, true, ml, ml
                            , "จำนวนหน่วย", 50, true, mc, mc
                            , "จำนวนเงิน (บาท)", 100, true, mc, mr
                            , "สถานะ", 150, true, mc, mc
                            , "", 0, false, mc, mc
                        );

                    NewData = false;
                    picExcel.Visible = true;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData(true);
        }

        public void Scan()
        {
            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@RcptWaterId", ""},
                    {"@MsWaterCode", ""},
                    {"@RcptWaterCode", txtCode.Text},
                    {"@RcptWaterNumber", txtInvoiceNumber.Text},
                    {"@RcptWaterDateBill", ""},
                    {"@RcptWaterTime", ""},
                    {"@RcptWaterMonth", ""},
                    {"@RcptWaterYear", ""},
                    {"@RcptWaterDateBefore", ""},
                    {"@RcptWaterNumeralBefore", ""},
                    {"@RcptWaterUnit", ""},
                    {"@RcptWaterStatus", ""},
                    {"@RcptWaterMoneyTotal", ""},
                    {"@RcptWaterPayDate", ""},
                    {"@RcptWaterDate", ""},
                    {"@RcptWaterNumeral", ""},
                    {"@RcptWaterPayment", ""},
                };

                db.Get("Spr_S_TblRecieptWater", Parameter, out strErr, out dt);
                GetDataGrid(dt);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNumber.Text != "" && Fn.getComboBoxValue(cbbStatus) != "0")
            {
                if (!Fn.IsDuplicates("InvoiceWater", Fn.getComboBoxValue(cbbMonth), Fn.getComboBoxValue(cbbYear), Detail: "Invoice " + cbbMonth.SelectedValue + "/" + cbbYear.SelectedValue))
                {
                    txtCode.Text = txtCode.Text = Fn.GetCodes("110", "", "Generated");
                    pbQrcode.Image = Barcode.QRCode(txtCode.Text, Color.Black, Color.White, "Q", 3, false);
                    strQR = qrHeader + "\r\n" + qrLine2 + "\r\n" + qrLine3 + "\r\n" + qrLine4 + "\r\n";

                    Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", txtCode.Text},
                        {"@User", strUserId},
                        {"@UserCode", Fn.getComboBoxValue(cbbUser)},
                        {"@Version", Fn.getComboBoxValue(cbbVersion)},
                        {"@InvoiceNumber", txtInvoiceNumber.Text},
                        {"@InvoiceDate", Date.GetDate(dtp : dtDate, Format : 4)},
                        {"@DateBill", Date.GetDate(dtp : dtDateNow, Format : 4)},
                        {"@DateBefore", Date.GetDate(dtp : dtDateBefor, Format : 4)},
                        {"@Numeral", Fn.MoveNumberStringComma(txtNumberNow.Text)},
                        {"@NumeralBefore", Fn.MoveNumberStringComma(txtNumberBefor.Text)},
                        {"@Time", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                        {"@InvoiceMonth", Fn.getComboBoxValue(cbbMonth)},
                        {"@InvoiceYear", Fn.getComboBoxValue(cbbYear)},
                        {"@Unit", Fn.MoveNumberStringComma(txtUnit.Text)},
                        {"@Raw", Fn.MoveNumberStringComma(txtDipMoney.Text)},
                        {"@Money", Fn.MoveNumberStringComma(txtMoneyWater.Text)},
                        {"@Total", Fn.MoveNumberStringComma(txtMoney.Text)},
                        {"@Vat", Fn.MoveNumberStringComma(txtMoneyVat.Text)},
                        {"@Pay", Fn.MoveNumberStringComma(txtMoneyAll.Text)},
                        {"@PayAll", Fn.MoveNumberStringComma(txtMoneyPay.Text)},
                        {"@Payment", Fn.getComboBoxValue(cbbPayment)},
                        {"@Discount", Fn.MoveNumberStringComma(txtDiscount.Text)},
                        {"@PayDate", Date.GetDate(dtp : dtPay, Format : 4)},
                        {"@PaymentDate", Date.GetDate(dtp : dtDatePayment, Format : 4)},
                        {"@Status", Fn.getComboBoxValue(cbbStatus)},
                        {"@Barcode", strBarcode},
                        {"@Remark", ""},
                    };

                    if (Insert.Add(strAppCode, strAppName, strUserId, "Store.InsertInvoiceWater", Parameter, txtCode.Text, "ใบแจ้งค่าน้ำ " + cbbMonth.Text + " " + cbbYear.Text))
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

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    Selected = true;
                    DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                    DataTable dt = new DataTable();

                    Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", row.Cells["Code"].Value.ToString()},
                        {"@User", ""},
                        {"@UserCode", "0"},
                        {"@Version", "0"},
                        {"@InvoiceNumber", ""},
                        {"@InvoiceDate", ""},
                        {"@DateBill", ""},
                        {"@DateBefore", ""},
                        {"@Numeral", ""},
                        {"@NumeralBefore", ""},
                        {"@Time", ""},
                        {"@InvoiceMonth", "0"},
                        {"@InvoiceYear", "0"},
                        {"@Unit", ""},
                        {"@Raw", ""},
                        {"@Money", ""},
                        {"@Total", ""},
                        {"@Vat",  ""},
                        {"@Pay", ""},
                        {"@PayAll", ""},
                        {"@Payment", "0"},
                        {"@Discount", ""},
                        {"@PayDate", ""},
                        {"@PaymentDate", ""},
                        {"@Status", "0"},
                        {"@Barcode", ""},
                        {"@Remark", ""},
                    };

                    db.Get("Store.SelectInvoiceWater", Parameter, out strErr, out dt);
                    ShowData(dt);
                    GetBill("Overdue");
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
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
                    {"@User", strUserId},
                    {"@UserCode", Fn.getComboBoxValue(cbbUser)},
                    {"@Version", Fn.getComboBoxValue(cbbVersion)},
                    {"@InvoiceNumber", txtInvoiceNumber.Text},
                    {"@InvoiceDate", Date.GetDate(dtp : dtDate, Format : 4)},
                    {"@DateBill", Date.GetDate(dtp : dtDateNow, Format : 4)},
                    {"@DateBefore", Date.GetDate(dtp : dtDateBefor, Format : 4)},
                    {"@Numeral", Fn.MoveNumberStringComma(txtNumberNow.Text)},
                    {"@NumeralBefore", Fn.MoveNumberStringComma(txtNumberBefor.Text)},
                    {"@Time", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                    {"@InvoiceMonth", Fn.getComboBoxValue(cbbMonth)},
                    {"@InvoiceYear", Fn.getComboBoxValue(cbbYear)},
                    {"@Unit", Fn.MoveNumberStringComma(txtUnit.Text)},
                    {"@Raw", Fn.MoveNumberStringComma(txtDipMoney.Text)},
                    {"@Money", Fn.MoveNumberStringComma(txtMoneyWater.Text)},
                    {"@Total", Fn.MoveNumberStringComma(txtMoney.Text)},
                    {"@Vat", Fn.MoveNumberStringComma(txtMoneyVat.Text)},
                    {"@Pay", Fn.MoveNumberStringComma(txtMoneyAll.Text)},
                    {"@PayAll", Fn.MoveNumberStringComma(txtMoneyPay.Text)},
                    {"@Payment", Fn.getComboBoxValue(cbbPayment)},
                    {"@Discount", Fn.MoveNumberStringComma(txtDiscount.Text)},
                    {"@PayDate", Date.GetDate(dtp : dtPay, Format : 4)},
                    {"@PaymentDate", Date.GetDate(dtp : dtDatePayment, Format : 4)},
                    {"@Status", Fn.getComboBoxValue(cbbStatus)},
                    {"@Barcode", strBarcode},
                    {"@Remark", ""},
                };

                if (Edit.Update(strAppCode, strAppName, strUserId, "Store.UpdateInvoiceWater", Parameter, txtCode.Text, "ใบแจ้งค่าน้ำ " + cbbMonth.Text + " " + cbbYear.Text))
                {
                    Clear();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                if (Delete.Drop(strAppCode, strAppName, strUserId, 0, Tb.InvoiceWater, txtCode, "ใบแจ้งค่าน้ำ " + cbbMonth.Text + " " + cbbYear.Text))
                {
                    Clear();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void picExcel_Click(object sender, EventArgs e)
        {

        }

        public void GetDataMaster(string Code)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id ", ""},
                    {"@Code", Code},
                    {"@Status", "0"},
                    {"@Version", ""},
                    {"@Rates", "0.00"},
                    {"@Service", "0.00"},
                    {"@Discount", ""},
                    {"@Vat", ""},
                    {"@RawText", ""},
                    {"@RawValue", "0.00"},
                    {"@DueDate", ""},
                    {"@User", ""},
                };

                db.Get("Store.SelectVersionWater", Parameter, out strErr, out dt);

                if (strErr == null && dt.Rows.Count > 0)
                {
                    txtDipValue.Text = dt.Rows[0]["RawValue"].ToString();
                    txtDipText.Text = dt.Rows[0]["RawText"].ToString();
                    txtDipCost.Text = dt.Rows[0]["Rates"].ToString();
                    txtFeeMonth.Text = dt.Rows[0]["Service"].ToString();
                    txtVat.Text = dt.Rows[0]["Vat"].ToString();
                    txtDiscount.Text = "0.00";
                }
                else
                {
                    txtDiscount.Text = "0.00";
                }
            }
            catch
            {

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
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void txtRecId_TextChanged(object sender, EventArgs e)
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
            Parameter = new string[,]
            {
                {"@InvoiceDate", Date.GetDate(dtp : dtDateBefor, Format : 4, Language : Cul.EN)},
            };

            if (Type == "Before")
            {
                db.Get("Store.SelectInvoiceWater" + "Before", Parameter, out strErr, out dt);

                dtBefore = dt.Rows.Count;

                if (dtBefore == 1)
                {
                    txtNumberBefor.Text = dt.Rows[0]["Numeral"].ToString();
                }
                else
                {
                    txtNumberBefor.Text = "";
                }
            }
            else if (Type == "Overdue")
            {
                db.Get("Store.SelectInvoiceWater" + "Overdue", Parameter, out strErr, out dts);

                dtOverdue = dts.Rows.Count;

                if (dtOverdue == 1)
                {
                    txtMoneyOverdue.Text = dts.Rows[0]["PayAll"].ToString();
                    txtMonthOverdue.Text = "1";
                }
                else
                {
                    txtMoneyOverdue.Text = "0.00";
                    txtMonthOverdue.Text = "0";
                }
            }
            else
            {
                DateTimePicker dpBefore = new DateTimePicker();
                dpBefore.Value = dtDate.Value.AddMonths(-1);

                Parameter = new string[,]
                {
                    {"@InvoiceDate", Date.GetDate(dtp : dpBefore, Format : 4, Language : Cul.EN)},
                };

                db.Get("Store.SelectInvoiceWater" + "Before", Parameter, out strErr, out dt);
                db.Get("Store.SelectInvoiceWater" + "Overdue", Parameter, out strErr, out dts);

                dtOverdue = dts.Rows.Count;
                dtBefore = dt.Rows.Count;

                if (txtNumberBefor.Text == "")
                {
                    if (dtBefore == 1)
                    {
                        txtNumberBefor.Text = dt.Rows[0]["Numeral"].ToString();
                    }
                    else
                    {
                        txtNumberBefor.Text = "";
                    }
                }

                if (dtOverdue == 1)
                {
                    txtMoneyOverdue.Text = dts.Rows[0]["PayAll"].ToString();
                    txtMonthOverdue.Text = "1";
                }
                else
                {
                    txtMoneyOverdue.Text = "0.00";
                    txtMonthOverdue.Text = "0";
                }
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtScan.Text != "" && txtScan.Text != "\r\n\r\n\r\n\r\n")
                {
                    strBarcode = txtScan.Text;
                    Fn.getWaterData(txtScan.Text);
                    CountEnter = 0;

                    foreach (WaterModel wm in GlobalVar.WaterDataList)
                    {
                        txtInvoiceNumber.Text = wm.ReceiptId;
                        dtDate.Value = wm.ReadDate;
                        dtTime.Text = "00:00:00";
                        dtDateNow.Value = wm.ReadDate;
                        dtPay.Value = wm.PayDate;
                        txtUnit.Text = Convert.ToString(wm.Unit);
                        txtMoneyPay.Text = Convert.ToString(wm.Amount);
                        break;
                    }
                }

                txtScan.Clear();
                SearchData(true);
                GetBill("All");
            }
            catch (Exception ex)
            {
                NewData = true;
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void txtScan_TextChanged(object sender, EventArgs e)
        {
            if (CountEnter == 4)
            {
                btnScan_Click(sender, e);
                txtScan.Clear();
                txtScan.Focus();
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
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void ShowData(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count == 1)
                {
                    cbbUser.SelectedValue = dt.Rows[0]["UserCode"].ToString();
                    cbbVersion.SelectedValue = dt.Rows[0]["Version"].ToString();
                    txtUnit.Text = dt.Rows[0]["Unit"].ToString();

                    txtCode.Text = dt.Rows[0]["Code"].ToString();
                    string strBarcode = dt.Rows[0]["Code"].ToString();
                    pbQrcode.Image = Barcode.QRCode(strBarcode, Color.Black, Color.White, "Q", 3, false);

                    txtInvoiceNumber.Text = dt.Rows[0]["InvoiceNumber"].ToString();

                    cbbMonth.SelectedValue = dt.Rows[0]["InvoiceMonth"].ToString();
                    cbbYear.SelectedValue = dt.Rows[0]["InvoiceYear"].ToString();

                    dtDate.Text = dt.Rows[0]["InvoiceDate"].ToString();
                    dtTime.Text = dt.Rows[0]["Time"].ToString();

                    dtDateBefor.Text = dt.Rows[0]["DateBefore"].ToString();
                    txtNumberBefor.Text = dt.Rows[0]["NumeralBefore"].ToString();

                    dtDateNow.Text = dt.Rows[0]["DateBill"].ToString();
                    txtNumberNow.Text = dt.Rows[0]["Numeral"].ToString();

                    txtDipValue.Text = dt.Rows[0]["RawValue"].ToString();
                    txtDipMoney.Text = dt.Rows[0]["Raw"].ToString();
                    txtMoneyWater.Text = dt.Rows[0]["Money"].ToString();
                    txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
                    txtMoney.Text = dt.Rows[0]["Total"].ToString();
                    txtMoneyVat.Text = dt.Rows[0]["Vat"].ToString();
                    txtMoneyAll.Text = dt.Rows[0]["Pay"].ToString();

                    dtPay.Text = dt.Rows[0]["PayDate"].ToString();
                    cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                    cbbPayment.SelectedValue = dt.Rows[0]["Payment"].ToString();
                    dtDatePayment.Text = dt.Rows[0]["PaymentDate"].ToString();
                    txtMoneyPay.Text = dt.Rows[0]["PayAll"].ToString();
                }
            }
            catch (Exception)
            {
                List.GetList(cbbUser, "", "WaterId");
                cbbUser.SelectedValue = dt.Rows[0]["MsWaterCode"].ToString();
            }
        }

        private void txtMoneyOverdue_TextChanged(object sender, EventArgs e)
        {
            if (txtMoneyOverdue.Text != "" && txtMoneyOverdue.Text != "0.00")
            {
                CalWaterValue();
            }
        }

        private void txtScan_KeyDown(object sender, KeyEventArgs e)
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
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
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
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        public void SearchData(bool Search)
        {
            txtInvoiceNumber.Enabled = true;
            Selected = false;
            dtBill = new DataTable();

            Parameter = new string[,]
            {
                {"@Id", ""},
                {"@Code", Search? txtCode.Text : ""},
                {"@User", ""},
                {"@UserCode", Search? Fn.getComboBoxValue(cbbUser) : "0"},
                {"@Version", Search? Fn.getComboBoxValue(cbbVersion) : "0"},
                {"@InvoiceNumber", Search? txtInvoiceNumber.Text : ""},
                {"@InvoiceDate", ""},
                {"@DateBill", ""},
                {"@DateBefore", ""},
                {"@Numeral", Search? Fn.MoveNumberStringComma(txtNumberNow.Text) : ""},
                {"@NumeralBefore", Search? Fn.MoveNumberStringComma(txtNumberBefor.Text) : ""},
                {"@Time", ""},
                {"@InvoiceMonth", "0"},
                {"@InvoiceYear", "0"},
                {"@Unit", Search? Fn.MoveNumberStringComma(txtUnit.Text) : ""},
                {"@Raw", Search? Fn.MoveNumberStringComma(txtDipMoney.Text) : ""},
                {"@Money", Search? Fn.MoveNumberStringComma(txtMoneyWater.Text) : ""},
                {"@Total", Search? Fn.MoveNumberStringComma(txtMoney.Text) : ""},
                {"@Vat", Search? Fn.MoveNumberStringComma(txtMoneyVat.Text) : ""},
                {"@Pay", Search? Fn.MoveNumberStringComma(txtMoneyAll.Text) : ""},
                {"@PayAll", Search? Fn.MoveNumberStringComma(txtMoneyPay.Text) : ""},
                {"@Payment", Search? Fn.getComboBoxValue(cbbPayment) : "0"},
                {"@Discount", Search? Fn.MoveNumberStringComma(txtDiscount.Text) : ""},
                {"@PayDate", ""},
                {"@PaymentDate", ""},
                {"@Status", Search? Fn.getComboBoxValue(cbbStatus) : "0"},
                {"@Barcode", ""},
                {"@Remark", ""},
            };

            db.Get("Store.SelectInvoiceWater", Parameter, out strErr, out dt);
            GetDataGrid(dt);
            ShowData(dt);

            if (txtMonthOverdue.Text == "")
            {
                GetBill("Overdue");
            }

            txtScan.Focus();
        }

        public void Clear()
        {
            try
            {
                cbbStatus.SelectedValue = 0;
                cbbUser.SelectedValue = 0;
                cbbVersion.SelectedValue = 0;
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

                int year = Convert.ToInt32(DateTime.Now.ToString("yyyy", Fn.SetFormatDate(Cul.EN)));
                int month = Convert.ToInt32(DateTime.Now.ToString("MM"));

                dtTime.Value = new DateTime(2020, 02, 02, 0, 0, 0);
                dtDate.Value = new DateTime(year, month, 12);
                dtDateNow.Value = dtDate.Value;
                dtDateBefor.Value = dtDate.Value.AddMonths(-1);
                dtPay.Value = new DateTime(year, month, 19);

                pbQrcode.Image = null;

                SearchData(false);
                CountEnter = 0;
                txtScan.Text = "";
                dataGridView.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void cbbVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbVersion.SelectedValue.ToString() != "0" && cbbVersion.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                GetDataMaster(Fn.getComboBoxValue(cbbVersion));
                btnScan.Enabled = true;
                txtScan.Enabled = true;
                txtInvoiceNumber.Enabled = true;
                dtTime.Value = new DateTime(2020, 02, 02, 0, 0, 0);
                txtScan.Focus();
            }
        }

        private void dtDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int theMonth = Convert.ToInt32(dtDate.Value.ToString("MM"));
                int theYear = Convert.ToInt32(dtDate.Value.ToString("yyyy"));

                if (theYear <= 2500)
                {
                    theYear = theYear + 543;
                }

                cbbMonth.SelectedIndex = theMonth;
                cbbYear.SelectedValue = theYear;
                dtDateBefor.Value = dtDate.Value.AddMonths(-1);
                dtDateNow.Value = dtDate.Value;
            }
            catch
            {

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

                dtPay.Value = dtDate.Value.AddDays(7);
                GetBill("All");
            }
        }

        private void FrmMWASA00_KeyDown(object sender, KeyEventArgs e)
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

            if (keyCode == "Ctrl+X")
            {
                btnExit_Click(sender, e);
            }

            if (keyCode == "Alt+F")
            {
                btnSearch_Click(sender, e);
            }

            if (keyCode == "Alt+C")
            {
                btnClear_Click(sender, e);
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
                    SearchData(true);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void txtUnit_Leave(object sender, EventArgs e)
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
            if (!Fn.IsCharacter(e.KeyChar, CharType.Barcode))
            {
                e.Handled = true;
            }
        }
    }
}