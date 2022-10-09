using SANSANG.Constant;
using System;
using System.IO;

namespace SANSANG.Class
{
    public class clsLog
    {
        private PathConstant Location = new PathConstant();
        private clsFunction Function = new clsFunction();

        public void WriteLogData(string strAppCode, string strAppName, string strUser, string strError)
        {
            try
            {
                string appCode = "RSA";
                string appPath = Function.GetPathLocation(Location.Logfile);
                DateTime appDate = DateTime.Now;
                string appDatePath = appDate.Year.ToString() + appDate.Month.ToString() + appDate.Day.ToString("00");
                string appFilePath = @"RSALog_" + appDatePath + ".txt";
                string appLogPath = Path.Combine(appPath, appFilePath);

                if (!File.Exists(appLogPath))
                {
                    if (!Directory.Exists(appPath))
                    {
                        Directory.CreateDirectory(appPath);
                    }
                }

                StreamWriter file = new StreamWriter(appLogPath, true);
                file.Write(appCode + " [" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "] ");
                file.Write(" | ");
                file.Write("Program: " + strAppCode + " (" + strAppName + ")");
                file.Write(" | ");
                file.Write("User: " + strUser);
                file.Write(" | ");
                file.Write("Detail: " + strError);
                file.Write(Environment.NewLine);
                file.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}