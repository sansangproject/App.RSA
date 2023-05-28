using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;
using SANSANG.Utilites.App.Model;

namespace SANSANG.Class
{
    public partial class clsInsert
    {
        public clsMessage Mess = new clsMessage();
        public clsDate Date = new clsDate();
        public DataTable dt = new DataTable();
        public clsFunction Function = new clsFunction();
        public dbConnection db = new dbConnection();
        public clsConvert Converts = new clsConvert();
        public TableConstant Table = new TableConstant();
        public OperationConstant Operation = new OperationConstant();
        public clsLog Log = new clsLog();
        public StoreConstant Store = new StoreConstant();
        public DateTime dTime;
        private string Error = "";
        string[,] Parameter = new string[,] { };

        public bool Add(string AppCodes = "", string AppNames = "", string UserId = "", string strStore = "", string[,] Parameter = null, string Codes = "", string Details = "")
        {
            string Operation = "I";
            string Code = Codes;
            string Detail = Details;
            string User = UserId;
            string AppCode = AppCodes;
            string AppName = AppNames;
            string Store = strStore;

            try
            {
                Mess.MessageConfirmation(Operation, Code, Detail);

                using (var Popup = new FrmMessagesBox(Mess.strOperation, Mess.strMes, "YES", "NO", Mess.strImage))
                {
                    var result = Popup.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Popup.Close();

                        db.Operations(Store, Parameter, out Error);

                        if (Error == null)
                        {
                            Mess.MessageResult(Operation, "C", Error);
                            Log.WriteLogData(AppCode, AppName, UserId, "Insert");
                            return true;
                        }
                        else
                        {
                            Mess.MessageResult(Operation, "E", Error);
                            Log.WriteLogData(AppCode, AppName, UserId, "Insert");
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return false;
            }
        }

        public void Expenses(ExpensesModel Model)
        {
            try
            {
                string Code = Function.GetCodes(Table.ExpenseId, "", "Generated");
                string List = Date.GetDate(dt: dTime, Format: 5);
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", Code},
                    {"@Status", "1000"},
                    {"@User", Model.UserId},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.InsertAbbr},
                    {"@List", List},
                    {"@MoneyId", Model.MoneyId},
                    {"@PaymentId", Model.CategoryId},
                    {"@PaymentSubId", Model.ItemId},
                    {"@IsDebit", Model.IsDebit},
                    {"@Item", Model.Item},
                    {"@Detail", ""},
                    {"@Amount", Model.Amount},
                    {"@UnitId", "1213"},
                    {"@Unit", "1"},
                    {"@Date", Model.Date},
                    {"@Receipt", Model.Receipt},
                };

                db.Operations(Store.ManageExpense, Parameter, out Error);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(Model.AppCode, Model.AppName, Model.UserId, ex.Message);
            }
        }

