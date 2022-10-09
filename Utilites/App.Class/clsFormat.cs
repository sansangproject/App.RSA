using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SANSANG.Class
{
    public class clsFormat
    {
        private const string moneyFormat = "{0:#,###,##0.#0}";
        private const string numberFormat = "{0:#,###,##0}";

        public string Tss
        {
            get { return Tss; }
            set { Tss = value; }
        }

        public string PhoneNumber(Object sender, KeyPressEventArgs e, TextBox txt, String srtType, String strRegion)
        {
            if (srtType == "BKK")
            {
                if (strRegion == "TH") //0-2-123-4567
                {
                    if (e.KeyChar == (char)Keys.Back)
                    {
                        return "";
                    }
                    else
                    {
                        if (txt.TextLength == 1)
                        {
                            txt.Text += "-";
                            txt.SelectionStart = 2;
                            txt.SelectionLength = 0;
                        }
                        if (txt.TextLength == 3)
                        {
                            txt.Text += "-";
                            txt.SelectionStart = 4;
                            txt.SelectionLength = 0;
                        }
                        if (txt.TextLength == 7)
                        {
                            txt.Text += "-";
                            txt.SelectionStart = 8;
                            txt.SelectionLength = 0;
                        }

                        return txt.Text;
                    }
                }
                else //+66-2-123-4567
                {
                    if (e.KeyChar == (char)Keys.Back)
                    {
                        return "";
                    }
                    else
                    {
                        if (txt.TextLength == 1)
                        {
                            txt.Text = "+66-";
                            txt.SelectionStart = 4;
                            txt.SelectionLength = 0;
                        }
                        if (txt.TextLength == 5)
                        {
                            txt.Text += "-";
                            txt.SelectionStart = 6;
                            txt.SelectionLength = 0;
                        }
                        if (txt.TextLength == 9)
                        {
                            txt.Text += "-";
                            txt.SelectionStart = 10;
                            txt.SelectionLength = 0;
                        }

                        return "";
                    }
                }
            }
            else if (srtType == "HOME")
            {
                if (strRegion == "TH") //0-53-12-3456
                {
                    if (e.KeyChar == (char)Keys.Back)
                    {
                        return "";
                    }
                    else
                    {
                        if (txt.TextLength == 1)
                        {
                            txt.Text += "-";
                            txt.SelectionStart = 2;
                            txt.SelectionLength = 0;
                        }
                        if (txt.TextLength == 4)
                        {
                            txt.Text += "-";
                            txt.SelectionStart = 5;
                            txt.SelectionLength = 0;
                        }
                        if (txt.TextLength == 7)
                        {
                            txt.Text += "-";
                            txt.SelectionStart = 8;
                            txt.SelectionLength = 0;
                        }

                        return txt.Text;
                    }
                }
                else //+66-53-12-3456
                {
                    if (e.KeyChar == (char)Keys.Back)
                    {
                        return "";
                    }
                    else
                    {
                        if (txt.TextLength == 1)
                        {
                            txt.Text = "+66-";
                            txt.SelectionStart = 4;
                            txt.SelectionLength = 0;
                        }
                        if (txt.TextLength == 6)
                        {
                            txt.Text += "-";
                            txt.SelectionStart = 7;
                            txt.SelectionLength = 0;
                        }
                        if (txt.TextLength == 9)
                        {
                            txt.Text += "-";
                            txt.SelectionStart = 10;
                            txt.SelectionLength = 0;
                        }

                        return txt.Text;
                    }
                }
            }
            else if (srtType == "MOBILE")
            {
                if (strRegion == "TH") //08-1123-4567
                {
                    if (e.KeyChar == (char)Keys.Back)
                    {
                        return "";
                    }
                    else
                    {
                        if (txt.TextLength == 2)
                        {
                            txt.Text = txt.Text + "-";
                            txt.SelectionStart = 4;
                            txt.SelectionLength = 0;
                        }
                        if (txt.TextLength == 7)
                        {
                            txt.Text += "-";
                            txt.SelectionStart = 8;
                            txt.SelectionLength = 0;
                        }
                        return txt.Text;
                    }
                }
                else //+668-1123-4567
                {
                    if (e.KeyChar == (char)Keys.Back)
                    {
                        return "";
                    }
                    else
                    {
                        if (txt.TextLength == 1)
                        {
                            txt.Text = "+66";
                            txt.SelectionStart = 4;
                            txt.SelectionLength = 0;
                        }
                        if (txt.TextLength == 4)
                        {
                            txt.Text += "-";
                            txt.SelectionStart = 5;
                            txt.SelectionLength = 0;
                        }
                        if (txt.TextLength == 9)
                        {
                            txt.Text += "-";
                            txt.SelectionStart = 10;
                            txt.SelectionLength = 0;
                        }
                        return txt.Text;
                    }
                }
            }
            else
            {
                return "";
            }
        }

        public string EMSTrack(String srtType, String strCode)
        {
            string str = "";

            try
            {
                if (srtType == "EMS")
                {
                    //RB 4688 6645 7 TH
                    str += strCode.Substring(0, 2).ToString() + " ";
                    str += strCode.Substring(2, 4).ToString() + " ";
                    str += strCode.Substring(6, 4).ToString() + " ";
                    str += strCode.Substring(10, 1).ToString() + " ";
                    str += strCode.Substring(11, 2).ToString();
                    return str;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string Time(TextBox tb)
        {
            try
            {
                if (tb.TextLength >= 6 && tb.TextLength <= 8)
                {
                    string H = "";
                    string M = "";
                    string S = "";
                    string HMS = "";

                    H = tb.Text.Substring(0, 2);
                    M = tb.Text.Substring(2, 2);
                    S = tb.Text.Substring(4, 2);

                    HMS = H + ":" + M + ":" + S;

                    return HMS;
                }
                else
                {
                    return tb.Text;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string TSSId(Object sender, KeyPressEventArgs e, TextBox txt)
        {
            var regex = new Regex(@"[^a-zA-Z0-9\s]");

            if (e.KeyChar == (char)Keys.Back)
            {
                return "";
            }
            else //E6-8BD233EE9C-8
            {
                e.KeyChar = Char.ToUpper(e.KeyChar);

                if (txt.TextLength == 2)
                {
                    txt.Text = txt.Text.ToUpper() + "-";
                    txt.SelectionStart = 3;
                    txt.SelectionLength = 0;
                }
                else if (txt.TextLength == 13)
                {
                    txt.Text = txt.Text.ToUpper() + "-";
                    txt.SelectionStart = 14;
                    txt.SelectionLength = 0;
                }

                return txt.Text.ToUpper();
            }
        }

        public string Number(String strNumber, string formatNumber = null)
        {
            try
            {
                if (formatNumber == "Money" || formatNumber == null)
                {
                    return String.Format(moneyFormat, Convert.ToDouble(strNumber));
                }
                else if (formatNumber == "Number")
                {
                    return String.Format(numberFormat, Convert.ToDouble(strNumber));
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}