using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using SANSANG.Utilites.App.Model;

namespace SANSANG.Class
{
    public class TssTextBoxNumber : TextBox
    {

        bool isPlaceHolder = true;
        string _placeHolder;

        public string PlaceHolder
        {
            get { return _placeHolder; }
            set
            {
                _placeHolder = value;
                setPlaceholder();
            }
        }

        public new string Text
        {
            get => isPlaceHolder ? string.Empty : base.Text;
            set => base.Text = value;
        }

        private void setPlaceholder()
        {
            if (string.IsNullOrEmpty(base.Text))
            {
                base.Text = PlaceHolder;
                this.ForeColor = Color.Gray;
                this.Font = new Font(this.Font, FontStyle.Italic);
                this.BorderStyle = BorderStyle.None;
                this.Font = new Font("Mitr Light", 9.75F);
                this.ForeColor = Color.Gray;
                this.Size = new Size(141, 21);

                isPlaceHolder = true;
            }
        }

        private void removePlaceHolder()
        {
            if (isPlaceHolder)
            {
                base.Text = "";
                this.ForeColor = Color.Gray;
                this.Font = new Font(this.Font, FontStyle.Italic);
                this.BorderStyle = BorderStyle.None;
                this.Font = new Font("Mitr Light", 9.75F);
                this.ForeColor = Color.Black;
                this.Size = new Size(141, 21);
                isPlaceHolder = false;
            }
        }
        public TssTextBoxNumber()
        {
            GotFocus += removePlaceHolder;
            LostFocus += setPlaceholder;
        }

        private void setPlaceholder(object sender, EventArgs e)
        {
            setPlaceholder();
        }

        private void removePlaceHolder(object sender, EventArgs e)
        {
            removePlaceHolder();
        }
    }
}
