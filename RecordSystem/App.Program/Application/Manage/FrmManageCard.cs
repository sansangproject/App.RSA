using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmManageCard : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string strAppCode = "MANCRE00";
        public string strAppName = "FrmManageCard";
        public string filePath = "SPA591012027-4";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string fileName = "";
        public string fileTypeA = "-";
        public string fileTypeB = "-";

        private DataTable dt = new DataTable();
        public string[,] Parameter = new string[,] { };
        private CultureInfo En = new CultureInfo("en-US");

        private clsMessage Message = new clsMessage();
        private clsInsert Insert = new clsInsert();
        private dbConnection db = new dbConnection();
        private clsFunction Fn = new clsFunction();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();
        private clsHelpper Helpe = new clsHelpper();
        private clsImage Images = new clsImage();
        public FrmManageCard(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmManageCard_Load(object sender, EventArgs e)
        {
            List.GetList(cbbStatus, "0", "Status");
            List.GetList(cbbShop, "Y", "Shop");
            List.GetList(cbbBank, "Y", "Bank");
            Clear();
        }

        public void Clear()
        {
            txtCode.Enabled = true;
            txtCode.Text = "";
            txtCode.Focus();
            txtName.Text = "";
            txtNumber.Text = "";
            txtFileLocation.Text = "";
            txtFileLocation2.Text = "";
            txtOwner.Text = "";
            txtType.Text = "";
            txtDetail.Text = "";
            dtStart.Text = "";
            dtEnd.Text = "";
            txtCardNameEn.Text = "";
            txtCardNameTh.Text = "";
            txtContact.Text = "";
            lblSearch.Text = "";
            fileTypeA = "-";
            fileTypeB = "-";
            fileName = "";
            pb_Credit_False.Show();
            cb_Credit.Checked = false;

            clsFunction TSSFN = new clsFunction();
            Images.ShowDefault(picFile);

            cbbStatus.SelectedValue = 0;
            cbbShop.SelectedValue = 0;
            cbbBank.SelectedValue = 0;

            Parameter = new string[,]
                {
                    {"@MsCardId", ""},
                    {"@MsCardCode", ""},
                    {"@MsCardNumber", ""},
                    {"@MsCardDisplay", ""},
                    {"@MsCardNameTh", ""},
                    {"@MsCardNameEn", ""},
                    {"@MsCardName", ""},
                    {"@MsCardOwner", ""},
                    {"@MsCardBank", "0"},
                    {"@MsCardProvider", "0"},
                    {"@MsCardDetail", ""},
                    {"@MsCardContact", ""},
                    {"@MsCardType", ""},
                    {"@MsCardDateStart", ""},
                    {"@MsCardDateEnd", ""},
                    {"@MsCardStatus", "0"},
                };

             
            db.Get("Spr_S_TblMasterCard", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        public void getDataGrid(DataTable dt)
        {
            clsFunction TSSFN = new clsFunction();

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
                Fn.MesNoData();
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                txtCode.Focus();
                lblCount.Text = "0";
            }
            else
            {
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "MsCardNumber", "MsCardDisplay", "CardOwerName", "MsCardDateEnd", "MsStatusNameTh", "MsCardId");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                      "ลำดับ", 30, true, mc, mc
                    , "หมายเลขบัตร", 70, true, mc, ml
                    , "ชื่อบัตร", 150, true, ml, ml
                    , "เจ้าของบัตร", 100, true, ml, ml
                    , "วันหมดอายุบัตร", 50, true, ml, ml
                    , "สถานะ", 80, true, ml, ml
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            Parameter = new string[,]
                {
                    {"@MsCardId", ""},
                    {"@MsCardCode", txtCode.Text},
                    {"@MsCardNumber", Fn.GetCardNumber(txtNumber.Text)},
                    {"@MsCardDisplay", ""},
                    {"@MsCardNameTh", txtCardNameTh.Text},
                    {"@MsCardNameEn", txtCardNameEn.Text},
                    {"@MsCardName", txtName.Text},
                    {"@MsCardOwner", txtOwner.Text},
                    {"@MsCardBank", Fn.getComboBoxValue(cbbBank)},
                    {"@MsCardProvider", Fn.getComboBoxValue(cbbShop)},
                    {"@MsCardDetail", txtDetail.Text},
                    {"@MsCardContact", txtContact.Text},
                    {"@MsCardType", txtType.Text},
                    {"@MsCardDateStart", dtStart.Text},
                    {"@MsCardDateEnd", dtEnd.Text},
                    {"@MsCardStatus", Fn.getComboBoxValue(cbbStatus)},
                };

             
            db.Get("Spr_S_TblMasterCard", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                strOpe = "I";

                txtCode.Text = Fn.GetCodes("73", "", "Generated");
                string[,] Parameter = new string[,]
            {
                {"@MsCardCode", txtCode.Text},
                {"@MsCardNumber", txtNumber.Text},
                {"@MsCardDisplay", txtCardNameEn.Text},
                {"@MsCardNameTh", txtCardNameTh.Text},
                {"@MsCardNameEn", txtCardNameEn.Text},
                {"@MsCardName", txtName.Text},
                {"@MsCardBank", Fn.getComboBoxValue(cbbBank)},
                {"@MsCardProvider", cb_Credit.Checked ? Fn.getComboBoxValue(cbbBank) : Fn.getComboBoxValue(cbbShop)},
                {"@MsCardOwner", Fn.getComboBoxValue(cbbShop)},
                {"@MsCardDetail", txtDetail.Text},
                {"@MsCardContact", txtContact.Text},
                {"@MsCardType", txtType.Text},
                {"@MsCardDateStart", dtStart.Text},
                {"@MsCardDateEnd", dtEnd.Text},
                {"@MsCardIsCreditCard", cb_Credit.Checked ? "1" : "0"},
                {"@MsCardFileA", txtFileLocation.Text},
                {"@MsCardFileB", txtFileLocation2.Text},
                {"@MsCardStatus", Fn.getComboBoxValue(cbbStatus)},
                {"@User", strUserId},
            };

                Message.MessageConfirmation(strOpe, txtNumber.Text, txtCardNameEn.Text);

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
                        db.Operations("Spr_I_TblMasterCard", Parameter, out strErr);

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
                    {"@MsCardId", row.Cells["MsCardId"].Value.ToString()},
                    {"@MsCardCode", ""},
                    {"@MsCardNumber", ""},
                    {"@MsCardDisplay", ""},
                    {"@MsCardNameTh", ""},
                    {"@MsCardNameEn", ""},
                    {"@MsCardName", ""},
                    {"@MsCardOwner", ""},
                    {"@MsCardBank", "0"},
                    {"@MsCardProvider", "0"},
                    {"@MsCardDetail", ""},
                    {"@MsCardContact", ""},
                    {"@MsCardType", ""},
                    {"@MsCardDateStart", ""},
                    {"@MsCardDateEnd", ""},
                    {"@MsCardStatus", "0"},
                };

                 
                db.Get("Spr_S_TblMasterCard", Parameter, out strErr, out dt);

                txtCode.Text = dt.Rows[0]["MsCardCode"].ToString();
                txtName.Text = dt.Rows[0]["MsCardName"].ToString();
                txtCardNameTh.Text = dt.Rows[0]["MsCardNameTh"].ToString();
                txtCardNameEn.Text = dt.Rows[0]["MsCardNameEn"].ToString();
                txtNumber.Text = dt.Rows[0]["MsCardNumber"].ToString();
                txtOwner.Text = dt.Rows[0]["MsCardProvider"].ToString();
                txtType.Text = dt.Rows[0]["MsCardType"].ToString();
                txtDetail.Text = dt.Rows[0]["MsCardDetail"].ToString();
                dtStart.Text = dt.Rows[0]["MsCardDateStart"].ToString();
                dtEnd.Text = dt.Rows[0]["MsCardDateEnd"].ToString();
                txtContact.Text = dt.Rows[0]["MsCardContact"].ToString();
                cbbStatus.SelectedValue = dt.Rows[0]["MsCardStatus"].ToString();
                cbbShop.SelectedValue = dt.Rows[0]["MsCardOwner"].ToString();
                cbbBank.SelectedValue = dt.Rows[0]["MsCardBank"].ToString();
                txtFileLocation.Text = dt.Rows[0]["MsCardFileA"].ToString();
                txtFileLocation2.Text = dt.Rows[0]["MsCardFileB"].ToString();
               
                if (dt.Rows[0]["MsCardIsCreditCard"].ToString() == "True")
                {
                    pb_Credit_True.Show();
                    pb_Credit_False.Hide();
                    cb_Credit.Checked = true;
                }
                else
                {
                    pb_Credit_True.Hide();
                    pb_Credit_False.Show();
                    cb_Credit.Checked = false;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                strOpe = "D";

                string[,] Parameter = new string[,]
                   {
                        {"@MsCardCode", txtCode.Text},
                        {"@DeleteType", "1"},
                        {"@User", strUserId},
                   };

                Message.MessageConfirmation(strOpe, txtNumber.Text, txtCardNameEn.Text);

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
                        db.Operations("Spr_D_TblMasterCard", Parameter, out strErr);
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
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                strOpe = "U";

                string[,] Parameter = new string[,]
                {
                    {"@MsCardCode", txtCode.Text},
                    {"@MsCardNumber", txtNumber.Text},
                    {"@MsCardDisplay", txtCardNameEn.Text},
                    {"@MsCardNameTh", txtCardNameTh.Text},
                    {"@MsCardNameEn", txtCardNameEn.Text},
                    {"@MsCardName", txtName.Text},
                    {"@MsCardBank", Fn.getComboBoxValue(cbbBank)},
                    {"@MsCardProvider", cb_Credit.Checked ? Fn.getComboBoxValue(cbbBank) : Fn.getComboBoxValue(cbbShop)},
                    {"@MsCardOwner", Fn.getComboBoxValue(cbbShop)},
                    {"@MsCardDetail", txtDetail.Text},
                    {"@MsCardContact", txtContact.Text},
                    {"@MsCardType", txtType.Text},
                    {"@MsCardDateStart", dtStart.Text},
                    {"@MsCardDateEnd", dtEnd.Text},
                    {"@MsCardIsCreditCard", cb_Credit.Checked ? "1" : "0"},
                    {"@MsCardFileA", txtFileLocation.Text},
                    {"@MsCardFileB", txtFileLocation2.Text},
                    {"@MsCardStatus", Fn.getComboBoxValue(cbbStatus)},
                    {"@User", strUserId},
                };

                Message.MessageConfirmation(strOpe, txtNumber.Text, txtCardNameEn.Text);

                using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                {
                    var result = Mes.ShowDialog();

                    if (result == DialogResult.Yes)
                    {
                        Mes.Close();
                        db.Operations("Spr_U_TblMasterCard", Parameter, out strErr);

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
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void picExcel_Click(object sender, EventArgs e)
        {
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            FrmImportImage Frm = new FrmImportImage(strAppCode, strUserId, strUserName, strUserSurname, strUserType, txtCode.Text, txtFileLocation.Text, "P5-8BE954AD8D-5", "C");
            Frm.ShowDialog();
            txtFileLocation.Text = Frm.strImageCode;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (txtFileLocation.Text != "")
            {
                FrmShowImage FrmShowImage = new FrmShowImage(txtFileLocation.Text);
                FrmShowImage.Show();
            }
        }

        private void FrmManageCard_KeyDown(object sender, KeyEventArgs e)
        {
            clsFunction TSSFN = new clsFunction();

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
            if (keyCode == "Ctrl+F")
            {
                btnSearch_Click(sender, e);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            clsFunction TSSFN = new clsFunction();
             
            clsSearch TSSSearch = new clsSearch();
            clsInsert Insert = new clsInsert();
            clsMessage Message = new clsMessage();

            if (txtCode.Text != "")
            {
                string idTSS = Fn.getTssFileName("CADRE");
                FrmAnimatedProgress frm = new FrmAnimatedProgress(10);
                string strErr = "";
                DataTable dt = new DataTable();
                string ExportToPath = "";
                DialogResult result = folderBrowserDialog.ShowDialog();
                frm.Show();

                try
                {
                    string[,] Parameter = new string[,]
                {
                    {"@MsCardCode", txtCode.Text},
                };

                    db.Get("Spr_R_TblMasterCard", Parameter, out strErr, out dt);

                    string Path = Fn.getPath("Report");
                    string ReportName = "RSA-R-MANCR00001";

                  

                    if (result == DialogResult.OK)
                    {
                        ExportToPath += folderBrowserDialog.SelectedPath + "\\TSSRSA-" + txtCode.Text;
                    }

                    Message.ShowMesInfo("Export File Complete\n\nName : TSSRSA-" + txtCode.Text + ".pdf\nLocation : " + ExportToPath + ".pdf");
                    System.Diagnostics.Process.Start(@"" + ExportToPath + ".pdf");

                    try
                    {
                        Insert.TblLogReport(idTSS, ReportName, "CardId = " + txtCode.Text, "RPTMAI00", "MANCR", "TSSRSA-" + txtCode.Text + ".PDF", ExportToPath + ".PDF", strUserName + " " + strUserSurname);
                    }
                    catch
                    {
                    }
                }
                catch (Exception ex)
                {
                    Message.ShowMesError("Generation Error : ", ex.ToString() + " " + strErr);
                }
            }
        }

        private void txtFileLocation_TextChanged(object sender, EventArgs e)
        {
            try
            {
                clsImage TSSImage = new clsImage();
                TSSImage.ShowImage(picFile, Code: txtFileLocation.Text);
            }
            catch
            {
            }
        }

        private void btnShow2_Click(object sender, EventArgs e)
        {
            FrmImportImage Frm = new FrmImportImage(strAppCode, strUserId, strUserName, strUserSurname, strUserType, txtCode.Text, txtFileLocation2.Text, "P5-8BE954AD8D-5", "C");
            Frm.ShowDialog();
            txtFileLocation2.Text = Frm.strImageCode;
        }

        private void btnBrowse2_Click(object sender, EventArgs e)
        {
            if (txtFileLocation2.Text != "")
            {
                FrmShowImage FrmShowImage = new FrmShowImage(txtFileLocation2.Text);
                FrmShowImage.Show();
            }
        }

        private void dtDatePickerStart_ValueChanged(object sender, EventArgs e)
        {
            dtStart.Clear();
            dtStart.Text = dtDatePickerStart.Value.ToString("M/yy", En);
        }

        private void dtDatePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            dtEnd.Clear();
            dtEnd.Text = dtDatePickerEnd.Value.ToString("M/yy", En);
        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnSearch);
        }

        private void btnCode_Click(object sender, EventArgs e)
        {
            txtCode.Text = Fn.GetCodes("73", "", "Generated");
        }

        private void Ticker(object sender, EventArgs e)
        {
            Helpe.CheckboxTicker(sender, this);
        }

        private void cb_Credit_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_Credit.Checked == true)
            {
                cbbBank.Enabled = true;
                cbbShop.Enabled = false;
                cbbShop.SelectedValue = 0;
            }
            else
            {
                cbbBank.Enabled = false;
                cbbBank.SelectedValue = 0;
                cbbShop.Enabled = true;


            }
        }
    }
}