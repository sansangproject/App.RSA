using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmLogin : Form
    {
        public string Error = "";
        public string AppCode = "LOGIN00";
        public string AppName = "FrmLogin";
        public string UserId = "";
        public string UserCode = "";
        public string UserName = "";
        public string UserSurname = "";
        public string UserType = "";
        public string Sex = "";

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            btnLogin.Focus();
        }

        public void Login()
        {
            if (txtUser.Text == "")
            {
                return;
            }
            else if (txtPass.Text == "")
            {
                return;
            }
            else
            {
                try
                {
                    dbConnection db = new dbConnection();
                    clsCryption Cryption = new clsCryption();
                    clsLog Log = new clsLog();
                    DataTable dt = new DataTable();
                    StoreConstant Store = new StoreConstant();

                    string[,] Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", txtUser.Text},
                        {"@Name", ""},
                        {"@Surname", ""},
                        {"@Password", Cryption.Encrypt(txtPass.Text)},
                        {"@Email", ""},
                        {"@Type", ""},
                        {"@Status", "1000"},
                        {"@Sex", ""},
                        {"@User", ""},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", "S"},
                    };

                    db.Get(Store.ManageUser, Parameter, out Error, out dt);

                    if (string.IsNullOrEmpty(Error))
                    {
                        int intRow = dt.Rows.Count;

                        if (intRow == 1)
                        {
                            UserId = dt.Rows[0]["Id"].ToString();
                            UserCode = dt.Rows[0]["Code"].ToString();
                            UserName = dt.Rows[0]["Name"].ToString();
                            UserSurname = dt.Rows[0]["Surname"].ToString();
                            UserType = dt.Rows[0]["Type"].ToString();
                            Sex = dt.Rows[0]["Sex"].ToString();

                            Log.WriteLogData(AppCode, AppName, UserId, "Login Successfully");
                            foreach (Form var in Application.OpenForms)
                            {
                                var.Hide();
                            }

                            FrmMenu Frm = new FrmMenu(UserId, UserName, UserSurname, UserType, Sex);
                            Frm.Show();
                        }
                        else
                        {
                            clsFunction Function = new clsFunction();
                            Function.MesPassNotMat();
                            txtPass.Clear();
                            txtPass.Focus();
                            Log.WriteLogData(AppCode, AppName, UserId, "Login Fail - User Name or Password is Incorrect");
                        }
                    }
                    else
                    {
                        Log.WriteLogData(AppCode, AppName, UserId, Error);
                    }
                }
                catch (Exception ex)
                {
                    clsLog Log = new clsLog();
                    Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                }
            }

            btnLogin.Focus();
        }

        private void Login(object sender, EventArgs e)
        {
            Login();
        }

        private void Close(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FrmKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                clsFunction Fn = new clsFunction();
                string keyCode = Fn.keyPress(sender, e);

                if (keyCode == "Enter")
                {
                    Login();
                }
            }
            catch (Exception ex)
            {
                clsLog Log = new clsLog();
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }
    }
}