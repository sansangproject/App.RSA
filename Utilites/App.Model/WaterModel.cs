using System;

namespace SANSANG.Utilites.App.Model
{

    public class WaterModel
    {
        public double Amount { get; set; }
        public DateTime ReadDate { get; set; }
        public DateTime PayDate { get; set; }
        public int Unit { get; set; }
        public string ReceiptId { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}