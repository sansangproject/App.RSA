using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Global;
using SANSANG.Utilites.App.Model;

namespace SANSANG
{
    public partial class FrmManageLogo : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "MANLG00";
        public string AppName = "FrmMangeLogo";
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

        public string[,] Parameter = new string[,] { };

        public FrmManageLogo(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
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
            List.GetLists(cbbType, string.Format(DataList.StatusId, "5"));
            List.GetLists(cbbPath, DataList.PathId);

            pbMain.Enabled = true;
            Clear();
            Timer.Stop();
        }

        public void Clear()
        {
            Function.ClearAll(gbForm);
            Images.ShowDefault(pbLogo);
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
                    dtGrid = dt.DefaultView.ToTable(true, "SNo", "Code", "NameEn", "Web", "TypeName", "Dates", "Id");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Function.showGridViewFormatFromStore(dtGrid, GridView,
                      " ลำดับ", 50, true, mc, mc
                    , "รหัส", 100, true, ml, ml
                    , "โลโก้", 150, true, ml, ml
                    , "เว็บไซต์", 150, true, ml, ml
                    , "ประเภท", 100, true, ml, ml
                    , "ข้อมูล ณ วันที่", 150, true, mc, mc
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
                    {"@Name", Search ? txtName.Text : ""},
                    {"@Status", Search ? Function.GetComboId(cbbStatus) : "0"},
                    {"@User", Search ? "" : ""},
                    {"@IsActive", Search ? "" : ""},
                    {"@IsDelete", Search ? "" : ""},
                    {"@Operation", Operation.SelectAbbr},
                    {"@NameEn", Search ? txtNameEn.Text : ""},
                    {"@Web", Search ? txtWeb.Text : ""},
                    {"@FileName", Search ? txtFileName.Text : ""},
                    {"@FileType", Search ? txtFileType.Text : ""},
                    {"@FileLocation", Search ? txtLocation.Text : ""},
                    {"@PathId", Search ? Function.GetComboId(cbbPath) : "0"},
                    {"@TypeId", Search ? Function.GetComboId(cbbType) : "0"},
                };

                string Condition = Function.ShowConditons(GetCondition());
                lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;
                db.Get(Store.ManageLogo, Parameter, out Error, out dt);
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
                strCondition += Function.GetComboId(cbbType) != "0" ? ", ประเภท: " + cbbType.Text : "";
                strCondition += txtName.Text != "" ? ", ชื่อโลโก้: " + txtName.Text : "";
                strCondition += txtNameEn.Text != "" ? ", ชื่อโลโก้ (อังกฤษ): " + txtNameEn.Text : "";
                strCondition += txtFileName.Text != "" ? ", ชื่อไฟล์: " + txtFileName.Text + txtFileType.Text : "";
                strCondition += txtWeb.Text != "" ? ", เว็บ: " + txtWeb.Text : "";
                strCondition += Function.GetComboId(cbbPath) != "0" ? ", ตำแหน่ง: " + cbbPath.Text : "";
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow RowIndex = this.GridView.Rows[e.RowIndex];

                Parameter = new string[,]
                {
                    {"@Id", RowIndex.Cells["Id"].Value.ToString()},
                    {"@Code", ""},
                    {"@Name", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", ""},
                    {"@IsDelete", ""},
                    {"@Operation", Operation.SelectAbbr},
                    {"@NameEn", ""},
                    {"@Web", ""},
                    {"@FileName", ""},
                    {"@FileType", ""},
                    {"@FileLocation", ""},
                    {"@PathId", "0"},
                    {"@TypeId", "0"},
                };

                db.Get(Store.ManageLogo, Parameter, out Error, out dt);
                ShowData(dt);
            }
        }

