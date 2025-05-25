using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Web.Services.Description;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;
using Org.BouncyCastle.Crypto.Macs;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;
using Color = System.Drawing.Color;

namespace SANSANG
{
    public partial class FrmStocks : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "SAVPS00";
        public string AppName = "FrmStocks";
        public string Error = "";
        public string Laguage;

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        private clsDate Date = new clsDate();
        private clsDelete Delete = new clsDelete();
        private clsEdit Edit = new clsEdit();
        private clsImage Images = new clsImage();
        private clsInsert Insert = new clsInsert();
        private clsFunction Function = new clsFunction();
        private clsSetting Setting = new clsSetting();
        private clsMessage Message = new clsMessage();
        private clsBarcode Barcode = new clsBarcode();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsHelpper Helper = new clsHelpper();
        private DataListConstant DataList = new DataListConstant();
        private clsLog Log = new clsLog();
        private clsEvent Event = new clsEvent();
        private TableConstant Table = new TableConstant();
        private StoreConstant Store = new StoreConstant();
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(20);
        private OperationConstant Operation = new OperationConstant();
        private clsConvert Converts = new clsConvert();
        private CharacterConstant CharType = new CharacterConstant();
        private Timer Timer = new Timer();
        public string[,] Parameter = new string[,] { };

        public FrmStocks(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
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
            Timer.Interval = (2000);
            Timer.Tick += new EventHandler(LoadList);
            Timer.Start();
        }

