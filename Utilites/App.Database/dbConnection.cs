using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace SANSANG.Database
{
    public partial class dbConnection
    {
        public void Get(string ProcedureName, string[,] Parameter, out string err, out DataTable dt)
        {
            int number = Parameter.Length / 2;
            string db = GetDatabase();
            string Config = System.Configuration.ConfigurationManager.ConnectionStrings[db].ConnectionString;
            SqlConnection Connect = new SqlConnection();

            try
            {
                Connect.ConnectionString = Config;
                Connect.Open();

                SqlCommand cmd = new SqlCommand(ProcedureName, Connect);
                cmd.CommandType = CommandType.StoredProcedure;

                var strParameters = new string[Parameter.Length];
                int r = 0;

                for (int i = 0; i < number; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (r < Parameter.Length)
                        {
                            strParameters[r] = Parameter[i, j];
                            r++;
                        }
                    }
                }

                int xA = 0;
                int xB = 1;

                for (int value = 1; value <= number; value++)
                {
                    cmd.Parameters.Add(new SqlParameter(strParameters[xA], strParameters[xB]));

                    xA = xA + 2;
                    xB = xA + 1;
                }

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                da.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                dt = null;
                err = ex.ToString();
                Connect.Close();
            }
            finally
            {
                err = null;
                Connect.Close();
            }
        }

        public void GetDataTable(string ProcedureName, string[,] Parameter, out string err, out DataTable dt, int index)
        {
            try
            {
                int number = Parameter.Length / 2;
                string db = GetDatabase();

                SqlConnection Connect = new SqlConnection();
                string Config = System.Configuration.ConfigurationManager.ConnectionStrings[db].ConnectionString;

                Connect.ConnectionString = Config;
                Connect.Open();

                SqlCommand cmd = new SqlCommand(ProcedureName, Connect);
                cmd.CommandType = CommandType.StoredProcedure;

                var strParameters = new string[Parameter.Length];
                int r = 0;

                for (int i = 0; i < number; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (r < Parameter.Length)
                        {
                            strParameters[r] = Parameter[i, j];
                            r++;
                        }
                    }
                }

                int xA = 0;
                int xB = 1;

                for (int value = 1; value <= number; value++)
                {
                    cmd.Parameters.Add(new SqlParameter(strParameters[xA], strParameters[xB]));

                    xA = xA + 2;
                    xB = xA + 1;
                }

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                da.Fill(ds);
                dt = ds.Tables[index];
                err = null;
            }
            catch (Exception ex)
            {
                dt = null;
                err = ex.ToString();
            }
        }

        public void GetReport(string ProcedureName, string[,] Parameter, out string err, out DataSet ds)
        {
            try
            {
                int Number = Parameter.Length / 2;
                string db = GetDatabase();

                SqlConnection Connect = new SqlConnection();
                String Config = System.Configuration.ConfigurationManager.ConnectionStrings[db].ConnectionString;

                Connect.ConnectionString = Config;
                Connect.Open();

                SqlCommand cmd = new SqlCommand(ProcedureName, Connect);
                cmd.CommandType = CommandType.StoredProcedure;

                var strParameters = new string[Parameter.Length];
                int r = 0;

                for (int i = 0; i < Number; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (r < Parameter.Length)
                        {
                            strParameters[r] = Parameter[i, j];
                            r++;
                        }
                    }
                }

                int xA = 0;
                int xB = 1;

                for (int value = 1; value <= Number; value++)
                {
                    cmd.Parameters.Add(new SqlParameter(strParameters[xA], strParameters[xB]));

                    xA = xA + 2;
                    xB = xA + 1;
                }

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsa = new DataSet();

                da.Fill(dsa);
                ds = dsa;
                err = null;
            }
            catch (Exception ex)
            {
                ds = null;
                err = ex.ToString();
            }
        }

        public void GetList(string ProcedureName, string[,] Parameter, out string err, out DataSet ds)
        {
            try
            {
                int Number = Parameter.Length / 2;
                string db = GetDatabase();

                SqlConnection Connect = new SqlConnection();
                String Config = System.Configuration.ConfigurationManager.ConnectionStrings[db].ConnectionString;

                Connect.ConnectionString = Config;
                Connect.Open();

                SqlCommand cmd = new SqlCommand(ProcedureName, Connect);
                cmd.CommandType = CommandType.StoredProcedure;

                var strParameters = new string[Parameter.Length];
                int r = 0;

                for (int i = 0; i < Number; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (r < Parameter.Length)
                        {
                            strParameters[r] = Parameter[i, j];
                            r++;
                        }
                    }
                }

                int xA = 0;
                int xB = 1;

                for (int value = 1; value <= Number; value++)
                {
                    cmd.Parameters.Add(new SqlParameter(strParameters[xA], strParameters[xB]));

                    xA = xA + 2;
                    xB = xA + 1;
                }

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dsa = new DataSet();

                da.Fill(dsa);
                ds = dsa;
                err = null;
            }
            catch (Exception ex)
            {
                ds = null;
                err = ex.ToString();
            }
        }

        public void Operations(string ProcedureName, string[,] Parameter, out string err)
        {
            try
            {
                int number = Parameter.Length / 2;
                string db = GetDatabase();

                SqlConnection Connect = new SqlConnection();
                string Config = System.Configuration.ConfigurationManager.ConnectionStrings[db].ConnectionString;

                Connect.ConnectionString = Config;
                Connect.Open();

                SqlCommand cmd = new SqlCommand(ProcedureName, Connect);
                cmd.CommandType = CommandType.StoredProcedure;

                var strParameters = new string[Parameter.Length];
                int r = 0;

                for (int i = 0; i < number; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (r < Parameter.Length)
                        {
                            strParameters[r] = Parameter[i, j];
                            r++;
                        }
                    }
                }

                int xA = 0;
                int xB = 1;

                for (int value = 1; value <= number; value++)
                {
                    cmd.Parameters.Add(new SqlParameter(strParameters[xA], strParameters[xB]));

                    xA = xA + 2;
                    xB = xA + 1;
                }

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                err = null;
            }
            catch (Exception ex)
            {
                err = ex.ToString();
            }
        }

        public void Operation(string ProcedureName, string[,] Parameter, out string err, out DataTable dt)
        {
            int number = Parameter.Length / 2;
            string db = GetDatabase();
            string Config = System.Configuration.ConfigurationManager.ConnectionStrings[db].ConnectionString;
            SqlConnection Connect = new SqlConnection();

            try
            {
                Connect.ConnectionString = Config;
                Connect.Open();

                SqlCommand cmd = new SqlCommand(ProcedureName, Connect);
                cmd.CommandType = CommandType.StoredProcedure;

                var strParameters = new string[Parameter.Length];
                int r = 0;

                for (int i = 0; i < number; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (r < Parameter.Length)
                        {
                            strParameters[r] = Parameter[i, j];
                            r++;
                        }
                    }
                }

                int xA = 0;
                int xB = 1;

                for (int value = 1; value <= number; value++)
                {
                    cmd.Parameters.Add(new SqlParameter(strParameters[xA], strParameters[xB]));

                    xA = xA + 2;
                    xB = xA + 1;
                }

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                da.Fill(ds);
                dt = ds.Tables[0];
                err = null;
                Connect.Close();
            }
            catch (Exception ex)
            {
                dt = null;
                err = ex.ToString();
                Connect.Close();
            }
        }

        public string GetDatabase()
        {
            string AppPath = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.Length - 10);
            string FilePath = @"Setting.xml";
            string settingPath = Path.Combine(AppPath, FilePath);
            string url = "";

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

        public void Gets(string ProcedureName, string[,] Parameter, out string err, out DataSet ds)
        {
            int number = Parameter.Length / 2;
            string db = GetDatabase();
            string Config = System.Configuration.ConfigurationManager.ConnectionStrings[db].ConnectionString;
            SqlConnection Connect = new SqlConnection();

            try
            {
                Connect.ConnectionString = Config;
                Connect.Open();

                SqlCommand cmd = new SqlCommand(ProcedureName, Connect);
                cmd.CommandType = CommandType.StoredProcedure;

                var strParameters = new string[Parameter.Length];
                int r = 0;

                for (int i = 0; i < number; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        if (r < Parameter.Length)
                        {
                            strParameters[r] = Parameter[i, j];
                            r++;
                        }
                    }
                }

                int xA = 0;
                int xB = 1;

                for (int value = 1; value <= number; value++)
                {
                    cmd.Parameters.Add(new SqlParameter(strParameters[xA], strParameters[xB]));

                    xA = xA + 2;
                    xB = xA + 1;
                }

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ds = new DataSet();

                da.Fill(ds);
                err = null;
                Connect.Close();
            }
            catch (Exception ex)
            {
                ds = null;
                err = ex.ToString();
                Connect.Close();
            }
        }

    }
}