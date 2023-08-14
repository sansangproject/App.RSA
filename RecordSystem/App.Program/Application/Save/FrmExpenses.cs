using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;
using SANSANG.Constant;
using SANSANG.Utilites.App.Model;
using RecordSystemApplication.App.Program.Application.Payment;

namespace SANSANG
{
    public partial class FrmExpenses : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "SAVEP00";
        public string AppName = "FrmExpenses";
        public string Laguage;
        public int Row = 0;
        public double Sum = 0;
        public bool SearchPress = false;
        public bool Start = true;

        private clsInsert Insert = new clsInsert();
        private clsFunction Function = new clsFunction();
        private clsMessage Message = new clsMessage();
        private clsDataList List = new clsDataList();
        private dbConnection db = new dbConnection();
        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        private clsDate Date = new clsDate();
        private clsLog Log = new clsLog();
        private clsSetting Setting = new clsSetting();
        private clsHelpper Helper = new clsHelpper();
        private clsEvent Event = new clsEvent();
        private clsDelete Delete = new clsDelete();
        private IdConstant Id = new IdConstant();
        private StringConstant Strings = new StringConstant();
        private DataListConstant DataList = new DataListConstant();
        private StoreConstant Store = new StoreConstant();
        private PaymentConstant Payment = new PaymentConstant();
        private TableConstant Table = new TableConstant();
        private OperationConstant Operation = new OperationConstant();
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(10);
        private CharacterConstant CharType = new CharacterConstant();
        private DateTime dNext;
        private DateTime dTime;

        private string[,] Parameter = new string[,] { };
        private Timer Timer = new Timer();
        private bool ShowNext;
        private string Error = "";
        private double Credit = 0;
        private double TotalCredit = 0;
        private double Debit = 0;
        private string Details = "";
        private string Items = "";
        private int DataRows = 0;
        private string IsDebit = "false";
        private string MoneyIsDelete = "";
        private bool IsSearchPayment = false;

