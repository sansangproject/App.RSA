using System;
using System.Windows.Forms;

namespace SANSANG.Class
{
    public class clsCalculator
    {
        public string Tss
        {
            get { return Tss; }
            set { Tss = value; }
        }

        public void PricebeforeVat(TextBox txtVat, TextBox txtPrice, TextBox txtPriceBeforeVat, TextBox txtVatProduct)
        {
            try
            {
                if (txtVat.Text != "" & txtPrice.Text != "")
                {
                    decimal Price = Convert.ToDecimal(txtPrice.Text);
                    decimal Vat = (Price * Convert.ToDecimal(txtVat.Text)) / (100 + Convert.ToDecimal(txtVat.Text));
                    decimal Total = Price - Vat;
                    txtPriceBeforeVat.Text = String.Format("{0:n}", Total);
                    txtVatProduct.Text = String.Format("{0:n}", Vat);
                }
                else
                {
                    txtPriceBeforeVat.Text = "0.00";
                }
            }
            catch (Exception)
            {
                txtPriceBeforeVat.Text = "0.00";
            }
        }

        public void Discount(TextBox txtPrice, TextBox txtDiscount, TextBox txtBalance)
        {
            try
            {
                if (txtPrice.Text != "" & txtDiscount.Text != "")
                {
                    decimal Price = Convert.ToDecimal(txtPrice.Text);
                    decimal Discount = Convert.ToDecimal(txtDiscount.Text);
                    decimal Total = Price - Discount;
                    txtBalance.Text = String.Format("{0:n}", Total);
                }
                else
                {
                    txtBalance.Text = "0.00";
                }
            }
            catch (Exception)
            {
                txtBalance.Text = "0.00";
            }
        }
    }
}