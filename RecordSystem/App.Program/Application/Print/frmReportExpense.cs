using DevComponents.DotNetBar;
using Microsoft.Reporting.WebForms;
using Microsoft.ReportingServices.Interfaces;
using Org.BouncyCastle.Asn1.Cmp;
using SANSANG;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace RecordSystemApplication.App.Program.Application.Print
{
    public partial class FrmReportExpense : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;
        public string Laguage;

        public string Error = "";
        public string AppCode = "SAVAD00";
        public string AppName = "FrmAddress";
        public string Address = "";
        public string Detail = "";

        public bool Start = true;

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        private clsFunction Function = new clsFunction();
        private DataListConstant DataList = new DataListConstant();
        private clsMessage Message = new clsMessage();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsReport Report = new clsReport();
        private OperationConstant Operation = new OperationConstant();
        private StoreConstant Store = new StoreConstant();
        private TableConstant Table = new TableConstant();
        private clsInsert Insert = new clsInsert();
        private clsDelete Delete = new clsDelete();
        private clsBarcode Barcode = new clsBarcode();
        private clsSetting Setting = new clsSetting();
        private clsEdit Edit = new clsEdit();
        private clsLog Log = new clsLog();
        private Timer Timer = new Timer();
        public string[,] Parameter = new string[,] { };
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(20);
        private clsDate Date = new clsDate();
        private clsConvert Converts = new clsConvert();

        public FrmReportExpense(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
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
            List.GetLists(cbbCategory, DataList.CategoryId);

            cbbCategory.Enabled = true;
            Timer.Stop();
        }

        private void btnPrints_Click(object sender, EventArgs e)
        {
            try
            {
                string strCondition = "";
                string Days = "";
                string DateFrom = "";
                string DateTo = "";
                string Months = "";
                string Year = "";
                string Item = "";
                string CategoryId = "";
                string Receipt = "";

                if (rdbDays.Checked)
                {
                    strCondition += "";
                    strCondition += dtDays.Value.ToString("d MMMM yyyy");

                    Days = dtDays.Value.ToString("yyyy-MM-dd");
                    DateFrom = "";
                    DateTo = "";
                    Months = "";
                    Year = "";
                    Item = "";
                }
                else if (rdbDate.Checked)
                {
                    strCondition += "";
                    strCondition += dtFrom.Value.ToString("d MMMM yyyy") + " - " + dtTo.Value.ToString("d MMMM yyyy");

                    Days = "";
                    DateFrom = dtFrom.Value.ToString("yyyy-MM-dd");
                    DateTo = dtTo.Value.ToString("yyyy-MM-dd");
                    Months = "";
                    Year = "";
                    Item = "";
                }
                else if (rdbMonth.Checked)
                {
                    strCondition += "";
                    strCondition += dtMonth.Value.ToString("MMMM yyyy");

                    Days = "";
                    DateFrom = "";
                    DateTo = "";
                    Months = dtMonth.Value.ToString("MM/yyyy");
                    Year = "";
                    Item = "";
                }
                else if (rdbYear.Checked)
                {
                    strCondition += "";
                    strCondition += dtYear.Value.ToString("ปี yyyy");

                    Days = "";
                    DateFrom = "";
                    DateTo = "";
                    Months = "";
                    Year = dtYear.Value.ToString("yyyy");
                    Item = "";
                }
                else if (rdbAll.Checked)
                {
                    strCondition += "";
                    strCondition += $"\"{txtItem.Text}\"";

                    Days = "";
                    DateFrom = "";
                    DateTo = "";
                    Months = "";
                    Year = "";
                    Item = txtItem.Text;
                }

                if (cbbCategory.SelectedValue.ToString() != "0")
                {
                    CategoryId = cbbCategory.SelectedValue.ToString();
                    strCondition += " (" + cbbCategory.Text + ")";
                }

                Receipt = txtReceipt.Text;
                strCondition += txtReceipt.Text != "" ? " / เลขที่ " + txtReceipt.Text : "";

                DataTable dtSource = GetDataExpense(Days, DateFrom, DateTo, Months, Year, CategoryId, Receipt, Item);

                if (dtSource == null || dtSource.Columns.Count == 0)
                {
                    dtSource = new DataTable();
                }

                DataTable dtResult = dtSource.Clone();

                foreach (DataRow row in dtSource.Rows)
                {
                    dtResult.ImportRow(row);
                }

                Report.Print(dtResult, "dsExpense", "rptExpenses.rdlc", strCondition);
                strCondition = "";
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public DataTable GetDataExpense(string Days, string DateFrom, string DateTo, string Months, string Year, string CategoryId, string Receipt, string Item)
        {
            string[,] Parameter = new string[,]
            {
                {"@Days", Days},
                {"@DateFrom", DateFrom},
                {"@DateTo", DateTo},
                {"@Months", Months},
                {"@Year", Year},
                {"@CategoryId", CategoryId},
                {"@Receipt", Receipt},
                {"@Item", Item}
            };

            db.Get(Store.FnGetReportIncomeExpense, Parameter, out Error, out dt);
            return dt;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rdbAll_CheckedChanged(object sender, EventArgs e)
        {
            txtItem.Focus();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string strCondition = "";
                string Days = "";
                string DateFrom = "";
                string DateTo = "";
                string Months = "";
                string Year = "";
                string Item = "";
                string CategoryId = "";
                string Receipt = "";

                if (rdbDays.Checked)
                {
                    strCondition += "";
                    strCondition += dtDays.Value.ToString("d MMMM yyyy");

                    Days = dtDays.Value.ToString("yyyy-MM-dd");
                    DateFrom = "";
                    DateTo = "";
                    Months = "";
                    Year = "";
                    Item = "";
                }
                else if (rdbDate.Checked)
                {
                    strCondition += "";
                    strCondition += dtFrom.Value.ToString("d MMMM yyyy") + " - " + dtTo.Value.ToString("d MMMM yyyy");

                    Days = "";
                    DateFrom = dtFrom.Value.ToString("yyyy-MM-dd");
                    DateTo = dtTo.Value.ToString("yyyy-MM-dd");
                    Months = "";
                    Year = "";
                    Item = "";
                }
                else if (rdbMonth.Checked)
                {
                    strCondition += "";
                    strCondition += dtMonth.Value.ToString("MMMM yyyy");

                    Days = "";
                    DateFrom = "";
                    DateTo = "";
                    Months = dtMonth.Value.ToString("MM/yyyy");
                    Year = "";
                    Item = "";
                }
                else if (rdbYear.Checked)
                {
                    strCondition += "";
                    strCondition += dtYear.Value.ToString("ปี yyyy");

                    Days = "";
                    DateFrom = "";
                    DateTo = "";
                    Months = "";
                    Year = dtYear.Value.ToString("yyyy");
                    Item = "";
                }
                else if (rdbAll.Checked)
                {
                    strCondition += "";
                    strCondition += $"\"{txtItem.Text}\"";

                    Days = "";
                    DateFrom = "";
                    DateTo = "";
                    Months = "";
                    Year = "";
                    Item = txtItem.Text;
                }

                if (cbbCategory.SelectedValue.ToString() != "0")
                {
                    CategoryId = cbbCategory.SelectedValue.ToString();
                    strCondition += " (" + cbbCategory.Text + ")";
                }

                Receipt = txtReceipt.Text;
                strCondition += txtReceipt.Text != "" ? " / เลขที่ " + txtReceipt.Text : "";

                DataTable dtSource = GetDataExpense(Days, DateFrom, DateTo, Months, Year, CategoryId, Receipt, Item);

                if (dtSource == null || dtSource.Columns.Count == 0)
                {
                    dtSource = new DataTable();
                }

                DataTable dtResult = dtSource.Clone();

                foreach (DataRow row in dtSource.Rows)
                {
                    dtResult.ImportRow(row);
                }

                Report.Print(dtResult, "dsExpense", "rptExpense.rdlc", strCondition);
                strCondition = "";
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }
    }
}