        private void LoadList(object sender, EventArgs e)
        {
            Laguage = clsSetting.ReadLanguageSetting();
            List.GetList(cbbProducts, DataList.Products);
            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "7"));
            List.GetLists(cbbShop, DataList.ShopId);
            List.GetLists(cbbUnit, DataList.UnitId);
            List.GetLists(cbbPayment, DataList.MoneyId);

            Clear();
            gbMain.Enabled = true;
            Timer.Stop();
        }

        public void Clear()
        {
            Function.ClearAll(gbMain);
            Images.ShowDefault(pbImage);

            pb_Sale_False.Visible = true;
            pb_Sale_True.Visible = false;

            pbQrcode.Image = null;

            Search(false);
        }

        public void Search(bool Search)
        {
            Parameter = new string[,]
            {
                {"@Id ", Search? txtId.Text : ""},
                {"@Code", Search? txtCode.Text : ""},
                {"@Barcode", Search? txtBarcode.Text : ""},
                {"@User", ""},
                {"@ProductId", Search? Function.getComboboxId(cbbProducts) : "0"},
                {"@Lot", Search? txtLot.Text : ""},
                {"@SerialNumber", Search? txtSerialNumber.Text : ""},
                {"@Qty", Search? txtQty.Text : ""},
                {"@UnitId", Search? Function.getComboboxId(cbbUnit) : "0"},
                {"@Price", Search? Function.RemoveComma(txtPrice.Text) : "0.00"},
                {"@PriceSpacial", Search? Function.RemoveComma(txtPriceSpacial.Text) : "0.00"},
                {"@Discount", Search? Function.RemoveComma(txtDiscount.Text) : "0.00"},
                {"@PriceExVat", Search? Function.RemoveComma(txtPriceExVat.Text) : "0.00"},
                {"@Vat", Search? Function.RemoveComma(txtVat.Text) : "0.00"},
                {"@VatValue", Search? Function.RemoveComma(txtVatValue.Text) : "0.00"},
                {"@MoneyId", Search? Function.getComboboxId(cbbPayment) : "0"},
                {"@ShopId", Search? Function.getComboboxId(cbbShop) : "0"},
                {"@Branch", Search? txtBranch.Text : ""},
                {"@Location", Search? txtLocation.Text : ""},
                {"@BuyDate", Search? txtBuyDate.Text == ""? "" :  Date.GetDate(dtp: BuyDate, Format : 4) : ""},
                {"@ReceiveDate", Search? txtReceiveDate.Text == ""? "" :  Date.GetDate(dtp: ReceiveDate, Format : 4) : ""},
                {"@Receipt", Search? txtReceipt.Text : ""},
                {"@WarrantyDate", Search? txtWarrantyDate.Text == ""? "" :  Date.GetDate(dtp: WarrantyDate, Format : 4) : ""},
                {"@IsSale", Search? cb_Sale.Checked? "1" : "0" : ""},
                {"@PriceSale", Search? Function.RemoveComma(txtPriceSale.Text) : "0.00"},
                {"@PriceRetail", Search? Function.RemoveComma(txtPriceRetail.Text) : "0.00"},
                {"@PriceWholeSale", Search? Function.RemoveComma(txtPriceWholeSale.Text) : "0.00"},
                {"@PriceMember", Search? Function.RemoveComma(txtPriceMember.Text) : "0.00"},
                {"@ManufacturingDate", Search? txtMfg.Text == ""? "" :  Date.GetDate(dtp: Mfg, Format : 4) : ""},
                {"@ExpiryDate", Search? txtExp.Text == ""? "" :  Date.GetDate(dtp: Exp, Format : 4) : ""},
                {"@Remark", Search? txtRemark.Text : ""},
                {"@Status", Search? Function.getComboboxId(cbbStatus) : "0"},
                {"@IsActive", "1"},
                {"@IsDelete", "0"},
                {"@OrderNo", Search? txtOrder.Text : ""},
                {"@Reference", Search? txtReference.Text : ""},
                {"@Operation", Operation.SelectAbbr},
            };

            string Condition = Function.ShowConditons(GetCondition());
            lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;

            db.Gets(Store.ManageStock, Parameter, out Error, out ds);

            if (string.IsNullOrEmpty(Error))
            {
                ShowGridView(ds.Tables[0]);
            }
        }

        public void ShowGridView(DataTable dt)
        {
            try
            {
                GridView.DataSource = null;

                if (Function.GetRows(dt) > 0)
                {
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "SNo", "Names", "Brand", "Size", "Prices", "Numbers", "Id", "Expired");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
                    DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                    Function.showGridViewFormatFromStore(dtGrid, GridView,
                          "ลำดับ", 50, true, mc, mc
                        , "สินค้า", 200, true, mc, ml
                        , "แบรนด์", 80, true, mc, ml
                        , "ขนาด", 80, true, mc, ml
                        , "ราคา", 80, true, mc, mr
                        , "จำนวน", 80, true, mc, mr
                        , "", 0, false, mc, mc
                        , "", 0, false, mc, mc
                    );

                    foreach (DataGridViewRow dgvr in GridView.Rows)
                    {
                        if (dgvr.Cells[7].Value.ToString() == "1")
                        {
                            dgvr.DefaultCellStyle.ForeColor = Color.Crimson;
                        }
                    }

                    txtCount.Text = Function.ShowNumberOfData(dt.Rows.Count);
                    GridView.Focus();
                }
                else
                {
                    txtCount.Text = Function.ShowNumberOfData(0);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void ShowData(DataTable dt)
        {
            pbQrcode.Image = null;
            txtId.Text = dt.Rows[0]["Id"].ToString();
            txtCode.Text = dt.Rows[0]["Code"].ToString();
            txtBarcode.Text = dt.Rows[0]["Barcode"].ToString();
            txtNameEn.Text = dt.Rows[0]["NameEn"].ToString();
            txtBrand.Text = dt.Rows[0]["Brand"].ToString();
            txtLot.Text = dt.Rows[0]["Lot"].ToString();
            txtSerialNumber.Text = dt.Rows[0]["SerialNumber"].ToString();
            txtBranch.Text = dt.Rows[0]["Branch"].ToString();
            txtReceipt.Text = dt.Rows[0]["Receipt"].ToString();
            txtQty.Text = dt.Rows[0]["Qty"].ToString();
            txtPrice.Text = dt.Rows[0]["Price"].ToString();
            txtPriceSpacial.Text = dt.Rows[0]["PriceSpacial"].ToString();
            txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
            txtPriceExVat.Text = dt.Rows[0]["PriceExVat"].ToString();
            txtVat.Text = dt.Rows[0]["Vat"].ToString();
            txtVatValue.Text = dt.Rows[0]["VatValue"].ToString();
            txtLocation.Text = dt.Rows[0]["Location"].ToString();
            txtOrder.Text = dt.Rows[0]["OrderNo"].ToString();
            txtReference.Text = dt.Rows[0]["Reference"].ToString();

            if (dt.Rows[0]["IsSale"].ToString() == "True")
            {
                pb_Sale_True.Show();
                pb_Sale_False.Hide();
                cb_Sale.Checked = true;
            }
            else
            {
                pb_Sale_True.Hide();
                pb_Sale_False.Show();
                cb_Sale.Checked = false;
            }

            txtPriceSale.Text = dt.Rows[0]["PriceSale"].ToString();
            txtPriceMember.Text = dt.Rows[0]["PriceMember"].ToString();
            txtPriceRetail.Text = dt.Rows[0]["PriceRetail"].ToString();
            txtPriceWholeSale.Text = dt.Rows[0]["PriceWholeSale"].ToString();
            txtRemark.Text = dt.Rows[0]["Remark"].ToString();

            cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
            cbbProducts.SelectedValue = dt.Rows[0]["ProductId"].ToString();
            cbbUnit.SelectedValue = dt.Rows[0]["UnitId"].ToString();
            cbbPayment.SelectedValue = dt.Rows[0]["MoneyId"].ToString();
            cbbShop.SelectedValue = dt.Rows[0]["ShopId"].ToString();

            string Barcodes = dt.Rows[0]["Barcode"].ToString();

            if (!string.IsNullOrEmpty(Barcodes))
            {
                pbQrcode.Image = Barcode.EAN13(Barcodes, Color.Black, Color.White, 60);
            }
            else
            {
                pbQrcode.Image = null;
            }

            Function.SetDate(BuyDate, txtBuyDate, dt.Rows[0]["BuyDate"].ToString());
            Function.SetDate(ReceiveDate, txtReceiveDate, dt.Rows[0]["ReceiveDate"].ToString());
            Function.SetDate(Mfg, txtMfg, dt.Rows[0]["ManufacturingDate"].ToString());
            Function.SetDate(Exp, txtExp, dt.Rows[0]["ExpiryDate"].ToString());
            Function.SetDate(WarrantyDate, txtWarrantyDate, dt.Rows[0]["WarrantyDate"].ToString());

            pbImage.ImageLocation = dt.Rows[0]["Locations"].ToString();
            GridView.Focus();
        }
        private string GetCondition()
        {
            try
            {
                string strCondition = "";

                if (cb_Sale.Checked)
                {
                    strCondition += ", สินค้าสำหรับจำหน่าย";
                }

                strCondition += txtCode.Text != "" ? ", รหัส: " + txtCode.Text : "";
                strCondition += txtBarcode.Text != "" ? ", Barcode: " + txtBarcode.Text : "";
                strCondition += cbbProducts.Text != ":: กรุณาเลือก ::" ? ", " + "สินค้า: " + cbbProducts.Text : "";
                strCondition += txtLot.Text != "" ? txtLot.Text != "-" ? ", ลอต: " + txtLot.Text : "" : "";
                strCondition += txtSerialNumber.Text != "" ? txtLot.Text != "-" ? ", หมายเลข: " + txtSerialNumber.Text : "" : "";
                strCondition += txtPrice.Text != "" ? ", ราคา: " + txtPrice.Text : "";
                strCondition += txtVat.Text != "" ? ", ภาษี: " + txtVat.Text : "";
                strCondition += txtRemark.Text != "" ? ", รายละเอียด: " + txtRemark.Text : "";
                strCondition += cbbStatus.Text != ":: กรุณาเลือก ::" ? ", " + "สถานะ: " + cbbStatus.Text : "";

                strCondition += txtLocation.Text != "" ? ", สถานที่จัดเก็บ: " + txtLocation.Text : "";
                strCondition += txtPrice.Text != "" ? ", ราคา: " + txtPrice.Text : "";

                strCondition += txtReceipt.Text != "" ? ", เลขที่ใบเสร็จ: " + txtReceipt.Text : "";
                strCondition += cbbPayment.Text != ":: กรุณาเลือก ::" ? ", " + "วิธีชำระเงิน: " + cbbPayment.Text : "";
                strCondition += cbbStatus.Text != ":: กรุณาเลือก ::" ? ", " + "สถานะสินค้า: " + cbbStatus.Text : "";

                strCondition += txtBuyDate.Text != "" ? ", วันที่ซื้อ: " + txtBuyDate.Text : "";
                strCondition += txtReceiveDate.Text != "" ? ", วันที่นำเข้า: " + txtReceiveDate.Text : "";

                strCondition += cbbShop.Text != ":: กรุณาเลือก ::" ? ", " + "ร้าน: " + cbbShop.Text : "";
                strCondition += txtBranch.Text != "" ? ", สาขา: " + txtBranch.Text : "";

                strCondition += txtOrder.Text != "" ? ", เลขคำสั่งซื้อ: " + txtPrice.Text : "";
                strCondition += txtReference.Text != "" ? ", เลขอ้างอิง: " + txtPrice.Text : "";

                strCondition += txtExp.Text != "" ? ", วันหมดอายุ: " + txtExp.Text : "";
                strCondition += txtMfg.Text != "" ? ", วันที่ผลิต: " + txtMfg.Text : "";
                strCondition += txtWarrantyDate.Text != "" ? ", วันสิ้นสุดประกัน: " + txtWarrantyDate.Text : "";

                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return "";
            }
        }

        private void ClearData(object sender, EventArgs e)
        {
            Clear();
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            string keyCode = Function.keyPress(sender, e);

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

            if (keyCode == "Ctrl+X")
            {
                this.Close();
            }

            if (keyCode == "Ctrl+F")
            {
                Search(true);
            }

            if (keyCode == "Alt+C")
            {
                ClearData(sender, e);
            }

            if (keyCode == "Enter")
            {
                Form Frm = (Form)sender;
                Search(true);
            }
        }

        private void cbNSale_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_Sale.Checked == true)
            {
                txtPriceSale.Enabled = true;
                txtPriceMember.Enabled = true;
                txtPriceWholeSale.Enabled = true;
                txtPriceRetail.Enabled = true;

                txtPriceSale.Text = "";
                txtPriceMember.Text = "";
                txtPriceWholeSale.Text = "";
                txtPriceRetail.Text = "";
                txtPriceSale.Focus();
            }
            else
            {
                txtPriceSale.Enabled = false;
                txtPriceMember.Enabled = false;
                txtPriceWholeSale.Enabled = false;
                txtPriceRetail.Enabled = false;

                txtPriceSale.Text = "";
                txtPriceMember.Text = "";
                txtPriceWholeSale.Text = "";
                txtPriceRetail.Text = "";
            }
        }

        private void PriceKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtVat.Text == "")
                {
                    txtVat.Focus();
                }
                else
                {
                    CalVat();
                }
            }
        }

        private void VatKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtVat.Text != "")
                {
                    CalVat();
                    GridView.Focus();
                }
            }
        }

        private void CalVat()
        {
            try
            {
                double Price = Convert.ToDouble(txtPrice.Text);
                double Vat = Convert.ToDouble(txtVat.Text);
                double VatAmount = (Price * Vat) / 100;
                double PriceExVat = Price - VatAmount;

                txtPriceExVat.Text = string.Format("{0:n}", PriceExVat);
                txtVatValue.Text = string.Format("{0:n}", VatAmount);
            }
            catch
            {
                txtPriceExVat.Text = "0.00";
                txtVatValue.Text = "0.00";
            }
        }

        private void SpecialPriceKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                double Price = Convert.ToDouble(txtPrice.Text);
                double Special = Convert.ToDouble(txtPriceSpacial.Text);
                double Amount = Price - Special;
                txtDiscount.Text = string.Format("{0:n}", Amount);
                GridView.Focus();
            }
        }

        private void SalePriceLeave(object sender, EventArgs e)
        {
            try
            {
                if (txtPriceSale.Text != "")
                {
                    txtPriceSale.Text = string.Format("{0:n}", Convert.ToDouble(txtPriceSale.Text));
                }
            }
            catch
            {

            }
        }

        private void MemberPriceLeave(object sender, EventArgs e)
        {
            if (txtPriceMember.Text != "")
            {
                txtPriceMember.Text = string.Format("{0:n}", Convert.ToDouble(txtPriceMember.Text));
            }
        }

        private void RetailPriceLeave(object sender, EventArgs e)
        {
            if (txtPriceWholeSale.Text != "")
            {
                txtPriceWholeSale.Text = string.Format("{0:n}", Convert.ToDouble(txtPriceWholeSale.Text));
            }
        }

        private void PriceLeave(object sender, EventArgs e)
        {
            if (txtPrice.Text != "")
            {
                txtPrice.Text = string.Format("{0:n}", Convert.ToDouble(txtPrice.Text));
            }
        }

        private void SpecialPriceLeave(object sender, EventArgs e)
        {
            if (txtPriceSpacial.Text != "")
            {
                txtPriceSpacial.Text = string.Format("{0:n}", Convert.ToDouble(txtPriceSpacial.Text));
            }
        }

        private void CodeTextChanged(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                pbQrcode.Image = Barcode.QRCode(txtCode.Text, Color.Black, Color.White, "Q", 3, false);
            }
        }

        private void PriceRetailLeave(object sender, EventArgs e)
        {
            if (txtPriceRetail.Text != "")
            {
                txtPriceRetail.Text = string.Format("{0:n}", Convert.ToDouble(txtPriceRetail.Text));
            }
        }

        private void MadeInLeave(object sender, EventArgs e)
        {

        }

        public void SetDate(object sender, EventArgs e)
        {
            Helper.ShowDate(sender, this);
        }

        private void Ticker(object sender, EventArgs e)
        {
            Helper.CheckboxTicker(sender, this);
            GridView.Focus();
        }

        private void BarodeKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Function.IsCharacter(e.KeyChar, CharType.Tracking))
            {
                e.Handled = true;
            }
            else
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter) && !string.IsNullOrEmpty(txtBarcode.Text))
                {
                    Search(true);
                    SearchBarcode();
                }
            }
        }

        private void NameEnLeave(object sender, EventArgs e)
        {
            Converts.TitleCase(txtNameEn);
        }

        private void SearchData(object sender, EventArgs e)
        {
            Search(true);
        }

        private void Exit(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DeleteData(object sender, EventArgs e)
        {
            try
            {
                if (Delete.Drop(AppCode, AppName, UserId, 0, Table.Stocks, txtCode, Details: GetDetails(), true))
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
                        {"@Id ", txtId.Text},
                        {"@Code", txtCode.Text},
                        {"@Barcode", txtBarcode.Text},
                        {"@User", ""},
                        {"@ProductId", Function.getComboboxId(cbbProducts)},
                        {"@Lot", txtLot.Text},
                        {"@SerialNumber", txtSerialNumber.Text},
                        {"@Qty", txtQty.Text},
                        {"@UnitId", Function.getComboboxId(cbbUnit)},
                        {"@Price", Function.RemoveComma(txtPrice.Text)},
                        {"@PriceSpacial", Function.RemoveComma(txtPriceSpacial.Text)},
                        {"@Discount", Function.RemoveComma(txtDiscount.Text)},
                        {"@PriceExVat", Function.RemoveComma(txtPriceExVat.Text)},
                        {"@Vat", Function.RemoveComma(txtVat.Text)},
                        {"@VatValue", Function.RemoveComma(txtVatValue.Text)},
                        {"@MoneyId", Function.getComboboxId(cbbPayment)},
                        {"@ShopId", Function.getComboboxId(cbbShop)},
                        {"@Branch", txtBranch.Text},
                        {"@Location", txtLocation.Text},
                        {"@BuyDate", txtBuyDate.Text == ""? "" :  Date.GetDate(dtp: BuyDate, Format : 4)},
                        {"@ReceiveDate", txtReceiveDate.Text == ""? "" :  Date.GetDate(dtp: ReceiveDate, Format : 4)},
                        {"@Receipt", txtReceipt.Text},
                        {"@WarrantyDate", txtWarrantyDate.Text == ""? "" :  Date.GetDate(dtp: WarrantyDate, Format : 4)},
                        {"@IsSale", cb_Sale.Checked? "1" : "0"},
                        {"@PriceSale", Function.RemoveComma(txtPriceSale.Text)},
                        {"@PriceRetail", Function.RemoveComma(txtPriceRetail.Text)},
                        {"@PriceWholeSale", Function.RemoveComma(txtPriceWholeSale.Text)},
                        {"@PriceMember", Function.RemoveComma(txtPriceMember.Text)},
                        {"@ManufacturingDate", txtMfg.Text == ""? "" :  Date.GetDate(dtp: Mfg, Format : 4)},
                        {"@ExpiryDate", txtExp.Text == ""? "" :  Date.GetDate(dtp: Exp, Format : 4)},
                        {"@Remark", txtRemark.Text},
                        {"@Status", Function.getComboboxId(cbbStatus)},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@OrderNo", txtOrder.Text},
                        {"@Reference", txtReference.Text},
                        {"@Operation", Operation.UpdateAbbr},
                    };

                    if (Edit.Update(AppCode, AppName, UserId, Store.ManageStock, Parameter, txtCode.Text, Details: GetDetails()))
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

        private void AddData(object sender, EventArgs e)
        {
            try
            {
                if (Function.GetComboId(cbbProducts) != "0" && !string.IsNullOrEmpty(txtQty.Text) && Function.GetComboId(cbbPayment) != "0" && Function.GetComboId(cbbStatus) != "0" && !string.IsNullOrEmpty(txtBuyDate.Text))
                {
                    if (!Function.IsDuplicates(Table.Stocks, Function.GetComboId(cbbShop), txtSerialNumber.Text, Date.GetDate(dtp: BuyDate), Detail: GetDetails()))
                    {
                        string Codes = Function.GetCodes(Table.ProductId, "", "Generated");
                        txtCode.Text = Codes;

                        Parameter = new string[,]
                        {
                            {"@Id ", ""},
                            {"@Code", txtCode.Text},
                            {"@Barcode", txtBarcode.Text},
                            {"@User", ""},
                            {"@ProductId", Function.getComboboxId(cbbProducts)},
                            {"@Lot", txtLot.Text},
                            {"@SerialNumber", txtSerialNumber.Text},
                            {"@Qty", txtQty.Text},
                            {"@UnitId", Function.getComboboxId(cbbUnit)},
                            {"@Price", Function.RemoveComma(txtPrice.Text)},
                            {"@PriceSpacial", Function.RemoveComma(txtPriceSpacial.Text)},
                            {"@Discount", Function.RemoveComma(txtDiscount.Text)},
                            {"@PriceExVat", Function.RemoveComma(txtPriceExVat.Text)},
                            {"@Vat", Function.RemoveComma(txtVat.Text)},
                            {"@VatValue", Function.RemoveComma(txtVatValue.Text)},
                            {"@MoneyId", Function.getComboboxId(cbbPayment)},
                            {"@ShopId", Function.getComboboxId(cbbShop)},
                            {"@Branch", txtBranch.Text},
                            {"@Location", txtLocation.Text},
                            {"@BuyDate", txtBuyDate.Text == ""? "" :  Date.GetDate(dtp: BuyDate, Format : 4)},
                            {"@ReceiveDate", txtReceiveDate.Text == ""? "" :  Date.GetDate(dtp: ReceiveDate, Format : 4)},
                            {"@Receipt", txtReceipt.Text},
                            {"@WarrantyDate", txtWarrantyDate.Text == ""? "" :  Date.GetDate(dtp: WarrantyDate, Format : 4)},
                            {"@IsSale", cb_Sale.Checked? "1" : "0"},
                            {"@PriceSale", Function.RemoveComma(txtPriceSale.Text)},
                            {"@PriceRetail", Function.RemoveComma(txtPriceRetail.Text)},
                            {"@PriceWholeSale", Function.RemoveComma(txtPriceWholeSale.Text)},
                            {"@PriceMember", Function.RemoveComma(txtPriceMember.Text)},
                            {"@ManufacturingDate", txtMfg.Text == ""? "" :  Date.GetDate(dtp: Mfg, Format : 4)},
                            {"@ExpiryDate", txtExp.Text == ""? "" :  Date.GetDate(dtp: Exp, Format : 4)},
                            {"@Remark", txtRemark.Text},
                            {"@Status", Function.getComboboxId(cbbStatus)},
                            {"@IsActive", "1"},
                            {"@IsDelete", "0"},
                            {"@OrderNo", txtOrder.Text},
                            {"@Reference", txtReference.Text},
                            {"@Operation", Operation.InsertAbbr},
                        };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageStock, Parameter, txtCode.Text, Details: GetDetails()))
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
                DataGridViewRow RowIndex = GridView.Rows[e.RowIndex];

                Parameter = new string[,]
                {
                    {"@Id", RowIndex.Cells["Id"].Value.ToString()},
                    {"@Code", ""},
                    {"@barcode", ""},
                    {"@User", ""},
                    {"@ProductId", "0"},
                    {"@Lot", ""},
                    {"@SerialNumber", ""},
                    {"@Qty", ""},
                    {"@UnitId", "0"},
                    {"@Price", "0.00"},
                    {"@PriceSpacial", "0.00"},
                    {"@Discount", "0.00"},
                    {"@PriceExVat", "0.00"},
                    {"@Vat", "0.00"},
                    {"@VatValue", "0.00"},
                    {"@MoneyId", "0"},
                    {"@ShopId", "0"},
                    {"@Branch", ""},
                    {"@Location", ""},
                    {"@BuyDate", ""},
                    {"@ReceiveDate", ""},
                    {"@Receipt", ""},
                    {"@WarrantyDate", ""},
                    {"@IsSale", ""},
                    {"@PriceSale", "0.00"},
                    {"@PriceRetail", "0.00"},
                    {"@PriceWholeSale", "0.00"},
                    {"@PriceMember", "0.00"},
                    {"@ManufacturingDate", ""},
                    {"@ExpiryDate", ""},
                    {"@Remark", ""},
                    {"@Status", "0"},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@OrderNo", ""},
                    {"@Reference", ""},
                    {"@Operation", Operation.SelectAbbr},
                };

                db.Get(Store.ManageStock, Parameter, out Error, out dt);
                ShowData(dt);
            }
        }

        public string GetDetails()
        {
            return String.Concat(cbbProducts.Text, " ", txtQty.Text, " x ", cbbUnit.Text);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (txtBarcode.Text != "")
            {
                try
                {
                    Clipboard.SetDataObject(txtBarcode.Text, true, 10, 100);
                }
                catch (Exception)
                {

                }
            }
        }

        private void cbbProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Function.getComboboxId(cbbProducts) != "0")
            {
                Parameter = new string[,]
                 {
                    {"@Id", Function.getComboboxId(cbbProducts)},
                    {"@Code", ""},
                    {"@User", ""},
                    {"@Barcode", ""},
                    {"@Name", ""},
                    {"@NameEn", ""},
                    {"@TypeId", "0"},
                    {"@Brand", ""},
                    {"@Generation", ""},
                    {"@Model", ""},
                    {"@Color", ""},
                    {"@Size", ""},
                    {"@SerialNumber", ""},
                    {"@Detail", ""},
                    {"@Price", "0.00"},
                    {"@PriceSpacial", "0.00"},
                    {"@Discount", "0.00"},
                    {"@PriceExVat", "0.00"},
                    {"@Vat", "0.00"},
                    {"@VatValue", "0.00"},
                    {"@Image",""},
                    {"@MadeIn", ""},
                    {"@Status", "0"},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.SelectAbbr},
                };

                db.Get(Store.ManageProduct, Parameter, out Error, out dt);

                txtBarcode.Text = dt.Rows[0]["Barcode"].ToString();
                txtNameEn.Text = dt.Rows[0]["NameEn"].ToString();
                txtBrand.Text = dt.Rows[0]["Brand"].ToString();

                if (!string.IsNullOrEmpty(txtBarcode.Text))
                {
                    pbQrcode.Image = Barcode.EAN13(txtBarcode.Text, Color.Black, Color.White, 60);
                }
                else
                {
                    pbQrcode.Image = null;
                }

                pbImage.ImageLocation = dt.Rows[0]["Locations"].ToString();
                GridView.Focus();
            }
        }

        private void SearchBarcode()
        {
            Parameter = new string[,]
             {
                    {"@Id", ""},
                    {"@Code", ""},
                    {"@User", ""},
                    {"@Barcode", txtBarcode.Text},
                    {"@Name", ""},
                    {"@NameEn", ""},
                    {"@TypeId", "0"},
                    {"@Brand", ""},
                    {"@Generation", ""},
                    {"@Model", ""},
                    {"@Color", ""},
                    {"@Size", ""},
                    {"@SerialNumber", ""},
                    {"@Detail", ""},
                    {"@Price", "0.00"},
                    {"@PriceSpacial", "0.00"},
                    {"@Discount", "0.00"},
                    {"@PriceExVat", "0.00"},
                    {"@Vat", "0.00"},
                    {"@VatValue", "0.00"},
                    {"@Image",""},
                    {"@MadeIn", ""},
                    {"@Status", "0"},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.SelectAbbr},
            };

            db.Get(Store.ManageProduct, Parameter, out Error, out dt);

            if (string.IsNullOrEmpty(Error))
            {
                cbbProducts.SelectedValue = dt.Rows[0]["Id"].ToString();
                txtBarcode.Text = dt.Rows[0]["Barcode"].ToString();
                txtNameEn.Text = dt.Rows[0]["NameEn"].ToString();
                txtBrand.Text = dt.Rows[0]["Brand"].ToString();

                if (!string.IsNullOrEmpty(txtBarcode.Text))
                {
                    pbQrcode.Image = Barcode.EAN13(txtBarcode.Text, Color.Black, Color.White, 60);
                }
                else
                {
                    pbQrcode.Image = null;
                }

                pbImage.ImageLocation = dt.Rows[0]["Locations"].ToString();
            }

            Search(true);
            GridView.Focus();
        }

        private void btnTitleCase_Click(object sender, EventArgs e)
        {
            Converts.TitleCase(txtBranch);
        }
    }
}