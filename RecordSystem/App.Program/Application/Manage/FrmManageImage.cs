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
    public partial class FrmManageImages : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string strAppCode = "IMGMAN01";
        public string strAppName = "FrmMangeImage";
        public string strErr = "";
        public string strLaguage = "EN";
        public string strOpe = "";

        public string filePath = "-";
        public string fileName = "-";
        public string fileType = ".jpg";
        public string PathFile = "";

        DataTable dt = new DataTable();
        StoreConstant Store = new StoreConstant();
        clsDelete Delete = new clsDelete();
        clsEdit Edit = new clsEdit();
        clsInsert Insert = new clsInsert();
        clsFunction Fn = new clsFunction();
        clsMessage Message = new clsMessage();
        dbConnection db = new dbConnection();
        clsDataList List = new clsDataList();
        clsLog Log = new clsLog();
        clsImage Images = new clsImage();
        TableConstant Tb = new TableConstant();

        private Timer Timer = new Timer();
        public bool Search = false;

        public string[,] Parameter = new string[,] { };

        public FrmManageImages(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            int sec = 1;
            Timer.Interval = (sec * 1000);
            Timer.Start();
            Timer.Tick += new EventHandler(LoadList);
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetList(cbbStatus, "10", "Status");
            List.GetList(cbbPath, "", "Path");

            pnMain.Enabled = true;
            Clear();
            Timer.Stop();
        }

        public void Clear()
        {
            Search = false;
            txtCode.Text = "";
            txtReferrence.Text = "";
            txtLocation.Text = "";
            txtName.Text = "";
            txtType.Text = "";
            txtSize.Text = "";
            txtCurrent.Text = "";
            lblSize.Text = "KB";

            cbbPath.SelectedValue = 0;
            cbbStatus.SelectedValue = 0;

            pbImage.Image = null;
            Images.ShowDefault(pbImage);

            Parameter = new string[,]
            {
                {"@Id", ""},
                {"@Code", Search ? txtCode.Text : ""},
                {"@Referrence", Search ? txtReferrence.Text : ""},
                {"@Path", Search ? Fn.getComboboxId(cbbPath) : "0"},
                {"@Name", Search ? txtName.Text : ""},
                {"@Type", Search ? txtType.Text : ""},
                {"@Size", Search ? txtSize.Text : ""},
                {"@SizeName", ""},
                {"@Location", Search ? txtLocation.Text : ""},
                {"@Byte", ""},
                {"@Status", Search ? Fn.getComboboxId(cbbStatus) : "0"},
                {"@User", ""},
            };

            db.Get("Store.SelectImage", Parameter, out strErr, out dt);
            GetDataGrid(dt);
            dataGridView.Focus();
        }

        public void GetDataGrid(DataTable dt)
        {
            if (Fn.GetRows(dt) == 0)
            {
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                txtNumberNow.Focus();
                txtNumberNow.Text = Fn.ShowNumberOfData(0);
            }
            else
            {
                dataGridView.DataSource = null;
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "Code", "ImageType", "ImageSize", "LocationName", "StatusName", "Id");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                  "ลำดับ", 80, true, mc, mc
                , "รหัส", 150, true, mc, mc
                , "ชนิด", 100, true, mc, mc
                , "ขนาด", 100, true, mc, ml
                , "ตำแหน่ง", 150, true, mc, ml
                , "สถานะ", 150, true, mc, ml
                , "", 0, false, mc, mc
                );

                picExcel.Visible = true;
                txtNumberNow.Text = Fn.ShowNumberOfData(dt.Rows.Count);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                if (!Fn.IsDuplicates(Tb.CheckImage, txtCode.Text, Detail: txtCode.Text + "." + txtType.Text))
                {
                    Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", txtCode.Text},
                        {"@Referrence", txtReferrence.Text == "" ? txtCode.Text : txtReferrence.Text},
                        {"@Location", txtLocation.Text},
                        {"@Name", txtName.Text},
                        {"@Type", txtType.Text},
                        {"@Size", txtSize.Text},
                        {"@SizeName", lblSize.Text},
                        {"@Path", Fn.getComboboxId(cbbPath)},
                        {"@Byte", ""},
                        {"@Status", Fn.getComboboxId(cbbStatus)},
                        {"@User", strUserId},
                    };

                    if (Insert.Add(strAppCode, strAppName, strUserId, "Store.InsertImage", Parameter, txtCode.Text, txtSize.Text + " " + lblSize.Text))
                    {
                        if (Images.ManageImage(txtCode.Text, "", out PathFile))
                        {
                            Clear();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtCode.Text))
                            {
                                Delete.Drop(strAppCode, strAppName, strUserId, 0, Tb.Image, txtCode, txtSize.Text + " " + lblSize.Text, false);
                            }
                        }
                    }
                    else
                    {
                        Clear();
                        pbImage.Image = null;
                    }
                }
            }
            else
            {
                Message.ShowRequestData();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", txtCode.Text},
                    {"@Referrence", txtReferrence.Text == "" ? txtName.Text : txtReferrence.Text},
                    {"@Location", txtLocation.Text},
                    {"@Name", txtName.Text},
                    {"@Type", txtType.Text},
                    {"@Size", txtSize.Text},
                    {"@SizeName", lblSize.Text},
                    {"@Path",Fn.getComboboxId(cbbPath)},
                    {"@Byte", ""},
                    {"@Status", Fn.getComboboxId(cbbStatus)},
                    {"@User", strUserId},
                };

                if (Edit.Update(strAppCode, strAppName, strUserId, "Store.UpdateImage", Parameter, txtCode.Text, txtSize.Text + " " + lblSize.Text))
                {
                    Clear();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                if (Delete.Drop(strAppCode, strAppName, strUserId, 0, Tb.Image, txtCode, txtSize.Text + " " + lblSize.Text))
                {
                    Clear();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            Images.SelectImage();
           
            foreach (ImageModel Img in GlobalVar.ImageDataList)
            {
                txtCode.Text = Img.Code;
                txtName.Text = Img.Name;
                txtType.Text = Img.Type;
                txtSize.Text = Convert.ToString(Img.Size);
                lblSize.Text = Img.Kind;
                txtLocation.Text = Img.Path;
                cbbStatus.SelectedValue = "Y";

                Image Picture = new Bitmap(Img.Path);
                pbImage.Image = Picture.GetThumbnailImage(150, 150, null, new IntPtr());

                break;
            }
        }

        public void SearchData(bool Search)
        {
            Parameter = new string[,]
            {
                {"@Id", ""},
                {"@Code", Search ? txtCode.Text : ""},
                {"@Referrence", Search ? txtReferrence.Text : ""},
                {"@Location", Search ? txtLocation.Text : ""},
                {"@Name", Search ? txtName.Text : ""},
                {"@Type", Search ? txtType.Text : ""},
                {"@Size", Search ? txtSize.Text : ""},
                {"@SizeName", ""},
                {"@Path", Search ? Fn.getComboboxId(cbbPath) : ""},
                {"@Byte", ""},
                {"@Status", Search ? Fn.getComboboxId(cbbStatus) : ""},
                {"@User", ""},
            };

            db.Get("Store.SelectImage", Parameter, out strErr, out dt);
            GetDataGrid(dt);
            dataGridView.Focus();
        }

        private void ShowData(DataTable dt)
        {
            try
            {
                if (Fn.GetRows(dt) > 0)
                {
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    txtCode.Text = dt.Rows[0]["Code"].ToString();
                    txtReferrence.Text = dt.Rows[0]["Referrence"].ToString();
                    txtType.Text = dt.Rows[0]["Type"].ToString();
                    txtSize.Text = dt.Rows[0]["Size"].ToString();
                    lblSize.Text = dt.Rows[0]["SizeName"].ToString();
                    cbbPath.SelectedValue = dt.Rows[0]["Path"].ToString() == "" ? "" : dt.Rows[0]["Path"].ToString();
                    cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();

                    pbImage.Image = null;

                    if (dt.Rows[0]["ImagePath"].ToString() != "")
                    {
                        txtCurrent.Text = dt.Rows[0]["ImagePath"].ToString();
                        Image Picture = new Bitmap(dt.Rows[0]["ImagePath"].ToString());
                        pbImage.Image = Picture;
                    }
                    
                    dataGridView.Focus();
                }
            }
            catch (Exception)
            {

            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                    DataTable dt = new DataTable();

                    Parameter = new string[,]
                    { 
                        {"@Id", row.Cells["Id"].Value.ToString()},
                        {"@Code", ""},
                        {"@Referrence", ""},
                        {"@Path", "0"},
                        {"@Name", ""},
                        {"@Type", ""},
                        {"@Size", ""},
                        {"@SizeName", ""},
                        {"@Location", ""},
                        {"@Byte", ""},
                        {"@Status", "0"},
                        {"@User", ""},
                    };

                    db.Get("Store.SelectImage", Parameter, out strErr, out dt);
                    ShowData(dt);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData(true);
        }

        private void cbbPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbStatus.SelectedValue = "YI";
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                txtReferrence.Text = txtName.Text;
            }
        }
    }
}