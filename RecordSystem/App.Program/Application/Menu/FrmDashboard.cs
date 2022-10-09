using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmDashboard : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string PathMenu;
        public string PathDebt;
        public string PathStatment;
        public string strErr;

        public Int32 intMenuAll = 0;

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();

        public FrmDashboard(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            PathMenu = Fn.getImagePath("P0-MENUPICTUR-0", "", "");
            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            LoadMenuItem();

            if (strUserType == "0")
            {
                tsslName.Text = strUserName + " " + strUserSurname + "[Administrator]";
            }
            else
            {
                tsslName.Text = strUserName + " " + strUserSurname + "[User]";
            }
        }

        private void RDDBMenu0_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadMenuItem()
        {
            clsSearch clsSerch = new clsSearch();
            DataTable dtMenuAll = new DataTable();
            try
            {
                string[,] para = new string[,]
                {
                    {"@MsMenuCode", ""},
                    {"@MsMenuStatus", "0"},
                    {"@MsMenuId", ""},
                    {"@MsMenuNameEn", ""},
                    {"@MsMenuNameTh", ""},
                    {"@MsMenuDisplay", ""},
                    {"@MsMenuType", ""},
                    {"@MsMenuMain", ""},
                    {"@MsMenuSub", ""},
                };

                db.GetDataTable("Spr_S_TblMasterMenu", para, out strErr, out dtMenuAll, 1);
                intMenuAll = Convert.ToInt32(dtMenuAll.Rows[0]["MsMenuMain"].ToString()) + 1;

                int countMenuAll = intMenuAll;

                string t = "";

                if (strUserType == "0")
                {
                    t = "";
                }
                else
                {
                    t = "01";
                }

                for (int i = 1; i < countMenuAll; i++)
                {
                    DataTable dtMenu = new DataTable();

                    string[,] Parameter = new string[,]
                {
                    {"@MsMenuCode", ""},
                    {"@MsMenuStatus", "Y"},
                    {"@MsMenuId", ""},
                    {"@MsMenuNameEn", ""},
                    {"@MsMenuNameTh", ""},
                    {"@MsMenuDisplay", ""},
                    {"@MsMenuType", t},
                    {"@MsMenuMain", Convert.ToString(i)},
                    {"@MsMenuSub", ""},
                };
                    db.Get("Spr_S_TblMasterMenu", Parameter, out strErr, out dtMenu);
                    AddMenuItems(dtMenu, i);
                }
            }
            catch (Exception)
            {
            }
        }

        private void AddMenuItems(DataTable menuData, int number)
        {
            int row = menuData.Rows.Count;

            for (int i = 1; i <= row; i++)
            {
                try
                {
                    Telerik.WinControls.UI.RadMenuItem newMenuItem = new RadMenuItem();

                    newMenuItem.AccessibleDescription = "MenuItem" + number + i;
                    newMenuItem.AccessibleName = "MenuItem" + number + i;
                    newMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    try
                    {
                        newMenuItem.Image = Image.FromFile(PathMenu + "MenuItem" + number + ".jpg");
                    }
                    catch (Exception)
                    {
                        newMenuItem.Image = Image.FromFile(PathMenu + "Non" + ".jpg");
                    }

                    newMenuItem.Name = menuData.Rows[i - 1]["MsMenuCode"].ToString();
                    newMenuItem.Text = menuData.Rows[i - 1]["MsMenuDisplay"].ToString();
                    newMenuItem.Visibility = Telerik.WinControls.ElementVisibility.Visible;
                    newMenuItem.Click += new System.EventHandler(MenuItemSelect);

                    if (number == 1)
                    {
                        //RDDBMenu1.Items.Add(newMenuItem);
                    }
                    else if (number == 2)
                    {
                        //RDDBMenu2.Items.Add(newMenuItem);
                    }
                    else if (number == 3)
                    {
                        //RDDBMenu3.Items.Add(newMenuItem);
                    }
                    else if (number == 4)
                    {
                        //RDDBMenu4.Items.Add(newMenuItem);
                    }
                    else if (number == 5)
                    {
                        //RDDBMenu5.Items.Add(newMenuItem);
                    }
                    else if (number == 6)
                    {
                        //RDDBMenu6.Items.Add(newMenuItem);
                    }
                    else if (number == 7)
                    {
                        //RDDBMenu7.Items.Add(newMenuItem);
                    }
                    else if (number == 8)
                    {
                        //RDDBMenu8.Items.Add(newMenuItem);
                    }
                    else if (number == 9)
                    {
                        //RDDBMenu9.Items.Add(newMenuItem);
                    }
                    else if (number == 10)
                    {
                        //RDDBMenu10.Items.Add(newMenuItem);
                    }
                    else if (number == 11)
                    {
                        //RDDBMenu11.Items.Add(newMenuItem);
                    }
                    else if (number == 12)
                    {
                        //RDDBMenu12.Items.Add(newMenuItem);
                    }
                    else if (number == 13)
                    {
                        //RDDBMenu13.Items.Add(newMenuItem);
                    }
                    else if (number == 14)
                    {
                        //RDDBMenu14.Items.Add(newMenuItem);
                    }
                    else if (number == 15)
                    {
                        //RDDBMenu15.Items.Add(newMenuItem);
                    }
                    else if (number == 16)
                    {
                        //RDDBMenu16.Items.Add(newMenuItem);
                    }
                    else if (number == 17)
                    {
                        //RDDBMenu17.Items.Add(newMenuItem);
                    }
                    else if (number == 18)
                    {
                        //RDDBMenu18.Items.Add(newMenuItem);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void MenuItemSelect(object sender, EventArgs e)
        {
            clsLog Log = new clsLog();

            if (((RadMenuItem)sender).Name == "MANUS00")
            {
                FrmMangeUser Frm = new FrmMangeUser(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MANME00")
            {
                FrmMangeMenu Frm = new FrmMangeMenu(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MANPA00")
            {
                FrmManagePay Frm = new FrmManagePay(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MANSU00")
            {
                FrmManagePaySub Frm = new FrmManagePaySub(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MANUN00")
            {
                FrmMangeUnit Frm = new FrmMangeUnit(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MANAC00")
            {
                FrmManageAccount Frm = new FrmManageAccount(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MANBA00")
            {
                FrmManageBank Frm = new FrmManageBank(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MANBR00")
            {
                FrmManageBranch Frm = new FrmManageBranch(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SEAPR00")
            {
                FrmSearchProvince Frm = new FrmSearchProvince(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MANAD00")
            {
                FrmManageAddress Frm = new FrmManageAddress(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MANCRE00")
            {
                FrmManageCard Frm = new FrmManageCard(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MANMO00")
            {
                FrmMangeMoney Frm = new FrmMangeMoney(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MANTY00")
            {
                FrmManageProductType Frm = new FrmManageProductType(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SAVLO00")
            {
                FrmSaveLogo Frm = new FrmSaveLogo(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SAVDE00")
            {
                FrmSaveDebt Frm = new FrmSaveDebt(strUserId, strUserName, strUserSurname, strUserType, PathDebt);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SEATE00")
            {
                FrmSearchTemple Frm = new FrmSearchTemple(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SAVEX00")
            {
                FrmSaveExpense Frm = new FrmSaveExpense(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SAVMO00")
            {
                FrmSaveMoney Frm = new FrmSaveMoney(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SAVST00")
            {
                FrmSaveStatment Frm = new FrmSaveStatment(strUserId, strUserName, strUserSurname, strUserType, PathStatment);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SAVSTP00")
            {
                FrmSaveStamp Frm = new FrmSaveStamp(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "KNWBNK00")
            {
                FrmKnowledgeBanknote Frm = new FrmKnowledgeBanknote(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "CHAUSR00")
            {
                FrmLogin Frm = new FrmLogin();
                Frm.Show();
                this.Hide();
            }
            if (((RadMenuItem)sender).Name == "SETPAT00")
            {
                FrmSettingPath Frm = new FrmSettingPath(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SEEMOV00")
            {
                FrmSeeMovie Frm = new FrmSeeMovie(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SAVMEM00")
            {
                FrmSaveMember Frm = new FrmSaveMember(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SAVCRE00")
            {
                FrmSaveCredit Frm = new FrmSaveCredit(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SAVFLY00")
            {
                FrmSaveFlight Frm = new FrmSaveFlight(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SEAHOS00")
            {
                FrmSearchHospital Frm = new FrmSearchHospital(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SEASCH00")
            {
                FrmSearchSchool Frm = new FrmSearchSchool(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "RPTMAI00")
            {
                FrmReportPage Frm = new FrmReportPage(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "APPLOA00")
            {
                FrmCalLoan Frm = new FrmCalLoan(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MWASA00")
            {
                FrmMWASA00 Frm = new FrmMWASA00(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MWAMA00")
            {
                FrmMWAMA00 Frm = new FrmMWAMA00(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MWARE00")
            {
                FrmMWARE00 Frm = new FrmMWARE00(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "PATMA00")
            {
                FrmPATMA00 Frm = new FrmPATMA00(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "DENSA00")
            {
                FrmDENSA00 Frm = new FrmDENSA00(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MANST00")
            {
                FrmMangeStatus Frm = new FrmMangeStatus(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "DENRE00")
            {
                FrmDENRE00 Frm = new FrmDENRE00(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "APPBCO00")
            {
                FrmBarQRCode Frm = new FrmBarQRCode(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SENEL01")
            {
                //FrmEnvelopes Frm = new FrmEnvelopes(strUserId, strUserName, strUserSurname, strUserType);
                //Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MEASA00")
            {
                FrmMEASA00 Frm = new FrmMEASA00(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MEAMA00")
            {
                FrmMEAMA00 Frm = new FrmMEAMA00(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "MEARE00")
            {
                FrmMEARE00 Frm = new FrmMEARE00(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "SETDAE00")
            {
                FrmChkError Frm = new FrmChkError(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "IMGCBA64")
            {
                
            }
            if (((RadMenuItem)sender).Name == "SAREB00")
            {
                FrmSaveRebate Frm = new FrmSaveRebate(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "IMGMAN00")
            {
                FrmImportImage Frm = new FrmImportImage("", strUserId, strUserName, strUserSurname, strUserType, "", "", "", "");
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "APPID00")
            {
                FrmAPPID00 Frm = new FrmAPPID00(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }
            if (((RadMenuItem)sender).Name == "PDUMAN00")
            {
                FrmProduct Frm = new FrmProduct(strUserId, strUserName, strUserSurname, strUserType);
                Frm.Show();
            }

            Log.WriteLogData("MENU", ((RadMenuItem)sender).Name, strUserId, "Open");
        }
    }
}