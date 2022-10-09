using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Database;
using SANSANG.Constant;
using SANSANG.Utilites.App.Forms;
using SANSANG.Utilites.App.Global;
using SANSANG.Utilites.App.Model;

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

        private DataTable dt = new DataTable();
        private DataListConstant DataList = new DataListConstant();
        private clsDelete Delete = new clsDelete();
        private clsEdit Edit = new clsEdit();
        private clsInsert Insert = new clsInsert();
        private clsFunction Function = new clsFunction();
        private clsDate Dates = new clsDate();
        private clsBarcode Barcode = new clsBarcode();
        private clsMessage Message = new clsMessage();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();
        private CultureConstant Culture = new CultureConstant();
        private TableConstant Table = new TableConstant();
        private CharacterConstant Character = new CharacterConstant();
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(25);
        private Timer Timer = new Timer();
        public string[,] Parameter = new string[,] { };

        public string QRCode = "";
        private bool Display = false;
        public int CountEnter = 0;

        public string qrHeader;
        public string qrLine2;
        public string qrLine3;
        public string qrLine4;
        public string strQR;

        public FrmElectricitys(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
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
            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "1"));
            List.GetLists(cbbPayment, DataList.MoneyId);

            //List.GetList(cbbUser, "Address", "Electricity");
            //List.GetList(cbbVersion, "Version", "Electricity");

            List.GetMonthList(cbbMonth);
            List.GetYearList(cbbYear);

            //Clear();
            Timer.Stop();
        }

        public void ShowDataGrid(DataTable dt)
        {
            try
            {
                if (Function.GetRows(dt) == 0)
                {
                    dataGridView.DataSource = null;
                    txtCount.Text = Function.ShowNumberOfData(0);
                }
                else
                {
                    dataGridView.DataSource = null;
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "SNo", "Dates", "MeterNow", "Unit", "Amount", "StatusNameTh", "Code");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
                    DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                    Function.showGridViewFormatFromStore(dtGrid, dataGridView,
                          "ลำดับ", 30, true, mc, mc
                        , "วันที่จดเลขอ่าน", 80, true, ml, ml
                        , "เลขในมาตร", 100, true, mc, mc
                        , "จำนวนหน่วย", 100, true, mc, mc
                        , "จำนวนเงิน", 100, true, mr, mr
                        , "สถานะการชำระ", 150, true, mc, mc
                        , "", 0, false, mc, mc
                        );

                    txtCount.Text = Function.ShowNumberOfData(dt.Rows.Count);
                    dataGridView.Focus();
                }
            }
            catch
            {

            }
        }

        private void Exit(object sender, EventArgs e)
        {
            Close();
        }

        private void ClearData(object sender, EventArgs e)
        {
            Clear();
        }

        private void AddData(object sender, EventArgs e)
        {
            if (txtInvoiceNumber.Text != "" && Function.getComboBoxValue(cbbStatus) != "0")
            {
                if (!Function.IsDuplicates("InvoiceElectricity", Function.getComboBoxValue(cbbMonth), Function.getComboBoxValue(cbbYear), Detail: "Invoice " + cbbMonth.SelectedValue + "/" + cbbYear.SelectedValue))
                {
                    txtCode.Text = txtCode.Text = Function.GetCodes("109", "", "Generated");
                    pbQrcode.Image = Barcode.QRCode(txtCode.Text, Color.Black, Color.White, "Q", 3, false);
                    strQR = qrHeader + "\r\n" + qrLine2 + "\r\n" + qrLine3 + "\r\n" + qrLine4 + "\r\n";

                    Parameter = new string[,]
                    {
                        { "@Id", ""},
                        { "@Code", txtCode.Text},
                        { "@User", UserId},
                        { "@UserCode", Function.getComboBoxValue(cbbUser)},
                        { "@Version", Function.getComboBoxValue(cbbVersion)},
                        { "@InvoiceNumber", txtInvoiceNumber.Text},
                        { "@InvoiceDate", Dates.GetDate(dtp : dtDate, Format : 4)},
                        { "@InvoiceTime", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                        { "@InvoiceMonth", Function.getComboBoxValue(cbbMonth)},
                        { "@InvoiceYear", Function.getComboBoxValue(cbbYear)},
                        { "@MeterNow", txtNumberNow.Text},
                        { "@MeterBefore", txtNumberBefor.Text},
                        { "@Unit", txtUnit.Text},
                        { "@Raw", Function.MoveNumberStringComma(txtRaw.Text)},
                        { "@Ft", Function.MoveNumberStringComma(txtMoneyFt.Text)},
                        { "@Discount", Function.MoveNumberStringComma(txtDiscount.Text)},
                        { "@DiscountDetail", txtDiscountDetail.Text},
                        { "@Sum", Function.MoveNumberStringComma(txtMoney.Text)},
                        { "@Vat", Function.MoveNumberStringComma(txtMoneyVat.Text)},
                        { "@SumAll", Function.MoveNumberStringComma(txtMoneyAll.Text)},
                        { "@Status", Function.getComboBoxValue(cbbStatus)},
                        { "@FileLocation", ""},
                        { "@PayAll", Function.MoveNumberStringComma(txtMoneyPay.Text)},
                        { "@Payment", Function.getComboBoxValue(cbbPayment)},
                        { "@EndDate", Dates.GetDate(dtp : dtPayEnd, Format : 4)},
                        { "@PayDate", Dates.GetDate(dtp : dtPay, Format : 4)},
                        { "@Barcode", strQR},
                        { "@Remark", txtRemark.Text},
                    };

                    if (Insert.Add(AppCode, AppName, UserId, "Store.InsertInvoiceElectricity", Parameter, txtCode.Text, "ใบแจ้งค่าไฟฟ้า " + cbbMonth.Text + " " + cbbYear.Text))
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

        private void CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                    DataTable dt = new DataTable();

                    Parameter = new string[,]
                    {
                        { "@Id", ""},
                        { "@Code", row.Cells["Code"].Value.ToString()},
                        { "@User", ""},
                        { "@UserCode", "0"},
                        { "@Version", "0"},
                        { "@InvoiceNumber", ""},
                        { "@InvoiceDate", ""},
                        { "@InvoiceTime", ""},
                        { "@InvoiceMonth", "0"},
                        { "@InvoiceYear", "0"},
                        { "@MeterNow", ""},
                        { "@MeterBefore", ""},
                        { "@Unit", ""},
                        { "@Raw", ""},
                        { "@Ft", ""},
                        { "@Discount", ""},
                        { "@DiscountDetail", ""},
                        { "@Sum", ""},
                        { "@Vat", ""},
                        { "@SumAll", ""},
                        { "@Status", "0"},
                        { "@FileLocation", ""},
                        { "@PayAll", ""},
                        { "@Payment", "0"},
                        { "@EndDate", ""},
                        { "@PayDate", ""},
                        { "@Barcode", ""},
                        { "@Remark", ""},
                    };

                    db.Get("Store.SelectInvoiceElectricity", Parameter, out Error, out dt);
                    SetValue(dt);
                }
            }
            catch
            {

            }
        }

        private void DeleteData(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                if (Delete.Drop(AppCode, AppName, UserId, 0, Table.InvoiceElectricity, txtCode, "ใบแจ้งค่าไฟฟ้า " + cbbMonth.Text + " " + cbbYear.Text))
                {
                    Clear();
                }
            }
        }

        public void SetFormat(TextBox tb)
        {
            try
            {
                double num = Convert.ToDouble(tb.Text);
                tb.Text = string.Format("{0:n}", num);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void EditData(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                Parameter = new string[,]
                {
                    { "@Id", ""},
                    { "@Code", txtCode.Text},
                    { "@User", UserId},
                    { "@UserCode", Function.getComboBoxValue(cbbUser)},
                    { "@Version", Function.getComboBoxValue(cbbVersion)},
                    { "@InvoiceNumber", txtInvoiceNumber.Text},
                    { "@InvoiceDate", Dates.GetDate(dtp : dtDate, Format : 4)},
                    { "@InvoiceTime", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                    { "@InvoiceMonth", Function.getComboBoxValue(cbbMonth)},
                    { "@InvoiceYear", Function.getComboBoxValue(cbbYear)},
                    { "@MeterNow", txtNumberNow.Text},
                    { "@MeterBefore", txtNumberBefor.Text},
                    { "@Unit", txtUnit.Text},
                    { "@Raw", Function.MoveNumberStringComma(txtRaw.Text)},
                    { "@Ft", Function.MoveNumberStringComma(txtMoneyFt.Text)},
                    { "@Discount", Function.MoveNumberStringComma(txtDiscount.Text)},
                    { "@DiscountDetail", txtDiscountDetail.Text},
                    { "@Sum", Function.MoveNumberStringComma(txtMoney.Text)},
                    { "@Vat", Function.MoveNumberStringComma(txtMoneyVat.Text)},
                    { "@SumAll", Function.MoveNumberStringComma(txtMoneyAll.Text)},
                    { "@Status", Function.getComboBoxValue(cbbStatus)},
                    { "@FileLocation", ""},
                    { "@PayAll", Function.MoveNumberStringComma(txtMoneyPay.Text)},
                    { "@Payment", Function.getComboBoxValue(cbbPayment)},
                    { "@EndDate", Dates.GetDate(dtp : dtPayEnd, Format : 4)},
                    { "@PayDate", Dates.GetDate(dtp : dtPay, Format : 4)},
                    { "@Barcode", strQR},
                    { "@Remark", txtRemark.Text},
                };

                if (Edit.Update(AppCode, AppName, UserId, "Store.UpdateInvoiceElectricity", Parameter, txtCode.Text, "ใบแจ้งค่าไฟฟ้า " + cbbMonth.Text + " " + cbbYear.Text))
                {
                    Clear();
                }
            }
        }

        public void Search(bool Search)
        {
            Parameter = new string[,]
            {
                    {"@Id", ""},
                    {"@Code", Search? txtCode.Text : ""},
                    {"@User", ""},
                    {"@UserCode", Search? Function.getComboBoxValue(cbbUser) : "0"},
                    {"@Version", Search? Function.getComboBoxValue(cbbVersion) : "0"},
                    {"@InvoiceNumber ", Search? txtInvoiceNumber.Text : ""},
                    {"@InvoiceDate", ""},
                    {"@InvoiceTime", ""},
                    {"@InvoiceMonth", Search? Function.getComboBoxValue(cbbMonth) : "0"},
                    {"@InvoiceYear", Search? Function.getComboBoxValue(cbbYear) : "0"},
                    {"@MeterNow", Search? txtNumberNow.Text : ""},
                    {"@MeterBefore", Search? txtNumberBefor.Text : ""},
                    {"@Unit", Search? txtUnit.Text : ""},
                    {"@Raw", Search? Function.SplitString(txtRaw.Text, ",", "") : ""},
                    {"@Ft", Search? Function.SplitString(txtMoneyFt.Text, ",", "") : ""},
                    {"@Discount", Search? Function.SplitString(txtDiscount.Text, ",", "") : ""},
                    {"@DiscountDetail", Search? txtDiscountDetail.Text : ""},
                    {"@Sum", Search? Function.SplitString(txtMoney.Text, ",", "") : ""},
                    {"@Vat", Search? Function.SplitString(txtMoneyVat.Text, ",", "") : ""},
                    {"@SumAll", Search? Function.SplitString(txtMoneyAll.Text, ",", "") : ""},
                    {"@Status", Search? Function.getComboBoxValue(cbbStatus) : "0"},
                    {"@FileLocation", ""},
                    {"@PayAll", Search? Function.SplitString(txtMoneyPay.Text, ",", "") : ""},
                    {"@Payment", Search?  Function.getComboBoxValue(cbbPayment)  : "0"},
                    {"@EndDate", ""},
                    {"@PayDate", ""},
                    {"@Barcode", ""},
                    {"@Remark", Search? txtRemark.Text : ""},
            };

            db.Get("Store.SelectInvoiceElectricity", Parameter, out Error, out dt);
            ShowDataGrid(dt);

            if (Display)
            {
                SetValue(dt);
            }

        }

        private void SearchData(object sender, EventArgs e)
        {
            Search(true);
        }

        public void GetDataMaster(string Code)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", Code},
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

                db.Get("Store.SelectVersionElectricity", Parameter, out Error, out dt);

                if (Error == null && dt.Rows.Count > 0)
                {
                    txtPerUnit.Text = dt.Rows[0]["Rates"].ToString();
                    txtFee.Text = dt.Rows[0]["Service"].ToString();
                    txtFt.Text = dt.Rows[0]["Ft"].ToString();
                    txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
                    txtVat.Text = dt.Rows[0]["Vat"].ToString();
                    txtDay.Text = dt.Rows[0]["DueDate"].ToString();
                }
            }
            catch
            {

            }
        }

        private void txtNumberNow_TextChanged(object sender, EventArgs e)
        {
            if (txtNumberNow.Text != "")
            {
                CalElectValue();
            }
            else
            {
                txtUnit.Text = "";
            }
        }

        public void CalElectValue()
        {
            try
            {
                txtUnit.Text = Convert.ToString(Math.Round(Convert.ToDouble(txtNumberNow.Text) - Convert.ToDouble(txtNumberBefor.Text), 2));
                txtRaw.Text = string.Format("{0:#,##0.00}", Math.Round(Convert.ToDouble(txtUnit.Text) * Convert.ToDouble(txtPerUnit.Text), 2));
                txtMoneyFt.Text = string.Format("{0:#,##0.00}", Math.Round(Convert.ToDouble(txtUnit.Text) * Convert.ToDouble(txtFt.Text), 2));

                txtMoney.Text = string.Format("{0:#,##0.00}", Math.Round((
                                Convert.ToDouble(txtRaw.Text) +
                                Convert.ToDouble(txtFee.Text) +
                                Convert.ToDouble(txtMoneyFt.Text)) -
                                Convert.ToDouble(txtDiscount.Text), 2));

                txtMoneyVat.Text = string.Format("{0:#,##0.00}", Math.Round(((Convert.ToDouble(txtMoney.Text) * (Convert.ToDouble(txtVat.Text)) / 100)), 2));
                txtMoneyAll.Text = string.Format("{0:#,##0.00}", Math.Round(Convert.ToDouble(txtMoney.Text) + Convert.ToDouble(txtMoneyVat.Text), 2));
            }
            catch
            {

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

        private void txtRecId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char.IsNumber(e.KeyChar) || e.KeyChar == 8) || (e.KeyChar == Convert.ToChar(Keys.Enter)))
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    Search(true);
                }
            }
            else
            {
                e.Handled = true;
                return;
            }
        }

        private void txtNumberNow_KeyPress(object sender, KeyPressEventArgs e)
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
                    SearchData(sender, e);
                }
                if (keyCode == "Alt+C")
                {
                    ClearData(sender, e);
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

        private void btnAd_Click(object sender, EventArgs e)
        {
            dtPayEnd.Value = dtPayEnd.Value.AddDays(1);
        }

        private void btnRe_Click(object sender, EventArgs e)
        {
            dtPayEnd.Value = dtPayEnd.Value.AddDays(-1);
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtScan.Text != "" && txtScan.Text != "\r\n\r\n\r\n\r\n")
                {
                    QRCode = txtScan.Text;
                    Function.getElectricData(txtScan.Text);

                    foreach (ElectricModel em in GlobalVar.ElectricDataList)
                    {
                        txtInvoiceNumber.Text = em.ReceiptId;
                        txtUnit.Text = Convert.ToString(em.Unit);
                        txtMoneyPay.Text = Convert.ToString(em.Amount);
                        dtPayEnd.Value = em.PayDate;
                        dtDate.Value = em.ReadDate;
                        qrHeader = em.Header;
                        qrLine2 = em.qrLine2;
                        qrLine3 = em.qrLine3;
                        qrLine4 = em.qrLine4;
                        break;
                    }

                    Search(true);
                }
                else
                {
                    Message.MessageConfirmation("V", "", "QRCode invalid please change language.");
                    var Popup = new FrmMessagesBoxOK(Message.strOperation, Message.strMes, "OK", Message.strImage);
                    Popup.ShowDialog();
                }

                txtScan.Clear();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void txtScan_TextChanged(object sender, EventArgs e)
        {
            if (CountEnter == 4)
            {
                CountEnter = 0;
                Display = true;
                btnScan_Click(sender, e);
            }
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

        private void txtScan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CountEnter++;
            }
        }

        private void txtScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Function.IsCharacter(e.KeyChar, Character.Tracking))
            {
                e.Handled = true;
            }
        }

        private void GetBefore(string Type = "")
        {
            DataTable dtBefore = new DataTable();
            DataTable dtOverdue = new DataTable();

            Parameter = new string[,]
            {
                {"@InvoiceDate", Dates.GetDate(dtp : dtDateBefor, Format : 4, Language : Culture.EN)},
            };

            if (Type == "Before")
            {
                db.Get("Store.SelectInvoiceElectricity" + "Before", Parameter, out Error, out dtBefore);

                if (Function.GetRows(dtBefore) == 1)
                {
                    txtNumberBefor.Text = dtBefore.Rows[0]["MeterNow"].ToString();
                }
                else
                {
                    txtNumberBefor.Text = "";
                }
            }
            else if (Type == "Overdue")
            {
                db.Get("Store.SelectInvoiceElectricity" + "Overdue", Parameter, out Error, out dtOverdue);

                if (Function.GetRows(dtOverdue) == 1)
                {
                    txtMoneyOverdue.Text = dt.Rows[0]["PayAll"].ToString();
                    txtMonthOverdue.Text = "1";
                    SumTotal();
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
                    {"@InvoiceDate", Dates.GetDate(dtp : dpBefore, Format : 4, Language : Culture.EN)},
                };

                db.Get("Store.SelectInvoiceElectricity" + "Before", Parameter, out Error, out dtBefore);
                db.Get("Store.SelectInvoiceElectricity" + "Overdue", Parameter, out Error, out dtOverdue);
     
                if (Function.GetRows(dtBefore) == 1)
                {
                    txtNumberBefor.Text = dt.Rows[0]["MeterNow"].ToString();
                }
                else
                {
                    txtNumberBefor.Text = "";
                }

                if (Function.GetRows(dtOverdue) == 1)
                {
                    txtMoneyOverdue.Text = dtOverdue.Rows[0]["PayAll"].ToString();
                    txtMonthOverdue.Text = "1";
                    SumTotal();
                }
                else
                {
                    txtMoneyOverdue.Text = "0.00";
                    txtMonthOverdue.Text = "0";
                }
            }
        }

        private void txtNumberBefor_TextChanged(object sender, EventArgs e)
        {
            if (txtNumberBefor.Text != "")
            {
                int Num = 0;
                Num = int.Parse(txtNumberBefor.Text) + int.Parse(txtUnit.Text);
                txtNumberNow.Text = Convert.ToString(Num);
            }
        }

        private void txtDisCount_TextChanged(object sender, EventArgs e)
        {
            CalElectValue();
        }

        private void dtTime_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    GetBefore();
                    int theMonth = Convert.ToInt32(dtDate.Value.ToString("MM"));
                    int theYear = Convert.ToInt32(dtDate.Value.ToString("yyyy")) + 543;

                    cbbMonth.SelectedIndex = theMonth;
                    cbbYear.SelectedValue = theYear;
                    dtDateNow.Text = dtDate.Text;
                    dtDateBefor.Value = dtDate.Value.AddMonths(-1);
                    dtPayStart.Value = dtDate.Value.AddDays(1);
                    dtPay.Value = DateTime.Today;
                    cbbStatus.SelectedValue = "NP";
                }
                catch (Exception)
                {
                    txtNumberBefor.Text = "0";
                    txtMoneyOverdue.Text = "0.00";
                    txtMonthOverdue.Text = "0";
                    cbbStatus.SelectedValue = "0";
                }

                if (txtNumberBefor.Text == "")
                {
                    txtNumberBefor.Focus();
                }
                else
                {
                    txtRemark.Focus();
                }
            }
        }

        public void SetValue(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                cbbUser.SelectedValue = dt.Rows[0]["UserCode"].ToString();
                cbbVersion.SelectedValue = dt.Rows[0]["Version"].ToString();
                txtInvoiceNumber.Text = dt.Rows[0]["InvoiceNumber"].ToString();
                dtDate.Text = dt.Rows[0]["InvoiceDate"].ToString();
                dtTime.Text = dt.Rows[0]["InvoiceTime"].ToString();
                cbbMonth.SelectedValue = dt.Rows[0]["InvoiceMonth"].ToString();
                cbbYear.SelectedValue = dt.Rows[0]["InvoiceYear"].ToString();
                dtDateNow.Text = dt.Rows[0]["InvoiceDate"].ToString();
                dtDateBefor.Value = dtDateNow.Value.AddMonths(-1);
                txtUnit.Text = dt.Rows[0]["Unit"].ToString();
                txtNumberBefor.Text = dt.Rows[0]["MeterBefore"].ToString();

                string strBarcode = dt.Rows[0]["Code"].ToString();
                pbQrcode.Image = Barcode.QRCode(strBarcode, Color.Black, Color.White, "Q", 3, false);

                txtCode.Text = dt.Rows[0]["Code"].ToString();
                txtRaw.Text = dt.Rows[0]["Raw"].ToString();

                txtMoneyFt.Text = dt.Rows[0]["Ft"].ToString();
                txtDiscountDetail.Text = dt.Rows[0]["DiscountDetail"].ToString();
                txtDiscount.Text = dt.Rows[0]["Discount"].ToString();

                txtMoney.Text = dt.Rows[0]["Sum"].ToString();
                txtMoneyVat.Text = dt.Rows[0]["Vat"].ToString();
                txtMoneyAll.Text = dt.Rows[0]["SumAll"].ToString();
                txtRemark.Text = dt.Rows[0]["Remark"].ToString();

                txtMoneyPay.Text = dt.Rows[0]["PayAll"].ToString();
                dtPayEnd.Text = dt.Rows[0]["EndDate"].ToString();
                dtPayStart.Value = dtDate.Value.AddDays(+1);

                cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                cbbPayment.SelectedValue = dt.Rows[0]["Payment"].ToString();
                dtPay.Text = dt.Rows[0]["PayDate"].ToString();

                Display = false;
                GetBefore("Overdue");
                dataGridView.Focus();
            }
            else
            {
                Display = false;
                GetBefore();
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
                txtScan.Focus();
            }
        }

        private void txtNumberBefor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtDiscountDetail.Text == "")
                {
                    txtDiscountDetail.Focus();
                }
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

        public void Clear()
        {
            CountEnter = 0;
            QRCode = "";
            qrHeader = "";
            strQR = "";
           
            pbQrcode.Image = null;
            btnScan.Enabled = true;
            txtScan.Enabled = true;
            txtInvoiceNumber.Enabled = false;
            dtDate.Enabled = false;
            dtPay.Enabled = false;
            dtTime.Enabled = false;
            cbbStatus.Enabled = false;
            Display = false;

            int Year = Convert.ToInt32(DateTime.Now.ToString("yyyy", Function.SetFormatDate(Culture.EN)));
            int Month = Convert.ToInt32(DateTime.Now.ToString("MM"));

            dtTime.Value = new DateTime(2020, 02, 02, 0, 0, 0);
            dtDate.Value = new DateTime(Year, Month, 18);
            dtDateNow.Value = dtDate.Value;
            dtDateBefor.Value = dtDate.Value.AddMonths(-1);
            dtPayStart.Value = dtDate.Value.AddDays(1);
            dtPayEnd.Value = dtPayStart.Value.AddDays(10);
            dtPay.Value = DateTime.Now;
            
            txtMoneyOverdue.Text = "0.00";
            txtMonthOverdue.Text = "0";

            Search(false);
        }

        public string GetDetails()
        {
            return txtCode.Text + " | " + txtRemark.Text + " (฿" + txtUnit.Text + ")";
        }
    }
}