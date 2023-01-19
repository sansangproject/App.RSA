using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Database;
using SANSANG.Constant;
using System.Drawing;
using System.Diagnostics;
using System.Web.Services.Description;

namespace SANSANG
{
    public partial class FrmCredits : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "SAVCR00";
        public string AppName = "FrmCredits";
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

        public FrmCredits(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
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
            List.GetLists(cbbShop, DataList.ShopId);
            List.GetLists(cbbEdc, DataList.ShopId);

            List.GetLists(cbbCard, DataList.CardId);
            List.GetLists(cbbBank, DataList.BankId);

            Clear();
            gbForm.Enabled = true;
            Timer.Stop();
        }

        public void Clear()
        {
            Function.ClearAll(gbForm);
            Images.ShowDefault(pbCard);

            pb_Date_True.Visible = false;
            pb_Date_False.Visible = true;
            cb_Date.Checked = false;

            pb_Save_True.Visible = false;
            pb_Save_False.Visible = true;
            cb_Save.Checked = false;

            btnCopy.Visible = false;

            pbQrcode.Image = null;
            dtDate.Value = DateTime.Now;
            dtTime.Value = DateTime.Now;
            dtTime.Format = DateTimePickerFormat.Time;
            dtTime.ShowUpDown = true;

            txtUseCredit.Text = "0.00";
            txtSumCredit.Text = "0.00";

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
                {"@CardId", Search? Function.GetComboId(cbbCard) : "0"},
                {"@BankId", Search? Function.GetComboId(cbbBank) : "0"},
                {"@Host", Search? txtHost.Text : ""},
                {"@Tid", Search? txtTid.Text : ""},
                {"@Mid", Search? txtMid.Text : ""},
                {"@Mer", Search? txtMer.Text : ""},
                {"@Edc", Search? Function.GetComboId(cbbEdc) : "0"},
                {"@Product", Search? txtProduct.Text : ""},
                {"@Receipt", Search? txtReceipt.Text : ""},
                {"@Reference", Search? txtReference.Text : ""},
                {"@Stan", Search? txtStan.Text : ""},
                {"@Batch", Search? txtBatch.Text : ""},
                {"@Trace", Search? txtTrace.Text : ""},
                {"@Approve", Search? txtApprove.Text : ""},
                {"@ShopId", Search? Function.GetComboId(cbbShop) : "0"},
                {"@Location", Search? txtLocation.Text : ""},
                {"@Amount", Search? Function.RemoveComma(txtAmount.Text) : "0.00"},
                {"@Date", Search? cb_Date.Checked? Date.GetDate(dtp : dtDate, Format : 4) : "" : ""},
                {"@Time", ""},
                {"@Detail", Search? txtDetail.Text : ""},
                {"@Remark", ""},
                {"@CreditNumber", Setting.GetCredit()},
                {"@CreditDate", Setting.GetCreditDate()},
            };

            string Condition = Function.ShowConditons(GetCondition());
            lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;

            db.Gets(Store.ManageCredit, Parameter, out Error, out ds);
            ShowGridView(ds.Tables[0]);
            SummaryCredit(ds.Tables[1]);
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";

                strCondition += txtCode.Text != "" ? ", รหัสอ้างอิง: " + txtCode.Text : "";

                strCondition += Function.GetComboId(cbbCard) != "0" ? ", บัตรเครดิต: " + cbbCard.Text : "";
                strCondition += Function.GetComboId(cbbShop) != "0" ? ", ร้านค้า/บริการ: " + cbbShop.Text : "";
                strCondition += Function.GetComboId(cbbBank) != "0" ? ", ธนาคาร: " + cbbBank.Text : "";
                strCondition += Function.GetComboId(cbbEdc) != "0" ? ", เครื่องรูดบัตร: " + cbbEdc.Text : "";

