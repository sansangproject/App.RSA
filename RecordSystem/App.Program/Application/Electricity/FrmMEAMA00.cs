using System;
using System.Data;
using SANSANG.Class;
using System.Drawing;
using SANSANG.Database;
using SANSANG.Constant;
using System.Windows.Forms;

namespace SANSANG
{
    public partial class FrmMEAMA00 : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string strAppCode = "MEAMA00";
        public string strAppName = "FrmMEAMA00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        private clsInsert Insert = new clsInsert();
        private clsDelete Delete = new clsDelete();
        private clsEdit Edit = new clsEdit();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();
        private DataTable dt = new DataTable();
        private TableConstant Tb = new TableConstant();
        private clsBarcode Barcode = new clsBarcode();
        private StoreConstant Store = new StoreConstant();

        public string[,] Parameter = new string[,] { };

        public FrmMEAMA00(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
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
                List.GetList(cbbUser, "Y", "ElectricityUser");
                List.GetList(cbbStatus, "6", "Status");
                Clear();
                gbMain.Enabled = true;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
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
                        {"@Id", row.Cells["Id"].Value.ToString()},
                        {"@Code", ""},
                        {"@Version", ""},
                        {"@Rates", ""},
                        {"@Service", ""},
                        {"@Ft", ""},
                        {"@Discount", ""},
                        {"@Vat", ""},
                        {"@DueDate", ""},
                        {"@CompanyName", ""},
                        {"@CompanyPhone", ""},
                        {"@Report", ""},
                        {"@Status", "0"},
                        {"@User", ""},
                    };

                    db.Get("Store.SelectVersionElectricity", Parameter, out strErr, out dt);

                    txtCode.Text = dt.Rows[0]["Code"].ToString();

                    txtVersion.Text = dt.Rows[0]["Version"].ToString();
                    txtReport.Text = dt.Rows[0]["Report"].ToString();
                    txtDay.Text = dt.Rows[0]["DueDate"].ToString();
                    txtVat.Text = dt.Rows[0]["Vat"].ToString();
                    cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();

                    txtPrice.Text = dt.Rows[0]["Rates"].ToString();
                    txtCompany.Text = dt.Rows[0]["CompanyName"].ToString();
                    txtPhone.Text = dt.Rows[0]["CompanyPhone"].ToString();
                    txtService.Text = dt.Rows[0]["Service"].ToString();

                    txtFt.Text = dt.Rows[0]["Ft"].ToString();
                    txtDiscount.Text = dt.Rows[0]["Discount"].ToString();

