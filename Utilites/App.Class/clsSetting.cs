using System;
using System.IO;
using System.Linq;
using System.Xml;
using SANSANG.Utilites.App.Model;

namespace SANSANG.Class
{
    public class clsSetting
    {
        public string result = "";
        public string url = "";
        public string appPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10);
        public string filePath = @"Setting.xml";

        public string GetOperationDate()
        {
            string Value = "";

            string PathApp = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10);
            string PathFile = @"Setting.xml";

            string PathSetting = Path.Combine(PathApp, PathFile);

            if (File.Exists(PathSetting))
            {
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(PathSetting);
                foreach (XmlNode ValueNode in XmlDoc.DocumentElement)
                {
                    switch (ValueNode.Name.ToString())
                    {
                        case "OperationDate":
                            Value = Convert.ToString(ValueNode.InnerText);
                            break;
                    }
                }
            }
            return Value;
        }

        public int GetNumberPrint()
        {
            string Value = "";

            string PathApp = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10);
            string PathFile = @"Setting.xml";

            string PathSetting = Path.Combine(PathApp, PathFile);

            if (File.Exists(PathSetting))
            {
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(PathSetting);
                foreach (XmlNode ValueNode in XmlDoc.DocumentElement)
                {
                    switch (ValueNode.Name.ToString())
                    {
                        case "NumberPrint":
                            Value = Convert.ToString(ValueNode.InnerText);
                            break;
                    }
                }
            }
            return Convert.ToInt32(Value);
        }

        public string GetCreditDate()
        {
            string Value = "";

            string PathApp = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10);
            string PathFile = @"Setting.xml";

            string PathSetting = Path.Combine(PathApp, PathFile);

            if (File.Exists(PathSetting))
            {
                XmlDocument XmlDoc = new XmlDocument();
                XmlDoc.Load(PathSetting);
                foreach (XmlNode ValueNode in XmlDoc.DocumentElement)
                {
                    switch (ValueNode.Name.ToString())
                    {
                        case "CreditDate":
                            Value = Convert.ToString(ValueNode.InnerText);
                            break;
                    }
                }
            }
            return Value;
        }













        public SettingModel ReadSettingXml()
        {
            SettingModel set = new SettingModel();
            set.Result = true;
            set.Message = "";
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
            }
            return set;
        }

        public void SaveSettingXml(string deviceid, string dateformat)
        {
            string settingPath = Path.Combine(appPath, filePath);
            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                xmlDoc.Save(settingPath);
            }
        }

        public string ReadPasswordSetting()
        {
            string passWord = string.Empty;
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
            }

            return passWord;
        }

        public SettingModel LoadSettingXml()
        {
            SettingModel set = new SettingModel();
            set.Result = true;
            set.Message = "";

            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
            }

            return set;
        }

        public static string ReadLanguageSetting()
        {
            try
            {
                string AppPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10);
                string FilePath = @"Setting.xml";

                string language = string.Empty;
                string SettingPath = Path.Combine(AppPath, FilePath);

                if (File.Exists(SettingPath))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(SettingPath);

                    foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                    {
                        switch (valueNode.Name.ToString())
                        {
                            case "Language":
                                language = valueNode.InnerText;
                                break;
                        }
                    }
                }
                return language;
            }
            catch
            {
                return "en";
            }
        }

        public static int DateFormat()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10);
            string filePath = @"Setting.xml";
            int DateFormat = 0;
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "DateFormat":
                            DateFormat = Convert.ToInt32(valueNode.InnerText);
                            break;
                    }
                }
            }

            return DateFormat;
        }

        public static int GetInstallments()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10);
            string filePath = @"Setting.xml";

            int installments = 0;
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "Installments":
                            installments = Convert.ToInt32(valueNode.InnerText);
                            break;
                    }
                }
            }

            return installments;
        }

        public string GetAccounts()
        {
            string accounts = "";
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "Account":
                            accounts = valueNode.InnerText;
                            break;
                    }
                }
            }

            return accounts;
        }

        public string GetWallet()
        {
            string wallets = "";
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "Wallet":
                            wallets = valueNode.InnerText;
                            break;
                    }
                }
            }

            return wallets;
        }

        public decimal GetOutstanding()
        {
            decimal balance = 0;
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "Outstanding":
                            balance = Convert.ToDecimal(valueNode.InnerText);
                            break;
                    }
                }
            }

            return balance;
        }


        public decimal GetCreditLimit()
        {
            decimal limit = 0;
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "CreditLimit":
                            limit = Convert.ToDecimal(valueNode.InnerText);
                            break;
                    }
                }
            }

            return limit;
        }

        public decimal GetInaccurate()
        {
            decimal limit = 0;
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "Inaccurate":
                            limit = Convert.ToDecimal(valueNode.InnerText);
                            break;
                    }
                }
            }

            return limit;
        }

        public string GetUrlAPIToken()
        {
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "UrlAPIToken":
                            url = valueNode.InnerText;
                            break;
                    }
                }
            }

            return url;
        }

        public string GetUrlAPITrack()
        {
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "UrlAPITrack":
                            url = valueNode.InnerText;
                            break;
                    }
                }
            }

            return url;
        }

        public string GetTrackToken()
        {
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "TrackToken":
                            result = valueNode.InnerText;
                            break;
                    }
                }
            }

            return result;
        }

        public string GetCredit()
        {
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "Credit":
                            result = valueNode.InnerText;
                            break;
                    }
                }
            }

            return result;
        }

        public string GetRoundPayment()
        {
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "RoundPayment":
                            url = valueNode.InnerText;
                            break;
                    }
                }
            }

            return url;
        }

        public string GetDatabase()
        {
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "Database":
                            url = valueNode.InnerText;
                            break;
                    }
                }
            }

            return url;
        }

        public string GetImageLocation()
        {
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "MenuPath":
                            url = valueNode.InnerText;
                            break;
                    }
                }
            }

            return url;
        }

        public bool GetDisplay()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10);
            string filePath = @"Setting.xml";

            bool display = true;
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "NextDisplay":
                            display = Convert.ToBoolean(valueNode.InnerText);
                            break;
                    }
                }
            }

            return display;
        }

        public decimal GetGoldReceived()
        {
            decimal balance = 0;
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    switch (valueNode.Name.ToString())
                    {
                        case "GoldReceived":
                            balance = Convert.ToDecimal(valueNode.InnerText);
                            break;
                    }
                }
            }

            return balance;
        }

        public string GetRequestList()
        {
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    if (valueNode.Name == "Request")
                    {
                        var array = valueNode.InnerText
                            .Split(',')
                            .Select(s => s.Trim().Trim('"'))
                            .ToArray();

                        return string.Join(", ", array);
                    }
                }
            }

            return string.Empty;
        }

        public string GetAdjustTransactionList()
        {
            string settingPath = Path.Combine(appPath, filePath);

            if (File.Exists(settingPath))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(settingPath);
                foreach (XmlNode valueNode in xmlDoc.DocumentElement)
                {
                    if (valueNode.Name == "AdjustTransaction")
                    {
                        var array = valueNode.InnerText
                            .Split(',')
                            .Select(s => s.Trim().Trim('"'))
                            .ToArray();

                        return string.Join(", ", array);
                    }
                }
            }

            return string.Empty;
        }
    }
}