using System;
using System.Data;
using System.Drawing;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Windows.Forms;
using DevComponents.AdvTree;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.VisualBasic.ApplicationServices;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Global;
using SANSANG.Utilites.App.Model;
using Telerik.Collections.Generic;
using Telerik.WinControls;
using Telerik.WinControls.Drawing;
using Telerik.WinControls.UI.Barcode.Symbology;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SANSANG
{
    public partial class FrmMembers : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "SAVME00";
        public string AppName = "FrmMembers";
        public string Error = "";

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        private StoreConstant Store = new StoreConstant();
        private OperationConstant Operation = new OperationConstant();
        private DataListConstant DataList = new DataListConstant();
        private clsDelete Delete = new clsDelete();
        private clsEdit Edit = new clsEdit();
        private clsInsert Insert = new clsInsert();
        private clsFunction Function = new clsFunction();
        private clsMessage Message = new clsMessage();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();
        private clsImage Images = new clsImage();
        private TableConstant Table = new TableConstant();
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(20);
        private Timer Timer = new Timer();
        private clsCryption Cryption = new clsCryption();

        public string[,] Parameter = new string[,] { };
        public string strEmailUser = "";
        public string strEmailServer = "";
        public string strEmailOther = "";

        public FrmMembers(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
        {
            InitializeComponent();

            UserId = UserIdLogin;
            UserName = UserNameLogin;
            UserSurname = UserSurNameLogin;
            UserType = UserTypeLogin;
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            Loading.Show();
            Timer.Interval = (1000);
            Timer.Start();
            Timer.Tick += new EventHandler(LoadList);
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "0"));
            List.GetLists(cbbShop, string.Format(DataList.ShopId));
            List.GetLists(cbbCard, string.Format(DataList.CardId));
            List.GetList(cbbUser, string.Format(DataList.Users));

            pbMain.Enabled = true;
            Clear();
            Timer.Stop();
        }

        public void Clear()
        {
            Function.ClearAll(gbForm);
            Images.ShowDefault(pbImage);
            rdbPointAll.Checked = true;
            rdbOMail.Checked = true;
            Search(false);            
        }

        public void ShowGridView(DataTable dt)
        {
            try
            {
                if (Function.GetRows(dt) == 0)
                {
                    GridView.DataSource = null;
                    txtCount.Text = Function.ShowNumberOfData(0);
                }
                else
                {
                    GridView.DataSource = null;
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "SNo", "Shops", "Usernames", "Email", "Webs", "PointSign", "Id");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                    Function.showGridViewFormatFromStore(dtGrid, GridView,
                          " ลำดับ", 50, true, mc, mc
                        , "บริการ", 100, true, ml, ml
                        , "ผู้ใช้งาน", 150, true, ml, ml
                        , "อีเมล", 150, true, ml, ml
                        , "App / Web", 100, true, ml, ml
                        , "สะสมแต้ม", 150, true, mc, mc
                        , "", 0, false, mc, mc
                    );

                    txtCount.Text = Function.ShowNumberOfData(dt.Rows.Count);
                    GridView.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SearchData(object sender, EventArgs e)
        {
            Search(true);
        }

        public void Search(bool Search)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", Search ? txtId.Text : ""},
                    {"@Code", Search ? txtCode.Text : ""},
                    {"@Phone", Search ? txtPhone.Text : ""},
                    {"@UserName", Search ? txtUser.Text : ""},
                    {"@Name", Search ? txtName.Text : ""},
                    {"@Surname", Search ? txtSurname.Text : ""},
                    {"@Number", Search ? txtNumber.Text : ""},
                    {"@Password", ""},
                    {"@Email", Search ? rdbGMail.Checked ? "@gmail" : rdbHMail.Checked ? "@hotmail" : "" : ""},
                    {"@Status", Search ? Function.GetComboId(cbbStatus) : "0"},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@IsPoint", rdbPoint.Checked ? "1" : "0"},
                    {"@Operation", Operation.SelectAbbr},
                    {"@CardId", Search ? Function.GetComboId(cbbCard) : "0"},
                    {"@ShopId", Search ? Function.GetComboId(cbbShop) : "0"},
                    {"@UserId", Search ? Function.GetComboId(cbbUser) : "0"},
                    {"@Website", Search ? txtWeb.Text : ""},
                };

                string Condition = Function.ShowConditons(GetCondition());
                lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;
                db.Get(Store.ManageMember, Parameter, out Error, out dt);
                ShowGridView(dt);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";

                strCondition += txtCode.Text != "" ? ", รหัสอ้างอิง: " + txtCode.Text : "";
                strCondition += txtPhone.Text != "" ? ", หมายเลขโทรศัพท์: " + txtPhone.Text : "";
                strCondition += txtUser.Text != "" ? ", ผู้ใช้: " + txtUser.Text : "";
                strCondition += txtName.Text != "" ? ", ชื่อ: " + txtName.Text : "";
                strCondition += txtSurname.Text != "" ? ", นามสกุล: " + txtSurname.Text : "";
                strCondition += txtNumber.Text != "" ? ", รหัสสมาชิก: " + txtNumber.Text : "";
                strCondition += txtEmail.Text != "" ? ", อีเมล: " + txtEmail.Text : "";
                strCondition += txtWeb.Text != "" ? ", เว็บไซต์: " + txtWeb.Text : "";
                strCondition += rdbPoint.Checked ? ", สะสมแต้ม" : "";
                strCondition += Function.GetComboId(cbbCard) != "0" ? ", บัตรสมาชิก: " + cbbCard.Text : "";
                strCondition += Function.GetComboId(cbbShop) != "0" ? ", บริการ: " + cbbShop.Text : "";
                strCondition += Function.GetComboId(cbbUser) != "0" ? ", ผู้ใช้: " + cbbUser.Text : "";
                strCondition += Function.GetComboId(cbbStatus) != "0" ? ", สถานะ: " + cbbStatus.Text : "";

                return strCondition;

            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return "";
            }
        }

        private void CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.GridView.Rows[e.RowIndex];

                    Parameter = new string[,]
                    {
                        {"@Id", row.Cells["Id"].Value.ToString()},
                        {"@Code", ""},
                        {"@Phone", ""},
                        {"@UserName", ""},
                        {"@Name", ""},
                        {"@Surname", ""},
                        {"@Number", ""},
                        {"@Password", ""},
                        {"@Email", ""},
                        {"@Status", "0"},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@IsPoint", "0"},
                        {"@Operation", Operation.SelectAbbr},
                        {"@CardId", "0"},
                        {"@ShopId", "0"},
                        {"@UserId", "0"},
                        {"@Website", ""},
                    };

                    db.Get(Store.ManageMember, Parameter, out Error, out dt);
                    ShowData(dt);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void ShowData(DataTable dt)
        {
            try
            {
                if (Function.GetRows(dt) > 0)
                {
                    cbbShop.SelectedValue = dt.Rows[0]["ShopId"].ToString();
                    cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                    cbbUser.SelectedValue = dt.Rows[0]["UserId"].ToString();
                    cbbCard.SelectedValue = dt.Rows[0]["CardId"].ToString();
                    txtId.Text = dt.Rows[0]["Id"].ToString();
                    txtCode.Text = dt.Rows[0]["Code"].ToString();

                    txtName.Text = dt.Rows[0]["Names"].ToString();
                    txtSurname.Text = dt.Rows[0]["Surnames"].ToString();
                    txtNumber.Text = dt.Rows[0]["Number"].ToString();
                    txtEmail.Text = dt.Rows[0]["Email"].ToString();
                    txtPhone.Text = dt.Rows[0]["Phone"].ToString();
                    txtUser.Text = dt.Rows[0]["Usernames"].ToString();
                    txtPassword.Text = Cryption.Decrypt(dt.Rows[0]["Password"].ToString());
                    txtWeb.Text = dt.Rows[0]["Website"].ToString();
                    rdbPoint.Checked = dt.Rows[0]["Points"].ToString() == "0" ? false : true;

                    if (dt.Rows[0]["Email"].ToString().Contains("@hotmail.com"))
                    {
                        rdbHMail.Checked = true;
                    }
                    else if (dt.Rows[0]["Email"].ToString().Contains("@gmail.com"))
                    {
                        rdbGMail.Checked = true;
                    }
                    else
                    {
                        rdbOMail.Checked = true;
                    }


                    GridView.Focus();
                }
            }
            catch (Exception ex)
            {
                pbImage.Image = null;
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }
        private void AddData(object sender, EventArgs e)
        {
            try
            {
                if ((Function.GetComboId(cbbShop) != "0" || Function.GetComboId(cbbStatus) != "0")
                    && !string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtUser.Text) && !string.IsNullOrEmpty(txtEmail.Text))
                {
                    if (!Function.IsDuplicates(Table.Members, Function.GetComboId(cbbUser), Function.GetComboId(cbbShop), Detail: "Member of " + cbbShop.Text + " (" + txtUser.Text + ")"))
                    {
                        txtCode.Text = Function.GetCodes(Table.MembertId, "", "Generated");

                        Parameter = new string[,]
                        {
                            {"@Id", ""},
                            {"@Code", txtCode.Text},
                            {"@Phone", txtPhone.Text},
                            {"@UserName", txtUser.Text},
                            {"@Name", txtName.Text},
                            {"@Surname", txtSurname.Text},
                            {"@Number", txtNumber.Text},
                            {"@Password", Cryption.Encrypt(txtPassword.Text)},
                            {"@Email", txtEmail.Text},
                            {"@Status", Function.GetComboId(cbbStatus)},
                            {"@IsActive", "1"},
                            {"@IsDelete", "0"},
                            {"@IsPoint", rdbPoint.Checked ? "1" : "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@CardId", Function.GetComboId(cbbCard)},
                            {"@ShopId", Function.GetComboId(cbbShop)},
                            {"@UserId", AddUser()},
                            {"@Website", txtWeb.Text},
                        };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageMember, Parameter, txtCode.Text, Details: "Member of " + cbbShop.Text + " (" + txtUser.Text + ")"))
                        {
                            Clear();
                            List.GetList(cbbUser, string.Format(DataList.Users));
                        }
                    }
                }
                else
                {
                    Message.ShowRequestData();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void EditData(object sender, EventArgs e)
        {
            try
            {
                if ((Function.GetComboId(cbbStatus) != "0") && !string.IsNullOrEmpty(txtId.Text))
                {
                    if (UpdateUser())
                    {
                        Parameter = new string[,]
                        {
                        {"@Id", txtId.Text},
                        {"@Code", txtCode.Text},
                        {"@Phone", txtPhone.Text},
                        {"@UserName", txtUser.Text},
                        {"@Name", txtName.Text},
                        {"@Surname", txtSurname.Text},
                        {"@Number", txtNumber.Text},
                        {"@Password", Cryption.Encrypt(txtPassword.Text)},
                        {"@Email", txtEmail.Text},
                        {"@Status", Function.GetComboId(cbbStatus)},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@IsPoint", rdbPoint.Checked ? "1" : "0"},
                        {"@Operation", Operation.UpdateAbbr},
                        {"@CardId", Function.GetComboId(cbbCard)},
                        {"@ShopId", Function.GetComboId(cbbShop)},
                        {"@UserId", Function.GetComboId(cbbUser)},
                        {"@Website", txtWeb.Text},
                        };

                        if (Edit.Update(AppCode, AppName, UserId, Store.ManageMember, Parameter, txtCode.Text, Details: "Member of " + cbbShop.Text + " (" + txtUser.Text + ")"))
                        {
                            Clear();
                            List.GetList(cbbUser, string.Format(DataList.Users));
                        }
                        else
                        {
                            Message.ShowRequestData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void DeleteData(object sender, EventArgs e)
        {
            try
            {
                if (Delete.Drop(AppCode, AppName, UserId, 0, Table.Members, txtCode, Details: "Member of " + cbbShop.Text + " (" + txtUser.Text + ")"))
                {
                    Clear();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void ClearData(object sender, EventArgs e)
        {
            Clear();
        }

        private void FrmKeyDown(object sender, KeyEventArgs e)
        {
            string keyCode = Function.KeyPress(sender, e);

            if (keyCode == "Ctrl+S")
            {
                AddData(sender, e);
            }
            if (keyCode == "Ctrl+E")
            {
                EditData(sender, e);
            }
            if (keyCode == "Ctrl+D")
            {
                DeleteData(sender, e);
            }
            if (keyCode == "Altl+C")
            {
                ClearData(sender, e);
            }
            if (keyCode == "Enter")
            {
                Search(true);
            }
        }

        private void cbbShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Function.IsDefault(cbbShop))
            {
                Images.Show(pbImage, Function.GetComboId(cbbShop));
                cbbStatus.SelectedValue = "1000";
            }
            else
            {
                Images.ShowDefault(pbImage);
                cbbStatus.SelectedValue = "0";
            }

            btnSearch.Focus();
        }

        private void cbShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = cbShowPass.Checked ? '\0' : '*';
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (txtPhone.Text != "")
            {
                txtPhone.Text = Function.ConvertPhoneNumber(txtPhone.Text);
            }
        }

        private void rdChecked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                string select = ((RadioButton)sender).Name;

                if (txtEmail.Text.Contains("@"))
                {
                    int indexOf = txtEmail.Text.IndexOf("@");
                    int length = txtEmail.Text.Length;
                    int size = length - indexOf;
                    strEmailUser = txtEmail.Text.Substring(0, indexOf);

                    if (txtEmail.Text.Contains("hotmail") || txtEmail.Text.Contains("gmail"))
                    {
                        strEmailServer = txtEmail.Text.Substring(indexOf, size);
                    }
                    else
                    {
                        strEmailOther = txtEmail.Text.Substring(indexOf, size);
                    }
                }
                else
                {
                    strEmailUser = txtEmail.Text;
                }

                if (select == "rdbHMail")
                {
                    txtEmail.Text = strEmailUser + "@hotmail.com";
                }
                else if (select == "rdbGMail")
                {
                    txtEmail.Text = strEmailUser + "@gmail.com";
                }
                else
                {
                    txtEmail.Text = strEmailUser + strEmailOther;
                }
            }
        }

        private string AddUser()
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", txtUser.Text},
                    {"@Name", txtName.Text},
                    {"@Surname", txtSurname.Text},
                    {"@Password", Cryption.Encrypt(txtPassword.Text)},
                    {"@Email", txtEmail.Text},
                    {"@Type", "1001"},
                    {"@Status", Function.GetComboId(cbbStatus)},
                    {"@Sex", "A"},
                    {"@User", "1000"},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.InsertAbbr}
                };

                return Insert.Add(AppCode, AppName, UserId, Store.ManageUser, Parameter);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return "";
            }
        }

        private bool UpdateUser()
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", Function.GetComboId(cbbUser)},
                    {"@Code", txtUser.Text},
                    {"@Name", txtName.Text},
                    {"@Surname", txtSurname.Text},
                    {"@Password", Cryption.Encrypt(txtPassword.Text)},
                    {"@Email", txtEmail.Text},
                    {"@Type", "1001"},
                    {"@Status", Function.GetComboId(cbbStatus)},
                    {"@Sex", "A"},
                    {"@User", "1000"},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.UpdateAbbr}
                };

                return string.IsNullOrEmpty(Edit.Update(AppCode, AppName, UserId, Store.ManageUser, Parameter))? true : false;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return false;
            }
        }
    }
}