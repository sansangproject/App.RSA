using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Database;
using SANSANG.Constant;
using System.Drawing;
using System.Diagnostics;
using System.Web.Services.Description;
using System.ComponentModel;
using System.Windows.Media.TextFormatting;
using static QRCoder.PayloadGenerator;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic;
using System.Globalization;
using DevComponents.DotNetBar;
using System.Windows.Media;
using static Telerik.WinControls.UI.ValueMapper;
using static SANSANG.Class.clsFunction;

namespace SANSANG
{
    public partial class FrmGolds : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "SAVGD00";
        public string AppName = "FrmGolds";
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

        public FrmGolds(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
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

            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "1"));

            List.GetList(cbbMonth, DataList.Workdays);
            List.GetList(cbbAccount, DataList.Account);
            List.GetList(cbbMember, DataList.Members);

            Clear();
            gbForm.Enabled = true;
            Timer.Stop();
        }

        public void Clear()
        {
            Function.ClearAll(gbForm);

            pb_Date_True.Visible = false;
            pb_Date_False.Visible = true;
            cb_Date.Checked = false;

            btnCopy.Visible = false;

            dtDate.Value = DateTime.Now;
            dtTime.Value = Convert.ToDateTime("16:00:00");
            dtTime.Format = DateTimePickerFormat.Time;
            dtTime.ShowUpDown = true;

            txtSumGold.Text = "0.00";
            txtSumMoney.Text = "0.00";

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
                {"@MemberId", Search? Function.GetComboId(cbbMember) : "0"},
                {"@AccountId", Search? Function.GetComboId(cbbAccount) : "0"},
                {"@WorkdayId", Search? Function.GetComboId(cbbMonth) : "0"},
                {"@Date", Search? cb_Date.Checked? Date.GetDate(dtp: dtDate, Format: 4) : "" : ""},
                {"@List", ""},
                {"@Days", "0"},
                {"@Month", "0"},
                {"@Year", "0"},
                {"@Time", ""},
                {"@GoldPriceSell", Search? Function.RemoveComma(txtGoldPriceSell.Text) : ""},
                {"@GoldPriceBuy", Search? Function.RemoveComma(txtGoldPriceBuy.Text) : ""},
                {"@GoldReceive", Search? txtReceive.Text : ""},
                {"@Detail", Search? txtDetail.Text : ""},
                {"@Remark", Search? txtRemark.Text : ""},
            };

            string Condition = Function.ShowConditons(GetCondition());
            lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;

            db.Gets(Store.ManageGold, Parameter, out Error, out ds);
            ShowGridView(ds.Tables[0]);
            SummaryGold(ds.Tables[1]);
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";

                strCondition += txtCode.Text != "" ? ", รหัสอ้างอิง: " + txtCode.Text : "";
                strCondition += Function.GetComboId(cbbMember) != "0" ? ", สมาชิก: " + cbbMember.Text : "";
                strCondition += Function.GetComboId(cbbAccount) != "0" ? ", บัญชี: " + cbbAccount.Text : "";
                strCondition += Function.GetComboId(cbbMonth) != "0" ? ", ประจำเดือน: " + cbbMonth.Text : "";
                strCondition += txtGoldPriceSell.Text != "" ? ", ราคาทอง (ขาย): " + txtGoldPriceSell.Text : "";
                strCondition += txtGoldPriceBuy.Text != "" ? ", ราคาทอง (ซื้อ): " + txtGoldPriceBuy.Text : "";
                strCondition += Function.GetComboId(cbbStatus) != "0" ? ", สถานะ: " + cbbStatus.Text : "";
                strCondition += txtDetail.Text != "" ? ", รายละเอียด: " + txtDetail.Text : "";
                strCondition += txtRemark.Text != "" ? ", หมายเหตุ: " + txtRemark.Text : "";

                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return "";
            }
        }

        public void ShowGridView(DataTable dt)
        {
            try
            {
                GridView.DataSource = null;

                if (Function.GetRows(dt) > 0)
                {
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "SNo", "Dates", "WorkingDay", "GoldPriceBuys", "GoldPriceSells", "GoldReceive", "Id");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                    Function.showGridViewFormatFromStore(dtGrid, GridView,
                          "ลำดับ", 50, true, mc, mc
                        , "วันที่", 80, true, mc, mc
                        , "วันทำการ (วัน)", 50, true, mc, mc
                        , "ราคาซื้อ (บาท)", 100, true, mc, mr
                        , "ราคาขาย (บาท)", 100, true, mc, mr
                        , "ทองคำ (กรัม)", 150, true, mc, mr
                        , "", 0, false, mc, mc
                        );

                    txtCount.Text = Function.ShowNumberOfData(dt.Rows.Count);
                    GridView.Focus();
                }
                else
                {
                    txtCount.Text = Function.ShowNumberOfData(0);
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

        private void ClearData(object sender, EventArgs e)
        {
            Clear();
        }

        private void CellContentClick(object sender, DataGridViewCellEventArgs e)
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
                    {"@MemberId", "0"},
                    {"@AccountId", "0"},
                    {"@WorkdayId", "0"},
                    {"@Date", ""},
                    {"@List", ""},
                    {"@Days", "0"},
                    {"@Month", "0"},
                    {"@Year", "0"},
                    {"@Time", ""},
                    {"@GoldPriceSell", ""},
                    {"@GoldPriceBuy", ""},
                    {"@GoldReceive", ""},
                    {"@Detail", ""},
                    {"@Remark", ""},
                };

                db.Get(Store.ManageGold, Parameter, out Error, out dt);
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

                    cbbMonth.SelectedValue = dt.Rows[0]["WorkdayId"].ToString();
                    cbbMember.SelectedValue = dt.Rows[0]["MemberId"].ToString();
                    cbbAccount.SelectedValue = dt.Rows[0]["AccountId"].ToString();
                    
                    txtWorkday.Text = dt.Rows[0]["WorkingDay"].ToString();
                    txtAmount.Text = dt.Rows[0]["AmountPerDay"].ToString();

                    double Sell = Convert.ToDouble(dt.Rows[0]["GoldPriceSells"].ToString());
                    txtGoldPriceSell.Text = string.Format("{0:n}", Sell);

                    double Buy = Convert.ToDouble(dt.Rows[0]["GoldPriceBuys"].ToString());
                    txtGoldPriceBuy.Text = string.Format("{0:n}", Buy);

                    dtDate.Text = dt.Rows[0]["Date"].ToString();
                    dtTime.Text = dt.Rows[0]["Time"].ToString();

                    txtDetail.Text = dt.Rows[0]["Detail"].ToString();
                    txtRemark.Text = dt.Rows[0]["Remark"].ToString();

                    txtReceive.Text = dt.Rows[0]["GoldReceive"].ToString();
                    cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                    btnCopy.Visible = true;
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
                ThaiBuddhistCalendar ThaiCalendar = new ThaiBuddhistCalendar();

                DateTime Dates = dtDate.Value;
                DateTime ThaiDate = new DateTime(ThaiCalendar.GetYear(Dates), ThaiCalendar.GetMonth(Dates), Dates.Day);
                string Years = ThaiDate.ToString("yyyy");
                string Months = ThaiDate.ToString("MM");
                string Days = Dates.ToString("dd");

                string Lists = string.Concat(Years, Months);

                if (!string.IsNullOrEmpty(txtReceive.Text))
                {
                    if (!Function.IsDuplicates(Table.Golds, Date.GetDate(dtp: dtDate, Format: 4), Function.RemoveComma(txtGoldPriceBuy.Text), Function.RemoveComma(txtGoldPriceSell.Text), (txtReceive.Text), Detail: GetDetails()))
                    {
                        txtCode.Text = Function.GetCodes(Table.GoldId, "", "Generated");

                        Parameter = new string[,]
                        {
                            {"@Id", ""},
                            {"@Code", txtCode.Text},
                            {"@Status", Function.GetComboId(cbbStatus)},
                            {"@User", UserId},
                            {"@IsActive", "1"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@MemberId", Function.GetComboId(cbbMember)},
                            {"@AccountId", Function.GetComboId(cbbAccount)},
                            {"@WorkdayId", Function.GetComboId(cbbMonth)},
                            {"@Date", Date.GetDate(dtp : dtDate, Format : 4)},
                            {"@List", Lists},
                            {"@Days", Days},
                            {"@Month", Months},
                            {"@Year", Years},
                            {"@Time", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                            {"@GoldPriceSell", Function.RemoveComma(txtGoldPriceSell.Text)},
                            {"@GoldPriceBuy", Function.RemoveComma(txtGoldPriceBuy.Text)},
                            {"@GoldReceive", txtReceive.Text},
                            {"@Detail", txtDetail.Text},
                            {"@Remark", txtRemark.Text},
                         };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageGold, Parameter, txtCode.Text, Details: GetDetails()))
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
                ThaiBuddhistCalendar ThaiCalendar = new ThaiBuddhistCalendar();

                DateTime Dates = dtDate.Value;
                DateTime ThaiDate = new DateTime(ThaiCalendar.GetYear(Dates), ThaiCalendar.GetMonth(Dates), Dates.Day);
                string Years = ThaiDate.ToString("yyyy");
                string Months = ThaiDate.ToString("MM");
                string Days = Dates.ToString("dd");

                string Lists = string.Concat(Years, Months);

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
                        {"@Operation", Operation.UpdateAbbr},
                        {"@MemberId", Function.GetComboId(cbbMember)},
                        {"@AccountId", Function.GetComboId(cbbAccount)},
                        {"@WorkdayId", Function.GetComboId(cbbMonth)},
                        {"@Date", Date.GetDate(dtp : dtDate, Format : 4)},
                        {"@List", Lists},
                        {"@Days", Days},
                        {"@Month", Months},
                        {"@Year", Years},
                        {"@Time", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                        {"@GoldPriceSell", Function.RemoveComma(txtGoldPriceSell.Text)},
                        {"@GoldPriceBuy", Function.RemoveComma(txtGoldPriceBuy.Text)},
                        {"@GoldReceive", txtReceive.Text},
                        {"@Detail", txtDetail.Text},
                        {"@Remark", txtRemark.Text},
                    };

                    if (Edit.Update(AppCode, AppName, UserId, Store.ManageGold, Parameter, txtCode.Text, Details: GetDetails()))
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
                if (Delete.Drop(AppCode, AppName, UserId, 0, Table.Golds, txtCode, Details: GetDetails(), true))
                {
                    Clear();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void SearchData(object sender, EventArgs e)
        {
            Search(true);
        }

        private void Ticker(object sender, EventArgs e)
        {
            Helper.CheckboxTicker(sender, this);
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
                    Form Frm = (Form)sender;

                    if (Frm.ActiveControl.Text == txtGoldPriceSell.Text)
                    {
                        CalculateGoldReceive();
                    }
                    else
                    {
                        Search(true);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public void CopyCode(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                try
                {
                    Clipboard.SetText(txtCode.Text);
                }
                catch (Exception)
                {

                }
            }
        }

        private void SummaryGold(DataTable Data)
        {
            double GoldTotal = 0;
            double MoneyTotal = 0;

            if (string.IsNullOrEmpty(Error) && Function.GetRows(Data) > 0)
            {
                GoldTotal = Convert.ToDouble(Data.Rows[0]["GoldTotal"].ToString());
                MoneyTotal = Convert.ToDouble(Data.Rows[0]["MoneyTotal"].ToString());
            }

            txtSumMoney.Text = string.Format("{0:#,##0.00}", MoneyTotal);
            var Number = (string.Format("{0:#,##0.0000}", GoldTotal));
            txtSumGold.Text = Function.FillFromRight(Number, 8);
        }

        public string GetDetails()
        {
            return dtDate.Text + " (฿" + txtReceive.Text + ")";
        }

        private void txtReceive_TextChanged(object sender, EventArgs e)
        {
            if (txtReceive.Text != "")
            {
                cbbStatus.SelectedValue = 1004;
            }
        }

        private void cbbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetWorkday();
        }

        public void GetWorkday()
        {
            try
            {
                if (Function.GetComboId(cbbMonth) != "0")
                {
                    Parameter = new string[,]
                    {
                        {"@Id", Function.GetComboId(cbbMonth)},
                        {"@Code", ""},
                        {"@Status", "0"},
                        {"@User", ""},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.SelectAbbr},
                        {"@FirstDay", ""},
                        {"@LastDay", ""},
                        {"@PaymentDay", ""},
                        {"@Days", "0"},
                        {"@Month", "0"},
                        {"@Year", "0"},
                        {"@SavingPerMonth", "0"},
                        {"@AmountPerDay", "0"},
                    };

                    db.Gets(Store.ManageWorkday, Parameter, out Error, out ds);

                    txtWorkday.Text = ds.Tables[0].Rows[0]["Days"].ToString();
                    txtAmount.Text = ds.Tables[0].Rows[0]["AmountPerDay"].ToString();
                    txtGoldPriceBuy.Focus();
                }
                else
                {
                    txtWorkday.Text = "";
                    txtAmount.Text = "";
                    txtGoldPriceBuy.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public void GetGoldReceive()
        {
            try
            {
                double amountPerDay = Convert.ToDouble(txtAmount.Text);
                double priceSell = Convert.ToDouble(txtGoldPriceSell.Text);
                double goldReceive = (amountPerDay * 1) / priceSell;

                string goldReceives = string.Format("{0:#,##0.0000000000}", goldReceive);
                txtReceive.Text = goldReceives.ToString().Substring(0, 8);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void txtGoldPriceSell_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtGoldPriceSell.Text))
            {
                txtReceive.Text = "";
            }
            else
            {
                CalculateGoldReceive();
            }
        }

        private void txtGoldPriceBuy_Leave(object sender, EventArgs e)
        {
            try
            {
                double num = Convert.ToDouble(txtGoldPriceBuy.Text);
                txtGoldPriceBuy.Text = String.Format("{0:n}", num);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void CalculateGoldReceive()
        {
            try
            {
                double num = Convert.ToDouble(txtGoldPriceSell.Text);
                txtGoldPriceSell.Text = String.Format("{0:n}", num);
                GetGoldReceive();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }
    }
}