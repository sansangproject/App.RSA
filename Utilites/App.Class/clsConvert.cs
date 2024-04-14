using Microsoft.VisualBasic.Devices;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SANSANG.Class
{
    public class clsConvert
    {
        private clsLog Log = new clsLog();

        public string NullToString(string value)
        {
            try
            {
                if (value == null)
                {
                    return "";
                }
                else
                {
                    return value;
                }
            }
            catch
            {
                return "";
            }
        }

        public string StringToDateTime(string Value)
        {
            try
            {
                if (Value != "" && Value != null)
                {
                    int Indexs = Value.IndexOf("+");
                    Value = Value.Substring(0, Indexs);
                    DateTime Dates = Convert.ToDateTime(Value).AddYears(-543);
                    return Dates.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }


















































        public string StringToDoubleString(string strNumber)
        {
            try
            {
                int NumLength = strNumber.Length;
                string strInteger = strNumber.Substring(0, NumLength - 2);
                string strDecimal = strNumber.Substring(NumLength - 2, 2);

                return strInteger + "." + strDecimal;
            }
            catch (Exception ex)
            {
                Log.WriteLogData("clsConvert", "Convert Class - StringToDoubleString", "TSS", ex.Message);
                return "";
            }
        }

        public double StringToDouble(string strNumber)
        {
            try
            {
                int NumLength = strNumber.Length;
                string strInteger = strNumber.Substring(0, NumLength - 2);
                string strDecimal = strNumber.Substring(NumLength - 2, 2);
                double douNumber = Convert.ToDouble(strInteger + "." + strDecimal);
                return douNumber;
            }
            catch (Exception ex)
            {
                Log.WriteLogData("clsConvert", "Convert Class - StringToDouble", "TSS", ex.Message);
                return 0;
            }
        }

        public DateTime StringToDateTimes(string strDate)
        {
            try
            {
                int NumLength = strDate.Length;
                string strDay = strDate.Substring(0, 2);
                string strMonth = strDate.Substring(2, 2);
                string strYear = Convert.ToString(Convert.ToInt64("25" + strDate.Substring(4, 2)) - 543);
                string strDMY = strDay + "/" + strMonth + "/" + strYear;
                DateTime dtValue = DateTime.ParseExact(strDMY, "dd/MM/yyyy", null);
                return dtValue;
            }
            catch (Exception ex)
            {
                Log.WriteLogData("clsConvert", "Convert Class - StringToDateTime", "TSS", ex.Message);
                return DateTime.Now;
            }
        }

        public string StringToDateTimeStrings(string strDate)
        {
            try
            {
                int NumLength = strDate.Length;
                string strDay = Convert.ToString(Convert.ToInt64(strDate.Substring(0, 2)) - Convert.ToInt64(strDate.Substring(6, 1)));
                string strMonth = strDate.Substring(2, 2);
                string strYear = Convert.ToString(Convert.ToInt64("25" + strDate.Substring(4, 2)) - 543);
                string strDMY = strDay + "/" + strMonth + "/" + strYear;
                return strDMY;
            }
            catch (Exception ex)
            {
                Log.WriteLogData("clsConvert", "Convert Class - StringToDateTimeString", "TSS", ex.Message);
                return "";
            }
        }

        public string ImageToBase64(string Path)
        {
            try
            {
                using (Image image = Image.FromFile(Path))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        string base64String = Convert.ToBase64String(imageBytes);
                        return base64String;
                    }
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public Image Base64ToImage(string strBase64)
        {
            Image img = null;
            try
            {
                byte[] imageBytes = Convert.FromBase64String(strBase64);
                using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    Image image = Image.FromStream(ms, true);
                    img = image;
                    return img;
                }
            }
            catch (Exception)
            {
                return img;
            }
        }

        public string StringToDateAndTime(string strDate)
        {
            try
            {
                if (strDate != "" && strDate != null)
                {
                    int NumLength = strDate.Length;
                    string strDay = strDate.Substring(0, 2);
                    string strMonth = strDate.Substring(3, 2);
                    string strYear = Convert.ToString(Convert.ToInt32(strDate.Substring(6, 4)) - 543);

                    string strTime = strDate.Substring(11, 8);

                    string strDateTime = strYear + "-" + strMonth + "-" + strDay + " " + strTime;
                    return strDateTime;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        public Bitmap PictureBoxGrayScale(PictureBox pictureBox)
        {
            Bitmap originalBitmap = (Bitmap)pictureBox.Image;
            Bitmap newBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);
          
            using (Graphics Graphics = Graphics.FromImage(newBitmap))
            {
                ColorMatrix colorMatrix = new ColorMatrix(
                   new float[][]
                   {
                         new float[] {.3f, .3f, .3f, 0, 0},
                         new float[] {.59f, .59f, .59f, 0, 0},
                         new float[] {.11f, .11f, .11f, 0, 0},
                         new float[] {0, 0, 0, 1, 0},
                         new float[] {0, 0, 0, 0, 1}
                   });
                
                using (ImageAttributes attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    Graphics.DrawImage(originalBitmap, new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height),
                                0, 0, originalBitmap.Width, originalBitmap.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return newBitmap;
        }

        public Bitmap ImageGrayScale(Bitmap picture)
        {
            Bitmap originalBitmap = picture;
            Bitmap newBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height);

            using (Graphics Graphics = Graphics.FromImage(newBitmap))
            {
                ColorMatrix colorMatrix = new ColorMatrix(
                   new float[][]
                   {
                         new float[] {.5f, .5f, .5f, 0, 0},
                         new float[] {.60f, .60f, .60f, 0, 0},
                         new float[] {.10f, .10f, .10f, 0, 0},
                         new float[] {0, 0, 0, 1, 0},
                         new float[] {0, 0, 0, 0, 1}
                   });

                using (ImageAttributes attributes = new ImageAttributes())
                {
                    attributes.SetColorMatrix(colorMatrix);
                    Graphics.DrawImage(originalBitmap, new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height),
                                0, 0, originalBitmap.Width, originalBitmap.Height, GraphicsUnit.Pixel, attributes);
                }
            }
            return newBitmap;
        }

        public void TitleCase(TextBox TextBox)
        {
            string Data = TextBox.Text;
            string Values = "";

            TextInfo Culture = new CultureInfo("en-US", false).TextInfo;
            int Number = 0;

            try
            {
                TextBox.Text = "";
                string[] Text = Data.Split(' ');

                foreach (var Words in Text)
                {
                    string NewWord = "";
                    string Texts = "";

                    Regex rgx = new Regex("[^a-zA-Z -]");
                    Texts = rgx.Replace(Words, "").ToLower();

                    if (Texts.Length >= 3)
                    {
                        NewWord = Culture.ToTitleCase(Texts);
                        Values += Number == 0 ? NewWord : " " + NewWord;
                    }
                    else
                    {
                        Values += Number == 0 ? Words : " " + Words;
                    }

                    Number++;
                }

                TextBox.Text = Values;
            }
            catch
            {
                TextBox.Text = Data;
            }
        }
}
}