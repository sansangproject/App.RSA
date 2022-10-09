using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using SANSANG.Utilites.App.Model;

namespace SANSANG.Class
{
    public class TssTextBox : TextBox
    {
        bool isPlaceHolder = true;
        string _placeHolderText;
        public string PlaceHolder
        {
            get { return _placeHolderText; }
            set
            {
                _placeHolderText = value;
                //addPlaceholder();
            }
        }

        public new string Text
        {
            get => base.Text;
            set => base.Text = value;
        }
        
        private void addPlaceholder()
        {
            if (string.IsNullOrEmpty(base.Text) && isPlaceHolder)
            {
                base.Text = PlaceHolder;
                this.ForeColor = Color.Gray;
                isPlaceHolder = true;
            }
        }
        
        private void removePlaceHolder()
        {
            if (isPlaceHolder)
            {
                base.Text = "";
                this.ForeColor = Color.Black;
                isPlaceHolder = false;
            }
        }
        public TssTextBox()
        {
            //GotFocus += removePlaceHolder;
            //LostFocus += addPlaceholder;
            //TextChanged += setPlaceholder;
        }

        private void setPlaceholder(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(base.Text))
            //{
            //    isPlaceHolder = true;
            //    addPlaceholder();
            //}
            //else
            //{
            //    isPlaceHolder = false;
            //    this.ForeColor = Color.Black;
            //    removePlaceHolder();
            //}
        }

        private void addPlaceholder(object sender, EventArgs e)
        {
            //addPlaceholder();
        }

        private void removePlaceHolder(object sender, EventArgs e)
        {
            //removePlaceHolder();
        }
    }
}