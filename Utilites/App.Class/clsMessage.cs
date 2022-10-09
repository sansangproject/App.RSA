using System;
using System.Linq;
using System.Windows.Forms;
using SANSANG.Constant;
using SANSANG.Utilites.App.Forms;

namespace SANSANG.Class
{
    public class clsMessage
    {
        private CultureConstant Cul = new CultureConstant();
        private PathConstant Path = new PathConstant();
        ImageConstant Photos = new ImageConstant();

        public string strMes { get; set; }
        public string strMessage { get; set; }

        public string strOperation { get; set; }
        public string strImage { get; set; }
        public string ReturnValue { get; set; }

        public string strMessageConfirmeTHA = "คุณต้องการ";
        public string strMessageConfirmeTHB = "หรือไม่";
        public string strMessageHeadTH = "ยืนยันการ";
        public string strResultHeadTH = "สำเร็จ";
        public string strResultHeadNoDataTH = "แจ้งเตือน";

        public string strUpdateDentalDetailTH = "ระบบไม่สามารถแก้ไขรายละเอียดการจ่ายเงินแต่ละรายการได้\nหากต้องการแก้ไขคุณจำเป็นต้องลบรายการนี้และเพิ่มเป็นรายการใหม่";
        public string strUpdateDentalDetailEN = "The system can not Edit the details of item in payments.\nIf you need to update this , you need to delete and add a new item .";

        public string strMessageConfirmeENA = "Do You Want to";
        public string strMessageDataDuplicateEN = " is already exists.";
        public string strMessageDataRequestEN = "Please fill in all required data.";
        public string strMessagePaymentEN = "Payment successful.";
        public string strMessageConfirmeENB = " ?\n";
        public string strMessageHeadEN = "Please Confirm to Proceed";
        public string strResultHeadEN = " Successfully";
        public string strResultHeadNoDataEN = "Information";
        public string strResultHeadWarningEN = "Warning";
        public string strResultHeadWarningTH = "Warning";
        public string strResultHeadErrorEN = "Error";
        public string strResultHeadErrorTH = "Error";
        public string strNext = "มีการยกยอดเงินจำนวน ";
        public string Language = "";
  
        public string strResultLastTH = "เรียบร้อย";
        public string strResultLastEN = " Successfully";

