using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmWaterVersion : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string strAppCode = "MWAMA00";
        public string strAppName = "FrmMWAMA00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        private DataTable dt = new DataTable();
        private StoreConstant Store = new StoreConstant();
        private clsInsert Insert = new clsInsert();
        private clsDelete Delete = new clsDelete();
        private clsEdit Edit = new clsEdit();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
        private clsDataList List = new clsDataList();
        private dbConnection db = new dbConnection();
        private clsDesign Design = new clsDesign();
        private clsFormat Format = new clsFormat();
        private clsLog Log = new clsLog();
        private clsBarcode Barcode = new clsBarcode();
        private Timer timer = new Timer();
        private TableConstant Tb = new TableConstant();
        public string[,] Parameter = new string[,] { };

        public FrmWaterVersion(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmMWAMA00_Load(object sender, EventArgs e)
        {
            int sec = 2;
            timer.Interval = (sec * 1000);
            timer.Tick += new EventHandler(LoadList);
            timer.Start();
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetList(cbbStatus, "6", "Status");
            Clear();
            gbMain.Enabled = true;
            timer.Stop();
        }

        public void getDataGrid(DataTable dt)
        {
            try
            {
                if (Fn.GetRows(dt) == 0)
                {
                    dataGridView.DataSource = null;
                    picExcel.Visible = false;
                    txtCount.Text = Fn.ShowNumberOfData(0);
                }
                else
                {
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "Code", "Version", "Rates", "Service", "StatusName", "Id");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                    Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                          "ลำดับ", 30, true, mc, mc
                        , "รหัสอ้างอิง", 100, true, ml, ml
                        , "เวอร์ชั่น", 100, true, ml, ml
                        , "อัตราค่าน้ำ (บาท/หน่วย)", 100, true, mc, mc
                        , "ค่าบริการรายเดือน", 100, true, mc, mc
                        , "สถานะ", 100, true, mc, mc
                        , "", 0, false, mc, mc
                        );

                    picExcel.Visible = false;
                    txtCount.Text = Fn.ShowNumberOfData(dt.Rows.Count);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id ", ""},
                    {"@Code", txtCode.Text},
                    {"@Status", Fn.getComboboxId(cbbStatus)},
                    {"@Version", txtVersion.Text},
                    {"@Rates", txtRate.Text},
                    {"@Service", txtService.Text},
                    {"@Discount", txtDiscount.Text },
                    {"@Vat", txtVat.Text},
                    {"@RawText", txtRawText.Text},
                    {"@RawValue", txtRawValue.Text},
                    {"@DueDate", txtDueDate.Text},
                    {"@User", strUserId},
                };

                db.Get("Store.SelectVersionWater", Parameter, out strErr, out dt);
                getDataGrid(dt);

                lblCondition.Text = Fn.ShowConditons(GetCondition());
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";
                strCondition += txtCode.Text != "" ? ", รหัส : " + txtCode.Text : "";
                strCondition += txtVersion.Text != "" ? ", " + label33.Text + " " + txtVersion.Text : "";
                strCondition += txtRate.Text != "" ? ", " + label19.Text + " " + txtRate.Text : "";
                strCondition += txtRawValue.Text != "" ? ", " + label27.Text + " " + txtRawValue.Text : "";
                strCondition += txtRawText.Text != "" ? ", เลขน้ำประปา : " + txtRawText.Text : "";
                strCondition += txtDiscount.Text != "" ? ", " + label31.Text + " " + txtDiscount.Text : "";
                strCondition += txtVat.Text != "" ? ", " + label35.Text + " " + txtVat.Text : "";
                strCondition += txtService.Text != "" ? ", " + label17.Text + " " + txtService.Text : "";
                strCondition += txtDueDate.Text != "" ? ", " + label39.Text + " " + txtDueDate.Text : "";
                strCondition += cbbStatus.Text != ":: กรุณาเลือก ::" ? ", " + label41.Text + " " + cbbStatus.Text : "";

                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
                return "";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtVersion.Text))
            {
                if (!Fn.IsDuplicates("MST_VersionWater", txtVersion.Text, Detail: "Version " + txtVersion.Text))
                {
                    txtCode.Text = Fn.GetCodes("10001", "", "Generated");

                    string[,] Parameter = new string[,]
                    {
                        {"@Id ", ""},
                        {"@Code", txtCode.Text},
                        {"@Status", Fn.getComboboxId(cbbStatus)},
                        {"@Version", txtVersion.Text},
                        {"@Rates", txtRate.Text},
                        {"@Service", txtService.Text},
                        {"@Discount", txtDiscount.Text },
                        {"@Vat", txtVat.Text},
                        {"@RawText", txtRawText.Text},
                        {"@RawValue", txtRawValue.Text},
                        {"@DueDate", txtDueDate.Text},
                        {"@User", strUserId},
                    };

                    if (Insert.Add(strAppCode, strAppName, strUserId, "Store.InsertVersionWater", Parameter, txtCode.Text, "Version " + txtVersion.Text))
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
                        {"@Id ", row.Cells["Id"].Value.ToString()},
                        {"@Code", ""},
                        {"@Status", "0"},
                        {"@Version", ""},
                        {"@Rates", "0.00"},
                        {"@Service", "0.00"},
                        {"@Discount", ""},
                        {"@Vat", ""},
                        {"@RawText", ""},
                        {"@RawValue", "0.00"},
                        {"@DueDate", ""},
                        {"@User", strUserId},
                    };

                    db.Get("Store.SelectVersionWater", Parameter, out strErr, out dt);

                    txtId.Text = dt.Rows[0]["Id"].ToString();
                    txtCode.Text = dt.Rows[0]["Code"].ToString();

                    txtVersion.Text = dt.Rows[0]["Version"].ToString();
                    txtRate.Text = dt.Rows[0]["Rates"].ToString();
                    txtRawValue.Text = dt.Rows[0]["RawValue"].ToString();
                    txtRawText.Text = dt.Rows[0]["RawText"].ToString();

                    txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
                    cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();

                    txtVat.Text = dt.Rows[0]["Vat"].ToString();
                    txtService.Text = dt.Rows[0]["Service"].ToString();
                    txtDueDate.Text = dt.Rows[0]["DueDate"].ToString();

                    string strBarcode = dt.Rows[0]["Code"].ToString();
                    pbQrcode.Image = Barcode.QRCode(strBarcode, Color.Black, Color.White, "Q", 3, false);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                Parameter = new string[,]
                {
                    {"@Id ", ""},
                    {"@Code", txtCode.Text},
                    {"@Status", Fn.getComboboxId(cbbStatus)},
                    {"@Version", txtVersion.Text},
                    {"@Rates", txtRate.Text},
                    {"@Service", txtService.Text},
                    {"@Discount", txtDiscount.Text },
                    {"@Vat", txtVat.Text},
                    {"@RawText", txtRawText.Text},
                    {"@RawValue", txtRawValue.Text},
                    {"@DueDate", txtDueDate.Text},
                    {"@User", strUserId},
                };

                if (Edit.Update(strAppCode, strAppName, strUserId, "Store.UpdateVersionWater", Parameter, txtCode.Text, "Version " + txtVersion.Text))
                {
                    Clear();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Delete.Drop(strAppCode, strAppName, strUserId, 0, Tb.VersionWater, txtCode, "Version " + txtVersion.Text))
            {
                Clear();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void FrmMWAMA00_KeyDown(object sender, KeyEventArgs e)
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

            if (keyCode == "Ctrl+X")
            {
                btnExit_Click(sender, e);
            }

            if (keyCode == "Ctrl+F")
            {
                btnSearch_Click(sender, e);
            }

            if (keyCode == "Alt+C")
            {
                btnClear_Click(sender, e);
            }
        }

        private void txtWaterId_KeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnSearch);
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnSearch);
        }

        public void Clear()
        {
            try
            {
                txtId.Text = "";
                txtCode.Text = "";

                txtVersion.Text = "";
                txtRate.Text = "";
                txtRawValue.Text = "";
                txtRawText.Text = "";

                txtDiscount.Text = "";
                cbbStatus.SelectedValue = 0;

                txtVat.Text = "";
                txtService.Text = "";
                txtDueDate.Text = "";

                lblCondition.Text = "";
                pbQrcode.Image = null;

                Parameter = new string[,]
                {
                    {"@Id ", ""},
                    {"@Code", ""},
                    {"@Status", "0"},
                    {"@Version", ""},
                    {"@Rates", "0.00"},
                    {"@Service", "0.00"},
                    {"@Discount", ""},
                    {"@Vat", ""},
                    {"@RawText", ""},
                    {"@RawValue", "0.00"},
                    {"@DueDate", ""},
                    {"@User", strUserId},
                };

                db.Get("Store.SelectVersionWater", Parameter, out strErr, out dt);
                getDataGrid(dt);
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }
    }
}