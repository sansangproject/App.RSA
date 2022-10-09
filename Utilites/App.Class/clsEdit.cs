using System;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;
using SANSANG.Utilites.App.Model;

namespace SANSANG.Class
{
    public class clsEdit
    {
        private dbConnection db = new dbConnection();
        private clsLog Log = new clsLog();
        private TableConstant Tb = new TableConstant();
        private clsFunction Fn = new clsFunction();
        private clsMessage Mess = new clsMessage();
        private clsInsert Insert = new clsInsert();
        private OperationConstant Operation = new OperationConstant();

        private string Error = "";

        public bool Update(string AppCodes = "", string AppNames = "", string UserId = "", string strStore = "", string[,] Parameter = null, string Codes = "", string Details = "")
        {
            string Code = Codes;
            string Detail = Details;
            string User = UserId;
            string AppCode = AppCodes;
            string AppName = AppNames;
            string Store = strStore;

            try
            {
                Mess.MessageConfirmation(Operation.UpdateAbbr, Code, Detail);

                using (var Popup = new FrmMessagesBox(Mess.strOperation, Mess.strMes, "YES", "NO", Mess.strImage))
                {
                    var result = Popup.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Popup.Close();
                        db.Operations(Store, Parameter, out Error);

                        if (Error == null)
                        {
                            Mess.MessageResult(Operation.UpdateAbbr, "C");
                            Insert.Log.WriteLogData(AppCode, AppName, UserId, "Insert");
                            return true;
                        }
                        else
                        {
                            Mess.MessageResult(Operation.UpdateAbbr, "E", Error);
                            Insert.Log.WriteLogData(AppCode, AppName, UserId, "Insert");
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

        public bool Pay(string AppCodes = "", string AppNames = "", string Users = "", string Stores = "", string[,] Parameter = null, string Codes = "", string Details = "")
        {
            try 
            { 
                db.Operations(Stores, Parameter, out Error);

                if (Error == null)
                {
                    Mess.ShowPayment();

                    Insert.Log.WriteLogData(AppCodes, AppNames, Users, "Insert");
                    return true;
                }
                else
                {
                    Insert.Log.WriteLogData(AppCodes, AppNames, Users, "Insert");
                    return false;
                }
                  
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCodes, AppNames, Users, ex.Message);
                return false;
            }
        }
    }
}