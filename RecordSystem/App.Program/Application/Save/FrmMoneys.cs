using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;
using SANSANG.Utilites.App.Forms;
using SANSANG.Utilites.App.Model;

namespace SANSANG
{
    public partial class FrmMoneys : Form
    {
        public string strUserId = "";
        public string strUserName;
        public string strUserSurname;
        public string strUserType;
        public string strAppCode = "SAVMO00";
        public string strAppName = "Save Money";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";
        public string strSign = "";
        public string strDetail = "";
        public string strAmout = "";
        public string strExpenseDetail = "";
        private string strCoin = "";
        private string strType = "";

        private DataTable dt = new DataTable();
        private clsFunction Fn = new clsFunction();
        private clsInsert Insert = new clsInsert();
        private clsFormat TSSFormat = new clsFormat();
        private clsDelete Delete = new clsDelete();
        private clsUpdate TSSUpdate = new clsUpdate();
       private dbConnection db = new dbConnection();
        private clsDate Date = new clsDate();
        private clsMessage Message = new clsMessage();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();
        private clsHelpper Help = new clsHelpper();
        private Timer timer = new Timer();
        private clsEvent Event = new clsEvent();
        private bool selected = false;

        public string[,] Parameter = new string[,] { };

        public FrmMoneys(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmSaveMoney_Load(object sender, EventArgs e)
        {
            Clear();
            int sec = 2;
            timer.Interval = (sec * 1000);
            timer.Tick += new EventHandler(LoadList);
            timer.Start();
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetList(cbbCoin, "Y", "Coin");
            cbbCoin.Enabled = true;
            timer.Stop();
        }

        public void Clear()
        {
            try
            {
                txtDetail.Text = "";
                txtCoin.Text = "";
                lblSearch.Text = "";
                lblHSearch.Text = "คำค้นหา";

                panelType.Visible = false;
                cb_Save.Checked = true;
                pb_Save_True.Visible = true;
                pb_Save_False.Visible = false;

                cb_Detail.Checked = false;
                pb_Detail_True.Visible = false;
                pb_Detail_False.Visible = true;

                cb_Use.Checked = false;
                pb_Use_True.Visible = false;
                pb_Use_False.Visible = true;

                cb_Date.Checked = false;
                cb_Coin.Checked = false;
                cb_Detail.Checked = false;

                cb_Date.Checked = false;
                pb_Date_True.Visible = false;
                pb_Date_False.Visible = true;

                cb_Coin.Checked = false;
                pb_Coin_True.Visible = false;
                pb_Coin_False.Visible = true;

                dtDate.Text = DateTime.Now.ToString();
                strSign = "";
                cb_Use.Checked = false;
                cbbCoin.SelectedValue = 0;
                rdbSumCollect.Checked = true;
                SearchData();

                if (cb_Save.Checked == true)
                {
                    panelType.Visible = true;
                }

                txtCoin.Focus();
                lblCoin.Text = "0.00";
                txtSum.Text = "";
                txtCoin.Text = "";
                txtSum.Text = "";
                lblType.Text = "เหรียญ";
                lblTypeSum.Text = "เหรียญ";
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
            SearchData();
        }

        public void SearchData()
        {
            try
            {
                string dtNow = Date.GetDate(dt: DateTime.Today, Format: 4);
                string dtSearch = Date.GetDate(dtp: dtDate, Format: 4);

                if (dtNow == dtSearch)
                {
                    dtSearch = "";
                }

                try
                {
                    Parameter = new string[,]
                {
                    {"@MoneyCode", ""},
                    {"@MsCoinCode", cb_Coin.Checked == false ? "0" : cbbCoin.SelectedValue.ToString()},
                    {"@MoneyValue", ""},
                    {"@MoneyNumber", ""},
                    {"@MoneyAmount", ""},
                    {"@MoneyDate", cb_Date.Checked == false ? "" : Date.GetDate(dtp: dtDate)},
                    {"@MoneyDetail", cb_Detail.Checked == false ? "" : txtDetail.Text},
                    {"@MoneyStatus", ""},
                };

                    db.Get("Spr_S_TblSaveMoney", Parameter, out strErr, out dt);
                    getDataGrid(dt);
                }
                catch (Exception ex)
                {
                    Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
                }
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
                    dataGridView.DataSource = null;
                    picExcel.Visible = false;
                    lblCount.Text = "0";
                }
                else
                {
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "Date", "MoneyAmounts", "Amount", "MoneyDetails", "MsUserName", "MoneyCode");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
                    DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                    Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                          "ลำดับ", 50, true, mc, mc
                        , "วัน-เดือน-ปี", 80, true, mc, mc
                        , "เหรียญ/ธนบัตร x จำนวน", 80, true, mc, ml
                        , "จำนวนเงิน", 50, true, mc, mr
                        , "รายละเอียด", 150, true, mc, ml
                        , "ผู้บันทึก", 120, true, mc, mc
                        , "", 0, false, mc, mc
                        );

                    picExcel.Visible = true;
                    lblCount.Text = row.ToString();
                }
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
                strSign = "";
                strDetail = "";

                if (cb_Use.Checked == true)
                {
                    strSign = "-";
                    strDetail = "นำเงินไปใช้\r\n";
                }

                txtCode.Text = Fn.GetCodes("121", "", "Generated");
                strOpe = "I";
                strAmout = Convert.ToString(Convert.ToDouble(lblCoin.Text) * Convert.ToInt32(strSign + txtCoin.Text));

                string[,] Parameter = new string[,]
                {
                    {"@MoneyCode", txtCode.Text},
                    {"@MsCoinCode", cbbCoin.SelectedValue.ToString()},
                    {"@MoneyValue", lblCoin.Text},
                    {"@MoneyNumber", strSign + txtCoin.Text},
                    {"@MoneyAmount",  strAmout},
                    {"@MoneyDate",  Date.GetDate(dtp: dtDate)},
                    {"@MoneyDetail", strDetail + txtDetail.Text},
                    {"@MoneyCollect", rdbK.Checked ? "0" : "1"},
                    {"@MoneyStatus", "Y"},
                    {"@User", strUserId},
                };

                Message.MessageConfirmation(strOpe, txtCode.Text, 
                 cb_Use.Checked == false ?  
                                          rdbS.Checked == true ? "สะสม" + cbbCoin.Text + " x " + txtCoin.Text + lblType.Text 
                                                               : "หยอดกระปุก" + cbbCoin.Text + " x " + txtCoin.Text + lblType.Text 
                                          : "ใช้" + cbbCoin.Text + " x " + txtCoin.Text + lblType.Text);

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
                        db.Operations("Spr_I_TblSaveMoney", Parameter, out strErr);

                        if (cb_Save.Checked == true)
                        {
                            strExpenseDetail = rdbS.Checked == true?  "เงินออม | สะสม - " + cbbCoin.Text : "เงินออม | หยอดกระปุก - " + cbbCoin.Text;

                            if (cb_Use.Checked == true)
                            {
                                strExpenseDetail = "เงินออม | ถอนเงินออม - " + lblCoin.Text + " บาท";
                            }

                            //var data = new SaveExpenseModel()
                            //{
                            //    UserId = strUserId,
                            //    AppCode = strAppCode,
                            //    AppName = strAppName,
                            //    ExpenseAmount = cb_Use.Checked == true ? strAmout.Replace("-", "") : strAmout,
                            //    ExpenseDate = dtDate.Value,
                            //    ExpenseDetails = "",
                            //    ExpenseItem = strExpenseDetail,
                            //    ExpenseMoney = "CAH00-01",
                            //    ExpensePay = "SAV-000-0",
                            //    ExpensePaySub = rdbS.Checked == true ?  cb_Use.Checked == false ? cbbCoin.SelectedValue.ToString() : "MON0005" : "SAV18985",
                            //    ExpenseStatus = "Y",
                            //    ExpenseIncome = cb_Use.Checked == false ? "0" : "1",
                            //    ExpenseRef = txtCode.Text,
                            //    ExpenseUnit = txtCoin.Text,
                            //    ExpenseUnitId = strCoin == "C" ? "1219" : "1220"
                            //};

                            //Insert.TblSaveExpense(data);
                        }
                        if (strErr == null)
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
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                    DataTable dt = new DataTable();

                    Parameter = new string[,]
                    {
                        {"@MoneyCode", row.Cells["MoneyCode"].Value.ToString()},
                        {"@MsCoinCode", "0"},
                        {"@MoneyValue", ""},
                        {"@MoneyNumber", ""},
                        {"@MoneyAmount", ""},
                        {"@MoneyDate", ""},
                        {"@MoneyDetail", ""},
                        {"@MoneyStatus", ""},
                    };

                    db.Get("Spr_S_TblSaveMoney", Parameter, out strErr, out dt);
                    selected = true;
                    int collectde = dt.Rows[0]["MoneyCollect"].ToString() == "True"? 1 : 0;
                    txtCoin.Text = TSSFormat.Number(dt.Rows[0]["MoneyNumber"].ToString(), formatNumber: "Number");
                    dtDate.Text = dt.Rows[0]["MoneyDate"].ToString();
                    txtCode.Text = dt.Rows[0]["MoneyCode"].ToString();
                    lblCoin.Text = dt.Rows[0]["MoneyValue"].ToString();
                    txtSum.Text = TSSFormat.Number(Fn.getCollectSummary("Number", dt.Rows[0]["MoneyValue"].ToString(), collectde), formatNumber: "Number");
                    cbbCoin.SelectedValue = dt.Rows[0]["MsCoinCode"].ToString();
                    rdbS.Checked = bool.Parse(dt.Rows[0]["MoneyCollect"].ToString());
                    rdbK.Checked = !bool.Parse(dt.Rows[0]["MoneyCollect"].ToString());

                    double coin = 0;
                    coin = Convert.ToDouble(txtCoin.Text);
                    cb_Use.Checked = coin > 0 ? false : true;
                    txtDetail.Text = cb_Use.Checked == false ? dt.Rows[0]["MoneyDetail"].ToString() : Fn.SplitString(dt.Rows[0]["MoneyDetail"].ToString(), "นำเงินไปใช้\r\n", "");
                    selected = false;
                    dataGridView.Focus();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                strOpe = "D";
                string NewCoin = "";
                NewCoin = Fn.SplitString(txtCoin.Text, ",", "");
                strAmout = Convert.ToString(Convert.ToDouble(lblCoin.Text) * Convert.ToInt32(strSign + NewCoin));

                string[,] Parameter = new string[,]
                {
                    {"@MoneyCode", txtCode.Text},
                    {"@DeleteType", "0"},
                    {"@User", strUserId},
                };

                Message.MessageConfirmation(strOpe, txtCode.Text, "ลบ" + cbbCoin.Text + " ฿" + strAmout);

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
                        db.Operations("Spr_D_TblSaveMoney", Parameter, out strErr);

                        if (cb_Save.Checked == true)
                        {
                            //var data = new SaveExpenseModel()
                            //{
                            //    UserId = strUserId,
                            //    AppCode = strAppCode,
                            //    AppName = strAppName,
                            //    ExpenseRef = txtCode.Text
                            //};

                            //Delete.TblSaveExpense(data);
                        }

                        if (strErr == null)
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
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                strSign = "";
                strDetail = "";

                if (cb_Use.Checked == true)
                {
                    strSign = "-";
                    strDetail = "นำเงินไปใช้\r\n";
                }

                strOpe = "U";
                string NewCoin = "";
                NewCoin = Fn.SplitString(txtCoin.Text, "-", "");
                strAmout = Convert.ToString(Convert.ToDouble(lblCoin.Text) * Convert.ToInt32(strSign + NewCoin));
                string[,] Parameter = new string[,]
                {
                    {"@MoneyCode", txtCode.Text},
                    {"@MsCoinCode", cbbCoin.SelectedValue.ToString()},
                    {"@MoneyValue", lblCoin.Text},
                    {"@MoneyNumber", strSign + NewCoin},
                    {"@MoneyAmount", strAmout},
                    {"@MoneyDate",  Date.GetDate(dtp: dtDate)},
                    {"@MoneyDetail", strDetail + txtDetail.Text},
                    {"@MoneyCollect", rdbK.Checked ? "0" : "1"},
                    {"@MoneyStatus", "E"},
                    {"@User", strUserId},
                };

                Message.MessageConfirmation(strOpe, txtCode.Text, cb_Use.Checked == false ?
                                          rdbS.Checked == true ? "สะสม" + cbbCoin.Text + " x " + txtCoin.Text + lblType.Text
                                                               : "หยอดกระปุก" + cbbCoin.Text + " x " + txtCoin.Text + lblType.Text
                                          : "ใช้" + cbbCoin.Text + " x " + txtCoin.Text + lblType.Text);

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
                        db.Operations("Spr_U_TblSaveMoney", Parameter, out strErr);

                        if (cb_Save.Checked == true)
                        {
                            strExpenseDetail = rdbS.Checked == true ? "เงินออม | สะสม - " + cbbCoin.Text : "เงินออม | หยอดกระปุก - " + cbbCoin.Text;

                            if (cb_Use.Checked == true)
                            {
                                strExpenseDetail = "เงินออม | ถอนเงินออม - " + lblCoin.Text + " บาท";
                            }

                            //var data = new SaveExpenseModel()
                            //{
                            //    UserId = strUserId,
                            //    AppCode = strAppCode,
                            //    AppName = strAppName,
                            //    ExpenseAmount = cb_Use.Checked == true ? strAmout.Replace("-", "") : strAmout,
                            //    ExpenseDate = dtDate.Value,
                            //    ExpenseDetails = "",
                            //    ExpenseItem = strExpenseDetail,
                            //    ExpenseMoney = "CAH00-01",
                            //    ExpensePay = "SAV-000-0",
                            //    ExpensePaySub = rdbS.Checked == true ? cb_Use.Checked == false ? cbbCoin.SelectedValue.ToString() : "MON0005" : "SAV18985",
                            //    ExpenseStatus = "Y",
                            //    ExpenseIncome = cb_Use.Checked == false ? "0" : "1",
                            //    ExpenseRef = txtCode.Text,
                            //    ExpenseUnit = txtCoin.Text,
                            //    ExpenseUnitId = strCoin == "C" ? "1219" : "1220"
                            //};

                            //TSSUpdate.TblSaveExpense(data);
                        }

                        if (strErr == null)
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
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void cbbCoin_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbCoin.SelectedValue.ToString().Contains("SAV0001") ||
                                cbbCoin.SelectedValue.ToString().Contains("SAV0002") ||
                                cbbCoin.SelectedValue.ToString().Contains("SAV0003") ||
                                cbbCoin.SelectedValue.ToString().Contains("SAV5623") ||
                                cbbCoin.SelectedValue.ToString().Contains("SAV0005") ||
                                cbbCoin.SelectedValue.ToString().Contains("SAV5628") ||
                                cbbCoin.SelectedValue.ToString().Contains("SAV1000") ||
                                cbbCoin.SelectedValue.ToString().Contains("SAV0020")
                                )
                {
                    strCoin = "C";
                }
                else
                {
                    strCoin = "B";
                }