        public bool MessageConfirmation(string Case, string Id, string Detail)
        {
            Language = clsSetting.ReadLanguageSetting();

            if (Language == Cul.TH)
            {
                if (Case == "I")
                {
                    strOperation = "เพิ่มข้อมูล";
                    strImage = Photos.Insert;
                }

                if (Case == "U")
                {
                    strOperation = "แก้ไขข้อมูล";
                    strImage = Photos.Update;
                }

                if (Case == "D")
                {
                    strOperation = "ลบข้อมูล";
                    strImage = Photos.Delete;
                }

                if (Case == "F")
                {
                    strOperation = "ค้นหาข้อมูล";
                    strImage = Photos.Search;
                }

                if (Case == "S")
                {
                    strOperation = "บันทึกภาพสำเร็จ";
                    strImage = Photos.Save;
                }

                if (Case == "N")
                {
                    strOperation = "ข้อมูลซ้ำ";
                    strImage = Photos.Duplicate;
                }

                if (Case == "Q")
                {
                    strOperation = "กรอกข้อมูลไม่ครบถ้วน";
                    strImage = Photos.Require;
                }

                if (Case == "P")
                {
                    strOperation = "ชำระเงิน";
                    strImage = Photos.Pay;
                }

                strMes = "";

                if (Case != "F")
                {
                    strMes = strMessageConfirmeTHA
                           + strOperation + strMessageConfirmeTHB + Environment.NewLine
                           + "รหัสอ้างอิง: " + Id + Environment.NewLine
                           + "รายละเอียด: " + Detail;
                }
                else
                {
                    strMes = Detail;
                }

                strMes = Detail;
                return true;
            }
            else if (Language == Cul.EN)
            {
                if (Case == "I")
                {
                    strOperation = " Add the Data";
                    strImage = Photos.Insert;
                }

                if (Case == "U")
                {
                    strOperation = " Update the Data";
                    strImage = Photos.Update;
                }

                if (Case == "D")
                {
                    strOperation = " Delete the Data";
                    strImage = Photos.Delete;
                }

                if (Case == "F")
                {
                    strOperation = " Search the Data";
                    strImage = Photos.Search;
                }

                if (Case == "S")
                {
                    strOperation = " Save Image";
                    strImage = Photos.Save;
                }

                if (Case == "N")
                {
                    strOperation = " Data Duplicate";
                    strImage = Photos.Duplicate;
                }

                if (Case == "Q")
                {
                    strOperation = " Data Require";
                    strImage = Photos.Require;
                }

                if (Case == "V")
                {
                    strOperation = " Data Invalid";
                    strImage = Photos.Invalid;
                }

                if (Case == "P")
                {
                    strOperation = "Payment";
                    strImage = Photos.Pay;
                }

                strMes = "";

                if (Case == "N")
                {
                    strMes = Detail + strMessageDataDuplicateEN;
                }
                else if (Case == "Q")
                {
                    strMes = strMessageDataRequestEN;
                }
                else if (Case == "P")
                {
                    strMes = strMessagePaymentEN;
                }
                else if (new[] { "I", "U", "D"}.Contains(Case))
                {
                    strMes = strMessageConfirmeENA
                           + strOperation + strMessageConfirmeENB + Environment.NewLine
                           + "Code: " + Id + Environment.NewLine
                           + "Detail: " + Detail;
                }
                else
                {
                    strMes = Detail;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public void MessageResult(string Case, string Status, string Mess = "")
        {
            if (Language == Cul.TH)
            {
                if (Case == "I")
                {
                    strOperation = "เพิ่มข้อมูล";
                    strImage = Photos.Insert;
                }

                if (Case == "U")
                {
                    strOperation = "แก้ไขข้อมูล";
                    strImage = Photos.Update;
                }

                if (Case == "D")
                {
                    strOperation = "ลบข้อมูล";
                    strImage = Photos.Delete;
                }
                if (Case == "R")
                {
                    strOperation = "ออกรายงาน";
                    strImage = Photos.Report;
                }
                if (Case == "N")
                {
                    strOperation = "ไม่พบข้อมูล";
                    strImage = Photos.NotFound;
                }

                if (Case == "S")
                {
                    strOperation = "บันทึกรูปภาพ";
                    strImage = Photos.Save;
                }

                if (Case == "DU")
                {
                    strOperation = "ข้อมูลซ้ำ";
                    strImage = Photos.Duplicate;
                }

                if (Case == "IN")
                {
                    strOperation = "อินเตอร์เน็ต";
                    strImage = Photos.Internet;
                }

                if (Case == "IM")
                {
                    strOperation = "เพิ่มข้อมูล";
                    strImage = Photos.Insert;
                }


            }
            else if (Language == Cul.EN)
            {
                if (Case == "I")
                {
                    strOperation = " Added Data";
                    strImage = Photos.Insert;
                }

                if (Case == "IM")
                {
                    strOperation = "Import Data";
                    strImage = Photos.Import;
                }

                if (Case == "U")
                {
                    strOperation = " Updated Data";
                    strImage = Photos.Update;
                }

                if (Case == "D")
                {
                    strOperation = " Deleted Data";
                    strImage = Photos.Delete;
                }
                if (Case == "R")
                {
                    strOperation = "Exported Report";
                    strImage = Photos.Report;
                }
                if (Case == "N")
                {
                    strOperation = "Data Not Found";
                    strImage = Photos.NotFound;
                }
                if (Case == "S")
                {
                    strOperation = "Save Image";
                    strImage = Photos.Save;
                }
                if (Case == "DU")
                {
                    strOperation = "Data Duplicate";
                    strImage = Photos.Duplicate;
                }
                if (Case == "IN")
                {
                    strOperation = "Internet";
                    strImage = Photos.Internet;
                }
                if (Case == "NU")
                {
                    strOperation = "Incomplete Information";
                    strImage = Photos.Incomplete;
                }
            }

            if (Status == "C")
            {
                if (Language == Cul.TH)
                {
                    FrmMessages popup = new FrmMessages(strResultHeadTH, strOperation + strResultLastTH, strImage);
                    popup.Show();
                }
                else
                {
                    FrmMessages popup = new FrmMessages(strResultHeadEN, strOperation + strResultLastEN, strImage);
                    popup.Show();
                }
            }
            else if (Status == "SH")
            {
                if (Language == Cul.TH)
                {
                    MessageBox.Show(Mess == "" ? strOperation : Mess, strResultHeadNoDataTH, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(Mess == "" ? strOperation : Mess, strResultHeadNoDataEN, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if (Status == "IN")
            {
                if (Language == Cul.TH)
                {
                    MessageBox.Show(strUpdateDentalDetailTH, strOperation, MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    MessageBox.Show(strUpdateDentalDetailEN, strOperation, MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            else if (Status == "DU")
            {
                if (Language == Cul.TH)
                {
                    FrmMessages popup = new FrmMessages(strResultHeadWarningTH, strOperation + Environment.NewLine + strNext + Mess + " บาทแล้ว", strImage);
                    popup.Show();
                }
                else
                {
                    FrmMessages popup = new FrmMessages(strResultHeadWarningEN, strOperation + Environment.NewLine + strNext + Mess + " บาทแล้ว", strImage);
                    popup.Show();
                }
            }
            else if (Status == "ER")
            {
                if (Language == Cul.TH)
                {
                    FrmMessages popup = new FrmMessages(strResultHeadErrorTH, strOperation + Environment.NewLine + Mess, strImage);
                    popup.Show();
                }
                else
                {
                    FrmMessages popup = new FrmMessages(strResultHeadErrorEN, strOperation + Environment.NewLine + Mess, strImage);
                    popup.Show();
                }
            }
            else if (Status == "AD" || Status == "TR")
            {
                if (Language == Cul.TH)
                {
                    FrmMessages popup = new FrmMessages(strResultHeadNoDataTH, strOperation + Environment.NewLine + Mess, strImage);
                    popup.Show();
                }
                else
                {
                    FrmMessages popup = new FrmMessages(strResultHeadWarningEN, strOperation + Environment.NewLine + Mess, strImage);
                    popup.Show();
                }
            }
            else if (Status == "ADDMEADUP")
            {
                if (Language == Cul.TH)
                {
                    FrmMessages popup = new FrmMessages(strResultHeadWarningTH, strOperation + Environment.NewLine + Mess, strImage);
                    popup.Show();
                }
                else
                {
                    FrmMessages popup = new FrmMessages(strResultHeadWarningEN, strOperation + Environment.NewLine + Mess, strImage);
                    popup.Show();
                }
            }
            else
            {
                if (Language == Cul.TH)
                {
                    MessageBox.Show("RSA : " + strOperation + "ล้มเหลว\n\n" + Mess, "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("RSA : " + strOperation + " Error\n\n" + Mess, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void ShowMesInfo(string Mes)
        {
            MessageBox.Show("RSA - " + Mes, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowMesError(string function, string ex)
        {
            MessageBox.Show("RSA-Error " + function + "\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowPayment()
        {
            MessageConfirmation("P", "", "");
            var Popup = new FrmMessagesBoxOK(strOperation, strMes, "OK", "", strImage);
            Popup.ShowDialog();
        }

        public void ShowRequestData()
        {
            MessageConfirmation("Q", "", "");
            var Popup = new FrmMessagesBoxOK(strOperation, strMes, "OK", "", strImage);
            Popup.ShowDialog();
        }
    }
}