                strCondition += txtLocation.Text != "" ? ", สาขา: " + txtLocation.Text : "";
                strCondition += txtDetail.Text != "" ? ", รายละเอียด: " + txtDetail.Text : "";
                strCondition += txtAmount.Text != "" ? ", จำนวนเงิน: " + txtAmount.Text : "";
                strCondition += txtProduct.Text != "" ? ", สินค้า: " + txtProduct.Text : "";
                strCondition += cb_Date.Checked ? ", วันที่: " + dtDate.Text : "";
                strCondition += cb_Date.Checked ? ", เวลา: " + dtTime.Text : "";
                strCondition += txtHost.Text != "" ? ", HOST: " + txtHost.Text : "";
                strCondition += txtTid.Text != "" ? ", TID: " + txtTid.Text : "";
                strCondition += txtMid.Text != "" ? ", MID: " + txtMid.Text : "";
                strCondition += txtMer.Text != "" ? ", MER: " + txtMer.Text : "";
                strCondition += txtReference.Text != "" ? ", REF: " + txtReference.Text : "";
                strCondition += txtStan.Text != "" ? ", STAN: " + txtStan.Text : "";
                strCondition += txtBatch.Text != "" ? ", BATCH: " + txtBatch.Text : "";
                strCondition += txtTrace.Text != "" ? ", TRACE: " + txtTrace.Text : "";
                strCondition += txtApprove.Text != "" ? ", APPR: " + txtApprove.Text : "";
                strCondition += txtReceipt.Text != "" ? ", RECEIPT: " + txtReceipt.Text : "";

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
            try
            {
                GridView.DataSource = null;

                if (Function.GetRows(dt) > 0)
                {
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "SNo", "Dates", "Cards", "Shops", "Location", "Amounts", "Id");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
                    DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                    Function.showGridViewFormatFromStore(dtGrid, GridView,
                          "ลำดับ", 50, true, mc, mc
                        , "วันที่", 80, true, mc, mc
                        , "บัตรเครดิต", 180, true, ml, ml
                        , "สินค้า/บริการ", 120, true, ml, ml
                        , "สาขา", 130, true, ml, ml
                        , "จำนวนเงิน", 120, true, mr, mr
                        , "", 0, false, mc, mc
                        );

                    foreach (DataGridViewRow dgvr in GridView.Rows)
                    {
                        if (Convert.ToDouble(dgvr.Cells[5].Value) < 0)
                        {
                            dgvr.DefaultCellStyle.ForeColor = Color.Red;
                        }
                    }

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
                    {"@CardId", "0"},
                    {"@BankId", "0"},
                    {"@Host", ""},
                    {"@Tid",  ""},
                    {"@Mid", ""},
                    {"@Mer", ""},
                    {"@Edc", "0"},
                    {"@Product", ""},
                    {"@Receipt", ""},
                    {"@Reference", ""},
                    {"@Stan", ""},
                    {"@Batch", ""},
                    {"@Trace", ""},
                    {"@Approve", ""},
                    {"@ShopId", "0"},
                    {"@Location", ""},
                    {"@Amount", "0.00"},
                    {"@Date",  ""},
                    {"@Time", ""},
                    {"@Detail", ""},
                    {"@Remark", ""},
                    {"@CreditNumber", Setting.GetCredit()},
                    {"@CreditDate", ""},
                };

                db.Get(Store.ManageCredit, Parameter, out Error, out dt);
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

                    cbbCard.SelectedValue = dt.Rows[0]["CardId"].ToString();
                    cbbShop.SelectedValue = dt.Rows[0]["ShopId"].ToString();
                    cbbBank.SelectedValue = dt.Rows[0]["BankId"].ToString();
                    cbbEdc.SelectedValue = dt.Rows[0]["Edc"].ToString();

                    txtLocation.Text = dt.Rows[0]["Location"].ToString();
                    txtDetail.Text = dt.Rows[0]["Detail"].ToString();

