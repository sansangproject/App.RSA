using Microsoft.ReportingServices.Interfaces;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace RecordSystemApplication.App.Program.Application.Print
{
    public partial class frmReportExpense : Form
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

        public frmReportExpense()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (cbbCategory.SelectedValue.ToString() != "0")
            {
                DataTable Data = GetDataExpense("string Days", "string DateFrom", "string DateTo", "string Months", "string Year", "string CategoryId", "string Receipt");
                DataTable Datas = Data.Clone();

                using (var Stream = new MemoryStream())
                {
                        foreach (DataRow dr in Data.Rows)
                        {
                            Datas.Rows.Add(dr.ItemArray);
                        }
                }

                Report.Print(Datas, "dsExpense", "rptExpense.rdlc");
            }
        }
        public DataTable GetDataExpense(string Days, string DateFrom, string DateTo, string Months, string Year, string CategoryId, string Receipt)
        {
            string[,] Parameter = new string[,]
            {
                {"@Days", Days},
                {"@DateFrom", DateFrom},
                {"@DateTo", DateTo},
                {"@Months", Months},
                {"@Year", Year},
                {"@CategoryId", CategoryId},
                {"@Receipt", Receipt}
            };

            db.Get(Store.FnGetReportIncomeExpense, Parameter, out Error, out dt);
            return dt;
        }
    }
}
