using SANSANG.Database;
using SANSANG.Utilites.App.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace SANSANG.Class
{
    public class clsStatement
    {
        public static readonly Dictionary<string, StatementConstant> BankConfigs =
        new Dictionary<string, StatementConstant>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "BAY",
                    new StatementConstant
                    {
                        Password = "02121989",
                        Pattern = @"\d{2}\/\d{2}\/\d{4} \d{2}:\d{2}:\d{2}\s+([\u0E00-\u0E7F\w.]+(?:\s+[\u0E00-\u0E7F\w.]+)*)\s+([\d,]+\.\d{2})\s+([\d,]+\.\d{2})(?:\s+([A-Z]+)\s+(.+))?"
                    }
                },
                {
                    "KBANK",
                    new StatementConstant
                    {
                        Password = "02121989",
                        Pattern = @"\d{2}-\d{2}-\d{2} \d{2}:\d{2} ([^\s]+) \d{1,3}(,\d{3})*\.\d{2} \d{1,3}(,\d{3})*\.\d{2} .*"
                    }
                },
                {
                    "KTB",
                    new StatementConstant
                    {
                        Password = "1770200059906",
                        Pattern = @"([0-9]{2}\/[0-9]{2}\/[0-9]{2}) ([A-Za-z\s\-]+(?: \(\w+\))?)\s([^:]{1,100})(\d{2}):(\d{2})"
                    }
                },
                {
                    "SCB",
                    new StatementConstant
                    {
                        Password = "02121989",
                        Pattern = @"(?m)^\d{2}/\d{2}/\d{2}\s+\d{2}:\d{2}\s+\w+\s+\w+\s+\d[\d,]*\.\d{2}\s+\d[\d,]*\.\d{2}.*$"
                    }
                },
                {
                    "TTB",
                    new StatementConstant
                    {
                        Password = "1770200059906",
                        Pattern = @"(\d{1,2})\s+(\w{3})\s+(\d{2})\s+(\d{2}:\d{2})\s+(.+?)\s+([+-]?[\d,]+\.\d{2})\s+([\d,]+\.\d{2})\s+(.+)"
                    }
                }
            };
    }
}