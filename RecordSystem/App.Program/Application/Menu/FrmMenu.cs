using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using SANSANG.Class;
using SANSANG.Database;
using SANSANG.Constant;
using System.Collections.Generic;

namespace SANSANG
{
    public partial class FrmMenu : Form
    {
        public string AppCode = "MENUS00";
        public string AppName = "FrmMenu";
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;
        public string Sex;
        public string PathMenu;
        public string PathDebt;
        public string PathStatment;
        public string Error;

        private Timer Timer = new Timer();

        private DataSet ds = new DataSet();

        private dbConnection db = new dbConnection();
        private StoreConstant Store = new StoreConstant();
        private clsFunction Function = new clsFunction();
        private clsSetting Setting = new clsSetting();
        private clsImage Images = new clsImage();
        private ImageConstant Photo = new ImageConstant();
        private GenderConstant Gender = new GenderConstant();

        public FrmMenu(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin, string UserSexLogin)
        {
            InitializeComponent();

            PathMenu = Setting.GetImageLocation();
            UserId = UserIdLogin;
            UserName = UserNameLogin;
            UserSurname = UserSurNameLogin;
            UserType = UserTypeLogin;
            Sex = UserSexLogin;
        }

        public void FrmLoad(object sender, EventArgs e)
        {
            LoadMenuItem();
            lblDisplayName.Text = (UserName + " " + UserSurname).ToUpper();
            lblDisplayDate.Text = DateTime.Now.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("en-EN")).ToUpper();

            if (Sex == Gender.Man)
            {
                Images.ShowImage(pbUserSex, Id: Photo.Man);
            }
            else if (Sex == Gender.Women)
            {
                Images.ShowImage(pbUserSex, Id: Photo.Women);
            }
            else
            {
                Images.ShowImage(pbUserSex, Id: Photo.Unisex);
            }

            if (UserType == "0")
            {
                lblDisplayPosition.Text = "Administrator";
                tsslName.Text = UserName + " " + UserSurname + " (Administrator)";
            }
            else
            {
                lblDisplayPosition.Text = "User";
                tsslName.Text = UserName + " " + UserSurname + " (User)";
            }

            Timer.Interval = 1000;
            Timer.Tick += new EventHandler(Tick);
            Timer.Start();
        }

        public void Tick(object sender, EventArgs e)
        {
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;

            string time = "";

            if (hh < 10)
            {
                time += "0" + hh;
            }
            else
            {
                time += hh;
            }

            time += ":";

            if (mm < 10)
            {
                time += "0" + mm;
            }
            else
            {
                time += mm;
            }

            time += ":";

            if (ss < 10)
            {
                time += "0" + ss;
            }
            else
            {
                time += ss;
            }

            lblTime.Text = time;
        }

