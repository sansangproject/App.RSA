using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmDENRE00 : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;
        public string programCode = "DENRE00";
        public string idTSS;
        public string strConditions;
        public string ReportName;
        public string strErr = "";
        public string ExportToPath = "";

        private string I1 = "", I2 = "", I3 = "", I4 = "", I5 = "";
        private string A1 = "", A2 = "", A3 = "", A4 = "", A5 = "";

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();

        public FrmDENRE00(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmDENRE00_Load(object sender, EventArgs e)
        {
            
            clear();
        }

        public void clear()
        {
            txtCode.Text = "";
            txtBookId.Text = "";
            txtBookNumber.Text = "";
            txtCode.Focus();

            List.GetMonthList(cbbMonth);
            List.GetYearList(cbbYear);

            rbtId.Checked = true;
            p1.Visible = true;
            p2.Visible = false;
            p3.Visible = false;
            strConditions = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnExpenPDF_Click(object sender, EventArgs e)
        {
            ExportToFile("PDF", sender, e);
        }

        private void btnExpenEXCEL_Click(object sender, EventArgs e)
        {
            ExportToFile("XLS", sender, e);
        }

        public void ExportToFile(string fileType, object sender, EventArgs e)
        {
            setCondition();

            idTSS = Fn.getTssFileName("DENRE");
            FrmAnimatedProgress frm = new FrmAnimatedProgress(10);

            try
            {
                string[,] Parameter = new string[,] { };

                if (rbtId.Checked | rbtNumber.Checked)
                {
                    Parameter = new string[,]
                    {
                        {"@RcptDentalCode", txtCode.Text},
                        {"@RcptDentalBookId", txtBookId.Text},
                        {"@RcptDentalBookNumber", txtBookNumber.Text},
                    };

                    db.Get("Spr_R_TblReceiptDental_Id", Parameter, out strErr, out dt);
                }
                else
                {
                    Parameter = new string[,]
                    {
                        {"@RcptDentalMonth", cbbMonth.SelectedIndex.ToString()},
                        {"@RcptDentalYear", Convert.ToString(Convert.ToInt64(cbbYear.Text) - 543)},
                    };

                    db.Get("Spr_R_TblReceiptDental_MonthYear", Parameter, out strErr, out dt);
                }

                if (strErr == null && dt.Rows.Count > 0)
                {
                    string Path = Fn.getPath("Report");
                    ReportName = "RSA-R-DENRE00001";

                    setItemAndPrice(dt);
                    DialogResult result = folderBrowserDialog.ShowDialog();
                    frm.Show();

                    if (result == DialogResult.OK)
                    {
                        ExportToPath = folderBrowserDialog.SelectedPath + "\\TSSRSA-DENRE00R" + Fn.getFileLastName();

                        if (fileType == "PDF")
                        {
                           
                            System.Diagnostics.Process.Start(@"" + ExportToPath + ".pdf");
                        }
                        else
                        {
                           
                            System.Diagnostics.Process.Start(@"" + ExportToPath + ".xls");
                        }

                        try
                        {
                            Insert.TblLogReport(idTSS, ReportName, strConditions, programCode, "DEN", "TSSRSA-DENRE00R" + Fn.getFileLastName() + "." + fileType, ExportToPath + "." + fileType, strUserName + " " + strUserSurname);
                        }
                        catch
                        {
                        }
                    }

                    strConditions = "";
                    clear();
                }
                else
                {
                    Message.MessageResult("N", "SH", strErr);
                    txtCode.Focus();
                }
            }
            catch (Exception)
            {
                Message.MessageResult("R", "E", strErr);
                strConditions = "";
                clear();
            }
        }

        private void cbbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbYear.SelectedValue = 2559;
        }

        private void rbtId_CheckedChanged(object sender, EventArgs e)
        {
            p1.Visible = true;
            p2.Visible = false;
            p3.Visible = false;
            txtCode.Focus();
            txtBookId.Text = "";
            txtBookNumber.Text = "";
        }

        private void rbtNumber_CheckedChanged(object sender, EventArgs e)
        {
            p1.Visible = false;
            p2.Visible = true;
            p3.Visible = false;
            txtCode.Text = "";
            txtBookId.Focus();
        }

        private void rbtMonth_CheckedChanged(object sender, EventArgs e)
        {
            p1.Visible = false;
            p2.Visible = false;
            p3.Visible = true;
        }

        public void setItemAndPrice(DataTable dt)
        {
            try
            {
                I1 = dt.Rows[0]["RcptDentalDetailName"].ToString();
                A1 = dt.Rows[0]["RcptDentalDetailPrice"].ToString();
            }
            catch (Exception)
            {
            }

            try
            {
                I2 = dt.Rows[1]["RcptDentalDetailName"].ToString();
                A2 = dt.Rows[1]["RcptDentalDetailPrice"].ToString();
            }
            catch (Exception)
            {
            }

            try
            {
                I3 = dt.Rows[2]["RcptDentalDetailName"].ToString();
                A3 = dt.Rows[2]["RcptDentalDetailPrice"].ToString();
            }
            catch (Exception)
            {
            }

            try
            {
                I4 = dt.Rows[3]["RcptDentalDetailName"].ToString();
                A4 = dt.Rows[3]["RcptDentalDetailPrice"].ToString();
            }
            catch (Exception)
            {
            }

            try
            {
                I5 = dt.Rows[4]["RcptDentalDetailName"].ToString();
                A5 = dt.Rows[4]["RcptDentalDetailPrice"].ToString();
            }
            catch (Exception)
            {
            }
        }

        public void setCondition()
        {
            if (txtCode.Text != "")
            {
                strConditions += " | รหัสอ้างอิง : " + txtCode.Text;
            }
            if (txtBookId.Text != "")
            {
                strConditions += " | เล่มที่ : " + txtBookId.Text;
            }
            if (txtBookNumber.Text != "")
            {
                strConditions += " | เลขที่ : " + txtBookNumber.Text;
            }
            if (Fn.getComboBoxValue(cbbMonth) != "0")
            {
                strConditions += " | เดือน : " + cbbMonth.Text;
            }
            if (Fn.getComboBoxValue(cbbYear) != "0")
            {
                strConditions += " | ปี : " + cbbYear.Text;
            }
        }
    }
}