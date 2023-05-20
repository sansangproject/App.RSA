using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SANSANG.Constant;
using SANSANG.Utilites.App.Global;
using SANSANG.Utilites.App.Model;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;

namespace SANSANG.Class
{
    public partial class clsFunction
    {
        string Error = "";
        dbConnection db = new dbConnection();
        AccountConstant Account = new AccountConstant();
        PaymentCodeConstant PaymentCode = new PaymentCodeConstant();
        CharacterConstant CharType = new CharacterConstant();
        ProgramConstant ProgramConstant = new ProgramConstant();
        StoreConstant Store = new StoreConstant();
        clsSetting Setting = new clsSetting();
        FormatConstant Fm = new FormatConstant();
        CultureConstant Cul = new CultureConstant();
        clsDesign Design = new clsDesign();
        clsMessage Message = new clsMessage();
        FieldConstant Field = new FieldConstant();

        IFormatProvider YearEn = new CultureInfo("en-US");
        IFormatProvider YearTh = new CultureInfo("th-TH");

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        private string strDay = "";
        private string strMonth = "";
        private string strYear = "";
        private string strDayMonthYear = "";
        private bool status = false;
        private string[,] Parameter = new string[,] { };

        public string GetPathLocation(string Code)
        {
            try
            {
                clsImage Images = new clsImage();
                StoreConstant Store = new StoreConstant();

                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", Code},
                    {"@Name", ""},
                    {"@Location", ""},
                    {"@Detail", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", ""},
                    {"@IsDelete", ""},
                    {"@Operation", "S"},
                };

                db.Get(Store.ManagePath, Parameter, out Error, out dt);
                return dt.Rows[0]["Location"].ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public int GetRows(DataTable dt)
        {
            try
            {
                return dt.Rows.Count;
            }
            catch
            {
                return 0;
            }
        }

        public string GetComboBoxId(ComboBox Combo, bool Isreturn = true)
        {
            string value;

            try
            {
                if (Isreturn)
                {
                    if (string.IsNullOrEmpty(Combo.Text) || Combo.Text == "System.Data.DataRowView" || Combo.Text == ":: กรุณาเลือก ::")
                    {
                        value = "";
                    }
                    else if (Combo.SelectedIndex != -1 && Combo.SelectedIndex >= 0)
                    {
                        if (Combo.SelectedValue.ToString() != "")
                        {
                            value = Combo.SelectedValue.ToString();
                        }
                        else
                        {
                            value = "";
                        }
                    }
                    else
                    {
                        value = "";
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(Combo.Text) || Combo.Text == "System.Data.DataRowView")
                    {
                        value = "";
                    }
                    else if (Combo.SelectedIndex != -1 && Combo.SelectedIndex >= 0)
                    {
                        if (Combo.SelectedValue.ToString() != "" && Combo.SelectedValue.ToString() != "0")
                        {
                            value = Combo.SelectedValue.ToString();
                        }
                        else
                        {
                            value = "";
                        }
                    }
                    else
                    {
                        value = "";
                    }
                }

                return value;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public bool GetPayment(string Id, out string Details, out string Items)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", Id},
                    {"@Code", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", ""},
                    {"@IsDelete", ""},
                    {"@Operation", "S"},
                    {"@Name", ""},
                    {"@NameEn", ""},
                    {"@Display", ""},
                    {"@Detail", ""},
                    {"@PaymentId", "0"},
                    {"@IsDebit", ""},
                };

                db.Get(Store.ManagePaymentSub, Parameter, out Error, out dt);
                Details = dt.Rows[0]["Detail"].ToString();
                Items = dt.Rows[0]["Item"].ToString();
                return Convert.ToBoolean(dt.Rows[0]["IsDebit"].ToString());
            }
            catch (Exception)
            {
                Details = "";
                Items = "";
                return false;
            }
        }

        public bool GetPayments(string Id, out string Details, out string Items)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", Id},
                    {"@Code", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", ""},
                    {"@IsDelete", ""},
                    {"@Operation", "S"},
                    {"@Name", ""},
                    {"@NameEn", ""},
                    {"@Display", ""},
                    {"@Detail", ""},
                    {"@PaymentId", "0"},
                    {"@IsDebit", ""},
                };

                db.Get(Store.ManagePaymentSub, Parameter, out Error, out dt);
                Details = dt.Rows[0]["Display"].ToString();
                Items = "รายการเดินบัญชี";
                return Convert.ToBoolean(dt.Rows[0]["IsDebit"].ToString());
            }
            catch (Exception)
            {
                Details = "";
                Items = "รายการเดินบัญชี";
                return false;
            }
        }

        public string GetPaymentItem(ComboBox Combo, string Column)
        {
            string Value = "";
            string Id = GetComboBoxId(Combo);

            try
            {
                Parameter = new string[,]
                {
                    {"@Id", Id},
                    {"@Code", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", ""},
                    {"@IsDelete", ""},
                    {"@Operation", "S"},
                    {"@Name", ""},
                    {"@NameEn", ""},
                    {"@Display", ""},
                    {"@Detail", ""},
                    {"@PaymentId", "0"},
                    {"@IsDebit", ""},
                };

                db.Get(Store.ManagePaymentSub, Parameter, out Error, out dt);
                return Error == null ? dt.Rows[0][Column].ToString() : "";
            }
            catch (Exception)
            {
                return Value;
            }
        }

        public void GetPayments(string Codes, out string PaymentId, out string Items, out string Details, out string Displays, out bool IsWithdrawal)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", ""},
                    {"@IsDelete", ""},
                    {"@Operation", "S"},
                    {"@Name", ""},
                    {"@NameEn", Codes},
                    {"@Display", ""},
                    {"@Detail", ""},
                    {"@PaymentId", "0"},
                    {"@IsDebit", ""},
                };

                db.Get(Store.ManagePaymentSub, Parameter, out Error, out dt);