        public void RDDBMenu0_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void LoadMenuItem()
        {
            DataTable dtMenu = new DataTable();
            DataTable dtMain = new DataTable();

            int Row = 0;
            int Number = 0;

            try
            {
                string[,] Param = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", ""},
                    {"@Name", ""},
                    {"@Form", ""},
                    {"@NameEn", ""},
                    {"@Display", ""},
                    {"@Type", ""},
                    {"@Main", ""},
                    {"@Sub", ""},
                    {"@Status", "1000"},
                    {"@User", ""},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", "S"},
                };

                db.Gets(Store.ManageMenu, Param, out Error, out ds);

                dtMenu = ds.Tables[0];
                dtMain = ds.Tables[1];
                Row = Function.GetRows(dtMain);

                List<int> Mains = new List<int>();

                for (int i = 0; i < Row; i++)
                {
                    Number = Convert.ToInt32(dtMain.Rows[i]["Main"].ToString());
                    Mains.Add(Number);
                }

                foreach (var Item in Mains)
                {
                    AddItem(dtMenu, Item);
                }
                
            }
            catch
            {

            }
        }

        public void AddItem(DataTable Menu, int Number)
        {
            DataTable dtMenu = new DataTable();

            dtMenu = Menu.AsEnumerable().Where(r => r.Field<int>("Main") == Number).CopyToDataTable();
            int row = Function.GetRows(dtMenu);

            for (int i = 1; i <= row; i++)
            {
                try
                {
                    RadMenuItem newMenuItem = new RadMenuItem();

                    newMenuItem.AccessibleDescription = "MenuItem" + Number + i;
                    newMenuItem.AccessibleName = "MenuItem" + Number + i;
                    newMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

                    try
                    {
                        newMenuItem.Image = Image.FromFile(PathMenu + "MenuItem" + Number + ".jpg");
                    }
                    catch (Exception)
                    {
                        newMenuItem.Image = Image.FromFile(PathMenu + "Non" + ".jpg");
                    }

                    newMenuItem.Name = dtMenu.Rows[i - 1]["Code"].ToString();
                    newMenuItem.Text = dtMenu.Rows[i - 1]["Display"].ToString();
                    newMenuItem.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    newMenuItem.Font = new Font("Mitr Light", 9.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(222)));
                    newMenuItem.Click += new EventHandler(MenuItemSelect);

                    if (Number == 1)
                    {
                        RDDBMenu1.Items.Add(newMenuItem);
                    }
                    else if (Number == 2)
                    {
                        RDDBMenu2.Items.Add(newMenuItem);
                    }
                    else if (Number == 3)
                    {
                        RDDBMenu3.Items.Add(newMenuItem);
                    }
                    else if (Number == 4)
                    {
                        RDDBMenu4.Items.Add(newMenuItem);
                    }
                    else if (Number == 5)
                    {
                        RDDBMenu5.Items.Add(newMenuItem);
                    }
                    else if (Number == 6)
                    {
                        RDDBMenu6.Items.Add(newMenuItem);
                    }
                    else if (Number == 7)
                    {
                        RDDBMenu7.Items.Add(newMenuItem);
                    }
                    else if (Number == 8)
                    {
                        RDDBMenu8.Items.Add(newMenuItem);
                    }
                    else if (Number == 9)
                    {
                        RDDBMenu9.Items.Add(newMenuItem);
                    }
                    else if (Number == 10)
                    {
                        RDDBMenu10.Items.Add(newMenuItem);
                    }
                    else if (Number == 11)
                    {
                        RDDBMenu11.Items.Add(newMenuItem);
                    }
                    else if (Number == 12)
                    {
                        RDDBMenu12.Items.Add(newMenuItem);
                    }
                    else if (Number == 13)
                    {
                        RDDBMenu13.Items.Add(newMenuItem);
                    }
                    else if (Number == 14)
                    {
                        RDDBMenu14.Items.Add(newMenuItem);
                    }
                    else if (Number == 15)
                    {
                        RDDBMenu15.Items.Add(newMenuItem);
                    }
                    else if (Number == 16)
                    {
                        RDDBMenu16.Items.Add(newMenuItem);
                    }
                    else if (Number == 17)
                    {
                        RDDBMenu17.Items.Add(newMenuItem);
                    }
                    else if (Number == 18)
                    {
                        RDDBMenu18.Items.Add(newMenuItem);
                    }
                    else if (Number == 19)
                    {
                        RDDBMenu19.Items.Add(newMenuItem);
                    }
                    else if (Number == 20)
                    {
                        RDDBMenu20.Items.Add(newMenuItem);
                    }
                    else if (Number == 21)
                    {
                        RDDBMenu21.Items.Add(newMenuItem);
                    }
                }
                catch
                {

                }
            }
        }

        public void MenuItemSelect(object sender, EventArgs e)
        {
            clsLog Log = new clsLog();

            if (((RadMenuItem)sender).Name == "LOGIN00")
            {
                FrmLogin Frm = new FrmLogin();
                Frm.Show();
                this.Hide();
            }
            else if (((RadMenuItem)sender).Name == "SAVAD00")
            {
                FrmAddress Frm = new FrmAddress(UserId, UserName, UserSurname, UserType);
                Frm.Show();
            }
            else if (((RadMenuItem)sender).Name == "SAVTK00")
            {
                FrmTracks Frm = new FrmTracks(UserId, UserName, UserSurname, UserType);
                Frm.Show();
            }
            else if (((RadMenuItem)sender).Name == "SAVTP00")
            {
                FrmTrackPost Frm = new FrmTrackPost(UserId, UserName, UserSurname, UserType);
                Frm.Show();
            }
            else if (((RadMenuItem)sender).Name == "SAVEP00")
            {
                FrmExpenses Frm = new FrmExpenses(UserId, UserName, UserSurname, UserType);
                Frm.Show();
            }
            else if (((RadMenuItem)sender).Name == "SAVDP00")
            {
                FrmDebts Frm = new FrmDebts(UserId, UserName, UserSurname, UserType);
                Frm.Show();
            }
            else if (((RadMenuItem)sender).Name == "MANLG00")
            {
                FrmMangeLogo Frm = new FrmMangeLogo(UserId, UserName, UserSurname, UserType);
                Frm.Show();
            }
            else if (((RadMenuItem)sender).Name == "MANSH00")
            {
                FrmMangeShop Frm = new FrmMangeShop(UserId, UserName, UserSurname, UserType);
                Frm.Show();
            }
            else if (((RadMenuItem)sender).Name == "SAVCR00")
            {
                FrmCredits Frm = new FrmCredits(UserId, UserName, UserSurname, UserType);
                Frm.Show();
            }
            else if (((RadMenuItem)sender).Name == "SAVEA00")
            {
                FrmElectricitys Frm = new FrmElectricitys(UserId, UserName, UserSurname, UserType);
                Frm.Show();
            }
            else if (((RadMenuItem)sender).Name == "SAVSM00")
            {
                FrmStatements Frm = new FrmStatements(UserId, UserName, UserSurname, UserType);
                Frm.Show();
            }
            else if (((RadMenuItem)sender).Name == "SAVPR00")
            {
                FrmProducts Frm = new FrmProducts(UserId, UserName, UserSurname, UserType);
                Frm.Show();
            }

            Log.WriteLogData("MENU", ((RadMenuItem)sender).Name, UserId, "Open");

        }
    }
}