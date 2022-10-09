using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Database;
using SANSANG.Constant;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmMangeThailand : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string strAppCode = "MANTH00";
        public string strAppName = "FrmMangeThailand";
        public string strLaguage = "EN";
        public string strErr = "";
        public string strOpe = "";
        public string strStatus = "";
        public string Error = "";

        private DataTable dt = new DataTable();
        private StoreConstant Store = new StoreConstant();
        private TableConstant Tb = new TableConstant();
        private clsInsert Insert = new clsInsert();
        private clsDelete Delete = new clsDelete();
        private clsEdit Edit = new clsEdit();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();

        private clsLog Log = new clsLog();
        private Timer timer = new Timer();
        public bool Start = true;
        public string strSearch = "";

        public string[,] Parameter = new string[,] { };

        public FrmMangeThailand(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            try
            {
                InitializeComponent();
                strUserId = userIdLogin;
                strUserName = userNameLogin;
                strUserSurname = userSurNameLogin;
                strUserType = userTypeLogin;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            try
            {
                int sec = 2;
                timer.Interval = (sec * 1000);
                timer.Tick += new EventHandler(LoadList);
                timer.Start();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void LoadList(object sender, EventArgs e)
        {
            Start = true;
            List.GetList(cbbStatusProvince, "0", "Status");
            List.GetList(cbbGeography, "0", "Geography");
            List.GetList(cbbGeographyMain, "0", "Geography");
            List.GetList(cbbGeographyTravel, "0", "Geography");
            List.GetList(cbbProvinceIsVicinity, "0", "Vicinity");

            List.GetList(cbbProvinceAmphoe, "Y", "ProvinceAll");
            List.GetList(cbbStatusAmphoe, "0", "Status");
            List.GetList(cbbGeographyAmphoe, "0", "Geography");

            List.GetList(cbbProvinceTambol, "Y", "ProvinceAll");
            List.GetList(cbbAmphoeTambol, "Y", "AmphoeAll");
            List.GetList(cbbStatusTambol, "0", "Status");
            List.GetList(cbbGeographyTambol, "0", "Geography");

            List.GetList(cbbIsActive, "99", "Status");
            List.GetList(cbbProvincePostcode, "Y", "ProvinceAll");
            List.GetList(cbbAmphoePostcode, "Y", "AmphoeAll");
            List.GetList(cbbTambolPostcode, "Y", "TambolAll");

            gbLoad.Enabled = true;

            Clear();
            timer.Stop();
        }

        public void getDataGrid(DataTable dt)
        {
            try
            {
                if (dt != null)
                {
                    int row = dt.Rows.Count;

                    if (row == 0)
                    {
                        dataGridView.DataSource = null;
                        picExcel.Visible = false;
                        lblResult.Text = Fn.ShowResult(0);
                    }
                    else
                    {
                        DataTable dtGrid = new DataTable();
                        dtGrid = dt.DefaultView.ToTable(true, "TambolNameTh", "AmphoeNameTh", "ProvinceNameTh", "Zipcode", "GeographyNameTh", "ZipcodeId");

                        DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                        DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                        Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                              "ลำดับ", 50, true, mc, mc
                            , "ตำบล", 150, true, ml, ml
                            , "อำเภอ", 300, true, ml, ml
                            , "จังหวัด", 150, true, ml, ml
                            , "รหัสไปรษณีย์", 100, true, ml, ml
                            , "ภูมิภาค", 150, true, mc, mc
                            , "", 0, false, mc, mc
                            );

                        picExcel.Visible = true;
                        lblResult.Text = Fn.ShowResult(row);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchAll(sender, e);
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                DataTable dt = new DataTable();

                string[,] Parameter = new string[,]
                {
                    {"@Search", row.Cells["ZipcodeId"].Value.ToString()},
                    {"@TypeCode", "RI" },
                };

                db.Get("Spr_S_Thailand", Parameter, out strErr, out dt);

                lblThailand.Visible = true;
                txtProvinceCode2.Text = dt.Rows[0]["Zipcode"].ToString().Substring(0, 2);
                txtAmphoeCode.Text = dt.Rows[0]["Zipcode"].ToString().Substring(2, 3);
                txtProvinceName.Text = dt.Rows[0]["ProvinceNameTh"].ToString();
                txtAmphoeName.Text = dt.Rows[0]["AmphoeNameTh"].ToString();
                txtTambolName.Text = dt.Rows[0]["TambolNameTh"].ToString();
                txtProvinceEn.Text = dt.Rows[0]["ProvinceNameEnUpper"].ToString();

                txtIdProvince.Text = dt.Rows[0]["ProvinceId"].ToString();
                txtCodeProvince.Text = dt.Rows[0]["ProvinceCode"].ToString();
                txtNameThProvince.Text = dt.Rows[0]["ProvinceNameTh"].ToString();
                txtNameEnProvince.Text = dt.Rows[0]["ProvinceNameEn"].ToString();

                cbbGeography.SelectedValue = dt.Rows[0]["GeographyId"].ToString();
                cbbGeographyMain.SelectedValue = dt.Rows[0]["GeographyIdMain"].ToString();
                cbbGeographyTravel.SelectedValue = dt.Rows[0]["GeographyIdTravel"].ToString();
                cbbProvinceIsVicinity.SelectedValue = dt.Rows[0]["ProvinceIsVicinity"].ToString();
                cbbStatusProvince.SelectedValue = dt.Rows[0]["ProvinceStatus"].ToString();

                txtProvinceArea.Text = string.Format("{0:#,##0.00}", Convert.ToDouble(dt.Rows[0]["ProvinceArea"].ToString()));
                txtProvinceHousehold.Text = string.Format("{0:#,##0}", Convert.ToInt32(dt.Rows[0]["ProvinceHousehold"].ToString()));
                txtProvincePopulation.Text = string.Format("{0:#,##0}", Convert.ToInt32(dt.Rows[0]["ProvincePopulation"].ToString()));
                txtProvinceMen.Text = string.Format("{0:#,##0}", Convert.ToInt32(dt.Rows[0]["ProvinceMen"].ToString()));
                txtProvinceWomen.Text = string.Format("{0:#,##0}", Convert.ToInt32(dt.Rows[0]["ProvinceWomen"].ToString()));

                txtIdAmphoe.Text = dt.Rows[0]["AmphoeId"].ToString();
                txtCodeAmphoe.Text = dt.Rows[0]["AmphoeCode"].ToString();
                txtNameThAmphoe.Text = dt.Rows[0]["AmphoeNameTh"].ToString();
                txtNameEnAmphoe.Text = dt.Rows[0]["AmphoeNameEn"].ToString();
                txtZipAmphoe.Text = dt.Rows[0]["AmphoePostcode"].ToString();
                cbbStatusAmphoe.SelectedValue = dt.Rows[0]["AmphoeStatus"].ToString();
                cbbGeographyAmphoe.SelectedValue = dt.Rows[0]["AmphoeGeoId"].ToString();
                txtRemark.Text = dt.Rows[0]["AmphoeRemark"].ToString();

                txtIdTambol.Text = dt.Rows[0]["TambolId"].ToString();
                txtCodeTambol.Text = dt.Rows[0]["TambolCode"].ToString();
                txtNameThTambol.Text = dt.Rows[0]["TambolNameTh"].ToString();
                txtNameEnTambol.Text = dt.Rows[0]["TambolNameEn"].ToString();

                cbbGeographyTambol.SelectedValue = dt.Rows[0]["TambolGeoId"].ToString();
                cbbProvinceTambol.SelectedValue = dt.Rows[0]["ProvinceId"].ToString();
                cbbAmphoeTambol.SelectedValue = dt.Rows[0]["AmphoeId"].ToString();
                cbbStatusTambol.SelectedValue = dt.Rows[0]["TambolStatus"].ToString();

                txtIdPostcode.Text = dt.Rows[0]["IdPostcode"].ToString();
                txtPostcode.Text = dt.Rows[0]["Postcode"].ToString();
                cbbIsActive.SelectedValue = dt.Rows[0]["IsActive"].ToString();
                cbbProvincePostcode.SelectedValue = dt.Rows[0]["PostcodeProvinceId"].ToString();
                cbbAmphoePostcode.SelectedValue = dt.Rows[0]["AmphoePostcodes"].ToString();
                cbbTambolPostcode.SelectedValue = dt.Rows[0]["TambolPostcode"].ToString();
                cbbProvinceAmphoe.SelectedValue = dt.Rows[0]["AmphoeProvinceId"].ToString();
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) && txtSearch.Text.Replace(" ", string.Empty) != "")
            {
                SearchAll(sender, e);
            }
        }

        private void SearchAll(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text != "")
                {
                    string[,] Parameter = new string[,]
                    {
                        {"@Search", txtSearch.Text.Replace(" ", string.Empty)},
                        {"@TypeCode", "SA" },
                    };

                    db.Get("Spr_S_Thailand", Parameter, out strErr, out dt);
                    getDataGrid(dt);
                    strSearch = txtSearch.Text;
                }
                else if (strSearch != "")
                {
                    string[,] Parameter = new string[,]
                    {
                        {"@Search", txtSearch.Text.Replace(" ", string.Empty)},
                        {"@TypeCode", "SA" },
                    };

                    db.Get("Spr_S_Thailand", Parameter, out strErr, out dt);
                    getDataGrid(dt);
                    txtSearch.Text = strSearch;
                    strSearch = "";
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        public void Clear()
        {
            lblThailand.Visible = false;
            strSearch = "";
            txtProvinceCode2.Text = "";
            txtAmphoeCode.Text = "";
            txtProvinceName.Text = "";
            txtAmphoeName.Text = "";
            txtTambolName.Text = "";
            txtProvinceEn.Text = "";

            List.GetList(cbbProvincePostcode, "Y", "ProvinceAll");
            List.GetList(cbbAmphoePostcode, "Y", "AmphoeAll");
            List.GetList(cbbTambolPostcode, "Y", "TambolAll");

            cbbStatusProvince.SelectedValue = "0";
            cbbGeography.SelectedValue = "0";
            cbbGeographyMain.SelectedValue = "0";
            cbbGeographyTravel.SelectedValue = "0";
            cbbProvinceIsVicinity.SelectedValue = "0";
            cbbGeographyTambol.SelectedValue = "0";

            txtProvinceArea.Text = "";
            txtProvinceHousehold.Text = "";
            txtProvincePopulation.Text = "";
            txtProvinceMen.Text = "";
            txtProvinceWomen.Text = "";
            txtIdProvince.Text = "";
            txtCodeProvince.Text = "";
            txtNameThProvince.Text = "";
            txtNameEnProvince.Text = "";
            dataGridView.DataSource = null;
            picExcel.Visible = false;
            lblResult.Text = Fn.ShowResult(0);

            txtIdAmphoe.Text = "";
            txtCodeAmphoe.Text = "";
            txtNameThAmphoe.Text = "";
            txtNameEnAmphoe.Text = "";
            txtZipAmphoe.Text = "";

            txtIdTambol.Text = "";
            txtCodeTambol.Text = "";
            txtNameThTambol.Text = "";
            txtNameEnTambol.Text = "";
            cbbAmphoeTambol.SelectedValue = "0";
            cbbProvinceTambol.SelectedValue = "0";
            cbbStatusTambol.SelectedValue = "0";
            cbbGeographyAmphoe.SelectedValue = "0";

            cbbProvinceAmphoe.SelectedValue = "0";
            cbbStatusAmphoe.SelectedValue = "0";

            txtRemark.Text = "";
            txtIdPostcode.Text = "";
            txtPostcode.Text = "";
            cbbIsActive.SelectedValue = "0";
            cbbProvincePostcode.SelectedValue = "0";
            cbbAmphoePostcode.SelectedValue = "0";
            cbbTambolPostcode.SelectedValue = "0";

            tabData.SelectedTab = tabPostcode;
            txtSearch.Text = "";
            txtSearch.Focus();
        }

        private void cbbProvinceTambol_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.getWhereList(cbbAmphoeTambol, "", Fn.selectedValue(cbbProvinceTambol), "Y", "Amphoe");
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnAddProvince_Click(object sender, EventArgs e)
        {
            if (txtCodeProvince.Text != "" && txtNameThProvince.Text != "")
            {
                if (Fn.IsDuplicates("MST_Province", txtCodeProvince.Text, txtNameThProvince.Text, txtNameEnProvince.Text))
                {
                    Message.ShowMesInfo("ข้อมูลซ้ำ กรุณาตรวจสอบ");
                    txtSearch.Focus();
                }
                else
                {
                    string[,] Parameter = new string[,]
                    {
                        {"@User", strUserId},
                        {"@Code", txtCodeProvince.Text},
                        {"@NameTh", txtNameThProvince.Text},
                        {"@NameEn", txtNameEnProvince.Text},
                        {"@GeoId", Fn.getComboboxId(cbbGeography)},
                        {"@GeoMainId", Fn.getComboboxId(cbbGeographyMain)},
                        {"@GeoTravelId", Fn.getComboboxId(cbbGeographyTravel)},
                        {"@IsVicinity", Fn.getComboboxId(cbbProvinceIsVicinity)},
                        {"@Area", txtProvinceArea.Text},
                        {"@Household", txtProvinceHousehold.Text},
                        {"@Population", txtProvincePopulation.Text},
                        {"@Men", txtProvinceMen.Text},
                        {"@Women", txtProvinceWomen.Text},
                        {"@Status", Fn.getComboboxId(cbbStatusProvince)},
                    };

                    string Detail = txtNameThProvince.Text + (txtNameEnProvince.Text == "" ? "" : " | " + txtNameEnProvince.Text);

                    if (Insert.Add(strAppCode, strAppName, strUserId, "Store.InsertProvince", Parameter, txtCodeProvince.Text, Detail))
                    {
                        Clear();
                    }
                }
            }
            else
            {
                Message.ShowMesInfo("กรอกข้อมูลไม่ครบถ้วน");
            }
        }

        private void btnEditProvince_Click(object sender, EventArgs e)
        {
            if (txtCodeProvince.Text != "" && txtNameThProvince.Text != "")
            {
                string[,] Parameter = new string[,]
                {
                        {"@Id", txtIdProvince.Text},
                        {"@User", strUserId},
                        {"@Code", txtCodeProvince.Text},
                        {"@NameTh", txtNameThProvince.Text},
                        {"@NameEn", txtNameEnProvince.Text},
                        {"@GeoId", Fn.getComboboxId(cbbGeography)},
                        {"@GeoMainId", Fn.getComboboxId(cbbGeographyMain)},
                        {"@GeoTravelId", Fn.getComboboxId(cbbGeographyTravel)},
                        {"@IsVicinity", Fn.getComboboxId(cbbProvinceIsVicinity)},
                        {"@Area", txtProvinceArea.Text},
                        {"@Household", txtProvinceHousehold.Text},
                        {"@Population", txtProvincePopulation.Text},
                        {"@Men", txtProvinceMen.Text},
                        {"@Women", txtProvinceWomen.Text},
                        {"@Status", Fn.getComboboxId(cbbStatusProvince)},
                };

                string Detail = "จังหวัด" + txtNameThProvince.Text + (txtNameEnProvince.Text == "" ? "" : " (" + txtNameEnProvince.Text + ")");

                if (Edit.Update(strAppCode, strAppName, strUserId, "Store.UpdateProvince", Parameter, txtCodeProvince.Text, Detail))
                {
                    Clear();
                }
            }
            else
            {
                Message.ShowMesInfo("กรอกข้อมูลไม่ครบถ้วน");
            }
        }

        private void btnDeleteProvince_Click(object sender, EventArgs e)
        {
            if (Delete.DropId(strAppCode, strAppName, strUserId, 1, Tb.Province, txtIdProvince, txtCodeProvince, "จังหวัด" + txtNameThProvince.Text + " (" + txtNameEnProvince.Text + ")"))
            {
                Clear();
            }
        }

        private void btnAddAmphoe_Click(object sender, EventArgs e)
        {
            if (txtCodeAmphoe.Text != "" && txtNameThAmphoe.Text != "" && Fn.getComboboxId(cbbProvinceAmphoe) != "0")
            {
                if (Fn.IsDuplicates("MST_Amphoe", txtCodeAmphoe.Text, txtNameThAmphoe.Text, Fn.getComboboxId(cbbProvinceAmphoe)))
                {
                    Message.ShowMesInfo("ข้อมูลซ้ำ กรุณาตรวจสอบ");
                    txtSearch.Focus();
                }
                else
                {
                    string[,] Parameter = new string[,]
                    {
                        {"@User", strUserId},
                        {"@Status", Fn.getComboboxId(cbbStatusAmphoe)},
                        {"@Code", txtCodeAmphoe.Text},
                        {"@NameTh", txtNameThAmphoe.Text},
                        {"@NameEn", txtNameEnAmphoe.Text},
                        {"@ProvinceId", Fn.getComboboxId(cbbProvinceAmphoe)},
                        {"@GeoId", Fn.getComboboxId(cbbGeographyAmphoe)},
                        {"@Postcode", txtZipAmphoe.Text},
                        {"@Remark", txtRemark.Text},
                    };

                    string Detail = txtNameThAmphoe.Text + (txtNameEnAmphoe.Text == "" ? "" : " | " + txtNameEnAmphoe.Text);

                    if (Insert.Add(strAppCode, strAppName, strUserId, "Store.InsertAmphoe", Parameter, txtCodeAmphoe.Text, Detail))
                    {
                        Clear();
                    }
                }
            }
        }

        private void btnEditAmphoe_Click(object sender, EventArgs e)
        {
            if (txtCodeAmphoe.Text != "" && txtNameThAmphoe.Text != "" && Fn.getComboboxId(cbbProvinceAmphoe) != "0")
            {
                string[,] Parameter = new string[,]
                {
                        {"@Id", txtIdAmphoe.Text},
                        {"@User", strUserId},
                        {"@Status", Fn.getComboboxId(cbbStatusAmphoe)},
                        {"@Code", txtCodeAmphoe.Text},
                        {"@NameTh", txtNameThAmphoe.Text},
                        {"@NameEn", txtNameEnAmphoe.Text},
                        {"@ProvinceId", Fn.getComboboxId(cbbProvinceAmphoe)},
                        {"@GeoId", Fn.getComboboxId(cbbGeographyAmphoe)},
                        {"@Postcode", txtZipAmphoe.Text},
                        {"@Remark", txtRemark.Text},
                };

                string Detail = (Fn.getComboboxId(cbbProvinceAmphoe) == "1" ? "" : "อำเภอ") + txtNameThAmphoe.Text + (txtNameEnAmphoe.Text == "" ? "" : " (" + txtNameEnAmphoe.Text + ")");

                if (Edit.Update(strAppCode, strAppName, strUserId, "Store.UpdateAmphoe", Parameter, txtCodeAmphoe.Text, Detail))
                {
                    Clear();
                }
            }
        }

        private void btnDeleteAmphoe_Click(object sender, EventArgs e)
        {
            if (Delete.DropId(strAppCode, strAppName, strUserId, 1, Tb.Amphoe, txtIdAmphoe, txtCodeAmphoe, (Fn.getComboboxId(cbbProvinceAmphoe) == "1" ? "" : "อำเภอ") + txtNameThAmphoe.Text + " (" + txtNameEnAmphoe.Text + ")"))
            {
                Clear();
            }
        }

        private void btnAddTambol_Click(object sender, EventArgs e)
        {
            if (txtCodeTambol.Text != ""
             && txtNameThTambol.Text != ""
             && Fn.getComboboxId(cbbProvinceTambol) != "0"
             && Fn.getComboboxId(cbbAmphoeTambol) != "0")
            {
                if (Fn.IsDuplicates("MST_Tambol",
                    txtCodeTambol.Text, txtNameThTambol.Text,
                    Fn.getComboboxId(cbbAmphoeTambol),
                    Fn.getComboboxId(cbbProvinceTambol)))
                {
                    Message.ShowMesInfo("ข้อมูลซ้ำ กรุณาตรวจสอบ");
                    txtSearch.Focus();
                }
                else
                {
                    string[,] Parameter = new string[,]
                    {
                        {"@User", strUserId},
                        {"@Status", Fn.getComboboxId(cbbStatusTambol)},
                        {"@Code", txtCodeTambol.Text},
                        {"@NameTh", txtNameThTambol.Text},
                        {"@NameEn", txtNameEnTambol.Text},
                        {"@AmphoeId", Fn.getComboboxId(cbbAmphoeTambol)},
                        {"@ProvinceId", Fn.getComboboxId(cbbProvinceTambol)},
                        {"@GeoId", Fn.getComboboxId(cbbGeographyTambol)},
                        {"@PostcodeAll", txtZipAmphoe.Text},
                        {"@Remark", txtRemark.Text},
                    };

                    string Detail = txtNameThTambol.Text + (txtNameEnTambol.Text == "" ? "" : " (" + txtNameEnTambol.Text + ")");

                    if (Insert.Add(strAppCode, strAppName, strUserId, "Store.InsertTambol", Parameter, txtCodeTambol.Text, Detail))
                    {
                        Clear();
                    }
                }
            }
        }

        private void btnEditTambol_Click(object sender, EventArgs e)
        {
            if (txtCodeTambol.Text != ""
            && txtNameThTambol.Text != ""
            && Fn.getComboboxId(cbbProvinceTambol) != "0"
            && Fn.getComboboxId(cbbAmphoeTambol) != "0")
            {
                string[,] Parameter = new string[,]
                {
                        {"@Id", txtIdTambol.Text},
                        {"@User", strUserId},
                        {"@Status", Fn.getComboboxId(cbbStatusTambol)},
                        {"@Code", txtCodeTambol.Text},
                        {"@NameTh", txtNameThTambol.Text},
                        {"@NameEn", txtNameEnTambol.Text},
                        {"@AmphoeId", Fn.getComboboxId(cbbAmphoeTambol)},
                        {"@ProvinceId", Fn.getComboboxId(cbbProvinceTambol)},
                        {"@GeoId", Fn.getComboboxId(cbbGeographyTambol)},
                        {"@PostcodeAll", txtZipAmphoe.Text},
                        {"@Remark", txtRemark.Text},
                };

                string Detail = (Fn.getComboboxId(cbbProvinceTambol) == "1" ? "แขวง" : "ตำบล") + txtNameThTambol.Text + (txtNameEnTambol.Text == "" ? "" : " (" + txtNameEnTambol.Text + ")");

                if (Edit.Update(strAppCode, strAppName, strUserId, "Store.UpdateTambol", Parameter, txtCodeTambol.Text, Detail))
                {
                    Clear();
                }
            }
        }

        private void btnDeleteTambol_Click(object sender, EventArgs e)
        {
            if (Delete.DropId(strAppCode, strAppName, strUserId, 1, Tb.Tambol, txtIdTambol, txtCodeTambol, 
                (Fn.getComboboxId(cbbProvinceTambol) == "1"? "แขวง" : "ตำบล") + txtNameThTambol.Text +
                " (" + txtNameEnTambol.Text + ")"))
            {
                Clear();
            }
        }

        private void AddEditDetail_Click(object sender, EventArgs e)
        {

        }

        private void cbbProvincePostcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.getWhereList(cbbAmphoePostcode, "", Fn.selectedValue(cbbProvincePostcode), "Y", "Amphoe");
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void cbbAmphoePostcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.getWhereList(cbbTambolPostcode, "", Fn.selectedValue(cbbAmphoePostcode), "Y", "Tambol");
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnAddPostcode_Click(object sender, EventArgs e)
        {
            if (txtPostcode.Text != ""
                && Fn.getComboboxId(cbbProvincePostcode) != "0"
                && Fn.getComboboxId(cbbAmphoePostcode) != "0"
                && Fn.getComboboxId(cbbTambolPostcode) != "0")
            {
                if (Fn.IsDuplicates("MST_Postcode", txtPostcode.Text,
                    Fn.getComboboxId(cbbTambolPostcode),
                    Fn.getComboboxId(cbbAmphoePostcode),
                    Fn.getComboboxId(cbbProvincePostcode)))
                {
                    Message.ShowMesInfo("ข้อมูลซ้ำ กรุณาตรวจสอบ");
                    txtSearch.Focus();
                }
                else
                {
                    string[,] Parameter = new string[,]
                    {
                        {"@User", strUserId},
                        {"@Zipcode", txtPostcode.Text},
                        {"@TambolId", Fn.getComboboxId(cbbTambolPostcode)},
                        {"@AmphoeId", Fn.getComboboxId(cbbAmphoePostcode)},
                        {"@ProvinceId", Fn.getComboboxId(cbbProvincePostcode)},
                        {"@NameTh", cbbTambolPostcode.Text},
                    };

                    string Detail = cbbTambolPostcode.Text + " จังหวัด" + cbbProvincePostcode.Text;

                    if (Insert.Add(strAppCode, strAppName, strUserId, "Store.InsertPostcode", Parameter, txtPostcode.Text, Detail))
                    {
                        Clear();
                    }
                }
            }
        }

        private void cbbGeographyAmphoe_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.getWhereList(cbbProvinceAmphoe, "", Fn.selectedValue(cbbGeographyAmphoe), "Y", "Province");
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnDeletePostcode_Click(object sender, EventArgs e)
        {
            if (Delete.DropId(strAppCode, strAppName, strUserId, 1, Tb.Postcode, txtIdPostcode, txtPostcode, (Fn.getComboboxId(cbbProvincePostcode) == "1" ? "แขวง" : "ตำบล") + cbbTambolPostcode.Text + " | จังหวัด" + cbbProvincePostcode.Text))
            {
                Clear();
            }
        }

        private void btnEditPostcode_Click(object sender, EventArgs e)
        {
            if (txtPostcode.Text != ""
                && Fn.getComboboxId(cbbProvincePostcode) != "0"
                && Fn.getComboboxId(cbbAmphoePostcode) != "0"
                && Fn.getComboboxId(cbbTambolPostcode) != "0")
            {
                string[,] Parameter = new string[,]
                {
                        {"@User", strUserId},
                        {"@Zipcode", txtPostcode.Text},
                        {"@TambolId", Fn.getComboboxId(cbbTambolPostcode)},
                        {"@TambolCode", Fn.getValue(Tb.Tambol, "Id", Fn.getComboboxId(cbbTambolPostcode), "Code")},
                        {"@AmphoeId", Fn.getComboboxId(cbbAmphoePostcode)},
                        {"@ProvinceId", Fn.getComboboxId(cbbProvincePostcode)},
                        {"@Status", Fn.getComboboxId(cbbIsActive) == "False"? "0" : "1"},
                };

                string Detail = (Fn.getComboboxId(cbbProvincePostcode) == "1" ? "แขวง" : "ตำบล") + cbbTambolPostcode.Text + " | จังหวัด" + cbbProvincePostcode.Text;

                if (Edit.Update(strAppCode, strAppName, strUserId, "Store.UpdatePostcode", Parameter, txtPostcode.Text, Detail))
                {
                    Clear();
                }
            }
        }

        private void cbbGeographyTambol_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.getWhereList(cbbProvinceTambol, "", Fn.selectedValue(cbbGeographyTambol), "Y", "Province");
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }
    }
}