using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Database;
using SANSANG.Constant;
using SANSANG.Utilites.App.Forms;
using System.Drawing;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;

namespace SANSANG
{
    public partial class FrmDebts : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "SAVDP00";
        public string AppName = "FrmDebts";
        public string Error = "";
        public string Laguage;

        public string DebtListBefore = "";
        public int IndexList = 0;
        public bool Change = false;
        public string Types = "";

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
        private CharacterConstant CharType = new CharacterConstant();
        private Timer Timer = new Timer();
        public string[,] Parameter = new string[,] { };

        public FrmDebts(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
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
            List.GetLists(cbbMoney, DataList.MoneyId);
            List.GetList(cbbMonth, DataList.MonthId);
            List.GetLists(cbbProvider, DataList.ShopId);
            List.GetLists(cbbLocation, DataList.ShopId);
            List.GetDebitCreditList(cbbType);

            gbForm.Enabled = true;
            Clear();
            Timer.Stop();
        }

        public void Clear()
        {
            Function.ClearAll(gbForm);

            pb_Month_True.Show();
            pb_Month_False.Hide();
            cb_Month.Checked = true;

            pb_Due_True.Hide();
            pb_Due_False.Show();
            cb_Due.Checked = false;

            pb_Pay_True.Hide();
            pb_Pay_False.Show();
            cb_Pay.Checked = false;

            txtTotal.Text = "0.00";
            txtCredit.Text = "0.00";
            txtDebit.Text = "0.00";
            txtPayment.Text = "0.00";
            lblService.Text = "";

            dtDueDate.Text = Convert.ToString(DateTime.Now);
            dtPaymentDate.Text = Convert.ToString(DateTime.Now);
            cbbMonth.SelectedValue = GetCurrentList();
            pbProvider.Image = null;

            Images.ShowDefault(pbProvider);
            SetPayment();
            cbbType.SelectedValue = "9";
            Change = false;

            Search(false);
        }

        public void Search(bool Search)
        {
            Parameter = new string[,]
            {
                {"@Id", Search? txtId.Text : ""},
                {"@Code", Search? txtCode.Text : ""},
                {"@Name", Search? txtName.Text : ""},
                {"@Status", Search? Function.GetComboId(cbbStatus) : "0"},
                {"@User", ""},
                {"@IsActive", "1"},
                {"@IsDelete", "0"},
                {"@Operation", Operation.SelectAbbr},
                {"@List", Search? cb_Month.Checked == true? Function.GetComboId(cbbMonth) : "" : GetCurrentList()},
                {"@Year", ""},
                {"@Month", ""},
                {"@IsDebit", Search? Function.GetComboId(cbbType) == "9"? "" : Function.GetComboId(cbbType) : ""},
                {"@Account", Search? txtAccount.Text : ""},
                {"@Provider", Search? Function.GetComboId(cbbProvider) : "0"},
                {"@Detail", Search? txtDetail.Text : ""},
                {"@Price", Search? Function.RemoveComma(txtAmount.Text) : "0.00"},
                {"@Pay", Search? Function.RemoveComma(txtPayment.Text) : "0.00"},
                {"@DueDate", Search? cb_Due.Checked == true? Date.GetDate(dtp : dtDueDate, Format : 4) : "" : ""},
                {"@PayDate", Search? cb_Pay.Checked == true? Date.GetDate(dtp : dtPaymentDate, Format : 4) : "" : ""},
                {"@Location", Search? Function.GetComboId(cbbLocation) : "0"},
                {"@MoneyId", Search? Function.GetComboId(cbbMoney) : "0"},
                {"@Receipt", Search? txtReceipt.Text : ""},
                {"@Reference", Search? txtReference.Text : ""},
            };

            string Condition = Function.ShowConditons(GetCondition());
            lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;
            
            if (lblCondition.Text != "ทั้งหมด")
            {
                db.Get(Store.ManageDept, Parameter, out Error, out dt);
                ShowGridView(dt);
            }
        }

