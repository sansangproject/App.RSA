using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Windows.Documents;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using PdfSharp.Pdf.Content.Objects;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;
using SANSANG.Utilites.App.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace SANSANG.Class
{
    public class clsImport
    {
        public string Operations = "";
        public string Messages = "";
        public string Errors = "";
        public string AccountId = "";

        private DataTable dt = new DataTable();
        private clsLog Log = new clsLog();
        private dbConnection db = new dbConnection();
        private AccountConstant Accounts = new AccountConstant();
        private BankConstant Bank = new BankConstant();
        private clsInsert Insert = new clsInsert();
        private clsMessage Message = new clsMessage();
        private clsFunction Function = new clsFunction();
        private TableConstant Table = new TableConstant();
        private OperationConstant Operation = new OperationConstant();
        private clsDate Dates = new clsDate();
        private StoreConstant Store = new StoreConstant();

        public void ImportStatment(string Acc)
        {
            Operations = "IM";
            AccountId = Acc;
            string FileLocations = "";

            OpenFileDialog Open = new OpenFileDialog();

            Open.Filter = "Statment Files (*.txt)|*.txt";
            Open.Title = "Import Statments";

            if (Open.ShowDialog() == DialogResult.OK)
            {
                FileLocations = Open.FileName;
                Open.RestoreDirectory = true;
            }

            string Files = FileLocations;

            if (Files != "")
            {
                if (AccountId == Accounts.CIMB2484 || AccountId == Accounts.CIMB4401)
                {
                    CIMB(Files);
                }
                else if (AccountId == Accounts.KTB6064)
                {
                    KTB(Files);
                }
                else if (AccountId == Accounts.KBANK3848)
                {
                    KBANK(Files);
                }
                else if (AccountId == Accounts.BBL8716)
                {

                }
                else if (AccountId == Accounts.TMB3334)
                {
                    TMB(Files);
                }
                else if (AccountId == Accounts.SCB2378)
                {
                    SCB(Files);
                }
                else if (AccountId == Accounts.BAY5954)
                {
                    BAY(Files);
                }
            }
        }

        private void CIMB(string Files)
        {
            string DateImports = "";
            var DataList = new List<CIMBSTModel>();

            var LogFile = File.ReadAllLines(Files);
            var LogList = new List<string>(LogFile);
            int Rows = LogList.Count - 1;
            int Row = 0;

            foreach (var Value in LogList)
            {
                CIMBSTModel Data = new CIMBSTModel();
                string[] Statements = Value.Split(new char[0]);

                DateImports += Row == 0 ? Statements[0].ToString() : "";
                DateImports += Row == Rows ? " - " + Statements[0].ToString() : "";

                if (Statements[1] == "ORF" && Statements[4].Substring(0,3) == "940")
                {
                    Data.Code = Statements[1] + "+";
                }
                else
                {
                    Data.Code = Statements[1];
                }

                Data.Date = Statements[0];
                Data.Amount = Function.MoveNumberStringComma(Statements[2]);
                Data.Balance = Function.MoveNumberStringComma(Statements[3]);
                Data.Channel = Statements[4].Substring(0,6);

                DataList.Add(Data);
                Row++;
            }

            Message.MessageConfirmation("I", "Import CIMB Statment", DateImports);

            using (var Popup = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
            {
                var Result = Popup.ShowDialog();
                string Error = "";

                if (Result == DialogResult.Yes)
                {
                    Popup.Close();

                    AddCIMBStatment(DataList, AccountId, out Error);

                    if (Error == "")
                    {
                        Message.MessageResult("IM", "C", Error);
                    }
                    else
                    {
                        Message.MessageResult("IM", "ER", Error);
                    }
                }
            }
        }

        private void KTB(string Files)
        {
            string DateStart = "";
            string DateEnd = "";

            KTBSTModel data = new KTBSTModel();
            var DataList = new List<KTBSTModel>();
            var LogFile = File.ReadAllLines(Files);
            var LogList = new List<string>(LogFile);
            int countRows = LogList.Count - 1;
            int row = 1;

            foreach (var value in LogList)
            {
                string[] statements = value.Split(new char[0]);

                DateStart += row == 1 ? statements[0].ToString() : "";
                DateEnd += row == countRows ? statements[0].ToString() : "";

                if (row % 2 == 0)
                {
                    string second = (Convert.ToInt32(Convert.ToDouble(data.Balance)) % 60).ToString("D2");
                    data.Time = string.Concat(statements[0].ToString(), ":", second);
                    DataList.Add(data);
                    data = new KTBSTModel();
                }
                else
                {
                    int count = statements.Count();

                    int IndexOfPaymentStart = value.IndexOf("(") + 1;
                    string Payments = value.Remove(0, IndexOfPaymentStart);

                    int IndexOfPaymentEnd = Payments.IndexOf(")");
                    string PaymentCode = Payments.Substring(0, IndexOfPaymentEnd);

                    data.Date = statements[0].Replace("/2", "/202");
                    data.Payment = PaymentCode;
                    data.Branch = statements[count - 1];
                    data.Balance = statements[count - 2];
                    data.Amount = statements[count - 3];

                    int Start = 0;
                    int LengthOfPayment = PaymentCode.Length + 1;
                    
                    Start = value.IndexOf("(" + PaymentCode + ")") + 2;

                    string Detail = value.Remove(0, (Start + LengthOfPayment));
                    string Details = Detail.Substring(0, Detail.IndexOf(data.Amount) - 1);

                    data.Detail = Details;
                }

                row++;
            }

            Message.MessageConfirmation("I", "IMPORT KTB STATMENT", string.Concat(DateEnd, " - ", DateStart));

            using (var Popup = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
            {
                var result = Popup.ShowDialog();
                string err = "";

                if (result == DialogResult.Yes)
                {
                    Popup.Close();

                    AddKTBStatment(DataList, AccountId, out err);

                    if (err == "")
                    {
                        Message.MessageResult("IM", "C", err);
                    }
                    else
                    {
                        Message.MessageResult("IM", "ER", err);
                    }
                }
            }
        }

        private void KBANK(string strFile)
        {
            string strDateImport = "";
            int numDetail = 0;
            var dataList = new List<KBANKSTModel>();
            List<string> operation = new List<string>() { "จาก", "เพื่อชำระ", "โอนไป", "รหัสอ้างอิง" };
            var logFile = File.ReadAllLines(strFile);
            var logList = new List<string>(logFile);
            int countRows = logList.Count - 1;
            int row = 0;

            foreach (var value in logList)
            {
                KBANKSTModel data = new KBANKSTModel();
                string[] statements = value.Split(new char[0]);

                strDateImport += row == 0 ? statements[0].ToString() : "";
                strDateImport += row == countRows ? " - " + statements[0].ToString() : "";

                data.StatmentDate = Function.formatTime(statements[0], 1);
                data.StatmentTime = statements[1] + ":00";
                data.PaymentCode = statements[2];
                data.Amount = statements[3];
                data.Balance = statements[4];

                for (int i = 5; i <= 8; i++)
                {
                    if (!operation.Exists(Check => statements[i].Contains(Check)))
                    {
                        data.Branch += statements[i] + " ";
                        numDetail = i;
                    }
                    else
                    {
                        break;
                    }
                }

                for (int i = numDetail + 1; i < statements.Length; i++)
                {
                    data.Detail += statements[i] + " ";
                }

                dataList.Add(data);
                row++;
            }

            Message.MessageConfirmation("I", "IMPORT KBANK STATMENT", strDateImport);

            using (var Popup = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
            {
                var result = Popup.ShowDialog();
                string err = "";
                if (result == DialogResult.Yes)
                {
                    Popup.Close();
                    AddKBANKStatment(dataList, "KBANK", out err);

                    if (err == "")
                    {
                        Message.MessageResult("IM", "C", err);
                    }
                    else
                    {
                        Message.MessageResult("IM", "ER", err);
                    }
                }
            }
        }

        private void TMB(string strFile)
        {
            string strDateImport = "";
            string strDateImportStart = "";
            string strDateImportEnd = "";
            var dataList = new List<TMBSTModel>();
            var logFile = File.ReadAllLines(strFile);
            var logList = new List<string>(logFile);

            int countRows = logList.Count - 1;
            int row = 0;

            foreach (var value in logList)
            {
                TMBSTModel data = new TMBSTModel();
                string[] statements = value.Split(new char[0]);

                strDateImportStart = row == 0 ? statements[0].ToString() : strDateImportStart;
                strDateImportEnd = row == countRows ? statements[0].ToString() + " - " : strDateImportEnd;

                data.StatmentDate = statements[0];
                data.PaymentCode = statements[1];
                data.Detail = statements[1] + " " + statements[2] + " " + statements[3];
                data.Branch = statements[4];
                data.Amount = statements[5];
                data.Balance = statements[6];

                dataList.Add(data);
                row++;
            }

            strDateImport = strDateImportEnd + strDateImportStart;
            Message.MessageConfirmation("I", "IMPORT TMB STATMENT", strDateImport);
            dataList.Reverse();

            using (var Popup = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
            {
                var result = Popup.ShowDialog();
                string err = "";

                if (result == DialogResult.Yes)
                {
                    Popup.Close();
                    AddTMBStatment(dataList, "TMB", out err);

                    if (err == "")
                    {
                        Message.MessageResult("IM", "C", err);
                    }
                    else
                    {
                        Message.MessageResult("IM", "ER", err);
                    }
                }
            }
        }

        private void SCB(string strFile)
        {
            string strDateImport = "";
            string strDateImportStart = "";
            string strDateImportEnd = "";
            var dataList = new List<SCBSTModel>();
            var logFile = File.ReadAllLines(strFile);
            var logList = new List<string>(logFile);

            int countRows = logList.Count - 1;
            int row = 0;

            foreach (var value in logList)
            {
                SCBSTModel data = new SCBSTModel();
                string[] statements = value.Split(new char[0]);
                int Indexs = statements.Length - 1;
                string Description = "";
                strDateImportStart = row == 0 ? statements[0].ToString() : strDateImportStart;
                strDateImportEnd = row == countRows ? statements[0].ToString() : strDateImportEnd;

                data.Date = Function.formatTime(statements[0], 2);
                data.Time = statements[1];
                data.Code = statements[2];
                data.Channel = statements[3];
                data.Amount = statements[4];
                data.Balance = statements[5];

                for (int i = 6; i <= Indexs; i++)
                {
                    Description += " " + statements[i];
                }

                data.Description = Description.TrimStart();
                dataList.Add(data);
                row++;
            }

            strDateImport = strDateImportStart + " - " + strDateImportEnd;
            Message.MessageConfirmation("I", "IMPORT SCB STATMENT", strDateImport);

            using (var Popup = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
            {
                var result = Popup.ShowDialog();
                string err = "";

                if (result == DialogResult.Yes)
                {
                    Popup.Close();
                    AddSCBStatment(dataList, "SCB", out err);

                    if (err == "")
                    {
                        Message.MessageResult("IM", "C", err);
                    }
                    else
                    {
                        Message.MessageResult("IM", "ER", err);
                    }
                }
            }
        }

        private void BAY(string Files)
        {
            string DateImports = "";
            var DataList = new List<BAYSTModel>();

            var LogFile = File.ReadAllLines(Files);
            var LogList = new List<string>(LogFile);
            int Rows = LogList.Count - 1;
            int Row = 0;

            foreach (var Value in LogList)
            {
                BAYSTModel Data = new BAYSTModel();
                string[] Statements = Value.Split(new char[0]);

                DateImports += Row == 0 ? Statements[0].ToString() : "";
                DateImports += Row == Rows ? " - " + Statements[0].ToString() : "";

                int Length = (Statements.Length);
                string Details = "";

                Data.Date = Statements[0];
                Data.Time = Statements[1];
                Data.Item = Statements[2];
                Data.Amount = Function.MoveNumberStringComma(Statements[3]);
                Data.Balance = Function.MoveNumberStringComma(Statements[4]);
                Data.Channel = Statements[5];

                for (int i = 6; i < Length; i++)
                {
                    Details += i == 6 ? "" : " ";
                    Details += Statements[i];
                }

                Data.Detail = Details;

                DataList.Add(Data);
                Row++;
            }

            Message.MessageConfirmation("I", "Import BAY Statment", DateImports);

            using (var Popup = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
            {
                var Result = Popup.ShowDialog();
                string Error = "";

                if (Result == DialogResult.Yes)
                {
                    Popup.Close();

                    AddBAYStatment(DataList, AccountId, out Error);

                    if (Error == "")
                    {
                        Message.MessageResult("IM", "C", Error);
                    }
                    else
                    {
                        Message.MessageResult("IM", "ER", Error);
                    }
                }
            }
        }

        public void AddCIMBStatment(List<CIMBSTModel> Datas, string AccountId, out string Error)
        {
            try
            {
                string Codes = "";
                string PaymentId = "";
                string Items = "";
                string Displays = "";
                string Details = "";
                bool IsWithdrawal = false;
                decimal Balance = 0;
                decimal BalanceNew = 0;


                for (int Rounds = 0; Rounds < Datas.Count; Rounds++)
                {
                    Codes = Function.GetCodes(Table.StatmentId, "", "Generated");
                    Function.GetPayments(Datas[Rounds].Code, out PaymentId, out Items, out Details, out Displays, out IsWithdrawal);

                    DateTime DateTime = Convert.ToDateTime(Datas[Rounds].Date);

                    string[,] Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", Codes},
                        {"@Status", "1000"},
                        {"@User", "1004"},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.InsertAbbr},
                        {"@AccountId", AccountId},
                        {"@Date", Dates.GetDate(dt : DateTime, Format : 4)},
                        {"@Time", "00:00:" + (Rounds % 60).ToString("D2")},
                        {"@PaymentId", PaymentId},
                        {"@Item", Items},
                        {"@MoneyId", "1028"},
                        {"@Branch", ""},
                        {"@Channel", Datas[Rounds].Channel},
                        {"@Withdrawal", IsWithdrawal? Function.RemoveComma(Datas[Rounds].Amount) : "0.00"},
                        {"@Deposit", !IsWithdrawal? Function.RemoveComma(Datas[Rounds].Amount) : "0.00"},
                        {"@Balance", Datas[Rounds].Balance},
                        {"@Number", ""},
                        {"@Detail", Details},
                        {"@Display", Displays},
                        {"@Reference", ""},
                    };

                    Balance = CheckBalance(Accounts.CIMB4401, Function.MoveNumberStringComma(Datas[Rounds].Amount), IsWithdrawal);
                    BalanceNew = decimal.Parse(Function.MoveNumberStringComma(Datas[Rounds].Balance));

                    if (!Function.IsDuplicate(
                            Table.Statments,
                            Value1: "CIMB",
                            Value2: AccountId,
                            Value3: PaymentId,
                            Value4: Dates.GetDate(dt: DateTime, Format: 4),
                            Value5: Function.RemoveComma(Datas[Rounds].Amount),
                            Value6: Function.RemoveComma(Datas[Rounds].Balance)))
                    {
                        if (Balance == BalanceNew)
                        {
                            db.Operations(Store.ManageStatement, Parameter, out Error);
                            Messages = "";
                        }
                        else
                        {
                            Messages = string.Format("Balance does not match. ({0})", Balance);
                            break;
                        }
                    }
                    else
                    {
                        Messages = "Last statment is duplicate.";
                        break;
                    }
                }

                Error = Messages;
            }
            catch (Exception ex)
            {
                Log.WriteLogData("IMPORT", "CIMB", "Import", ex.Message);
                Error = ex.Message;
            }
        }

        public void AddKBANKStatment(List<KBANKSTModel> datas, string Banks, out string err)
        {
            try
            {
                string chanel = "ACK00-01";
                string strCode = "";
                string paymentCode = "";
                string paymentDetail = "";
                string paymentDisplay = "";

                Operations = "I";

                for (int i = 0; i < datas.Count; i++)
                {
                    Wait(1000);

                    Function.GetPaymentSubCode(Banks, datas[i].PaymentCode, datas[i].Branch, out paymentCode, out paymentDetail, out paymentDisplay);
                    strCode = Function.GetCodes("124", "", "Generated");
                    string deposit = Function.GetIncome(paymentCode);
                    decimal balance = 0;
                    decimal newBalance = 0;

                    string[,] Parameter = new string[,]
                    {
                        {"@User", "IMPORT"},
                        {"@StatmentCode", strCode},
                        //{"@StatmentAccounts", Accounts},
                        {"@StatmentDate", datas[i].StatmentDate},
                        {"@StatmentPayment", paymentCode},
                        {"@StatmentPaymentDetail", paymentDetail},
                        {"@StatmentChanel", chanel},
                        {"@StatmentNumber", ""},
                        {"@StatmentDetail", datas[i].Detail.TrimEnd()},
                        {"@StatmentStatus", "Y"},
                        {"@StatmentFileType", ""},
                        {"@StatmentFileLocation", "-"},
                        //{"@Bank", Accounts},
                        {"@Amount", Function.MoveNumberStringComma(datas[i].Amount)},
                        {"@WithdrawalOrDeposit", deposit},
                        {"@StatmentTime", datas[i].StatmentTime},
                        {"@StatmentBranch", datas[i].Branch.TrimEnd()},
                        {"@Balance", Function.MoveNumberStringComma(datas[i].Balance)}
                    };

                    string[,] check = new string[,]
                    {
                        //{"@StatmentAccounts", Accounts},
                        {"@StatmentDate", datas[i].StatmentDate},
                        {"@StatmentTime", datas[i].StatmentTime},
                        {"@StatmentPayment", paymentCode},
                        {"@StatmentPaymentDetail", paymentDetail},
                        {"@StatmentChanel", chanel},
                        {"@StatmentDetail", paymentDisplay},
                        {"@StatmentStatus", "Y"},
                        {"@StatmentBranch", datas[i].Branch},
                        {"@StatmentAmount", Function.MoveNumberStringComma(datas[i].Amount)},
                        {"@StatmentBalance", Function.MoveNumberStringComma(datas[i].Balance)},
                    };

                    //balance = CheckBalance(Accounts, Function.MoveNumberStringComma(datas[i].Amount), deposit);
                    newBalance = decimal.Parse(Function.MoveNumberStringComma(datas[i].Balance));

                    if (CheckDuplicate(check))
                    {
                        if (balance == newBalance)
                        {
                            db.Operations("Spr_I_TblSaveStatment", Parameter, out Errors);
                            Messages = "";
                        }
                        else
                        {
                            Messages = string.Format("Balance does not match. ({0})", balance);
                            break;
                        }
                    }
                    else
                    {
                        Messages = "Last statment is duplicate.";
                        break;
                    }
                }
                err = Messages;
            }
            catch (Exception ex)
            {
                Log.WriteLogData("IMPORT", "KBANK", "Import", ex.Message);
                err = ex.Message;
            }
        }

        public void AddTMBStatment(List<TMBSTModel> datas, string Banks, out string err)
        {
            try
            {
                string chanel = "ACK00-03";
                string strCode = "";
                string paymentCode = "";
                string paymentDetail = "";
                string paymentDisplay = "";

                Operations = "I";

                for (int i = 0; i < datas.Count; i++)
                {
                    Function.GetPaymentSubCode(Banks, datas[i].PaymentCode, datas[i].Branch, out paymentCode, out paymentDetail, out paymentDisplay);
                    strCode = Function.GetCodes("124", "", "Generated");
                    string deposit = Function.GetIncome(paymentCode);
                    decimal balance = 0;
                    decimal newBalance = 0;

                    string[,] Parameter = new string[,]
                    {
                        {"@User", "IMPORT"},
                        {"@StatmentCode", strCode},
                        //{"@StatmentAccounts", Accounts},
                        {"@StatmentDate", datas[i].StatmentDate},
                        {"@StatmentPayment", paymentCode},
                        {"@StatmentPaymentDetail", paymentDetail},
                        {"@StatmentChanel", chanel},
                        {"@StatmentNumber", ""},
                        {"@StatmentDetail", datas[i].Detail.TrimEnd()},
                        {"@StatmentStatus", "Y"},
                        {"@StatmentFileType", ""},
                        {"@StatmentFileLocation", "-"},
                        //{"@Bank", Accounts},
                        {"@Amount", Function.MoveNumberStringComma(datas[i].Amount)},
                        {"@WithdrawalOrDeposit", deposit},
                        {"@StatmentTime", Function.GetTime(1)},
                        {"@StatmentBranch", datas[i].Branch.TrimEnd()},
                        {"@Balance", Function.MoveNumberStringComma(datas[i].Balance)}
                    };

                    string[,] check = new string[,]
                    {
                        //{"@StatmentAccounts", Accounts},
                        {"@StatmentDate", datas[i].StatmentDate},
                        {"@StatmentTime", ""},
                        {"@StatmentPayment", paymentCode},
                        {"@StatmentPaymentDetail", paymentDetail},
                        {"@StatmentChanel", chanel},
                        {"@StatmentDetail", paymentDisplay},
                        {"@StatmentStatus", "Y"},
                        {"@StatmentBranch", datas[i].Branch},
                        {"@StatmentAmount", Function.MoveNumberStringComma(datas[i].Amount)},
                        {"@StatmentBalance", Function.MoveNumberStringComma(datas[i].Balance)},
                    };

                    //balance = CheckBalance(Accounts, Function.MoveNumberStringComma(datas[i].Amount), deposit);
                    newBalance = decimal.Parse(Function.MoveNumberStringComma(datas[i].Balance));

                    if (CheckDuplicate(check))
                    {
                        if (balance == newBalance)
                        {
                            db.Operations("Spr_I_TblSaveStatment", Parameter, out Errors);
                            Messages = "";
                        }
                        else
                        {
                            Messages = string.Format("Balance does not match. ({0})", balance);
                            break;
                        }
                    }
                    else
                    {
                        Messages = "Last statment is duplicate.";
                        break;
                    }
                }
                err = Messages;
            }
            catch (Exception ex)
            {
                Log.WriteLogData("IMPORT", "TMB", "Import", ex.Message);
                err = ex.Message;
            }
        }

        public void AddKTBStatment(List<KTBSTModel> Datas, string Banks, out string Error)
        {
            try
            {
                string Codes = "";
                string PaymentId = "";
                string Items = "";
                string Displays = "";
                string Details = "";
                bool IsWithdrawal = false;
                decimal Balance = 0;
                decimal BalanceNew = 0;

                for (int Rounds = (Datas.Count - 1); Rounds >= 0; Rounds--)
                {
                    Codes = Function.GetCodes(Table.StatmentId, "", "Generated");
                    Function.GetPayments(Datas[Rounds].Payment, out PaymentId, out Items, out Details, out Displays, out IsWithdrawal);

                    string[,] Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", Codes},
                        {"@Status", "1000"},
                        {"@User", "1004"},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.InsertAbbr},
                        {"@AccountId", Banks},
                        {"@Date", Dates.GetDate(dt: Convert.ToDateTime(Datas[Rounds].Date), Format: 4)},
                        {"@Time", Datas[Rounds].Time},
                        {"@PaymentId", PaymentId},
                        {"@Item", Items},
                        {"@MoneyId", "1069"},
                        {"@Branch", Datas[Rounds].Branch},
                        {"@Channel", ""},
                        {"@Withdrawal", IsWithdrawal? Function.RemoveComma(Datas[Rounds].Amount) : "0.00"},
                        {"@Deposit", !IsWithdrawal? Function.RemoveComma(Datas[Rounds].Amount) : "0.00"},
                        {"@Balance", Datas[Rounds].Balance},
                        {"@Number", ""},
                        {"@Detail", Datas[Rounds].Detail},
                        {"@Display", Details},
                        {"@Reference", ""},
                    };

                    Balance = CheckBalance(Accounts.KTB6064, Function.MoveNumberStringComma(Datas[Rounds].Amount), IsWithdrawal);
                    BalanceNew = decimal.Parse(Function.MoveNumberStringComma(Datas[Rounds].Balance));

                    if (!Function.IsDuplicate(
                            Table.Statments,
                            Value1: "KTB-IMPORT",
                            Value2: AccountId,
                            Value3: PaymentId,
                            Value4: Dates.GetDate(dt: Convert.ToDateTime(Datas[Rounds].Date), Format: 4),
                            Value5: Function.RemoveComma(Datas[Rounds].Amount),
                            Value6: Function.RemoveComma(Datas[Rounds].Balance),
                            Value7: Function.RemoveComma(Datas[Rounds].Time)))
                    {
                        if (Balance == BalanceNew)
                        {
                            db.Operations(Store.ManageStatement, Parameter, out Error);
                            Messages = string.IsNullOrEmpty(Error) ? "" : Error.ToString();
                        }
                        else
                        {
                            Messages = string.Format("Balance does not match. ({0})", Balance);
                            break;
                        }
                    }
                    else
                    {
                        Messages = "Last statment is duplicate.";
                        break;
                    }
                }

                Error = Messages;
            }
            catch (Exception ex)
            {
                Log.WriteLogData("IMPORT", "KTB", "Import", ex.Message);
                Error = ex.Message;
            }
        }

        public void AddSCBStatment(List<SCBSTModel> datas, string Banks, out string err)
        {
            try
            {
                string chanel = "SCB00-01";
                string strCode = "";
                string paymentCode = "";
                string paymentDetail = "";
                string paymentDisplay = "";

                Operations = "I";

                for (int i = 0; i < datas.Count; i++)
                {
                    Function.GetPaymentSubCode(Banks, datas[i].Code, datas[i].Channel, out paymentCode, out paymentDetail, out paymentDisplay);
                    strCode = Function.GetCodes("124", "", "Generated");
                    string deposit = Function.GetIncome(paymentCode);
                    decimal balance = 0;
                    decimal newBalance = 0;

                    string[,] Parameter = new string[,]
                    {
                        {"@User", "IMPORT"},
                        {"@StatmentCode", strCode},
                        //{"@StatmentAccounts", Accounts},
                        {"@StatmentDate", datas[i].Date},
                        {"@StatmentPayment", paymentCode},
                        {"@StatmentPaymentDetail", paymentDetail},
                        {"@StatmentChanel", chanel},
                        {"@StatmentNumber", ""},
                        {"@StatmentDetail", datas[i].Description.TrimEnd()},
                        {"@StatmentStatus", "Y"},
                        {"@StatmentFileType", ""},
                        {"@StatmentFileLocation", "-"},
                        //{"@Bank", Accounts},
                        {"@Amount", Function.MoveNumberStringComma(datas[i].Amount)},
                        {"@WithdrawalOrDeposit", deposit},
                        {"@StatmentTime", datas[i].Time + ":" + Function.GetTime(0)},
                        {"@StatmentBranch", datas[i].Channel.TrimEnd()},
                        {"@Balance", Function.MoveNumberStringComma(datas[i].Balance)}
                    };

                    string[,] check = new string[,]
                    {
                        //{"@StatmentAccounts", Accounts},
                        {"@StatmentDate", datas[i].Date},
                        {"@StatmentTime", ""},
                        {"@StatmentPayment", paymentCode},
                        {"@StatmentPaymentDetail", ""},
                        {"@StatmentChanel", chanel},

                        {"@StatmentDetail", ""},
                        {"@StatmentStatus", "Y"},
                        {"@StatmentBranch", datas[i].Channel},
                        {"@StatmentAmount", Function.MoveNumberStringComma(datas[i].Amount)},
                        {"@StatmentBalance", Function.MoveNumberStringComma(datas[i].Balance)},
                    };

                    //balance = CheckBalance(Accounts, Function.MoveNumberStringComma(datas[i].Amount), deposit);
                    newBalance = decimal.Parse(Function.MoveNumberStringComma(datas[i].Balance));

                    if (CheckDuplicate(check))
                    {
                        if (balance == newBalance)
                        {
                            db.Operations("Spr_I_TblSaveStatment", Parameter, out Errors);
                            Messages = "";
                        }
                        else
                        {
                            Messages = string.Format("Balance does not match. ({0})", balance);
                            break;
                        }
                    }
                    else
                    {
                        Messages = "Last statment is duplicate.";
                        break;
                    }
                }
                err = Messages;
            }
            catch (Exception ex)
            {
                Log.WriteLogData("IMPORT", "SCB", "Import", ex.Message);
                err = ex.Message;
            }
        }

        public void AddBAYStatment(List<BAYSTModel> Datas, string AccountId, out string Error)
        {
            try
            {
                string Codes = "";
                string PaymentId = "";
                string Item = "";
                string Detail = "";
                string Display = "";
                bool IsWithdrawal = false;

                decimal BalanceNow = 0;
                decimal BalanceNew = 0;

                for (int Rounds = 0; Rounds < Datas.Count; Rounds++)
                {
                    Codes = Function.GetCodes(Table.StatmentId, "", "Generated");
                    Function.GetPaymentByName(Datas[Rounds].Item, "1070", out PaymentId, out Item, out Detail, out Display, out IsWithdrawal);

                    DateTime DateTime = Convert.ToDateTime(Datas[Rounds].Date);

                    string[,] Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", Codes},
                        {"@Status", "1000"},
                        {"@User", "1004"},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.InsertAbbr},
                        {"@AccountId", AccountId},
                        {"@Date", Dates.GetDate(dt : DateTime, Format : 4)},
                        {"@Time", Datas[Rounds].Time},
                        {"@PaymentId", PaymentId},
                        {"@Item", "รายการเดินบัญชี"},
                        {"@MoneyId", "1192"},
                        {"@Branch", ""},
                        {"@Channel", Datas[Rounds].Channel},
                        {"@Withdrawal", IsWithdrawal? Datas[Rounds].Amount : "0.00"},
                        {"@Deposit", !IsWithdrawal? Datas[Rounds].Amount : "0.00"},
                        {"@Balance", Datas[Rounds].Balance},
                        {"@Number", ""},
                        {"@Detail", Datas[Rounds].Detail},
                        {"@Display", Display},
                        {"@Reference", ""},
                    };

                    BalanceNow = CheckBalance(Accounts.BAY5954, Function.MoveNumberStringComma(Datas[Rounds].Amount), IsWithdrawal);
                    BalanceNew = decimal.Parse(Function.MoveNumberStringComma(Datas[Rounds].Balance));

                    if (!Function.IsDuplicate(
                            Table.Statments,
                            Value1: "BAY",
                            Value2: AccountId,
                            Value3: PaymentId,
                            Value4: Dates.GetDate(dt: DateTime, Format: 4),
                            Value5: Datas[Rounds].Amount,
                            Value6: Datas[Rounds].Balance))
                    {
                        if (BalanceNow == BalanceNew)
                        {
                            db.Operations(Store.ManageStatement, Parameter, out Error);
                            Messages = "";
                        }
                        else
                        {
                            Messages = string.Format("Balance does not match. ({0})", String.Format("{0:n}", BalanceNow));
                            break;
                        }
                    }
                    else
                    {
                        Messages = string.Format("{0} | {1}{2}{3} is duplicate.", Datas[Rounds].Date, Item, Environment.NewLine, String.Format("{0:n}", BalanceNew));
                        break;
                    }
                }

                Error = Messages;
            }
            catch (Exception ex)
            {
                Log.WriteLogData("IMPORT", "BAY", "Import", ex.Message);
                Error = ex.Message;
            }
        }

        public bool CheckDuplicate(string[,] data)
        {
            try
            {
                db.Get("Spr_F_GetDuplicateStatment", data, out Errors, out dt);
                return dt.Rows.Count >= 1 ? false : true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public decimal CheckBalance(string Account, string Amount, bool IsWithdrawal)
        {
            decimal Value = 0;

            try
            {
                string[,] Parameter = new string[,]
                {
                        {"@Account", Account},
                };

                db.Get(Store.FnGetBankBalance, Parameter, out Errors, out dt);

                Value = IsWithdrawal ?
                        decimal.Parse(dt.Rows[0]["Balances"].ToString()) - decimal.Parse(Amount) :
                        decimal.Parse(dt.Rows[0]["Balances"].ToString()) + decimal.Parse(Amount);

                return Value;
            }
            catch (Exception)
            {
                return Value;
            }
        }

        public void Wait(int milliseconds)
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
    }
}