using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmCalLoan : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
        private clsDataList List = new clsDataList();

        public string strSql = "";
        public string condition = "";

        public FrmCalLoan(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmCalLoan_Load(object sender, EventArgs e)
        {
            txtNumber.Text = "";
            txtInterest.Text = "";
            txtMonth.Text = "";
            txtBaht.Text = "";
            txtD.Text = DateTime.Today.ToString("dd");
            txtM.Text = DateTime.Today.ToString("MM");
            txtY.Text = DateTime.Today.ToString("yyyy");

            cbbPayType.DataSource = List.getPayTypeList();
            cbbPayType.DisplayMember = "value";
            cbbPayType.ValueMember = "index";
            cbbPayType.SelectedIndex = 0;

            txtNumber.Focus();
            txtMonth.Enabled = true;
            txtBaht.Enabled = true;
        }

        public void getDataGrid(string strSql)
        {
            int row = Fn._countRow(strSql).Rows.Count;

            if (row == 0)
            {
            }
            else
            {
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnShowPDF_Click(object sender, EventArgs e)
        {
            if (txtNumber.Text != "")
            {
                condition = condition += " | ยอดเงินกู้ " + txtNumber.Text + " บาท";
                if (txtInterest.Text != "")
                {
                    condition = condition += " | อัตราดอกเบี้ย " + txtInterest.Text + "% ต่อปี";
                    if (txtMonth.Text != "" || txtBaht.Text != "")
                    {
                        if (txtMonth.Text != "")
                        {
                            condition = condition += " | ผ่อนชำระ " + txtMonth.Text + " เดือน";
                        }
                        if (txtBaht.Text != "")
                        {
                            condition = condition += " | ชำระเดือนละ " + txtMonth.Text + " บาท";
                        }

                        if (cbbPayType.SelectedIndex != 0)
                        {
                            condition = condition += " | ชำระแบบ" + cbbPayType.Text;
                            calLoan("PDF");
                        }
                        else
                        {
                            Message.ShowMesInfo("กรุณาระบุวิธีการชำระเงิน");
                            cbbPayType.Focus();
                        }
                    }
                    else
                    {
                        Message.ShowMesInfo("กรุณาระบุงวดหรือจำนวนเงินที่ชำระ");
                        txtMonth.Focus();
                    }
                }
                else
                {
                    Message.ShowMesInfo("กรุณาระบุอัตราดอกเบี้ย");
                    txtInterest.Focus();
                }
            }
            else
            {
                Message.ShowMesInfo("กรุณาระบุจำนวนเงินกู้");
                txtNumber.Focus();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            FrmCalLoan_Load(sender, e);
        }

        private void picExcel_Click(object sender, EventArgs e)
        {
        }

        private void txtNumberkeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void dtTime_ValueChanged(object sender, EventArgs e)
        {
            txtD.Text = dtTime.Value.ToString("dd");
            txtM.Text = dtTime.Value.ToString("MM");
            txtY.Text = dtTime.Value.ToString("yyyy");
        }

        public void calLoan(string fileType)
        {
            double Number = Convert.ToDouble(txtNumber.Text);
            double Interest = Convert.ToDouble(txtInterest.Text);
            double Baht = 0;
            int Month = 0;
            double Principle = 0;
            double Balance = 0;
            double interest = 0;
            double total = 0;
            double totalPrinciple = 0;
            double totalTnterest = 0;
            double totalALL = 0;

            string D = txtD.Text;
            string M = txtM.Text;
            string Y = txtY.Text;

            DataTable dt = new DataTable("RSA-R-CALLOAN");
            DataRow row;

            dt.Columns.Add("Period", typeof(int));
            dt.Columns.Add("Balance", typeof(double));
            dt.Columns.Add("PayDate", typeof(string));
            dt.Columns.Add("Day", typeof(int));
            dt.Columns.Add("Interest", typeof(double));
            dt.Columns.Add("Principle", typeof(double));
            dt.Columns.Add("Total", typeof(double));
            dt.Columns.Add("Year", typeof(string));

            int round = 0;

            if (txtMonth.Text != "")
            {
                Month = Convert.ToInt32(txtMonth.Text);
                round = Month;
            }
            else
            {
                Baht = Convert.ToDouble(txtBaht.Text);

                if (Number % Baht != 0)
                {
                    round = Convert.ToInt32(Number / Baht) + 1;
                }
                else
                {
                    round = Convert.ToInt32(Number / Baht);
                }
            }

            // Calculator Total Interest

            for (int i = 1; i <= round; i++)
            {
                Principle = Ceiling(Convert.ToDouble(txtNumber.Text) / round, 10.00);
                Balance = (Convert.ToDouble(txtNumber.Text) - ((i - 1) * Principle));

                string strDate = D + "/" + M + "/" + Y;
                DateTime date = Convert.ToDateTime(strDate.ToString());
                var lastDayOfMonth = DateTime.DaysInMonth(date.Year, date.Month);
                DateTime firstOfNextMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1);
                DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);

                DateTime dec31 = new DateTime(Convert.ToInt32(Y) - 543, 12, 31);
                int numDayOfYear = dec31.DayOfYear;

                if (Balance > Principle)
                {
                    totalPrinciple = totalPrinciple + Principle;
                }
                else
                {
                    totalPrinciple = totalPrinciple + Balance;
                }

                if (i == 1)
                {
                    double day = Math.Round(Convert.ToDouble(lastDayOfMonth) / numDayOfYear, 5);
                    double inter = Convert.ToDouble(txtInterest.Text) / 100;
                    double money = Convert.ToDouble(txtNumber.Text);
                    interest = money * inter * (day);
                }
                else
                {
                    double day = Math.Round(Convert.ToDouble(lastDayOfMonth) / numDayOfYear, 5);
                    double inter = Convert.ToDouble(txtInterest.Text) / 100;
                    interest = Balance * inter * (day);
                }

                totalTnterest = totalTnterest + interest;
                totalALL = totalALL + interest + Principle;

                if (Convert.ToString(Convert.ToInt32(M) + 1) != "13")
                {
                    D = Convert.ToString(1);
                    M = Convert.ToString(Convert.ToInt32(M) + 1);
                }
                else
                {
                    D = Convert.ToString(1);
                    M = Convert.ToString(1);
                    Y = Convert.ToString(Convert.ToInt32(txtY.Text) + 1);
                }
            }

            //Add data to Report

            for (int i = 1; i <= round; i++)
            {
                string strDate = txtD.Text + "/" + txtM.Text + "/" + txtY.Text;
                DateTime date = Convert.ToDateTime(strDate.ToString());

                var lastDayOfMonth = DateTime.DaysInMonth(date.Year, date.Month);
                DateTime firstOfNextMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1);
                DateTime lastOfThisMonth = firstOfNextMonth.AddDays(-1);

                row = dt.NewRow();
                row["Period"] = i;
                row["PayDate"] = Convert.ToString(Fn._formatDate(Convert.ToString(lastOfThisMonth), "dMMMMyyyy"));
                row["Day"] = lastDayOfMonth;

                DateTime dec31 = new DateTime(Convert.ToInt32(txtY.Text) - 543, 12, 31);
                int numDayOfYear = dec31.DayOfYear;

                if (cbbPayType.SelectedIndex == 1)
                {
                    if (txtMonth.Text == "")
                    {
                        Balance = Math.Round((Convert.ToDouble(txtNumber.Text) - ((i - 1) * Principle)), 2);
                        row["Balance"] = Balance;

                        if (i == 1)
                        {
                            double day = Math.Round(Convert.ToDouble(lastDayOfMonth) / numDayOfYear, 5);
                            double inter = Convert.ToDouble(txtInterest.Text) / 100;
                            double money = Convert.ToDouble(txtNumber.Text);
                            interest = Math.Round(money * inter * (day), 2);
                        }
                        else
                        {
                            double day = Math.Round(Convert.ToDouble(lastDayOfMonth) / numDayOfYear, 5);
                            double inter = Convert.ToDouble(txtInterest.Text) / 100;

                            interest = Math.Round(Balance * inter * (day), 2);
                        }

                        row["Interest"] = interest;

                        row["Principle"] = Math.Round((Convert.ToDouble(txtNumber.Text) / round), 2);
                        Principle = 0;
                        Principle = Math.Round((Convert.ToDouble(txtNumber.Text) / round), 0);
                        row["Total"] = interest + Principle;
                    }
                    else
                    {
                        Principle = Math.Round(Ceiling(Convert.ToDouble(txtNumber.Text) / round, 10.00), 2);
                        Balance = Math.Round((Convert.ToDouble(txtNumber.Text) - ((i - 1) * Principle)), 2);

                        if (Balance > Principle)
                        {
                            row["Balance"] = Balance;
                            row["Principle"] = Principle;
                        }
                        else
                        {
                            row["Balance"] = Balance;
                            row["Principle"] = Balance;
                        }

                        if (i == 1)
                        {
                            double day = Math.Round(Convert.ToDouble(lastDayOfMonth) / numDayOfYear, 5);
                            double inter = Convert.ToDouble(txtInterest.Text) / 100;
                            double money = Convert.ToDouble(txtNumber.Text);
                            interest = Math.Round(money * inter * (day), 2);
                        }
                        else
                        {
                            double day = Math.Round(Convert.ToDouble(lastDayOfMonth) / numDayOfYear, 5);
                            double inter = Convert.ToDouble(txtInterest.Text) / 100;

                            interest = Math.Round(Balance * inter * (day), 2);
                        }

                        row["Interest"] = interest;
                        row["Total"] = interest + Principle;
                    }
                }
                else if (cbbPayType.SelectedIndex == 2)
                {
                    total = Math.Round(Ceiling(Convert.ToDouble(totalALL) / round, 10.00), 2);
                    row["Total"] = total;

                    if (i == 1)
                    {
                        Balance = Convert.ToDouble(txtNumber.Text);

                        double day = Math.Round(Convert.ToDouble(lastDayOfMonth) / numDayOfYear, 5);
                        double inter = Convert.ToDouble(txtInterest.Text) / 100;
                        double money = Convert.ToDouble(txtNumber.Text);
                        interest = Math.Round(money * inter * (day), 2);
                        Principle = total - interest;
                        row["Balance"] = Balance;
                        row["Interest"] = interest;
                        row["Principle"] = Principle;
                    }
                    else
                    {
                        Balance = Balance - Principle;
                        double day = Math.Round(Convert.ToDouble(lastDayOfMonth) / numDayOfYear, 5);
                        double inter = Convert.ToDouble(txtInterest.Text) / 100;
                        double money = Balance;
                        interest = Math.Round(money * inter * (day), 2);
                        Principle = total - interest;
                        row["Balance"] = Balance;
                        row["Interest"] = interest;
                        row["Principle"] = Principle;
                    }
                }

                row["Year"] = txtY.Text;
                dt.Rows.Add(row);

                if (Convert.ToString(Convert.ToInt32(txtM.Text) + 1) != "13")
                {
                    txtD.Text = Convert.ToString(1);
                    txtM.Text = Convert.ToString(Convert.ToInt32(txtM.Text) + 1);
                }
                else
                {
                    txtD.Text = Convert.ToString(1);
                    txtM.Text = Convert.ToString(1);
                    txtY.Text = Convert.ToString(Convert.ToInt32(txtY.Text) + 1);
                }
            }

            txtD.Text = DateTime.Today.ToString("dd");
            txtM.Text = DateTime.Today.ToString("MM");
            txtY.Text = DateTime.Today.ToString("yyyy");

            string Path = Fn.getPath("App.Report");
            string ReportName = "RSA-R-APPLO00001";

            string ExportToPath = "";
            DialogResult result = folderBrowserDialog.ShowDialog();
            FrmAnimatedProgress frm = new FrmAnimatedProgress(10);
            frm.Show();

            if (result == DialogResult.OK)
            {
                ExportToPath += folderBrowserDialog.SelectedPath + "\\TSSRSA-APPLO00R000";

                if (fileType == "PDF")
                {
                   
                    System.Diagnostics.Process.Start(@"" + ExportToPath + ".pdf");
                }
                else
                {
                  
                    System.Diagnostics.Process.Start(@"" + ExportToPath + ".xls");
                }

                try
                {
                    Insert.TblLogReport(Fn.getTssFileName("CLOAN"), ReportName, "", "APPLOA00", "LOAN", "TSSRSA-APPLO00R000" + "." + fileType, ExportToPath + "." + fileType, strUserName + " " + strUserSurname);
                }
                catch
                {
                }
            }
        }

        private void txtMonth_TextChanged(object sender, EventArgs e)
        {
            txtBaht.Text = "";
            txtBaht.Enabled = false;

            if (txtMonth.Text == "")
            {
                txtBaht.Enabled = true;
            }
        }

        private void txtBaht_TextChanged(object sender, EventArgs e)
        {
            txtMonth.Text = "";
            txtMonth.Enabled = false;

            if (txtBaht.Text == "")
            {
                txtMonth.Enabled = true;
            }
        }

        private void btnShowXLS_Click(object sender, EventArgs e)
        {
            if (txtNumber.Text != "")
            {
                condition = condition += " | ยอดเงินกู้ " + txtNumber.Text + " บาท";
                if (txtInterest.Text != "")
                {
                    condition = condition += " | อัตราดอกเบี้ย " + txtInterest.Text + "% ต่อปี";
                    if (txtMonth.Text != "" || txtBaht.Text != "")
                    {
                        if (txtMonth.Text != "")
                        {
                            condition = condition += " | ผ่อนชำระ " + txtMonth.Text + " เดือน";
                        }
                        if (txtBaht.Text != "")
                        {
                            condition = condition += " | ชำระเดือนละ " + txtMonth.Text + " บาท";
                        }

                        if (cbbPayType.SelectedIndex != 0)
                        {
                            condition = condition += " | ชำระแบบ" + cbbPayType.Text;
                            calLoan("XLS");
                        }
                        else
                        {
                            Message.ShowMesInfo("กรุณาระบุวิธีการชำระเงิน");
                            cbbPayType.Focus();
                        }
                    }
                    else
                    {
                        Message.ShowMesInfo("กรุณาระบุงวดหรือจำนวนเงินที่ชำระ");
                        txtMonth.Focus();
                    }
                }
                else
                {
                    Message.ShowMesInfo("กรุณาระบุอัตราดอกเบี้ย");
                    txtInterest.Focus();
                }
            }
            else
            {
                Message.ShowMesInfo("กรุณาระบุจำนวนเงินกู้");
                txtNumber.Focus();
            }
        }

        private void btnShowProgress_Click(object sender, EventArgs e)
        {
        }

        public double Ceiling(double value, double significance)
        {
            if ((value % significance) != 0)
            {
                return ((int)(value / significance) * significance) + significance;
            }

            return Convert.ToDouble(value);
        }

        private void cbbPayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbPayType.SelectedIndex == 2)
            {
                txtBaht.Text = "";
                txtBaht.Enabled = false;
                txtMonth.Focus();
            }
            else
            {
                txtBaht.Enabled = true;
            }
        }
    }
}