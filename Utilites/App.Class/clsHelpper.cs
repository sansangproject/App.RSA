using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SANSANG.Class
{
    public class clsHelpper
    {
        public bool CheckboxTicker(object sender, Form frm)
        {
            var pb = (PictureBox)sender;
            string pbName = pb.Name.ToString();

            string pattern = @"pb_(\w+)_(\w+)";
            Match n = Regex.Match(pbName, pattern);

            string strNameTrue = n.Groups[2].Value == "True" ? pbName : "pb_" + n.Groups[1].Value + "_True";
            string strNameFalse = n.Groups[2].Value == "False" ? pbName : "pb_" + n.Groups[1].Value + "_False";

            Boolean value = n.Groups[2].Value == "True" ? true : false;
            PictureBox pbTrue = frm.Controls.Find(strNameTrue, true).FirstOrDefault() as PictureBox;
            PictureBox pbFalse = frm.Controls.Find(strNameFalse, true).FirstOrDefault() as PictureBox;
            CheckBox cb = frm.Controls.Find("cb_" + n.Groups[1].Value, true).FirstOrDefault() as CheckBox;

            if (value == true)
            {
                pbFalse.Show();
                pbTrue.Hide();
                cb.Checked = false;
                return false;
            }
            else
            {
                pbFalse.Hide();
                pbTrue.Show();
                cb.Checked = true;
                return true;
            }
        }

        public void ShowDate(object sender, Form frm)
        {
            try
            {
                var DatePicker = (DateTimePicker)sender;
                string Value = DatePicker.Value.ToString("dd MMM yyyy");
                string Name = "txt" + DatePicker.Name.ToString();

                TextBox Textbox = frm.Controls.Find(Name, true).FirstOrDefault() as TextBox;

                if (Textbox != null)
                {
                    Textbox.Text = Value;
                }
            }
            catch
            {

            }
        }
    }
}