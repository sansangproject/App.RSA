using Microsoft.ReportingServices.Interfaces;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Logging;
using Org.BouncyCastle.Ocsp;
using SANSANG;
using SANSANG.Class;
using SANSANG.Constant;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Telerik.WinControls;
using Tesseract;
using static Telerik.WinControls.VirtualKeyboard.VirtualKeyboardNativeMethods;

namespace App
{
    public partial class FrmCodeGenerate : Form
    {
        public string AppCode = "SETCG00";
        public string AppName = "FrmCodeGenerate";

        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;
        public string AbbProvince;
        public bool Start = true;
        private Timer Timer = new Timer();
        private clsLog Log = new clsLog();
        private clsSetting Setting = new clsSetting();
        private clsDataList List = new clsDataList();
        private DataListConstant DataList = new DataListConstant();
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(25);
        private clsFunction Function = new clsFunction();
        private clsDate Date = new clsDate();
        private TableConstant Table = new TableConstant();
        
        public FrmCodeGenerate(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
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
            Timer.Interval = (1000);
            Timer.Start();
            Timer.Tick += new EventHandler(LoadList);
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetLists(cbbProvince, string.Format(DataList.ProvinceId, "status", "1000"));
            cbbProvince.BackColor = Color.WhiteSmoke;

            Start = true;
            AbbProvince = "";
            gbForm.Enabled = true;
            Clear();
            Timer.Stop();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            Function.ClearAll(gbForm);
            cbbProvince.BackColor = Color.WhiteSmoke;
            dtTravelDateFrom.Value = DateTime.Now;
            dtTravelDateTo.Value = DateTime.Now;
            Start = false;
        }

        public string GenerateTripDescription(string tripCode)
        {
            if (string.IsNullOrWhiteSpace(tripCode) || tripCode.Length != 13)
                throw new ArgumentException("Invalid Trip Code");

            string tripType = tripCode.Substring(0, 1);
            string provinceCode = tripCode.Substring(1, 3);
            int year = 2000 + int.Parse(tripCode.Substring(4, 2));
            int month = int.Parse(tripCode.Substring(6, 2));
            int day = int.Parse(tripCode.Substring(8, 2));
            int tripDays = int.Parse(tripCode.Substring(11, 2));

            string provinceNameTh = Function.getValue(Table.Province, "AbbEn", "'" + provinceCode + "'", "Name");

            var index = cbbProvince.FindStringExact(provinceNameTh);
            cbbProvince.SelectedIndex = index >= 0 ? index : 0;

            txtDay.Text = tripCode.Substring(11, 2);

            var travelDate = new DateTime(year, month, day);

            dtTravelDateFrom.Value = travelDate;
            dtTravelDateTo.Value = travelDate.AddDays(tripDays-1);

            var thaiCulture = new CultureInfo("th-TH");

            string dayOfWeekTh = travelDate.ToString("dddd", thaiCulture);
            string monthTh = travelDate.ToString("MMMM", thaiCulture);
            int displayYear = travelDate.Year;

            string tripPrefix = tripType == "T" ? "ท่องเที่ยว" : "ทริป";

            return $"{tripPrefix}{provinceNameTh} วัน{dayOfWeekTh}ที่ {day} {monthTh} {displayYear} ({tripDays} วัน)";
        }

        public string GenerateTripCodeFromUI(
            string provinceCode,
            DateTime travelDateFrom,
            DateTime travelDateTo)
        {
            if (string.IsNullOrWhiteSpace(provinceCode))
            {
                cbbProvince.BackColor = Color.Salmon;
                return string.Empty;
            }

            if (travelDateTo < travelDateFrom)
                return string.Empty;

            const string tripType = "T";

            DateTime tripDate = travelDateFrom;

            int numberOfTripDays = (travelDateTo - travelDateFrom).Days + 1;

            string year = tripDate.ToString("yy");
            string month = tripDate.ToString("MM");
            string day = tripDate.ToString("dd");

            int dayOfWeek = ((int)tripDate.DayOfWeek + 1);

            string tripDays = numberOfTripDays.ToString("D2");

            return $"{tripType}{provinceCode}{year}{month}{day}{dayOfWeek}{tripDays}";

        }


        protected void btnEncode_Click(object sender, EventArgs e)
        {
            string provinceCode = Function.getValue(Table.Province, "Id", Function.getComboboxId(cbbProvince), "AbbEn");
            DateTime travelFrom = dtTravelDateFrom.Value;
            DateTime travelTo = dtTravelDateTo.Value;

            string tripCode = GenerateTripCodeFromUI(provinceCode, travelFrom, travelTo);

            txtCode.Text = tripCode;
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCode.Text))
            {
                txtDetail.Text = GenerateTripDescription(txtCode.Text);
            }
        }

        private void cbbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Start && cbbProvince.SelectedIndex != 0)
                {
                    cbbProvince.BackColor = Color.WhiteSmoke;

                    AbbProvince = Function.getValue(
                        Table.Province,
                        "Id",
                        Function.getComboboxId(cbbProvince),
                        "AbbEn"
                    );
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }
    }
}