                    double Amount = Convert.ToDouble(dt.Rows[0]["Amount"].ToString());
                    txtAmount.Text = string.Format("{0:n}", Amount);

                    txtProduct.Text = dt.Rows[0]["Product"].ToString();
                    txtHost.Text = dt.Rows[0]["Host"].ToString();
                    txtTid.Text = dt.Rows[0]["Tid"].ToString();
                    txtMid.Text = dt.Rows[0]["Mid"].ToString();
                    txtMer.Text = dt.Rows[0]["Mer"].ToString();
                    txtReference.Text = dt.Rows[0]["Reference"].ToString();
                    txtStan.Text = dt.Rows[0]["Stan"].ToString();
                    txtBatch.Text = dt.Rows[0]["Batch"].ToString();
                    txtTrace.Text = dt.Rows[0]["Trace"].ToString();
                    txtApprove.Text = dt.Rows[0]["Approve"].ToString();
                    txtReceipt.Text = dt.Rows[0]["Receipt"].ToString();
                    dtDate.Text = dt.Rows[0]["Date"].ToString();
                    dtTime.Text = dt.Rows[0]["Time"].ToString();
                    cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                    pbQrcode.Image = Barcode.QRCode(dt.Rows[0]["Code"].ToString(), Color.Black, Color.White, "Q", 4, false);

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
                if (Function.GetComboId(cbbCard) != "0" && Function.GetComboId(cbbShop) != "0" && !string.IsNullOrEmpty(txtAmount.Text))
                {
                    if (!Function.IsDuplicates(Table.Credits, Date.GetDate(dtp: dtDate, Format: 4), Function.GetComboId(cbbCard), Function.GetComboId(cbbShop), txtAmount.Text, Detail: GetDetails()))
                    {
                        txtCode.Text = Function.GetCodes(Table.ExpenseId, "", "Generated");

                        Parameter = new string[,]
                        {
                            {"@Id", txtId.Text},
                            {"@Code", txtCode.Text},
                            {"@Status", Function.GetComboId(cbbStatus)},
                            {"@User", UserId},
                            {"@IsActive", "1"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@CardId", Function.GetComboId(cbbCard)},
                            {"@BankId", Function.GetComboId(cbbBank)},
                            {"@Host", txtHost.Text},
                            {"@Tid", txtTid.Text},
                            {"@Mid", txtMid.Text},
                            {"@Mer", txtMer.Text},
                            {"@Edc", Function.GetComboId(cbbEdc)},
                            {"@Product", txtProduct.Text},
                            {"@Receipt", txtReceipt.Text},
                            {"@Reference", txtReference.Text},
                            {"@Stan", txtStan.Text},
                            {"@Batch", txtBatch.Text},
                            {"@Trace", txtTrace.Text},
                            {"@Approve", txtApprove.Text},
                            {"@ShopId", Function.GetComboId(cbbShop)},
                            {"@Location", txtLocation.Text},
                            {"@Amount", Function.RemoveComma(txtAmount.Text)},
                            {"@Date", Date.GetDate(dtp : dtDate, Format : 4)},
                            {"@Time", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                            {"@Detail", txtDetail.Text},
                            {"@Remark", ""},
                            {"@CreditNumber", Setting.GetCredit()},
                            {"@CreditDate", ""},
                         };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageCredit, Parameter, txtCode.Text, Details: GetDetails()))
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
                        {"@CardId", Function.GetComboId(cbbCard)},
                        {"@BankId", Function.GetComboId(cbbBank)},
                        {"@Host", txtHost.Text},
                        {"@Tid", txtTid.Text},
                        {"@Mid", txtMid.Text},
                        {"@Mer", txtMer.Text},
                        {"@Edc", Function.GetComboId(cbbEdc)},
                        {"@Product", txtProduct.Text},
                        {"@Receipt", txtReceipt.Text},
                        {"@Reference", txtReference.Text},
                        {"@Stan", txtStan.Text},
                        {"@Batch", txtBatch.Text},
                        {"@Trace", txtTrace.Text},
                        {"@Approve", txtApprove.Text},
                        {"@ShopId", Function.GetComboId(cbbShop)},
                        {"@Location", txtLocation.Text},
                        {"@Amount", Function.RemoveComma(txtAmount.Text)},
                        {"@Date", Date.GetDate(dtp : dtDate, Format : 4)},
                        {"@Time", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                        {"@Detail", txtDetail.Text},
                        {"@Remark", ""},
                        {"@CreditNumber", Setting.GetCredit()},
                        {"@CreditDate", ""},
                    };

                    if (Edit.Update(AppCode, AppName, UserId, Store.ManageCredit, Parameter, txtCode.Text, Details: GetDetails()))
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
                if (Delete.Drop(AppCode, AppName, UserId, 0, Table.Credits, txtCode, Details: GetDetails(), true))
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

        private void CardSelected(object sender, EventArgs e)
        {
            try
            {
                if (!Function.IsDefault(cbbCard))
                {
                    Parameter = new string[,]
                    {
                        {"@Id", Function.GetComboId(cbbCard)},
                        {"@Code", ""},
                        {"@Name", ""},
                        {"@Status", "0"},
                        {"@User", ""},
                        {"@IsActive", ""},
                        {"@IsDelete", ""},
                        {"@Operation", "S"},
                        {"@Number", ""},
                        {"@NameEn", ""},
                        {"@Display", ""},
                        {"@Holder", ""},
                        {"@Provider", ""},
                        {"@Detail", ""},
                        {"@Contact", ""},
                        {"@IsCredit", ""},
                        {"@Type", ""},
                        {"@DateStart", ""},
                        {"@DateEnd", ""},
                        {"@ImageFront", ""},
                        {"@ImageBack", ""},
                    };

                    db.Get(Store.ManageCard, Parameter, out Error, out dt);

                    if (string.IsNullOrEmpty(Error))
                    {
                        txtCardNo.Text = dt.Rows[0]["Number"].ToString();
                        cbbBank.SelectedValue = dt.Rows[0]["Provider"].ToString();
                        cbbStatus.SelectedValue = 1004;
                        Images.ShowImage(pbCard, Referrence: dt.Rows[0]["Id"].ToString());
                        txtAmount.Focus();
                    }
                }
                else
                {
                    Images.ShowDefault(pbCard);
                    txtCardNo.Text = "";
                    cbbBank.SelectedValue = 0;
                }
            }
            catch (Exception)
            {
                Images.ShowDefault(pbCard);
                txtCardNo.Text = "";
                cbbBank.SelectedValue = 0;
            }
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
                    Search(true);
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

        private void SummaryCredit(DataTable Data)
        {
            double Total = 0;
            double TotalUsed = 0;
            double TotalPayment = 0;

            if (string.IsNullOrEmpty(Error) && Function.GetRows(Data) > 0)
            {
                TotalUsed = Convert.ToDouble(string.IsNullOrEmpty(Data.Rows[0]["TotalUsed"].ToString())? "0.00" : Data.Rows[0]["TotalUsed"].ToString());
                TotalPayment = Convert.ToDouble(string.IsNullOrEmpty(Data.Rows[0]["TotalPayment"].ToString()) ? "0.00" : Data.Rows[0]["TotalPayment"].ToString());
            }

            Total = (TotalPayment - Convert.ToDouble(Setting.GetChargeLotus())) - TotalUsed;
            txtSumCredit.Text = string.Format("{0:#,##0.00}", Total);
            txtUseCredit.Text = string.Format("{0:#,##0.00}", TotalUsed);
        }

        public string GetDetails()
        {
            return cbbShop.Text + " | " + txtProduct.Text + " (฿" + txtAmount.Text + ")";
        }
    }
}