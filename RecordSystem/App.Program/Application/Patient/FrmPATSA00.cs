using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;
using SANSANG.Constant;
using SANSANG.Utilites.App.Forms;
using SANSANG.Utilites.App.Model;

namespace SANSANG
{
    public partial class FrmPATSA00 : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string strAppCode = "PATSA00";
        public string strAppName = "FrmPATSA00";

        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string strFilePath = "P4-8BE9741649-7";
        public string strFileName = "-";
        public string strFileType = ".jpg";
        public int row = 0;
        public double sum = 0;

        private clsInsert Insert = new clsInsert();
        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();
        private clsDate Date = new clsDate();
        private clsSearch Search = new clsSearch();
        private clsFunction Fn = new clsFunction();
       private dbConnection db = new dbConnection();
        private clsMessage Message = new clsMessage();
        private clsEvent Event = new clsEvent();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();
        private Timer timer = new Timer();

        public string[,] Parameter = new string[,] { };

        public FrmPATSA00(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            try
            {
                InitializeComponent();
                strUserId = userIdLogin;
                strUserName = userNameLogin;
                strUserSurname = userSurNameLogin;
                strUserType = userTypeLogin;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void FrmPATSA00_Load(object sender, EventArgs e)
        {
            try
            {
                int sec = 1;
                timer.Interval = (sec * 1000);
                timer.Tick += new EventHandler(LoadList);
                timer.Start();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetList(cbbStatus, "1", "Status");
            List.GetList(cbbHospital, "Y", "HospitalId");
            List.GetList(cbbPatital, "Y", "PatientId");
            List.GetList(cbbMoney, "Y", "Money");

            cbbStatus.Enabled = true;
            cbbMoney.Enabled = true;
            gbMenu.Enabled = true;
            Clear();
            timer.Stop();
        }

        public void Clear()
        {
            try
            {
                cbbPatital.SelectedValue = 0;
                cbbHospital.SelectedValue = 0;
                cbbStatus.SelectedValue = 0;
                cbbMoney.SelectedValue = 0;

                lblSearch.Text = "";
                txtName.Text = "";
                txtCode.Text = "";
                txtBookId.Text = "";
                txtBookNumber.Text = "";
                txtAmount.Text = "";
                txtAmountText.Text = "";
                txtPayee.Text = "";
                txtFileLocation.Text = "";
                dtDate.Value = DateTime.Today;

                txtItem1.Text = "";
                txtItem2.Text = "";
                txtItem3.Text = "";
                txtItem4.Text = "";
                txtItem5.Text = "";

                txtAmount1.Text = "";
                txtAmount2.Text = "";
                txtAmount3.Text = "";
                txtAmount4.Text = "";
                txtAmount5.Text = "";

                txtItem1.Enabled = true;
                txtAmount1.Enabled = true;
                txtItem2.Enabled = false;
                txtAmount2.Enabled = false;
                txtItem3.Enabled = false;
                txtAmount3.Enabled = false;
                txtItem4.Enabled = false;
                txtAmount4.Enabled = false;
                txtItem5.Enabled = false;
                txtAmount5.Enabled = false;

                sum = 0.00;
                SearchData();
                btnSearch.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        public void getDataGrid(DataTable dt)
        {
            try
            {
                int row = dt.Rows.Count;

                if (row == 0)
                {
                    dataGridView.DataSource = null;
                    picExcel.Visible = false;
                    lblCount.Text = "0";
                }
                else
                {
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "Date", "RcptId", "Patient", "MsHospitalName", "Amounts", "RcptDentalCode");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
                    DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                    Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                          "ลำดับ", 50, true, mc, mc
                        , "วันที่", 120, true, ml, ml
                        , "เลขที่ใบเสร็จ", 200, true, ml, ml
                        , "ผู้ป่วย", 250, true, ml, ml
                        , "สถาพยาบาล", 300, true, ml, ml
                        , "จำนวนเงิน", 150, true, mc, mr
                        , "", 0, false, mc, mc
                        );

                    picExcel.Visible = true;
                    lblCount.Text = row.ToString();
                }
            }
            catch (Exception)
            {
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
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@RcptDentalCode", txtCode.Text},
                    {"@MsPatientCode", cbbPatital.SelectedValue.ToString()},
                    {"@RcptDentalBookId", txtBookId.Text},
                    {"@RcptDentalBookNumber", txtBookNumber.Text},
                    {"@RcptDentalDate", ""},
                    {"@RcptDentalAmount", ""},
                    {"@RcptDentalPayee", txtPayee.Text},
                    {"@RcptDentalStatus", cbbStatus.SelectedValue.ToString()},
                    {"@RcptDentalPay", cbbMoney.SelectedValue.ToString()}
                };

                db.Get("Spr_S_TblRecieptPatient", Parameter, out strErr, out dt);
                getDataGrid(dt);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAmount.Text != "" & txtBookNumber.Text != "")
                {
                    txtCode.Text = Fn.GetCodes("107", "", "Generated");
                    strOpe = "I";

                    string[,] Parameter = new string[,]
                    {
                        {"@RcptDentalCode",txtCode.Text},
                        {"@MsPatientCode", cbbPatital.SelectedValue.ToString()},
                        {"@RcptDentalBookId", txtBookId.Text},
                        {"@RcptDentalBookNumber", txtBookNumber.Text},
                        {"@RcptDentalDate", Date.GetDate(dtp : dtDate)},
                        {"@RcptDentalTime", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                        {"@RcptDentalAmount", Fn.MoveNumberStringComma(txtAmount.Text)},
                        {"@RcptDentalAmountText", txtAmountText.Text},
                        {"@RcptDentalPayee", txtPayee.Text},
                        {"@RcptDentalFileLocation", txtFileLocation.Text},
                        {"@RcptDentalStatus", cbbStatus.SelectedValue.ToString()},
                        {"@RcptDentalPay", cbbMoney.SelectedValue.ToString()},
                        {"@User", strUserId},
                    };

                    Message.MessageConfirmation(strOpe, txtCode.Text, "ใบเสร็จเลขที่ " + txtBookId.Text + "/" + txtBookNumber.Text + " (" + txtAmount.Text + " บาท)");

                    using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                    {
                        var result = Mes.ShowDialog();

                        if (result == DialogResult.Yes)
                        {
                            Mes.Close();
                            db.Operations("Spr_I_TblRecieptDental", Parameter, out strErr);

                            if (strErr == null && SaveDetail())
                            {
                                Message.MessageResult(strOpe, "C", strErr);
                                Clear();
                            }
                            else
                            {
                                Message.MessageResult(strOpe, "E", strErr);
                            }
                        }

                        
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                DataTable dt = new DataTable();

                Parameter = new string[,]
                {
                    {"@RcptDentalCode", row.Cells["RcptDentalCode"].Value.ToString()},
                    {"@MsPatientCode", "0"},
                    {"@RcptDentalBookId", ""},
                    {"@RcptDentalBookNumber", ""},
                    {"@RcptDentalDate", ""},
                    {"@RcptDentalAmount", ""},
                    {"@RcptDentalPayee", ""},
                    {"@RcptDentalStatus", "0"},
                    {"@RcptDentalPay", "0"},
                };

                db.Get("Spr_S_TblRecieptDental", Parameter, out strErr, out dt);

                clrItem();
                txtCode.Text = dt.Rows[0]["RcptDentalCode"].ToString();
                txtName.Text = dt.Rows[0]["MsPatientName"].ToString();

                cbbHospital.SelectedValue = dt.Rows[0]["MsHospitalCode"].ToString();
                cbbStatus.SelectedValue = dt.Rows[0]["RcptDentalStatus"].ToString();
                cbbPatital.SelectedValue = dt.Rows[0]["MsPatientCode"].ToString();
                cbbMoney.SelectedValue = dt.Rows[0]["MsMoneyCode"].ToString();

                dtDate.Text = dt.Rows[0]["RcptDentalDate"].ToString();
                dtDate.Text = dt.Rows[0]["RcptDentalDate"].ToString();
                txtBookId.Text = dt.Rows[0]["RcptDentalBookId"].ToString();
                txtBookNumber.Text = dt.Rows[0]["RcptDentalBookNumber"].ToString();
                txtPayee.Text = dt.Rows[0]["RcptDentalPayee"].ToString();
                txtFileLocation.Text = dt.Rows[0]["RcptDentalFileLocation"].ToString();

                dtDate.Text = dt.Rows[0]["RcptDentalDate"].ToString();
                dtTime.Text = dt.Rows[0]["RcptDentalTime"].ToString();

                GetItem(row.Cells["RcptDentalCode"].Value.ToString());
                txtAmount.Text = dt.Rows[0]["RcptDentalAmount"].ToString();
                txtAmountText.Text = dt.Rows[0]["RcptDentalAmountText"].ToString();

                btnView.Focus();
            }
        }

        private bool ClearDetail(bool delete = true)
        {
            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@RcptDentalCode", txtCode.Text},
                    {"@DeleteType", "0"},
                    {"@User", strUserId},
                };

                db.Operations("Spr_D_TblRecieptDentalDetail", Parameter, out strErr);

                string[,] Parameters = new string[,]
                {
                    {"@ExpenseRef", String.Format("{0}/{1}", txtBookId.Text, txtBookNumber.Text)},
                    {"@DeleteType", "0"},
                    {"@User", strUserId},
                };

                db.Operations("Spr_D_TblSaveExpenseRef", Parameters, out strErr);

                return strErr == null ? true : false;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
                return false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (ClearDetail())
            {
                try
                {
                    strOpe = "U";

                    string[,] Parameter = new string[,]
                    {
                        {"@RcptDentalCode", txtCode.Text},
                        {"@MsPatientCode", cbbPatital.SelectedValue.ToString()},
                        {"@RcptDentalBookId", txtBookId.Text},
                        {"@RcptDentalBookNumber", txtBookNumber.Text},
                        {"@RcptDentalDate", Date.GetDate(dtp : dtDate)},
                        {"@RcptDentalTime", string.Format("{0:00}:{1:00}:{2:00}", dtTime.Value.Hour, dtTime.Value.Minute, dtTime.Value.Second)},
                        {"@RcptDentalPayee", txtPayee.Text},
                        {"@RcptDentalFileLocation", txtFileLocation.Text},
                        {"@RcptDentalStatus", cbbStatus.SelectedValue.ToString()},
                        {"@RcptDentalAmount", Fn.MoveNumberStringComma(txtAmount.Text)},
                        {"@RcptDentalAmountText", txtAmountText.Text},
                        {"@RcptDentalPay", cbbMoney.SelectedValue.ToString()},
                        {"@User", strUserId},
                    };

                    Message.MessageConfirmation(strOpe, txtCode.Text, "ใบเสร็จเลขที่ " + txtBookId.Text + "/" + txtBookNumber.Text + " (" + txtAmount.Text  + " บาท)");

                    using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                    {
                        var result = Mes.ShowDialog();

                        if (result == DialogResult.Yes)
                        {
                            Mes.Close();
                            db.Operations("Spr_U_TblRecieptDental", Parameter, out strErr);

                            if (strErr == null)
                            {
                                if (SaveDetail())
                                {
                                    Message.MessageResult(strOpe, "C", strErr);
                                    Clear();
                                }
                            }
                            else
                            {
                                Message.MessageResult(strOpe, "E", strErr);
                            }
                            Clear();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                strOpe = "D";

                string[,] Parameter = new string[,]
                    {
                        {"@RcptDentalCode", txtCode.Text},
                        {"@DeleteType", "0"},
                        {"@User", strUserId},
                    };

                Message.MessageConfirmation(strOpe, txtCode.Text, "ใบเสร็จเลขที่ " + txtBookId.Text + "/" + txtBookNumber.Text + " (" + txtAmount.Text + " บาท)");

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
                        db.Operations("Spr_D_TblRecieptDental", Parameter, out strErr);
                  
                        if (strErr == null)
                        {
                            ClearDetail();
                            Message.MessageResult(strOpe, "C", strErr);
                            Clear();
                        }
                        else
                        {
                            Message.MessageResult(strOpe, "E", strErr);
                        }
                    }
                }

                
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void picExcel_Click(object sender, EventArgs e)
        {
        }

        private void FrmPATSA00_KeyDown(object sender, KeyEventArgs e)
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

            if (keyCode == "Ctrl+F")
            {
                btnSearch_Click(sender, e);
            }

            if (keyCode == "Alt+C")
            {
                btnClear_Click(sender, e);
            }
        }

        private void cbbPatital_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbPatital.SelectedValue.ToString() == "0")
            {
                cbbHospital.SelectedValue = 0;
                txtName.Text = "";
            }
            else
            {
                try
                {
                    getDataMaster(Fn.getComboBoxValue(cbbPatital));
                    txtBookId.Focus();
                }
                catch (Exception)
                {
                    txtName.Text = "";
                }
            }
        }

        public void getDataMaster(string value)
        {
            string[,] Parameter = new string[,]
                {
                    {"@MsHospitalCode", "0"},
                    {"@MsPatientCode", value},
                    {"@MsPatientNumber", ""},
                    {"@MsPatientName", ""},
                    {"@MsPatientStatus", "0"},
                };

            db.Get("Spr_S_TblMasterPatient", Parameter, out strErr, out dt);

            if (strErr == null && dt.Rows.Count > 0)
            {
                txtName.Text = dt.Rows[0]["MsPatientName"].ToString();
                cbbHospital.SelectedValue = dt.Rows[0]["MsHospitalCode"].ToString();
            }
            else
            {
                txtName.Text = "";
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtAmount.Text = string.Format("{0:#,##0.00}", double.Parse(txtAmount.Text));
                txtAmountText.Text = clsBahtText.ToBahtText(sum);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            double a1 = 0.00;
            double a2 = 0.00;
            double a3 = 0.00;
            double a4 = 0.00;
            double a5 = 0.00;

            try
            {
                a1 = Convert.ToDouble(txtAmount1.Text);
            }
            catch (Exception)
            {
                a1 = 0.00;
            }

            try
            {
                a2 = Convert.ToDouble(txtAmount2.Text);
            }
            catch (Exception)
            {
                a2 = 0.00;
            }

            try
            {
                a3 = Convert.ToDouble(txtAmount3.Text);
            }
            catch (Exception)
            {
                a3 = 0.00;
            }

            try
            {
                a4 = Convert.ToDouble(txtAmount4.Text);
            }
            catch (Exception)
            {
                a4 = 0.00;
            }

            try
            {
                a5 = Convert.ToDouble(txtAmount5.Text);
            }
            catch (Exception)
            {
                a5 = 0.00;
            }

            try
            {
                sum = a1 + a2 + a3 + a4 + a5;
                txtAmount.Text = Convert.ToString(sum);
            }
            catch (Exception)
            {
                txtAmount.Text = "0.00";
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (txtFileLocation.Text != "")
            {
                FrmShowImage FrmShowImage = new FrmShowImage(txtCode.Text);
                FrmShowImage.Show();
            }
        }

        public bool SaveDetail()
        {
            try
            {
                bool Status = false;
                int Item = 0;

                if (txtAmount1.Text != "")
                {
                    Item += 1;
                }
                if (txtAmount2.Text != "")
                {
                    Item += 1;
                }
                if (txtAmount3.Text != "")
                {
                    Item += 1;
                }
                if (txtAmount4.Text != "")
                {
                    Item += 1;
                }
                if (txtAmount5.Text != "")
                {
                    Item += 1;
                }

                for (int Number = 1; Number <= Item; Number++)
                {
                    string strItem = "txtItem" + Number.ToString();
                    string strAmount = "txtAmount" + Number.ToString();

                    Control[] txtItemName = this.Controls.Find(strItem, true);
                    Control[] txtIAmount = this.Controls.Find(strAmount, true);

                    string[,] Parameter = new string[,]
                    {
                        {"@RcptDentalCode",txtCode.Text},
                        {"@RcptDentalDetailName",txtItemName[0].Text},
                        {"@RcptDentalDetailPrice",Fn.MoveNumberStringComma(txtIAmount[0].Text)},
                        {"@RcptDentalDetailStatus",cbbStatus.SelectedValue.ToString()},
                        {"@User",strUserId},
                    };

                    db.Operations("Spr_I_TblRecieptDentalDetail", Parameter, out strErr);

                    Status = strErr == null ? true : false;

                    //var data = new SaveExpenseModel()
                    //{
                    //    UserId = strUserId,
                    //    AppCode = strAppCode,
                    //    AppName = strAppName,
                    //    ExpenseAmount = Fn.MoveNumberStringComma(txtIAmount[0].Text),
                    //    ExpenseDate = dtDate.Value,
                    //    ExpenseDetails = "",
                    //    ExpenseItem = "ค่ารักษาพยาบาล | ค่ายา - " + Fn.Substring(txtItemName[0].Text, SubstringConstant.LastIndexToSpace, "ค่า"),
                    //    ExpenseMoney = cbbMoney.SelectedValue.ToString(),
                    //    ExpensePay = "HOS-000-0",
                    //    ExpensePaySub = "HOS0002",
                    //    ExpenseStatus = "Y",
                    //    ExpenseIncome = "0",
                    //    ExpenseRef = String.Format("{0}/{1}", txtBookId.Text, txtBookNumber.Text),
                    //    ExpenseUnit = "1",
                    //    ExpenseUnitId = "1222",
                    //    ExpenseReciept = ""
                    //};

                    //Insert.TblSaveExpense(data);
                }

                return Status;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
                return true;
            }
        }

        private TextBox FindControl(object p)
        {
            throw new NotImplementedException();
        }

        public void GetItem(string code)
        {
            Parameter = new string[,]
                {
                    {"@RcptDentalCode", code},
                };

            db.Get("Spr_S_TblRecieptDentalDetail", Parameter, out strErr, out dt);

            try
            {
                row = dt.Rows.Count;
            }
            catch (Exception)
            {
                row = 0;
            }

            for (int i = 0; i < row; i++)
            {
                try
                {
                    Control[] ctlItem = this.Controls.Find("txtItem" + (i + 1).ToString(), true);
                    Control[] ctlAmount = this.Controls.Find("txtAmount" + (i + 1).ToString(), true);

                    foreach (TextBox Item in ctlItem)
                    {
                        Item.Text = dt.Rows[i]["RcptDentalDetailName"].ToString();
                    }

                    foreach (TextBox Amount in ctlAmount)
                    {
                        Amount.Text = dt.Rows[i]["Amounts"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
                }
            }
        }

        private void clrItem()
        {
            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    Control[] ctlItem = this.Controls.Find("txtItem" + (i).ToString(), true);
                    Control[] ctlAmount = this.Controls.Find("txtAmount" + (i).ToString(), true);

                    foreach (TextBox Item in ctlItem)
                    {
                        Item.Text = "";
                    }

                    foreach (TextBox Amount in ctlAmount)
                    {
                        Amount.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
                }
            }
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchData();
            }
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                FrmImportImage Frm = new FrmImportImage(strAppCode, strUserId, strUserName, "", strUserType, txtCode.Text, txtFileLocation.Text, strFilePath, "P");
                Frm.ShowDialog();
                txtFileLocation.Text = Frm.strImageCode;
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (txtFileLocation.Text != "" && txtFileLocation.Text != "-")
            {
                FrmShowImage FrmShowImage = new FrmShowImage(txtFileLocation.Text);
                FrmShowImage.Show();
            }
        }

        private void dtDate_ValueChanged(object sender, EventArgs e)
        {
            dtDate.Text = Date.GetDate(dtp: dtDate);
        }

        private void txtPayee_Leave(object sender, EventArgs e)
        {
            if (txtPayee.Text != "")
            {
                cbbStatus.SelectedValue = "YP";
            }
            else
            {
                cbbStatus.SelectedValue = "0";
            }
        }

        private void AmountKeyPress(object sender, KeyPressEventArgs e)
        {
            Event.AmountKeyPress(sender, e, (TextBox)sender);
        }

        private void AmountFomat(object sender, EventArgs e)
        {
            TextBox Amount = (TextBox)sender;

            if (Amount.Text != "")
            {
                Amount.Text = string.Format("{0:#,##0.00}", double.Parse(Amount.Text));
            }

            btnCal_Click(sender, e);
        }

        private void txtAmount1_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount1.Text != "")
            {
                txtItem2.Enabled = true;
                txtAmount2.Enabled = true;
            }
            else
            {
                txtItem2.Enabled = false;
                txtAmount2.Enabled = false;
            }
        }

        private void txtAmount2_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount2.Text != "")
            {
                txtItem3.Enabled = true;
                txtAmount3.Enabled = true;
            }
            else
            {
                txtItem3.Enabled = false;
                txtAmount3.Enabled = false;
            }
        }

        private void txtAmount3_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount3.Text != "")
            {
                txtItem4.Enabled = true;
                txtAmount4.Enabled = true;
            }
            else
            {
                txtItem4.Enabled = false;
                txtAmount4.Enabled = false;
            }
        }

        private void txtAmount4_TextChanged(object sender, EventArgs e)
        {
            if (txtAmount4.Text != "")
            {
                txtItem5.Enabled = true;
                txtAmount5.Enabled = true;
            }
            else
            {
                txtItem5.Enabled = false;
                txtAmount5.Enabled = false;
            }
        }

        private void cbbMoney_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAdd.Focus();
        }
    }
}