                PaymentId = dt.Rows[0]["Id"].ToString();
                Items = "รายการเดินบัญชี";
                Details = dt.Rows[0]["Display"].ToString();
                Displays = dt.Rows[0]["Detail"].ToString();
                IsWithdrawal = Convert.ToBoolean(dt.Rows[0]["IsDebit"].ToString()) ? false : true;
            }
            catch (Exception)
            {
                PaymentId = "";
                Items = "";
                Details = "";
                Displays = "";
                IsWithdrawal = false;
            }
        }

        public void GetPaymentByName(string Codes, string Payments, out string PaymentId, out string Items, out string Details, out string Displays, out bool IsWithdrawal)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", ""},
                    {"@IsDelete", ""},
                    {"@Operation", "S"},
                    {"@Name", Codes},
                    {"@NameEn", ""},
                    {"@Display", ""},
                    {"@Detail", ""},
                    {"@PaymentId", Payments},
                    {"@IsDebit", ""},
                };

                db.Get(Store.ManagePaymentSub, Parameter, out Error, out dt);

                PaymentId = dt.Rows[0]["Id"].ToString();
                Items = dt.Rows[0]["NameEn"].ToString();
                Details = dt.Rows[0]["Detail"].ToString();
                Displays = dt.Rows[0]["NameEn"].ToString() + " | " + dt.Rows[0]["Name"].ToString();
                IsWithdrawal = Convert.ToBoolean(dt.Rows[0]["IsDebit"].ToString()) ? false : true;
            }
            catch (Exception)
            {
                PaymentId = "";
                Items = "";
                Details = "";
                Displays = "";
                IsWithdrawal = false;
            }
        }

        public string ConvertBoolToString(bool Bools)
        {
            try
            {
                if (Bools)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch
            {
                return "0";
            }
        }

        public string SumBalance(string date)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Date", date},
                };

                db.Get(Store.FnGetMoneyBalance, Parameter, out Error, out dt);

                return string.Format("{0:#,##0.00}", double.Parse(Convert.ToString(dt.Rows[0]["Sum"].ToString())));
            }
            catch (Exception)
            {
                return "0.00";
            }
        }

        public string GetComboZero(ComboBox cbb)
        {
            string value = "0";

            if (cbb.SelectedIndex > -1)
            {
                value = cbb.SelectedValue.ToString();
            }

            return value;
        }

        public string GetCodes(string Id, string Code, string Operation)
        {
            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@Id", Id},
                    {"@Code", Code},
                    {"@Operation", Operation},
                };

                db.Get(Store.FnGeneratedId, Parameter, out Error, out dt);
                return dt.Rows[0][0].ToString();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsDuplicate(string Table = "", string Value1 = "", string Value2 = "", string Value3 = "", string Value4 = "", string Value5 = "", string Value6 = "", string Value7 = "")
        {
            try
            {
                int NumberOfDuplicate = 0;

                Parameter = new string[,]
                {
                    {"@Table",      Table},
                    {"@Value1",     Value1},
                    {"@Value2",     Value2},
                    {"@Value3",     Value3},
                    {"@Value4",     Value4},
                    {"@Value5",     Value5},
                    {"@Value6",     Value6},
                    {"@Value7",     Value7},
                };

                db.Get(Store.FnGetDataDuplicate, Parameter, out Error, out dt);

                NumberOfDuplicate = string.IsNullOrEmpty(Error) ? Convert.ToInt32(dt.Rows[0]["ROW"].ToString()) : 1;
                return NumberOfDuplicate > 0 ? true : false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public bool IsDuplicates(string strTable = "", string strValue1 = "", string strValue2 = "", string strValue3 = "", string strValue4 = "", string strValue5 = "", string Detail = "")
        {
            try
            {
                int NumberOfDuplicate = 0;
                Parameter = new string[,]
                {
                    {"@Table",      strTable},
                    {"@Value1",     strValue1},
                    {"@Value2",     strValue2},
                    {"@Value3",     strValue3},
                    {"@Value4",     strValue4},
                    {"@Value5",     strValue5},
                    {"@Value6",     ""},
                    {"@Value7",     ""},
                };

                db.Get(Store.FnGetDataDuplicate, Parameter, out Error, out dt);
                NumberOfDuplicate = string.IsNullOrEmpty(Error) ? Convert.ToInt32(dt.Rows[0]["ROW"].ToString()) : 1;

                if (NumberOfDuplicate > 0)
                {
                    if (Detail != "")
                    {
                        Message.MessageConfirmation("N", "", Detail);
                        var Popup = new FrmMessagesBoxOK(Message.strOperation, Message.strMes, "OK", Id: Message.strImage);
                        Popup.ShowDialog();
                    }
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public int NumberDuplicate(string strTable = "", string strValue1 = "", string strValue2 = "", string strValue3 = "", string strValue4 = "", string Detail = "")
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Table",      strTable},
                    {"@Value1",     strValue1},
                    {"@Value2",     strValue2},
                    {"@Value3",     strValue3},
                    {"@Value4",     strValue4},
                    {"@Value5",     ""},
                    {"@Value6",     ""},
                    {"@Value7",     ""},
                };

                db.Get(Store.FnGetDataDuplicate, Parameter, out Error, out dt);
                return string.IsNullOrEmpty(Error) ? Convert.ToInt32(dt.Rows[0]["ROW"].ToString()) : 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public string ReplaceComma(string Amount)
        {
            try
            {
                string strNumber = "";
                double douNumber = 0;

                douNumber = Convert.ToDouble(Amount);
                strNumber = Convert.ToString(douNumber);

                return strNumber;
            }
            catch (Exception)
            {
                return Amount;
            }
        }

        public void ShowGridViewFormatFromStore(DataTable dt, DataGridView dataGridView,
             string Col0Name, int Col0Width, Boolean Col0Show, DataGridViewContentAlignment Col0HeadStyle, DataGridViewContentAlignment Col0DefaultStyle
           , string Col1Name, int Col1Width, Boolean Col1Show, DataGridViewContentAlignment Col1HeadStyle, DataGridViewContentAlignment Col1DefaultStyle
           , string Col2Name, int Col2Width, Boolean Col2Show, DataGridViewContentAlignment Col2HeadStyle, DataGridViewContentAlignment Col2DefaultStyle
           , string Col3Name, int Col3Width, Boolean Col3Show, DataGridViewContentAlignment Col3HeadStyle, DataGridViewContentAlignment Col3DefaultStyle
           , string Col4Name, int Col4Width, Boolean Col4Show, DataGridViewContentAlignment Col4HeadStyle, DataGridViewContentAlignment Col4DefaultStyle
           , string Col5Name, int Col5Width, Boolean Col5Show, DataGridViewContentAlignment Col5HeadStyle, DataGridViewContentAlignment Col5DefaultStyle
           , string Col6Name, int Col6Width, Boolean Col6Show, DataGridViewContentAlignment Col6HeadStyle, DataGridViewContentAlignment Col6DefaultStyle)
        {
            clsSearch clsSearch = new clsSearch();

            dataGridView.DataSource = dt;
            numberRow(dataGridView);

            dataGridView.Columns[0].Visible = Col0Show;
            dataGridView.Columns[0].HeaderText = Col0Name;
            dataGridView.Columns[0].Width = Col0Width;
            dataGridView.Columns[0].HeaderCell.Style.Alignment = Col0HeadStyle;
            dataGridView.Columns[0].DefaultCellStyle.Alignment = Col0DefaultStyle;

            dataGridView.Columns[1].Visible = Col1Show;
            dataGridView.Columns[1].HeaderText = Col1Name;
            dataGridView.Columns[1].Width = Col1Width;
            dataGridView.Columns[1].HeaderCell.Style.Alignment = Col1HeadStyle;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = Col1DefaultStyle;

            dataGridView.Columns[2].Visible = Col2Show;
            dataGridView.Columns[2].HeaderText = Col2Name;
            dataGridView.Columns[2].Width = Col2Width;
            dataGridView.Columns[2].HeaderCell.Style.Alignment = Col2HeadStyle;
            dataGridView.Columns[2].DefaultCellStyle.Alignment = Col2DefaultStyle;

            dataGridView.Columns[3].Visible = Col3Show;
            dataGridView.Columns[3].HeaderText = Col3Name;
            dataGridView.Columns[3].Width = Col3Width;
            dataGridView.Columns[3].HeaderCell.Style.Alignment = Col3HeadStyle;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = Col3DefaultStyle;

            dataGridView.Columns[4].Visible = Col4Show;
            dataGridView.Columns[4].HeaderText = Col4Name;
            dataGridView.Columns[4].Width = Col4Width;
            dataGridView.Columns[4].HeaderCell.Style.Alignment = Col4HeadStyle;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = Col4DefaultStyle;

            dataGridView.Columns[5].Visible = Col5Show;
            dataGridView.Columns[5].HeaderText = Col5Name;
            dataGridView.Columns[5].Width = Col5Width;
            dataGridView.Columns[5].HeaderCell.Style.Alignment = Col5HeadStyle;
            dataGridView.Columns[5].DefaultCellStyle.Alignment = Col5DefaultStyle;

            dataGridView.Columns[6].Visible = Col6Show;
            dataGridView.Columns[6].HeaderText = Col6Name;
            dataGridView.Columns[6].Width = Col6Width;
            dataGridView.Columns[6].HeaderCell.Style.Alignment = Col6HeadStyle;
            dataGridView.Columns[6].DefaultCellStyle.Alignment = Col6DefaultStyle;
        }

        public void ShowGridViewFormatFromStore(DataTable dt, DataGridView dataGridView,
            string Col0Name, int Col0Width, Boolean Col0Show, DataGridViewContentAlignment Col0HeadStyle, DataGridViewContentAlignment Col0DefaultStyle
          , string Col1Name, int Col1Width, Boolean Col1Show, DataGridViewContentAlignment Col1HeadStyle, DataGridViewContentAlignment Col1DefaultStyle
          , string Col2Name, int Col2Width, Boolean Col2Show, DataGridViewContentAlignment Col2HeadStyle, DataGridViewContentAlignment Col2DefaultStyle
          , string Col3Name, int Col3Width, Boolean Col3Show, DataGridViewContentAlignment Col3HeadStyle, DataGridViewContentAlignment Col3DefaultStyle
          , string Col4Name, int Col4Width, Boolean Col4Show, DataGridViewContentAlignment Col4HeadStyle, DataGridViewContentAlignment Col4DefaultStyle
          , string Col5Name, int Col5Width, Boolean Col5Show, DataGridViewContentAlignment Col5HeadStyle, DataGridViewContentAlignment Col5DefaultStyle
          , string Col6Name, int Col6Width, Boolean Col6Show, DataGridViewContentAlignment Col6HeadStyle, DataGridViewContentAlignment Col6DefaultStyle
          , string Col7Name, int Col7Width, Boolean Col7Show, DataGridViewContentAlignment Col7HeadStyle, DataGridViewContentAlignment Col7DefaultStyle
          , string Col8Name, int Col8Width, Boolean Col8Show, DataGridViewContentAlignment Col8HeadStyle, DataGridViewContentAlignment Col8DefaultStyle
          )
        {
            dataGridView.DataSource = dt;
            numberRow(dataGridView);

            dataGridView.Columns[0].Visible = Col0Show;
            dataGridView.Columns[0].HeaderText = Col0Name;
            dataGridView.Columns[0].Width = Col0Width;
            dataGridView.Columns[0].HeaderCell.Style.Alignment = Col0HeadStyle;
            dataGridView.Columns[0].DefaultCellStyle.Alignment = Col0DefaultStyle;

            dataGridView.Columns[1].Visible = Col1Show;
            dataGridView.Columns[1].HeaderText = Col1Name;
            dataGridView.Columns[1].Width = Col1Width;
            dataGridView.Columns[1].HeaderCell.Style.Alignment = Col1HeadStyle;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = Col1DefaultStyle;

            dataGridView.Columns[2].Visible = Col2Show;
            dataGridView.Columns[2].HeaderText = Col2Name;
            dataGridView.Columns[2].Width = Col2Width;
            dataGridView.Columns[2].HeaderCell.Style.Alignment = Col2HeadStyle;
            dataGridView.Columns[2].DefaultCellStyle.Alignment = Col2DefaultStyle;

            dataGridView.Columns[3].Visible = Col3Show;
            dataGridView.Columns[3].HeaderText = Col3Name;
            dataGridView.Columns[3].Width = Col3Width;
            dataGridView.Columns[3].HeaderCell.Style.Alignment = Col3HeadStyle;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = Col3DefaultStyle;

            dataGridView.Columns[4].Visible = Col4Show;
            dataGridView.Columns[4].HeaderText = Col4Name;
            dataGridView.Columns[4].Width = Col4Width;
            dataGridView.Columns[4].HeaderCell.Style.Alignment = Col4HeadStyle;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = Col4DefaultStyle;

            dataGridView.Columns[5].Visible = Col5Show;
            dataGridView.Columns[5].HeaderText = Col5Name;
            dataGridView.Columns[5].Width = Col5Width;
            dataGridView.Columns[5].HeaderCell.Style.Alignment = Col5HeadStyle;
            dataGridView.Columns[5].DefaultCellStyle.Alignment = Col5DefaultStyle;

            dataGridView.Columns[6].Visible = Col6Show;
            dataGridView.Columns[6].HeaderText = Col6Name;
            dataGridView.Columns[6].Width = Col6Width;
            dataGridView.Columns[6].HeaderCell.Style.Alignment = Col6HeadStyle;
            dataGridView.Columns[6].DefaultCellStyle.Alignment = Col6DefaultStyle;

            dataGridView.Columns[7].Visible = Col7Show;
            dataGridView.Columns[7].HeaderText = Col7Name;
            dataGridView.Columns[7].Width = Col7Width;
            dataGridView.Columns[7].HeaderCell.Style.Alignment = Col7HeadStyle;
            dataGridView.Columns[7].DefaultCellStyle.Alignment = Col7DefaultStyle;

            dataGridView.Columns[8].Visible = Col8Show;
            dataGridView.Columns[8].HeaderText = Col8Name;
            dataGridView.Columns[8].Width = Col8Width;
            dataGridView.Columns[8].HeaderCell.Style.Alignment = Col8HeadStyle;
            dataGridView.Columns[8].DefaultCellStyle.Alignment = Col8DefaultStyle;
        }

        public string GetComboId(ComboBox cbb)
        {
            string value = "";

            if (cbb.SelectedIndex > -1)
            {
                value = cbb.SelectedValue.ToString();
            }

            if (value == "System.Data.DataRowView")
            {
                value = "";
            }

            return value;
        }

        public string GetPath(string folderName)
        {
            return AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10) + folderName + "\\";
        }

        public string DeleteData(string strId, string strCode, string strTable, string strType = "1", string strUser = "System.Admin")
        {
            try
            {
                Parameter = new string[,]
                {
                     {"@Id", strId},
                     {"@Code", strCode},
                     {"@Table", strTable},
                     {"@Type", strType},
                     {"@User", strUser},
                };

                db.Get(Store.DeleteData, Parameter, out Error, out dt);

                return Error;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string KeyPress(object sender, KeyEventArgs e)
        {
            string codeString = "";

            if (e.Control == true)
            {
                codeString = "Ctrl";
            }
            else if (e.Shift == true)
            {
                codeString = "Shift";
            }
            else if (e.Alt == true)
            {
                codeString = "Alt";
            }
            else
            {
            }

            if (e.KeyCode == Keys.S)
            {
                codeString += "+S";
            }
            else if (e.KeyCode == Keys.E)
            {
                codeString += "+E";
            }
            else if (e.KeyCode == Keys.D)
            {
                codeString += "+D";
            }
            else if (e.KeyCode == Keys.F)
            {
                codeString += "+F";
            }
            else if (e.KeyCode == Keys.C)
            {
                codeString += "+C";
            }
            else if (e.KeyCode == Keys.N)
            {
                codeString += "+N";
            }
            else if (e.KeyCode == Keys.P)
            {
                codeString += "+P";
            }
            else if (e.KeyCode == Keys.Enter)
            {
                codeString += "Enter";
            }
            else
            {
            }

            return codeString;
        }

        public string ShowConditons(string Condition)
        {
            try
            {
                string NewCondition = "";

                if (!string.IsNullOrEmpty(Condition))
                {
                    string Conditions = Condition.Substring(2, Condition.Length - 2);
                    string[] ConditionList = Conditions.Split(new[] { ", " }, StringSplitOptions.None);

                    if (ConditionList.Length > 5)
                    {
                        for (int i = 0; i <= 5; i++)
                        {
                            NewCondition += ConditionList[i];

                            if (i != 5)
                            {
                                NewCondition += " | ";
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= ConditionList.Length - 1; i++)
                        {
                            NewCondition += ConditionList[i];

                            if (i != ConditionList.Length - 1)
                            {
                                NewCondition += " | ";
                            }
                        }
                    }

                    return NewCondition;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        public string GetImageAsBase64Url(string urlSignature)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData(urlSignature);
                    string base64String = Convert.ToBase64String(data);
                    return base64String;
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public CultureInfo SetFormatDate(string Format)
        {
            if (Format.ToUpper() == Cul.TH.ToUpper())
            {
                return CultureInfo.CreateSpecificCulture(Cul.Thai);
            }
            else if (Format == Cul.EN)
            {
                return CultureInfo.CreateSpecificCulture(Cul.English);
            }
            else
            {
                return CultureInfo.CreateSpecificCulture(Cul.Thai);
            }
        }

        public void ClearAll(GroupBox GroupBoxs)
        {
            foreach (Control Controls in GroupBoxs.Controls)
            {
                TextBox TextBoxs = Controls as TextBox;

                if (TextBoxs != null)
                {
                    TextBoxs.Text = "";
                }

                ComboBox ComboBoxs = Controls as ComboBox;

                if (ComboBoxs != null)
                {
                    ComboBoxs.SelectedValue = 0;
                }

                RadioButton RadioButtons = Controls as RadioButton;

                if (RadioButtons != null)
                {
                    RadioButtons.Checked = true;
                }

                CheckBox CheckBoxs = Controls as CheckBox;

                if (CheckBoxs != null)
                {
                    CheckBoxs.Checked = false;
                }

            }
        }

        public void ClearText(GroupBox GroupBoxs)
        {
            foreach (Control Controls in GroupBoxs.Controls)
            {
                TextBox TextBoxs = Controls as TextBox;

                if (TextBoxs != null)
                {
                    TextBoxs.Text = "";
                }
            }
        }

        public void ClearSelect(GroupBox GroupBoxs)
        {
            foreach (Control Controls in GroupBoxs.Controls)
            {
                ComboBox ComboBoxs = Controls as ComboBox;

                if (ComboBoxs != null)
                {
                    ComboBoxs.SelectedValue = 0;
                }
            }
        }

        public string FormatNumber(Double value)
        {
            string number = "";

            try
            {
                number = string.Format("{0:#,##0.00}", value);
                return number;
            }
            catch (Exception)
            {
                return "0.00";
            }
        }

        public string RemoveComma(string Value)
        {
            try
            {
                string Values = Value == "" ? "0" : Value;
                string Numbers = "";
                double Number = 0;

                Number = Convert.ToDouble(Values);
                Numbers = string.Format("{0:###0.00}", Number);

                return Numbers;
            }
            catch (Exception)
            {
                return Value;
            }
        }

        public string ShowNumberOfData(int Rows)
        {
            try
            {
                if (Rows > 0)
                {
                    return "ข้อมูลทั้งหมด " + string.Format("{0:#,##0}", Rows) + " รายการ";
                }
                else
                {
                    return "ข้อมูลทั้งหมด 0 รายการ";
                }
            }
            catch (Exception)
            {
                return "ข้อมูลทั้งหมด 0 รายการ";
            }
        }

        public DataTable GetBankBalance(string Account)
        {
            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@Account", Account},
                };

                db.Get(Store.FnGetBankBalance, Parameter, out Error, out dt);
                return dt;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SetDate(DateTimePicker Datepicker, TextBox Textbox, string Value)
        {
            if (!string.IsNullOrEmpty(Value))
            {
                Datepicker.Value = DateTime.Today;
                Datepicker.Value = DateTime.ParseExact(Value, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
            else
            {
                Datepicker.Value = DateTime.Today;
                Textbox.Text = "";
            }
        }

        public string FillFromRight(string Number, int MaxLength)
        {
            string Numbers = Number;
            char RepeateThis = '0';

            try
            {
                var format = "{0:-" + (MaxLength) + ":" + RepeateThis + "}";
                Numbers = string.Format(new PaddedStringFormatInfo(), format, Number);
            }
            catch
            {
                return "";
            }

            return Numbers;
        }

        public sealed class PaddedStringFormatInfo : IFormatProvider, ICustomFormatter
        {
            public object GetFormat(Type formatType)
            {
                if (typeof(ICustomFormatter).Equals(formatType)) return this;
                return null;
            }

            public string Format(string format, object arg, IFormatProvider formatProvider)
            {
                if (arg == null)
                    throw new ArgumentNullException("Argument cannot be null");

                string[] args;
                if (format != null)
                    args = format.Split(':');
                else
                    return arg.ToString();

                if (args.Length == 1)
                    String.Format("{0, " + format + "}", arg);

                int padLength = 0;

                if (!int.TryParse(args[0], out padLength))
                    throw new ArgumentException("Padding lenght should be an integer");
                switch (args.Length)
                {
                    case 2:
                        if (padLength > 0)
                            return (arg as string).PadLeft(padLength, args[1][0]);
                        return (arg as string).PadRight(padLength * -1, args[1][0]);
                    default:
                        return string.Format("{0," + format + "}", arg);
                }
            }
        }



















































        public void getMenu(string programCode, TextBox lbTh, TextBox lbEn)
        {
            try
            {
                string strErr = "";

                string[,] Parameter = new string[,]
                {
                    {"@MsMenuCode", programCode},
                    {"@MsMenuForm", ""},
                    {"@MsMenuStatus", "Y"},
                    {"@MsMenuId", ""},
                    {"@MsMenuNameEn", ""},
                    {"@MsMenuNameTh", ""},
                    {"@MsMenuDisplay", ""},
                    {"@MsMenuType", ""},
                    {"@MsMenuMain", ""},
                    {"@MsMenuSub", ""},
                };

                db.Get("Spr_S_TblMasterMenu", Parameter, out strErr, out dt);
                if (dt.Rows.Count == 1)
                {
                    lbEn.Text = dt.Rows[0]["MsMenuNameEn"].ToString().ToUpper();
                    lbTh.Text = "" + dt.Rows[0]["MsMenuNameTh"].ToString();
                    Design.ProgramName(lbTh, lbEn);
                }
            }
            catch (Exception)
            {
            }
        }

        public string getImagePath(string Path, string imageName, string imageType)
        {
            clsImage Images = new clsImage();
            StoreConstant Store = new StoreConstant();
            DataTable dt = new DataTable();
            string strErr = "";
            string path = "";

            try
            {
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", ""},
                    {"@Referrence", ""},
                    {"@Name", ""},
                    {"@Type", ""},
                    {"@Size", ""},
                    {"@SizeName", ""},
                    {"@Location", ""},
                    {"@Path", Path},
                    {"@Byte", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                };

                db.Get("Store.SelectImage", Parameter, out strErr, out dt);

                path = dt.Rows[0]["ImageLocation"].ToString() + imageName + imageType;
                return path;
            }
            catch (Exception)
            {
                path = getImagePathOriginal(imageName, imageType);
                return path;
            }
        }

        public string getCodePath(string CardCode = null, string ProductCode = null)
        {
            DataTable dt = new DataTable();
            string strErr = "";
            string codePath = "";

            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@MsPathName", "getCardCode"},
                    {"@MsPathStatus", ""},
                    {"@MsPathCode", CardCode != null? CardCode : ProductCode},
                    {"@MsPathLocation", ""},
                    {"@MsPathDetail", ""},
                };

                db.Get("Spr_S_TblMasterPath", Parameter, out strErr, out dt);

                codePath = dt.Rows[0]["MsPathCode"].ToString();
                return codePath;
            }
            catch (Exception)
            {
                return codePath;
            }
        }

        public string getImagePathOriginal(string ImgName, string ImgType)
        {
            return AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10) + "Picture\\" + ImgName + ImgType;
        }

        public string getComboBoxValue(ComboBox cbb, bool returns = true)
        {
            string value;

            try
            {
                if (returns)
                {
                    if (cbb.Text == "")
                    {
                        value = "0";
                    }
                    else if (cbb.SelectedIndex != -1 && cbb.SelectedIndex >= 0)
                    {
                        if (cbb.SelectedValue.ToString() != "")
                        {
                            value = cbb.SelectedValue.ToString();
                        }
                        else
                        {
                            value = "0";
                        }
                    }
                    else
                    {
                        value = "0";
                    }
                }
                else
                {
                    if (cbb.Text == "")
                    {
                        value = "";
                    }
                    else if (cbb.SelectedIndex != -1 && cbb.SelectedIndex >= 0)
                    {
                        if (cbb.SelectedValue.ToString() != "" && cbb.SelectedValue.ToString() != "0")
                        {
                            value = cbb.SelectedValue.ToString();
                        }
                        else
                        {
                            value = "";
                        }
                    }
                    else
                    {
                        value = "";
                    }
                }

                return value;
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public string getId(string table, string column, string IDName)
        {
            DataTable dt = new DataTable();
            string strErr = "";


            try
            {
                string id = "";
                int sum = 0;

                string[,] Parameter = new string[,]
                   {
                    {"@Table", table},
                    {"@Column", column},
                   };

                db.Get("Spr_F_GetTopId", Parameter, out strErr, out dt);

                // XXX123456789-0
                // "XXX" + "123456" + "7" + "89" - "0"
                // ____________________________________________________________________________________________
                //
                // "XXX"    = 3 ตัว รับจากหน้าจอตั้งค่าให้สอดคล้องกับโปรแกรม
                // "123456" = 6 ตัว ปี 2 หลัก เดือน 2 หลัก วันที่ 2 หลัก
                // "7"      = 1 ตัว ผลรวมวันที่ 6 หลัก MOD 9 รับค่า 1 หลัก
                // "89"     = 2 ตัว ค่า Id ล่าสุด ใน Table นั้นๆ MOD 9 รับค่า 2 หลัก
                // "0"      = 1 ตัว สำหรับเช็คการป้อนตัวเลข 1-9 หลัก โดยนำค่าตัวเลขคูณค่าหลักจากมากไปน้อยซึ่งเริ่มจาก 9
                //              เช่น (1*9) + (2*8) + (3*7) + (4*6) + (5*5) + (6*4) + (7*3) + (8*2) + (9*1) MOD 9
                // _____________________________________________________________________________________________

                string strCode = IDName;
                string strTime = Convert.ToString(DateTime.Now.ToString("HHmmss"));
                string strDay = Convert.ToString(DateTime.Now.ToString("yyMMdd"));

                int numSumDate = 0;
                int row = Convert.ToInt32(dt.Rows[0][column].ToString());

                for (int i = 0; i <= 5; i++)
                {
                    numSumDate += Convert.ToInt32(strDay.Substring(i, 1));
                }

                string strDate = String.Format("{0}", (numSumDate % 9).ToString());
                string strRow = (row % 99).ToString().PadLeft(2, '0');

                string strSum = strDay + strDate + strRow;

                for (int i = 0; i <= 8; i++)
                {
                    int result = (Convert.ToInt32(strSum.Substring(i, 1)) * (9 - i));
                    sum += result;
                }

                string strCheck = String.Format("{0}", (sum % 9).ToString());
                id = strCode + strTime + strDate + strRow + "-" + strCheck;

                return id;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string getId(string table, string column, string IDName, DateTime Date)
        {
            DataTable dt = new DataTable();
            string strErr = "";


            try
            {
                string id = "";
                int sum = 0;

                string[,] Parameter = new string[,]
                {
                    {"@Table", table},
                    {"@Column", column},
                };

                db.Get("Spr_F_GetTopId", Parameter, out strErr, out dt);

                // XXX123456789-0
                // "XXX" + "123456" + "7" + "89" - "0"
                // ____________________________________________________________________________________________
                //
                // "XXX"    = 3 ตัว รับจากหน้าจอตั้งค่าให้สอดคล้องกับโปรแกรม
                // "123456" = 6 ตัว ปี 2 หลัก เดือน 2 หลัก วันที่ 2 หลัก
                // "7"      = 1 ตัว ผลรวมวันที่ 6 หลัก MOD 9 รับค่า 1 หลัก
                // "89"     = 2 ตัว ค่า Id ล่าสุด ใน Table นั้นๆ MOD 9 รับค่า 2 หลัก
                // "0"      = 1 ตัว สำหรับเช็คการป้อนตัวเลข 1-9 หลัก โดยนำค่าตัวเลขคูณค่าหลักจากมากไปน้อยซึ่งเริ่มจาก 9
                //              เช่น (1*9) + (2*8) + (3*7) + (4*6) + (5*5) + (6*4) + (7*3) + (8*2) + (9*1) MOD 9
                // _____________________________________________________________________________________________

                string strCode = IDName;
                string strTime = Convert.ToString(Date.ToString("HHmmss"));
                string strDay = Convert.ToString(Date.ToString("yyMMdd"));

                int numSumDate = 0;
                int row = Convert.ToInt32(dt.Rows[0][column].ToString());

                for (int i = 0; i <= 5; i++)
                {
                    numSumDate += Convert.ToInt32(strDay.Substring(i, 1));
                }

                string strDate = String.Format("{0}", (numSumDate % 9).ToString());
                string strRow = (row % 99).ToString().PadLeft(2, '0');

                string strSum = strDay + strDate + strRow;

                for (int i = 0; i <= 8; i++)
                {
                    int result = (Convert.ToInt32(strSum.Substring(i, 1)) * (9 - i));
                    sum += result;
                }

                string strCheck = String.Format("{0}", (sum % 9).ToString());
                id = strCode + strTime + strDate + strRow + "-" + strCheck;

                return id;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string getCode(string table, string column, string IDName)
        {
            DataTable dt = new DataTable();
            string strErr = "";


            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@Table", table},
                    {"@Column", column},
                };

                db.Get("Spr_F_GetTopId", Parameter, out strErr, out dt);

                // A99-8BCB87B2C9-9
                // "A" + "99" + "-" + "8BCB87B2C9" + "-" +  "9"
                // ____________________________________________________________________________________________
                //
                // "A"                  = 1 ตัว รหัสโปรแกรมตัวแรก
                // "9"                  = 1 ตัว ค่า Id ล่าสุด ใน Table นั้นๆ MOD 9
                // "8BCB87B2C9"         = 10 ตัว วันที่และเวลา 12 หลัก yyMMddHHmmss แปลงเป็นฐาน 16
                // "9"                  = 1 ตัว สำหรับเช็คการป้อนตัวเลข 1-9 หลัก โดยนำวันที่แต่ละหลักมาบวกกัน แล้วนำผลรวมมา MOD 9
                //                          เช่น 60/04/15 12:21:21 (600415122121) = (6+0+0+4+1+5+1+2+2+1+2+1) MOD 9
                // _____________________________________________________________________________________________

                string strCode = IDName;

                int row = Convert.ToInt32(dt.Rows[0][column].ToString());
                string strRow = (row % 9).ToString().PadLeft(1, '0');

                string strDateTime = Convert.ToString(DateTime.Now.ToString("yyMMddHHmmss"));
                string hexDateTime = (long.Parse(strDateTime)).ToString("X10");

                int numSumDateTime = 0;

                for (int i = 0; i <= 11; i++)
                {
                    numSumDateTime += Convert.ToInt32(strDateTime.Substring(i, 1));
                }

                string strChekDigit = String.Format("{0}", (numSumDateTime % 9).ToString());

                return strCode + strRow + "-" + hexDateTime + "-" + strChekDigit;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string getCode(string table, string column, string IDName, DateTime Date)
        {
            DataTable dt = new DataTable();
            string strErr = "";


            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@Table", table},
                    {"@Column", column},
                };

                db.Get("Spr_F_GetTopId", Parameter, out strErr, out dt);

                // A9-8BCB87B2C9-9
                // "A" + "9" + "-" + "8BCB87B2C9" + "-" +  "9"
                // ____________________________________________________________________________________________
                //
                // "A"                  = 1 ตัว รหัสโปรแกรมตัวแรก
                // "9"                  = 1 ตัว ค่า Id ล่าสุด ใน Table นั้นๆ MOD 9
                // "8BCB87B2C9"         = 10 ตัว วันที่และเวลา 12 หลัก yyMMddHHmmss แปลงเป็นฐาน 16
                // "9"                  = 1 ตัว สำหรับเช็คการป้อนตัวเลข 1-9 หลัก โดยนำวันที่แต่ละหลักมาบวกกัน แล้วนำผลรวมมา MOD 9
                //                          เช่น 60/04/15 12:21:21 (600415122121) = (6+0+0+4+1+5+1+2+2+1+2+1) MOD 9
                // _____________________________________________________________________________________________

                string strCode = IDName;
                int rowNumber = 0;

                try
                {
                    rowNumber = Convert.ToInt32(dt.Rows[0][column].ToString());
                }
                catch (Exception)
                {
                    rowNumber = 0;
                }

                int row = rowNumber != 0 ? rowNumber : 1;

                string strRow = (row % 9).ToString().PadLeft(1, '0');

                string strDateTime = Convert.ToString(DateTime.Now.ToString("yyMMddHHmmss"));
                string hexDateTime = (long.Parse(strDateTime)).ToString("X10");

                int numSumDateTime = 0;

                for (int i = 0; i <= 11; i++)
                {
                    numSumDateTime += Convert.ToInt32(strDateTime.Substring(i, 1));
                }

                string strChekDigit = String.Format("{0}", (numSumDateTime % 9).ToString());

                return strCode + strRow + "-" + hexDateTime + "-" + strChekDigit;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string getPaySubCode(string strPayment, string strDetail)
        {
            DataTable dt = new DataTable();
            string strErr = "";


            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@MsPaymentCode", strPayment},
                    {"@MsPaymentSubDetail", strDetail},
                };

                db.Get("Spr_F_GetPaySubCode", Parameter, out strErr, out dt);
                return dt.Rows[0]["Code"].ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string getCode(Int32 strRound, string IDName, DateTime Date)
        {
            try
            {
                // A9-8BCB87B2C9-9
                // "A" + "9" + "-" + "8BCB87B2C9" + "-" +  "9"
                // ____________________________________________________________________________________________
                //
                // "A"                  = 1 ตัว รหัสโปรแกรมตัวแรก
                // "9"                  = 1 ตัว ค่า Id ล่าสุด ใน Table นั้นๆ MOD 9
                // "8BCB87B2C9"         = 10 ตัว วันที่และเวลา 12 หลัก yyMMddHHmmss แปลงเป็นฐาน 16
                // "9"                  = 1 ตัว สำหรับเช็คการป้อนตัวเลข 1-9 หลัก โดยนำวันที่แต่ละหลักมาบวกกัน แล้วนำผลรวมมา MOD 9
                //                          เช่น 60/04/15 12:21:21 (600415122121) = (6+0+0+4+1+5+1+2+2+1+2+1) MOD 9
                // _____________________________________________________________________________________________

                string strCode = IDName;
                int row = strRound;
                string strRow = (row % 9).ToString().PadLeft(1, '0');

                string strDateTime = Convert.ToString(DateTime.Now.ToString("yyMMddHHmmss"));
                string hexDateTime = (long.Parse(strDateTime)).ToString("X10");

                int numSumDateTime = 0;

                for (int i = 0; i <= 11; i++)
                {
                    numSumDateTime += Convert.ToInt32(strDateTime.Substring(i, 1));
                }

                string strChekDigit = String.Format("{0}", (numSumDateTime % 9).ToString());

                return strCode + strRow + "-" + hexDateTime + "-" + strChekDigit;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public void setCode(TextBox txtCode, TextBox txtProgramICode, TextBox txtDate, TextBox txtTime, TextBox txtCheck, PictureBox pb)
        {
            try
            {
                clsImage TSSImage = new clsImage();
                clsMessage Mes = new clsMessage();
                String hexValue = txtCode.Text.Substring(3, 10);
                Int64 decemalValue = Convert.ToInt64(hexValue, 16);

                String strValue = Convert.ToString(decemalValue);
                String strYear = Convert.ToString(Convert.ToInt32(strValue.Substring(0, 2)) < 50 ?
                                          Convert.ToInt32("20" + strValue.Substring(0, 2)) + 543 : Convert.ToInt32("25" + strValue.Substring(0, 2)));
                String strMonth = strValue.Substring(2, 2);
                String strDay = strValue.Substring(4, 2);

                var dateValue = Convert.ToDateTime(strYear + "/" + strMonth + "/" + strDay);
                int numSumDateTime = 0;

                for (int i = 0; i <= 11; i++)
                {
                    numSumDateTime += Convert.ToInt32(strValue.Substring(i, 1));
                }
                string strChekDigit = String.Format("{0}", (numSumDateTime % 9).ToString());

                if (strChekDigit == txtCode.Text.Substring(14, 1))
                {
                    txtProgramICode.Text = txtCode.Text.Substring(0, 1);
                    txtCheck.Text = txtCode.Text.Substring(14, 1);
                    txtTime.Text = strValue.Substring(6, 2) + ":" + strValue.Substring(8, 2) + ":" + strValue.Substring(10, 2);
                    txtDate.Text = dateValue.ToString("dd MMMM yyyy");

                    //TSSImage.Show("I5-8BDE0218CD-4", pb, "P1-PROGRAM000-1");
                }
                else
                {
                    //TSSImage.Show("I3-8BDE0218E0-5", pb, "P1-PROGRAM000-1");
                    txtProgramICode.Text = "";
                    txtCheck.Text = "";
                    txtTime.Text = "";
                    txtDate.Text = "";
                    Message.ShowMesError("\nรหัส " + txtCode.Text + " ไม่ถูกต้อง\n", "");
                    txtCode.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        public string getIdRunning(string table, string column, string IDName)
        {
            DataTable dt = new DataTable();
            string strErr = "";


            try
            {
                string id = "";
                string row = "";

                string[,] Parameter = new string[,]
                {
                    {"@Table", table},
                    {"@Column", column},
                };

                db.Get("Spr_F_GetTopId", Parameter, out strErr, out dt);

                row = String.Format("{0:0000}", Convert.ToInt32(dt.Rows[0][column].ToString()));
                id = IDName + row;
                return id;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string setStringNull(string str, string sign, string value)
        {
            if (value == "" || value == " ")
            {
                return "";
            }
            else
            {
                return str + sign + value + "\r\n";
            }
        }

        public string setComma(string value)
        {
            try
            {
                return string.Format("{0:#,##0}", Convert.ToInt64(value));
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public string getPath(string folderName)
        {
            return AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10) + folderName + "\\";
        }

        public string GetIncome(string strPaySub)
        {
            Parameter = new string[,]
            {
                {"@MsPaymentSubCode", strPaySub},

            };

            db.Get("Spr_S_TblMasterPaymentSub", Parameter, out Error, out dt);
            return dt.Rows[0]["MsPaymentSubType"].ToString();
        }

        public string getFileLastName()
        {
            string id = "";

            try
            {
                id += DateTime.Today.ToString("MM");
                id += DateTime.Today.ToString("dd");
                id += DateTime.Now.ToString("Hmm-ss");

                return id;
            }
            catch
            {
                return id;
            }
        }

        public string getComboboxId(ComboBox cbb)
        {
            string value = "";

            if (cbb.SelectedIndex > -1)
            {
                value = cbb.SelectedValue.ToString();
            }

            if (value == "System.Data.DataRowView")
            {
                value = "";
            }

            return value;
        }

        public string getComboboxIdZero(ComboBox cbb)
        {
            string value = "0";

            if (cbb.SelectedIndex > -1)
            {
                value = cbb.SelectedValue.ToString();
            }

            return value;
        }

        public string getComboboxValueBefore(ComboBox cbb)
        {
            string value = "";

            try
            {
                if (cbb.SelectedIndex > -1)
                {
                    int before = cbb.SelectedIndex - 1;
                    cbb.SelectedIndex = before;
                    value = cbb.SelectedValue.ToString();
                }

                return value;
            }
            catch (Exception)
            {
                return value;
            }
        }

        public string getCollectSummary(string strType, string strMoneyValue, int collected)
        {
            try
            {
                DataTable dt = new DataTable();
                string strErr = "";


                string[,] Parameter = new string[,]
                {
                    {"@MoneyValue", strMoneyValue},
                    {"@MoneyCollect",  collected.ToString()}
                };

                db.Get("Spr_F_GetTotalCoin", Parameter, out strErr, out dt);

                if (strType == "Amount")
                {
                    return dt.Rows[0]["MoneyAmount"].ToString();
                }
                else
                {
                    return dt.Rows[0]["MoneyNumber"].ToString();
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public void ShowLogoImage(string strCode, PictureBox pb)
        {
            try
            {
                clsImage Images = new clsImage();
                StoreConstant Store = new StoreConstant();
                DataTable dt = new DataTable();

                string[,] Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", strCode},
                    {"@MsLogoNameTh", ""},
                    {"@MsLogoNameEn", ""},
                    {"@MsLogoWeb", ""},
                    {"@MsLogoFileName", ""},
                    {"@MsLogoType", "0"},
                    {"@MsLogoStatus", "0"},
                };

                db.Get("Store.SelectLogo", Parameter, out Error, out dt);
                Images.ShowImage(pb, Code: GetValue(dt, Field.Path));
            }
            catch (Exception)
            {
            }
        }

        public void showGridViewFormatFromStore(DataTable dt, DataGridView dataGridView,
            string Col0Name, int Col0Width, Boolean Col0Show, DataGridViewContentAlignment Col0HeadStyle, DataGridViewContentAlignment Col0DefaultStyle
          , string Col1Name, int Col1Width, Boolean Col1Show, DataGridViewContentAlignment Col1HeadStyle, DataGridViewContentAlignment Col1DefaultStyle
          , string Col2Name, int Col2Width, Boolean Col2Show, DataGridViewContentAlignment Col2HeadStyle, DataGridViewContentAlignment Col2DefaultStyle
          , string Col3Name, int Col3Width, Boolean Col3Show, DataGridViewContentAlignment Col3HeadStyle, DataGridViewContentAlignment Col3DefaultStyle
          , string Col4Name, int Col4Width, Boolean Col4Show, DataGridViewContentAlignment Col4HeadStyle, DataGridViewContentAlignment Col4DefaultStyle
          , string Col5Name, int Col5Width, Boolean Col5Show, DataGridViewContentAlignment Col5HeadStyle, DataGridViewContentAlignment Col5DefaultStyle
          , string Col6Name, int Col6Width, Boolean Col6Show, DataGridViewContentAlignment Col6HeadStyle, DataGridViewContentAlignment Col6DefaultStyle

          )
        {
            clsSearch clsSearch = new clsSearch();

            dataGridView.DataSource = dt;
            numberRow(dataGridView);

            dataGridView.Columns[0].Visible = Col0Show;
            dataGridView.Columns[0].HeaderText = Col0Name;
            dataGridView.Columns[0].Width = Col0Width;
            dataGridView.Columns[0].HeaderCell.Style.Alignment = Col0HeadStyle;
            dataGridView.Columns[0].DefaultCellStyle.Alignment = Col0DefaultStyle;

            dataGridView.Columns[1].Visible = Col1Show;
            dataGridView.Columns[1].HeaderText = Col1Name;
            dataGridView.Columns[1].Width = Col1Width;
            dataGridView.Columns[1].HeaderCell.Style.Alignment = Col1HeadStyle;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = Col1DefaultStyle;

            dataGridView.Columns[2].Visible = Col2Show;
            dataGridView.Columns[2].HeaderText = Col2Name;
            dataGridView.Columns[2].Width = Col2Width;
            dataGridView.Columns[2].HeaderCell.Style.Alignment = Col2HeadStyle;
            dataGridView.Columns[2].DefaultCellStyle.Alignment = Col2DefaultStyle;

            dataGridView.Columns[3].Visible = Col3Show;
            dataGridView.Columns[3].HeaderText = Col3Name;
            dataGridView.Columns[3].Width = Col3Width;
            dataGridView.Columns[3].HeaderCell.Style.Alignment = Col3HeadStyle;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = Col3DefaultStyle;

            dataGridView.Columns[4].Visible = Col4Show;
            dataGridView.Columns[4].HeaderText = Col4Name;
            dataGridView.Columns[4].Width = Col4Width;
            dataGridView.Columns[4].HeaderCell.Style.Alignment = Col4HeadStyle;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = Col4DefaultStyle;

            dataGridView.Columns[5].Visible = Col5Show;
            dataGridView.Columns[5].HeaderText = Col5Name;
            dataGridView.Columns[5].Width = Col5Width;
            dataGridView.Columns[5].HeaderCell.Style.Alignment = Col5HeadStyle;
            dataGridView.Columns[5].DefaultCellStyle.Alignment = Col5DefaultStyle;

            dataGridView.Columns[6].Visible = Col6Show;
            dataGridView.Columns[6].HeaderText = Col6Name;
            dataGridView.Columns[6].Width = Col6Width;
            dataGridView.Columns[6].HeaderCell.Style.Alignment = Col6HeadStyle;
            dataGridView.Columns[6].DefaultCellStyle.Alignment = Col6DefaultStyle;
        }

        public void showGridViewFormatFromStore(DataTable dt, DataGridView dataGridView,
            string Col0Name, int Col0Width, Boolean Col0Show, DataGridViewContentAlignment Col0HeadStyle, DataGridViewContentAlignment Col0DefaultStyle
          , string Col1Name, int Col1Width, Boolean Col1Show, DataGridViewContentAlignment Col1HeadStyle, DataGridViewContentAlignment Col1DefaultStyle
          , string Col2Name, int Col2Width, Boolean Col2Show, DataGridViewContentAlignment Col2HeadStyle, DataGridViewContentAlignment Col2DefaultStyle
          , string Col3Name, int Col3Width, Boolean Col3Show, DataGridViewContentAlignment Col3HeadStyle, DataGridViewContentAlignment Col3DefaultStyle
          , string Col4Name, int Col4Width, Boolean Col4Show, DataGridViewContentAlignment Col4HeadStyle, DataGridViewContentAlignment Col4DefaultStyle
          , string Col5Name, int Col5Width, Boolean Col5Show, DataGridViewContentAlignment Col5HeadStyle, DataGridViewContentAlignment Col5DefaultStyle
          , string Col6Name, int Col6Width, Boolean Col6Show, DataGridViewContentAlignment Col6HeadStyle, DataGridViewContentAlignment Col6DefaultStyle
          , string Col7Name, int Col7Width, Boolean Col7Show, DataGridViewContentAlignment Col7HeadStyle, DataGridViewContentAlignment Col7DefaultStyle

          )
        {
            clsSearch clsSearch = new clsSearch();

            dataGridView.DataSource = dt;
            numberRow(dataGridView);

            dataGridView.Columns[0].Visible = Col0Show;
            dataGridView.Columns[0].HeaderText = Col0Name;
            dataGridView.Columns[0].Width = Col0Width;
            dataGridView.Columns[0].HeaderCell.Style.Alignment = Col0HeadStyle;
            dataGridView.Columns[0].DefaultCellStyle.Alignment = Col0DefaultStyle;

            dataGridView.Columns[1].Visible = Col1Show;
            dataGridView.Columns[1].HeaderText = Col1Name;
            dataGridView.Columns[1].Width = Col1Width;
            dataGridView.Columns[1].HeaderCell.Style.Alignment = Col1HeadStyle;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = Col1DefaultStyle;

            dataGridView.Columns[2].Visible = Col2Show;
            dataGridView.Columns[2].HeaderText = Col2Name;
            dataGridView.Columns[2].Width = Col2Width;
            dataGridView.Columns[2].HeaderCell.Style.Alignment = Col2HeadStyle;
            dataGridView.Columns[2].DefaultCellStyle.Alignment = Col2DefaultStyle;

            dataGridView.Columns[3].Visible = Col3Show;
            dataGridView.Columns[3].HeaderText = Col3Name;
            dataGridView.Columns[3].Width = Col3Width;
            dataGridView.Columns[3].HeaderCell.Style.Alignment = Col3HeadStyle;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = Col3DefaultStyle;

            dataGridView.Columns[4].Visible = Col4Show;
            dataGridView.Columns[4].HeaderText = Col4Name;
            dataGridView.Columns[4].Width = Col4Width;
            dataGridView.Columns[4].HeaderCell.Style.Alignment = Col4HeadStyle;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = Col4DefaultStyle;

            dataGridView.Columns[5].Visible = Col5Show;
            dataGridView.Columns[5].HeaderText = Col5Name;
            dataGridView.Columns[5].Width = Col5Width;
            dataGridView.Columns[5].HeaderCell.Style.Alignment = Col5HeadStyle;
            dataGridView.Columns[5].DefaultCellStyle.Alignment = Col5DefaultStyle;

            dataGridView.Columns[6].Visible = Col6Show;
            dataGridView.Columns[6].HeaderText = Col6Name;
            dataGridView.Columns[6].Width = Col6Width;
            dataGridView.Columns[6].HeaderCell.Style.Alignment = Col6HeadStyle;
            dataGridView.Columns[6].DefaultCellStyle.Alignment = Col6DefaultStyle;

            dataGridView.Columns[7].Visible = Col7Show;
            dataGridView.Columns[7].HeaderText = Col7Name;
            dataGridView.Columns[7].Width = Col7Width;
            dataGridView.Columns[7].HeaderCell.Style.Alignment = Col7HeadStyle;
            dataGridView.Columns[7].DefaultCellStyle.Alignment = Col7DefaultStyle;
        }

        public string CopyImage(string strPathName, string strLocation, string strImgCode, string strImgType, string strOperation, bool save = false)
        {
            string oldFileLocation = strLocation;
            string newFileLocation = getImagePath(strPathName, strImgCode, strImgType);
            string moveFileLocation = getImagePath(strPathName, "_" + strImgCode, strImgType);

            if (strLocation == "-" | strLocation == "" | strLocation == null)
            {
                try
                {
                    if (File.Exists(newFileLocation))
                    {
                        if (File.Exists(moveFileLocation))
                        {
                            FileSystem.DeleteFile(moveFileLocation, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                            FileSystem.DeleteFile(newFileLocation, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                            return "-";
                        }
                        else
                        {
                            FileSystem.DeleteFile(newFileLocation, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                            return "-";
                        }
                    }
                    else
                    {
                        return "-";
                    }
                }
                catch (Exception)
                {
                    return "-";
                }
            }
            else if (strLocation == newFileLocation & strOperation != "DI")
            {
                return strPathName;
            }
            else
            {
                try
                {
                    if (strOperation == "I")
                    {
                        try
                        {
                            Image bitmap = Image.FromFile(oldFileLocation);
                            bitmap.Save(newFileLocation);

                            return strPathName;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                File.Delete(newFileLocation);

                                Image bitmap = Image.FromFile(oldFileLocation);
                                bitmap.Save(newFileLocation);
                                return strPathName;
                            }
                            catch (Exception)
                            {
                                return "-";
                            }
                        }
                    }
                    else if (strOperation == "U")
                    {
                        try
                        {
                            if (File.Exists(newFileLocation))
                            {
                                File.Move(newFileLocation, moveFileLocation);
                                Image bitmap = Image.FromFile(oldFileLocation);
                                bitmap.Save(newFileLocation);
                            }
                            else
                            {
                                Image bitmap = Image.FromFile(oldFileLocation);
                                bitmap.Save(newFileLocation);
                            }

                            return strPathName;
                        }
                        catch (Exception)
                        {
                            return "-";
                        }
                    }
                    else if (strOperation == "D")
                    {
                        try
                        {
                            if (File.Exists(newFileLocation))
                            {
                                if (File.Exists(moveFileLocation))
                                {
                                    FileSystem.DeleteFile(moveFileLocation, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                                    FileSystem.DeleteFile(newFileLocation, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                                    return "-";
                                }
                                else
                                {
                                    FileSystem.DeleteFile(newFileLocation, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                                    return "-";
                                }
                            }
                            else
                            {
                                return "-";
                            }
                        }
                        catch (Exception)
                        {
                            return "-";
                        }
                    }
                    else
                    {
                        return "-";
                    }
                }
                catch (Exception)
                {
                    return "-";
                }
            }
        }

        public void numberRow(DataGridView dataGridView)
        {
            if (dataGridView.Name == "dataGridView0")
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    row.Cells["No0"].Value = row.Index + 1;
                }
            }
            else if (dataGridView.Name == "dataGridView1")
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    row.Cells["No1"].Value = row.Index + 1;
                }
            }
            else if (dataGridView.Name == "dataGridView")
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    row.Cells["No"].Value = row.Index + 1;
                }
            }
            else if (dataGridView.Name == "dtgNProduct")
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    row.Cells["NNo"].Value = row.Index + 1;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    row.Cells["SNo"].Value = row.Index + 1;
                }
            }
        }

        public string sumDebit(string date)
        {
            DataTable dt = new DataTable();
            string strErr = "";


            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@Date", date},
                };

                db.Get("Spr_F_GetMoneyDebit", Parameter, out strErr, out dt);

                return string.Format("{0:#,##0.00}", double.Parse(Convert.ToString(dt.Rows[0]["Sum"].ToString())));
            }
            catch (Exception)
            {
                return "0.00";
            }
        }

        public string sumCredit(string date)
        {
            DataTable dt = new DataTable();
            string strErr = "";


            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@Date", date},
                };

                db.Get("Spr_F_GetMoneyCredit", Parameter, out strErr, out dt);

                return string.Format("{0:#,##0.00}", double.Parse(Convert.ToString(dt.Rows[0]["Sum"].ToString())));
            }
            catch (Exception)
            {
                return "0.00";
            }
        }

        public string SumBankDebit(string date, string account = "")
        {
            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@Date", date},
                    {"@Account", account},
                };

                db.Get("Spr_F_GetBankDebit", Parameter, out Error, out dt);

                return string.Format("{0:#,##0.00}", double.Parse(Convert.ToString(dt.Rows[0]["Sum"].ToString())));
            }
            catch (Exception)
            {
                return "0.00";
            }
        }

        public string sumWallet(string date, string account = "")
        {
            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@Date", date},
                    {"@MoneyCode", account},
                };

                db.Get("Spr_F_GetWalletBalance", Parameter, out Error, out dt);

                return string.Format("{0:#,##0.00}", double.Parse(Convert.ToString(dt.Rows[0]["Sum"].ToString())));
            }
            catch (Exception)
            {
                return "0.00";
            }
        }

        public string SumBankCredit(string date, string account = "")
        {
            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@Date", date},
                    {"@Account", account},
                };

                db.Get("Spr_F_GetBankCredit", Parameter, out Error, out dt);

                return string.Format("{0:#,##0.00}", double.Parse(Convert.ToString(dt.Rows[0]["Sum"].ToString())));
            }
            catch (Exception)
            {
                return "0.00";
            }
        }

        public string sumWalletBalance(string date, string account = "")
        {
            try
            {
                account = account == "" ? Setting.GetWallet() : account;
                return string.Format("{0:#,##0.00}", double.Parse(sumWallet(date, account)));
            }
            catch (Exception)
            {
                return "0.00";
            }
        }

        public string ConvertPhoneNumber(string phoneNumber)
        {
            try
            {
                string numberOnly = "";
                
                for (int i = 0; i < phoneNumber.Length; i++)
                {
                    if (Char.IsDigit(phoneNumber[i]))
                        numberOnly += phoneNumber[i];
                }

                Int64 intNumber = Convert.ToInt64(numberOnly);
                string strNumber = Convert.ToString(intNumber);

                //Mobile//
                if (strNumber.Length == 9)
                {
                    return Regex.Replace(strNumber, @"(\d{2})(\d{3})(\d{4})", "0" + "$1 $2 $3");
                }

                //Phone//
                if (strNumber.Length == 8)
                {
                    //BKK//
                    if (strNumber.Substring(0, 1) == "2")
                    {
                        return Regex.Replace(strNumber, @"(\d{4})(\d{4})", "0 " + "$1 $2");
                    }
                    //UPC//
                    else
                    {
                        return Regex.Replace(strNumber, @"(\d{2})(\d{3})(\d{3})", "0" + "$1 $2 $3");
                    }
                }

                return strNumber;
            }
            catch
            {
                return phoneNumber;
            }
        }

        public bool CheckEmail(string strEmail)
        {
            try
            {
                return Regex.IsMatch(strEmail,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public void btnEnter(KeyPressEventArgs e, Button likeBtnClick)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                likeBtnClick.PerformClick();
            }
        }

        public string SubstringBefore(string str, string IndexOf)
        {
            int posA = str.IndexOf(IndexOf);
            if (posA == -1)
            {
                return "";
            }
            return str.Substring(0, posA);
        }

        public string SubstringAfter(string str, string IndexOf)
        {
            int posA = str.LastIndexOf(IndexOf);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + IndexOf.Length;
            if (adjustedPosA >= str.Length)
            {
                return "";
            }
            return str.Substring(adjustedPosA);
        }

        public string keyPress(object sender, KeyEventArgs e)
        {
            string codeString = "";

            if (e.Control == true)
            {
                codeString = "Ctrl";
            }
            else if (e.Shift == true)
            {
                codeString = "Shift";
            }
            else if (e.Alt == true)
            {
                codeString = "Alt";
            }
            else
            {
            }

            if (e.KeyCode == Keys.S)
            {
                codeString += "+S";
            }
            else if (e.KeyCode == Keys.E)
            {
                codeString += "+E";
            }
            else if (e.KeyCode == Keys.D)
            {
                codeString += "+D";
            }
            else if (e.KeyCode == Keys.F)
            {
                codeString += "+F";
            }
            else if (e.KeyCode == Keys.C)
            {
                codeString += "+C";
            }
            else if (e.KeyCode == Keys.N)
            {
                codeString += "+N";
            }
            else if (e.KeyCode == Keys.P)
            {
                codeString += "+P";
            }
            else if (e.KeyCode == Keys.Enter)
            {
                codeString += "Enter";
            }
            else
            {
            }

            return codeString;
        }

        public void MesNoData()
        {
            MessageBox.Show("ไม่พบข้อมูลที่คุณค้นหา", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void MesPassNotMat()
        {
            MessageBox.Show("ชื่อหรือรหัสผ่านไม่ถูกต้อง กรุณาตรวจสอบใหม่อีกครั้ง", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }





        public int RowDuplicate(string strTable = "", string strValue1 = "", string strValue2 = "", string strValue3 = "", string strValue4 = "", string strDetail = "")
        {
            try
            {
                DataTable dt = new DataTable();
                string strErr = "";


                string[,] Parameter = new string[,]
                    {
                        {"@Table",      strTable},
                        {"@Value1",     strValue1},
                        {"@Value2",     strValue2},
                        {"@Value3",     strValue3},
                        {"@Value4",     strValue4},
                    };

                db.Get("Spr_F_GetDataDuplicate", Parameter, out strErr, out dt);
                int row = Convert.ToInt32(dt.Rows[0]["Num"].ToString());

                return row;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void getWaterData(string qrCode)
        {
            clsConvert Converts = new clsConvert();
            string[] lines = qrCode.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            int Units = Convert.ToInt32(lines[2].Substring(8, 7));

            WaterModel wm = new WaterModel();
            GlobalVar.WaterDataList.Clear();

            wm.Amount = Converts.StringToDouble(lines[3]);
            wm.PayDate = ConvertToDate(lines[2].Substring(0, 6), Fm.DDMMYY);

            int intDay = Convert.ToInt32(wm.PayDate.Day);
            int intMonth = Convert.ToInt32(wm.PayDate.Month);
            int intYear = Convert.ToInt32(lines[2].Substring(4, 2));

            string strReadDateNext = "12" + String.Format("{0:D2}", intMonth - 1) + String.Format("{0:D2}", intYear);
            string strReadDateCurrent = "12" + String.Format("{0:D2}", intMonth) + String.Format("{0:D2}", intYear);

            if (intDay > 15)
            {
                wm.ReadDate = ConvertToDate(strReadDateCurrent, Fm.DDMMYY);
            }
            else
            {
                wm.ReadDate = ConvertToDate(strReadDateNext, Fm.DDMMYY);
            }

            wm.ReceiptId = lines[1].Substring(11, 6) + "-" + lines[1].Substring(17, 1);
            wm.Unit = Units > 100 ? Units / 10 : Units;
            GlobalVar.WaterDataList.Add(wm);
        }

        public void getElectricData(string qrCode)
        {
            clsConvert Converts = new clsConvert();
            string[] lines = qrCode.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            ElectricModel em = new ElectricModel();

            em.Header = "|" + lines[0];

            if (lines[1].Length == 18)
            {
                em.CaRefNo = lines[1].Substring(0, 9);
                em.Unit = Convert.ToInt32(lines[1].Substring(lines[1].Length - 4, 3));
                em.qrLine2 = lines[1];
            }

            if (lines[2].Length == 18)
            {
                em.ReceiptId = lines[2].Substring(1, 11);
                em.PayDate = ConvertToDate(lines[2].Substring((lines[2].Length) - 6, 6), Fm.DDMMYY);

                int intDay = Convert.ToInt32(em.PayDate.Day);
                int intMonth = Convert.ToInt32(em.PayDate.Month);

                string strMonthYear = "18" + String.Format("{0:D2}", intMonth - 1) + lines[2].Substring((lines[2].Length) - 2, 2);

                if (intDay > 15)
                {
                    em.ReadDate = ConvertToDate("18" + lines[2].Substring((lines[2].Length) - 4, 4), Fm.DDMMYY);
                }
                else
                {
                    em.ReadDate = ConvertToDate(strMonthYear, Fm.DDMMYY);
                }

                em.qrLine3 = lines[2];
            }

            if (lines[3].Length >= 3)
            {
                em.Amount = Converts.StringToDouble(lines[3]);
                em.qrLine4 = lines[3];
            }

            GlobalVar.ElectricDataList.Clear();
            GlobalVar.ElectricDataList.Add(em);
        }

        public DateTime _dueDate(string dueDate)
        {
            return Convert.ToDateTime(dueDate.ToString());
        }

        public DateTime _payDate(string payDate)
        {
            return Convert.ToDateTime(payDate.ToString());
        }

        public string _createDate()
        {
            return DateTime.Today.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-EN"));
        }

        public string _createDateAndTime()
        {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.CreateSpecificCulture("en-EN"));
        }

        public string _updatDate()
        {
            return DateTime.Today.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-EN"));
        }

        public string _Date(DateTime dt, string format)
        {
            return dt.ToString(format, CultureInfo.CreateSpecificCulture("en-EN"));
        }

        public string _getPaySubID(String txtID)
        {
            try
            {
                string getID = "";
                string txtSql = "SELECT * FROM [TB_PAYSUB] WHERE [PAYSUB_PAY] = @ID ORDER BY [PAYSUB_NAME]";
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                Connect.ConnectionString = strConnect;
                Connect.Open();

                StringBuilder strSql = new StringBuilder(txtSql);
                SqlCommand runSql = new SqlCommand(strSql.ToString());

                runSql.Parameters.Add(new SqlParameter("@ID", txtID));

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(runSql);
                DataTable dt = new DataTable();

                da.Fill(dt);
                int number = dt.Rows.Count;
                string myString = txtID.ToString();
                getID = (myString).Substring(0, 3) + (number + 1).ToString("0###");
                //myString.Substring(3)  + (number + 1);

                Connect.Close();
                return getID;
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA getPaySub-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public void checkNumber(object sender, KeyPressEventArgs e)
        {
        }

        public string _formatToDigit(int digit, string str)
        {
            try
            {
                return (str.ToString()).PadLeft(digit, '0');
            }
            catch
            {
                return str;
            }
        }

        public string _where(string Condition, string txtbox, string tableAndColumn)
        {
            string strSql = "";
            return strSql = " " + Condition + " " + tableAndColumn + " LIKE '%" + txtbox + "%'";
        }

        public string _textSearch(string txtbox, string txtSearch)
        {
            string strSearch = "";
            return strSearch = " | " + txtSearch + "  = '" + txtbox + "' ";
        }

        public void _excel(DataGridView dataGridView, string strName)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;
                ws.Name = "TB_" + strName;

                excel.Visible = true;

                ws.Cells[1, 1] = "NO";
                ws.Cells[1, 2] = strName + "_ID";
                ws.Cells[1, 3] = strName + "_NAME";
                ws.Cells[1, 4] = strName + "_STATUS";
                ws.Cells[1, 5] = strName + "_CREATE_DATE";
                ws.Cells[1, 6] = strName + "_UPDATE_DATE";

                for (int j = 1; j <= dataGridView.Rows.Count; j++)
                {
                    for (int i = 1; i <= 6; i++)
                    {
                        ws.Cells[j + 1, i] = dataGridView.Rows[j - 1].Cells[i - 1].Value;
                    }
                }

                MessageBox.Show("Export ข้อมูลเป็น Excel เรียบร้อย", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA exportExcel-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _excelFix(DataGridView dataGridView, string strName, string colID, string colName, string col1, string col2, string col3)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = excel.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;
                ws.Name = "TB_" + strName;

                excel.Visible = true;

                ws.Cells[1, 1] = "ลำดับ";
                ws.Cells[1, 2] = colID;
                ws.Cells[1, 3] = colName;
                ws.Cells[1, 4] = col1;
                ws.Cells[1, 5] = col2;
                ws.Cells[1, 6] = col3;

                for (int j = 1; j <= dataGridView.Rows.Count; j++)
                {
                    for (int i = 1; i <= 6; i++)
                    {
                        ws.Cells[j + 1, i] = dataGridView.Rows[j - 1].Cells[i - 1].Value;
                    }
                }

                MessageBox.Show("Export ข้อมูลเป็น Excel เรียบร้อย", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA exportExcelFix-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listStatus(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_STATUS]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA statusList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listPath(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_PATH] WHERE [PATH_STATUS] = 'Y'";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA pathList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listAccount(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_ACCOUNT]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA accountList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listPayBank(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_PAYSUB] WHERE [PAYSUB_PAY] = 'BST-000-1' ORDER BY [PAYSUB_NAME]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA listPayBank-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listStatusWhere(ComboBox ccbName, string txtDisplay, string txtValue, string txtType)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_STATUS] WHERE [STATUS_TYPE] IN (" + txtType + ") ORDER BY [STATUS_NAME_TH]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA statusListWhere-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listCoinType(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_COIN_TYPE]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA coinTypeList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listMonth(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT DISTINCT [DEBT_LIST] , [DEBT_MONTH] + '/' + [DEBT_YEAR] AS [DEBT_MONTH] FROM  [dbo].[TB_DEBT] ORDER BY [DEBT_LIST]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA listMonth-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listlogo(ComboBox ccbName, string txtDisplay, string txtValue, string txtWhere)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_LOGO]" + txtWhere;

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA logoList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listMoney(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [Tbl_Master_Money] ORDER BY [MsMoneyName]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA moneyList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listPaySub(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_PAYSUB]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA paySubList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listPaySubWhere(ComboBox ccbName, string txtDisplay, string txtValue, string ID)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_PAYSUB] WHERE [PAYSUB_PAY]  = '" + ID + "' AND [PAYSUB_STATUS] = 'Y' ORDER BY [PAYSUB_NAME]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception)
            {
            }
        }

        public void _listUserType(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_USER_TYPE] WHERE [USER_TYPE_STATUS] = 'Y'";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA userTypeList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listPay(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_PAY] WHERE [PAY_STATUS] = 'Y' ORDER BY [PAY_NAME]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA payList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listPayWhere(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_PAY] WHERE [PAY_STATUS] = 'Y' AND [PAY_ID] != 'BST-000-0' ORDER BY [PAY_NAME]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA payList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listDebt(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT DISTINCT[DEBT_LOGO],[DEBT_NAME] FROM [dbRSA].[dbo].[TB_DEBT] ORDER BY[DEBT_NAME]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA depyList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listMonthDebt(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT DISTINCT[DEBT_MONTH] FROM [dbRSA].[dbo].[TB_DEBT]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA ListMonthDebt-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listYear(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT DISTINCT[DEBT_YEAR] FROM [dbRSA].[dbo].[TB_DEBT]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA ListYear-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listBank(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_BANK] WHERE [BANK_STATUS] = 'Y'";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA bankList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listBranch(ComboBox ccbName, string txtDisplay, string txtValue, string txtWhere)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_BRANCH] WHERE [BRANCH_STATUS] = 'Y' AND [BRANCH_BANK] = '" + txtWhere + "'";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA branchList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listProvince(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_PROVINCE] WHERE [PROVINCE_STATUS] = 'Y' ORDER BY [PROVINCE_NAME]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception)
            {
            }
        }

        public void _listCredit(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_CARD] WHERE [CARD_STATUS] = 'Y'";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA listCredit-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listAmphur(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_AMPHUR] WHERE [AMPHUR_STATUS] = 'Y'";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception)
            {
            }
        }

        public void _listTambol(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_DISTRICT] WHERE [DISTRICT_STATUS] = 'Y'";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show("RSA branchList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listGeographyt(ComboBox ccbName, string txtDisplay, string txtValue)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_GEOGRAPHY] WHERE [GEOGRAPHY_STATUS] = 'Y'";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show("RSA branchList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listTambolWhere(ComboBox ccbName, string txtDisplay, string txtValue, string txtWhere)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_DISTRICT] WHERE [DISTRICT_STATUS] = 'Y' AND [AMPHUR_ID] = '" + txtWhere + "'";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show("RSA branchList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listAmphurWhere(ComboBox ccbName, string txtDisplay, string txtValue, string txtWhere)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_AMPHUR] WHERE [AMPHUR_STATUS] = 'Y' AND [AMPHUR_PROVINCE] = '" + txtWhere + "'";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show("RSA branchList-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listProvinceWhere(ComboBox ccbName, string txtDisplay, string txtValue, string txtWhere)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_PROVINCE] WHERE [PROVINCE_STATUS] = 'Y' AND [PROVINCE_ID] = '" + txtWhere + "' ORDER BY [PROVINCE_NAME]";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception)
            {
                //MessageBox.Show("RSA ProvinceListWhere-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _listGeographyWhere(ComboBox ccbName, string txtDisplay, string txtValue, string txtWhere)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                StringBuilder strSql = new StringBuilder();
                SqlCommand runSql = new SqlCommand();

                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();

                string txtSql = "SELECT * FROM [TB_GEOGRAPHY] WHERE [GEOGRAPHY_STATUS] = 'Y' AND [GEOGRAPHY_ID] = '" + txtWhere + "'";

                Connect.ConnectionString = strConnect;
                Connect.Open();

                strSql = new StringBuilder(txtSql);
                runSql = new SqlCommand(strSql.ToString());

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                da = new SqlDataAdapter(runSql);
                da.Fill(ds, "DataList");

                ccbName.DataSource = ds.Tables["DataList"];
                ccbName.DisplayMember = txtDisplay;
                ccbName.ValueMember = txtValue;

                Connect.Close();
            }
            catch (Exception)
            {
            }
        }

        public void _showDuplicate()
        {
            MessageBox.Show("มีข้อมูลนี้แล้ว กรุณาตรวจสอบ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public DataTable _countRow(string txtSql)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                Connect.ConnectionString = strConnect;
                Connect.Open();

                StringBuilder strSql = new StringBuilder(txtSql);
                SqlCommand runSql = new SqlCommand(strSql.ToString());
                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(runSql);
                DataTable dt = new DataTable();

                da.Fill(dt);
                Connect.Close();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable _countRowST(string txtSql)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbStamp"].ConnectionString;

                Connect.ConnectionString = strConnect;
                Connect.Open();

                StringBuilder strSql = new StringBuilder(txtSql);
                SqlCommand runSql = new SqlCommand(strSql.ToString());
                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(runSql);
                DataTable dt = new DataTable();

                da.Fill(dt);
                Connect.Close();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable _setValue(string txtSql)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                Connect.ConnectionString = strConnect;
                Connect.Open();

                StringBuilder strSql = new StringBuilder(txtSql);
                SqlCommand runSql = new SqlCommand(strSql.ToString());
                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(runSql);
                DataTable dt = new DataTable();

                da.Fill(dt);
                Connect.Close();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DataTable _setValueStamp(string txtSql)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbStamp"].ConnectionString;

                Connect.ConnectionString = strConnect;
                Connect.Open();

                StringBuilder strSql = new StringBuilder(txtSql);
                SqlCommand runSql = new SqlCommand(strSql.ToString());
                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(runSql);
                DataTable dt = new DataTable();

                da.Fill(dt);
                Connect.Close();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //public DataTable _login(String txtUser, String txtPass)
        //{
        //    try
        //    {
        //        string txtSql = "SELECT * FROM [TB_USER] WHERE [USER_ID] = @User AND [USER_PASSWORD] = @Pass";
        //        SqlConnection Connect = new SqlConnection();
        //        String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

        //        Connect.ConnectionString = strConnect;
        //        Connect.Open();

        //        StringBuilder strSql = new StringBuilder(txtSql);
        //        SqlCommand runSql = new SqlCommand(strSql.ToString());

        //        runSql.Parameters.Add(new SqlParameter("@User", txtUser));
        //        runSql.Parameters.Add(new SqlParameter("@Pass", txtPass));

        //        runSql.Connection = Connect;
        //        runSql.ExecuteNonQuery();

        //        SqlDataAdapter da = new SqlDataAdapter(runSql);
        //        DataTable dt = new DataTable();

        //        da.Fill(dt);
        //        Connect.Close();
        //        return dt;

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("RSA daConnectLogin-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;
        //    }
        //}

        public DataTable _countRow(string tbFrom, string colWhere, string value)
        {
            try
            {
                string txtSql = "SELECT * FROM [dbRSA].[dbo].[" + tbFrom + "] WHERE [" + colWhere + "] = @VALUE";
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                Connect.ConnectionString = strConnect;
                Connect.Open();

                StringBuilder strSql = new StringBuilder(txtSql);
                SqlCommand runSql = new SqlCommand(strSql.ToString());

                runSql.Parameters.Add(new SqlParameter("@VALUE", value));

                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(runSql);
                DataTable dt = new DataTable();

                da.Fill(dt);
                Connect.Close();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void _showGridViewDateFormat(
                DataGridView dataGridView,
                string StrSql,
                string colName1,
                string colName2,
                string colName3,
                string colName4,
                string colName5)
        {
            clsSearch clsSearch = new clsSearch();

            //dataGridView.DataSource = clsSearch._searchForGrid(StrSql);
            numberRow(dataGridView);

            dataGridView.Columns[0].Width = 100;
            dataGridView.Columns[1].HeaderText = colName1;
            dataGridView.Columns[1].Width = 200;
            dataGridView.Columns[2].HeaderText = colName2;
            dataGridView.Columns[2].Width = 450;
            dataGridView.Columns[3].HeaderText = colName3;
            dataGridView.Columns[3].Width = 300;
            dataGridView.Columns[4].HeaderText = colName4;
            dataGridView.Columns[4].Width = 200;
            dataGridView.Columns[5].HeaderText = colName5;
            dataGridView.Columns[5].Width = 200;

            dataGridView.Columns[5].DefaultCellStyle = new DataGridViewCellStyle { Format = "d MMMM yyyy" };
            dataGridView.Columns[4].DefaultCellStyle = new DataGridViewCellStyle { Format = "d MMMM yyyy" };

            dataGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void _showGridViewDateFormatLast(
        DataGridView dataGridView,
        string StrSql,
        string colName1,
        string colName2,
        string colName3,
        string colName4,
        string colName5)
        {
            clsSearch clsSearch = new clsSearch();

            // //dataGridView.DataSource = clsSearch._searchForGrid(StrSql);
            numberRow(dataGridView);

            dataGridView.Columns[0].Width = 100;
            dataGridView.Columns[1].HeaderText = colName1;
            dataGridView.Columns[1].Width = 250;
            dataGridView.Columns[2].HeaderText = colName2;
            dataGridView.Columns[2].Width = 300;
            dataGridView.Columns[3].HeaderText = colName3;
            dataGridView.Columns[3].Width = 400;
            dataGridView.Columns[4].HeaderText = colName4;
            dataGridView.Columns[4].Width = 200;
            dataGridView.Columns[5].HeaderText = colName5;
            dataGridView.Columns[5].Width = 200;

            dataGridView.Columns[5].DefaultCellStyle = new DataGridViewCellStyle { Format = "d MMMM yyyy" };

            dataGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void _showGridViewStamp(
                   DataGridView dataGridView,
                   string StrSql,
                   string colName1,
                   string colName2,
                   string colName3,
                   string colName4,
                   string colName5,
                   string colName6)
        {
            clsSearch clsSearch = new clsSearch();

            //dataGridView.DataSource = clsSearch._searchForGrid(StrSql);
            numberRow(dataGridView);

            dataGridView.Columns[0].Width = 50;
            dataGridView.Columns[1].HeaderText = colName1;
            dataGridView.Columns[1].Width = 100;
            dataGridView.Columns[2].HeaderText = colName2;
            dataGridView.Columns[2].Width = 450;
            dataGridView.Columns[3].HeaderText = colName3;
            dataGridView.Columns[3].Width = 200;
            dataGridView.Columns[4].HeaderText = colName4;
            dataGridView.Columns[4].Width = 200;
            dataGridView.Columns[5].HeaderText = colName5;
            dataGridView.Columns[5].Width = 150;
            dataGridView.Columns[6].Visible = false;

            dataGridView.Columns[4].DefaultCellStyle = new DataGridViewCellStyle { Format = "d MMMM yyyy" };

            dataGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void _showGridView(
            DataGridView dataGridView,
            string StrSql,
            string colName1,
            string colName2,
            string colName3,
            string colName4,
            string colName5)
        {
            clsSearch clsSearch = new clsSearch();

            //dataGridView.DataSource = clsSearch._searchForGrid(StrSql);
            numberRow(dataGridView);

            dataGridView.Columns[0].Width = 100;
            dataGridView.Columns[1].HeaderText = colName1;
            dataGridView.Columns[1].Width = 200;
            dataGridView.Columns[2].HeaderText = colName2;
            dataGridView.Columns[2].Width = 500;
            dataGridView.Columns[3].HeaderText = colName3;
            dataGridView.Columns[3].Width = 200;
            dataGridView.Columns[4].HeaderText = colName4;
            dataGridView.Columns[4].Width = 200;
            dataGridView.Columns[5].HeaderText = colName5;
            dataGridView.Columns[5].Width = 200;

            dataGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            try
            {
                dataGridView.Columns[6].Visible = false;
            }
            catch
            {
            }
        }

        public void _showGridViewFormat(DataGridView dataGridView, string StrSql,
             string Col0Name, int Col0Width, Boolean Col0Show, DataGridViewContentAlignment Col0HeadStyle, DataGridViewContentAlignment Col0DefaultStyle
           , string Col1Name, int Col1Width, Boolean Col1Show, DataGridViewContentAlignment Col1HeadStyle, DataGridViewContentAlignment Col1DefaultStyle
           , string Col2Name, int Col2Width, Boolean Col2Show, DataGridViewContentAlignment Col2HeadStyle, DataGridViewContentAlignment Col2DefaultStyle
           , string Col3Name, int Col3Width, Boolean Col3Show, DataGridViewContentAlignment Col3HeadStyle, DataGridViewContentAlignment Col3DefaultStyle
           , string Col4Name, int Col4Width, Boolean Col4Show, DataGridViewContentAlignment Col4HeadStyle, DataGridViewContentAlignment Col4DefaultStyle
           , string Col5Name, int Col5Width, Boolean Col5Show, DataGridViewContentAlignment Col5HeadStyle, DataGridViewContentAlignment Col5DefaultStyle
           , string Col6Name, int Col6Width, Boolean Col6Show, DataGridViewContentAlignment Col6HeadStyle, DataGridViewContentAlignment Col6DefaultStyle

           )
        {
            clsSearch clsSearch = new clsSearch();

            //dataGridView.DataSource = clsSearch._searchForGrid(StrSql);
            numberRow(dataGridView);

            dataGridView.Columns[0].Visible = Col0Show;
            dataGridView.Columns[0].Width = Col0Width;
            dataGridView.Columns[0].HeaderCell.Style.Alignment = Col0HeadStyle;
            dataGridView.Columns[0].DefaultCellStyle.Alignment = Col0DefaultStyle;

            dataGridView.Columns[1].Visible = Col1Show;
            dataGridView.Columns[1].HeaderText = Col1Name;
            dataGridView.Columns[1].Width = Col1Width;
            dataGridView.Columns[1].HeaderCell.Style.Alignment = Col1HeadStyle;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = Col1DefaultStyle;

            dataGridView.Columns[2].Visible = Col2Show;
            dataGridView.Columns[2].HeaderText = Col2Name;
            dataGridView.Columns[2].Width = Col2Width;
            dataGridView.Columns[2].HeaderCell.Style.Alignment = Col2HeadStyle;
            dataGridView.Columns[2].DefaultCellStyle.Alignment = Col2DefaultStyle;

            dataGridView.Columns[3].Visible = Col3Show;
            dataGridView.Columns[3].HeaderText = Col3Name;
            dataGridView.Columns[3].Width = Col3Width;
            dataGridView.Columns[3].HeaderCell.Style.Alignment = Col3HeadStyle;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = Col3DefaultStyle;

            dataGridView.Columns[4].Visible = Col4Show;
            dataGridView.Columns[4].HeaderText = Col4Name;
            dataGridView.Columns[4].Width = Col4Width;
            dataGridView.Columns[4].HeaderCell.Style.Alignment = Col4HeadStyle;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = Col4DefaultStyle;

            dataGridView.Columns[5].Visible = Col5Show;
            dataGridView.Columns[5].HeaderText = Col5Name;
            dataGridView.Columns[5].Width = Col5Width;
            dataGridView.Columns[5].HeaderCell.Style.Alignment = Col5HeadStyle;
            dataGridView.Columns[5].DefaultCellStyle.Alignment = Col5DefaultStyle;

            dataGridView.Columns[6].Visible = Col6Show;
            dataGridView.Columns[6].HeaderText = Col6Name;
            dataGridView.Columns[6].Width = Col6Width;
            dataGridView.Columns[6].HeaderCell.Style.Alignment = Col6HeadStyle;
            dataGridView.Columns[6].DefaultCellStyle.Alignment = Col6DefaultStyle;
        }

        public void _showGridViewListBank(
        DataGridView dataGridView,
        string StrSql,
        string colName1,
        string colName2,
        string colName3,
        string colName4,
        string colName5)
        {
            clsSearch clsSearch = new clsSearch();

            //dataGridView.DataSource = clsSearch._searchForGrid(StrSql);
            numberRow(dataGridView);

            dataGridView.Columns[0].Width = 100;
            dataGridView.Columns[1].HeaderText = colName1;
            dataGridView.Columns[1].Width = 100;
            dataGridView.Columns[2].HeaderText = colName2;
            dataGridView.Columns[2].Width = 500;
            dataGridView.Columns[3].HeaderText = colName3;
            dataGridView.Columns[3].Width = 200;
            dataGridView.Columns[4].HeaderText = colName4;
            dataGridView.Columns[4].Width = 200;
            dataGridView.Columns[5].HeaderText = colName5;
            dataGridView.Columns[5].Width = 300;

            dataGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView.Columns[6].Visible = false;
        }

        public void _showGridView0(
        DataGridView dataGridView,
        string StrSql,
        string colName1,
        string colName2,
        string colName3)
        {
            clsSearch clsSearch = new clsSearch();

            ////dataGridView.DataSource = clsSearch._searchForGrid(StrSql);
            numberRow(dataGridView);

            dataGridView.Columns[0].Width = 50;
            dataGridView.Columns[1].HeaderText = colName1;
            dataGridView.Columns[1].Width = 310;
            dataGridView.Columns[2].HeaderText = colName2;
            dataGridView.Columns[2].Width = 120;
            dataGridView.Columns[3].HeaderText = colName3;
            dataGridView.Columns[3].Width = 120;

            dataGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView.Columns[4].Visible = false;
        }

        public void _showGridView1(
       DataGridView dataGridView,
       string StrSql,
       string colName1,
       string colName2,
       string colName3)
        {
            clsSearch clsSearch = new clsSearch();

            ////dataGridView.DataSource = clsSearch._searchForGrid(StrSql);
            numberRow(dataGridView);

            dataGridView.Columns[0].Width = 50;
            dataGridView.Columns[1].HeaderText = colName1;
            dataGridView.Columns[1].Width = 310;
            dataGridView.Columns[2].HeaderText = colName2;
            dataGridView.Columns[2].Width = 120;
            dataGridView.Columns[3].HeaderText = colName3;
            dataGridView.Columns[3].Width = 120;

            dataGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dataGridView.Columns[4].Visible = false;
        }

        public string _copyFile(string Name, string Type, string filePath, string pathId)
        {
            try
            {
                string Oldfile = getImagePath(pathId, Name, Type);
                string NewFile = "#" + getImagePath(pathId, Name, Type);
                if (File.Exists(NewFile))
                {
                    File.Delete(NewFile);
                    File.Move(Oldfile, NewFile);
                }
                else
                {
                    File.Move(Oldfile, NewFile);
                }

                string imagePath = getImagePath(pathId, "", "");
                Image bitmap = Image.FromFile(filePath);
                bitmap.Save(imagePath + Name + Type);
                return imagePath;
            }
            catch (Exception)
            {
                string imagePath = getImagePath(pathId, "", "");

                try
                {
                    Image bitmap = Image.FromFile(filePath);
                    bitmap.Save(imagePath + Name + Type);
                    return imagePath;
                }
                catch (Exception)
                {
                    return imagePath;
                }
            }
        }

        public void _deleteFile(string Name, string Type, string filePath, string pathId)
        {
            try
            {
                string Oldfile = getImagePath(pathId, Name, Type);
                string Newfile = getImagePath(pathId, "_" + Name, Type);

                if (File.Exists(Oldfile))
                {
                    File.Move(Oldfile, Newfile);
                    File.Delete(Oldfile);
                }
            }
            catch (Exception)
            {
            }
        }

        public string _getLocationByID(string PathId)
        {
            //string path = "";
            //clsSearch search = new clsSearch();

            //try
            //{
            //    DataTable dt;// = search._searchOneTable("TB_PATH", "PATH_ID", PathId);
            //    path = dt.Rows[0]["PATH_LOCATION"].ToString();
            //    return path;
            //}
            //catch (Exception)
            //{
            //    path = getImagePathOriginal("", "");
            //    return path;
            //}
            return "";
        }

        public string _getLocationByName(string PathName)
        {
            return "";
        }

        public void _showImageByID(PictureBox pictureBox, string name, string type, string location)
        {
            clsImage Images = new clsImage();
            StoreConstant Store = new StoreConstant();

            try
            {
                string path = _getLocationByID(location) + name + type;
                Image img = new Bitmap(path);
                pictureBox.Image = img.GetThumbnailImage(160, 150, null, new IntPtr());
            }
            catch (Exception)
            {
                Images.ShowDefault(pictureBox);
            }
        }

        public void _showImageByName(PictureBox pictureBox, string name, string type, string location)
        {
            clsImage Images = new clsImage();
            StoreConstant Store = new StoreConstant();

            try
            {
                string path = _getLocationByName(location) + name + type;
                Image img = new Bitmap(path);
                pictureBox.Image = img.GetThumbnailImage(160, 150, null, new IntPtr());
            }
            catch (Exception)
            {
                Images.ShowDefault(pictureBox);
            }
        }

        public string getStampMainID(string strStampNo)
        {
            try
            {
                string chk = "";
                int no = Convert.ToInt32(strStampNo);
                string id = no.ToString("000#");
                int sumValue = 0;

                int numberOfNo = id.Count();

                for (int i = 0; i < numberOfNo; i++)
                {
                    int value = Convert.ToInt32((id).Substring(i, 1));
                    sumValue += value * (i + 1);
                }

                chk = "S" + no.ToString("000#") + "00-" + Convert.ToString(sumValue % 9);

                return chk;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public int checkStampMainID(string strStampID)
        {
            try
            {
                string chkstrStampID = strStampID.Substring(1, 4);
                int chkDigit = Convert.ToInt32(strStampID.Substring(8, 1));
                int sumValue = 0;
                int digit = 0;

                for (int i = 0; i < 4; i++)
                {
                    int value = Convert.ToInt32((chkstrStampID).Substring(i, 1));
                    sumValue += value * (i + 1);
                }

                digit = sumValue % 9;

                if (digit == chkDigit)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void checkStampNo(TextBox tb, string str)
        {
            int numberOfNo = str.Count();

            if (numberOfNo == 9)
            {
                if (str.Substring(7, 1) == "-")
                {
                    if (checkStampMainID(str) == 1)
                    {
                        tb.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        tb.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            else
            {
                tb.ForeColor = System.Drawing.Color.Red;
            }
        }

        public string _getRowID(string txt)
        {
            //string chk = "";

            //try
            //{
            //    DataTable dt = new DataTable();
            //    clsSearch sh = new clsSearch();
            //    dt = sh._searchTable("TB_BANKNOTE");
            //    int row = dt.Rows.Count;
            //    int numberOfRow = row + Convert.ToInt32(DateTime.Now.ToString("yyMMdd"));
            //    string code = numberOfRow.ToString("000000000#");

            //    int sumValue = 0;

            //    for (int i = 0; i < code.Length; i++)
            //    {
            //        int value = Convert.ToInt32((code).Substring(i, 1));
            //        sumValue += value * (i + 1);

            //    }

            //    chk = txt + code + "-" + Convert.ToString(sumValue % 9);

            //    return chk;
            //}
            //catch (Exception)
            //{
            //    return chk;
            //}
            return "";
        }

        public string _copyImage(string nameSet, string FilePath, string copyToFolder)
        {
            string newFileName = "";
            string oldPath = "";
            string newPath = "";
            string typeFile = "";
            try
            {
                oldPath = FilePath;
                newPath = getImagePathOriginal(copyToFolder, "");

                FileInfo file = new FileInfo(oldPath);

                if (file.Exists)
                {
                    if (!Directory.Exists(newPath))
                    {
                        Directory.CreateDirectory(newPath);
                    }

                    //numFile = Directory.GetFiles(newPath, "*", SearchOption.AllDirectories).Length + 1;
                    //newFileName = nameSet + numFile.ToString("000#");
                    //typeFile = file.Extension;
                    //file.CopyTo(string.Format("{0}{1}{2}", newPath, newFileName, file.Extension));

                    //MessageBox.Show("จัดเก็บภาพ " + newFileName + " เข้าระบบเรียบร้อย");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("พบข้อผิดพลาด :\n" + ex);
                return FilePath;
            }

            return newPath + newFileName + typeFile;
        }

        public DataTable _countRowStamp(string txtSql)
        {
            try
            {
                SqlConnection Connect = new SqlConnection();
                String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbStamp"].ConnectionString;

                Connect.ConnectionString = strConnect;
                Connect.Open();

                StringBuilder strSql = new StringBuilder(txtSql);
                SqlCommand runSql = new SqlCommand(strSql.ToString());
                runSql.Connection = Connect;
                runSql.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter(runSql);
                DataTable dt = new DataTable();

                da.Fill(dt);
                Connect.Close();
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string _formatNumber(string value, string bahtNumber)
        {
            string num = "";
            Decimal val = Convert.ToDecimal(value);
            try
            {
                if (bahtNumber == "Baht")
                {
                    num = string.Format("{0:#,##0.00}", val);
                }
                if (bahtNumber == "Number")
                {
                    num = string.Format("{0:#,##0}", val);
                }

                return num;
            }
            catch (Exception)
            {
                return value;
            }
        }

        public string _formatDate(string value, string formatDate)
        {
            string date = "";
            DateTime val = Convert.ToDateTime(value);

            try
            {
                if (formatDate == "dMMMMyyyy")
                {
                    date = val.ToString("d MMMM yyyy");
                }

                return date;
            }
            catch (Exception)
            {
                return value;
            }
        }

        public void _showGridViewStamp(
            DataGridView dataGridView,
            string StrSql,
            string colName1,
            string colName2,
            string colName3,
            string colName4
            )
        {
            clsSearch clsSearch = new clsSearch();

            //dataGridView.DataSource = clsSearch._searchForGridStamp(StrSql);
            numberRow(dataGridView);

            dataGridView.Columns[0].Width = 100;
            dataGridView.Columns[1].HeaderText = colName1;
            dataGridView.Columns[1].Width = 200;
            dataGridView.Columns[2].HeaderText = colName2;
            dataGridView.Columns[2].Width = 500;
            dataGridView.Columns[3].HeaderText = colName3;
            dataGridView.Columns[3].Width = 200;
            dataGridView.Columns[4].HeaderText = colName4;
            dataGridView.Columns[4].Width = 200;

            dataGridView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public static Boolean IsFileLocked(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                //Don't change FileAccess to ReadWrite,
                //because if a file is in readOnly, it fails.
                stream = file.Open
                    (
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.None
                    );
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            //file is not locked
            return false;
        }

        public string getDate(String strType, String strValue)
        {
            try
            {
                if (strType == "DateWithTime")
                {
                    if (strValue == "Now")
                    {
                        return DateTime.Now.ToString("yyMMddHHmmss", CultureInfo.CreateSpecificCulture("en-EN"));
                    }

                    return "";
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string _convertTextDateToPara(string txtDate)
        {
            try
            {
                DateTime datetime = Convert.ToDateTime(txtDate);
                return datetime.ToString("dd/MM/yyyy");
            }
            catch
            {
                return "";
            }
        }

        public string getTssFileName(string strType)
        {
            string id = "";

            try
            {
                id += "TSS";
                id += strType;
                id += DateTime.Today.ToString("yy");
                id += DateTime.Today.ToString("MM");
                id += DateTime.Today.ToString("dd");
                id += DateTime.Now.ToString("Hmmss");

                return id;
            }
            catch
            {
                return id;
            }
        }

        public string MoveNumberStringComma(string str, bool returns = false)
        {
            try
            {
                string strNumber = "";
                double dbeNumber = 0;

                dbeNumber = Convert.ToDouble(str);
                strNumber = Convert.ToString(dbeNumber);

                return strNumber;
            }
            catch (Exception)
            {
                return returns ? "0" : str;
            }
        }

        public static string GetMonthFullname(int month)
        {
            return DateTime.ParseExact("01" + (month > 9 ? month.ToString() : "0" + month) + "2000", "ddMMyyyy", new CultureInfo("th-TH")).ToString("MMMM");
        }

        public static string convertDate(string date) // แปลงเป็น ปี ค.ศ.
        {
            if (date.Length == 0)
            {
                return "";
            }

            DateTime dateTime = DateTime.Today;
            System.IFormatProvider formatd = new System.Globalization.CultureInfo("en-US");
            try
            {
                if (date.Trim().Split('/').Length >= 3)
                {
                    string format = "d/MM/yyyy";
                    if (date.Trim().Split('/')[0].Length > 1)
                        format = "dd/MM/yyyy";
                    dateTime = DateTime.ParseExact(date, format, new System.Globalization.CultureInfo("th-TH"));
                }
                else if (date.Trim().Split(' ').Length >= 3)
                {
                    string format = "d MMM yyyy";
                    if (date.Trim().Split(' ')[0].Length > 1)
                        format = "dd MMM yyyy";
                    dateTime = DateTime.ParseExact(date, format, new System.Globalization.CultureInfo("th-TH"));
                }
            }
            catch (Exception)
            {
            }
            return dateTime.ToString("d/MM/yyyy", formatd);
        }

        public DateTime ConvertToDate(string date, string strFormat)
        {
            DateTime dt = DateTime.Today;
            string formated = "dd/MM/yyyy";

            try
            {
                if (strFormat == Fm.DDMMYY)
                {
                    strDay = date.Substring(0, 2);
                    strMonth = date.Substring(2, 2);
                    strYear = (int.Parse("25" + date.Substring(4, 2)) - 543).ToString();

                    strDayMonthYear = strDay + "/" + strMonth + "/" + strYear;
                    dt = DateTime.ParseExact(strDayMonthYear, formated, YearEn);
                    return dt;
                }
                else
                {
                    return dt;
                }
            }
            catch
            {
                return dt;
            }
        }

        public static string GetDateNow() // เรียกใช้ วันที่ปัจจุบัน datePicker
        {
            System.IFormatProvider format = new System.Globalization.CultureInfo("th-TH");
            DateTime today = DateTime.Today;
            DateTime date1 = new DateTime(today.Year, today.Month, today.Day);

            return date1.ToString("dd MMM yyyy", format);
        }

        public void ClearPanels(GroupBox control)
        {
            foreach (ListBox listBox in control.Controls.OfType<ListBox>())
            {
                listBox.Items.Clear();
            }
            foreach (TextBox textBox in control.Controls.OfType<TextBox>())
            {
                textBox.Clear();
            }
            foreach (CheckedListBox listBox in control.Controls.OfType<CheckedListBox>())
            {
                listBox.Items.Clear();
            }
            foreach (ListView listView in control.Controls.OfType<ListView>())
            {
                listView.Items.Clear();
            }
            foreach (CheckBox checkBox in control.Controls.OfType<CheckBox>())
            {
                checkBox.Checked = false;
            }
            foreach (ComboBox comboBox in control.Controls.OfType<ComboBox>())
            {
                comboBox.SelectedIndex = 0;
            }
            foreach (DateTimePicker dtp in control.Controls.OfType<DateTimePicker>())
            {
                //dtp.Value = DateTime.Now;
            }
            foreach (PictureBox pb in control.Controls.OfType<PictureBox>())
            {
                pb.Image = null;
            }
        }

        public string SplitString(String Value, String Pattern, String Replacement)
        {
            try
            {
                String Patterns = @"" + Pattern;
                String Replacements = Replacement;
                Regex rgx = new Regex(Patterns);
                return rgx.Replace(Value, Replacements);
            }
            catch (Exception)
            {
                return "";
            }
        }

        //public static int ToInt(this string value)
        //{
        //    int tmp = 0;
        //    Int32.TryParse(value, out tmp);
        //    return tmp;
        //}

        //public static int ToInt(this object value)
        //{
        //    int tmp = 0;
        //    Int32.TryParse(value.ToString(), out tmp);
        //    return tmp;
        //}

        //public static Double ToDouble(this object value)
        //{
        //    Double tmp = 0.0;
        //    Double.TryParse(value.ToString(), out tmp);
        //    return tmp;
        //}

        //public static Decimal ToDecimal(this object value)
        //{
        //    Decimal tmp = 0;
        //    Decimal.TryParse(value.ToString(), out tmp);
        //    return tmp;
        //}

        //public static string ToDate(this object value, string format = "")
        //{
        //    if (value is DateTime)
        //    {
        //        DateTime date = (DateTime)value;
        //        if (string.IsNullOrEmpty(format))
        //            return date.ToString();
        //        else
        //            return date.ToString(format);
        //    }
        //    return string.Empty;
        //}

        //public static DateTime ToDateTime(this object value, string format = "dd/MM/yyyy HH:mm:ss")
        //{
        //    if (value is DateTime)
        //    {
        //        DateTime date = (DateTime)value;
        //        return date;
        //    }
        //    else if (value is string)
        //    {
        //        return DateTime.ParseExact(value.ToString(), format, new CultureInfo("en-US"));
        //    }
        //    return DateTime.MinValue;
        //}

        //public static DateTime ToDateTime(this object value, string format, CultureInfo cultureInfo)
        //{
        //    if (value is DateTime)
        //    {
        //        DateTime date = (DateTime)value;
        //        return date;
        //    }
        //    else if (value is string)
        //    {
        //        return DateTime.ParseExact(value.ToString(), format, cultureInfo);
        //    }
        //    return DateTime.MinValue;
        //}
        public void showGridViewFormatFromStores(DataTable dt, DataGridView dataGridView,
           string Col0Name, int Col0Width, Boolean Col0Show, DataGridViewContentAlignment Col0HeadStyle, DataGridViewContentAlignment Col0DefaultStyle
         , string Col1Name, int Col1Width, Boolean Col1Show, DataGridViewContentAlignment Col1HeadStyle, DataGridViewContentAlignment Col1DefaultStyle
         , string Col2Name, int Col2Width, Boolean Col2Show, DataGridViewContentAlignment Col2HeadStyle, DataGridViewContentAlignment Col2DefaultStyle
         , string Col3Name, int Col3Width, Boolean Col3Show, DataGridViewContentAlignment Col3HeadStyle, DataGridViewContentAlignment Col3DefaultStyle
         , string Col4Name, int Col4Width, Boolean Col4Show, DataGridViewContentAlignment Col4HeadStyle, DataGridViewContentAlignment Col4DefaultStyle
         , string Col5Name, int Col5Width, Boolean Col5Show, DataGridViewContentAlignment Col5HeadStyle, DataGridViewContentAlignment Col5DefaultStyle
         , string Col6Name, int Col6Width, Boolean Col6Show, DataGridViewContentAlignment Col6HeadStyle, DataGridViewContentAlignment Col6DefaultStyle
         , string Col7Name, int Col7Width, Boolean Col7Show, DataGridViewContentAlignment Col7HeadStyle, DataGridViewContentAlignment Col7DefaultStyle
         )
        {
            clsSearch clsSearch = new clsSearch();

            dataGridView.DataSource = dt;
            numberRow(dataGridView);

            dataGridView.Columns[0].Visible = Col0Show;
            dataGridView.Columns[0].Width = Col0Width;
            dataGridView.Columns[0].HeaderCell.Style.Alignment = Col0HeadStyle;
            dataGridView.Columns[0].DefaultCellStyle.Alignment = Col0DefaultStyle;

            dataGridView.Columns[1].Visible = Col1Show;
            dataGridView.Columns[1].HeaderText = Col1Name;
            dataGridView.Columns[1].Width = Col1Width;
            dataGridView.Columns[1].HeaderCell.Style.Alignment = Col1HeadStyle;
            dataGridView.Columns[1].DefaultCellStyle.Alignment = Col1DefaultStyle;

            dataGridView.Columns[2].Visible = Col2Show;
            dataGridView.Columns[2].HeaderText = Col2Name;
            dataGridView.Columns[2].Width = Col2Width;
            dataGridView.Columns[2].HeaderCell.Style.Alignment = Col2HeadStyle;
            dataGridView.Columns[2].DefaultCellStyle.Alignment = Col2DefaultStyle;

            dataGridView.Columns[3].Visible = Col3Show;
            dataGridView.Columns[3].HeaderText = Col3Name;
            dataGridView.Columns[3].Width = Col3Width;
            dataGridView.Columns[3].HeaderCell.Style.Alignment = Col3HeadStyle;
            dataGridView.Columns[3].DefaultCellStyle.Alignment = Col3DefaultStyle;

            dataGridView.Columns[4].Visible = Col4Show;
            dataGridView.Columns[4].HeaderText = Col4Name;
            dataGridView.Columns[4].Width = Col4Width;
            dataGridView.Columns[4].HeaderCell.Style.Alignment = Col4HeadStyle;
            dataGridView.Columns[4].DefaultCellStyle.Alignment = Col4DefaultStyle;

            dataGridView.Columns[5].Visible = Col5Show;
            dataGridView.Columns[5].HeaderText = Col5Name;
            dataGridView.Columns[5].Width = Col5Width;
            dataGridView.Columns[5].HeaderCell.Style.Alignment = Col5HeadStyle;
            dataGridView.Columns[5].DefaultCellStyle.Alignment = Col5DefaultStyle;

            dataGridView.Columns[6].Visible = Col6Show;
            dataGridView.Columns[6].HeaderText = Col6Name;
            dataGridView.Columns[6].Width = Col6Width;
            dataGridView.Columns[6].HeaderCell.Style.Alignment = Col6HeadStyle;
            dataGridView.Columns[6].DefaultCellStyle.Alignment = Col6DefaultStyle;

            dataGridView.Columns[7].Visible = Col6Show;
            dataGridView.Columns[7].HeaderText = Col6Name;
            dataGridView.Columns[7].Width = Col6Width;
            dataGridView.Columns[7].HeaderCell.Style.Alignment = Col6HeadStyle;
            dataGridView.Columns[7].DefaultCellStyle.Alignment = Col6DefaultStyle;
        }

        public void _showCheck()
        {
            MessageBox.Show("ชื่อหรือรหัสผ่านไม่ถูกต้อง กรุณาตรวจสอบใหม่อีกครั้ง", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void SetNewCode(TextBox txtBox)
        {
            txtBox.Text = "";
        }

        public string SubstringRight(String strValue, int intMaxLength)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                strValue = string.Empty;
            }
            else if (strValue.Length > intMaxLength)
            {
                strValue = strValue.Substring(strValue.Length - intMaxLength, intMaxLength);
            }
            return strValue;
        }

        public double GetSalary(string Month)
        {
            double salary = 0;
            string value = "";

            string[,] Parameter = new string[,]
            {
                {"DebtId", ""},
                {"DebtCode", ""},
                {"DebtList", Month},
                {"DebtYear", ""},
                {"DebtMonth", ""},
                {"DebtType", "1"},
                {"DebtAccountId", ""},
                {"DebtName", "iCONEXT | เงินเดือน"},
                {"DebtDetail", ""},
                {"DebtPrice", ""},
                {"DebtDueDate", ""},
                {"DebtPay", ""},
                {"DebtPayDate", ""},
                {"DebtPayLocation", "0"},
                {"DebtMoney", "0"},
                {"DebtStatus", "YP"},
                {"DebtReciept", ""},
                {"DebtLogo", "0"},
                {"DebtIsActive", "1"},
            };

            db.Get("Spr_S_TblSaveDebt", Parameter, out Error, out dt);

            if (dt.Rows.Count == 1)
            {
                value = dt.Rows[0]["DebtPricesType"].ToString();
                salary = Convert.ToDouble(value);
            }

            return salary;
        }

        public string GetConstant(string vale)
        {
            string result = "";
            try
            {
                switch (vale)
                {
                    case "S01635A0269B916":
                        result = Account.KTB;
                        break;

                    case "S01348585BBD164":
                        result = Account.KTB;
                        break;

                    case "S015070F383E136":
                        result = Account.CIMB;
                        break;

                    case "S01835A026F1BD0":
                        result = Account.TMB;
                        break;

                    case "S01623357CE0567":
                        result = Account.SCB;
                        break;

                    case "S01035A026F7031":
                        result = Account.KBANK;
                        break;

                    case "S01422F9E0E4A30":
                        result = Account.BBL;
                        break;

                    case "S01512B977E24C6":
                        result = Account.CIMB;
                        break;

                    default:
                        result = "";
                        break;
                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public void ShowImage(PictureBox pictureBox, string strType = null, string strValue = null)
        {
            try
            {
                Parameter = new string[,]{

                    {"Type", strType},
                    {"Value", strValue},
               };

                db.Get("Spr_F_GetPath", Parameter, out Error, out dt);

                if (dt.Rows.Count > 0)
                {
                    pictureBox.Image = Image.FromFile(dt.Rows[0]["Location"].ToString());
                }
            }
            catch (Exception)
            {

            }
        }

        public void SetComma(TextBox textBox)
        {
            try
            {
                double num = Convert.ToDouble(textBox.Text);
                textBox.Text = String.Format("{0:n}", num);
            }
            catch
            {
                textBox.Text = "";
            }
        }

        public int GetCharacterNumber(string WaterId)
        {
            int CharNumber = 0;

            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@MsWaterId", ""},
                    {"@MsWaterCode", WaterId},
                    {"@MsWaterNumber", ""},
                    {"@MsWaterBranchCode", ""},
                    {"@MsWaterBranchId", ""},
                    {"@MsWaterBranchSub", ""},
                    {"@MsWaterBranchName", ""},
                    {"@MsWaterBranchTel", ""},
                    {"@MsWaterLineId", ""},
                    {"@MsWaterLineOrder", ""},
                    {"@MsWaterRawText", ""},
                    {"@MsWaterRawValue", ""},
                    {"@MsWaterCost", ""},
                    {"@MsWaterDiscount", ""},
                    {"@MsWaterFee", ""},
                    {"@MsWaterVat", ""},
                    {"@MsWaterName", ""},
                    {"@MsWaterAddress1", ""},
                    {"@MsWaterAddress2", ""},
                    {"@MsWaterAddress3", ""},
                    {"@MsWaterVersion", ""},
                    {"@MsWaterStatus", "0"},
                };

                db.Get("Spr_S_TblMasterWater", Parameter, out Error, out dt);

                CharNumber = Convert.ToInt32(dt.Rows[0]["MsWaterCharacter"].ToString());

                return CharNumber;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void GetPaymentSubCode(string Bank, string Code, string Branch, out string paymentCode, out string paymentDetail, out string paymentDisplay)
        {
            if (Bank == Account.CIMBs)
            {
                string strTranfer = Code == "ORF" ?
                                    Branch == "94006Y" ? "%+%" :
                                    Branch == "94004Y" ? "%+%" :
                                    Branch == "94011Y" ? "%+%" :
                                    Branch == "94014Y" ? "%+%" : "%-%"
                                    : "";

                string[,] Parameter = new string[,]
                {
                {"@MsPaymentCode", Bank == "CIMB" ? "BST-022-1" : ""},
                {"@MsPaymentSubName", Code + strTranfer}
                };

                db.Get("Spr_F_GetPaymentSubCode", Parameter, out Error, out dt);
                paymentCode = dt.Rows[0]["Code"].ToString();
                paymentDetail = dt.Rows[0]["Detail"].ToString();
                paymentDisplay = dt.Rows[0]["Display"].ToString();
            }

            else if (Bank == Account.KBANKs)
            {
                string[,] Parameter = new string[,]
                {
                    {"@MsPaymentCode", Account.KBANK},
                    {"@MsPaymentSubName", Code}
                };

                db.Get("Spr_F_GetPaymentSubCode", Parameter, out Error, out dt);
                paymentCode = dt.Rows[0]["Code"].ToString();
                paymentDetail = dt.Rows[0]["Detail"].ToString();
                paymentDisplay = dt.Rows[0]["Display"].ToString();
            }

            else if (Bank == Account.TMBs)
            {
                string[,] Parameter = new string[,]
                {
                    {"@MsPaymentCode", Account.TMB},
                    {"@MsPaymentSubName", Code}
                };

                db.Get("Spr_F_GetPaymentSubCode", Parameter, out Error, out dt);
                paymentCode = dt.Rows[0]["Code"].ToString();
                paymentDetail = dt.Rows[0]["Detail"].ToString();
                paymentDisplay = dt.Rows[0]["Display"].ToString();
            }

            else if (Bank == Account.KTBs)
            {
                string[,] Parameter = new string[,]
                {
                    {"@MsPaymentCode", Account.KTB},
                    {"@MsPaymentSubName", Code}
                };

                db.Get("Spr_F_GetPaymentSubCode", Parameter, out Error, out dt);
                paymentCode = dt.Rows[0]["Code"].ToString();
                paymentDetail = dt.Rows[0]["Detail"].ToString();
                paymentDisplay = dt.Rows[0]["Display"].ToString();
            }

            else if (Bank == Account.SCBs)
            {
                string[,] Parameter = new string[,]
                {
                    {"@MsPaymentCode", Account.SCB},
                    {"@MsPaymentSubName", Code}
                };

                db.Get("Spr_F_GetPaymentSubCode", Parameter, out Error, out dt);
                paymentCode = dt.Rows[0]["Code"].ToString();
                paymentDetail = dt.Rows[0]["Detail"].ToString();
                paymentDisplay = dt.Rows[0]["Display"].ToString();
            }

            else
            {
                paymentCode = "";
                paymentDetail = "";
                paymentDisplay = "";
            }
        }

        public string GetBankPaymentCode(string vale)
        {
            string result = "";
            try
            {
                switch (vale)
                {
                    case "S01635A0269B916":
                        result = PaymentCode.KTB;
                        break;

                    case "S015070F383E136":
                        result = PaymentCode.CIMB;
                        break;

                    case "S01835A026F1BD0":
                        result = PaymentCode.TMB;
                        break;

                    case "S01035A026F7031":
                        result = PaymentCode.KBANK;
                        break;

                    case "S01422F9E0E4A30":
                        result = PaymentCode.BBL;
                        break;

                    case "S01348585BBD164":
                        result = PaymentCode.KTB;
                        break;

                    default:
                        result = "";
                        break;
                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }

        public string GetDetail(int line, string str)
        {
            string values = "";
            bool has = false;

            try
            {
                if (line == 1)
                {
                    has = str.Contains("\r");
                    values = has ? str.Substring(0, str.IndexOf("\r")) : str;
                }
                else if (line == 2)
                {
                    has = str.Contains("\n");
                    values = has ? str.Substring(str.IndexOf("\n") + 1) : "";
                }

                return values;
            }
            catch (Exception)
            {
                return values;
            }
        }

        public string GetCardNumber(string CardNo)
        {
            string values = "";

            try
            {
                if (CardNo != "")
                {
                    values = (CardNo.Replace(" ", "")).Replace("-", "");
                }

                return values;
            }
            catch (Exception)
            {
                return values;
            }
        }

        public string ShowResult(int Number)
        {
            string value = "";
            string strData = "";
            string strRow = "";
            string strNumber = Number.ToString();

            try
            {
                strData = clsSetting.ReadLanguageSetting() != "TH" ? ProgramConstant.lblResultTh : ProgramConstant.lblResultEn;
                strRow = clsSetting.ReadLanguageSetting() != "TH" ? ProgramConstant.lblRowTh : ProgramConstant.lblRowEn;

                strNumber = Number >= 1000 ? setComma(strNumber) : strNumber;

                value = strData + " " + strNumber + " " + strRow;
                return value;
            }
            catch (Exception)
            {
                return value;
            }
        }

        public string Substring(string Text, int Style, string Sign = "")
        {
            string Value = "";
            int Length = 0;
            int LastIndex = 0;

            try
            {
                Length = Text.Length;

                if (Style == 1)
                {
                    LastIndex = Text.LastIndexOf(Sign) + Sign.Length;
                    Value = Text.Substring(LastIndex, Length - LastIndex);
                }

                return Value;
            }
            catch (Exception)
            {
                return Value;
            }
        }

        public string GetTime(int format)
        {
            string times = "";

            try
            {
                if (format == 0)
                {
                    wait(1000);
                    times = DateTime.Now.ToString("ss");
                }
                if (format == 1)
                {
                    wait(1000);
                    times = DateTime.Now.ToString("HH:mm:ss");
                }

                return times;
            }
            catch (Exception)
            {
                return times;
            }
        }

        public void wait(int milliseconds)
        {
            var timer = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            timer.Interval = milliseconds;
            timer.Enabled = true;
            timer.Start();

            timer.Tick += (s, e) =>
            {
                timer.Enabled = false;
                timer.Stop();
            };

            while (timer.Enabled)
            {
                Application.DoEvents();
            }
        }

        public string formatTime(string time, int format)
        {
            string value = "";
            string strDay = "";
            string strMonth = "";
            string strYear = "";

            try
            {
                if (format == 1)
                {
                    string[] date = time.Split('-');

                    strDay = date[0];
                    strMonth = date[1];
                    strYear = date[2];

                    value = strDay + "/" + strMonth + "/" + "20" + strYear;
                }
                else if (format == 2)
                {
                    string[] date = time.Split('/');

                    strDay = date[0];
                    strMonth = date[1];
                    strYear = date[2];

                    value = strDay + "/" + strMonth + "/" + "20" + strYear;
                }

                return value;
            }
            catch
            {
                return time;
            }
        }

        public string selectedValue(ComboBox cbb)
        {
            string value = "";

            try
            {
                if (cbb.SelectedValue != null)
                {
                    if (cbb.SelectedValue.ToString() == "System.Data.DataRowView")
                    {
                        value = "";
                    }
                    else if (cbb.SelectedValue.ToString() == null)
                    {
                        value = "";
                    }
                    else
                    {
                        value = cbb.SelectedValue.ToString();
                    }
                }
                return value;
            }
            catch
            {
                return value;
            }
        }

        public bool CheckScanKeycode(string TextCode)
        {
            try
            {
                string StartChar = TextCode.Substring(0, 1);
                char CodeChar = char.Parse(StartChar);

                if (CodeChar >= 0 && CodeChar <= 127)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }

                return status;
            }
            catch
            {
                return status;
            }
        }

        public void GetBalance(string Date, out DataTable Balance)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Date", Date},
                    {"@Account", Setting.GetAccounts()},
                    {"@Money", Setting.GetWallet()},
                };

                db.Get(Store.FnGetBalance, Parameter, out Error, out dt);
                Balance = dt;

            }
            catch (Exception)
            {
                Balance = null;
            }
        }

        public bool IsCharacter(char chars, string type)
        {
            if (type == CharType.Tracking)
            {

                if (chars >= 97 && chars <= 122 ||
                    chars >= 65 && chars <= 90 ||
                    chars == 8 || chars == 13 ||
                    chars >= 48 && chars <= 57)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (type == CharType.Barcode)
            {

                if (chars >= 97 && chars <= 122 ||
                    chars >= 65 && chars <= 90 ||
                    chars == 8 || chars == 13 ||
                    chars >= 48 && chars <= 57)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (type == CharType.Receipt)
            {

                if (chars >= 97 && chars <= 122 ||
                    chars >= 65 && chars <= 90 ||
                    chars == 8 || chars == 13 || chars == 45 ||
                    chars >= 47 && chars <= 57)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public string getValue(string strTable, string strColumn, string strVal, string strShow)
        {
            clsImage Images = new clsImage();
            StoreConstant Store = new StoreConstant();

            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@Table", strTable},
                    {"@Column", strColumn},
                    {"@Value", strVal},
                    {"@Select", strShow}
                };

                db.Get("Store.FunctionGetValue", Parameter, out Error, out dt);

                return dt.Rows[0][strShow].ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string ShowDate(string Value)
        {
            try
            {
                if (Value != "")
                {
                    string Date = Convert.ToDateTime(Value).ToString("dd MMM yyyy");
                    return Date;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }

        public bool IsDefault(ComboBox cbb)
        {
            try
            {
                if (cbb.SelectedIndex == -1)
                {
                    return true;
                }
                else if (cbb.Text == "System.Data.DataRowView")
                {
                    return true;
                }
                else if (cbb.Text == ":: กรุณาเลือก ::")
                {
                    return true;
                }
                else if (cbb.SelectedValue.ToString() == "0")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;
            }
        }

        public string GetValue(DataTable dt, string Field, int Index = 0)
        {
            try
            {
                return dt.Rows[Index][Field].ToString();
            }
            catch
            {
                return "";
            }
        }

        public string SplitBarcode(string Codes)
        {
            try
            {
                if (!string.IsNullOrEmpty(Codes) && Codes.Contains("5102TH9104"))
                {
                    int IndexOf = Codes.IndexOf("5102TH9104");
                    string Value = Codes.Substring(0, IndexOf);

                    int Counts = Value.Count();
                    string NewCodes = Value.Substring((Value.Length - 20), 20);
                    return NewCodes;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
        }
    }
}