using System;

namespace SANSANG.Utilites.App.Model
{
    public class ElectricModel
    {
        public double Amount { get; set; }
        public DateTime ReadDate { get; set; }
        public DateTime PayDate { get; set; }
        public int Unit { get; set; }
        public string ReceiptId { get; set; }
        public string CaRefNo { get; set; }
        public string Header { get; set; }
        public string qrLine2 { get; set; }
        public string qrLine3 { get; set; }
        public string qrLine4 { get; set; }
    }
}