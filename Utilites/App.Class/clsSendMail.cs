using System;
using System.Net.Mail;

namespace SANSANG.Class
{
    public class clsSendMail
    {
        private clsFunction Fn = new clsFunction(); private clsMessage Mes = new clsMessage();

        public string strSmtpHotmail = "smtp.live.com";
        public string strMailFrom = "tuathor.st@hotmail.com";
        public string strMailPassword = "thor02DEC2532";
        public string strWelcomNewMember = "RSA APPLICATION : ยินดีต้อนรับสมาชิกใหม่‏";
        public string strBodyNewMember = "";

        public void newMember(string strMailTo, string strName, string strUser, string strPass)
        {
            try
            {
                getStrBodyNewMember(strName, strUser, strPass);
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(strSmtpHotmail);

                mail.From = new MailAddress(strMailFrom);
                mail.To.Add(strMailTo);
                mail.Subject = strWelcomNewMember;
                mail.Body = strBodyNewMember;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(strMailFrom, strMailPassword);
                SmtpServer.EnableSsl = true;

                //Attachment att = new Attachment(Fn.getPathLocation("I6-8E0E0F286C-1"));
                //att.ContentDisposition.Inline = true;

                mail.IsBodyHtml = true;
                //mail.Attachments.Add(att);

                SmtpServer.Send(mail);
            }
            catch (Exception)
            {
            }
        }

        public void getStrBodyNewMember(string strName, string strUser, string strPass)
        {
            strBodyNewMember = "Welcome to Records Systems Application (RSA)\n\n"

                        + "เรียน " + strName + "\n\n"

                        + "ยินดีต้อนรับท่านเข้าสู่การเป็นสมาชิก\n"
                        + "คุณได้สมัครเป็นสมาชิก RSA APPLICATION โดยสมบูรณ์" + "\n"
                        + "ซึ่งสามารถเข้าสู่ระบบเพื่อใช้งานระบบและตรวจสอบข้อมูลของท่านได้ โดยใช้ข้อมูลด้านล่างนี้" + "\n\n"

                        + "USER (ชื่อผู้ใช้งาน) : " + strUser + "\n"
                        + "PASSWORD (รหัสผ่าน) : " + strPass + "\n"
                        + "";

            getStrFootEmail();
        }

        public void getStrFootEmail()
        {
            strBodyNewMember += "\n\n\n\n\n"

                                + "Best Regards.\n"
                                + "________________________________________\n\n"

                                + "SANSANG STUDIO\n"
                                + "123/511 Nanthana Village Rattanathibat Rd.,\n"
                                + "BangRukNoi,Muang Nonthaburi,\n"
                                + "Nonthaburi 11000,Thailand\n\n"

                                + "Tel:     06 5628 2852\n"
                                + "Line:    tuathor.line\n"
                                + "Email:   tuathor.st@hotmail.com\n"
                                + "Web:     www.sansangstudio.com\n";
        }
    }
}