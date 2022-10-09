using System;
using System.Linq;
using System.Windows.Forms;

namespace SANSANG.Class
{
    public class clsEvent
    {
        private clsLog Log = new clsLog();
        public string strAppCode = "SANSANG.Utilites.Class";
        public string strAppName = "clsEvent";
        public string strUserId = "Admin";

        public void SetAutoMoney(ComboBox Payment, ComboBox Money)
        {
            try
            {
                //if (Payment.SelectedValue.ToString() == "1488")
                //{
                //    Money.SelectedValue = 1030;
                //}

                //if (Payment.SelectedValue.ToString() == "1486")
                //{
                //    Money.SelectedValue = 1023;
                //}

                //if (Payment.SelectedValue.ToString() == "1060")
                //{
                //    Money.SelectedValue = 1001;
                //}

                //if (Payment.SelectedValue.ToString() == "2064")
                //{
                //    Money.SelectedValue = 4051;
                //}
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }


















        public void AmountKeyPress(object sender, KeyPressEventArgs e, TextBox txtAmount)
        {
            try
            {
                int valueKey = Convert.ToInt16(e.KeyChar);

                if (Char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8)
                {
                    if (new int[] { 43, 3665, 3666, 3667, 3668, 3669, 3670, 3671, 3591}.Contains(valueKey))
                    {
                        if (valueKey == 43)
                        {
                            txtAmount.Text += "1";
                        }
                        if (valueKey == 3665)
                        {
                            txtAmount.Text += "2";
                        }
                        if (valueKey == 3666)
                        {
                            txtAmount.Text += "3";
                        }
                        if (valueKey == 3667)
                        {
                            txtAmount.Text += "4";
                        }
                        if (valueKey == 3668)
                        {
                            txtAmount.Text += "5";
                        }
                        if (valueKey == 3669)
                        {
                            txtAmount.Text += "8";
                        }
                        if (valueKey == 3670)
                        {
                            txtAmount.Text += "9";
                        }
                        if (valueKey == 3671)
                        {
                            txtAmount.Text += "0";
                        }
                        if (valueKey == 3591)
                        {
                            txtAmount.Text += ".";
                        }

                        e.Handled = true;
                        txtAmount.Focus();
                        txtAmount.Select(txtAmount.Text.Length, 0);
                    }
                }
                else if (new int[] { 43, 3653, 47, 45, 3616, 3606, 3640, 3638, 3588, 3605, 3592, 3641, 3647 }.Contains(valueKey))
                {
                    if (valueKey == 3653)
                    {
                        txtAmount.Text += "1";
                    }
                    if (valueKey == 43)
                    {
                        txtAmount.Text += "1";
                    }
                    if (valueKey == 47)
                    {
                        txtAmount.Text += "2";
                    }
                    if (valueKey == 45)
                    {
                        txtAmount.Text += "3";
                    }
                    if (valueKey == 3616)
                    {
                        txtAmount.Text += "4";
                    }
                    if (valueKey == 3606)
                    {
                        txtAmount.Text += "5";
                    }
                    if (valueKey == 3641)
                    {
                        txtAmount.Text += "6";
                    }
                    if (valueKey == 3640)
                    {
                        txtAmount.Text += "6";
                    }
                    if (valueKey == 3638)
                    {
                        txtAmount.Text += "7";
                    }
                    if (valueKey == 3647)
                    {
                        txtAmount.Text += "7";
                    }
                    if (valueKey == 3588)
                    {
                        txtAmount.Text += "8";
                    }
                    if (valueKey == 3605)
                    {
                        txtAmount.Text += "9";
                    }
                    if (valueKey == 3592)
                    {
                        txtAmount.Text += "0";
                    }

                    e.Handled = true;
                    txtAmount.Focus();
                    txtAmount.Select(txtAmount.Text.Length, 0);
                }
                else
                {
                    e.Handled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }
    }
}