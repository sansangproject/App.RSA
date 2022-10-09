using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANSANG.Constant
{
    public class PaymentConstant
    {
        public string Payment;
        public string SubPayment;
        public string Money;

        public string PaymentCode;
        public string SubPaymentCode;
        public string MoneyCode;

        //Power Cash (PC)
        public string PCPaymentName = "PowerCash";
        public string PCSubPaymentCode = "TOP13744";
        public string PCMoneyCode = "POS00-01";

        public string PCPaymentMoneyValue = "CAH00-01";
        public string PCPaymentSubValue = "CAR0008";
        public string PCPaymentValue = "CAR-000-0";
        public string PCPaymentDetailValue = "ค่าเดินทาง | ค่าน้ำมัน - เติมเงิน Power Cash";

        //True Wallet (TW)
        public string TWPaymentName = "TrueWallet";
        public string TWSubPaymentCode = "TOP13747";
        public string TWMoneyCode = "TMW01-01";
                
        public string TWPaymentMoneyValue = "CAH00-01";
        public string TWPaymentSubValue = "TOP13837";
        public string TWPaymentValue = "TOP-001-0";
        public string TWPaymentDetailValue = "เติมเงิน | True Wallet - Top Up";

        //Rabbit Card (RC)
        public string RCPaymentName = "Rabbit Card";
        public string RCSubPaymentCode = "TOP13731";
        public string RCMoneyCode = "RAC00-02";
                      
        public string RCPaymentMoneyValue = "CAH00-01";
        public string RCPaymentSubValue = "TOP13731";
        public string RCPaymentValue = "TOP-001-0";
        public string RCPaymentDetailValue = "เติมเงิน | Rabbit Card - Top Up";

        //AirPay (AP)
        public string APPaymentName = "AirPay";
        public string APSubPaymentCode = "TOP14876";
        public string APMoneyCode = "ACK00-02";
                      
        public string APPaymentMoneyValue = "ARP00-00";
        public string APPaymentSubValue = "TOP14877";
        public string APPaymentValue = "TOP-001-0";
        public string APPaymentDetailValue = "เติมเงิน | Airpay - KTB";

        //ShopeePay (SP)
        public string SPPaymentName = "Shopee Pay";
        public string SPSubPaymentCode = "TOP14917";
        public string SPMoneyCode = "SHP01-00";
                      
        public string SPPaymentMoneyValue = "ACK00-02";
        public string SPPaymentSubValue = "TOP14918";
        public string SPPaymentValue = "TOP-001-0";
        public string SPPaymentDetailValue = "เติมเงิน | Shopee Pay - 66870951151";
    }

    public class PaymentCodeConstant
    {
        public string TMB = "BNK-000-0";
        public string KBANK = "BNK-000-1";
        public string KTB = "BNK-000-3";
        public string CIMB = "BNK-000-2";
        public string BBL = "BNK-000-4";

    } 
}