                    string strBarcode = dt.Rows[0]["Code"].ToString();
                    pbQrcode.Image = Barcode.QRCode(strBarcode, Color.Black, Color.White, "Q", 3, false);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        public void getDataGrid(DataTable dt)
        {
            try
            {
                if (Fn.GetRows(dt) == 0)
                {
                    dataGridView.DataSource = null;
                    picExcel.Visible = false;
                }
                else
                {
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "Version", "Rate", "Service", "Ft", "CompanyName", "Id");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                    Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                          "ลำดับ", 20, true, mc, mc
                        , "Version", 80, true, ml, ml
                        , "ค่าไฟฟ้าต่อหน่วย", 100, true, ml, ml
                        , "ค่าบริการรายเดือน", 100, true, ml, ml
                        , "Ft", 80, true, ml, ml
                        , "บริษัทจดบันทึกค่าไฟฟ้า", 200, true, mc, mc
                        , "", 0, false, mc, mc
                        );

                    picExcel.Visible = false;
                    lblResult.Text = Fn.ShowResult(dt.Rows.Count);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtVersion.Text))
            {
                if (!Fn.IsDuplicates("MST_VersionElectricity", txtVersion.Text, Detail: "Version " + txtVersion.Text))
                {
                    txtCode.Text = Fn.GetCodes("139", "", "Generated");

                    string[,] Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", txtCode.Text},
                        {"@Version", txtVersion.Text},
                        {"@Rates", txtPrice.Text},
                        {"@Service", txtService.Text},
                        {"@Ft", txtFt.Text},
                        {"@Discount", txtDiscount.Text},
                        {"@Vat", txtVat.Text},
                        {"@DueDate", txtDay.Text},
                        {"@CompanyName", txtCompany.Text},
                        {"@CompanyPhone", txtPhone.Text},
                        {"@Report", txtReport.Text},
                        {"@Status", Fn.getComboboxId(cbbStatus)},
                        {"@User", strUserId},
                    };

                    if (Insert.Add(strAppCode, strAppName, strUserId, "Store.InsertVersionElectricity", Parameter, txtCode.Text, "Version " + txtVersion.Text))
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                if (Delete.Drop(strAppCode, strAppName, strUserId, 0, Tb.VersionElectricity, txtCode, "Version " + txtVersion.Text))
                {
                    Clear();
                }
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
                    {"@Version", txtVersion.Text},
                    {"@Rates", txtPrice.Text},
                    {"@Service", txtService.Text},
                    {"@Ft", txtFt.Text},
                    {"@Discount", txtDiscount.Text},
                    {"@Vat", txtVat.Text},
                    {"@DueDate", txtDay.Text},
                    {"@CompanyName", txtCompany.Text},
                    {"@CompanyPhone", txtPhone.Text},
                    {"@Report", txtReport.Text},
                    {"@Status", Fn.getComboboxId(cbbStatus)},
                    {"@User", strUserId},
                };

                if (Edit.Update(strAppCode, strAppName, strUserId, "Store.UpdateVersionElectricity", Parameter, txtCode.Text, "Version " + txtVersion.Text))
                {
                    Clear();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";

                strCondition += txtCode.Text != "" ? ", รหัสอ้างอิง: " + txtCode.Text : "";
                strCondition += txtVersion.Text != "" ? ", " + label17.Text + " " + txtVersion.Text : "";
                strCondition += txtReport.Text != "" ? ", เลขรายงาน: " + txtReport.Text : "";
                strCondition += txtPrice.Text != "" ? ", " + label10.Text + " " + txtPrice.Text : "";
                strCondition += txtVat.Text != "" ? ", " + label5.Text + " " + txtVat.Text : "";
                strCondition += txtService.Text != "" ? ", " + label12.Text + " " + txtService.Text : "";
                strCondition += txtFt.Text != "" ? ", " + label13.Text + " " + txtFt.Text : "";
                strCondition += txtDiscount.Text != "" ? ", " + label14.Text + " " + txtDiscount.Text : "";
                strCondition += txtDay.Text != "" ? ", " + label16.Text + " " + txtPhone.Text : "";
                strCondition += txtCompany.Text != "" ? ", " + label18.Text + " " + txtCompany.Text : "";
                strCondition += txtPhone.Text != "" ? ", " + label26.Text + " " + txtPhone.Text : "";
                strCondition += cbbStatus.Text != ":: กรุณาเลือก ::" ? ", " + label19.Text + " " + cbbStatus.Text : "";

                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
                return "";
            }
        }

        public void SearchData()
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", txtCode.Text},
                    {"@Version", txtVersion.Text},
                    {"@Rates", txtPrice.Text},
                    {"@Service", txtService.Text},
                    {"@Ft", txtFt.Text},
                    {"@Discount", txtDiscount.Text},
                    {"@Vat", txtVat.Text},
                    {"@DueDate", txtDay.Text},
                    {"@CompanyName", txtCompany.Text},
                    {"@CompanyPhone", txtPhone.Text},
                    {"@Report", txtReport.Text},
                    {"@Status", Fn.getComboboxId(cbbStatus)},
                    {"@User", strUserId},
                };

                db.Get("Store.SelectVersionElectricity", Parameter, out strErr, out dt);
                getDataGrid(dt);

                lblCondition.Text = Fn.ShowConditons(GetCondition());
            }
            catch (Exception ex)
            {
                lblCondition.Text = "";
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        public void Clear()
        {
            try
            {
                cbbStatus.SelectedValue = 0;
                txtDay.Text = "";
                txtVersion.Text = "";
                txtReport.Text = "";
                txtCompany.Text = "";
                txtPhone.Text = "";
                txtPrice.Text = "";
                txtService.Text = "";
                txtFt.Text = "";
                txtDiscount.Text = "";
                txtCode.Text = "";
                lblCondition.Text = "";
                txtVat.Text = "";
                pbQrcode.Image = null;

                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", ""},
                    {"@Version", ""},
                    {"@Rates", ""},
                    {"@Service", ""},
                    {"@Ft", ""},
                    {"@Discount", ""},
                    {"@Vat", ""},
                    {"@DueDate", ""},
                    {"@CompanyName", ""},
                    {"@CompanyPhone", ""},
                    {"@Report", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                };

                db.Get("Store.SelectVersionElectricity", Parameter, out strErr, out dt);
                getDataGrid(dt);
                dataGridView.Focus();
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void FrmKeyDown(object sender, KeyEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }
    }
}