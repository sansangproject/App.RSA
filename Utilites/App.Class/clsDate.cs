using System;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Database;

namespace SANSANG.Class
{
    public partial class clsDate
    {
        public clsFunction Fn = new clsFunction();
        public string Year = "";
        public string Month = "";
        public string Months = "";
        public string Day = "";
        public string strDate = "";

        public void SetValue(DateTimePicker Dt, String Country, String Type)
        {
            int year = Convert.ToInt32(DateTime.Now.ToString("yyyy", Fn.SetFormatDate(Country)));
            int month = Convert.ToInt32(DateTime.Now.ToString("MM", Fn.SetFormatDate(Country)));
            int day = Convert.ToInt32(DateTime.Now.ToString("dd", Fn.SetFormatDate(Country)));

            Dt.Value = new DateTime(year, month, day);
        }

        public void SetValue(DateTimePicker Dt, String Country, String Type, int intValue)
        {
            int year = Convert.ToInt32(DateTime.Now.ToString("yyyy", Fn.SetFormatDate(Country)));
            int month = Convert.ToInt32(DateTime.Now.ToString("MM", Fn.SetFormatDate(Country)));
            int day = Convert.ToInt32(DateTime.Now.ToString("dd", Fn.SetFormatDate(Country)));

            if (Type == "Now")
            {
                Dt.Value = new DateTime(year, month, day);
            }
            else if (Type == "SetY")
            {
                Dt.Value = new DateTime(intValue, month, day);
            }
            else if (Type == "SetM")
            {
                Dt.Value = new DateTime(year, intValue, day);
            }
            else if (Type == "SetD")
            {
                Dt.Value = new DateTime(year, month, intValue);
            }
        }

        public void SetValue(DateTimePicker Dt, String Country, String Type, int intValue1, int intValue2)
        {
            int year = Convert.ToInt32(DateTime.Now.ToString("yyyy", Fn.SetFormatDate(Country)));
            int month = Convert.ToInt32(DateTime.Now.ToString("MM", Fn.SetFormatDate(Country)));
            int day = Convert.ToInt32(DateTime.Now.ToString("dd", Fn.SetFormatDate(Country)));

            if (Type == "Now")
            {
                Dt.Value = new DateTime(year, month, day);
            }
            else if (Type == "SetYM")
            {
                Dt.Value = new DateTime(intValue1, intValue2, day);
            }
            else if (Type == "SetYD")
            {
                Dt.Value = new DateTime(intValue1, month, intValue2);
            }
            else if (Type == "SetMD")
            {
                Dt.Value = new DateTime(year, intValue1, intValue2);
            }
        }

        public void SetValue(DateTimePicker Dt, String Country, String Type, int intYear, int intMonth, int intDay)
        {
            int year = Convert.ToInt32(DateTime.Now.ToString("yyyy", Fn.SetFormatDate(Country)));
            int month = Convert.ToInt32(DateTime.Now.ToString("MM", Fn.SetFormatDate(Country)));
            int day = Convert.ToInt32(DateTime.Now.ToString("dd", Fn.SetFormatDate(Country)));

            if (Type == "Now")
            {
                Dt.Value = new DateTime(year, month, day);
            }
            else if (Type == "SetYMD")
            {
                Dt.Value = new DateTime(intYear, intMonth, intDay);
            }
        }

        public string SetYearBE()
        {
            try
            {
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string SetYearAD()
        {
            try
            {
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string GetDayOfWeek(DateTimePicker dtp)
        {
            try
            {
                DateTime dt = dtp.Value;
                int day = 9;
                string StrDay = "";
                day = (int)dt.DayOfWeek;

                if (day <= 6)
                {
                    StrDay = day == 1 ? "จันทร์" : day == 2 ? "อังคาร" : day == 3 ? "พุธ" : day == 4 ? "พฤหัสบดี" : day == 5 ? "ศุกร์" : day == 6 ? "เสาร์" : day == 0 ? "อาทิตย์" : "";
                }
                return StrDay;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string GetDate(DateTimePicker dtp = null, DateTime dt = default(DateTime), string Language = null, int Format = 0)
        {
            string strLanguage = Language == null ? clsSetting.ReadLanguageSetting() : Language;

            int DateFormat = Format == 0 ? clsSetting.DateFormat() : Format;

            if (dtp != null)
            {
                Year = dtp.Value.ToString("yyyy", Fn.SetFormatDate(strLanguage));
                Month = dtp.Value.ToString("MM");
                Months = dtp.Value.ToString("MMMM");
                Day = dtp.Value.ToString("dd");
            }
            else
            {
                Year = dt.ToString("yyyy", Fn.SetFormatDate(strLanguage));
                Month = dt.ToString("MM");
                Months = dt.ToString("MMMM");
                Day = dt.ToString("dd");
            }
            //dd/MM/yyyy
            if (DateFormat == 1)
            {
                strDate = Day + "/" + Month + "/" + Year;
            }
            //MM/dd/yyyy
            else if (DateFormat == 2)
            {
                strDate = strDate = Month + "/" + Day + "/" + Year;
            }
            //yyyy/MM/dd
            else if (DateFormat == 3)
            {
                strDate = Year + "/" + Month + "/" + Day;
            }
            //"yyyy-MM-dd"
            else if (DateFormat == 4)
            {
                strDate = Year + "-" + Month + "-" + Day;
            }
            //yyyyMMdd
            else if (DateFormat == 5)
            {
                strDate = Year + Month + Day;
            }
            //dd MMMM yyyy
            else if (DateFormat == 6)
            {
                strDate = Day + " " + Months + " " + Year;
            } 
            //MM
            else if (DateFormat == 7)
            {
                strDate = Month;
            } 
            //yyyy
            else if (DateFormat == 8)
            {
                strDate = Year;
            }
            //MMMM yyyy
            else if (DateFormat == 9)
            {
                strDate = Months + " " + Year;
            }
            else
            {
                strDate = "";
            }

            return strDate;
        }

        public string GetTime(DateTimePicker dtp = null, DateTime dt = default(DateTime), string Language = null, int Format = 0)
        {
            string HH = "";
            string MM = "";
            string SS = "";
            string strTime = string.Empty;
            string strLanguage = Language == null ? clsSetting.ReadLanguageSetting() : Language;
            int DateFormat = Format == 0 ? clsSetting.DateFormat() : Format;

            if (dtp != null)
            {
                HH = dtp.Value.ToString("HH");
                MM = dtp.Value.ToString("mm");
                SS = dtp.Value.ToString("ss");
            }

            //HH:MM
            if (DateFormat == 1)
            {
                strTime = HH + ":" + MM;
            }
            //HH:MM:SS
            else if (DateFormat == 2)
            {
                strTime = HH + ":" + MM + ":" + SS;
            }
            else
            {
                strTime = "";
            }

            return strTime;
        }
    }
}