        public FrmExpenses(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
        {
            try
            {
                InitializeComponent();
                UserId = UserIdLogin;
                UserName = UserNameLogin;
                UserSurname = UserSurNameLogin;
                UserType = UserTypeLogin;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void FormLoad(object sender, EventArgs e)
        {
            try
            {
                Loading.Show();
                Timer.Interval = (1000);
                Timer.Tick += new EventHandler(LoadList);
                Timer.Start();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void LoadList(object sender, EventArgs e)
        {
            Start = true;

            Laguage = clsSetting.ReadLanguageSetting();
            ShowNext = Setting.GetDisplay();

            List.GetLists(cbbMoney, DataList.MoneyId);
            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "0"));
            List.GetLists(cbbCategory, DataList.CategoryId);
            List.GetLists(cbbUnit, DataList.UnitId);

            cbbMoney.Enabled = true;
            cbbStatus.Enabled = true;
            cbbCategory.Enabled = true;
            cbbItem.Enabled = true;
            cbbUnit.Enabled = true;

            Clear(true);
            Timer.Stop();
        }

        public void Clear(bool IsLoad)
        {
            try
            {
                Start = false;
                IsDebit = "false";
                txtCode.Text = "";
                txtId.Text = "";
                txtAmount.Text = "";
                txtPrice.Text = "";
                txtDetails.Text = "";
                txtItem.Text = "";
                txtReceipt.Text = "";
                txtUnit.Text = "";
                txtId.Text = "";
                MoneyIsDelete = "";
                lblBalance.Text = "เงินคงเหลือ";

                SearchPress = false;
                cb_Date.Checked = false;
                cb_Paysub.Checked = false;
               
                cbbMoney.Enabled = true;
                pbHide.Visible = false;

                pb_Date_True.Hide();
                pb_Paysub_True.Hide();
                pb_Date_False.Show();
                pb_Paysub_False.Show();

                if (!IsSearchPayment)
                {
                    cb_Receipt.Checked = true;
                    pb_Receipt_True.Show();
                    pb_Receipt_False.Hide();
                }

                Sum = 0;

                cbbStatus.SelectedValue = 0;
                cbbMoney.SelectedValue = 0;
                cbbCategory.SelectedValue = 0;
                cbbUnit.SelectedValue = 0;

                cbbItem.Text = ":: กรุณาเลือก ::";

                DataRows = 0;
                DataGridView0.DataSource = null;
                DataGridView1.DataSource = null;

                dTime = Convert.ToDateTime(dtExpense.Text);
                dNext = dTime.AddDays(+1);
                string strDate = Date.GetDate(dt: dTime, Format: 4);

                if (IsLoad)
                {
                    GetDataGrid(strDate);
                }

                GetBalance(dTime, dNext);

                if (DataGridView0.DataSource != null)
                {
                    DataGridView0.Focus();
                }
                else
                {
                    txtItem.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public void GetBalance(DateTime dtCalBalance, DateTime dtCalBalanceNext)
        {
            try
            {
                string CalBalanceDate = Date.GetDate(dt: dtCalBalance, Format: 4);
                string CalBalanceNextDate = Date.GetDate(dt: dtCalBalanceNext, Format: 4);

                DataTable Balance = new DataTable();
                Function.GetBalance(CalBalanceDate, out Balance);

                if (Balance != null)
                {
                    txtSumDebit.Text = string.Format("{0:#,##0.00}", double.Parse(Convert.ToString(Balance.Rows[0]["DebitBalance"].ToString())));
                    txtSumCredit.Text = string.Format("{0:#,##0.00}", double.Parse(Convert.ToString(Balance.Rows[0]["CreditBalance"].ToString())));
                    txtSumBank.Text = string.Format("{0:#,##0.00}", double.Parse(Convert.ToString(Balance.Rows[0]["BankBalance"].ToString())));
                    txtSumWallet.Text = string.Format("{0:#,##0.00}", double.Parse(Convert.ToString(Balance.Rows[0]["WalletBalance"].ToString())));
                }

                txtTotalReal.Text = Function.SumBalance(CalBalanceNextDate);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public void GetDataGrid(string Date)
        {
            try
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
                    {"@List", ""},
                    {"@MoneyId", "0"},
                    {"@CategoryId", "0"},
                    {"@ItemId", "0"},
                    {"@IsDebit", ""},
                    {"@Item", ""},
                    {"@Detail", ""},
                    {"@Amount", "0.00"},
                    {"@Price", "0.00"},
                    {"@UnitId", "0"},
                    {"@Unit", "0.00"},
                    {"@Date", Date},
                    {"@Receipt", ""},
                };

                db.Gets(Store.ManageExpense, Parameter, out Error, out ds);
                ShowDataGridView(ds);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchPress = true;
                Search(false, "");
                pbHide.Visible = false;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void cbbPay_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Start)
                {
                    List.GetLists(cbbItem, string.Format(DataList.ItemId, cbbCategory.SelectedValue.ToString()));
                    cbbMoney.SelectedValue = "0";

                    txtDetails.Text = "";
                    txtItem.Text = "";

                    Event.SetAutoMoney(cbbItem, cbbMoney);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void cbbPaySub_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Start)
                {
                    Function.GetPayment(Function.GetComboBoxId(cbbItem), out Details, out Items);

                    txtItem.Text = Items;
                    txtDetails.Text = Details;

                    Event.SetAutoMoney(cbbItem, cbbMoney);
                    txtAmount.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                txtItem.Text = "";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            IsSearchPayment = false;
            Clear(true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string Codes = Function.GetCodes(Table.ExpenseId, "", "Generated");

                if (cbbCategory.SelectedIndex == 0)
                {
                    cbbCategory.Focus();
                }
                else if (txtAmount.Text == "")
                {
                    txtAmount.Focus();
                }
                else if (txtItem.Text == "")
                {
                    txtItem.Focus();
                }
                else
                {
                    try
                    {
                        dtExpense.Value = Setting.GetOperationDate() == "" ? dtExpense.Value : Convert.ToDateTime(Setting.GetOperationDate());

                        string List = Date.GetDate(dt: dtExpense.Value, Format: 5);

                        txtCode.Text = Codes != "" ? Codes : Function.GetCodes(Table.ExpenseId, "", "Generated");

                        string[,] Parameter = new string[,]
                        {
                            {"@Id", ""},
                            {"@Code", txtCode.Text},
                            {"@Status", Function.GetComboId(cbbStatus)},
                            {"@User", UserId},
                            {"@IsActive", "1"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@List", List},
                            {"@MoneyId", Function.GetComboId(cbbMoney)},
                            {"@CategoryId", Function.GetComboId(cbbCategory)},
                            {"@ItemId", Function.GetComboId(cbbItem)},
                            {"@IsDebit", ""},
                            {"@Item", txtItem.Text},
                            {"@Detail", txtDetails.Text},
                            {"@Amount", Function.MoveNumberStringComma(txtAmount.Text)},
                            {"@Price", Function.MoveNumberStringComma(txtPrice.Text)},
                            {"@UnitId", Function.GetComboId(cbbUnit) == "0"? "1213" : Function.GetComboId(cbbUnit)},
                            {"@Unit", txtUnit.Text == ""? "1.00" : txtUnit.Text},
                            {"@Date", Date.GetDate(dtp : dtExpense)},
                            {"@Receipt", cb_Receipt.Checked? txtReceipt.Text : ""},
                        };

                        string[,] Parameters = new string[,]
                        {
                            {"@Id", ""},
                            {"@Code", txtCode.Text},
                            {"@User", UserId},
                            {"@IsActive", "1"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@Date", Date.GetDate(dtp : dtExpense)},
                            {"@Receipt", cb_Receipt.Checked? "" : txtReceipt.Text},
                            {"@MoneyId1", Function.GetComboId(cbbMoney)},
                            {"@Amount1", Function.MoveNumberStringComma(txtAmount.Text)},
                            {"@MoneyId2", "0"},
                            {"@Amount2", "0"},
                            {"@MoneyId3", "0"},
                            {"@Amount3", "0"},
                            {"@MoneyId4", "0"},
                            {"@Amount4", "0"},
                            {"@MoneyId5", "0"},
                            {"@Amount5", "0"},
                            {"@UpdateType", ""},
                        };

                        Message.MessageConfirmation(Operation.InsertAbbr, txtCode.Text, cbbItem.Text + " ฿" + txtAmount.Text + " (" + cbbMoney.Text + ")");

                        using (var Mes = new FrmMessagesBox(Message.strOperation.Substring(1, Message.strOperation.Length - 1), Message.strMes, "YES", "NO", Message.strImage))
                        {
                            var result = Mes.ShowDialog();

                            if (result == DialogResult.Yes)
                            {
                                Mes.Close();
                                db.Operations(Store.ManageExpense, Parameter, out Error);
                                db.Operations(Store.ManagePayments, Parameters, out Error);

                                if (string.IsNullOrEmpty(Error))
                                {
                                    Message.MessageResult(Operation.InsertAbbr, "C", Error);
                                    Clear(true);
                                }
                                else
                                {
                                    Message.MessageResult(Operation.InsertAbbr, "E", Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void dataGridView0_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = DataGridView0.Rows[e.RowIndex];
                    DataTable dt = new DataTable();
                    IsDebit = "false";
                    string[,] Parameter = new string[,]
                    {
                        {"@Id", row.Cells["Id"].Value.ToString()},
                        {"@Code", ""},
                        {"@Status", "0"},
                        {"@User", ""},
                        {"@IsActive", ""},
                        {"@IsDelete", ""},
                        {"@Operation", Operation.SelectAbbr},
                        {"@List", ""},
                        {"@MoneyId", "0"},
                        {"@CategoryId", "0"},
                        {"@ItemId", "0"},
                        {"@IsDebit", "0"},
                        {"@Item", ""},
                        {"@Detail", ""},
                        {"@Amount", "0.00"},
                        {"@Price", "0.00"},
                        {"@UnitId", "0"},
                        {"@Unit", "0.00"},
                        {"@Date", ""},
                        {"@Receipt", ""},
                    };

                    db.Gets(Store.ManageExpense, Parameter, out Error, out ds);

                    if (string.IsNullOrEmpty(Error))
                    {
                        ShowData(ds.Tables[0]);
                        DataGridView0.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = DataGridView1.Rows[e.RowIndex];
                    DataTable dt = new DataTable();
                    IsDebit = "true";

                    string[,] Parameter = new string[,]
                    {
                        {"@Id", row.Cells["Id"].Value.ToString()},
                        {"@Code", ""},
                        {"@Status", "0"},
                        {"@User", ""},
                        {"@IsActive", ""},
                        {"@IsDelete", ""},
                        {"@Operation", "S"},
                        {"@List", ""},
                        {"@MoneyId", "0"},
                        {"@CategoryId", "0"},
                        {"@ItemId", "0"},
                        {"@IsDebit", "1"},
                        {"@Item", ""},
                        {"@Detail", ""},
                        {"@Amount", "0.00"},
                        {"@Price", "0.00"},
                        {"@UnitId", "0"},
                        {"@Unit", "0.00"},
                        {"@Date", ""},
                        {"@Receipt", ""},
                    };

                    db.Gets(Store.ManageExpense, Parameter, out Error, out ds);

                    if (string.IsNullOrEmpty(Error))
                    {
                        ShowData(ds.Tables[1]);
                        DataGridView1.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text != "")
                {
                    string List = Date.GetDate(dtp: dtExpense, Format: 5);

                    string[,] Parameter = new string[,]
                    {
                        {"@Id", txtId.Text},
                        {"@Code", txtCode.Text},
                        {"@Status", Function.GetComboId(cbbStatus)},
                        {"@User", UserId},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.UpdateAbbr},
                        {"@List", List},
                        {"@MoneyId", MoneyIsDelete == ""? Function.GetComboId(cbbMoney) : MoneyIsDelete},
                        {"@CategoryId", Function.GetComboId(cbbCategory)},
                        {"@ItemId", Function.GetComboId(cbbItem)},
                        {"@IsDebit", ""},
                        {"@Item", txtItem.Text},
                        {"@Detail", txtDetails.Text},
                        {"@Amount", Function.MoveNumberStringComma(txtAmount.Text)},
                        {"@Price", Function.MoveNumberStringComma(txtPrice.Text)},
                        {"@UnitId", Function.GetComboId(cbbUnit) == "0"? "2222" : Function.GetComboId(cbbUnit)},
                        {"@Unit", txtUnit.Text == ""? "1.00" : txtUnit.Text},
                        {"@Date", Date.GetDate(dtp : dtExpense)},
                        {"@Receipt", cb_Receipt.Checked? txtReceipt.Text : ""},
                    };

                    string[,] Payment = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", txtCode.Text},
                        {"@User", UserId},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.UpdateAbbr},
                        {"@Date", Date.GetDate(dtp : dtExpense)},
                        {"@Receipt", cb_Receipt.Checked? "" : txtReceipt.Text},
                        {"@MoneyId1", MoneyIsDelete == ""? Function.GetComboId(cbbMoney) : MoneyIsDelete},
                        {"@Amount1", Function.MoveNumberStringComma(txtAmount.Text)},
                        {"@MoneyId2", "0"},
                        {"@Amount2", "0"},
                        {"@MoneyId3", "0"},
                        {"@Amount3", "0"},
                        {"@MoneyId4", "0"},
                        {"@Amount4", "0"},
                        {"@MoneyId5", "0"},
                        {"@Amount5", "0"},
                        {"@UpdateType", "EXPENSE"},
                    };

                    Message.MessageConfirmation(Operation.UpdateAbbr, txtCode.Text, cbbItem.Text + " ฿" + txtAmount.Text + " (" + cbbMoney.Text + ")");

                    using (var Mes = new FrmMessagesBox(Message.strOperation.Substring(1, Message.strOperation.Length - 1), Message.strMes, "YES", "NO", Message.strImage))
                    {
                        var result = Mes.ShowDialog();

                        if (result == DialogResult.Yes)
                        {
                            Mes.Close();
                            db.Operations(Store.ManageExpense, Parameter, out Error);
                            db.Operations(Store.ManagePayments, Payment, out Error);

                            if (string.IsNullOrEmpty(Error))
                            {
                                Message.MessageResult(Operation.UpdateAbbr, "C", Error);
                                Clear(true);
                            }
                            else
                            {
                                Message.MessageResult(Operation.UpdateAbbr, "E", Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Delete.Drop(AppCode, AppName, UserId, 0, Table.Expenses, txtCode, cbbItem.Text + " ฿" + txtAmount.Text + " (" + cbbMoney.Text + ")")
                 && Delete.Drop(AppCode, AppName, UserId, 0, Table.Payments, txtCode, "", false))
                {
                    Clear(true);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                var Sender = (Button)sender;
                string Button = Sender.Name;

                string Amounts = Button == "btnSend" ? txtTotalReal.Text : txtTotal.Text;

                DateTimePicker dtpNext = new DateTimePicker();
                dtpNext.Value = dtExpense.Value.AddDays(1);

                if (!Function.IsDuplicates(Table.Expenses, Id.SubPaymentCarry, Date.GetDate(dtp: dtpNext)))
                {
                    if (!string.IsNullOrEmpty(Amounts))
                    {
                        DateTime dt = (Convert.ToDateTime(dtExpense.Text).AddDays(1));
                        string List = Date.GetDate(dt: dt, Format: 5);
                        txtCode.Text = Function.GetCodes(Table.ExpenseId, "", "Generated");

                        Parameter = new string[,]
                        {
                            {"@Id", ""},
                            {"@Code", txtCode.Text},
                            {"@Status", Id.StatusActive},
                            {"@User", UserId},
                            {"@IsActive", "1"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@List", List},
                            {"@MoneyId", Id.MoneyCash},
                            {"@CategoryId", Id.PaymentCarry},
                            {"@ItemId", Id.SubPaymentCarry},
                            {"@IsDebit", "1"},
                            {"@Item", "เงินยกยอด | ยกยอดมา"},
                            {"@Detail", ""},
                            {"@Amount", Function.ReplaceComma(Amounts)},
                            {"@Price", Function.ReplaceComma(Amounts)},
                            {"@UnitId", Id.UnitDefault},
                            {"@Unit", "1"},
                            {"@Date", Date.GetDate(dt : dt)},
                            {"@Receipt", ""},
                        };

                        Message.MessageConfirmation(Operation.InsertAbbr, txtCode.Text, "ยกยอดเงิน ฿" + Amounts + " (เงินสด)");

                        using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                        {
                            var result = Mes.ShowDialog();

                            if (result == DialogResult.Yes)
                            {
                                Mes.Close();
                                db.Operations(Store.ManageExpense, Parameter, out Error);

                                if (Error == null)
                                {
                                    Message.MessageResult(Operation.InsertAbbr, "C", Error);
                                    Clear(true);
                                }
                                else
                                {
                                    Message.MessageResult(Operation.InsertAbbr, "E", Error);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public void Search(bool Status, string Types)
        {
            try
            {
                DateTime dt;
                DateTime dts = Convert.ToDateTime(dtExpense.Text);
                string DataDates = "";
                string Receipt = txtReceipt.Text;
                string Dates = cb_Date.Checked ? Date.GetDate(dt: dts, Format: 4) : "";
                DataRows = 0;

                if (Status == true && Types == Strings.Nexts)
                {
                    dt = Convert.ToDateTime(dtExpense.Text).AddDays(1);
                    dtExpense.Text = Date.GetDate(dt: dt, Format: 1);

                    DataDates = Date.GetDate(dt: dt, Format: 4);
                    GetDataGrid(DataDates);
                    GetBalance(dt, dt.AddDays(+1));
                }
                else if (Status == true && Types == Strings.Previous)
                {
                    dt = Convert.ToDateTime(dtExpense.Text).AddDays(-1);
                    dtExpense.Text = Date.GetDate(dt: dt, Format: 1);

                    DataDates = Date.GetDate(dt: dt, Format: 4);
                    GetDataGrid(DataDates);
                    GetBalance(dt, dt.AddDays(+1));
                }
                else if (Status == true && Types == Strings.Payment)
                {
                    Clear(false);
                    SearchBalance(Strings.Payment, Receipt, Dates);
                }
                else if (Status == true && Types == Strings.Receipt)
                {
                    Clear(false);
                    SearchBalance(Strings.Receipt, Receipt, Dates);
                }
                else
                {
                    SearchData();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void SearchData()
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", txtCode.Text},
                    {"@Status", Function.GetComboZero(cbbStatus)},
                    {"@User", ""},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.SelectAbbr},
                    {"@List", ""},
                    {"@MoneyId", Function.GetComboZero(cbbMoney)},
                    {"@CategoryId", Function.GetComboZero(cbbCategory)},
                    {"@ItemId", Function.GetComboZero(cbbItem)},
                    {"@IsDebit", ""},
                    {"@Item", txtItem.Text},
                    {"@Detail", txtDetails.Text},
                    {"@Amount", txtAmount.Text == ""? "0.00" : Function.SplitString(txtAmount.Text, ",", "")},
                    {"@Price", txtPrice.Text == ""? "0.00" : Function.SplitString(txtPrice.Text, ",", "")},
                    {"@UnitId", Function.GetComboZero(cbbUnit)},
                    {"@Unit", txtUnit.Text == ""? "0.00" : txtUnit.Text},
                    {"@Date", cb_Date.Checked? Date.GetDate(dtp: dtExpense, Format: 4) : ""},
                    {"@Receipt", txtReceipt.Text},
                };

                db.Gets(Store.ManageExpense, Parameter, out Error, out ds);
                ShowDataGridView(ds);

                db.Gets(Store.FnGetBalanceSearch, Parameter, out Error, out ds);

                if (string.IsNullOrEmpty(Error))
                {
                    Credit = double.Parse(Convert.ToString(ds.Tables[2].Rows[0]["SumCredit"].ToString()));
                    TotalCredit = double.Parse(Convert.ToString(ds.Tables[2].Rows[0]["TotalCredit"].ToString()));
                    Debit = double.Parse(Convert.ToString(ds.Tables[2].Rows[0]["SumDebit"].ToString()));
                }
                else
                {
                    Credit = 0.00;
                    TotalCredit = 0.00;
                    Debit = 0.00;
                }

                lblBalance.Text = "คงเหลือ";

                double TotalReal = Math.Abs(Debit - (Credit > TotalCredit ? Credit : TotalCredit));
                txtSumCredit.Text = string.Format("{0:#,##0.00}", Credit > TotalCredit ? Credit : TotalCredit);
                txtSumDebit.Text = string.Format("{0:#,##0.00}", Debit);
                txtTotalReal.Text = string.Format("{0:#,##0.00}", TotalReal);
                txtPayStatus.Text = "";
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void pbAddPayType_Click(object sender, EventArgs e)
        {
            try
            {
                FrmManageCategory FrmManageCategory = new FrmManageCategory(UserId, UserName, UserSurname, UserType);
                FrmManageCategory.Show();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void pbAddSubPayType_Click(object sender, EventArgs e)
        {
            try
            {
                FrmManageItem FrmManageItem = new FrmManageItem(UserId, UserName, UserSurname, UserType);
                FrmManageItem.Show();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void pbAddCashType_Click(object sender, EventArgs e)
        {
            try
            {
                //FrmMangeMoney FrmMangeMoney = new FrmMangeMoney(UserId, UserName, UserSurname, UserType);
                //FrmMangeMoney.Show();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void txtSumCARD_TextChanged(object sender, EventArgs e)
        {
            GetTotal();
        }

        private void txtTotalRealkeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8)
                {
                    if (e.KeyChar == Convert.ToChar(Keys.Enter))
                    {
                        if (txtTotalReal.Text == "" || txtTotalReal.Text == "0.00")
                        {
                            Message.ShowMesInfo("กรุณาระบุเงินยกยอดคงเหลือ");
                            txtTotalReal.Focus();
                        }
                        else
                        {
                            btnAdd_Click(sender, e);
                        }
                    }
                }
                else if (e.KeyChar == 43)
                {
                    Sum += Convert.ToDouble(txtTotalReal.Text);
                    txtTotalReal.Text = "";
                    txtTotalReal.Focus();
                    e.Handled = true;
                    return;
                }
                else if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    Sum += Convert.ToDouble(txtTotalReal.Text);
                    txtTotalReal.Text = string.Format("{0:#,##0.00}", Math.Abs(Sum));
                    Sum = 0;
                    btnNext.Focus();
                    e.Handled = true;
                    return;
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

        private void txtTotalReal_Leave(object sender, EventArgs e)
        {
            try
            {
                GetValue();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                txtTotalReal.Text = "0.00";
            }
        }

        public void GetValue()
        {
            try
            {
                txtPayStatus.Text = "";

                if (!SearchPress)
                {
                    if (txtTotalReal.Text != "0.00")
                    {
                        if (Convert.ToDouble(txtTotal.Text) == Convert.ToDouble(txtTotalReal.Text))
                        {
                            txtPayStatus.ForeColor = Color.DarkSeaGreen;
                            txtPayStatus.Text = "บันทึกข้อมูลถูกต้อง";
                        }

                        if (Convert.ToDouble(txtTotal.Text) < Convert.ToDouble(txtTotalReal.Text))
                        {
                            txtPayStatus.ForeColor = Color.SteelBlue;
                            txtPayStatus.Text = "รายรับขาดดุล " + String.Format("{0:n}", (Convert.ToDouble(txtTotalReal.Text) - Convert.ToDouble(txtTotal.Text))) + " บาท";
                        }

                        if (Convert.ToDouble(txtTotal.Text) > Convert.ToDouble(txtTotalReal.Text))
                        {
                            txtPayStatus.ForeColor = Color.Firebrick;
                            txtPayStatus.Text = "รายจ่ายไม่ครบ " + String.Format("{0:n}", (Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtTotalReal.Text))) + " บาท";
                        }
                    }
                    else
                    {
                        if (txtSumDebit.Text != "0.00" && txtSumCredit.Text != "0.00")
                        {
                            double total = (Convert.ToDouble(txtTotal.Text) - Convert.ToDouble(txtTotalReal.Text));
                            txtPayStatus.ForeColor = Color.DarkSeaGreen;

                            if (total != 0)
                            {
                                txtPayStatus.ForeColor = Color.Firebrick;
                                txtPayStatus.Text = "รายจ่ายไม่ครบ " + String.Format("{0:n}", total) + " บาท";
                            }
                            else
                            {
                                txtPayStatus.ForeColor = Color.DarkSeaGreen;
                                txtPayStatus.Text = "บันทึกข้อมูลถูกต้อง";
                            }
                        }
                        else
                        {
                            if (txtSumDebit.Text == "0.00" && txtSumCredit.Text == "0.00" && DataGridView0.Rows.Count > 0)
                            {
                                txtPayStatus.ForeColor = Color.DarkSeaGreen;
                                txtPayStatus.Text = "บันทึกข้อมูลถูกต้อง";
                            }
                            else
                            {
                                txtPayStatus.ForeColor = Color.Salmon;
                                txtPayStatus.Text = "กรุณาป้อนยอดเงินคงเหลือ";
                            }
                        }
                    }
                }
                else
                {
                    txtPayStatus.ForeColor = Color.Orange;
                    txtTotalReal.Text = txtTotal.Text;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void txtAmountkeyPress(object sender, KeyPressEventArgs e)
        {
            Event.AmountKeyPress(sender, e, txtAmount);
        }

        private void cbbStatuskeyPress(object sender, KeyPressEventArgs e)
        {
            Function.btnEnter(e, btnAdd);
        }

        private void picExcel0_Click(object sender, EventArgs e)
        {
            Function._excel(DataGridView0, "รายรับ");
        }

        private void picExcel1_Click(object sender, EventArgs e)
        {
            Function._excel(DataGridView1, "รายจ่าย");
        }

        private void txtSumDebit_TextChanged(object sender, EventArgs e)
        {
            GetTotal();
        }

        private void txtTotalReal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GetValue();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                txtTotalReal.Text = "0.00";
            }
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            GetValue();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.Right))
                {
                    MessageBox.Show("Do Something");
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return false;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                Clear(false);
                Search(true, Strings.Nexts);
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
                if (keyCode == "Ctrl+F")
                {
                    btnSearch_Click(sender, e);
                }
                if (keyCode == "Alt+C")
                {
                    btnClear_Click(sender, e);
                }
                if (keyCode == "Alt+N")
                {
                    btnSend_Click(sender, e);
                }
                if (keyCode == "Ctrl+N")
                {
                    btnNext_Click(sender, e);
                }
                if (keyCode == "Ctrl+P")
                {
                    btnPre_Click(sender, e);
                }
                if (keyCode == "Enter")
                {
                    Form Frm = (Form)sender;

                    if (Frm.ActiveControl.Text != txtTotalReal.Text && Frm.ActiveControl.Text != txtReceipt.Text)
                    {
                        Search(true, "");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            try
            {
                Clear(false);
                Search(true, Strings.Previous);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void cbbMoney_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Start)
                {
                    if ((cbbMoney.Text == "ฟรี") || (cbbMoney.Text == "Free"))
                    {
                        txtAmount.Text = "0";
                        txtReceipt.Focus();
                    }
                    else
                    {
                        txtAmount.Text = "";
                        txtAmount.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    Clear(false);
                    SearchBalance(Strings.Receipt, txtReceipt.Text);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //FrmImportImage Frm = new FrmImportImage(AppCode, UserId, UserName, UserSurname, UserType, txtCode.Text, "", "", "E");
            //Frm.ShowDialog();
            //txtDetails.Text += Frm.strImageCode;
        }

        private void btnTaq_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                //FrmPrintTag Frm = new FrmPrintTag(txtCode.Text);
                //Frm.ShowDialog();
            }
        }

        private void btnKeyboad_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }

        private void Ticker(object sender, EventArgs e)
        {
            Helper.CheckboxTicker(sender, this);
        }

        public void AutoAddExpense()
        {
            try
            {
                var Data = new ExpensesModel();

                Data = new ExpensesModel()
                {
                    User = UserId,
                    MoneyId = "",
                    CategoryId = "",
                    ItemId = "",
                    IsDebit = "",
                    Item = "",
                    Amount = Function.MoveNumberStringComma(txtAmount.Text),
                    Date = Date.GetDate(dtp: dtExpense),
                    Receipt = txtCode.Text,
                };

                Insert.Expenses(Data);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void txtItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                try
                {
                    SearchPress = true;
                    Search(false, "");
                }
                catch (Exception ex)
                {
                    Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                }
            }
        }

        private void txtUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            Event.AmountKeyPress(sender, e, txtUnit);
        }

        private void SearchBalance(string Type, string Receipt, string Date = "")
        {
            try
            {
                double Credit = 0;
                double Debit = 0;

                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", ""},
                    {"@IsDelete", ""},
                    {"@Operation", Operation.SelectAbbr},
                    {"@List", ""},
                    {"@MoneyId", "0"},
                    {"@CategoryId", "0"},
                    {"@ItemId", "0"},
                    {"@IsDebit", ""},
                    {"@Item", ""},
                    {"@Detail", ""},
                    {"@Amount", "0.00"},
                    {"@Price", "0.00"},
                    {"@UnitId", "0"},
                    {"@Unit", "0.00"},
                    {"@Date", Date},
                    {"@Receipt", Receipt},
                };

                if (Type == Strings.Receipt)
                {
                    db.Gets(Store.ManageExpense, Parameter, out Error, out ds);
                    ShowDataGridView(ds);

                    db.Gets(Store.FnGetBalanceSearch, Parameter, out Error, out ds);
                    Credit = double.Parse(Convert.ToString(ds.Tables[0].Rows[0]["SumCredit"].ToString()));
                    Debit = double.Parse(Convert.ToString(ds.Tables[0].Rows[0]["SumDebit"].ToString()));
                }
                else
                {
                    string[,] Parameters = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", ""},
                        {"@User", ""},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.SelectAbbr},
                        {"@Date", Date},
                        {"@Receipt", Receipt},
                        {"@MoneyId1", ""},
                        {"@Amount1", ""},
                        {"@MoneyId2", "0"},
                        {"@Amount2", "0"},
                        {"@MoneyId3", "0"},
                        {"@Amount3", "0"},
                        {"@MoneyId4", "0"},
                        {"@Amount4", "0"},
                        {"@MoneyId5", "0"},
                        {"@Amount5", "0"},
                        {"@UpdateType", ""},
                    };

                    db.Gets(Store.ManagePayments, Parameters, out Error, out ds);
                    ShowDataGridView(ds);

                    db.Gets(Store.FnGetBalanceSearch, Parameter, out Error, out ds);
                    Credit = double.Parse(Convert.ToString(ds.Tables[1].Rows[0]["SumCredit"].ToString()));
                    Debit = double.Parse(Convert.ToString(ds.Tables[1].Rows[0]["SumDebit"].ToString()));
                }

                txtReceipt.Text = Receipt;
                lblBalance.Text = "รวมทั้งสิ้น";
                txtTotalReal.Text = string.Format("{0:#,##0.00}", Math.Abs(Credit));
                txtSumCredit.Text = string.Format("{0:#,##0.00}", Credit);
                txtSumDebit.Text = string.Format("{0:#,##0.00}", Debit);
                txtPayStatus.Text = "";
            }
            catch (Exception ex)
            {
                Credit = 0;
                Debit = 0;
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void btnSearchPayment_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReceipt.Text))
            {
                Search(true, Strings.Payment);
                pbHide.Visible = true;
            }
            else
            {
                txtReceipt.Focus();
            }
        }

        private void btnSearchReceipt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReceipt.Text))
            {
                IsSearchPayment = true;

                if (cb_Receipt.Checked)
                {
                    Search(true, Strings.Receipt);
                    pbHide.Visible = true;
                }
                else
                {
                    Search(true, Strings.Payment);
                    pbHide.Visible = true;
                }
            }
            else
            {
                txtReceipt.Focus();
            }

        }

        private void txtReceipt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Function.IsCharacter(e.KeyChar, CharType.Receipt))
            {
                e.Handled = true;
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                string Codes = Function.SplitBarcode(txtReceipt.Text);
                txtReceipt.Text = Codes == "" ? txtReceipt.Text : Codes;
                Search(false, "");
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text != "")
                {
                    Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", txtCode.Text},
                        {"@User", ""},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.SelectAbbr},
                        {"@Date", ""},
                        {"@Receipt", ""},
                        {"@MoneyId1", "0"},
                        {"@Amount1", "0"},
                        {"@MoneyId2", "0"},
                        {"@Amount2", "0"},
                        {"@MoneyId3", "0"},
                        {"@Amount3", "0"},
                        {"@MoneyId4", "0"},
                        {"@Amount4", "0"},
                        {"@MoneyId5", "0"},
                        {"@Amount5", "0"},
                        {"@UpdateType", "0"},
                    };

                    db.Gets(Store.ManagePayments, Parameter, out Error, out ds);

                    if (ds.Tables[2] != null && Function.GetRows(ds.Tables[2]) > 0)
                    {
                        FrmPayment Frm = new FrmPayment(txtCode.Text, UserId, dtExpense.Value, ds.Tables[2].Rows[0]["IsDebit"].ToString());
                        Frm.FormClosed += new FormClosedEventHandler(FrmPaymentClosed);
                        Frm.Show();
                    }
                    else
                    {
                        string[,] Payment = new string[,]
                        {
                            {"@Id", ""},
                            {"@Code", txtCode.Text},
                            {"@User", UserId},
                            {"@IsActive", "1"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@Date", Date.GetDate(dtp : dtExpense)},
                            {"@Receipt", ""},
                            {"@MoneyId1", cbbMoney.SelectedValue.ToString()},
                            {"@Amount1", Function.MoveNumberStringComma(txtAmount.Text)},
                            {"@MoneyId2", "0"},
                            {"@Amount2", "0"},
                            {"@MoneyId3", "0"},
                            {"@Amount3", "0"},
                            {"@MoneyId4", "0"},
                            {"@Amount4", "0"},
                            {"@MoneyId5", "0"},
                            {"@Amount5", "0"},
                            {"@UpdateType", "0"},
                        };

                        db.Operations(Store.ManagePayments, Payment, out Error);

                        if (string.IsNullOrEmpty(Error))
                        {
                            FrmPayment Frm = new FrmPayment(txtCode.Text, UserId, dtExpense.Value, IsDebit);
                            Frm.FormClosed += new FormClosedEventHandler(FrmPaymentClosed);
                            Frm.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }
        private void FrmPaymentClosed(object sender, FormClosedEventArgs e)
        {
            Clear(true);
        }

        private void GetTotal()
        {
            try
            {
                double Amount = double.Parse(Convert.ToString(Convert.ToDouble(txtSumDebit.Text) - Convert.ToDouble(txtSumCredit.Text)));
                txtTotal.Text = string.Format("{0:#,##0.00}", Amount);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                txtTotal.Text = "0.00";
            }
        }

        public void ShowData(DataTable Data)
        {
            try
            {
                string UnitId = Data.Rows[0]["UnitId"].ToString() == "" ? "0" : Data.Rows[0]["UnitId"].ToString();
                string StatusId = Data.Rows[0]["Status"].ToString() == "" ? "0" : Data.Rows[0]["Status"].ToString();
                string CategoryId = Data.Rows[0]["CategoryId"].ToString() == "" ? "0" : Data.Rows[0]["CategoryId"].ToString();
                string ItemId = Data.Rows[0]["ItemId"].ToString() == "" ? "0" : Data.Rows[0]["ItemId"].ToString();
                string MoneyId = Data.Rows[0]["MoneyId"].ToString() == "" ? "0" : Data.Rows[0]["MoneyId"].ToString();
                string MoneyName = Data.Rows[0]["Moneys"].ToString();

                txtId.Text = Data.Rows[0]["Id"].ToString();
                dtExpense.Text = Data.Rows[0]["Date"].ToString();
                txtId.Text = Data.Rows[0]["Id"].ToString();
                txtCode.Text = Data.Rows[0]["Code"].ToString();
                txtReceipt.Text = Data.Rows[0]["Receipt"].ToString();

                txtUnit.Text = Data.Rows[0]["Unit"].ToString() == "0.00" ? "" : decimal.Parse(Data.Rows[0]["Unit"].ToString()).ToString("G29");

                cbbUnit.SelectedValue = UnitId;
                cbbCategory.SelectedValue = CategoryId;
                cbbItem.SelectedValue = ItemId;
                cbbMoney.SelectedValue = MoneyId;

                if (cbbMoney.SelectedValue == null)
                {
                    MoneyIsDelete = MoneyId;
                    cbbMoney.Text = MoneyName;
                }
                else
                {
                    MoneyIsDelete = "";
                }

                txtItem.Text = Data.Rows[0]["Item"].ToString();
                txtDetails.Text = Data.Rows[0]["Detail"].ToString();
                txtAmount.Text = Data.Rows[0]["Amount"].ToString();
                txtPrice.Text = Data.Rows[0]["Prices"].ToString();
                cbbStatus.SelectedValue = StatusId;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public void ShowDataGridView(DataSet Value)
        {
            try
            {
                DataTable Data = new DataTable();

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
                DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                for (int Index = 0; Index <= 1; Index++)
                {
                    Data = Value.Tables[Index].DefaultView.ToTable(true, "SNo", "Code", "Payments", "Moneys", "Amounts", "StatusName", "Id");
                    DataRows += Function.GetRows(Data);

                    if (Function.GetRows(Data) > 0)
                    {
                        if (Index == 0)
                        {

                            DataGridView0.DataSource = null;

                            Function.ShowGridViewFormatFromStore(Data, DataGridView0,
                              " ลำดับ", 35, true, mc, mc
                            , "รหัสอ้างอิง", 50, false, mc, mc
                            , "รายการ", 180, true, ml, ml
                            , "ประเภทเงิน", 80, true, ml, ml
                            , "จำนวนเงิน", 70, true, mr, mr
                            , "สถานะ", 0, false, ml, ml
                            , "", 0, false, mc, mc
                            );

                            CurrencyManager Cm = (CurrencyManager)BindingContext[DataGridView0.DataSource];

                            foreach (DataGridViewRow dgvr in DataGridView0.Rows)
                            {
                                if (dgvr.Cells[3].Value.ToString() == "Cash")
                                {
                                    dgvr.DefaultCellStyle.ForeColor = Color.DimGray;
                                }

                                if (!ShowNext && dgvr.Cells[2].Value.ToString() == Strings.Next)
                                {
                                    Cm.SuspendBinding();
                                    dgvr.Visible = false;
                                }
                            }
                        }
                        else
                        {
                            DataGridView1.DataSource = null;

                            Function.ShowGridViewFormatFromStore(Data, DataGridView1,
                              " ลำดับ", 35, true, mc, mc
                            , "รหัสอ้างอิง", 50, false, mc, mc
                            , "รายการ", 180, true, ml, ml
                            , "ประเภทเงิน", 80, true, ml, ml
                            , "จำนวนเงิน", 70, true, mr, mr
                            , "สถานะ", 0, false, ml, ml
                            , "", 0, false, mc, mc
                            );

                            CurrencyManager Cm = (CurrencyManager)BindingContext[DataGridView1.DataSource];

                            foreach (DataGridViewRow dgvr in DataGridView1.Rows)
                            {
                                if (dgvr.Cells[3].Value.ToString() == "Cash")
                                {
                                    dgvr.DefaultCellStyle.ForeColor = Color.DimGray;
                                }

                                if (!ShowNext && dgvr.Cells[2].Value.ToString() == Strings.Next)
                                {
                                    Cm.SuspendBinding();
                                    dgvr.Visible = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Index == 0)
                        {
                            DataGridView0.DataSource = null;
                        }
                        else
                        {
                            DataGridView1.DataSource = null;
                        }
                    }

                    DataGridView0.Focus();
                }

                if (DataRows <= 0)
                {
                    Message.MessageConfirmation("F", "", Laguage == "th" ? "ไม่พบข้อมูลที่ค้นหา" : "No data found");
                    using (var Mes = new FrmMessagesBoxOK(Message.strOperation, Message.strMes, "OK", Message.strImage))
                    {
                        var result = Mes.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (txtReceipt.Text != "")
            {
                try
                {
                    Clipboard.SetDataObject(txtReceipt.Text, true, 10, 100);
                }
                catch (Exception)
                {

                }
            }
        }

        private void cbbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbStatus.SelectedValue = 1000;
        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                double num = Convert.ToDouble(txtPrice.Text);
                txtPrice.Text = String.Format("{0:n}", num);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void txtPricekeyPress(object sender, KeyPressEventArgs e)
        {
            Event.AmountKeyPress(sender, e, txtPrice);
        }
    }
}