        private void ShowData(DataTable dt)
        {
            try
            {
                if (Function.GetRows(dt) > 0)
                {
                    cbbType.SelectedValue = dt.Rows[0]["TypeId"].ToString();
                    cbbPath.SelectedValue = dt.Rows[0]["PathId"].ToString();
                    cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                    txtId.Text = dt.Rows[0]["Id"].ToString();
                    txtCode.Text = dt.Rows[0]["Code"].ToString();
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    txtNameEn.Text = dt.Rows[0]["NameEn"].ToString();
                    txtWeb.Text = dt.Rows[0]["Web"].ToString();
                    txtFileName.Text = dt.Rows[0]["FileName"].ToString();
                    txtFileType.Text = dt.Rows[0]["FileType"].ToString();
                    txtFolder.Text = dt.Rows[0]["Locations"].ToString();
                    txtLocation.Text = dt.Rows[0]["Locations"].ToString();

                    Image Picture = new Bitmap(txtLocation.Text);
                    pbLogo.Image = Picture.GetThumbnailImage(150, 150, null, new IntPtr());

                    GridView.Focus();
                }
            }
            catch (Exception ex)
            {
                pbLogo.Image = null;
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }      
        }
        private void AddData(object sender, EventArgs e)
        {
            try
            {
                if ((Function.GetComboId(cbbType) != "0" || Function.GetComboId(cbbPath) != "0") && !string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtLocation.Text))
                {
                    if (!Function.IsDuplicates(Table.Logo, txtName.Text, Function.GetComboId(cbbType), Detail: "Logo " + txtName.Text))
                    {
                        txtCode.Text = Function.GetCodes(Table.ExpenseId, "1014", "Generated");

                        Parameter = new string[,]
                        {
                            {"@Id", ""},
                            {"@Code", txtCode.Text },
                            {"@Name", txtName.Text},
                            {"@Status", Function.GetComboId(cbbStatus)},
                            {"@User", UserId},
                            {"@IsActive", Function.GetComboId(cbbStatus) == "1000"? "1" : "0"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@NameEn", txtNameEn.Text},
                            {"@Web", txtWeb.Text},
                            {"@FileName", txtFileName.Text},
                            {"@FileType", txtFileType.Text},
                            {"@FileLocation", txtLocation.Text},
                            {"@PathId", Function.GetComboId(cbbPath)},
                            {"@TypeId", Function.GetComboId(cbbType)},
                        };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageLogo, Parameter, txtCode.Text, Details: "Logo " + txtNameEn.Text))
                        {
                            Clear();
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
                if (txtId.Text != "")
                {
                    Parameter = new string[,]
                    {
                        {"@Id", txtId.Text},
                        {"@Code", txtCode.Text },
                        {"@Name", txtName.Text},
                        {"@Status", Function.GetComboId(cbbStatus)},
                        {"@User", UserId},
                        {"@IsActive", Function.GetComboId(cbbStatus) == "1000"? "1" : "0"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.UpdateAbbr},
                        {"@NameEn", txtNameEn.Text},
                        {"@Web", txtWeb.Text},
                        {"@FileName", txtFileName.Text},
                        {"@FileType", txtFileType.Text},
                        {"@FileLocation", txtLocation.Text},
                        {"@PathId", Function.GetComboId(cbbPath)},
                        {"@TypeId", Function.GetComboId(cbbType)},
                    };

                    if (Edit.Update(AppCode, AppName, UserId, Store.ManageLogo, Parameter, txtCode.Text, Details: "Logo " + txtNameEn.Text))
                    {
                        Clear();
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
                if (Delete.Drop(AppCode, AppName, UserId, 0, Table.Logo, txtCode, Details: "Logo " + txtNameEn.Text, true))
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

        private void Browse(object sender, EventArgs e)
        {
            Images.SelectImage();

            foreach (ImageModel Photo in GlobalVar.ImageDataList)
            {
                txtFileName.Text = Photo.Name;
                txtFileType.Text = Photo.Type;
                txtLocation.Text = Photo.Path;
                txtFolder.Text = "";
                cbbStatus.SelectedValue = "1000";
                cbbPath.SelectedValue = "1040";

                Image Picture = new Bitmap(Photo.Path);
                pbLogo.Image = Picture.GetThumbnailImage(150, 150, null, new IntPtr());

                break;
            }

            txtFolder.Focus();
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
    }
}