                strType = strCoin == "C" ? " เหรียญ" : " ธนบัตร";
                lblType.Text = strType;
                lblTypeSum.Text = strType;

                Parameter = new string[,]
                {
                    {"@MsCoinId", ""},
                    {"@MsCoinCode", cbbCoin.SelectedValue.ToString()},
                    {"@MsCoinValue", ""},
                    {"@MsCoinName", ""},
                    {"@MsCoinNameEn", ""},
                    {"@MsCoinDetail", ""},
                    {"@MsCoinStatus", ""},
                };

                db.Get("Spr_S_TblMasterCoin", Parameter, out strErr, out dt);


                int collectde = 0;

                if (selected)
                {
                    collectde = dt.Rows[0]["MoneyCollect"].ToString() == "True" ? 1 : 0;
                }
                else
                {
                    collectde = rdbSumCollect.Checked? 1 : 0;
                }

                lblCoin.Text = dt.Rows[0]["MsCoinValue"].ToString();
                txtSum.Text = TSSFormat.Number(Fn.getCollectSummary("Number", dt.Rows[0]["MsCoinValue"].ToString(), collectde), formatNumber: "Number");
                txtCoin.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void txtCoin_KeyPress(object sender, KeyPressEventArgs e)
        {
            Event.AmountKeyPress(sender, e, txtCoin);
        }

