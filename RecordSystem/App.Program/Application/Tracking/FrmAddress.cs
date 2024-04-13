using System;
using SANSANG.Class;
using SANSANG.Database;
using System.Data;
using System.Windows.Forms;
using SANSANG.Utilites.App.Forms;
using SANSANG.Constant;
using System.Text.RegularExpressions;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace SANSANG
{
    public partial class FrmAddress : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;
        public string Laguage;

        public string Error = "";
        public string AppCode = "SAVAD00";
        public string AppName = "FrmAddress";
        public string Address = "";
        public string Detail = "";

        public bool Start = true;

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        private clsFunction Function = new clsFunction();
        private DataListConstant DataList = new DataListConstant();
        private clsMessage Message = new clsMessage();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsReport Report = new clsReport();
        private OperationConstant Operation = new OperationConstant();
        private StoreConstant Store = new StoreConstant();
        private TableConstant Table = new TableConstant();
        private clsInsert Insert = new clsInsert();
        private clsDelete Delete = new clsDelete();
        private clsBarcode Barcode = new clsBarcode();
        private clsSetting Setting = new clsSetting();
        private clsEdit Edit = new clsEdit();
        private clsLog Log = new clsLog();
        private Timer Timer = new Timer();
        public string[,] Parameter = new string[,] { };
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(20);
        private clsDate Date = new clsDate();
        private clsConvert Converts = new clsConvert();

        public FrmAddress(string UserIds, string UserNames, string UserSurNames, string UserTypes)
        {
            InitializeComponent();

            UserId = UserIds;
            UserName = UserNames;
            UserSurname = UserSurNames;
            UserType = UserTypes;
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
            Start = true;
            Laguage = clsSetting.ReadLanguageSetting();

            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "0"));
            List.GetLists(cbbAuthorizations, string.Format(DataList.StatusId, "5"));
            List.GetLists(cbbPrefix, string.Format(DataList.StatusId, "11"));

            List.GetLists(cbbProvince, string.Format(DataList.ProvinceId, "status", "1000"));
            List.GetLists(cbbAmphoe, string.Format(DataList.AmphoeId, "status", "1000"));
            List.GetLists(cbbTambol, string.Format(DataList.TambolId, "status", "1000"));
            List.GetLists(cbbGeography, DataList.GeographyId);

            gbFrm.Enabled = true;
            Clear();
            Timer.Stop();
        }

        public void SearchData(bool Search)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", Search? "" : ""},
                    {"@Code", Search? txtCode.Text : ""},
                    {"@Name", Search? txtName.Text : ""},
                    {"@User", ""},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.SelectAbbr},
                    {"@Institution", Search? txtInstitution.Text : ""},
                    {"@Number", Search? txtNumber.Text : ""},
                    {"@Village", Search ? txtVillage.Text : ""},
                    {"@Building", Search? txtBuilding.Text : ""},
                    {"@Room", Search? txtRoom.Text : ""},
                    {"@Floor", Search? txtFloor.Text : ""},
                    {"@Moo", Search? txtMoo.Text : ""},
                    {"@Soi", Search? txtSoi.Text : ""},
                    {"@Road", Search? txtRoad.Text : ""},
                    {"@ZipCode", Search? txtPostcode.Text : ""},
                    {"@Map", Search? txtMap.Text : ""},
                    {"@Phone", Search? txtPhone.Text : ""},
                    {"@Website", Search? txtWebsite.Text : ""},
                    {"@Remark", Search? txtRemark.Text : ""},
                    {"@Authorizations", Search? Function.GetComboId(cbbAuthorizations) : "0"},
                    {"@Prefix", Search? Function.GetComboId(cbbPrefix) : "0"},
                    {"@TambolId", Search? Function.GetComboId(cbbTambol) : "0"},
                    {"@AmphoeId", Search? Function.GetComboId(cbbAmphoe) : "0"},
                    {"@ProvinceId", Search? Function.GetComboId(cbbProvince) : "0"},
                    {"@GeoId", Search? Function.GetComboId(cbbGeography) : "0"},
                    {"@Status", Search? Function.GetComboId(cbbStatus) : "0"},
                };

                string Condition = Function.ShowConditons(GetCondition());
                lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;
                db.Get(Store.ManageAddress, Parameter, out Error, out dt);
                ShowGridView(dt);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public void ShowGridView(DataTable dt)
        {
            try
            {
                if (Function.GetRows(dt) == 0)
                {
                    GridView.DataSource = null;
                    ExportExcel.Visible = false;
                    txtCount.Text = Function.ShowNumberOfData(0);
                    txtSearch.Focus();
                }
                else
                {
                    GridView.DataSource = null;
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "SNo", "Numbers", "Names", "TambolName", "AmphoeName", "ProvinceName", "ZipCode", "Id");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                    Function.showGridViewFormatFromStore(dtGrid, GridView,
                          " ลำดับ", 50, true, mc, mc
                        , "เลขที่", 80, true, ml, ml
                        , "ชื่อ/บริษัท/ห้างร้าน", 230, true, ml, ml
                        , "ตำบล", 80, true, ml, ml
                        , "เขต/อำเภอ", 100, true, ml, ml
                        , "จังหวัด", 100, true, ml, ml
                        , "รหัสไปรษณีย์", 120, true, ml, ml
                        , "รหัส", 0, false, mc, mc
                        );

                    ExportExcel.Visible = true;
                    txtCount.Text = Function.ShowNumberOfData(dt.Rows.Count);

                    GridView.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void AddData(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(txtName.Text) || !string.IsNullOrEmpty(txtInstitution.Text)) && !string.IsNullOrEmpty(txtPostcode.Text) && Function.GetComboId(cbbStatus) != "0")
                {
                    if (!Function.IsDuplicates(Table.Address, txtInstitution.Text, txtNumber.Text, Function.GetComboId(cbbTambol), Detail: "Address No. " + txtNumber.Text + " at " + cbbTambol.Text))
                    {
                        txtCode.Text = Function.GetCodes(Table.ExpenseId, "", "Generated");

                        Parameter = new string[,]
                        {
                            {"@Id", ""},
                            {"@Code", txtCode.Text},
                            {"@Name", txtName.Text},
                            {"@User", UserId},
                            {"@IsActive", Function.GetComboId(cbbStatus) == "1000"? "1" : "0"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@Institution", txtInstitution.Text},
                            {"@Number", txtNumber.Text},
                            {"@Village", txtVillage.Text},
                            {"@Building", txtBuilding.Text},
                            {"@Room", txtRoom.Text},
                            {"@Floor", txtFloor.Text},
                            {"@Moo", txtMoo.Text},
                            {"@Soi", txtSoi.Text},
                            {"@Road", txtRoad.Text},
                            {"@ZipCode", txtPostcode.Text},
                            {"@Map", txtMap.Text},
                            {"@Phone", txtPhone.Text},
                            {"@Website", txtWebsite.Text},
                            {"@Remark", txtRemark.Text},
                            {"@Authorizations", Function.GetComboId(cbbAuthorizations)},
                            {"@Prefix", Function.GetComboId(cbbPrefix)},
                            {"@TambolId", Function.GetComboId(cbbTambol)},
                            {"@AmphoeId", Function.GetComboId(cbbAmphoe)},
                            {"@ProvinceId", Function.GetComboId(cbbProvince)},
                            {"@GeoId", Function.GetComboId(cbbGeography)},
                            {"@Status", Function.GetComboId(cbbStatus)},
                        };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageAddress, Parameter, txtCode.Text, Details: "Address No. " + txtNumber.Text + " at " + cbbTambol.Text))
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
                    {"@User", ""},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.SelectAbbr},
                    {"@Institution", ""},
                    {"@Number", ""},
                    {"@Village", ""},
                    {"@Building", ""},
                    {"@Room", ""},
                    {"@Floor", ""},
                    {"@Moo", ""},
                    {"@Soi", ""},
                    {"@Road", ""},
                    {"@ZipCode", ""},
                    {"@Map", ""},
                    {"@Phone", ""},
                    {"@Website", ""},
                    {"@Remark", ""},
                    {"@Authorizations", "0"},
                    {"@Prefix", "0"},
                    {"@TambolId", "0"},
                    {"@AmphoeId", "0"},
                    {"@ProvinceId", "0"},
                    {"@GeoId", "0"},
                    {"@Status", "0"},
                };

                db.Get(Store.ManageAddress, Parameter, out Error, out dt);
                ShowData(dt);
            }
        }

        private void Print(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text) && cbbProvince.SelectedValue.ToString() != "0")
            {
                int Number = Setting.GetNumberPrint();
                DataTable Data = GetDataForPrint(txtCode.Text);
                DataTable Datas = Data.Clone();

                Bitmap BarcodeImage = Barcode.QRCodeBitmap(txtCode.Text, Color.Black, Color.White, "Q", 3, false);

                using (var Stream = new MemoryStream())
                {
                    BarcodeImage.Save(Stream, ImageFormat.Bmp);

                    for (int i = 0; i < Number; i++)
                    {
                        foreach (DataRow dr in Data.Rows)
                        {
                            string Base64 = Convert.ToBase64String(Stream.ToArray());
                            dr["AddressBarcode"] = Base64;
                            Datas.Rows.Add(dr.ItemArray);
                        }
                    }
                }

                Report.Print(Datas, "dsPostAddress", "rptEnvelope.rdlc");
            }
        }

        private void cbbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Start)
                {
                    List.GetLists(cbbAmphoe, string.Format(DataList.AmphoeId, "ProvinceId", cbbProvince.SelectedValue.ToString()));
                    cbbAmphoe.SelectedValue = "0";
                    cbbAmphoe.Enabled = true;
                    txtPostcode.Text = "";
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void cbbAmphoe_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Start)
                {
                    List.GetLists(cbbTambol, string.Format(DataList.TambolId, "AmphoeId", cbbAmphoe.SelectedValue.ToString()));
                    txtPostcode.Text = "";
                    cbbTambol.SelectedValue = "0";
                    cbbTambol.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void cbbGeography_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Start)
                {
                    if (cbbGeography.SelectedValue.ToString() == "0")
                    {
                        List.GetLists(cbbProvince, string.Format(DataList.ProvinceId, "status", "1000"));
                    }
                    else
                    {
                        List.GetLists(cbbProvince, string.Format(DataList.ProvinceId, "GeoId", cbbGeography.SelectedValue.ToString()));
                    }

                    txtPostcode.Text = "";
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void cbbGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbAuthorizations.SelectedValue.ToString() != "0")
            {
                txtName.Focus();
            }

            if (cbbAuthorizations.SelectedValue.ToString() != "1039" &&
                cbbAuthorizations.SelectedValue.ToString() != "1090" &&
                cbbAuthorizations.SelectedValue.ToString() != "1095" &&
                cbbAuthorizations.SelectedValue.ToString() != "1096")
            {
                txtInstitution.Visible = true;
                lblInstitutionTh.Visible = true;
                lblInstitutionEn.Visible = true;
                lblDot.Visible = true;
                btnTitleCase.Visible = true;
                txtInstitution.Focus();
            }
            else
            {
                txtInstitution.Text = "";
                txtInstitution.Visible = false;
                lblInstitutionTh.Visible = false;
                lblInstitutionEn.Visible = false;
                lblDot.Visible = false;
                btnTitleCase.Visible = false;
            }
        }

        private void cbbPrefix_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbPrefix.SelectedValue.ToString() != "0")
            {
                txtName.Focus();
            }
        }

        private void SearchZipcode(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@User", ""},
                        {"@IsActive", ""},
                        {"@IsDelete", ""},
                        {"@Operation", Operation.SelectAbbr},
                        {"@TambolId", "0"},
                        {"@TambolCode", ""},
                        {"@AmphoeId", "0"},
                        {"@ProvinceId", "0"},
                        {"@Zipcode", ""},
                        {"@Search", txtSearch.Text},
                    };

                    db.Gets(Store.ManagePostcode, Parameter, out Error, out ds);

                    dt = ds.Tables[1];
                    int Row = Function.GetRows(dt);

                    if (Row == 1)
                    {
                        cbbGeography.SelectedValue = dt.Rows[0]["GeoId"].ToString();
                        cbbProvince.SelectedValue = dt.Rows[0]["ProvinceId"].ToString();
                        cbbAmphoe.SelectedValue = dt.Rows[0]["AmphoeId"].ToString();
                        cbbTambol.SelectedValue = dt.Rows[0]["TambolId"].ToString();
                        txtPostcode.Text = dt.Rows[0]["Zipcode"].ToString();
                        txtSearch.Text = "";
                        txtSearch.Focus();
                    }
                    else
                    {
                        if (Row <= 0)
                        {
                            Detail = string.Format("Tambol '{0}' can not found please try again.", txtSearch.Text, dt.Rows.Count);
                        }
                        else
                        {
                            string Districts = "(";
                            int Round = Row - 1;

                            for (int i = 0; i < Row; i++)
                            {
                                int Now = i;
                                Districts += dt.Rows[i]["AmphoeName"].ToString();
                                Districts += Now == Round ? ")" : ", ";
                            }

                            Detail = string.Format("Tambol '{0}' can be found in {1} times" + Environment.NewLine + "Please fill which one is district {2} at the end.", txtSearch.Text, dt.Rows.Count, Districts);
                        }

                        Message.MessageConfirmation("F", "", Detail);
                        var Popup = new FrmMessagesBoxOK(Message.strOperation, Message.strMes, "OK", Message.strImage);

                        Popup.ShowDialog();
                        txtSearch.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void cbbTambol_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Start && Function.GetComboId(cbbTambol) != "0")
                {
                    Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@User", ""},
                        {"@IsActive", ""},
                        {"@IsDelete", ""},
                        {"@Operation", Operation.SelectAbbr},
                        {"@TambolId", Function.GetComboId(cbbTambol)},
                        {"@TambolCode", ""},
                        {"@AmphoeId", "0"},
                        {"@ProvinceId", "0"},
                        {"@Zipcode", ""},
                        {"@Search", ""},
                    };

                    db.Get(Store.ManagePostcode, Parameter, out Error, out dt);

                    if (Function.GetRows(dt) != 0)
                    {
                        string Remark = dt.Rows[0]["Remarks"].ToString();
                        string Zipcode = dt.Rows[0]["Zipcode"].ToString();

                        txtPostcode.Text = Zipcode;
                        txtRemark.Text = Regex.Replace(Remark, @"\s+", "") == Zipcode ? "" : Remark;
                    }

                    btnExit.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void CopyData(object sender, EventArgs e)
        {
            Address = "";

            Address += "กรุณาส่ง";
            Address += txtName.Text != "" ? Environment.NewLine + cbbPrefix.Text : "";
            Address += txtName.Text;
            Address += txtName.Text != "" && txtPhone.Text != "" ? " (" + txtPhone.Text + ")" : "";
            Address += cbbAuthorizations.SelectedValue.ToString() != "1039" ? Environment.NewLine + txtInstitution.Text : "";
            Address += txtNumber.Text != "" ? Environment.NewLine + "เลขที่ " + txtNumber.Text : "";
            Address += txtVillage.Text != "" ? " หมู่บ้าน" + txtVillage.Text : "";
            Address += txtBuilding.Text != "" ? " อาคาร " + txtBuilding.Text : "";
            Address += txtRoom.Text != "" ? " ห้อง " + txtRoom.Text : "";
            Address += txtFloor.Text != "" ? " ชั้น " + txtFloor.Text : "";
            Address += txtMoo.Text != "" ? " หมู่ที่ " + txtMoo.Text : "";
            Address += txtSoi.Text != "" ? " ซอย" + txtSoi.Text : "";

            if (txtRoad.Text != "")
            {
                Address += txtRoad.Text != "" ? " ถนน" + txtRoad.Text : "";

                if (cbbProvince.Text == "กรุงเทพมหานคร")
                {
                    Address += cbbTambol.SelectedIndex != 0 ? Environment.NewLine + "แขวง" + cbbTambol.Text : "";
                    Address += cbbAmphoe.SelectedIndex != 0 ? " " + cbbAmphoe.Text : "";
                }
                else
                {
                    Address += cbbTambol.SelectedIndex != 0 ? Environment.NewLine + "ตำบล" + cbbTambol.Text : "";
                    Address += cbbAmphoe.SelectedIndex != 0 ? " อำเภอ" + cbbAmphoe.Text : "";
                }
            }
            else
            {
                Address += txtRoad.Text != "" ? " ถนน" + txtRoad.Text : "";

                if (cbbProvince.Text == "กรุงเทพมหานคร")
                {
                    Address += cbbTambol.SelectedIndex != 0 ? " แขวง" + cbbTambol.Text + Environment.NewLine : "";
                    Address += cbbAmphoe.SelectedIndex != 0 ? cbbAmphoe.Text : "";
                }
                else
                {
                    Address += cbbTambol.SelectedIndex != 0 ? " ตำบล" + cbbTambol.Text + Environment.NewLine : "";
                    Address += cbbAmphoe.SelectedIndex != 0 ? "อำเภอ" + cbbAmphoe.Text : "";
                }
            }

            Address += cbbProvince.SelectedIndex != 0 ? " จังหวัด" + cbbProvince.Text : "";
            Address += txtPostcode.Text != "" ? " " + txtPostcode.Text : "";

            if (Address != "")
            {
                Clipboard.SetText(Address);
            }
        }

        public DataTable GetDataForPrint(string Code)
        {
            string[,] Parameter = new string[,]
            {
                {"@Code", Code}
            };

            db.Get(Store.FnGetPostAddress, Parameter, out Error, out dt);
            return dt;
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                SearchZipcode(sender, e);
            }
        }

        private void txtZip_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPostcode.Text))
            {
                cbbStatus.SelectedValue = "1000";
            }
        }

        private void btnTaq_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                //FrmPrintAddress Frm = new FrmPrintAddress(txtCode.Text);
                //Frm.ShowDialog();
                //Clear();
            }
        }

        public void ShowData(DataTable Data)
        {
            try
            {

                if (Function.GetRows(Data) > 0)
                {
                    btnCopy.Enabled = true;
                    Address = Data.Rows[0]["Address"].ToString();
                    txtId.Text = Data.Rows[0]["Id"].ToString();

                    cbbPrefix.SelectedValue = Data.Rows[0]["Prefix"].ToString();
                    txtCode.Text = Data.Rows[0]["Code"].ToString();
                    txtFloor.Text = Data.Rows[0]["Floor"].ToString();
                    txtRoom.Text = Data.Rows[0]["Room"].ToString();
                    txtVillage.Text = Data.Rows[0]["Village"].ToString();
                    txtName.Text = Data.Rows[0]["Name"].ToString();
                    txtWebsite.Text = Data.Rows[0]["Website"].ToString();
                    txtMap.Text = Data.Rows[0]["Map"].ToString();
                    txtMoo.Text = Data.Rows[0]["Moo"].ToString();
                    txtBuilding.Text = Data.Rows[0]["Building"].ToString();
                    txtNumber.Text = Data.Rows[0]["Number"].ToString();
                    txtPhone.Text = Data.Rows[0]["Phone"].ToString();
                    txtSoi.Text = Data.Rows[0]["Soi"].ToString();
                    txtRoad.Text = Data.Rows[0]["Road"].ToString();

                    cbbAuthorizations.SelectedValue = Data.Rows[0]["Authorizations"].ToString();
                    txtInstitution.Text = Data.Rows[0]["Institution"].ToString();

                    cbbGeography.SelectedValue = Data.Rows[0]["GeoId"].ToString();
                    cbbProvince.SelectedValue = Data.Rows[0]["ProvinceId"].ToString();
                    cbbAmphoe.SelectedValue = Data.Rows[0]["AmphoeId"].ToString();
                    cbbTambol.SelectedValue = Data.Rows[0]["TambolId"].ToString();

                    cbbStatus.SelectedValue = Data.Rows[0]["Status"].ToString();

                    txtPostcode.Text = Data.Rows[0]["ZipCode"].ToString();
                    txtRemark.Text = Data.Rows[0]["Remark"].ToString();

                    txtSearch.Text = "";
                    GridView.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        public void Clear()
        {
            Start = false;
            txtInstitution.Text = "";
            Address = "";
            txtId.Text = "";

            cbbStatus.SelectedValue = "0";
            cbbAuthorizations.SelectedValue = "0";
            cbbPrefix.SelectedValue = "0";
            cbbGeography.SelectedValue = "0";
            cbbProvince.SelectedValue = "0";
            cbbAmphoe.SelectedValue = "0";
            cbbTambol.SelectedValue = "0";

            cbbAmphoe.Enabled = false;
            cbbTambol.Enabled = false;

            txtInstitution.Visible = false;
            lblInstitutionTh.Visible = false;
            lblInstitutionEn.Visible = false;
            lblDot.Visible = false;
            btnTitleCase.Visible = false;

            txtFloor.Text = "";
            txtRoom.Text = "";
            txtVillage.Text = "";
            txtName.Text = "";
            txtRemark.Text = "";
            txtWebsite.Text = "";
            txtMap.Text = "";
            txtMoo.Text = "";
            txtBuilding.Text = "";
            txtNumber.Text = "";
            txtPhone.Text = "";
            txtPostcode.Text = "";
            txtSoi.Text = "";
            txtRoad.Text = "";
            txtSearch.Text = "";
            txtCode.Text = "";
            txtCount.Text = Function.ShowNumberOfData(0);
            btnCopy.Enabled = false;

            SearchData(false);
        }

        private void Exit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearData(object sender, EventArgs e)
        {
            Clear();
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";

                strCondition += txtCode.Text != "" ? ", รหัสอ้างอิง: " + txtCode.Text : "";
                strCondition += txtSearch.Text != "" ? ", ตำบล: " + txtSearch.Text : "";

                strCondition += txtInstitution.Text != "" ? ", " + cbbAuthorizations.Text + ": " + txtInstitution.Text : "";
                strCondition += cbbPrefix.Text != ":: กรุณาเลือก ::" ? ", คำนำหน้า: " + cbbPrefix.Text : "";

                strCondition += txtName.Text != "" ? ", ชื่อ: " + txtName.Text : "";
                strCondition += txtNumber.Text != "" ? ", เลขที่: " + txtNumber.Text : "";
                strCondition += txtVillage.Text != "" ? ", หมู่บ้าน: " + txtVillage.Text : "";
                strCondition += txtBuilding.Text != "" ? ", อาคาร: " + txtBuilding.Text : "";
                strCondition += txtRoom.Text != "" ? ", ห้อง: " + txtRoom.Text : "";
                strCondition += txtFloor.Text != "" ? ", ชั้น: " + txtFloor.Text : "";
                strCondition += txtMoo.Text != "" ? ", หมู่: " + txtMoo.Text : "";
                strCondition += txtSoi.Text != "" ? ", ซอย: " + txtSoi.Text : "";
                strCondition += txtRoad.Text != "" ? ", ถนน: " + txtRoad.Text : "";

                strCondition += cbbTambol.Text != ":: กรุณาเลือก ::" ? ", ตำบล: " + cbbTambol.Text : "";
                strCondition += cbbAmphoe.Text != ":: กรุณาเลือก ::" ? ", อำเภอ: " + cbbAmphoe.Text : "";
                strCondition += cbbProvince.Text != ":: กรุณาเลือก ::" ? ", จังหวัด: " + cbbProvince.Text : "";
                strCondition += txtPostcode.Text != "" ? ", รหัสไปรณีย์: " + txtPostcode.Text : "";

                strCondition += cbbGeography.Text != ":: กรุณาเลือก ::" ? ", ภูมิภาค: " + cbbGeography.Text : "";

                strCondition += txtMap.Text != "" ? ", แผนที่: " + txtMap.Text : "";
                strCondition += txtPhone.Text != "" ? ", เบอร์: " + txtPhone.Text : "";
                strCondition += txtWebsite.Text != "" ? ", เว็บไซต์: " + txtWebsite.Text : "";
                strCondition += txtRemark.Text != "" ? ", หมายเหตุ: " + txtRemark.Text : "";

                strCondition += cbbStatus.Text != ":: กรุณาเลือก ::" ? ", สถานะ: " + cbbStatus.Text : "";

                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return "";
            }
        }

        private void DeleteData(object sender, EventArgs e)
        {
            try
            {
                if (Delete.Drop(AppCode, AppName, UserId, 0, Table.Address, txtCode, Details: "Address No. " + txtNumber.Text + " at " + cbbTambol.Text, true))
                {
                    Clear();
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
                        {"@Code", txtCode.Text},
                        {"@Name", txtName.Text},
                        {"@User", UserId},
                        {"@IsActive", Function.GetComboId(cbbStatus) == "1000"? "1" : "0"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.UpdateAbbr},
                        {"@Institution", txtInstitution.Text},
                        {"@Number", txtNumber.Text},
                        {"@Village", txtVillage.Text},
                        {"@Building", txtBuilding.Text},
                        {"@Room", txtRoom.Text},
                        {"@Floor", txtFloor.Text},
                        {"@Moo", txtMoo.Text},
                        {"@Soi", txtSoi.Text},
                        {"@Road", txtRoad.Text},
                        {"@ZipCode", txtPostcode.Text},
                        {"@Map", txtMap.Text},
                        {"@Phone", txtPhone.Text},
                        {"@Website", txtWebsite.Text},
                        {"@Remark", txtRemark.Text},
                        {"@Authorizations", Function.GetComboId(cbbAuthorizations)},
                        {"@Prefix", Function.GetComboId(cbbPrefix)},
                        {"@TambolId", Function.GetComboId(cbbTambol)},
                        {"@AmphoeId", Function.GetComboId(cbbAmphoe)},
                        {"@ProvinceId", Function.GetComboId(cbbProvince)},
                        {"@GeoId", Function.GetComboId(cbbGeography)},
                        {"@Status", Function.GetComboId(cbbStatus)},
                    };

                    if (Edit.Update(AppCode, AppName, UserId, Store.ManageAddress, Parameter, txtCode.Text, Details: "Address No. " + txtNumber.Text + " at " + cbbTambol.Text))
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

        private void SearchData(object sender, EventArgs e)
        {
            SearchData(true);
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
            if (keyCode == "Ctrl+P")
            {
                Print(sender, e);
            }
            if (keyCode == "Enter")
            {
                Form Frm = (Form)sender;

                if (Frm.ActiveControl.Text != txtSearch.Text)
                {
                    SearchData(true);
                }
            }
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (txtPhone.Text != "")
            {
                txtPhone.Text = Function.ConvertPhoneNumber(txtPhone.Text);
            }
        }

        private void btnTitleCase_Click(object sender, EventArgs e)
        {
            Converts.TitleCase(txtInstitution);
        }
    }
}