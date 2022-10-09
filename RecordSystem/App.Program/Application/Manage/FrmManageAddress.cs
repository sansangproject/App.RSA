using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using SANSANG.Class; 
using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmManageAddress : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "MANAD00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";
        public string strAddress = "";

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

        public FrmManageAddress(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmManageAddress_Load(object sender, EventArgs e)
        {
            

            List.GetList(cbbStatus, "0", "Status");
            List.GetList(cbbGeography, "Y", "Geography");
            Clear();
        }

        public void Clear()
        {
            cbbStatus.SelectedValue = "0";
            cbbTambol.SelectedValue = "0";
            cbbAmphur.SelectedValue = "0";
            cbbProvince.SelectedValue = "0";
            cbbGeography.SelectedValue = "0";

            txtCode.Text = "";
            txtNumber.Text = "";
            txtBuilding.Text = "";
            txtMoo.Text = "";
            txtSoi.Text = "";
            txtRoad.Text = "";
            txtPhone.Text = "";
            txtZip.Text = "";

            string[,] Parameter = new string[,]
                {
                    {"@AddressId", ""},
                    {"@AddressCode", ""},
                    {"@AddressNumber", ""},
                    {"@AddressMoo", ""},
                    {"@AddressBuilding", ""},
                    {"@AddressSoi", ""},
                    {"@AddressRoad", ""},
                    {"@AddressDistrict", "0"},
                    {"@AddressAmphur", "0"},
                    {"@AddressProvince", "0"},
                    {"@AddressGeo", "0"},
                    {"@AddressPostcode", ""},
                    {"@AddressPhone", ""},
                    {"@AddressStatus", "0"},
                };

            db.Get("Spr_S_TblSaveAddress", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        public void getDataGrid(DataTable dt)
        {
            int row = 0;
            if (dt.Rows.Count != 0)
            {
                row = dt.Rows.Count;
            }

            if (row == 0)
            {
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                lblCount.Text = "0";
            }
            else
            {
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "AddressCode", "strAddress", "ProvinceNameTh", "AmphurPostcode", "MsStatusNameTh", "strDate");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                      "ลำดับ", 30, true, mc, mc
                    , "รหัส", 90, true, mc, mc
                    , "ที่อยู่", 420, true, ml, ml
                    , "จังหวัด", 70, true, ml, ml
                    , "รหัสไปรษณีย์", 65, true, ml, ml
                    , "สถานะ", 45, true, ml, ml
                    , "ข้อมูล ณ วันที่", 120, true, ml, ml
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

        private void picExcel_Click(object sender, EventArgs e)
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            strOpe = "D";

            string[,] Parameter = new string[,]
                {
                {"@AddressCode", txtCode.Text},
                {"@DeleteType", "0"},
                {"@User", strUserId},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_D_TblSaveAddress", Parameter, out strErr);

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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            strOpe = "U";

            string[,] Parameter = new string[,]
                {
                    {"@User", strUserId },
                    {"@AddressCode", txtCode.Text },
                    {"@AddressNumber", txtNumber.Text },
                    {"@AddressMoo", txtMoo.Text },
                    {"@AddressBuilding", txtBuilding.Text },
                    {"@AddressSoi", txtSoi.Text },
                    {"@AddressRoad", txtRoad.Text },
                    {"@AddressDistrict", cbbTambol.SelectedValue.ToString() },
                    {"@AddressAmphur", cbbAmphur.SelectedValue.ToString() },
                    {"@AddressProvince", cbbProvince.SelectedValue.ToString() },
                    {"@AddressGeo", cbbGeography.SelectedValue.ToString() },
                    {"@AddressPostcode", txtZip.Text },
                    {"@AddressPhone", txtPhone.Text },
                    {"@AddressStatus", cbbStatus.SelectedValue.ToString() },
                    {"@AddressName","" },
                    {"@AddressMap",txtMap.Text },
                    {"@AddressSend","2" },
                    {"@Address","" },
                    {"@AddressHome","" },
                    {"@AddressRoom","" },
                    {"@AddressFloor","" },
                    {"@AddressPrefix","" },
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_U_TblSaveAddress", Parameter, out strErr);

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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            string[,] Parameter = new string[,]
                {
                    {"@AddressId", ""},
                    {"@AddressCode", txtCode.Text },
                    {"@AddressNumber", txtNumber.Text },
                    {"@AddressMoo", txtMoo.Text },
                    {"@AddressBuilding", txtBuilding.Text },
                    {"@AddressSoi", txtSoi.Text },
                    {"@AddressRoad", txtRoad.Text },
                    {"@AddressDistrict", cbbTambol.SelectedValue.ToString() },
                    {"@AddressAmphur", cbbAmphur.SelectedValue.ToString() },
                    {"@AddressProvince", cbbProvince.SelectedValue.ToString() },
                    {"@AddressGeo", cbbGeography.SelectedValue.ToString() },
                    {"@AddressPostcode", txtZip.Text },
                    {"@AddressPhone", txtPhone.Text },
                    {"@AddressStatus", cbbStatus.SelectedValue.ToString() },
                };

            db.Get("Spr_S_TblSaveAddress", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtCode.Text = Fn.GetCodes("114", "", "Generated");
            strOpe = "I";

            string[,] Parameter = new string[,]
                {
                    {"@User",strUserId },
                    {"@AddressCode",txtCode.Text },
                    {"@AddressNumber",txtNumber.Text },
                    {"@AddressMoo",txtMoo.Text },
                    {"@AddressBuilding",txtBuilding.Text },
                    {"@AddressSoi",txtSoi.Text },
                    {"@AddressRoad",txtRoad.Text },
                    {"@AddressDistrict",cbbTambol.SelectedValue.ToString() },
                    {"@AddressAmphur",cbbAmphur.SelectedValue.ToString() },
                    {"@AddressProvince",cbbProvince.SelectedValue.ToString() },
                    {"@AddressGeo",cbbGeography.SelectedValue.ToString() },
                    {"@AddressPostcode",txtZip.Text },
                    {"@AddressPhone",txtPhone.Text },
                    {"@AddressStatus",cbbStatus.SelectedValue.ToString() },
                    {"@AddressName","" },
                    {"@AddressMap",txtMap.Text },
                    {"@AddressSend","2" },
                    {"@Address","" },
                    {"@AddressHome","" },
                    {"@AddressRoom","" },
                    {"@AddressFloor","" },
                    {"@AddressPrefix","" },
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_I_TblSaveAddress", Parameter, out strErr);

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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                DataTable dt = new DataTable();

                string[,] Parameter = new string[,]
                 {
                    {"@AddressId", ""},
                    {"@AddressCode", row.Cells["AddressCode"].Value.ToString()},
                    {"@AddressNumber", ""},
                    {"@AddressMoo", ""},
                    {"@AddressBuilding", ""},
                    {"@AddressSoi", ""},
                    {"@AddressRoad", ""},
                    {"@AddressDistrict", "0"},
                    {"@AddressAmphur", "0"},
                    {"@AddressProvince", "0"},
                    {"@AddressGeo", "0"},
                    {"@AddressPostcode", ""},
                    {"@AddressPhone", ""},
                    {"@AddressStatus", "0"},
                 };

                db.Get("Spr_S_TblSaveAddress", Parameter, out strErr, out dt);

                txtCode.Text = dt.Rows[0]["AddressCode"].ToString();
                txtNumber.Text = dt.Rows[0]["AddressNumber"].ToString();
                txtBuilding.Text = dt.Rows[0]["AddressBuilding"].ToString();
                txtMoo.Text = dt.Rows[0]["AddressMoo"].ToString();
                cbbStatus.SelectedValue = dt.Rows[0]["AddressStatus"].ToString();

                txtSoi.Text = dt.Rows[0]["AddressSoi"].ToString();
                txtRoad.Text = dt.Rows[0]["AddressRoad"].ToString();
                txtPhone.Text = dt.Rows[0]["AddressPhone"].ToString();

                cbbGeography.SelectedValue = dt.Rows[0]["AddressGeo"].ToString();
                cbbProvince.SelectedValue = dt.Rows[0]["AddressProvince"].ToString();
                cbbAmphur.SelectedValue = dt.Rows[0]["AddressAmphur"].ToString();
                cbbTambol.SelectedValue = dt.Rows[0]["AddressDistrict"].ToString();
                txtZip.Text = dt.Rows[0]["AddressPostcode"].ToString();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
        }

        private void cbbGeography_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.getWhereList(cbbProvince, "", Fn.selectedValue(cbbGeography), "Y", "Province");
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
                List.getWhereList(cbbAmphur, "", Fn.selectedValue(cbbProvince), "Y", "Amphur");
                txtZip.Text = "";
            }
            catch (Exception)
            {
            }
        }

        private void cbbTambol_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void picCopy_Click(object sender, EventArgs e)
        {
            strAddress = "";
            strAddress += "เลขที่ " + txtNumber.Text + "\r\n";
            strAddress += "หมู่บ้าน/อาคาร " + txtBuilding.Text + "\r\n";
            strAddress += "หมู่ที่ " + txtMoo.Text;
            strAddress += " ซอย" + txtSoi.Text;
            strAddress += " ถนน" + txtRoad.Text + "\r\n";
            strAddress += "ตำบล" + cbbTambol.Text + "\r\n";
            strAddress += "อำเภอ" + cbbAmphur.Text + "\r\n";
            strAddress += "จังหวัด" + cbbProvince.Text + "\r\n";
            strAddress += "รหัสไปรษณีย์ " + txtZip.Text + "\r\n";
            strAddress += "โทร " + txtPhone.Text;

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

        private void FrmSaveAddress_KeyDown(object sender, KeyEventArgs e)
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
            if (keyCode == "Ctrl+P")
            {
                btnPrint_Click(sender, e);
            }
        }

        private void cbbAmphur_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtZip.Text = "";
                List.getWhereList(cbbTambol, "", Fn.selectedValue(cbbAmphur), "Y", "Tambol");

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
                //txtZip.Text = dt.Rows[0]["AmphurPostcode"].ToString();
            }
            catch (Exception)
            {
            }
        }
    }
}