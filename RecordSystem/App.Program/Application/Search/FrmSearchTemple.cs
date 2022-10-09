using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmSearchTemple : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "SEATE00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string filePath = "-";
        public string fileName = "-";
        public string fileType = ".jpg";

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        public string[,] Parameter = new string[,] { };

        public FrmSearchTemple(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmSearchTemple_Load(object sender, EventArgs e)
        {
            
            List.GetList(cbbStatus, "0", "Status");
            List.GetList(cbbGeo, "0", "GeographyAll");
            List.GetList(cbbProvince, "", "ProvinceAll");
            txtSearch.Focus();
        }

        public void Clear()
        {
            try
            {
                txtSearch.Text = "";
                txtSearch.Focus();

                cbbStatus.SelectedIndex = 0;
                cbbGeo.SelectedIndex = 0;
                cbbProvince.SelectedIndex = 0;

                txtId.Text = "";
                txtName.Text = "";
                txtNameEn.Text = "";
                txtSect.Text = "";
                txtNumber.Text = "";
                txtMoo.Text = "";
                txtSoi.Text = "";
                txtRoad.Text = "";
                txtZipcode.Text = "";
                txtPhone.Text = "";
                txtWeb.Text = "";
                txtEmail.Text = "";
                txtAddress.Text = "";

                SearchData();

                cbbAmphur.SelectedIndex = 0;
                cbbTambol.SelectedIndex = 0;
            }
            catch (Exception)
            {
            }
        }

        public void getDataGrid(DataTable dt)
        {
            try
            {
                int row = dt.Rows.Count;

                if (row == 0)
                {
                    dataGridView.DataSource = null;
                    picExcel.Visible = false;
                    lblCount.Text = "0";
                }
                else
                {
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "Name", "AmphurNameTh", "ProvinceNameTh", "MsStatusNameTh", "Date", "MsTempleId");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                    Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                          "ลำดับ", 50, true, mc, mc
                        , "ชื่อ", 200, true, ml, ml
                        , "เขต / อำเภอ", 100, true, ml, ml
                        , "จังหวัด", 150, true, ml, ml
                        , "สถานะ", 100, true, mc, mc
                        , "ข้อมูล ณ วันที่", 150, true, mc, mc
                        , "", 0, false, mc, mc
                        );

                    picExcel.Visible = true;
                    lblCount.Text = Fn.setComma(dt.Rows[0]["DataRow"].ToString());
                    txtSearch.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            FrmAnimatedProgress frm = new FrmAnimatedProgress(10);

            try
            {
                if (txtSearch.Text != "")
                {
                    lblSearch.Text = txtSearch.Text;
                }
                else
                {
                    lblSearch.Text = "";
                }

                string[,] Parameter = new string[,]
                {
                    {"@MsTempleId", txtId.Text},
                    {"@MsTempleNameTh", txtName.Text},
                    {"@MsTempleNameEn", txtNameEn.Text},
                    {"@MsTempleSect", txtSect.Text},
                    {"@MsTempleNumber", txtNumber.Text},
                    {"@MsTempleMoo", txtMoo.Text},
                    {"@MsTempleSoi", txtSoi.Text},
                    {"@MsTempleRoad", txtRoad.Text},
                    {"@MsTempleDistrict", Fn.getComboBoxValue(cbbTambol)},
                    {"@MsTempleAmphur", Fn.getComboBoxValue(cbbAmphur)},
                    {"@MsTempleProvince", Fn.getComboBoxValue(cbbProvince)},
                    {"@MsTempleGeography", Fn.getComboBoxValue(cbbGeo)},
                    {"@MsTempleBuddGeo",""},
                    {"@MsTempleZipcode", txtZipcode.Text},
                    {"@MsTemplePhone1", txtPhone.Text},
                    {"@MsTemplePhone2",""},
                    {"@MsTempleFax",""},
                    {"@MsTempleWebsite", txtWeb.Text},
                    {"@MsTempleMail", txtEmail.Text},
                    {"@MsTempleMap",""},
                    {"@MsTempleStatus", Fn.getComboBoxValue(cbbStatus)},
                    {"@Find", txtSearch.Text},
                };

                frm.Show();
                db.Get("Spr_S_TblMasterTemple", Parameter, out strErr, out dt);
                getDataGrid(dt);
            }
            catch (Exception)
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            strOpe = "I";

            string[,] Parameter = new string[,]
                {
                    {"@MsTempleNameTh", txtName.Text},
                    {"@MsTempleNameEn", txtNameEn.Text},
                    {"@MsTempleSect", txtSect.Text},
                    {"@MsTempleNumber", txtNumber.Text},
                    {"@MsTempleMoo", txtMoo.Text},
                    {"@MsTempleSoi", txtSoi.Text},
                    {"@MsTempleRoad", txtRoad.Text},
                    {"@MsTempleDistrict", Fn.getComboBoxValue(cbbTambol)},
                    {"@MsTempleAmphur", Fn.getComboBoxValue(cbbAmphur)},
                    {"@MsTempleProvince", Fn.getComboBoxValue(cbbProvince)},
                    {"@MsTempleGeography", Fn.getComboBoxValue(cbbGeo)},
                    {"@MsTempleBuddGeo",""},
                    {"@MsTempleZipcode", txtZipcode.Text},
                    {"@MsTemplePhone1", txtPhone.Text},
                    {"@MsTemplePhone2",""},
                    {"@MsTempleFax",""},
                    {"@MsTempleWebsite", txtWeb.Text},
                    {"@MsTempleMail", txtEmail.Text},
                    {"@MsTempleMap",""},
                    {"@MsTempleStatus", Fn.getComboBoxValue(cbbStatus)},
                    {"@User",strUserId },
                };

            bool Action = Message.MessageConfirmation(strOpe, "วัด" + txtName.Text, "");

            if (Action == true)
            {
                db.Operations("Spr_I_TblMasterTemple", Parameter, out strErr);

                if (strErr == null)
                {
                    Message.MessageResult(strOpe, "C", strErr);
                    Clear();
                }
                else
                {
                    Message.MessageResult(strOpe, "E", strErr);
                }
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                    DataTable dt = new DataTable();

                    string[,] Parameter = new string[,]
                    {
                        {"@MsTempleId",  row.Cells["MsTempleId"].Value.ToString()},
                        {"@MsTempleNameTh",""},
                        {"@MsTempleNameEn",""},
                        {"@MsTempleSect",""},
                        {"@MsTempleNumber",""},
                        {"@MsTempleMoo",""},
                        {"@MsTempleSoi",""},
                        {"@MsTempleRoad",""},
                        {"@MsTempleDistrict","0"},
                        {"@MsTempleAmphur","0"},
                        {"@MsTempleProvince","0"},
                        {"@MsTempleGeography","0"},
                        {"@MsTempleBuddGeo",""},
                        {"@MsTempleZipcode",""},
                        {"@MsTemplePhone1",""},
                        {"@MsTemplePhone2",""},
                        {"@MsTempleFax",""},
                        {"@MsTempleWebsite",""},
                        {"@MsTempleMail",""},
                        {"@MsTempleMap",""},
                        {"@MsTempleStatus","0"},
                        {"@Find",""},
                        };

                    db.Get("Spr_S_TblMasterTemple", Parameter, out strErr, out dt);

                    txtId.Text = dt.Rows[0]["MsTempleId"].ToString();
                    cbbStatus.SelectedValue = dt.Rows[0]["MsTempleStatus"].ToString();
                    cbbGeo.SelectedValue = dt.Rows[0]["ProvinceGeo"].ToString();
                    cbbProvince.SelectedValue = dt.Rows[0]["MsTempleProvince"].ToString();
                    cbbAmphur.SelectedValue = dt.Rows[0]["MsTempleAmphur"].ToString();
                    cbbTambol.SelectedValue = dt.Rows[0]["DistrictId"].ToString();
                    txtZipcode.Text = dt.Rows[0]["AmphurPostcode"].ToString();
                    txtSoi.Text = dt.Rows[0]["MsTempleSoi"].ToString();
                    txtMoo.Text = dt.Rows[0]["MsTempleMoo"].ToString();
                    txtRoad.Text = dt.Rows[0]["MsTempleRoad"].ToString();
                    txtNumber.Text = dt.Rows[0]["MsTempleNumber"].ToString();
                    txtSect.Text = dt.Rows[0]["MsTempleSect"].ToString();
                    txtName.Text = "วัด" + dt.Rows[0]["MsTempleNameTh"].ToString();
                    txtNameEn.Text = dt.Rows[0]["MsTempleNameEn"].ToString();
                    txtEmail.Text = dt.Rows[0]["MsTempleMail"].ToString();
                    txtWeb.Text = dt.Rows[0]["MsTempleWebsite"].ToString();
                    txtPhone.Text = dt.Rows[0]["MsTemplePhone1"].ToString();
                    txtAddress.Text = "";
                    txtAddress.Text += Fn.setStringNull("วัด", "", dt.Rows[0]["MsTempleNameTh"].ToString());
                    txtAddress.Text += Fn.setStringNull("เลขที่", " ", dt.Rows[0]["MsTempleNumber"].ToString());
                    txtAddress.Text += Fn.setStringNull("", "", dt.Rows[0]["MsTempleMoo"].ToString());
                    txtAddress.Text += Fn.setStringNull("ถนน", "", dt.Rows[0]["MsTempleRoad"].ToString());
                    txtAddress.Text += Fn.setStringNull("ซอย", "", dt.Rows[0]["MsTempleSoi"].ToString());
                    txtAddress.Text += Fn.setStringNull("ตำบล", "", dt.Rows[0]["DistrictNameTh"].ToString());
                    txtAddress.Text += Fn.setStringNull("อำเภอ", "", dt.Rows[0]["AmphurNameTh"].ToString());
                    txtAddress.Text += Fn.setStringNull("จังหวัด", "", dt.Rows[0]["ProvinceNameTh"].ToString());
                    txtAddress.Text += Fn.setStringNull("รหัสไปรษณีย์", " ", dt.Rows[0]["AmphurPostcode"].ToString());

                    txtSearch.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text != "")
                {
                    strOpe = "U";

                    string[,] Parameter = new string[,]
                {
                    {"@MsTempleId", txtId.Text},
                    {"@MsTempleNameTh", txtName.Text},
                    {"@MsTempleNameEn", txtNameEn.Text},
                    {"@MsTempleSect", txtSect.Text},
                    {"@MsTempleNumber", txtNumber.Text},
                    {"@MsTempleMoo", txtMoo.Text},
                    {"@MsTempleSoi", txtSoi.Text},
                    {"@MsTempleRoad", txtRoad.Text},
                    {"@MsTempleDistrict", Fn.getComboBoxValue(cbbTambol)},
                    {"@MsTempleAmphur", Fn.getComboBoxValue(cbbAmphur)},
                    {"@MsTempleProvince", Fn.getComboBoxValue(cbbProvince)},
                    {"@MsTempleGeography", Fn.getComboBoxValue(cbbGeo)},
                    {"@MsTempleBuddGeo",""},
                    {"@MsTempleZipcode", txtZipcode.Text},
                    {"@MsTemplePhone1", txtPhone.Text},
                    {"@MsTemplePhone2",""},
                    {"@MsTempleFax",""},
                    {"@MsTempleWebsite", txtWeb.Text},
                    {"@MsTempleMail", txtEmail.Text},
                    {"@MsTempleMap",""},
                    {"@MsTempleStatus", Fn.getComboBoxValue(cbbStatus)},
                    {"@User",strUserId },
                };

                    bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

                    if (Action == true)
                    {
                        db.Operations("Spr_U_TblMasterTemple", Parameter, out strErr);

                        if (strErr == null)
                        {
                            Message.MessageResult(strOpe, "C", strErr);
                            Clear();
                        }
                        else
                        {
                            Message.MessageResult(strOpe, "E", strErr);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text != "")
                {
                    strOpe = "D";

                    string[,] Parameter = new string[,]
                        {
                           {"@MsTempleId", txtId.Text},
                        };

                    bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

                    if (Action == true)
                    {
                        db.Operations("Spr_D_TblMasterTemple", Parameter, out strErr);

                        if (strErr == null)
                        {
                            Message.MessageResult(strOpe, "C", strErr);
                            Clear();
                        }
                        else
                        {
                            Message.MessageResult(strOpe, "E", strErr);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void picExcel_Click(object sender, EventArgs e)
        {
            Fn._excelFix(dataGridView, "TEMPLES", "ชื่อวัด", "อำเภอ", "จังหวัด", "สถานะ", "ข้อมูล ณ วันที่");
        }

        private void FrmSearchTemple_KeyDown(object sender, KeyEventArgs e)
        {
            string keyCode = Fn.keyPress(sender, e);

            if (keyCode == "Ctrl+S")
            {
                btnAdd_Click(sender, e);
            }
            if (keyCode == "Ctrl+E")
            {
                btnEdit_Click(sender, e);
            }
            if (keyCode == "Ctrl+D")
            {
                btnDelete_Click(sender, e);
            }
            if (keyCode == "Altl+C")
            {
                btnClear_Click(sender, e);
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchData();
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
        }

        private void cbbGeo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.getWhereList(cbbProvince, "", cbbGeo.SelectedValue.ToString(), "Y", "ProvinceAll");
                txtZipcode.Text = "";
            }
            catch (Exception)
            {
            }
        }

        private void cbbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.getWhereList(cbbAmphur, "", cbbProvince.SelectedValue.ToString(), "Y", "AmphurAll");
                txtZipcode.Text = "";
            }
            catch (Exception)
            {
            }
        }

        private void cbbAmphur_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.getWhereList(cbbTambol, "", cbbAmphur.SelectedValue.ToString(), "Y", "TambolAll");

                txtZipcode.Text = "";
                string[,] Parameter = new string[,]
                        {
                            {"@AmphurId", cbbAmphur.SelectedValue.ToString()},
                            {"@AmphurNameTh", ""},
                            {"@AmphurNameEn", ""},
                            {"@AmphurStatus", "0"},
                            {"@AmphurPostcode", ""},
                            {"@AmphurGeo", ""},
                            {"@AmphurProvince", ""},
                        };

                db.Get("Spr_S_TblMasterAmphoe", Parameter, out strErr, out dt);
                txtZipcode.Text = dt.Rows[0]["AmphurPostcode"].ToString();
            }
            catch (Exception)
            {
            }
        }
    }
}