        public void ShowGridView(DataTable dt)
        {
            try
            {
                double Debit = 0;
                double Credit = 0;
                
                if (Function.GetRows(dt) == 0)
                {
                    txtCount.Text = Function.ShowNumberOfData(0);
                    GridView.DataSource = null;

                    Message.MessageConfirmation("F", "", Laguage == "th" ? "ไม่พบข้อมูลที่ค้นหา" : "No data found");

                    using (var Mes = new FrmMessagesBoxOK(Message.strOperation, Message.strMes, "OK", Message.strImage))
                    {
                        var result = Mes.ShowDialog();
                    }

                    txtCode.Focus();
                }
                else
                {
                    DataTable Data = new DataTable();
                    Data = dt.DefaultView.ToTable(true, "SNo", "Names", "Shops", "StatusNames", "Dates", "Amounts", "Id");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
                    DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                    Function.showGridViewFormatFromStore(Data, GridView,
                          " ลำดับ", 30, true, mc, mc
                        , "รายการ", 150, true, ml, ml
                        , "ห้างร้าน | ธนาคาร | บุคคล", 120, true, ml, ml
                        , "สถานะ", 100, true, ml, ml
                        , "กำหนดชำระ", 100, true, mc, mc
                        , "จำนวนเงิน", 100, true, mr, mr
                        , "", 0, false, mc, mc
                     );

                    foreach (DataGridViewRow dgvr in GridView.Rows)
                    {
                        Types = dgvr.Cells[1].Value.ToString().Substring(0, 1);

                        if (Types == "+")
                        {
                            Debit += Convert.ToDouble(dgvr.Cells[5].Value);
                        }
                        else
                        {
                            if ((dgvr.Cells[3].Value.ToString() != "ฟรี")
                             && (dgvr.Cells[3].Value.ToString() != "ยกเลิก")
                             && (dgvr.Cells[3].Value.ToString() != "พักชำระ"))
                            {
                                Credit += Convert.ToDouble(dgvr.Cells[5].Value);
                            }
                        }

                        if (dgvr.Cells[3].Value.ToString() == "*เกินกำหนดชำระ")
                        {
                            dgvr.DefaultCellStyle.ForeColor = Color.Firebrick;
                        }
                        else if (dgvr.Cells[3].Value.ToString() == "*โปรดชำระวันนี้")
                        {
                            dgvr.DefaultCellStyle.ForeColor = Color.Salmon;
                        }
                        else if (dgvr.Cells[3].Value.ToString() == "ชำระแล้ว ")
                        {
                            dgvr.DefaultCellStyle.ForeColor = Color.DarkOrange;
                        }
                        else if (dgvr.Cells[3].Value.ToString() == "ชำระแล้ว  ")
                        {
                            dgvr.DefaultCellStyle.ForeColor = Color.Teal;
                        }
                        else if (dgvr.Cells[3].Value.ToString() == "ฟรี")
                        {
                            dgvr.DefaultCellStyle.ForeColor = Color.Teal;
                        }
                        else if (dgvr.Cells[3].Value.ToString() == "พักชำระ")
                        {
                            dgvr.DefaultCellStyle.ForeColor = Color.Teal;
                        }
                        else if (dgvr.Cells[3].Value.ToString() == "ยกเลิก")
                        {
                            dgvr.DefaultCellStyle.ForeColor = Color.LightGray;
                        }

                        Types = "";
                    }

                    txtDebit.Text = String.Format("{0:n}", Debit);
                    txtCredit.Text = String.Format("{0:n}", Credit);
                    txtTotal.Text = String.Format("{0:n}", Debit - Credit);

                    if (Debit >= Credit)
                    {
                        txtTotal.ForeColor = Color.Green;
                    }
                    else
                    {
                        txtTotal.ForeColor = Color.Red;
                    }

                    txtCount.Text = Function.ShowNumberOfData(Function.GetRows(dt));
                    GridView.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";

                strCondition += txtCode.Text != "" ? ", รหัสอ้างอิง: " + txtCode.Text : "";
                strCondition += cb_Month.Checked == true ? ", รอบบิล: " + cbbMonth.Text : "";
                strCondition += txtName.Text != "" ? ", รายการ: " + txtName.Text : "";
                strCondition += Function.GetComboId(cbbProvider) != "0" ? ", ผู้ให้บริการ: " + cbbProvider.Text : "";
                strCondition += txtAccount.Text != "" ? ", บัญชี: " + txtAccount.Text : "";
                strCondition += Function.GetComboId(cbbType) != "9" ? ", ประเภท: " + cbbType.Text : "";
                strCondition += txtAmount.Text != "" ? ", จำนวนเงิน: " + txtAmount.Text : "";
                strCondition += cb_Due.Checked == true ? ", วันที่ต้องชำระ: " + Date.GetDate(dtp: dtDueDate, Format: 4) : "";
                strCondition += txtPayment.Text != "" ? ", ยอดชำระ: " + txtPayment.Text : "";
                strCondition += cb_Pay.Checked == true ? ", วันที่ชำระ: " + Date.GetDate(dtp: dtPaymentDate, Format: 4) : "";
                strCondition += txtDetail.Text != "" ? ", รายละเอียด: " + txtDetail.Text : "";
                strCondition += txtReceipt.Text != "" ? ", ใบเสร็จ: " + txtReceipt.Text : "";
                strCondition += txtReference.Text != "" ? ", เลขอ้างอิง: " + txtReference.Text : "";
                strCondition += Function.GetComboId(cbbLocation) != "0" ? ", สถานที่ชำระ: " + cbbLocation.Text : "";
                strCondition += Function.GetComboId(cbbMoney) != "0" ? ", ช่องทางชำระ: " + cbbMoney.Text : "";
                strCondition += Function.GetComboId(cbbStatus) != "0" ? ", สถานะ: " + cbbStatus.Text : "";

                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return "";
            }
        }

        public string GetCurrentList()
        {
            string value = "";

            if (string.IsNullOrEmpty(Setting.GetRoundPayment()))
            {

                value = DateTime.Today.ToString("yyyy", CultureInfo.GetCultureInfo("th-TH")) + DateTime.Today.ToString("MM", Function.SetFormatDate("en"));
            }
            else
            {
                value = Setting.GetRoundPayment();
            }

            return value;
        }

        public string GetBeforeList(int Type)
        {
            string Dates = "";

            if (Type == 1)
            {
                DateTime date = new DateTime();
                date = DateTime.Today.AddMonths(-1);
                Dates = date.ToString("yyyy", Function.SetFormatDate("TH")) + date.ToString("MM", Function.SetFormatDate("EN"));
            }
            else
            {
                Dates = Function.getComboboxValueBefore(cbbMonth);
            }

            return Dates;
        }

        public void SetPayment()
        {
            try
            {
                if (Function.getComboboxId(cbbStatus) == "1004")
                {
                    txtPayment.Enabled = true;
                    cbbMoney.Enabled = true;
                    txtPayment.Enabled = true;
                    cbbLocation.Enabled = true;
                    btnPayment.Enabled = true;
                    btnPayment.Focus();
                }
                else
                {
                    txtPayment.Enabled = false;
                    cbbMoney.SelectedIndex = 0;
                    cbbMoney.Enabled = false;
                    txtPayment.Enabled = false;
                    cbbLocation.SelectedIndex = 0;
                    txtPayment.Text = "";
                    cbbLocation.Enabled = false;
                    btnPayment.Enabled = false;
                    btnPayment.Focus();
                }
            }
            catch (Exception)
            {

            }
        }

        private void AddData(object sender, EventArgs e)
        {
            try
            {
                DateTime DueDate = dtDueDate.Value;
                ThaiBuddhistCalendar ThaiDate = new ThaiBuddhistCalendar();

                string Years = ThaiDate.GetYear(DueDate).ToString();
                string Months = ThaiDate.GetMonth(DueDate).ToString("D2");
                string Days = ThaiDate.GetDayOfMonth(DueDate).ToString();

                string Lists = string.Concat(Years, Months);

                if (!string.IsNullOrEmpty(txtAccount.Text)
                 && !string.IsNullOrEmpty(txtName.Text)
                 && cbbMonth.SelectedValue.ToString() != "0"
                 && cbbProvider.SelectedValue.ToString() != "0"
                 && cbbStatus.SelectedValue.ToString() != "0"
                 && cbbType.SelectedValue.ToString() != "00")
                {
                    if (!Function.IsDuplicates(Table.Depts, Lists, txtAccount.Text, txtName.Text, Function.GetComboId(cbbProvider), Function.RemoveComma(txtAmount.Text),
                        Detail: GetDetails() + Environment.NewLine + "in " + Months + " " + Years))
                    {
                        txtCode.Text = Function.GetCodes(Table.TrackingsId, "1049", "Generated");

                        Parameter = new string[,]
                        {
                            {"@Id", ""},
                            {"@Code", txtCode.Text},
                            {"@Name", txtName.Text},
                            {"@Status", Function.GetComboId(cbbStatus)},
                            {"@User", UserId},
                            {"@IsActive", "1"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@List", Lists},
                            {"@Year", Years},
                            {"@Month", Months},
                            {"@IsDebit", Function.GetComboId(cbbType)},
                            {"@Account", txtAccount.Text},
                            {"@Provider", Function.GetComboId(cbbProvider)},
                            {"@Detail", txtDetail.Text},
                            {"@Price", Function.RemoveComma(txtAmount.Text)},
                            {"@Pay", Function.GetComboId(cbbStatus) == "1004"? Function.RemoveComma(txtPayment.Text) : "0.00"},
                            {"@DueDate", Date.GetDate(dtp : dtDueDate, Format : 4)},
                            {"@PayDate", Function.GetComboId(cbbStatus) == "1004"? Date.GetDate(dtp : dtPaymentDate, Format : 4) : ""},
                            {"@Location", Function.GetComboId(cbbLocation)},
                            {"@MoneyId", Function.GetComboId(cbbMoney)},
                            {"@Receipt", txtReceipt.Text},
                            {"@Reference", txtReference.Text},
                        };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageDept, Parameter, txtCode.Text, Details: GetDetails()))
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
                        {"@Name", txtName.Text},
                        {"@Status", Function.GetComboId(cbbStatus)},
                        {"@User", UserId},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.UpdateAbbr},
                        {"@List", ""},
                        {"@Year", ""},
                        {"@Month", ""},
                        {"@IsDebit", Function.GetComboId(cbbType)},
                        {"@Account", txtAccount.Text},
                        {"@Provider", Function.GetComboId(cbbProvider)},
                        {"@Detail", txtDetail.Text},
                        {"@Price", Function.RemoveComma(txtAmount.Text)},
                        {"@Pay", Function.GetComboId(cbbStatus) != "1005"? Function.RemoveComma(txtPayment.Text) : "0.00"},
                        {"@DueDate", Date.GetDate(dtp : dtDueDate, Format : 4)},
                        {"@PayDate", Function.GetComboId(cbbStatus) != "1005"? Date.GetDate(dtp : dtPaymentDate, Format : 4) : ""},
                        {"@Location", Function.GetComboId(cbbLocation)},
                        {"@MoneyId", Function.GetComboId(cbbMoney)},
                        {"@Receipt", txtReceipt.Text},
                        {"@Reference", txtReference.Text},
                    };

                    if (Edit.Update(AppCode, AppName, UserId, Store.ManageDept, Parameter, txtCode.Text, Details: GetDetails()))
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
                if (Delete.Drop(AppCode, AppName, UserId, 0, Table.Depts, txtCode, Details: GetDetails(), true))
                {
                    Clear();
                }
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

        private void Exit(object sender, EventArgs e)
        {
            Close();
        }

        private void SearchData(object sender, EventArgs e)
        {
            Search(true);
        }

        private void txtPay_Leave(object sender, EventArgs e)
        {
            CheckPay();
        }

        public void CheckPay()
        {
            try
            {
                double Price = 0;
                double Pay = 0;
                double Diff = 0;

                if (txtAmount.Text != "")
                {
                    Price = Convert.ToDouble(txtAmount.Text);
                }
                else
                {
                    Price = 0;
                }

                if (txtPayment.Text != "")
                {
                    Pay = Convert.ToDouble(txtPayment.Text);
                }
                else
                {
                    Pay = 0;
                }

                Diff = Pay - Price;

                if (cbbType.SelectedIndex == 1)
                {
                    if (Diff < 0)
                    {
                        if (cbbStatus.Text == "ฟรี")
                        {
                            lblService.Visible = false;
                            lblService.Text = "";
                        }
                        else if (cbbStatus.Text == "พักชำระ")
                        {
                            lblService.Visible = false;
                            lblService.Text = "";
                        }
                        else
                        {
                            lblService.Visible = true;
                            lblService.Text = "*** ค่าธรรมเนียม " + Function.FormatNumber(Diff * -1) + " บาท";
                        }
                    }
                    else
                    {
                        lblService.Visible = false;
                        lblService.Text = "";
                    }
                }
                else
                {
                    if (Diff >= 0.01)
                    {
                        if (new[] { 8.00, 10.00, 13.00, 15.00, 25.00, 30.00 }.Contains(Diff))
                        {
                            lblService.Visible = true;
                            lblService.Text = "*** ค่าบริการ " + Function.FormatNumber(Diff) + " บาท";
                        }
                        else
                        {
                            lblService.Visible = true;
                            lblService.Text = "*** ชำระเกิน/ค่าธรรมเนียม " + Function.FormatNumber(Diff) + " บาท";
                        }
                    }
                    else if (Diff < 0)
                    {
                        if (cbbStatus.Text == "ฟรี")
                        {
                            lblService.Visible = false;
                            lblService.Text = "";
                        }
                        else if (cbbStatus.Text == "พักชำระ")
                        {
                            lblService.Visible = false;
                            lblService.Text = "";
                        }
                        else if (cbbStatus.Text == "ยกเลิก")
                        {
                            lblService.Visible = false;
                            lblService.Text = "";
                        }
                        else
                        {
                            string statusPayment = "";

                            //if (txtName.Text.Contains("สินเชื่อบุคคล") || txtName.Text.Contains("บัตรเครดิต"))
                            //{
                            //    statusPayment = "*** ยอดค้างชำระ ";
                            //}
                            //else
                            //{
                            //    statusPayment = "*** ส่วนลด ";
                            //}

                            statusPayment = "*** ส่วนลด ";

                            lblService.Visible = true;
                            lblService.Text = string.Concat(statusPayment,Function.FormatNumber(Diff * -1)," บาท");
                        }
                    }
                    else
                    {
                        lblService.Visible = false;
                        lblService.Text = "";
                    }
                }
            }
            catch (Exception)
            {
                lblService.Visible = false;
                lblService.Text = "";
            }
        }

        public void SetFormat(TextBox TextBox)
        {
            try
            {
                double Number = Convert.ToDouble(TextBox.Text);
                TextBox.Text = string.Format("{0:n}", Number);
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
                    Form Frm = (Form)sender;

                    if (Frm.ActiveControl.Text != txtReference.Text && Frm.ActiveControl.Name != "txtDetail")
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

        private void cbbLogo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Function.IsDefault(cbbProvider))
            {
                Images.Show(pbProvider, Function.GetComboId(cbbProvider));
            }
            else
            {
                Images.ShowDefault(pbProvider);
            }

            btnSearch.Focus();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            Event.AmountKeyPress(sender, e, txtAmount);
        }

        private void txtPrice_Leave(object sender, EventArgs e)
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

        private void cbbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Function.getComboboxId(cbbStatus) == "1004")
            {
                if (txtPayment.Text == "" || txtPayment.Text == "0.00")
                {
                    txtPayment.Text = txtAmount.Text;
                    btnPayment.Focus();
                }
            }
            else if (Function.getComboboxId(cbbStatus) == "1008")
            {
                txtPayment.Clear();
                btnPayment.Focus();
            }
            else
            {
                txtPayment.Text = "0.00";
                txtReceipt.Text = "";
                txtReference.Text = "";
                btnPayment.Focus();
            }

            SetPayment();
        }

