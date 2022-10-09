using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmSearchProvince : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "SEAPR00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string filePath = "";
        public string fileName = "-";
        public string fileType = ".jpg";
        public string strAddress = "";

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();

        private clsSearch Search = new clsSearch();
        private clsFunction Fn = new clsFunction();
        private clsInsert Insert = new clsInsert();
       private dbConnection db = new dbConnection();
        private clsMessage Message = new clsMessage();
        private clsDataList List = new clsDataList();

        public string[,] Parameter = new string[,] { };

        public FrmSearchProvince(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmSearchProvince_Load(object sender, EventArgs e)
        {
            
            List.GetList(cbbStatus, "0", "StatusAll");
            List.GetList(cbbGeography, "0", "GeographyAll");
            List.getWhereList(cbbProvince, "", "0", "Y", "ProvinceAll");
            List.getWhereList(cbbAmphur, "", "0", "Y", "AmphurAll");
            List.getWhereList(cbbTambol, "", "0", "Y", "TambolAll");
            Clear();
        }

        public void Clear()
        {
            cbbStatus.SelectedValue = 0;
            cbbGeography.SelectedValue = 0;
            cbbProvince.SelectedValue = 0;
            cbbAmphur.SelectedValue = 0;
            cbbTambol.SelectedValue = 0;

            txtZip.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
            txtSearch.Focus();
            lblSearch.Text = "";
            dataGridView.DataSource = null;
        }

        public void getDataGrid(DataTable dt)
        {
            int row;

            try
            {
                row = dt.Rows.Count;
            }
            catch (Exception)
            {
                row = 0;
            }

            if (row == 0)
            {
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                txtSearch.Focus();
                lblCount.Text = "0";
            }
            else
            {
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "DistrictNameTh", "AmphurNameTh", "ProvinceNameTh", "GeographyNameTh", "AmphurPostcode", "DistrictId");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                      "ลำดับ", 50, true, mc, mc
                    , "ตำบล", 100, true, ml, ml
                    , "อำเภอ", 150, true, ml, ml
                    , "จังหวัด", 150, true, ml, ml
                    , "ภูมิภาค", 100, true, ml, ml
                    , "รหัสไปรษณีย์", 100, true, mc, mc
                    , "", 0, false, mc, mc
                    );

                picExcel.Visible = true;
                lblCount.Text = row.ToString();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                    DataTable dt = new DataTable();

                    Parameter = new string[,]
                     {
                        {"@Status", "0"},
                        {"@Geography", "0"},
                        {"@Province", "0"},
                        {"@Amphur", "0"},
                        {"@Tambol", row.Cells["DistrictId"].Value.ToString()},
                        {"@Zip", ""},
                        {"@Find", ""},
                     };

                    db.Get("Spr_S_TblMasterProvince", Parameter, out strErr, out dt);

                    cbbStatus.SelectedValue = dt.Rows[0]["DistrictStatus"].ToString();
                    cbbGeography.SelectedValue = dt.Rows[0]["GeographyId"].ToString();
                    cbbProvince.SelectedValue = dt.Rows[0]["ProvinceId"].ToString();
                    cbbAmphur.SelectedValue = dt.Rows[0]["AmphurId"].ToString();
                    cbbTambol.SelectedValue = dt.Rows[0]["DistrictId"].ToString();

                    txtZip.Text = dt.Rows[0]["AmphurPostcode"].ToString();

                    string strAddress = "";
                    strAddress += ":: ข้อมูล ::" + "\r\n";
                    strAddress += "ตำบล" + dt.Rows[0]["DistrictNameTh"].ToString() + "\r\n";
                    strAddress += "อำเภอ" + dt.Rows[0]["AmphurNameTh"].ToString() + "\r\n";
                    strAddress += "จังหวัด" + dt.Rows[0]["ProvinceNameTh"].ToString() + "\r\n";
                    strAddress += "รหัสไปรษณีย์ " + dt.Rows[0]["AmphurPostcode"].ToString() + "\r\n";
                    txtAddress.Text = strAddress;
                    txtSearch.Text = "";
                    txtSearch.Focus();
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
        }

        public void SearchData()
        {
            txtAddress.Text = "";

            try
            {
                Parameter = new string[,]
                {
                    {"@Status", cbbStatus.SelectedValue.ToString()},
                    {"@Geography", cbbGeography.SelectedValue.ToString()},
                    {"@Province", cbbProvince.SelectedValue.ToString()},
                    {"@Amphur", cbbAmphur.SelectedValue.ToString()},
                    {"@Tambol", cbbTambol.SelectedValue.ToString()},
                    {"@Zip", txtZip.Text},
                    {"@Find", txtSearch.Text},
                };

                db.Get("Spr_S_TblMasterProvince", Parameter, out strErr, out dt);
                getDataGrid(dt);
                lblSearch.Text = getTextFind();
            }
            catch (Exception)
            {
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                txtSearch.Focus();
                lblCount.Text = "0";
                lblSearch.Text = "";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void cbbGeography_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.getWhereList(cbbProvince, "", Fn.selectedValue(cbbGeography), "Y", "ProvinceAll");
                txtZip.Text = "";
            }
            catch (Exception)
            {
            }
        }

        private void cbbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.getWhereList(cbbAmphur, "", Fn.selectedValue(cbbProvince), "Y", "AmphurAll");
                txtZip.Text = "";
            }
            catch (Exception)
            {
            }
        }

        private void cbbAmphur_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.getWhereList(cbbTambol, "", Fn.selectedValue(cbbAmphur), "Y", "TambolAll");

                txtZip.Text = "";
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
               // txtZip.Text = dt.Rows[0]["AmphurPostcode"].ToString();
            }
            catch (Exception)
            {
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnSearch);
        }

        private void txtZip_KeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnSearch);
        }

        public string getTextFind()
        {
            string strFind = "";

            if (txtSearch.Text != "")
            {
                strFind += txtSearch.Text + " ";
            }

            if (cbbGeography.SelectedValue.ToString() != "0")
            {
                strFind += "ภูมิภาค : " + cbbGeography.Text + " ";
            }

            if (cbbProvince.SelectedValue.ToString() != "0")
            {
                strFind += " จังหวัด : " + cbbProvince.Text + " ";
            }

            if (cbbAmphur.SelectedValue.ToString() != "0")
            {
                strFind += " อำเภอ : " + cbbAmphur.Text + " ";
            }

            if (cbbTambol.SelectedValue.ToString() != "0")
            {
                strFind += " ตำบล : " + cbbTambol.Text + " ";
            }

            if (txtZip.Text != "")
            {
                strFind += " รหัสไปรษณีย์ : " + txtZip.Text;
            }

            return strFind;
        }

        private void picCopy_Click(object sender, EventArgs e)
        {
            strAddress = "";
            strAddress += "ตำบล" + cbbTambol.Text + " ";
            strAddress += "อำเภอ" + cbbAmphur.Text + " ";
            strAddress += "จังหวัด" + cbbProvince.Text + " ";
            strAddress += "รหัสไปรษณีย์ " + txtZip.Text;
            copyToClipboard();
        }

        protected void copyToClipboard()
        {
            Thread clipboardThread = new Thread(somethingToRunInThread);
            clipboardThread.SetApartmentState(ApartmentState.STA);
            clipboardThread.IsBackground = false;
            clipboardThread.Start();
        }

        public void somethingToRunInThread()
        {
            System.Windows.Forms.Clipboard.SetText(strAddress);
        }

        private void cbbTambol_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}