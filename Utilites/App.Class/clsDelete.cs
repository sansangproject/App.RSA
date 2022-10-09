using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;
using SANSANG.Utilites.App.Model;

namespace SANSANG.Class
{
    public class clsDelete
    {
        private dbConnection db = new dbConnection();
        private clsLog Log = new clsLog();
        private TableConstant Tb = new TableConstant();
        private OperationConstant Operation = new OperationConstant();
        private StoreConstant Store = new StoreConstant();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Mess = new clsMessage();
        private DataTable dt = new DataTable();

        private string Error = "";
        string[,] Parameter = new string[,] { };

        public bool TrackPost(string TrackingId)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", ""},
                    {"@Status", ""},
                    {"@User", ""},
                    {"@IsActive", ""},
                    {"@IsDelete", ""},
                    {"@Operation", Operation.DeleteAbbr},
                    {"@TrackingId", TrackingId},
                    {"@Barcode", ""},
                    {"@Number", ""},
                    {"@Date", ""},
                    {"@Location", ""},
                    {"@Postcode", ""},
                    {"@Description", ""},
                    {"@DeliveryDate", ""},
                    {"@Receiver", ""},
                    {"@SignatureId", ""},
                };

                db.Operations(Store.ManageTrackPost, Parameter, out Error);

                if (string.IsNullOrEmpty(Error))
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

































        public void _delete(string txtTableName, string txtColName, string txtValue)
        {
            try
            {
                var answer = MessageBox.Show("คุณต้องการลบ " + txtValue + " หรือไม่ ", "ยืนยันการลบข้อมูล", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (answer == DialogResult.Cancel)
                {
                    return;
                }
                if (answer == DialogResult.Yes)
                {
                    string strSQL = "DELETE FROM " + txtTableName + " WHERE " + txtColName + " = @VALUE";

                    SqlConnection Connect = new SqlConnection();
                    String strConnect = System.Configuration.ConfigurationManager.ConnectionStrings["dbRSA"].ConnectionString;

                    Connect.ConnectionString = strConnect;
                    Connect.Open();

                    StringBuilder strSql = new StringBuilder(strSQL);
                    SqlCommand runSql = new SqlCommand(strSql.ToString());

                    runSql.Parameters.Add(new SqlParameter("@VALUE", txtValue));

                    runSql.Connection = Connect;
                    runSql.ExecuteNonQuery();
                    Connect.Close();

                    MessageBox.Show("ลบข้อมูลเรียบร้อย", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RSA delete-Error\n\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string DeleteData(string strCode, string strField, string strName, string strStored, string strUser, string strAppCode, string strAppName)
        {
            string strOpe = "";
            string strErr = "";
            string strReturn = "";

            try
            {
                if (strCode != "")
                {
                    strOpe = "D";

                    string[,] Parameter = new string[,]
                    {
                       {"@" + strField, strCode},
                    };

                    Mess.MessageConfirmation(strOpe, strCode, strName);

                    using (var Popup = new FrmMessagesBox(Mess.strOperation, Mess.strMes, "YES", "NO", Mess.strImage))
                    {
                        var result = Popup.ShowDialog();

                        if (result == DialogResult.Yes)
                        {
                            Popup.Close();
                            db.Operations(strStored, Parameter, out strErr);

                            if (strErr == null)
                            {
                                Mess.MessageResult(strOpe, "C", strErr);
                                strReturn = "";
                            }
                            else
                            {
                                Mess.MessageResult(strOpe, "E", strErr);
                                strReturn = "Error";
                            }
                        }
                    }

                    Log.WriteLogData(strAppCode, strAppName, strUser, "Delete");
                }

                return strReturn;
            }
            catch (Exception ex)
            {
                strReturn = "Error";
                Log.WriteLogData(strAppCode, strAppName, strUser, ex.Message);
                return strReturn;
            }
        }

        public bool Drop(string AppCodes = "", string AppNames = "", string UserId = "", int Types = 1, string srtTable = "", TextBox tbCode = null, string Details = "", bool IsConfirm = true)
        {
            string Code = tbCode.Text;
            string Detail = Details;
            string User = UserId;
            string AppCode = AppCodes;
            string AppName = AppNames;
            string Type = Convert.ToString(Types);
            string Table = srtTable;

            if (!string.IsNullOrEmpty(Code))
            {
                try
                {
                    if (IsConfirm)
                    {
                        Mess.MessageConfirmation(Operation.DeleteAbbr, Code, Detail);

                        using (var Popup = new FrmMessagesBox(Mess.strOperation, Mess.strMes, "YES", "NO", Mess.strImage))
                        {
                            if (Popup.ShowDialog() == DialogResult.Yes)
                            {
                                Popup.Close();

                                if (string.IsNullOrEmpty(Fn.DeleteData("", Code, Table, Type, User)))
                                {
                                    Mess.MessageResult(Operation.DeleteAbbr, "C", Error);
                                    return true;
                                }
                                else
                                {
                                    Mess.MessageResult(Operation.DeleteAbbr, "E", Error);
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Fn.DeleteData("", Code, Table, Type, User)))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteLogData(AppCode, AppName, User, ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool DropId(string AppCodes = "", string AppNames = "", string UserId = "", int Types = 1, string srtTable = "", TextBox tbId = null, TextBox tbCode = null, string Details = "")
        {
            string Id = tbId.Text;
            string Code = tbCode.Text;
            string Operation = "D";
            string Detail = Details;
            string User = UserId;
            string AppCode = AppCodes;
            string AppName = AppNames;
            string Type = Convert.ToString(Types);
            string Table = srtTable;

            if (!string.IsNullOrEmpty(Id))
            {
                try
                {
                    Mess.MessageConfirmation(Operation, Code, Detail);

                    using (var Popup = new FrmMessagesBox(Mess.strOperation, Mess.strMes, "YES", "NO", Mess.strImage))
                    {
                        if (Popup.ShowDialog() == DialogResult.Yes)
                        {
                            Popup.Close();

                            if (string.IsNullOrEmpty(Fn.DeleteData(Id, "", Table, Type, User)))
                            {
                                Mess.MessageResult(Operation, "C", Error);
                                return true;
                            }
                            else
                            {
                                Mess.MessageResult(Operation, "E", Error);
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
                    Log.WriteLogData(AppCode, AppName, User, ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    
    }
}