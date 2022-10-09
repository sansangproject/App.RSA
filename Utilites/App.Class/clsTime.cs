using System;
using System.Windows.Forms;

namespace SANSANG.Class
{
    public class clsTime
    {
        public string time = "";
        private clsFunction Fn = new clsFunction();

        public void ShowTime(Label lblTime, string strUserId)
        {
            try
            {
                Timer t = new Timer();
                t.Interval = 1000;
                t.Tick += new EventHandler(this.t_Tick);
                t.Start();

                lblTime.Text = time;
            }
            catch (Exception ex)
            {
                clsLog Log = new clsLog();
                Log.WriteLogData("clsTime", "clsTime", strUserId, ex.Message);
            }
        }

        private void t_Tick(object sender, EventArgs e)
        {
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;

            time = "";

            if (hh < 10)
            {
                time += "0" + hh;
            }
            else
            {
                time += hh;
            }

            time += ":";

            if (mm < 10)
            {
                time += "0" + mm;
            }
            else
            {
                time += mm;
            }

            time += ":";

            if (ss < 10)
            {
                time += "0" + ss;
            }
            else
            {
                time += ss;
            }
        }

        public string GetTime(string strTime, string strSymbol)
        {
            try
            {
                string newTime = "";
                string strHour = "";
                string strMinute = "";

                if (strTime.Length >= 4)
                {
                    strHour = strTime.Substring(0, 2);
                    strMinute = Fn.SubstringRight(strTime,2);
                    newTime = strHour + strSymbol + strMinute;
                    return newTime;
                }

                return strTime;
            }
            catch (Exception)
            {
                return strTime;
            }
        }
    }
}