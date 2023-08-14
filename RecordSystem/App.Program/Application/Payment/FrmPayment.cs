using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Database;
using SANSANG.Constant;

namespace RecordSystemApplication.App.Program.Application.Payment
{
    public partial class FrmPayment : Form
    {
        public string Id;
        public string UserId;
        public DateTime DateExpense;
        public string IsDebits;
        public string AppCode = "SAVPM00";
        public string AppName = "FrmPayment";
        public string Error;
        public double Amount;
        private clsBarcode Barcode = new clsBarcode();
        private clsDataList List = new clsDataList();
        private DataListConstant Object = new DataListConstant();
        private CharacterConstant CharType = new CharacterConstant();
        private clsEvent Event = new clsEvent();
        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        private clsFunction Function = new clsFunction();
        private dbConnection db = new dbConnection();
        string[,] Parameter = new string[,] { };
        private OperationConstant Operation = new OperationConstant();
        private StoreConstant Store = new StoreConstant();
        private clsDate Date = new clsDate();
        private DataListConstant DataList = new DataListConstant();
        private clsLog Log = new clsLog();

        public FrmPayment(string Code, string User, DateTime dt, string IsDebit)
        {
            InitializeComponent();
            Id = Code;
            UserId = User;
            DateExpense = dt;
            IsDebits = IsDebit;
        }
        private void FrmPayment_Load(object sender, EventArgs e)
        {
            List.GetLists(cbbMoney2, DataList.MoneyId);
            List.GetLists(cbbMoney3, DataList.MoneyId);
            List.GetLists(cbbMoney4, DataList.MoneyId);
            List.GetLists(cbbMoney5, DataList.MoneyId);

            txtCode.Text = Id;
            pbQrcode.Image = Barcode.QRCode(Id, Color.Black, Color.White, "Q", 3, false);
            Search();
            Payments();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Sum()
        {
            double Sum = 0.00;

            double Amount1 = txtAmount1.Text != "" ? double.Parse(txtAmount1.Text) : 0;
            double Amount2 = txtAmount2.Text != "" ? double.Parse(txtAmount2.Text) : 0;
            double Amount3 = txtAmount3.Text != "" ? double.Parse(txtAmount3.Text) : 0;
            double Amount4 = txtAmount4.Text != "" ? double.Parse(txtAmount4.Text) : 0;
            double Amount5 = txtAmount5.Text != "" ? double.Parse(txtAmount5.Text) : 0;

            Sum = Amount1 + Amount2 + Amount3 + Amount4 + Amount5;

            txtSum.Text = string.Format("{0:#,##0.00}", Sum);
        }

        private void txtAmountkeyPress(object sender, KeyPressEventArgs e)
        {
            Event.AmountKeyPress(sender, e, txtAmount2);
            Event.AmountKeyPress(sender, e, txtAmount3);
            Event.AmountKeyPress(sender, e, txtAmount4);
            Event.AmountKeyPress(sender, e, txtAmount5);
        }

        private void Leaves(object sender, EventArgs e, TextBox txt)
        {
            try
            {
                double num = Convert.ToDouble(txt.Text);
                txt.Text = string.Format("{0:n}", num);
            }
            catch
            {

            }
        }

        private void txtAmount2_Leave(object sender, EventArgs e)
        {
            Leaves(sender, e, txtAmount2);
            Sum();
        }

        private void txtAmount3_Leave(object sender, EventArgs e)
        {
            Leaves(sender, e, txtAmount3);
            Sum();
        }

        private void txtAmount4_Leave(object sender, EventArgs e)
        {
            Leaves(sender, e, txtAmount4);
            Sum();
        }

        private void txtAmount5_Leave(object sender, EventArgs e)
        {
            Leaves(sender, e, txtAmount5);
            Sum();
        }

        private void Search()
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", txtCode.Text},
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
                    {"@Date", ""},
                    {"@Receipt", ""},
                };

                db.Gets(Store.ManageExpense, Parameter, out Error, out ds);