        public void TrackPost(List<TrackModel> Data, string TrackingId)
        {
            try
            {
                foreach (var Item in Data)
                {
                    string Code = Function.GetCodes(Table.TrackPostId, "", "Generated");

                    Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", Code},
                        {"@Status", Converts.NullToString(Item.DeliveryStatus)},
                        {"@User", "1000"},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.InsertAbbr},
                        {"@TrackingId", TrackingId},
                        {"@Barcode", Item.Barcode},
                        {"@Number", Item.Status},
                        {"@Date", Converts.StringToDateTime(Item.StatusDate)},
                        {"@Location", Item.Location},
                        {"@Postcode", Item.Postcode},
                        {"@Description", Converts.NullToString(Item.DeliveryDescription)},
                        {"@DeliveryDate", Converts.StringToDateTime(Item.DeliveryDate)},
                        {"@Receiver", Converts.NullToString(Item.ReceiverName)},
                        {"@SignatureId", Converts.NullToString(Item.Signature) != "" ? Signature(Item.Signature) : "0"},
                    };

                    db.Operations(Store.ManageTrackPost, Parameter, out Error);
                }
            }
            catch (Exception)
            {

            }
        }

        public string Signature(string Url)
        {
            try
            {
                string Code = Function.GetCodes(Table.SignatureId, "", "Generated");
                string Images = Function.GetImageAsBase64Url(Url);

                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", Code},
                    {"@Url", Url},
                    {"@Image", Images},
                    {"@Operation", Operation.InsertAbbr},
                };

                db.Get(Store.ManageSignatures, Parameter, out Error, out dt);
                return dt.Rows[0]["Id"].ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }




































































        public void TblLogReport(string LogReportCode, string LogReportName, string LogReportCondition, string LogReportProgram, string LogReportSubProgram, string LogReportFileName, string LogReportFileLocation, string LogReportPrintBy)
        {
            string[,] Parameter = new string[,]
                {
                    {"@LogReportCode", LogReportCode},
                    {"@LogReportName", LogReportName},
                    {"@LogReportCondition", LogReportCondition},
                    {"@LogReportProgram", LogReportProgram },
                    {"@LogReportSubProgram", LogReportSubProgram},
                    {"@LogReportFileName", LogReportFileName},
                    {"@LogReportFileLocation", LogReportFileLocation },
                    {"@LogReportPrintBy", LogReportPrintBy},
                };

            db.Operations("Spr_I_TblLogReport", Parameter, out Error);
        }

        

        public void TblSaveMoney(SaveMoneyModels Model)
        {
            try
            {
                string strSign = "";
                string strDetail = "";
                string strCode = "";
                string strAmout = "";

                if (Model.CoinUsed == true)
                {
                    strSign = "-";
                    strDetail = "นำเงินไปใช้\r\n";
                }

                strCode = Function.GetCodes("121", "", "Generated");
                strAmout = Convert.ToString(Convert.ToDouble(Model.CoinValue) * Convert.ToInt32(strSign + Model.CoinValue));
                string[,] Parameter = new string[,]
                {
                    {"@MoneyCode", strCode},
                    {"@MsCoinCode", Model.CoinType},
                    {"@MoneyValue", Model.CoinName},
                    {"@MoneyNumber", strSign + Model.CoinValue},
                    {"@MoneyAmount",  strAmout},
                    {"@MoneyDate",  Model.CoinDate.ToString("dd/MM/yyyy", CultureInfo.CreateSpecificCulture("en-EN")),},
                    {"@MoneyDetail", strDetail + Model.CoinDetail},
                    {"@MoneyStatus", "Y"},
                    {"@User", Model.UserId},
                };

                db.Operations("Spr_I_TblSaveMoney", Parameter, out Error);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(Model.AppCode, Model.AppName, Model.UserId, ex.Message);
            }
        }

        public void _usre(
                string strUSER_NAME,
                string strUSER_SURNAME,
                string strUSER_PASSWORD,
                string strUSER_TYPE,
                string strUSER_STATUS,
                string strUSER_ID,
                string strBY)
        {
            try
            {
                var answer = MessageBox.Show(" " + strUSER_ID + " ", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_USER] ");
                    strSql.AppendLine("            ([USER_ID] ");
                    strSql.AppendLine("            ,[USER_NAME] ");
                    strSql.AppendLine("            ,[USER_SURNAME] ");
                    strSql.AppendLine("            ,[USER_PASSWORD] ");
                    strSql.AppendLine("            ,[USER_TYPE] ");
                    strSql.AppendLine("            ,[USER_STATUS] ");
                    strSql.AppendLine("            ,[USER_CREATE_BY] ");
                    strSql.AppendLine("            ,[USER_CREATE_DATE] ");
                    strSql.AppendLine("            ,[USER_UPDATE_BY] ");
                    strSql.AppendLine("            ,[USER_UPDATE_DATE]) ");
                    strSql.AppendLine("      VALUES ");
                    strSql.AppendLine("            (@USER_ID ");
                    strSql.AppendLine("            ,@USER_NAME ");
                    strSql.AppendLine("            ,@USER_SURNAME ");
                    strSql.AppendLine("            ,@USER_PASSWORD ");
                    strSql.AppendLine("            ,@USER_TYPE ");
                    strSql.AppendLine("            ,@USER_STATUS ");
                    strSql.AppendLine("            ,@USER_CREATE_BY ");
                    strSql.AppendLine("            ,@USER_CREATE_DATE ");
                    strSql.AppendLine("            ,@USER_UPDATE_BY ");
                    strSql.AppendLine("            ,@USER_UPDATE_DATE) ");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@USER_ID", strUSER_ID));
                    runSql.Parameters.Add(new SqlParameter("@USER_NAME", strUSER_NAME));
                    runSql.Parameters.Add(new SqlParameter("@USER_SURNAME", strUSER_SURNAME));
                    runSql.Parameters.Add(new SqlParameter("@USER_PASSWORD", strUSER_PASSWORD));
                    runSql.Parameters.Add(new SqlParameter("@USER_TYPE", strUSER_TYPE));
                    runSql.Parameters.Add(new SqlParameter("@USER_STATUS", strUSER_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@USER_CREATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@USER_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@USER_UPDATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@USER_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_USER-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _menu(
                string strMENU_ID,
                string strMENU_NAME_EN,
                string strMENU_NAME_TH,
                string strMENU_DISPLAY,
                string strMENU_TYPE,
                string strMENU_MAIN,
                string strMENU_SUB,
                string strMENU_STATUS,
                string strBY)
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strMENU_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_MENU] ");
                    strSql.AppendLine("            ([MENU_ID] ");
                    strSql.AppendLine("            ,[MENU_NAME_EN] ");
                    strSql.AppendLine("            ,[MENU_NAME_TH] ");
                    strSql.AppendLine("            ,[MENU_DISPLAY] ");
                    strSql.AppendLine("            ,[MENU_TYPE] ");
                    strSql.AppendLine("            ,[MENU_MAIN] ");
                    strSql.AppendLine("            ,[MENU_SUB] ");
                    strSql.AppendLine("            ,[MENU_STATUS] ");
                    strSql.AppendLine("            ,[MENU_CREATE_DATE] ");
                    strSql.AppendLine("            ,[MENU_CREATE_BY] ");
                    strSql.AppendLine("            ,[MENU_UPDATE_DATE] ");
                    strSql.AppendLine("            ,[MENU_UPDATE_BY]) ");
                    strSql.AppendLine("      VALUES ");
                    strSql.AppendLine("            (@MENU_ID ");
                    strSql.AppendLine("            ,@MENU_NAME_EN ");
                    strSql.AppendLine("            ,@MENU_NAME_TH ");
                    strSql.AppendLine("            ,@MENU_DISPLAY ");
                    strSql.AppendLine("            ,@MENU_TYPE ");
                    strSql.AppendLine("            ,@MENU_MAIN ");
                    strSql.AppendLine("            ,@MENU_SUB ");
                    strSql.AppendLine("            ,@MENU_STATUS ");
                    strSql.AppendLine("            ,@MENU_CREATE_DATE ");
                    strSql.AppendLine("            ,@MENU_CREATE_BY ");
                    strSql.AppendLine("            ,@MENU_UPDATE_DATE ");
                    strSql.AppendLine("            ,@MENU_UPDATE_BY)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@MENU_ID", strMENU_ID));
                    runSql.Parameters.Add(new SqlParameter("@MENU_NAME_EN", strMENU_NAME_EN));
                    runSql.Parameters.Add(new SqlParameter("@MENU_NAME_TH", strMENU_NAME_TH));
                    runSql.Parameters.Add(new SqlParameter("@MENU_DISPLAY", strMENU_DISPLAY));
                    runSql.Parameters.Add(new SqlParameter("@MENU_TYPE", strMENU_TYPE));
                    runSql.Parameters.Add(new SqlParameter("@MENU_MAIN", strMENU_MAIN));
                    runSql.Parameters.Add(new SqlParameter("@MENU_SUB", strMENU_SUB));
                    runSql.Parameters.Add(new SqlParameter("@MENU_STATUS", strMENU_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@MENU_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@MENU_CREATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@MENU_UPDATE_DATE", Function._updatDate()));
                    runSql.Parameters.Add(new SqlParameter("@MENU_UPDATE_BY", strBY));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_MENU-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _pay(
                string strPAY_ID,
                string strPAY_NAME,
                string strPAY_STATUS,
                string strBY)
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strPAY_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_PAY] ");
                    strSql.AppendLine("            ([PAY_ID] ");
                    strSql.AppendLine("            ,[PAY_NAME] ");
                    strSql.AppendLine("            ,[PAY_STATUS] ");
                    strSql.AppendLine("            ,[PAY_CREATE_BY] ");
                    strSql.AppendLine("            ,[PAY_CREATE_DATE] ");
                    strSql.AppendLine("            ,[PAY_UPDATE_BY] ");
                    strSql.AppendLine("            ,[PAY_UPDATE_DATE]) ");
                    strSql.AppendLine("      VALUES ");
                    strSql.AppendLine("            (@PAY_ID ");
                    strSql.AppendLine("            ,@PAY_NAME ");
                    strSql.AppendLine("            ,@PAY_STATUS ");
                    strSql.AppendLine("            ,@PAY_CREATE_BY ");
                    strSql.AppendLine("            ,@PAY_CREATE_DATE ");
                    strSql.AppendLine("            ,@PAY_UPDATE_BY ");
                    strSql.AppendLine("            ,@PAY_UPDATE_DATE) ");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@PAY_ID", strPAY_ID));
                    runSql.Parameters.Add(new SqlParameter("@PAY_NAME", strPAY_NAME));
                    runSql.Parameters.Add(new SqlParameter("@PAY_STATUS", strPAY_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@PAY_CREATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@PAY_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@PAY_UPDATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@PAY_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_PAY-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _paySub(
                string strPAYSUB_ID,
                string strPAYSUB_NAME,
                string strPAYSUB_DETAIL,
                string strPAYSUB_PAY,
                string strPAYSUB_TYPE,
                string strPAYSUB_STATUS,
                string strBY)
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strPAYSUB_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_PAYSUB] ");
                    strSql.AppendLine("            ([PAYSUB_ID] ");
                    strSql.AppendLine("            ,[PAYSUB_NAME] ");
                    strSql.AppendLine("            ,[PAYSUB_DETAIL] ");
                    strSql.AppendLine("            ,[PAYSUB_PAY] ");
                    strSql.AppendLine("            ,[PAYSUB_TYPE] ");
                    strSql.AppendLine("            ,[PAYSUB_STATUS] ");
                    strSql.AppendLine("            ,[PAYSUB_CREATE_BY] ");
                    strSql.AppendLine("            ,[PAYSUB_CREATE_DATE] ");
                    strSql.AppendLine("            ,[PAYSUB_UPDATE_BY] ");
                    strSql.AppendLine("            ,[PAYSUB_UPDATE_DATE]) ");
                    strSql.AppendLine("      VALUES ");
                    strSql.AppendLine("            (@PAYSUB_ID ");
                    strSql.AppendLine("            ,@PAYSUB_NAME ");
                    strSql.AppendLine("            ,@PAYSUB_DETAIL ");
                    strSql.AppendLine("            ,@PAYSUB_PAY ");
                    strSql.AppendLine("            ,@PAYSUB_TYPE ");
                    strSql.AppendLine("            ,@PAYSUB_STATUS ");
                    strSql.AppendLine("            ,@PAYSUB_CREATE_BY ");
                    strSql.AppendLine("            ,@PAYSUB_CREATE_DATE ");
                    strSql.AppendLine("            ,@PAYSUB_UPDATE_BY ");
                    strSql.AppendLine("            ,@PAYSUB_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@PAYSUB_ID", strPAYSUB_ID));
                    runSql.Parameters.Add(new SqlParameter("@PAYSUB_NAME", strPAYSUB_NAME));
                    runSql.Parameters.Add(new SqlParameter("@PAYSUB_DETAIL", strPAYSUB_DETAIL));
                    runSql.Parameters.Add(new SqlParameter("@PAYSUB_PAY", strPAYSUB_PAY));
                    runSql.Parameters.Add(new SqlParameter("@PAYSUB_TYPE", strPAYSUB_TYPE));
                    runSql.Parameters.Add(new SqlParameter("@PAYSUB_STATUS", strPAYSUB_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@PAYSUB_CREATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@PAYSUB_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@PAYSUB_UPDATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@PAYSUB_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_PAYSUB-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _unit(
                string strUNIT_ID,
                string strUNIT_NAME,
                string strUNIT_STATUS,
                string strBY)
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strUNIT_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_UNIT] ");
                    strSql.AppendLine("            ([UNIT_ID] ");
                    strSql.AppendLine("            ,[UNIT_NAME] ");
                    strSql.AppendLine("            ,[UNIT_STATUS] ");
                    strSql.AppendLine("            ,[UNIT_CREATE_BY] ");
                    strSql.AppendLine("            ,[UNIT_CREATE_DATE] ");
                    strSql.AppendLine("            ,[UNIT_UPDATE_BY] ");
                    strSql.AppendLine("            ,[UNIT_UPDATE_DATE]) ");
                    strSql.AppendLine("      VALUES ");
                    strSql.AppendLine("            (@UNIT_ID");
                    strSql.AppendLine("            ,@UNIT_NAME");
                    strSql.AppendLine("            ,@UNIT_STATUS");
                    strSql.AppendLine("            ,@UNIT_CREATE_BY");
                    strSql.AppendLine("            ,@UNIT_CREATE_DATE");
                    strSql.AppendLine("            ,@UNIT_UPDATE_BY");
                    strSql.AppendLine("            ,@UNIT_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@UNIT_ID", strUNIT_ID));
                    runSql.Parameters.Add(new SqlParameter("@UNIT_NAME", strUNIT_NAME));
                    runSql.Parameters.Add(new SqlParameter("@UNIT_STATUS", strUNIT_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@UNIT_CREATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@UNIT_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@UNIT_UPDATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@UNIT_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_UNIT-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _account(
                string strACCOUNT_ID,
                string strACCOUNT_NUMBER,
                string strACCOUNT_NAME,
                string strACCOUNT_BANK,
                string strACCOUNT_BRANCH,
                string strACCOUNT_STATUS,
                string strBY,
                string strACCOUNT_FILE
            )
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strACCOUNT_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_ACCOUNT]");
                    strSql.AppendLine("            ([ACCOUNT_ID]");
                    strSql.AppendLine("            ,[ACCOUNT_NUMBER]");
                    strSql.AppendLine("            ,[ACCOUNT_NAME]");
                    strSql.AppendLine("            ,[ACCOUNT_BANK]");
                    strSql.AppendLine("            ,[ACCOUNT_BRANCH]");
                    strSql.AppendLine("            ,[ACCOUNT_FILE]");
                    strSql.AppendLine("            ,[ACCOUNT_STATUS]");
                    strSql.AppendLine("            ,[ACCOUNT_CREATE_BY]");
                    strSql.AppendLine("            ,[ACCOUNT_CREATE_DATE]");
                    strSql.AppendLine("            ,[ACCOUNT_UPDATE_BY]");
                    strSql.AppendLine("            ,[ACCOUNT_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES ");
                    strSql.AppendLine("            (@ACCOUNT_ID");
                    strSql.AppendLine("            ,@ACCOUNT_NUMBER");
                    strSql.AppendLine("            ,@ACCOUNT_NAME");
                    strSql.AppendLine("            ,@ACCOUNT_BANK");
                    strSql.AppendLine("            ,@ACCOUNT_BRANCH");
                    strSql.AppendLine("            ,@ACCOUNT_FILE");
                    strSql.AppendLine("            ,@ACCOUNT_STATUS");
                    strSql.AppendLine("            ,@ACCOUNT_CREATE_BY");
                    strSql.AppendLine("            ,@ACCOUNT_CREATE_DATE");
                    strSql.AppendLine("            ,@ACCOUNT_UPDATE_BY");
                    strSql.AppendLine("            ,@ACCOUNT_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@ACCOUNT_ID", strACCOUNT_ID));
                    runSql.Parameters.Add(new SqlParameter("@ACCOUNT_NUMBER", strACCOUNT_NUMBER));
                    runSql.Parameters.Add(new SqlParameter("@ACCOUNT_NAME", strACCOUNT_NAME));
                    runSql.Parameters.Add(new SqlParameter("@ACCOUNT_BANK", strACCOUNT_BANK));
                    runSql.Parameters.Add(new SqlParameter("@ACCOUNT_BRANCH", strACCOUNT_BRANCH));
                    runSql.Parameters.Add(new SqlParameter("@ACCOUNT_FILE", strACCOUNT_FILE));
                    runSql.Parameters.Add(new SqlParameter("@ACCOUNT_STATUS", strACCOUNT_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@ACCOUNT_CREATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@ACCOUNT_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@ACCOUNT_UPDATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@ACCOUNT_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_ACCOUNT-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _bank(
                string strBANK_ID,
                string strBANK_NAME_TH,
                string strBANK_NAME_EN,
                string strBANK_ADDRESS,
                string strBANK_STATUS,
                string strBY)
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strBANK_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_BANK]");
                    strSql.AppendLine("            ([BANK_ID]");
                    strSql.AppendLine("            ,[BANK_NAME_TH]");
                    strSql.AppendLine("            ,[BANK_NAME_EN]");
                    strSql.AppendLine("            ,[BANK_ADDRESS]");
                    strSql.AppendLine("            ,[BANK_STATUS]");
                    strSql.AppendLine("            ,[BANK_CREATE_BY]");
                    strSql.AppendLine("            ,[BANK_CREATE_DATE]");
                    strSql.AppendLine("            ,[BANK_UPDATE_BY]");
                    strSql.AppendLine("            ,[BANK_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@BANK_ID");
                    strSql.AppendLine("            ,@BANK_NAME_TH");
                    strSql.AppendLine("            ,@BANK_NAME_EN");
                    strSql.AppendLine("            ,@BANK_ADDRESS");
                    strSql.AppendLine("            ,@BANK_STATUS");
                    strSql.AppendLine("            ,@BANK_CREATE_BY");
                    strSql.AppendLine("            ,@BANK_CREATE_DATE");
                    strSql.AppendLine("            ,@BANK_UPDATE_BY");
                    strSql.AppendLine("            ,@BANK_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@BANK_ID", strBANK_ID));
                    runSql.Parameters.Add(new SqlParameter("@BANK_NAME_TH", strBANK_NAME_TH));
                    runSql.Parameters.Add(new SqlParameter("@BANK_NAME_EN", strBANK_NAME_EN));
                    runSql.Parameters.Add(new SqlParameter("@BANK_ADDRESS", strBANK_ADDRESS));
                    runSql.Parameters.Add(new SqlParameter("@BANK_STATUS", strBANK_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@BANK_CREATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@BANK_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@BANK_UPDATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@BANK_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_BANK-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _branch(
                string strBRANCH_ID,
                string strBRANCH_NAME_TH,
                string strBRANCH_NAME_EN,
                string strBRANCH_ADDRESS,
                string strBRANCH_STATUS,
                string strBRANCH_BANK,
                string strBY)
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strBRANCH_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_BRANCH]");
                    strSql.AppendLine("            ([BRANCH_ID]");
                    strSql.AppendLine("            ,[BRANCH_NAME_TH]");
                    strSql.AppendLine("            ,[BRANCH_NAME_EN]");
                    strSql.AppendLine("            ,[BRANCH_ADDRESS]");
                    strSql.AppendLine("            ,[BRANCH_STATUS]");
                    strSql.AppendLine("            ,[BRANCH_BANK]");
                    strSql.AppendLine("            ,[BRANCH_CREATE_BY]");
                    strSql.AppendLine("            ,[BRANCH_CREATE_DATE]");
                    strSql.AppendLine("            ,[BRANCH_UPDATE_BY]");
                    strSql.AppendLine("            ,[BRANCH_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@BRANCH_ID");
                    strSql.AppendLine("            ,@BRANCH_NAME_TH");
                    strSql.AppendLine("            ,@BRANCH_NAME_EN");
                    strSql.AppendLine("            ,@BRANCH_ADDRESS");
                    strSql.AppendLine("            ,@BRANCH_STATUS");
                    strSql.AppendLine("            ,@BRANCH_BANK");
                    strSql.AppendLine("            ,@BRANCH_CREATE_BY");
                    strSql.AppendLine("            ,@BRANCH_CREATE_DATE");
                    strSql.AppendLine("            ,@BRANCH_UPDATE_BY");
                    strSql.AppendLine("            ,@BRANCH_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@BRANCH_ID", strBRANCH_ID));
                    runSql.Parameters.Add(new SqlParameter("@BRANCH_NAME_TH", strBRANCH_NAME_TH));
                    runSql.Parameters.Add(new SqlParameter("@BRANCH_NAME_EN", strBRANCH_NAME_EN));
                    runSql.Parameters.Add(new SqlParameter("@BRANCH_ADDRESS", strBRANCH_ADDRESS));
                    runSql.Parameters.Add(new SqlParameter("@BRANCH_STATUS", strBRANCH_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@BRANCH_BANK", strBRANCH_BANK));
                    runSql.Parameters.Add(new SqlParameter("@BRANCH_CREATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@BRANCH_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@BRANCH_UPDATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@BRANCH_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_BRANCH-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _address(
                string strADDRESS_ID,
                string strADDRESS_NUMBER,
                string strADDRESS_MOO,
                string strADDRESS_BUILDING,
                string strADDRESS_SOI,
                string strADDRESS_ROAD,
                int strADDRESS_DISTRICT,
                int strADDRESS_AMPHUR,
                int strADDRESS_PROVINCE,
                int strADDRESS_GEO,
                string strADDRESS_POSTCODE,
                string strADDRESS_PHONE,
                string strADDRESS_STATUS,
                string strBY)
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strADDRESS_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_ADDRESS]");
                    strSql.AppendLine("            ([ADDRESS_ID]");
                    strSql.AppendLine("            ,[ADDRESS_NUMBER]");
                    strSql.AppendLine("            ,[ADDRESS_MOO]");
                    strSql.AppendLine("            ,[ADDRESS_BUILDING]");
                    strSql.AppendLine("            ,[ADDRESS_SOI]");
                    strSql.AppendLine("            ,[ADDRESS_ROAD]");
                    strSql.AppendLine("            ,[ADDRESS_DISTRICT]");
                    strSql.AppendLine("            ,[ADDRESS_AMPHUR]");
                    strSql.AppendLine("            ,[ADDRESS_PROVINCE]");
                    strSql.AppendLine("            ,[ADDRESS_GEO]");
                    strSql.AppendLine("            ,[ADDRESS_POSTCODE]");
                    strSql.AppendLine("            ,[ADDRESS_PHONE]");
                    strSql.AppendLine("            ,[ADDRESS_STATUS]");
                    strSql.AppendLine("            ,[ADDRESS_CREATE_BY]");
                    strSql.AppendLine("            ,[ADDRESS_CREATE_DATE]");
                    strSql.AppendLine("            ,[ADDRESS_UPDATE_BY]");
                    strSql.AppendLine("            ,[ADDRESS_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@ADDRESS_ID");
                    strSql.AppendLine("            ,@ADDRESS_NUMBER");
                    strSql.AppendLine("            ,@ADDRESS_MOO");
                    strSql.AppendLine("            ,@ADDRESS_BUILDING");
                    strSql.AppendLine("            ,@ADDRESS_SOI ");
                    strSql.AppendLine("            ,@ADDRESS_ROAD ");
                    strSql.AppendLine("            ,@ADDRESS_DISTRICT ");
                    strSql.AppendLine("            ,@ADDRESS_AMPHUR ");
                    strSql.AppendLine("            ,@ADDRESS_PROVINCE ");
                    strSql.AppendLine("            ,@ADDRESS_GEO ");
                    strSql.AppendLine("            ,@ADDRESS_POSTCODE");
                    strSql.AppendLine("            ,@ADDRESS_PHONE ");
                    strSql.AppendLine("            ,@ADDRESS_STATUS ");
                    strSql.AppendLine("            ,@ADDRESS_CREATE_BY ");
                    strSql.AppendLine("            ,@ADDRESS_CREATE_DATE ");
                    strSql.AppendLine("            ,@ADDRESS_UPDATE_BY");
                    strSql.AppendLine("            ,@ADDRESS_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_ID", strADDRESS_ID));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_NUMBER", strADDRESS_NUMBER));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_MOO", strADDRESS_MOO));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_BUILDING", strADDRESS_BUILDING));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_SOI", strADDRESS_SOI));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_ROAD", strADDRESS_ROAD));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_DISTRICT", Convert.ToInt32(strADDRESS_DISTRICT)));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_AMPHUR", Convert.ToInt32(strADDRESS_AMPHUR)));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_PROVINCE", Convert.ToInt32(strADDRESS_PROVINCE)));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_GEO", Convert.ToInt32(strADDRESS_GEO)));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_POSTCODE", strADDRESS_POSTCODE));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_PHONE", strADDRESS_PHONE));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_STATUS", strADDRESS_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_CREATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_UPDATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@ADDRESS_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_ADDRESS-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _money(
                string strMsMoneyCode,
                string strMsMoneyName,
                string strMsMoneyName_EN,
                string strMONEY_STATUS,
                string strBY)
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strMsMoneyCode + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [Tbl_Master_Money]");
                    strSql.AppendLine("            ([MsMoneyCode]");
                    strSql.AppendLine("            ,[MsMoneyName]");
                    strSql.AppendLine("            ,[MsMoneyName_EN]");
                    strSql.AppendLine("            ,[MONEY_STATUS]");
                    strSql.AppendLine("            ,[MONEY_CREATE_BY]");
                    strSql.AppendLine("            ,[MONEY_CREATE_DATE]");
                    strSql.AppendLine("            ,[MONEY_UPDATE_BY]");
                    strSql.AppendLine("            ,[MONEY_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@MsMoneyCode");
                    strSql.AppendLine("            ,@MsMoneyName");
                    strSql.AppendLine("            ,@MsMoneyName_EN");
                    strSql.AppendLine("            ,@MONEY_STATUS");
                    strSql.AppendLine("            ,@MONEY_CREATE_BY");
                    strSql.AppendLine("            ,@MONEY_CREATE_DATE");
                    strSql.AppendLine("            ,@MONEY_UPDATE_BY");
                    strSql.AppendLine("            ,@MONEY_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@MsMoneyCode", strMsMoneyCode));
                    runSql.Parameters.Add(new SqlParameter("@MsMoneyName", strMsMoneyName));
                    runSql.Parameters.Add(new SqlParameter("@MsMoneyName_EN", strMsMoneyName_EN));
                    runSql.Parameters.Add(new SqlParameter("@MONEY_STATUS", strMONEY_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@MONEY_CREATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@MONEY_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@MONEY_UPDATE_BY", strBY));
                    runSql.Parameters.Add(new SqlParameter("@MONEY_UPDATE_DATE", Function._updatDate()));
                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_Tbl_Master_Money-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _productType(
                string strPRODUCT_TYPE_ID,
                string strPRODUCT_TYPE_NAME,
                string strPRODUCT_TYPE_STATUS,
                string strID
            )
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strPRODUCT_TYPE_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_PRODUCT_TYPE]");
                    strSql.AppendLine("            ([PRODUCT_TYPE_ID]");
                    strSql.AppendLine("            ,[PRODUCT_TYPE_NAME]");
                    strSql.AppendLine("            ,[PRODUCT_TYPE_STATUS]");
                    strSql.AppendLine("            ,[PRODUCT_TYPE_CREATE_BY]");
                    strSql.AppendLine("            ,[PRODUCT_TYPE_CREATE_DATE]");
                    strSql.AppendLine("            ,[PRODUCT_TYPE_UPDATE_BY]");
                    strSql.AppendLine("            ,[PRODUCT_TYPE_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@PRODUCT_TYPE_ID");
                    strSql.AppendLine("            ,@PRODUCT_TYPE_NAME");
                    strSql.AppendLine("            ,@PRODUCT_TYPE_STATUS");
                    strSql.AppendLine("            ,@PRODUCT_TYPE_CREATE_BY");
                    strSql.AppendLine("            ,@PRODUCT_TYPE_CREATE_DATE");
                    strSql.AppendLine("            ,@PRODUCT_TYPE_UPDATE_BY");
                    strSql.AppendLine("            ,@PRODUCT_TYPE_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@PRODUCT_TYPE_ID", strPRODUCT_TYPE_ID));
                    runSql.Parameters.Add(new SqlParameter("@PRODUCT_TYPE_NAME", strPRODUCT_TYPE_NAME));
                    runSql.Parameters.Add(new SqlParameter("@PRODUCT_TYPE_STATUS", strPRODUCT_TYPE_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@PRODUCT_TYPE_CREATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@PRODUCT_TYPE_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@PRODUCT_TYPE_UPDATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@PRODUCT_TYPE_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_PRODUCT_TYPE-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _productStatus(
                string strPRODUCT_STATUS_ID,
                string strPRODUCT_STATUS_NAME,
                string strPRODUCT_STATUS_STATUS,
                string strID
            )
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strPRODUCT_STATUS_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_PRODUCT_STATUS]");
                    strSql.AppendLine("            ([PRODUCT_STATUS_ID]");
                    strSql.AppendLine("            ,[PRODUCT_STATUS_NAME]");
                    strSql.AppendLine("            ,[PRODUCT_STATUS_STATUS]");
                    strSql.AppendLine("            ,[PRODUCT_STATUS_CREATE_BY]");
                    strSql.AppendLine("            ,[PRODUCT_STATUS_CREATE_DATE]");
                    strSql.AppendLine("            ,[PRODUCT_STATUS_UPDATE_BY]");
                    strSql.AppendLine("            ,[PRODUCT_STATUS_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@PRODUCT_STATUS_ID");
                    strSql.AppendLine("            ,@PRODUCT_STATUS_NAME");
                    strSql.AppendLine("            ,@PRODUCT_STATUS_STATUS");
                    strSql.AppendLine("            ,@PRODUCT_STATUS_CREATE_BY");
                    strSql.AppendLine("            ,@PRODUCT_STATUS_CREATE_DATE");
                    strSql.AppendLine("            ,@PRODUCT_STATUS_UPDATE_BY");
                    strSql.AppendLine("            ,@PRODUCT_STATUS_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@PRODUCT_STATUS_ID", strPRODUCT_STATUS_ID));
                    runSql.Parameters.Add(new SqlParameter("@PRODUCT_STATUS_NAME", strPRODUCT_STATUS_NAME));
                    runSql.Parameters.Add(new SqlParameter("@PRODUCT_STATUS_STATUS", strPRODUCT_STATUS_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@PRODUCT_STATUS_CREATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@PRODUCT_STATUS_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@PRODUCT_STATUS_UPDATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@PRODUCT_STATUS_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_PRODUCT_STATUS-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _logo(
                string strLOGO_ID,
                string strLOGO_NAME,
                string strLOGO_NAME_EN,
                string strLOGO_FILENAME,
                string strLOGO_FILETYPE,
                string strLOGO_FILELOCATION,
                string strLOGO_STATUS,
                string strID,
                string strLOGO_MAINFOLDER
            )
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strLOGO_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_LOGO] ");
                    strSql.AppendLine("            ([LOGO_ID] ");
                    strSql.AppendLine("            ,[LOGO_NAME] ");
                    strSql.AppendLine("            ,[LOGO_NAME_EN] ");
                    strSql.AppendLine("            ,[LOGO_FILENAME] ");
                    strSql.AppendLine("            ,[LOGO_FILETYPE] ");
                    strSql.AppendLine("            ,[LOGO_FILELOCATION] ");
                    strSql.AppendLine("            ,[LOGO_MAINFOLDER] ");
                    strSql.AppendLine("            ,[LOGO_STATUS] ");
                    strSql.AppendLine("            ,[LOGO_CREATE_BY] ");
                    strSql.AppendLine("            ,[LOGO_CREATE_DATE] ");
                    strSql.AppendLine("            ,[LOGO_UPDATE_BY] ");
                    strSql.AppendLine("            ,[LOGO_UPDATE_DATE]) ");
                    strSql.AppendLine("      VALUES ");
                    strSql.AppendLine("            (@LOGO_ID ");
                    strSql.AppendLine("            ,@LOGO_NAME ");
                    strSql.AppendLine("            ,@LOGO_NAME_EN ");
                    strSql.AppendLine("            ,@LOGO_FILENAME ");
                    strSql.AppendLine("            ,@LOGO_FILETYPE ");
                    strSql.AppendLine("            ,@LOGO_FILELOCATION ");
                    strSql.AppendLine("            ,@LOGO_MAINFOLDER ");
                    strSql.AppendLine("            ,@LOGO_STATUS ");
                    strSql.AppendLine("            ,@LOGO_CREATE_BY ");
                    strSql.AppendLine("            ,@LOGO_CREATE_DATE ");
                    strSql.AppendLine("            ,@LOGO_UPDATE_BY ");
                    strSql.AppendLine("            ,@LOGO_UPDATE_DATE) ");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@LOGO_ID", strLOGO_ID));
                    runSql.Parameters.Add(new SqlParameter("@LOGO_NAME", strLOGO_NAME));
                    runSql.Parameters.Add(new SqlParameter("@LOGO_NAME_EN", strLOGO_NAME_EN));
                    runSql.Parameters.Add(new SqlParameter("@LOGO_FILENAME", strLOGO_FILENAME));
                    runSql.Parameters.Add(new SqlParameter("@LOGO_FILETYPE", strLOGO_FILETYPE));
                    runSql.Parameters.Add(new SqlParameter("@LOGO_FILELOCATION", strLOGO_FILELOCATION));
                    runSql.Parameters.Add(new SqlParameter("@LOGO_MAINFOLDER", strLOGO_MAINFOLDER));
                    runSql.Parameters.Add(new SqlParameter("@LOGO_STATUS", strLOGO_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@LOGO_CREATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@LOGO_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@LOGO_UPDATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@LOGO_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_LOGO-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _debt(
                string strDEBT_ID,
                string strDEBT_LIST,
                string strDEBT_YEAR,
                string strDEBT_MONTH,
                string strDEBT_NAME,
                string strDEBT_DETAIL,
                string strDEBT_PRICE,
                string strDEBT_DUE_DATE,
                string strDEBT_PAY,
                string strDEBT_PAY_DATE,
                string strDEBT_LOCATION,
                string strDEBT_MONEY,
                string strDEBT_STATUS,
                string strDEBT_LOGO,
                string strID,
                string strDEBT_FILE,
                string strDEBT_RECEIPT
            )
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strDEBT_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_DEBT]");
                    strSql.AppendLine("            ([DEBT_ID]");
                    strSql.AppendLine("            ,[DEBT_LIST]");
                    strSql.AppendLine("            ,[DEBT_YEAR]");
                    strSql.AppendLine("            ,[DEBT_MONTH]");
                    strSql.AppendLine("            ,[DEBT_NAME]");
                    strSql.AppendLine("            ,[DEBT_DETAIL]");
                    strSql.AppendLine("            ,[DEBT_PRICE]");
                    strSql.AppendLine("            ,[DEBT_DUE_DATE]");
                    strSql.AppendLine("            ,[DEBT_PAY_DATE]");
                    strSql.AppendLine("            ,[DEBT_MONEY]");
                    strSql.AppendLine("            ,[DEBT_PAY]");
                    strSql.AppendLine("            ,[DEBT_LOCATION]");
                    strSql.AppendLine("            ,[DEBT_FILE]");
                    strSql.AppendLine("            ,[DEBT_RECEIPT]");
                    strSql.AppendLine("            ,[DEBT_STATUS]");
                    strSql.AppendLine("            ,[DEBT_LOGO]");
                    strSql.AppendLine("            ,[DEBT_CREATE_BY]");
                    strSql.AppendLine("            ,[DEBT_CREATE_DATE]");
                    strSql.AppendLine("            ,[DEBT_UPDATE_BY]");
                    strSql.AppendLine("            ,[DEBT_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@DEBT_ID");
                    strSql.AppendLine("            ,@DEBT_LIST");
                    strSql.AppendLine("            ,@DEBT_YEAR");
                    strSql.AppendLine("            ,@DEBT_MONTH");
                    strSql.AppendLine("            ,@DEBT_NAME");
                    strSql.AppendLine("            ,@DEBT_DETAIL");
                    strSql.AppendLine("            ,@DEBT_PRICE");
                    strSql.AppendLine("            ,@DEBT_DUE_DATE");
                    strSql.AppendLine("            ,@DEBT_PAY_DATE");
                    strSql.AppendLine("            ,@DEBT_MONEY");
                    strSql.AppendLine("            ,@DEBT_PAY");
                    strSql.AppendLine("            ,@DEBT_LOCATION");
                    strSql.AppendLine("            ,@DEBT_FILE");
                    strSql.AppendLine("            ,@DEBT_RECEIPT");
                    strSql.AppendLine("            ,@DEBT_STATUS");
                    strSql.AppendLine("            ,@DEBT_LOGO");
                    strSql.AppendLine("            ,@DEBT_CREATE_BY");
                    strSql.AppendLine("            ,@DEBT_CREATE_DATE");
                    strSql.AppendLine("            ,@DEBT_UPDATE_BY");
                    strSql.AppendLine("            ,@DEBT_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@DEBT_ID", strDEBT_ID));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_LIST", strDEBT_LIST));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_YEAR", strDEBT_YEAR));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_MONTH", strDEBT_MONTH));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_NAME", strDEBT_NAME));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_DETAIL", strDEBT_DETAIL));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_PRICE", strDEBT_PRICE));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_DUE_DATE", Function._dueDate(strDEBT_DUE_DATE.ToString())));

                    runSql.Parameters.Add(new SqlParameter("@DEBT_PAY_DATE", Function._payDate(strDEBT_PAY_DATE.ToString())));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_MONEY", strDEBT_MONEY));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_PAY", strDEBT_PAY));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_LOCATION", strDEBT_LOCATION));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_FILE", strDEBT_FILE));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_RECEIPT", strDEBT_RECEIPT));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_STATUS", strDEBT_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_LOGO", strDEBT_LOGO));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_CREATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_UPDATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@DEBT_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA inser_Debt-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _expense(
            string strEXPENSE_ID,
            string strEXPENSE_LIST,
            string strEXPENSE_MONEY,
            string strEXPENSE_PAY,
            string strEXPENSE_PAYSUB,
            string strEXPENSE_INCOME,
            string strEXPENSE_DETAIL,
            string strEXPENSE_AMOUNT,
            string strEXPENSE_DATE,
            string strEXPENSE_STATUS,
            string strID
    )
        {
            try
            {
                string mes = "¤Ø³µéÍ§¡ÒÃà¾ÔèÁ";

                if (strEXPENSE_INCOME == "1")
                {
                    mes += "ÃÒÂÃÑº ";
                }
                else
                {
                    mes += "ÃÒÂ¨èÒÂ ";
                }

                var answer = MessageBox.Show(mes + strEXPENSE_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_EXPENSE]");
                    strSql.AppendLine("            ([EXPENSE_ID]");
                    strSql.AppendLine("            ,[EXPENSE_LIST]");
                    strSql.AppendLine("            ,[EXPENSE_MONEY]");
                    strSql.AppendLine("            ,[EXPENSE_PAY]");
                    strSql.AppendLine("            ,[EXPENSE_PAYSUB]");
                    strSql.AppendLine("            ,[EXPENSE_INCOME]");
                    strSql.AppendLine("            ,[EXPENSE_DETAIL]");
                    strSql.AppendLine("            ,[EXPENSE_AMOUNT]");
                    strSql.AppendLine("            ,[EXPENSE_DATE]");
                    strSql.AppendLine("            ,[EXPENSE_STATUS]");
                    strSql.AppendLine("            ,[EXPENSE_CREATE_BY]");
                    strSql.AppendLine("            ,[EXPENSE_CREATE_DATE]");
                    strSql.AppendLine("            ,[EXPENSE_UPDATE_BY]");
                    strSql.AppendLine("            ,[EXPENSE_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@EXPENSE_ID");
                    strSql.AppendLine("            ,@EXPENSE_LIST");
                    strSql.AppendLine("            ,@EXPENSE_MONEY");
                    strSql.AppendLine("            ,@EXPENSE_PAY");
                    strSql.AppendLine("            ,@EXPENSE_PAYSUB");
                    strSql.AppendLine("            ,@EXPENSE_INCOME");
                    strSql.AppendLine("            ,@EXPENSE_DETAIL");
                    strSql.AppendLine("            ,@EXPENSE_AMOUNT");
                    strSql.AppendLine("            ,@EXPENSE_DATE");
                    strSql.AppendLine("            ,@EXPENSE_STATUS");
                    strSql.AppendLine("            ,@EXPENSE_CREATE_BY");
                    strSql.AppendLine("            ,@EXPENSE_CREATE_DATE");
                    strSql.AppendLine("            ,@EXPENSE_UPDATE_BY");
                    strSql.AppendLine("            ,@EXPENSE_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@EXPENSE_ID", strEXPENSE_ID));
                    runSql.Parameters.Add(new SqlParameter("@EXPENSE_LIST", strEXPENSE_LIST));
                    runSql.Parameters.Add(new SqlParameter("@EXPENSE_MONEY", strEXPENSE_MONEY));
                    runSql.Parameters.Add(new SqlParameter("@EXPENSE_PAY", strEXPENSE_PAY));
                    runSql.Parameters.Add(new SqlParameter("@EXPENSE_PAYSUB", strEXPENSE_PAYSUB));
                    runSql.Parameters.Add(new SqlParameter("@EXPENSE_INCOME", strEXPENSE_INCOME));
                    runSql.Parameters.Add(new SqlParameter("@EXPENSE_DETAIL", strEXPENSE_DETAIL));
                    runSql.Parameters.Add(new SqlParameter("@EXPENSE_AMOUNT", strEXPENSE_AMOUNT));
                    runSql.Parameters.Add(new SqlParameter("@EXPENSE_DATE", strEXPENSE_DATE));
                    runSql.Parameters.Add(new SqlParameter("@EXPENSE_STATUS", strEXPENSE_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@EXPENSE_CREATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@EXPENSE_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@EXPENSE_UPDATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@EXPENSE_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_EXPENSE-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _coin(
                string strCOIN_ID,
                decimal strCOIN_TYPE,
                int strCOIN_NUMBER,
                decimal strCOIN_AMOUNT,
                string strCOIN_DATE,
                string strCOIN_DETAIL,
                string strID
   )
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strCOIN_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_COIN]");
                    strSql.AppendLine("            ([COIN_ID]");
                    strSql.AppendLine("            ,[COIN_TYPE]");
                    strSql.AppendLine("            ,[COIN_NUMBER]");
                    strSql.AppendLine("            ,[COIN_AMOUNT]");
                    strSql.AppendLine("            ,[COIN_DATE]");
                    strSql.AppendLine("            ,[COIN_DETAIL]");
                    strSql.AppendLine("            ,[COIN_STATUS]");
                    strSql.AppendLine("            ,[COIN_CREATE_BY]");
                    strSql.AppendLine("            ,[COIN_CREATE_DATE]");
                    strSql.AppendLine("            ,[COIN_UPDATE_BY]");
                    strSql.AppendLine("            ,[COIN_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@COIN_ID");
                    strSql.AppendLine("            ,@COIN_TYPE");
                    strSql.AppendLine("            ,@COIN_NUMBER");
                    strSql.AppendLine("            ,@COIN_AMOUNT");
                    strSql.AppendLine("            ,@COIN_DATE");
                    strSql.AppendLine("            ,@COIN_DETAIL");
                    strSql.AppendLine("            ,@COIN_STATUS");
                    strSql.AppendLine("            ,@COIN_CREATE_BY");
                    strSql.AppendLine("            ,@COIN_CREATE_DATE");
                    strSql.AppendLine("            ,@COIN_UPDATE_BY");
                    strSql.AppendLine("            ,@COIN_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@COIN_ID", strCOIN_ID));
                    runSql.Parameters.Add(new SqlParameter("@COIN_TYPE", strCOIN_TYPE));
                    runSql.Parameters.Add(new SqlParameter("@COIN_NUMBER", strCOIN_NUMBER));
                    runSql.Parameters.Add(new SqlParameter("@COIN_AMOUNT", strCOIN_AMOUNT));
                    runSql.Parameters.Add(new SqlParameter("@COIN_DATE", Function._dueDate(strCOIN_DATE.ToString())));
                    runSql.Parameters.Add(new SqlParameter("@COIN_DETAIL", strCOIN_DETAIL));
                    runSql.Parameters.Add(new SqlParameter("@COIN_STATUS", "Y"));
                    runSql.Parameters.Add(new SqlParameter("@COIN_CREATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@COIN_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@COIN_UPDATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@COIN_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_EXPENSE-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _statment1(string strSTATMENTE_ID
           , string strSTATMENTE_ACCOUNT
           , string strSTATMENTE_DATE
           , string strSTATMENTE_CODE
           , decimal strSTATMENTE_DEPOSIT
           , decimal strSTATMENTE_BALANCE
           , string strSTATMENTE_NUMBER
           , string strSTATMENTE_FILETYPE
           , string strSTATMENTE_DETAIL
           , string strSTATMENTE_STATUS
           , string strSTATMENTE_LIST
           , string strSTATMENTE_CHANEL
           , string strSTATMENTE_FILELOCATION
           , string strID
   )
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strSTATMENTE_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_STATMENT]");
                    strSql.AppendLine("            ([STATMENTE_ID]");
                    strSql.AppendLine("            ,[STATMENTE_ACCOUNT]");
                    strSql.AppendLine("            ,[STATMENTE_DATE]");
                    strSql.AppendLine("            ,[STATMENTE_CODE]");
                    strSql.AppendLine("            ,[STATMENTE_DEPOSIT]");
                    strSql.AppendLine("            ,[STATMENTE_BALANCE]");
                    strSql.AppendLine("            ,[STATMENTE_NUMBER]");
                    strSql.AppendLine("            ,[STATMENTE_FILETYPE]");
                    strSql.AppendLine("            ,[STATMENTE_DETAIL]");
                    strSql.AppendLine("            ,[STATMENTE_STATUS]");
                    strSql.AppendLine("            ,[STATMENTE_LIST]");
                    strSql.AppendLine("            ,[STATMENTE_CHANEL]");
                    strSql.AppendLine("            ,[STATMENTE_FILELOCATION]");
                    strSql.AppendLine("            ,[STATMENTE_CREATE_BY]");
                    strSql.AppendLine("            ,[STATMENTE_CREATE_DATE]");
                    strSql.AppendLine("            ,[STATMENTE_UPDATE_BY]");
                    strSql.AppendLine("            ,[STATMENTE_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@STATMENTE_ID");
                    strSql.AppendLine("            ,@STATMENTE_ACCOUNT");
                    strSql.AppendLine("            ,@STATMENTE_DATE");
                    strSql.AppendLine("            ,@STATMENTE_CODE");
                    strSql.AppendLine("            ,@STATMENTE_DEPOSIT");
                    strSql.AppendLine("            ,@STATMENTE_BALANCE");
                    strSql.AppendLine("            ,@STATMENTE_NUMBER");
                    strSql.AppendLine("            ,@STATMENTE_FILETYPE");
                    strSql.AppendLine("            ,@STATMENTE_DETAIL");
                    strSql.AppendLine("            ,@STATMENTE_STATUS");
                    strSql.AppendLine("            ,@STATMENTE_LIST");
                    strSql.AppendLine("            ,@STATMENTE_CHANEL");
                    strSql.AppendLine("            ,@STATMENTE_FILELOCATION");
                    strSql.AppendLine("            ,@STATMENTE_CREATE_BY");
                    strSql.AppendLine("            ,@STATMENTE_CREATE_DATE");
                    strSql.AppendLine("            ,@STATMENTE_UPDATE_BY");
                    strSql.AppendLine("            ,@STATMENTE_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_ID", strSTATMENTE_ID));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_ACCOUNT", strSTATMENTE_ACCOUNT));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_DATE", Function._dueDate(strSTATMENTE_DATE.ToString())));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_CODE", strSTATMENTE_CODE));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_DEPOSIT", strSTATMENTE_DEPOSIT));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_BALANCE", strSTATMENTE_BALANCE));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_NUMBER", strSTATMENTE_NUMBER));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_FILETYPE", strSTATMENTE_FILETYPE));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_DETAIL", strSTATMENTE_DETAIL));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_STATUS", strSTATMENTE_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_LIST", strSTATMENTE_LIST));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_CHANEL", strSTATMENTE_CHANEL));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_FILELOCATION", strSTATMENTE_FILELOCATION));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_CREATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_UPDATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_DEPOSIT_TB_STATMENT-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _statment0(string strSTATMENTE_ID
          , string strSTATMENTE_ACCOUNT
          , string strSTATMENTE_DATE
          , string strSTATMENTE_CODE
          , decimal strSTATMENTE_WITHDRAWAL
          , decimal strSTATMENTE_BALANCE
          , string strSTATMENTE_NUMBER
          , string strSTATMENTE_FILETYPE
          , string strSTATMENTE_DETAIL
          , string strSTATMENTE_STATUS
          , string strSTATMENTE_LIST
          , string strSTATMENTE_CHANEL
          , string strSTATMENTE_FILELOCATION
          , string strID

  )
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strSTATMENTE_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [TB_STATMENT]");
                    strSql.AppendLine("            ([STATMENTE_ID]");
                    strSql.AppendLine("            ,[STATMENTE_ACCOUNT]");
                    strSql.AppendLine("            ,[STATMENTE_DATE]");
                    strSql.AppendLine("            ,[STATMENTE_CODE]");
                    strSql.AppendLine("            ,[STATMENTE_WITHDRAWAL]");
                    strSql.AppendLine("            ,[STATMENTE_BALANCE]");
                    strSql.AppendLine("            ,[STATMENTE_NUMBER]");
                    strSql.AppendLine("            ,[STATMENTE_FILETYPE]");
                    strSql.AppendLine("            ,[STATMENTE_DETAIL]");
                    strSql.AppendLine("            ,[STATMENTE_STATUS]");
                    strSql.AppendLine("            ,[STATMENTE_LIST]");
                    strSql.AppendLine("            ,[STATMENTE_CHANEL]");
                    strSql.AppendLine("            ,[STATMENTE_FILELOCATION]");
                    strSql.AppendLine("            ,[STATMENTE_CREATE_BY]");
                    strSql.AppendLine("            ,[STATMENTE_CREATE_DATE]");
                    strSql.AppendLine("            ,[STATMENTE_UPDATE_BY]");
                    strSql.AppendLine("            ,[STATMENTE_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@STATMENTE_ID");
                    strSql.AppendLine("            ,@STATMENTE_ACCOUNT");
                    strSql.AppendLine("            ,@STATMENTE_DATE");
                    strSql.AppendLine("            ,@STATMENTE_CODE");
                    strSql.AppendLine("            ,@STATMENTE_WITHDRAWAL");
                    strSql.AppendLine("            ,@STATMENTE_BALANCE");
                    strSql.AppendLine("            ,@STATMENTE_NUMBER");
                    strSql.AppendLine("            ,@STATMENTE_FILETYPE");
                    strSql.AppendLine("            ,@STATMENTE_DETAIL");
                    strSql.AppendLine("            ,@STATMENTE_STATUS");
                    strSql.AppendLine("            ,@STATMENTE_LIST");
                    strSql.AppendLine("            ,@STATMENTE_CHANEL");
                    strSql.AppendLine("            ,@STATMENTE_FILELOCATION");
                    strSql.AppendLine("            ,@STATMENTE_CREATE_BY");
                    strSql.AppendLine("            ,@STATMENTE_CREATE_DATE");
                    strSql.AppendLine("            ,@STATMENTE_UPDATE_BY");
                    strSql.AppendLine("            ,@STATMENTE_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_ID", strSTATMENTE_ID));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_ACCOUNT", strSTATMENTE_ACCOUNT));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_DATE", Function._dueDate(strSTATMENTE_DATE.ToString())));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_CODE", strSTATMENTE_CODE));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_WITHDRAWAL", strSTATMENTE_WITHDRAWAL));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_BALANCE", strSTATMENTE_BALANCE));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_NUMBER", strSTATMENTE_NUMBER));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_FILETYPE", strSTATMENTE_FILETYPE));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_DETAIL", strSTATMENTE_DETAIL));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_STATUS", strSTATMENTE_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_LIST", strSTATMENTE_LIST));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_CHANEL", strSTATMENTE_CHANEL));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_FILELOCATION", strSTATMENTE_FILELOCATION));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_CREATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_UPDATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@STATMENTE_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_WITHDRAWAL_TB_STATMENT-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _banknote(
            string strBANKNOTE_NAME
           , string strBANKNOTE_SIZE_UNIT
           , decimal strBANKNOTE_SIZE_WIDTH
           , decimal strBANKNOTE_SIZE_HIGH
           , string strBANKNOTE_PRICE
           , string strBANKNOTE_PRICE_CHANGE
           , string strBANKNOTE_DATE
           , string strBANKNOTE_DATE_CHANGE
           , string strBANKNOTE_PIC_FONT_DETAIL
           , string strBANKNOTE_PIC_BEHIDE_DETAIL
           , string strBANKNOTE_PIC_FONT
           , string strBANKNOTE_PIC_BEHIDE
           , string strID

)
        {
            try
            {
                string id = Function._getRowID("BNT");

                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + id + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [dbo].[TB_BANKNOTE]");
                    strSql.AppendLine("            ([BANKNOTE_ID]");
                    strSql.AppendLine("            ,[BANKNOTE_NAME]");
                    strSql.AppendLine("            ,[BANKNOTE_SIZE_UNIT]");
                    strSql.AppendLine("            ,[BANKNOTE_SIZE_WIDTH]");
                    strSql.AppendLine("            ,[BANKNOTE_SIZE_HIGH]");
                    strSql.AppendLine("            ,[BANKNOTE_PRICE]");
                    strSql.AppendLine("            ,[BANKNOTE_PRICE_CHANGE]");
                    strSql.AppendLine("            ,[BANKNOTE_DATE]");
                    strSql.AppendLine("            ,[BANKNOTE_DATE_CHANGE]");
                    strSql.AppendLine("            ,[BANKNOTE_PIC_FONT_DETAIL]");
                    strSql.AppendLine("            ,[BANKNOTE_PIC_BEHIDE_DETAIL]");
                    strSql.AppendLine("            ,[BANKNOTE_PIC_FONT]");
                    strSql.AppendLine("            ,[BANKNOTE_PIC_BEHIDE]");
                    strSql.AppendLine("            ,[BANKNOTE_CREATE_BY]");
                    strSql.AppendLine("            ,[BANKNOTE_CREATE_DATE]");
                    strSql.AppendLine("            ,[BANKNOTE_UPDATE_BY]");
                    strSql.AppendLine("            ,[BANKNOTE_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (");
                    strSql.AppendLine(" 		   @BANKNOTE_ID");
                    strSql.AppendLine("            ,@BANKNOTE_NAME");
                    strSql.AppendLine("            ,@BANKNOTE_SIZE_UNIT");
                    strSql.AppendLine("            ,@BANKNOTE_SIZE_WIDTH");
                    strSql.AppendLine("            ,@BANKNOTE_SIZE_HIGH");
                    strSql.AppendLine("            ,@BANKNOTE_PRICE");
                    strSql.AppendLine("            ,@BANKNOTE_PRICE_CHANGE");
                    strSql.AppendLine("            ,@BANKNOTE_DATE");
                    strSql.AppendLine("            ,@BANKNOTE_DATE_CHANGE");
                    strSql.AppendLine("            ,@BANKNOTE_PIC_FONT_DETAIL");
                    strSql.AppendLine("            ,@BANKNOTE_PIC_BEHIDE_DETAIL");
                    strSql.AppendLine("            ,@BANKNOTE_PIC_FONT");
                    strSql.AppendLine("            ,@BANKNOTE_PIC_BEHIDE");
                    strSql.AppendLine("            ,@BANKNOTE_CREATE_BY ");
                    strSql.AppendLine("            ,@BANKNOTE_CREATE_DATE");
                    strSql.AppendLine("            ,@BANKNOTE_UPDATE_BY");
                    strSql.AppendLine("            ,@BANKNOTE_UPDATE_DATE");
                    strSql.AppendLine(" 		   )");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_ID", id));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_NAME", strBANKNOTE_NAME));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_SIZE_UNIT", strBANKNOTE_SIZE_UNIT));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_SIZE_WIDTH", strBANKNOTE_SIZE_WIDTH));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_SIZE_HIGH", strBANKNOTE_SIZE_HIGH));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_PRICE", strBANKNOTE_PRICE));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_PRICE_CHANGE", strBANKNOTE_PRICE_CHANGE));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_DATE", strBANKNOTE_DATE));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_DATE_CHANGE", strBANKNOTE_DATE_CHANGE));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_PIC_FONT_DETAIL", strBANKNOTE_PIC_FONT_DETAIL));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_PIC_BEHIDE_DETAIL", strBANKNOTE_PIC_BEHIDE_DETAIL));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_PIC_FONT", Function._copyImage(id + "F", strBANKNOTE_PIC_FONT, "Banknote")));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_PIC_BEHIDE", Function._copyImage(id + "B", strBANKNOTE_PIC_BEHIDE, "Banknote")));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_CREATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_UPDATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@BANKNOTE_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_BANKNOTE-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //public void _log(string strId, string strMenuId, string strAction)
        //{
        //    try
        //    {
        //        SqlConnection Connect = new SqlConnection();
        //        String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

        //        Connect.ConnectionString = strConnect;
        //        Connect.Open();

        //        StringBuilder strSql = new StringBuilder();

        //        strSql.AppendLine(" INSERT INTO [TB_LOG]");
        //        strSql.AppendLine("            ([USER_ID],[MENU_ID],[LOG_ACTION],[LOG_DATE])");
        //        strSql.AppendLine("      VALUES");
        //        strSql.AppendLine("            (@USER_ID");
        //        strSql.AppendLine("            ,@MENU_ID");
        //        strSql.AppendLine("            ,@LOG_ACTION");
        //        strSql.AppendLine("            ,@LOG_DATE)");

        //        SqlCommand runSql = new SqlCommand(strSql.ToString());

        //        runSql.Parameters.Add(new SqlParameter("@USER_ID", strId));
        //        runSql.Parameters.Add(new SqlParameter("@MENU_ID", strMenuId));
        //        runSql.Parameters.Add(new SqlParameter("@LOG_ACTION", strAction));
        //        runSql.Parameters.Add(new SqlParameter("@LOG_DATE", Function._createDateAndTime()));

        //        runSql.Connection = Connect;
        //        runSql.ExecuteNonQuery();
        //        Connect.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        public void _path(
                string strPATH_ID
                , string strPATH_NAME
                , string strPATH_LOCATION
                , string strPATH_DETAIL
                , string strPATH_STATUS
                , string strID)
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strPATH_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [dbRSA].[dbo].[TB_PATH]");
                    strSql.AppendLine("            ([PATH_ID]");
                    strSql.AppendLine("            ,[PATH_NAME]");
                    strSql.AppendLine("            ,[PATH_LOCATION]");
                    strSql.AppendLine("            ,[PATH_DETAIL]");
                    strSql.AppendLine("            ,[PATH_STATUS]");
                    strSql.AppendLine("            ,[PATH_CREATE_BY]");
                    strSql.AppendLine("            ,[PATH_CREATE_DATE]");
                    strSql.AppendLine("            ,[PATH_UPDATE_BY]");
                    strSql.AppendLine("            ,[PATH_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@PATH_ID");
                    strSql.AppendLine("            ,@PATH_NAME");
                    strSql.AppendLine("            ,@PATH_LOCATION");
                    strSql.AppendLine("            ,@PATH_DETAIL");
                    strSql.AppendLine("            ,@PATH_STATUS");
                    strSql.AppendLine("            ,@PATH_CREATE_BY");
                    strSql.AppendLine("            ,@PATH_CREATE_DATE");
                    strSql.AppendLine("            ,@PATH_UPDATE_BY");
                    strSql.AppendLine("            ,@PATH_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("PATH_ID", strPATH_ID));
                    runSql.Parameters.Add(new SqlParameter("PATH_NAME", strPATH_NAME));
                    runSql.Parameters.Add(new SqlParameter("PATH_LOCATION", strPATH_LOCATION));
                    runSql.Parameters.Add(new SqlParameter("PATH_DETAIL", strPATH_DETAIL));
                    runSql.Parameters.Add(new SqlParameter("PATH_STATUS", strPATH_STATUS));
                    runSql.Parameters.Add(new SqlParameter("PATH_CREATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("PATH_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("PATH_UPDATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("PATH_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_PATH-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _member(
                   string ID
                   , string strMEMBER_Name
                   , string strMEMBER_NAMEEN
                   , string strMEMBER_NAME
                   , string strMEMBER_SURNAME
                   , string strMEMBER_USER
                   , string strMEMBER_PASSWORD
                   , string strMEMBER_WEBSITE
                   , string strMEMBER_EMAIL
                   , string strMEMBER_PHONE
                   , string strMEMBER_LOGO
                   , string strMEMBER_STATUS
                   , string strID)
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine("INSERT INTO [dbRSA].[dbo].[TB_MEMBER]");
                    strSql.AppendLine("           ([MEMBER_ID]");
                    strSql.AppendLine("           ,[MEMBER_Name]");
                    strSql.AppendLine("           ,[MEMBER_NAMEEN]");
                    strSql.AppendLine("           ,[MEMBER_NAME]");
                    strSql.AppendLine("           ,[MEMBER_SURNAME]");
                    strSql.AppendLine("           ,[MEMBER_USER]");
                    strSql.AppendLine("           ,[MEMBER_PASSWORD]");
                    strSql.AppendLine("           ,[MEMBER_WEBSITE]");
                    strSql.AppendLine("           ,[MEMBER_EMAIL]");
                    strSql.AppendLine("           ,[MEMBER_PHONE]");
                    strSql.AppendLine("           ,[MEMBER_LOGO]");
                    strSql.AppendLine("           ,[MEMBER_STATUS]");
                    strSql.AppendLine("           ,[MEMBER_CREATE_BY]");
                    strSql.AppendLine("           ,[MEMBER_CREATE_DATE]");
                    strSql.AppendLine("           ,[MEMBER_UPDATE_BY]");
                    strSql.AppendLine("           ,[MEMBER_UPDATE_DATE])");
                    strSql.AppendLine("     VALUES");
                    strSql.AppendLine("           (@MEMBER_ID");
                    strSql.AppendLine("           ,@MEMBER_Name");
                    strSql.AppendLine("           ,@MEMBER_NAMEEN");
                    strSql.AppendLine("           ,@MEMBER_NAME");
                    strSql.AppendLine("           ,@MEMBER_SURNAME");
                    strSql.AppendLine("           ,@MEMBER_USER");
                    strSql.AppendLine("           ,@MEMBER_PASSWORD");
                    strSql.AppendLine("           ,@MEMBER_WEBSITE");
                    strSql.AppendLine("           ,@MEMBER_EMAIL");
                    strSql.AppendLine("           ,@MEMBER_PHONE");
                    strSql.AppendLine("           ,@MEMBER_LOGO");
                    strSql.AppendLine("           ,@MEMBER_STATUS");
                    strSql.AppendLine("           ,@MEMBER_CREATE_BY");
                    strSql.AppendLine("           ,@MEMBER_CREATE_DATE");
                    strSql.AppendLine("           ,@MEMBER_UPDATE_BY");
                    strSql.AppendLine("           ,@MEMBER_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@MEMBER_ID", ID));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_Name", strMEMBER_Name));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_NAMEEN", strMEMBER_NAMEEN));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_NAME", strMEMBER_NAME));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_SURNAME", strMEMBER_SURNAME));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_USER", strMEMBER_USER));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_PASSWORD", strMEMBER_PASSWORD));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_WEBSITE", strMEMBER_WEBSITE));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_EMAIL", strMEMBER_EMAIL));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_PHONE", strMEMBER_PHONE));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_LOGO", strMEMBER_LOGO));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_STATUS", strMEMBER_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_CREATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_UPDATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@MEMBER_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_MEMBER-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _creditStatment(
               string strCREDIT_ID,
               string strCREDIT_NUMBER,
               string strCREDIT_BANK,
               string strCREDIT_HOST,
               string strCREDIT_TID,
               string strCREDIT_MID,
               string strCREDIT_STAN,
               string strCREDIT_TRACE,
               string strCREDIT_BATCH,
               string strCREDIT_APPCODE,
               string strCREDIT_REF,
               string strCREDIT_SHOP,
               string strCREDIT_PRICE,
               string strCREDIT_DATE,
               string strCREDIT_TIME,
               string strCREDIT_DETAIL,
               string strCREDIT_FILE,
               string strCREDIT_STATUS,
               string strID,
               string strCREDIT_LOCATION)
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strCREDIT_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [dbRSA].[dbo].[TB_CREDIT]");
                    strSql.AppendLine("            ([CREDIT_ID]");
                    strSql.AppendLine("            ,[CREDIT_NUMBER]");
                    strSql.AppendLine("            ,[CREDIT_BANK]");
                    strSql.AppendLine("            ,[CREDIT_HOST]");
                    strSql.AppendLine("            ,[CREDIT_TID]");
                    strSql.AppendLine("            ,[CREDIT_MID]");
                    strSql.AppendLine("            ,[CREDIT_STAN]");
                    strSql.AppendLine("            ,[CREDIT_TRACE]");
                    strSql.AppendLine("            ,[CREDIT_BATCH]");
                    strSql.AppendLine("            ,[CREDIT_APPCODE]");
                    strSql.AppendLine("            ,[CREDIT_REF]");
                    strSql.AppendLine("            ,[CREDIT_SHOP]");
                    strSql.AppendLine("            ,[CREDIT_LOCATION]");
                    strSql.AppendLine("            ,[CREDIT_PRICE]");
                    strSql.AppendLine("            ,[CREDIT_DATE]");
                    strSql.AppendLine("            ,[CREDIT_TIME]");
                    strSql.AppendLine("            ,[CREDIT_DETAIL]");
                    strSql.AppendLine("            ,[CREDIT_FILE]");
                    strSql.AppendLine("            ,[CREDIT_STATUS]");
                    strSql.AppendLine("            ,[CREDIT_CREATE_BY]");
                    strSql.AppendLine("            ,[CREDIT_CREATE_DATE]");
                    strSql.AppendLine("            ,[CREDIT_UPDATE_BY]");
                    strSql.AppendLine("            ,[CREDIT_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@CREDIT_ID");
                    strSql.AppendLine("            ,@CREDIT_NUMBER");
                    strSql.AppendLine("            ,@CREDIT_BANK");
                    strSql.AppendLine("            ,@CREDIT_HOST");
                    strSql.AppendLine("            ,@CREDIT_TID");
                    strSql.AppendLine("            ,@CREDIT_MID");
                    strSql.AppendLine("            ,@CREDIT_STAN");
                    strSql.AppendLine("            ,@CREDIT_TRACE");
                    strSql.AppendLine("            ,@CREDIT_BATCH");
                    strSql.AppendLine("            ,@CREDIT_APPCODE");
                    strSql.AppendLine("            ,@CREDIT_REF");
                    strSql.AppendLine("            ,@CREDIT_SHOP");
                    strSql.AppendLine("            ,@CREDIT_LOCATION");
                    strSql.AppendLine("            ,@CREDIT_PRICE");
                    strSql.AppendLine("            ,@CREDIT_DATE");
                    strSql.AppendLine("            ,@CREDIT_TIME");
                    strSql.AppendLine("            ,@CREDIT_DETAIL");
                    strSql.AppendLine("            ,@CREDIT_FILE");
                    strSql.AppendLine("            ,@CREDIT_STATUS");
                    strSql.AppendLine("            ,@CREDIT_CREATE_BY");
                    strSql.AppendLine("            ,@CREDIT_CREATE_DATE");
                    strSql.AppendLine("            ,@CREDIT_UPDATE_BY");
                    strSql.AppendLine("            ,@CREDIT_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@CREDIT_ID", strCREDIT_ID));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_NUMBER", strCREDIT_NUMBER));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_BANK", strCREDIT_BANK));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_HOST", strCREDIT_HOST));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_TID", strCREDIT_TID));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_MID", strCREDIT_MID));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_STAN", strCREDIT_STAN));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_TRACE", strCREDIT_TRACE));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_BATCH", strCREDIT_BATCH));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_APPCODE", strCREDIT_APPCODE));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_REF", strCREDIT_REF));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_SHOP", strCREDIT_SHOP));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_LOCATION", strCREDIT_LOCATION));
                    //runSql.Parameters.Add(new SqlParameter("@CREDIT_PRICE", Function._chkNum(strCREDIT_PRICE)));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_DATE", strCREDIT_DATE));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_TIME", strCREDIT_TIME));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_DETAIL", strCREDIT_DETAIL));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_FILE", strCREDIT_FILE));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_STATUS", strCREDIT_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_CREATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_UPDATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_CREDIT-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _card(
             string strCARD_ID
           , string strCARD_NUMBER
           , string strCARD_NAME
           , string strCARD_BANK
           , string strCARD_DETAIL
           , string strCARD_TYPE
           , string strCARD_START
           , string strCARD_END
           , string strCARD_FILE
           , string strCARD_STATUS
           , string strID)
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strCARD_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [dbRSA].[dbo].[TB_CARD]");
                    strSql.AppendLine("            ([CARD_ID]");
                    strSql.AppendLine("            ,[CARD_NUMBER]");
                    strSql.AppendLine("            ,[CARD_NAME]");
                    strSql.AppendLine("            ,[CARD_BANK]");
                    strSql.AppendLine("            ,[CARD_DETAIL]");
                    strSql.AppendLine("            ,[CARD_TYPE]");
                    strSql.AppendLine("            ,[CARD_START]");
                    strSql.AppendLine("            ,[CARD_END]");
                    strSql.AppendLine("            ,[CARD_FILE]");
                    strSql.AppendLine("            ,[CARD_STATUS]");
                    strSql.AppendLine("            ,[CARD_CREATE_BY]");
                    strSql.AppendLine("            ,[CARD_CREATE_DATE]");
                    strSql.AppendLine("            ,[CARD_UPDATE_BY]");
                    strSql.AppendLine("            ,[CREDIT_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@CARD_ID");
                    strSql.AppendLine("            ,@CARD_NUMBER");
                    strSql.AppendLine("            ,@CARD_NAME");
                    strSql.AppendLine("            ,@CARD_BANK");
                    strSql.AppendLine("            ,@CARD_DETAIL");
                    strSql.AppendLine("            ,@CARD_TYPE");
                    strSql.AppendLine("            ,@CARD_START");
                    strSql.AppendLine("            ,@CARD_END");
                    strSql.AppendLine("            ,@CARD_FILE");
                    strSql.AppendLine("            ,@CARD_STATUS");
                    strSql.AppendLine("            ,@CARD_CREATE_BY");
                    strSql.AppendLine("            ,@CARD_CREATE_DATE");
                    strSql.AppendLine("            ,@CARD_UPDATE_BY");
                    strSql.AppendLine("            ,@CREDIT_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@CARD_ID", strCARD_ID));
                    runSql.Parameters.Add(new SqlParameter("@CARD_NUMBER", strCARD_NUMBER));
                    runSql.Parameters.Add(new SqlParameter("@CARD_NAME", strCARD_NAME));
                    runSql.Parameters.Add(new SqlParameter("@CARD_BANK", strCARD_BANK));
                    runSql.Parameters.Add(new SqlParameter("@CARD_DETAIL", strCARD_DETAIL));
                    runSql.Parameters.Add(new SqlParameter("@CARD_TYPE", strCARD_TYPE));
                    runSql.Parameters.Add(new SqlParameter("@CARD_START", strCARD_START));
                    runSql.Parameters.Add(new SqlParameter("@CARD_END", strCARD_END));
                    runSql.Parameters.Add(new SqlParameter("@CARD_FILE", strCARD_FILE));
                    runSql.Parameters.Add(new SqlParameter("@CARD_STATUS", strCARD_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@CARD_CREATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@CARD_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@CARD_UPDATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@CREDIT_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("à¾ÔèÁ¢éÍÁÙÅàÃÕÂºÃéÍÂ", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_CARD-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void _movie(
               string strMOVIE_ID
               , string strMOVIE_CINEMA
               , string strMOVIE_BRANCH
               , string strMOVIE_TITLE_EN
               , string strMOVIE_TITLE_TH
               , string strMOVIE_DATE
               , string strMOVIE_SHOWTIME
               , string strMOVIE_THEARE
               , string strMOVIE_SAET
               , string strMOVIE_PRICE
               , string strMOVIE_POS_ID
               , string strMOVIE_TAXINV_ABB
               , string strMOVIE_TAX_ID
               , string strMOVIE_DETAIL
               , string strMOVIE_FILE
               , string strMOVIE_STATUS
               , string strMOVIE_CREATE_BY
               , string strMOVIE_CREATE_DATE
               , string strMOVIE_UPDATE_BY
               , string strMOVIE_UPDATE_DATE
               , string strID)
        {
            try
            {
                var answer = MessageBox.Show("¤Ø³µéÍ§¡ÒÃà¾ÔèÁ " + strMOVIE_ID + " ËÃ×ÍäÁè ", "Â×¹ÂÑ¹¡ÒÃà¾ÔèÁ¢éÍÁÙÅ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine(" INSERT INTO [dbRSA].[dbo].[TB_MOVIE]");
                    strSql.AppendLine("            ([MOVIE_ID]");
                    strSql.AppendLine("            ,[MOVIE_CINEMA]");
                    strSql.AppendLine("            ,[MOVIE_BRANCH]");
                    strSql.AppendLine("            ,[MOVIE_TITLE_EN]");
                    strSql.AppendLine("            ,[MOVIE_TITLE_TH]");
                    strSql.AppendLine("            ,[MOVIE_DATE]");
                    strSql.AppendLine("            ,[MOVIE_SHOWTIME]");
                    strSql.AppendLine("            ,[MOVIE_THEARE]");
                    strSql.AppendLine("            ,[MOVIE_SAET]");
                    strSql.AppendLine("            ,[MOVIE_PRICE]");
                    strSql.AppendLine("            ,[MOVIE_POS_ID]");
                    strSql.AppendLine("            ,[MOVIE_TAXINV_ABB]");
                    strSql.AppendLine("            ,[MOVIE_TAX_ID]");
                    strSql.AppendLine("            ,[MOVIE_DETAIL]");
                    strSql.AppendLine("            ,[MOVIE_FILE]");
                    strSql.AppendLine("            ,[MOVIE_STATUS]");
                    strSql.AppendLine("            ,[MOVIE_CREATE_BY]");
                    strSql.AppendLine("            ,[MOVIE_CREATE_DATE]");
                    strSql.AppendLine("            ,[MOVIE_UPDATE_BY]");
                    strSql.AppendLine("            ,[MOVIE_UPDATE_DATE])");
                    strSql.AppendLine("      VALUES");
                    strSql.AppendLine("            (@MOVIE_ID");
                    strSql.AppendLine("            ,@MOVIE_CINEMA");
                    strSql.AppendLine("            ,@MOVIE_BRANCH");
                    strSql.AppendLine("            ,@MOVIE_TITLE_EN");
                    strSql.AppendLine("            ,@MOVIE_TITLE_TH");
                    strSql.AppendLine("            ,@MOVIE_DATE");
                    strSql.AppendLine("            ,@MOVIE_SHOWTIME");
                    strSql.AppendLine("            ,@MOVIE_THEARE");
                    strSql.AppendLine("            ,@MOVIE_SAET");
                    strSql.AppendLine("            ,@MOVIE_PRICE");
                    strSql.AppendLine("            ,@MOVIE_POS_ID");
                    strSql.AppendLine("            ,@MOVIE_TAXINV_ABB");
                    strSql.AppendLine("            ,@MOVIE_TAX_ID");
                    strSql.AppendLine("            ,@MOVIE_DETAIL");
                    strSql.AppendLine("            ,@MOVIE_FILE");
                    strSql.AppendLine("            ,@MOVIE_STATUS");
                    strSql.AppendLine("            ,@MOVIE_CREATE_BY");
                    strSql.AppendLine("            ,@MOVIE_CREATE_DATE");
                    strSql.AppendLine("            ,@MOVIE_UPDATE_BY");
                    strSql.AppendLine("            ,@MOVIE_UPDATE_DATE)");

                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@MOVIE_ID", strMOVIE_ID));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_CINEMA", strMOVIE_CINEMA));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_BRANCH", strMOVIE_BRANCH));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_TITLE_EN", strMOVIE_TITLE_EN));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_TITLE_TH", strMOVIE_TITLE_TH));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_DATE", strMOVIE_DATE));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_SHOWTIME", strMOVIE_SHOWTIME));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_THEARE", strMOVIE_THEARE));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_SAET", strMOVIE_SAET));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_PRICE", strMOVIE_PRICE));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_POS_ID", strMOVIE_POS_ID));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_TAXINV_ABB", strMOVIE_TAXINV_ABB));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_TAX_ID", strMOVIE_TAX_ID));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_DETAIL", strMOVIE_DETAIL));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_FILE", strMOVIE_FILE));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_STATUS", strMOVIE_STATUS));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_CREATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_CREATE_DATE", Function._createDate()));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_UPDATE_BY", strID));
                    runSql.Parameters.Add(new SqlParameter("@MOVIE_UPDATE_DATE", Function._updatDate()));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA insert_TB_MOVIE-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}