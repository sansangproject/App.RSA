using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using SANSANG.Utilites.App.Model;

namespace SANSANG.Class
{
    public class clsTextbox
    {
        public void SetValue(TextBox txt, string value)
        {
            try
            {
                txt.Focus();
                txt.Text = value;
            }
            catch
            {
                
            }
        }
    }
}