        private void Ticker(object sender, EventArgs e)
        {
            var pb = (PictureBox)sender;

            if (pb.Name == "pb_Save_True" || pb.Name == "pb_Save_False")
            {
                panelType.Visible = Help.CheckboxTicker(sender, this) ? true : false;
            }
            else
            {
                Help.CheckboxTicker(sender, this);
            }
        }

        private void rdbSumCollect_CheckedChanged(object sender, EventArgs e)
        {
            var rd = (RadioButton)sender;

            if (rdbSumCollect.Checked == true)
            {
                GetSummary(rd.Name);
            }
        }

        private void rdbSumSave_CheckedChanged(object sender, EventArgs e)
        {
            var rd = (RadioButton)sender;

            if (rdbSumSave.Checked == true)
            {
                GetSummary(rd.Name);
            }
        }

        private void GetSummary(string Collect)
        {
            if (Collect == "rdbSumCollect")
            {
                SetPanel(true);
                txtSumAll.Text = TSSFormat.Number(Fn.getCollectSummary("Amount", "", 1));
                txtSum25.Text = TSSFormat.Number(Fn.getCollectSummary("Amount", "0.25", 1));
                txtSum50.Text = TSSFormat.Number(Fn.getCollectSummary("Amount", "0.50", 1));
                txtSum2.Text = TSSFormat.Number(Fn.getCollectSummary("Amount", "2.00", 1));
                txtSum5.Text = TSSFormat.Number(Fn.getCollectSummary("Amount", "5.00", 1));
                txtSum10.Text = TSSFormat.Number(Fn.getCollectSummary("Amount", "10.00", 1));

                txtCoinAll.Text = TSSFormat.Number(Fn.getCollectSummary("Number", "", 1), formatNumber: "Number");
                txtCoin25.Text = TSSFormat.Number(Fn.getCollectSummary("Number", "0.25", 1), formatNumber: "Number");
                txtCoin50.Text = TSSFormat.Number(Fn.getCollectSummary("Number", "0.50", 1), formatNumber: "Number");
                txtCoin2.Text = TSSFormat.Number(Fn.getCollectSummary("Number", "2.00", 1), formatNumber: "Number");
                txtCoin5.Text = TSSFormat.Number(Fn.getCollectSummary("Number", "5.00", 1), formatNumber: "Number");
                txtCoin10.Text = TSSFormat.Number(Fn.getCollectSummary("Number", "10.00", 1), formatNumber: "Number");
            }
            else
            {
                SetPanel(false);
                txtSumAll.Text = TSSFormat.Number(Fn.getCollectSummary("Amount", "", 0));
                txtCoinAll.Text = TSSFormat.Number(Fn.getCollectSummary("Number", "", 0), formatNumber: "Number");

                txtSumCoin5.Text = TSSFormat.Number(Fn.getCollectSummary("Amount", "5.00", 0));
                txtSumCoin10.Text = TSSFormat.Number(Fn.getCollectSummary("Amount", "10.00", 0));
                txtSumBank20.Text = TSSFormat.Number(Fn.getCollectSummary("Amount", "20.00", 0));

                txtAmontCoin5.Text = TSSFormat.Number(Fn.getCollectSummary("Number", "5.00", 0), formatNumber: "Number");
                txtAmontCoin10.Text = TSSFormat.Number(Fn.getCollectSummary("Number", "10.00", 0), formatNumber: "Number");
                txtAmontBank20.Text = TSSFormat.Number(Fn.getCollectSummary("Number", "20.00", 0), formatNumber: "Number");
            }
        }

        private void SetPanel(bool hidden)
        {
            if (hidden)
            {
                pnSave.Visible = false;

                pnCoin5.Visible = false;
                txtAmontCoin5.Text = "0";
                pnCoin10.Visible = false;
                txtAmontCoin10.Text = "0";
                pnBank20.Visible = false;
                txtAmontBank20.Text = "0";

                pnSumCoin5.Visible = false;
                txtSumCoin5.Text = "0";
                pnSumCoin10.Visible = false;
                txtSumCoin10.Text = "0";
                pnSumBank20.Visible = false;
                txtSumBank20.Text = "0";
            }
            else
            {
                pnSave.Visible = true;

                pnCoin5.Visible = true;
                txtAmontCoin5.Text = "0";
                pnCoin10.Visible = true;
                txtAmontCoin10.Text = "0";
                pnBank20.Visible = true;
                txtAmontBank20.Text = "0";

                pnSumCoin5.Visible = true;
                txtSumCoin5.Text = "0";
                pnSumCoin10.Visible = true;
                txtSumCoin10.Text = "0";
                pnSumBank20.Visible = true;
                txtSumBank20.Text = "0";
            }
        }
    }
}