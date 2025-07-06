using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Database;
using SANSANG.Constant;
using SANSANG.Utilites.App.Forms;
using SANSANG.Utilites.App.Model;

namespace SANSANG
{
    public partial class FrmStatements : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "SAVSM00";
        public string AppName = "FrmStatements";
        public string Error = "";
        public string Laguage;
        public bool IsStart = true;
        public bool IsClear = true;
        public string Account = "0";

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
        private clsImport Imports = new clsImport();
        private clsFormat Formats = new clsFormat();
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
        private string[,] Parameter = new string[,] { };
        private bool IsWithdrawal = false;
        private string Details = "";
        private string Items = "";

        public FrmStatements(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
        {
            InitializeComponent();
            UserId = UserIdLogin;
            UserName = UserNameLogin;
            UserSurname = UserSurNameLogin;
            UserType = UserTypeLogin;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            Loading.Show();
            Timer.Interval = (2000);
            Timer.Tick += new EventHandler(LoadList);
            Timer.Start();
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "0"));
            List.GetLists(cbbMoney, DataList.MoneyId);
            List.GetList(cbbAccount, DataList.AccountId);
            List.GetList(cbbBankName, DataList.Banks);

            IsStart = true;
            gbForm.Enabled = true;
            Clear();
            Timer.Stop();
        }

        public void Clear()
        {
            IsClear = true;
            Account = Function.GetComboId(cbbAccount);
            Function.ClearAll(gbForm);
            txtCode.Enabled = true;

            cb_Save.Checked = false;
            pb_Save_True.Visible = false;
            pb_Save_False.Visible = true;

            cb_Date.Checked = false;
            pb_Date_True.Visible = false;
            pb_Date_False.Visible = true;

            if (IsStart)
            {
                cbbAccount.SelectedValue = Setting.GetAccounts();
                IsStart = false;
            }
            else
            {
                cbbAccount.SelectedValue = Account;
            }

            dtDate.Value = DateTime.Today;
            dtTime.Text = "00:00:00";
            txtBalance.Text = "0.00";
            txtItem.Text = "";
            Search(false);
            IsClear = false;
            IsWithdrawal = false;
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
                {"@AccountId", Function.GetComboId(cbbAccount)},
                {"@Date", Search? cb_Date.Checked == true? Date.GetDate(dtp : dtDate, Format : 4) : "" : ""},
                {"@Time", ""},
                {"@PaymentId", Search? Function.GetComboId(cbbTransactions) : "0"},
                {"@Item", Search? txtItem.Text : ""},
                {"@MoneyId", Search? Function.GetComboId(cbbMoney) : "0"},
                {"@Branch", Search? txtBranch.Text : ""},
                {"@Withdrawal", Search? !IsWithdrawal? Function.RemoveComma(txtAmount.Text) : "0.00" : ""},
                {"@Deposit", Search? IsWithdrawal? Function.RemoveComma(txtAmount.Text) : "0.00" : ""},
                {"@Balance", "0.00"},
                {"@Number", Search? txtNumber.Text : ""},
                {"@Detail", Search? txtDetail.Text : ""},
                {"@Channel", Search? txtChannel.Text : ""},
                {"@Display", Search? txtDisplay.Text : ""},
                {"@Reference", Search? txtReference.Text : ""},
            };

            string Condition = Function.ShowConditons(GetCondition());
            lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;

            db.Get(Store.ManageStatement, Parameter, out Error, out dt);
            ShowGridView(dt);
            Balance(false);
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";

                strCondition += txtCode.Text != "" ? ", รหัสอ้างอิง: " + txtCode.Text : "";
                strCondition += cbbAccount.SelectedValue.ToString() != "0" ? ", บัญชี: " + cbbAccount.Text : "";
                strCondition += cbbTransactions.SelectedValue.ToString() != "0" ? ", ธุรกรรม: " + cbbTransactions.Text : "";
                strCondition += cbbMoney.SelectedValue.ToString() != "0" ? ", ประเภทเงิน: " + cbbMoney.Text : "";
                strCondition += txtAmount.Text != "" ? ", จำนวนเงิน: " + txtAmount.Text : "";
                strCondition += cb_Date.Checked == true ? ", วันที่ทำรายการ: " + Date.GetDate(dtp: dtDate, Format: 4) : "";
                strCondition += txtNumber.Text != "" ? ", หมายเลข: " + txtNumber.Text : "";
                strCondition += txtDetail.Text != "" ? ", รายละเอียด: " + txtDetail.Text : "";
                strCondition += txtDisplay.Text != "" ? ", แสดงผล: " + txtDisplay.Text : "";
                strCondition += txtBranch.Text != "" ? ", สาขา: " + txtBranch.Text : "";
                strCondition += txtItem.Text != "" ? ", รายการ: " + txtItem.Text : "";
                strCondition += txtReference.Text != "" ? ", เลขอ้างอิง: " + txtReference.Text : "";
                strCondition += txtChannel.Text != "" ? ", ช่องทาง: " + txtChannel.Text : "";

                strCondition += Function.GetComboId(cbbStatus) != "0" ? ", สถานะ: " + cbbStatus.Text : "";

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
            int row;

            try
            {
                row = dt.Rows.Count;
            }
            catch (Exception)
            {
                row = 0;
            }

            if (row == 0)
            {
                GridView.DataSource = null;
                picExcel.Visible = false;
                txtCode.Text = "";
                txtCode.Focus();
            }
            else
            {
                GridView.DataSource = null;
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "SNo", "Dates", "Displays", "Channel", "Moneys", "Values", "Balances", "Id");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
                DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                Function.showGridViewFormatFromStore(dtGrid, GridView,
                      "ลำดับ", 50, true, mr, mc
                    , "วันที่ | เวลา", 120, true, ml, ml
                    , "รายการ", 300, true, ml, ml
                    , "ช่องทาง", 300, true, ml, ml
                    , "ประเภทเงิน", 120, true, ml, ml
                    , "จำนวนเงิน", 200, true, mr, mr
                    , "คงเหลือ", 200, true, mr, mr
                    , "", 0, false, mr, mr
                    );

                GridView.Focus();
            }

            txtCount.Text = Function.ShowNumberOfData(Function.GetRows(dt));
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SearchData(object sender, EventArgs e)
        {
            Search(true);
        }

        private void AddData(object sender, EventArgs e)
        {
            try
            {
                if (Function.GetComboId(cbbMoney) != "0" && Function.GetComboId(cbbAccount) != "0" && Function.GetComboId(cbbTransactions) != "0" && !string.IsNullOrEmpty(txtAmount.Text))
                {
                    if (!Function.IsDuplicates(Table.Statments,
                        "ONE",
                        Function.GetComboId(cbbAccount),
                        Function.GetComboId(cbbTransactions),
                        Date.GetDate(dtp: dtDate, Format: 4),
                        Function.RemoveComma(txtAmount.Text),
                        Date.GetDate(dtp: dtDate, Format: 6) + System.Environment.NewLine + cbbTransactions.Text + " - " + txtAmount.Text))
                    {
                        txtCode.Text = Function.GetCodes(Table.StatmentId, "", "Generated");

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
                            {"@Date", Date.GetDate(dtp : dtDate, Format : 4)},
                            {"@Time", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                            {"@PaymentId", Function.GetComboId(cbbTransactions)},
                            {"@Item", txtItem.Text},
                            {"@MoneyId", Function.GetComboId(cbbMoney)},
                            {"@Branch", txtBranch.Text},
                            {"@Withdrawal", !IsWithdrawal? Function.RemoveComma(txtAmount.Text) : "0.00"},
                            {"@Deposit", IsWithdrawal? Function.RemoveComma(txtAmount.Text) : "0.00"},
                            {"@Balance", "0.00"},
                            {"@Number", txtNumber.Text},
                            {"@Detail", txtDetail.Text},
                            {"@Channel", txtChannel.Text},
                            {"@Display", txtDisplay.Text},
                            {"@Reference", txtReference.Text},
                        };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageStatement, Parameter, txtCode.Text, Details: GetDetails()))
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
                        {"@Operation", Operation.UpdateAbbr},
                        {"@AccountId", Function.GetComboId(cbbAccount)},
                        {"@Date", Date.GetDate(dtp : dtDate, Format : 4)},
                        {"@Time", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                        {"@PaymentId", Function.GetComboId(cbbTransactions)},
                        {"@Item", txtItem.Text},
                        {"@MoneyId", Function.GetComboId(cbbMoney)},
                        {"@Branch", txtBranch.Text},
                        {"@Withdrawal", !IsWithdrawal? Function.RemoveComma(txtAmount.Text) : "0.00"},
                        {"@Deposit", IsWithdrawal? Function.RemoveComma(txtAmount.Text) : "0.00"},
                        {"@Balance", "0.00"},
                        {"@Number", txtNumber.Text},
                        {"@Detail", txtDetail.Text},
                        {"@Channel", txtChannel.Text},
                        {"@Display", txtDisplay.Text},
                        {"@Reference", txtReference.Text},
                    };

                    if (Edit.Update(AppCode, AppName, UserId, Store.ManageStatement, Parameter, txtCode.Text, Details: GetDetails()))
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
                if (Delete.Drop(AppCode, AppName, UserId, 0, Table.Statments, txtCode, Details: GetDetails(), true))
                {
                    Clear();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void GridViewClick(object sender, DataGridViewCellEventArgs e)
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
                    {"@AccountId",  "0"},
                    {"@Date", ""},
                    {"@Time", ""},
                    {"@PaymentId", "0"},
                    {"@Item", ""},
                    {"@MoneyId", "0"},
                    {"@Branch", ""},
                    {"@Withdrawal", "0.00"},
                    {"@Deposit", "0.00"},
                    {"@Balance", "0.00"},
                    {"@Number", ""},
                    {"@Detail", ""},
                    {"@Channel", ""},
                    {"@Display", ""},
                    {"@Reference", ""},
                };

                db.Get(Store.ManageStatement, Parameter, out Error, out dt);
                ShowData(dt);
                Balance(true);
            }
        }

        public void ShowData(DataTable Data)
        {
            try
            {
                txtId.Text = dt.Rows[0]["Id"].ToString();
                txtCode.Text = dt.Rows[0]["Code"].ToString();
                cbbAccount.SelectedValue = dt.Rows[0]["AccountId"].ToString();
                dtDate.Text = dt.Rows[0]["Date"].ToString();
                dtTime.Text = dt.Rows[0]["Time"].ToString();
                cbbTransactions.SelectedValue = dt.Rows[0]["PaymentId"].ToString();
                txtItem.Text = dt.Rows[0]["Item"].ToString();
                cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                cbbMoney.SelectedValue = dt.Rows[0]["MoneyId"].ToString();
                txtAmount.Text = Formats.Number(dt.Rows[0]["Values"].ToString(), "Money");
                txtNumber.Text = dt.Rows[0]["Number"].ToString();
                txtDetail.Text = dt.Rows[0]["Detail"].ToString();
                txtReference.Text = dt.Rows[0]["Reference"].ToString();
                txtBranch.Text = dt.Rows[0]["Branch"].ToString();
                txtChannel.Text = dt.Rows[0]["Channel"].ToString();
                txtDisplay.Text = dt.Rows[0]["Display"].ToString();
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

        private void TransactionsChanged(object sender, EventArgs e)
        {
            try
            {
                if (!IsClear)
                {
                    IsWithdrawal = Function.GetPayments(Function.selectedValue(cbbTransactions), out Details, out Items);
                    txtDisplay.Text = Details;
                    txtItem.Text = Items;
                }
            }
            catch (Exception)
            {
                IsWithdrawal = false;
                txtDisplay.Text = "";
                txtItem.Text = "";
            }

            GridView.Focus();
        }

        private void Ticker(object sender, EventArgs e)
        {
            Helper.CheckboxTicker(sender, this);
        }

        private void AccountChanged(object sender, EventArgs e)
        {
            string Accounts = Function.selectedValue(cbbAccount);
            cbbBankName.SelectedValue = Images.GetBankLogo(pbLogo, Accounts);

            if (!IsStart)
            {
                dtDate.Value = DateTime.Today;
                dtTime.Text = "00:00:00";
                txtBalance.Text = "0.00";

                txtCode.Text = "";
                txtItem.Text = "";
                txtAmount.Text = "";
                txtNumber.Text = "";
                txtDetail.Text = "";
                txtDisplay.Text = "";
                txtReference.Text = "";
                txtBranch.Text = "";
                txtChannel.Text = "";

                cbbStatus.SelectedValue = 0;
                cbbTransactions.SelectedValue = 0;
                cbbMoney.SelectedValue = 0;
                Search(false);
            }
        }

        private void GetTransactions()
        {
            List.GetList(cbbTransactions, DataList.Transactions, cbbBankName.Text);
            cbbTransactions.SelectedValue = "0";
            cbbMoney.SelectedValue = "0";
            txtItem.Text = "";
            txtDetail.Text = "";
            txtCode.Focus();
        }

        private void BankNameChanged(object sender, EventArgs e)
        {
            GetTransactions();
        }

        private void Balance(bool Search)
        {
            string Deposits = "0.00";
            string Withdrawals = "0.00";
            string Balances = "0.00";

            try
            {
                if (Search)
                {
                    dt = Function.GetBankBalance(txtId.Text, cbbAccount.SelectedValue.ToString());
                }
                else
                {
                    dt = Function.GetBankBalance("", cbbAccount.SelectedValue.ToString());
                }

                if (dt != null)
                {
                    Deposits = string.Format("{0:#,##0.00}", double.Parse(Convert.ToString(dt.Rows[0]["Deposits"].ToString())));
                    Withdrawals = string.Format("{0:#,##0.00}", double.Parse(Convert.ToString(dt.Rows[0]["Withdrawals"].ToString())));
                    Balances = string.Format("{0:#,##0.00}", double.Parse(Convert.ToString(dt.Rows[0]["Balances"].ToString())));
                }

                txtDeposit.Text = Deposits;
                txtWithdrawal.Text = Withdrawals;
                txtBalance.Text = Balances;
            }
            catch
            {
                txtWithdrawal.Text = "0.00";
                txtDeposit.Text = "0.00";
                txtBalance.Text = "0.00";
            }
        }

        public string GetDetails()
        {
            return txtDetail.Text + " (฿" + txtAmount.Text + ")";
        }

        private void ImportData(object sender, EventArgs e)
        {
            Imports.ImportStatment(Function.GetComboId(cbbAccount));
            Clear();
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount.Text != "")
            {
                cbbStatus.SelectedValue = "1000";
            }
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                double num = Convert.ToDouble(txtAmount.Text);
                txtAmount.Text = String.Format("{0:n}", num);
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
    }
}