                if (IsDebits.ToUpper() == "TRUE")
                {
                    DataTable dtDebit = ds.Tables[1];

                    if (dtDebit != null && dtDebit.Rows.Count > 0)
                    {
                        cbbPayment1.Text = dtDebit.Rows[0]["Moneys"].ToString() + " x " + 
                                           dtDebit.Rows[0]["Amount"].ToString();

                        txtAmount1.Text = dtDebit.Rows[0]["Amount"].ToString();
                        txtUnit.Text = string.Concat(decimal.Parse(dtDebit.Rows[0]["Unit"].ToString()).ToString("G29"), " ", dtDebit.Rows[0]["Units"].ToString());
             
                        txtSum.Text = dtDebit.Rows[0]["Amount"].ToString();
                        Amount = double.Parse(dtDebit.Rows[0]["Amount"].ToString());

                        string strItem = dtDebit.Rows[0]["Item"].ToString();
                        int strSign = strItem.IndexOf(" - ");
                        txtExpence.Text = strItem.Substring(0, strSign);

                        int strEnd = strItem.Length - ((txtExpence.Text).Length + 3);
                        txtDetail.Text = strItem.Substring(strSign + 3, strEnd);
                    }
                }
                else
                {
                    DataTable dtCredit = ds.Tables[0];

                    if (dtCredit != null && dtCredit.Rows.Count > 0)
                    {
                        cbbPayment1.Text = dtCredit.Rows[0]["Moneys"].ToString() + " x " +
                                           dtCredit.Rows[0]["Amount"].ToString();

                        txtUnit.Text = string.Concat(decimal.Parse(dtCredit.Rows[0]["Unit"].ToString()).ToString("G29"), " ", dtCredit.Rows[0]["Units"].ToString());
                        txtAmount1.Text = dtCredit.Rows[0]["Amount"].ToString();

                        txtSum.Text = dtCredit.Rows[0]["Amount"].ToString();
                        Amount = double.Parse(dtCredit.Rows[0]["Amount"].ToString());

                        string strItem = dtCredit.Rows[0]["Item"].ToString();
                        int strSign = strItem.IndexOf(" - ");
                        txtExpence.Text = strItem.Substring(0, strSign);

                        int strEnd = strItem.Length - ((txtExpence.Text).Length + 3);
                        txtDetail.Text = strItem.Substring(strSign + 3, strEnd);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSum.Text = string.Format("{0:#,##0.00}", Amount);

            cbbMoney2.SelectedIndex = 0;
            cbbMoney3.SelectedIndex = 0;
            cbbMoney4.SelectedIndex = 0;
            cbbMoney5.SelectedIndex = 0;

            txtAmount2.Text = "";
            txtAmount3.Text = "";
            txtAmount4.Text = "";
            txtAmount5.Text = "";

            txtReceipt.Text = "";

            Search();
            Payments();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string[,] Payment = new string[,]
            {
                {"@Id", ""},
                {"@Code", txtCode.Text},
                {"@User", UserId},
                {"@IsActive", "1"},
                {"@IsDelete", "0"},
                {"@Operation", Operation.UpdateAbbr},
                {"@Date", Date.GetDate(dt : DateExpense)},
                {"@Receipt", txtReceipt.Text},
                {"@MoneyId1", "0"},
                {"@Amount1", "0"},
                {"@MoneyId2", cbbMoney2.SelectedValue.ToString()},
                {"@Amount2", Function.MoveNumberStringComma(txtAmount2.Text, true)},
                {"@MoneyId3", cbbMoney3.SelectedValue.ToString()},
                {"@Amount3", Function.MoveNumberStringComma(txtAmount3.Text, true)},
                {"@MoneyId4", cbbMoney4.SelectedValue.ToString()},
                {"@Amount4", Function.MoveNumberStringComma(txtAmount4.Text, true)},
                {"@MoneyId5", cbbMoney5.SelectedValue.ToString()},
                {"@Amount5", Function.MoveNumberStringComma(txtAmount5.Text, true)},
                {"@UpdateType", "PAYMENT"},
            };

            db.Operations(Store.ManagePayments, Payment, out Error);
            Close();
        }

        private void Payments()
        {
            string[,] Payment = new string[,]
            {
                {"@Id", ""},
                {"@Code", Id},
                {"@User", ""},
                {"@IsActive", "1"},
                {"@IsDelete", "0"},
                {"@Operation", Operation.SelectAbbr},
                {"@Date", ""},
                {"@Receipt", ""},
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
                {"@UpdateType", "0"},
            };

            db.Gets(Store.ManagePayments, Payment, out Error, out ds);


            if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
            {
                cbbMoney2.SelectedValue = ds.Tables[2].Rows[0]["MoneyId2"].ToString();
                txtAmount2.Text = ds.Tables[2].Rows[0]["Amount2"].ToString();

                cbbMoney3.SelectedValue = ds.Tables[2].Rows[0]["MoneyId3"].ToString();
                txtAmount3.Text = ds.Tables[2].Rows[0]["Amount3"].ToString();

                cbbMoney4.SelectedValue = ds.Tables[2].Rows[0]["MoneyId4"].ToString();
                txtAmount4.Text = ds.Tables[2].Rows[0]["Amount4"].ToString();

                cbbMoney5.SelectedValue = ds.Tables[2].Rows[0]["MoneyId5"].ToString();
                txtAmount5.Text = ds.Tables[2].Rows[0]["Amount5"].ToString();

                txtReceipt.Text = ds.Tables[2].Rows[0]["Receipt"].ToString();
                Sum();
            }
        }

        private void btnSum_Click(object sender, EventArgs e)
        {
            Sum();
        }

        private void btnRemove2_Click(object sender, EventArgs e)
        {
            cbbMoney2.SelectedIndex = 0;
            txtAmount2.Text = "";
        }

        private void btnRemove3_Click(object sender, EventArgs e)
        {
            cbbMoney3.SelectedIndex = 0;
            txtAmount3.Text = "";
        }

        private void btnRemove4_Click(object sender, EventArgs e)
        {
            cbbMoney4.SelectedIndex = 0;
            txtAmount4.Text = "";
        }

        private void btnRemove5_Click(object sender, EventArgs e)
        {
            cbbMoney5.SelectedIndex = 0;
            txtAmount5.Text = "";
        }

        private void txtReff_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Function.IsCharacter(e.KeyChar, CharType.Tracking))
            {
                e.Handled = true;
            }
            else
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    btnEdit.Focus();
                }

                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    string Codes = Function.SplitBarcode(txtReceipt.Text);
                    txtReceipt.Text = Codes == "" ? txtReceipt.Text : Codes;
                }
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            txtReceipt.Text = Clipboard.GetText();
        }
    }
}