        private void CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.GridView.Rows[e.RowIndex];
                    Change = true;
                    lblService.Text = "";
                    Parameter = new string[,]
                    {
                        {"@Id", row.Cells["Id"].Value.ToString()},
                        {"@Code", ""},
                        {"@Name", ""},
                        {"@Status", "0"},
                        {"@User", ""},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.SelectAbbr},
                        {"@List", ""},
                        {"@Year", ""},
                        {"@Month", ""},
                        {"@IsDebit", ""},
                        {"@Account", ""},
                        {"@Provider", "0"},
                        {"@Detail", ""},
                        {"@Price", "0.00"},
                        {"@Pay", "0.00"},
                        {"@DueDate", ""},
                        {"@PayDate", ""},
                        {"@Location", "0"},
                        {"@MoneyId", "0"},
                        {"@Receipt", ""},
                        {"@Reference", ""},
                    };

                    db.Get(Store.ManageDept, Parameter, out Error, out dt);
                    ShowData(dt);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            Event.AmountKeyPress(sender, e, txtPayment);
        }

        private void txtDetail_TextChanged(object sender, EventArgs e)
        {
            Chang(false);
        }

        public void Chang(bool status)
        {
            Change = status;
        }

        private void cbbPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chang(false);
        }

        private void cbbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            Chang(false);
            btnPayment.Focus();
        }

        private void Next(object sender, EventArgs e)
        {
            int itemMonth = cbbMonth.Items.Count - 1;
            int IndexCurrentMonth = Convert.ToInt32(cbbMonth.SelectedIndex.ToString());

            if (IndexCurrentMonth != itemMonth && IndexCurrentMonth < itemMonth)
            {
                Function.ClearAll(gbForm);
                cbbType.SelectedValue = 9;
                cbbMonth.SelectedIndex = IndexCurrentMonth + 1;
                cb_Month.Checked = true;
                Search(true);
            }
        }

        private void Previous(object sender, EventArgs e)
        {
            int IndexCurrentMonth = Convert.ToInt32(cbbMonth.SelectedIndex.ToString());

            if (IndexCurrentMonth != 1 && IndexCurrentMonth > 1)
            {
                Function.ClearAll(gbForm);
                cbbType.SelectedValue = 9;
                cbbMonth.SelectedIndex = IndexCurrentMonth - 1;
                cb_Month.Checked = true;
                Search(true);
            }
        }

        private void SearchName(object sender, EventArgs e)
        {
            Parameter = new string[,]
            {
                {"@Id", ""},
                {"@Code", ""},
                {"@Name", txtName.Text},
                {"@Status", "0"},
                {"@User", ""},
                {"@IsActive", "1"},
                {"@IsDelete", "0"},
                {"@Operation", Operation.SelectAbbr},
                {"@List", ""},
                {"@Year", ""},
                {"@Month", ""},
                {"@IsDebit", ""},
                {"@Account", ""},
                {"@Provider", "0"},
                {"@Detail", ""},
                {"@Price", "0.00"},
                {"@Pay", "0.00"},
                {"@DueDate", ""},
                {"@PayDate", ""},
                {"@Location", "0"},
                {"@MoneyId", "0"},
                {"@Receipt", ""},
                {"@Reference", ""},
            };

            db.Get(Store.ManageDept, Parameter, out Error, out dt);
            ShowGridView(dt);
        }

        private void AutoAddData(object sender, EventArgs e)
        {
            try
            {
                int Round = clsSetting.GetInstallments();
                Message.MessageConfirmation("I", "Auto " + Round + " Months", txtName.Text + " (" + txtAmount.Text + ")");

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();

                        if (!string.IsNullOrEmpty(txtAccount.Text) &&
                            !string.IsNullOrEmpty(txtName.Text) &&
                            cbbMonth.SelectedValue.ToString() != "0" &&
                            cbbProvider.SelectedValue.ToString() != "0" &&
                            cbbStatus.SelectedValue.ToString() != "0" &&
                            cbbType.SelectedValue.ToString() != "00")
                        {

                            for (int i = 1; i <= Round; i++)
                            {
                                string Lists = "";
                                DateTimePicker dtList = new DateTimePicker();
                                dtList.Value = dtDueDate.Value.AddMonths(i);
                                DateTime Dates = dtList.Value;

                                ThaiBuddhistCalendar th = new ThaiBuddhistCalendar();

                                string Years = th.GetYear(Dates).ToString();
                                string Months = th.GetMonth(Dates).ToString("D2");
                                string Days = th.GetDayOfMonth(Dates).ToString();

                                Lists = string.Concat(Years, Months);
                                  
                                string Codes = Function.GetCodes(Table.TrackingsId, "1049", "Generated");

                                if (Codes != "" && !Function.IsDuplicates(Table.Depts, Lists, txtAccount.Text, txtName.Text, Function.GetComboId(cbbProvider), Detail: "Bill " + txtName.Text + " (" + Lists + ") duplicate."))
                                {
                                    Parameter = new string[,]
                                    {
                                        {"@Id", ""},
                                        {"@Code", Codes},
                                        {"@Name", txtName.Text},
                                        {"@Status", Function.GetComboId(cbbStatus)},
                                        {"@User", UserId},
                                        {"@IsActive", "1"},
                                        {"@IsDelete", "0"},
                                        {"@Operation", Operation.InsertAbbr},
                                        {"@List", Lists},
                                        {"@Year", Years},
                                        {"@Month", Months},
                                        {"@IsDebit", Function.GetComboId(cbbType)},
                                        {"@Account", txtAccount.Text},
                                        {"@Provider", Function.GetComboId(cbbProvider)},
                                        {"@Detail", txtDetail.Text},
                                        {"@Price", Function.RemoveComma(txtAmount.Text)},
                                        {"@Pay", "0.00"},
                                        {"@DueDate", Date.GetDate(dtp : dtList, Format : 4)},
                                        {"@PayDate", ""},
                                        {"@Location", "0"},
                                        {"@MoneyId", "0"},
                                        {"@Receipt", ""},
                                        {"@Reference", ""},
                                    };

                                    db.Operations(Store.ManageDept, Parameter, out Error);
                                    Error += Error;
                                    Codes = "";
                                }

                                Task.Delay(1000).Wait();
                            }

                            if (string.IsNullOrEmpty(Error))
                            {
                                Message.MessageResult(Operation.InsertAbbr, "C");
                                Clear();
                            }
                            else
                            {
                                Message.MessageResult(Operation.InsertAbbr, "E");
                            }
                        }
                        else
                        {
                            Message.ShowRequestData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                Message.MessageResult(Operation.InsertAbbr, "E");
            }
        }

        private void Ticker(object sender, EventArgs e)
        {
            Helper.CheckboxTicker(sender, this);
        }

        private void btnTaq_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                //FrmPrintTag Frm = new FrmPrintTag(txtCode.Text);
                //Frm.ShowDialog();
            }
        }

        public void ShowData(DataTable Data)
        {
            try
            {
                if (Function.GetRows(Data) > 0)
                {
                    txtId.Text = Data.Rows[0]["Id"].ToString();
                    txtCode.Text = Data.Rows[0]["Code"].ToString();
                    txtName.Text = Data.Rows[0]["Name"].ToString();
                    txtDetail.Text = Data.Rows[0]["Detail"].ToString();
                    txtAccount.Text = Data.Rows[0]["Account"].ToString();
                    txtReference.Text = Data.Rows[0]["Reference"].ToString();

                    double Price = Convert.ToDouble(Data.Rows[0]["Price"].ToString());
                    txtAmount.Text = string.Format("{0:n}", Price);

                    double Amount = Convert.ToDouble(Data.Rows[0]["Pay"].ToString());
                    txtPayment.Text = Amount == 0? "" : string.Format("{0:n}", Amount);

                    dtPaymentDate.Text = Data.Rows[0]["Status"].ToString() == "1005" ? Convert.ToString(DateTime.Now) : Data.Rows[0]["PayDate"].ToString();
                    dtDueDate.Text = Data.Rows[0]["DueDate"].ToString();

                    cbbLocation.SelectedValue = Data.Rows[0]["Location"].ToString();
                    cbbStatus.SelectedValue = Data.Rows[0]["Status"].ToString();
                    cbbMoney.SelectedValue = Data.Rows[0]["MoneyId"].ToString();
                    cbbType.SelectedValue = Data.Rows[0]["Type"].ToString();
                    cbbMonth.SelectedValue = Data.Rows[0]["List"].ToString();

                    cbbProvider.SelectedValue = Data.Rows[0]["Provider"].ToString();

                    if (Function.GetComboBoxId(cbbStatus) != "1005")
                    {
                        CheckPay();
                    }

                    txtReceipt.Text = Data.Rows[0]["Receipt"].ToString();
                    GridView.Focus();
                }
                else
                {
                    Clear();
                }

            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void Payment(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text != "")
                {
                    Parameter = new string[,]
                    {
                        {"@Id", txtId.Text},
                        {"@Code", ""},
                        {"@Name", ""},
                        {"@Status", Function.GetComboId(cbbStatus)},
                        {"@User", UserId},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.PaymentAbbr},
                        {"@List", ""},
                        {"@Year", ""},
                        {"@Month", ""},
                        {"@IsDebit", ""},
                        {"@Account", ""},
                        {"@Provider", ""},
                        {"@Detail", txtDetail.Text},
                        {"@Price", ""},
                        {"@Pay", Function.GetComboId(cbbStatus) == "1004"? Function.RemoveComma(txtPayment.Text) : "0.00"},
                        {"@DueDate", ""},
                        {"@PayDate", Function.GetComboId(cbbStatus) == "1004"? Date.GetDate(dtp : dtPaymentDate, Format : 4) : ""},
                        {"@Location", Function.GetComboId(cbbLocation)},
                        {"@MoneyId", Function.GetComboId(cbbMoney)},
                        {"@Receipt", txtReceipt.Text},
                        {"@Reference", txtReference.Text},
                    };

                    if (Edit.Pay(AppCode, AppName, UserId, Store.ManageDept, Parameter, txtCode.Text, Details: GetDetails()))
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

        public string GetDetails()
        {
            return txtName.Text + " - " + txtAmount.Text;
        }

        public void CopyData(object sender, EventArgs e)
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

        private void txtReference_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Function.IsCharacter(e.KeyChar, CharType.Tracking))
            {
                e.Handled = true;
            }
            else if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                string Codes = Function.SplitBarcode(txtReference.Text);
                txtReference.Text = Codes == "" ? txtReference.Text : Codes;
                Search(true);
            }
        }
    }
}