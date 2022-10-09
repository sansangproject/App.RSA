using System;
using SANSANG.Class; using SANSANG.Database;
using System.Data;
using System.Windows.Forms;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmShopRegister : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string strErr = "";
        public string strAppCode = "SHOPR00";
        public string strAppName = "FrmShopRegister";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string filePath = "-";
        public string fileName = "-";
        public string fileType = ".jpg";
        public string ExportToPath = "";
        public bool start = false;

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();
        private clsTextbox TSS = new clsTextbox();
        private Timer timer = new Timer();
        public string[,] Parameter = new string[,] { };

        public FrmShopRegister(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmShopRegister_Load(object sender, EventArgs e)
        {
            int sec = 2;
            timer.Interval = (sec * 1000);
            timer.Tick += new EventHandler(LoadList);
            timer.Start();
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetList(cbbStatus, "0", "Status");
            start = true;
            gbFrm.Enabled = true;
            Clear();
            timer.Stop();
        }

        public void Clear()
        {
            cbbStatus.SelectedValue = "0";
            txtFloor.Text = "";
            txtSearch.Focus();

            //string[,] Parameter = new string[,]
            //{
            //        {"@", ""},
            //};

            //db.Get("Spr_S_Tbl", Parameter, out strErr, out dt);
            //getDataGrid(dt);
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
                    txtCount.Text = "0";
                }
                else
                {
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "AddressNumbers", "AddressNames", "TambolNameTh", "AmphurNameTh", "ProvinceNameTh", "AddressZipCode", "AddressCode");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                    Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                          "ลำดับ", 50, true, mc, mc
                        , "เลขที่", 100, true, ml, ml
                        , "ชื่อ/บริษัท/ห้างร้าน", 180, true, mc, ml
                        , "ตำบล", 100, true, ml, ml
                        , "เขต/อำเภอ", 100, true, ml, ml
                        , "จังหวัด", 150, true, ml, ml
                        , "รหัสไปรษณีย์", 150, true, mc, mc
                        , "", 0, false, mc, mc
                        );

                    picExcel.Visible = true;
                    txtCount.Text = row.ToString();
                }
            }
            catch (Exception)
            {

                throw;
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
            if (txtCode.Text != "")
            {
                strOpe = "D";

                string[,] Parameter = new string[,]
                {
                {"@DeleteType", "0"},
                {"@AddressCode", txtCode.Text},
                {"@User", strUserId},
                };

                Message.MessageConfirmation(strOpe, txtCode.Text, txtName.Text + " เลขที่ " + txtNumber.Text + " จังหวัด" + cbbProvince.Text);

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
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

                
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                strOpe = "U";

                string[,] Parameter = new string[,]
                {
                    {"@User", strUserId},
                    {"@AddressCode", txtCode.Text},
                    {"@AddressAuthorization", Fn.getComboboxId(cbbGenre)},
                    {"@AddressInstitution", txtInstitution.Text},
                    {"@AddressPrefix", Fn.getComboboxId(cbbPrefix)},
                    {"@AddressName", txtName.Text},
                    {"@AddressNumber", txtNumber.Text},
                    {"@AddressVillage", txtVillage.Text},
                    {"@AddressBuilding", txtBuilding.Text},
                    {"@AddressRoom", txtRoom.Text},
                    {"@AddressFloor", txtFloor.Text},
                    {"@AddressMoo", txtMoo.Text},
                    {"@AddressSoi", txtSoi.Text},
                    {"@AddressRoad", txtRoad.Text},
                    {"@AddressTambolId", Fn.getComboboxId(cbbTambol)},
                    {"@AddressAmphurId", Fn.getComboboxId(cbbAmphur)},
                    {"@AddressProvinceId", Fn.getComboboxId(cbbProvince)},
                    {"@AddressZipCode", txtZip.Text},
                    {"@AddressGeoId", Fn.getComboboxId(cbbGeography)},
                    {"@AddressMap", txtMap.Text},
                    {"@AddressPhone", txtPhone.Text},
                    {"@AddressWebsite", txtWebsite.Text},
                    {"@AddressRemark", txtRemark.Text},
                    {"@AddressStatus", Fn.getComboboxId(cbbStatus)},
                };

                Message.MessageConfirmation(strOpe, txtCode.Text, txtName.Text + " เลขที่ " + txtNumber.Text + " จังหวัด" + cbbProvince.Text);

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
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
                    {"@AddressCode", txtCode.Text},
                    {"@AddressAuthorization", Fn.getComboboxId(cbbGenre)},
                    {"@AddressInstitution", txtInstitution.Text},
                    {"@AddressPrefix", Fn.getComboboxId(cbbPrefix)},
                    {"@AddressName", txtName.Text},
                    {"@AddressNumber", txtNumber.Text},
                    {"@AddressVillage", txtVillage.Text},
                    {"@AddressBuilding", txtBuilding.Text},
                    {"@AddressRoom", txtRoom.Text},
                    {"@AddressFloor", txtFloor.Text},
                    {"@AddressMoo", txtMoo.Text},
                    {"@AddressSoi", txtSoi.Text},
                    {"@AddressRoad", txtRoad.Text},
                    {"@AddressTambolId", Fn.getComboboxId(cbbTambol)},
                    {"@AddressAmphurId", Fn.getComboboxId(cbbAmphur)},
                    {"@AddressProvinceId", Fn.getComboboxId(cbbProvince)},
                    {"@AddressZipCode", txtZip.Text},
                    {"@AddressGeoId", Fn.getComboboxId(cbbGeography)},
                    {"@AddressMap", txtMap.Text},
                    {"@AddressPhone", txtPhone.Text},
                    {"@AddressWebsite", txtWebsite.Text},
                    {"@AddressRemark", txtRemark.Text},
                    {"@AddressStatus", Fn.getComboboxId(cbbStatus)},
                };

            db.Get("Spr_S_TblSaveAddress", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Fn.IsDuplicates("Tbl_Save_Address", txtName.Text, txtNumber.Text, Fn.getComboboxId(cbbTambol)))
                {
                    txtCode.Text = Fn.GetCodes("114", "", "Generated");
                    strOpe = "I";

                    string[,] Parameter = new string[,]
                    {
                    {"@User", strUserId},
                    {"@AddressCode", txtCode.Text},
                    {"@AddressAuthorization", Fn.getComboboxId(cbbGenre)},
                    {"@AddressInstitution", txtInstitution.Text},
                    {"@AddressPrefix", Fn.getComboboxId(cbbPrefix)},
                    {"@AddressName", txtName.Text},
                    {"@AddressNumber", txtNumber.Text},
                    {"@AddressVillage", txtVillage.Text},
                    {"@AddressBuilding", txtBuilding.Text},
                    {"@AddressRoom", txtRoom.Text},
                    {"@AddressFloor", txtFloor.Text},
                    {"@AddressMoo", txtMoo.Text},
                    {"@AddressSoi", txtSoi.Text},
                    {"@AddressRoad", txtRoad.Text},
                    {"@AddressTambolId", Fn.getComboboxId(cbbTambol)},
                    {"@AddressAmphurId", Fn.getComboboxId(cbbAmphur)},
                    {"@AddressProvinceId", Fn.getComboboxId(cbbProvince)},
                    {"@AddressZipCode", txtZip.Text},
                    {"@AddressGeoId", Fn.getComboboxId(cbbGeography)},
                    {"@AddressMap", txtMap.Text},
                    {"@AddressPhone", txtPhone.Text},
                    {"@AddressWebsite", txtWebsite.Text},
                    {"@AddressRemark", txtRemark.Text},
                    {"@AddressStatus", Fn.getComboboxId(cbbStatus)},
                    };

                    Message.MessageConfirmation(strOpe, txtCode.Text, txtName.Text + " เลขที่ " + txtNumber.Text + " จังหวัด" + cbbProvince.Text);

                    using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                    {
                        var result = Mes.ShowDialog();

                        if (result == DialogResult.Yes)
                        {
                            Mes.Close();
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

                    
                }
                else
                {
                    Message.MessageResult("DU", "AD", "เลขที่ " + txtNumber.Text + " ชื่อ " + txtName.Text);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
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
                    {"@AddressCode", row.Cells["AddressCode"].Value.ToString()},
                    {"@AddressAuthorization", "0"},
                    {"@AddressInstitution", ""},
                    {"@AddressPrefix", "0"},
                    {"@AddressName", ""},
                    {"@AddressNumber", ""},
                    {"@AddressVillage", ""},
                    {"@AddressBuilding", ""},
                    {"@AddressRoom", ""},
                    {"@AddressFloor", ""},
                    {"@AddressMoo", ""},
                    {"@AddressSoi", ""},
                    {"@AddressRoad", ""},
                    {"@AddressTambolId", "0"},
                    {"@AddressAmphurId", "0"},
                    {"@AddressProvinceId", "0"},
                    {"@AddressZipCode", ""},
                    {"@AddressGeoId", "0"},
                    {"@AddressMap", ""},
                    {"@AddressPhone", ""},
                    {"@AddressWebsite", ""},
                    {"@AddressRemark", ""},
                    {"@AddressStatus", "0"},
                };

                db.Get("Spr_S_TblSaveAddress", Parameter, out strErr, out dt);

                btnCopy.Enabled = dt.Rows.Count == 1 ? true : false;

                cbbStatus.SelectedValue = dt.Rows[0]["AddressStatus"].ToString();
                cbbGenre.SelectedValue = dt.Rows[0]["AddressAuthorization"].ToString();
                txtInstitution.Text = dt.Rows[0]["AddressInstitution"].ToString();
                cbbPrefix.SelectedValue = dt.Rows[0]["AddressPrefix"].ToString();
                cbbGeography.SelectedValue = dt.Rows[0]["AddressGeoId"].ToString();
                cbbProvince.SelectedValue = dt.Rows[0]["AddressProvinceId"].ToString();
                cbbAmphur.SelectedValue = dt.Rows[0]["AddressAmphurId"].ToString();
                cbbTambol.SelectedValue = dt.Rows[0]["AddressTambolId"].ToString();

                txtSearch.Text = "";
                txtCode.Text = dt.Rows[0]["AddressCode"].ToString();
                txtFloor.Text = dt.Rows[0]["AddressFloor"].ToString();
                txtRoom.Text = dt.Rows[0]["AddressRoom"].ToString();
                txtVillage.Text = dt.Rows[0]["AddressVillage"].ToString();
                txtName.Text = dt.Rows[0]["AddressName"].ToString();
                txtRemark.Text = dt.Rows[0]["AddressRemark"].ToString();
                txtWebsite.Text = dt.Rows[0]["AddressWebsite"].ToString();
                txtMap.Text = dt.Rows[0]["AddressMap"].ToString();
                txtMoo.Text = dt.Rows[0]["AddressMoo"].ToString();
                txtBuilding.Text = dt.Rows[0]["AddressBuilding"].ToString();
                txtNumber.Text = dt.Rows[0]["AddressNumber"].ToString();
                txtPhone.Text = dt.Rows[0]["AddressPhone"].ToString();
                txtSoi.Text = dt.Rows[0]["AddressSoi"].ToString();
                txtRoad.Text = dt.Rows[0]["AddressRoad"].ToString();
                txtZip.Text = dt.Rows[0]["AddressZipCode"].ToString();
                txtSearch.Focus();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                FrmAnimatedProgress frm = new FrmAnimatedProgress(10);
                DataTable dtData = getDataForPrint();

                try
                {
                    if (dtData.Rows.Count > 0)
                    {
                        //string Path = Fn.getPath("App.Report");
                        //ReportName = "RSA-R-SENEL00002";

                        //DialogResult result = folderBrowserDialog.ShowDialog();

                        //rpt = new ReportDocument();
                        //rpt.Load(Path + ReportName + ".rpt");
                        //rpt.SetDataSource(dtData);

                        //crystalReportViewer.ReportSource = rpt;
                        //crystalReportViewer.Refresh();

                        //ExportToPath = folderBrowserDialog.SelectedPath + "\\" + txtCode.Text;
                        //rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, ExportToPath + ".pdf");
                        //rpt.Close();
                        //System.Diagnostics.Process.Start(@"" + ExportToPath + ".pdf");

                        //if (result == DialogResult.OK)
                        //{
                        //    rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, ExportToPath + ".pdf");
                        //    rpt.Close();
                        //}

                        //rpt.Close();
                        Clear();
                    }
                    else
                    {
                    }
                }
                catch (Exception)
                {
                    Clear();
                }
            }
        }

        private void FrmManageBank_KeyDown(object sender, KeyEventArgs e)
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

        private void cbbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List.getWhereList(cbbAmphur, "", cbbProvince.SelectedValue.ToString(), "Y", "Amphur");
                cbbAmphur.SelectedValue = "0";
                cbbAmphur.Enabled = true;
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
                txtZip.Text = "";
                List.getWhereList(cbbTambol, "", cbbAmphur.SelectedValue.ToString(), "Y", "Tambol");

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
                cbbTambol.SelectedValue = "0";
                cbbTambol.Enabled = true;
            }
            catch (Exception)
            {
            }
        }

        private void cbbGeography_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbbGeography.SelectedValue.ToString() == "0")
                {
                    List.GetList(cbbProvince, "Y", "ProvinceAll");
                }
                else
                {
                    List.getWhereList(cbbProvince, "", cbbGeography.SelectedValue.ToString(), "Y", "Province");
                }

                txtZip.Text = "";
            }
            catch (Exception)
            {
            }
        }

        private void cbbGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbGenre.SelectedValue.ToString() != "0")
            {
                cbbStatus.SelectedValue = "Y";
                txtName.Focus();
            }

            if (cbbGenre.SelectedValue.ToString() != "PE")
            {
                txtInstitution.Visible = true;
                lblInstitutionTh.Visible = true;
                lblInstitutionEn.Visible = true;
                lblDot.Visible = true;
            }
            else
            {
                txtInstitution.Visible = false;
                lblInstitutionTh.Visible = false;
                lblInstitutionEn.Visible = false;
                lblDot.Visible = false;
            }
        }

        private void cbbPrefix_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbPrefix.SelectedValue.ToString() != "0")
            {
                txtName.Focus();
            }
        }

        private void btnSearchZip_Click(object sender, EventArgs e)
        {
            string ProvinceId = "";
            string AmphurId = "";
            string TambolId = "";
            string Zipcode = "";

            try
            {
                if (txtSearch.Text != "")
                {
                    string[,] Parameter = new string[,]
                    {
                        {"@TambolName", txtSearch.Text},
                    };

                    db.Get("Spr_S_Postcode", Parameter, out strErr, out dt);

                    if (dt.Rows.Count == 1)
                    {
                        ProvinceId = dt.Rows[0]["ProvinceId"].ToString();
                        AmphurId = dt.Rows[0]["AmphurId"].ToString();
                        TambolId = dt.Rows[0]["TambolId"].ToString();
                        Zipcode = dt.Rows[0]["Zipcode"].ToString();

                        cbbGeography.SelectedValue = dt.Rows[0]["TambolGeoId"].ToString();
                        cbbProvince.SelectedValue = ProvinceId;
                        cbbAmphur.SelectedValue = AmphurId;
                        cbbTambol.SelectedValue = TambolId;
                        txtZip.Text = Zipcode;
                        txtSearch.Text = "";
                    }
                    else
                    {
                        var Mes = new FrmMessagesBoxOK("ตำบล '" + txtSearch.Text + "'", "Items can be found in " + dt.Rows.Count + " time(s)", "OK", "I4-8BEE9535E5-1");
                        Mes.ShowDialog();
                        Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void cbbTambol_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[,] Parameter = new string[,]
                    {
                        {"@Id", Fn.getComboboxId(cbbTambol)},
                        {"@Code", ""},
                        {"@NameTh", ""},
                        {"@NameEn", ""},
                        {"@AmphoeId", ""},
                        {"@ProvinceId", ""},
                        {"@GeoId", ""},
                        {"@PostcodeAll", ""},
                        {"@Remark", ""},
                        {"@Status", "0"},
                    };

                db.Get("Spr_S_TblMasterTambol", Parameter, out strErr, out dt);
                txtZip.Text = dt.Rows[0]["Zipcode"].ToString();
                btnExit.Focus();
            }
            catch (Exception)
            {
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            string strAddress = "";

            strAddress += "กรุณาส่ง";
            strAddress += txtName.Text != "" ? Environment.NewLine + cbbPrefix.Text : "";
            strAddress += txtName.Text;
            strAddress += txtName.Text != "" && txtPhone.Text != "" ? " (" + txtPhone.Text + ")" : "";
            strAddress += cbbGenre.SelectedValue.ToString() != "PE" ? Environment.NewLine + txtInstitution.Text : "";
            strAddress += txtNumber.Text != "" ? Environment.NewLine + "เลขที่ " + txtNumber.Text : "";
            strAddress += txtVillage.Text != "" ? " หมู่บ้าน" + txtVillage.Text : "";
            strAddress += txtBuilding.Text != "" ? " อาคาร" + txtBuilding.Text : "";
            strAddress += txtRoom.Text != "" ? " ห้อง " + txtRoom.Text : "";
            strAddress += txtFloor.Text != "" ? " ชั้น " + txtFloor.Text : "";
            strAddress += txtMoo.Text != "" ? " หมู่ที่ " + txtMoo.Text : "";
            strAddress += txtSoi.Text != "" ? " ซอย" + txtSoi.Text : "";

            if (txtRoad.Text != "")
            {
                strAddress += txtRoad.Text != "" ? " ถนน" + txtRoad.Text : "";

                if (cbbProvince.Text == "กรุงเทพมหานคร")
                {
                    strAddress += cbbTambol.SelectedIndex != 0 ? Environment.NewLine + "แขวง" + cbbTambol.Text : "";
                    strAddress += cbbAmphur.SelectedIndex != 0 ? " " + cbbAmphur.Text : "";
                }
                else
                {
                    strAddress += cbbTambol.SelectedIndex != 0 ? Environment.NewLine + "ตำบล" + cbbTambol.Text : "";
                    strAddress += cbbAmphur.SelectedIndex != 0 ? " อำเภอ" + cbbAmphur.Text : "";
                }
            }
            else
            {
                strAddress += txtRoad.Text != "" ? " ถนน" + txtRoad.Text : "";

                if (cbbProvince.Text == "กรุงเทพมหานคร")
                {
                    strAddress += cbbTambol.SelectedIndex != 0 ? " แขวง" + cbbTambol.Text + Environment.NewLine : "";
                    strAddress += cbbAmphur.SelectedIndex != 0 ? cbbAmphur.Text : "";
                }
                else
                {
                    strAddress += cbbTambol.SelectedIndex != 0 ? " ตำบล" + cbbTambol.Text + Environment.NewLine : "";
                    strAddress += cbbAmphur.SelectedIndex != 0 ? "อำเภอ" + cbbAmphur.Text : "";
                }
            }

            strAddress += cbbProvince.SelectedIndex != 0 ? " จังหวัด" + cbbProvince.Text : "";
            strAddress += txtZip.Text != "" ? " " + txtZip.Text : "";

            if (strAddress != "")
            {
                Clipboard.SetText(strAddress);
            }
        }
        public DataTable getDataForPrint()
        {
            DataTable dtAddress = new DataTable();
            dtAddress.Columns.Add(new DataColumn("AddressLine1", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("AddressLine2", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("AddressLine3", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("Mobile", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("Name", typeof(string)));

            dtAddress.Columns.Add(new DataColumn("Postcode1", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("Postcode2", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("Postcode3", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("Postcode4", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("Postcode5", typeof(string)));
            DataRow AddressRow;

            string[,] Parameter = new string[,]
            {
                {"@Code", txtCode.Text}
            };

            db.Get("Spr_S_PostAddress", Parameter, out strErr, out dt);

            if (string.IsNullOrEmpty(strErr))
            {
                AddressRow = dtAddress.NewRow();
                AddressRow["Name"] = dt.Rows[0]["AddressPrefix"].ToString() + dt.Rows[0]["AddressName"].ToString();
                AddressRow["Mobile"] = dt.Rows[0]["AddressPhone"].ToString();

                AddressRow["AddressLine1"] = dt.Rows[0]["AddressLine1"].ToString();
                AddressRow["AddressLine2"] = dt.Rows[0]["AddressLine2"].ToString();
                AddressRow["AddressLine3"] = dt.Rows[0]["AddressLine3"].ToString();

                AddressRow["Postcode1"] = dt.Rows[0]["Postcode1"].ToString();
                AddressRow["Postcode2"] = dt.Rows[0]["Postcode2"].ToString();
                AddressRow["Postcode3"] = dt.Rows[0]["Postcode3"].ToString();
                AddressRow["Postcode4"] = dt.Rows[0]["Postcode4"].ToString();
                AddressRow["Postcode5"] = dt.Rows[0]["Postcode5"].ToString();

                dtAddress.Rows.Add(AddressRow);
            }
            return dtAddress;
        }
    }
}