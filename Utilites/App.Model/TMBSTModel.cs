using System;
using System.Collections.Generic;

namespace SANSANG.Utilites.App.Model
{
    public class TTBSTModel
    {
        public string StatmentDate { get; set; }
        public string StatmentTime { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public string rawDate { get; set; }
        public string rawTime { get; set; }
        public string PaymentCode { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Branch { get; set; }
        public string Details { get; set; }
        public string TransactionType { get; set; }
        public string Channel { get; set; }
    }
}
