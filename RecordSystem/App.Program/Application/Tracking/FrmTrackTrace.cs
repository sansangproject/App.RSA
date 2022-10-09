using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SANSANG.Class; 
using SANSANG.Utilites.App.Forms;
using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmTrackTrace : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string strAppName = "FrmTrackTrace";
        public string programCode = "SENTT00";

        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string filePath = "";
        public string fileName = "-";
        public string fileType = ".jpg";
        public string strAddress = "";
        public string ExportToPath = "";
        public string idTSS;

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();
        private ReportDocument rpt = new ReportDocument();
        private clsMessage Message = new clsMessage();
        private clsDate Date = new clsDate();
        private dbConnection db = new dbConnection();
        private clsFunction Fn = new clsFunction();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();
        public string[,] Parameter = new string[,] { };

        public FrmTrackTrace(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            
            List.GetList(cbbDescription, "12", "Status");
            List.GetList(cbbStatus, "0", "Status");
            List.GetList(cbbLocation, "", "PostLocation");
            List.GetList(cbbSendAddress, "Y", "PostAddress");
            List.GetList(cbbReceiveAddress, "Y", "PostAddress");
            txtCodeSearch.Focus();
        }

        public void Clear()
        {
            cbbDescription.SelectedValue = 0;
            cbbStatus.SelectedValue = 0;
            cbbLocation.SelectedValue = 0;
            cbbReceiveAddress.SelectedValue = 0;
            cbbSendAddress.SelectedValue = 0;

            gbEMS.Visible = false;
            gbREG.Visible = false;
            pbLicense.Visible = false;
            txtCode.Text = "";
            dtDate.Text = "";
            txtTime.Text = "";
            txtDay.Text = "";
            txtProduct.Text = "";
            txtDeliveryStatus.Text = "";
            txtRemark.Text = "";
            txtReceiveName.Text = "";
            txtReceiveAddress.Text = "";
            txtSendName.Text = "";
            txtSendAddress.Text = "";
            txtCode.Text = "";
            txtPath.Text = "";
            txtPostCode.Text = "";
            txtWeight.Text = "";
            txtPrice.Text = "";
            txtEntrance.Text = "";
            txtEntranceId.Text = "";
            txtCodeSearch.Focus();

            dataGridView.DataSource = null;
        }

        public DataTable getDataTable(String strCode, String strName, String strZip, String strDistric)
        {
             

            string[,] Parameter = new string[,]
                    {
                        {"@TrackId", ""},
                        {"@TrackCode", strCode},
                        {"@TrackDate", ""},
                        {"@TrackDay", ""},
                        {"@TrackTime", ""},
                        {"@TrackScanLocation", ""},
                        {"@TrackDescription", ""},
                        {"@TrackDeliveryStatus", ""},
                        {"@TrackProduct", ""},
                        //{"@TrackZip", ""},
                        //{"@TrackWeight", "0"},
                        //{"@TrackPrice", "0"},
                        //{"@TrackRegisterIDateTime", ""},
                        //{"@TrackRegisterId", ""},
                        {"@TrackRemark", ""},
                        {"@TrackStatus", ""},
                        {"@TrackSendAddress", "0"},
                        {"@TrackReceiveAddress", "0"},
                    };

            db.Get("Spr_S_TblSaveTracking", Parameter, out strErr, out dt);
            return dt;
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
                txtCode.Text = "";
                txtCodeSearch.Focus();

                gbEMS.Visible = true;
                gbREG.Visible = true;
            }
            else
            {
                txtCode.Text = txtCodeSearch.Text;

                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "StrDate", "StrLocation", "StrStatus", "TrackDeliveryStatus", "StrDataStatus", "TrackId");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                clsFormat TSSFormat = new clsFormat();
                clsFunction TSSFN = new clsFunction();
                Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                      "ลำดับ", 30, true, mc, mc
                    , "วันที่ / เวลา", 100, true, mc, ml
                    , "หน่วยงาน", 170, true, ml, ml
                    , "คำอธิบาย", 80, true, ml, ml
                    , "ผลการนำจ่าย", 100, true, ml, ml
                    , "สถานะการชำระ", 0, false, ml, ml
                    , "", 0, false, mc, mc
                    );

                try
                {
                    if (txtCodeSearch.Text != "")
                    {
                        String strCode = "";

                        if (txtCodeSearch.Text.Substring(0, 1) == "R")
                        {
                            gbEMS.Visible = false;
                            gbREG.Visible = true;
                            lblBarcodeReg.Text = TSSFormat.EMSTrack("EMS", txtCodeSearch.Text);
                            strCode = txtCodeSearch.Text;
                            clsBarcode Barcode = new clsBarcode();
                            pbBarcodeReg.Image = Barcode.Code128(strCode, Color.Black, Color.White, 40);
                        }
                        else if (txtCodeSearch.Text.Substring(0, 1) == "E")
                        {
                            gbEMS.Visible = true;
                            gbREG.Visible = false;
                            lblCode.Text = TSSFormat.EMSTrack("EMS", txtCodeSearch.Text);
                            strCode = txtCodeSearch.Text;
                            clsBarcode Barcode = new clsBarcode();
                            pbCode.Image = Barcode.Code128(strCode, Color.Black, Color.White, 40);
                        }
                        else
                        {
                        }
                    }
                }
                catch (Exception)
                {
                }
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

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cbbReceiveAddress.SelectedValue = 0;
                cbbSendAddress.SelectedValue = 0;

                if (e.RowIndex >= 0)
                {
                    txtReceiveName.Text = "";
                    txtReceiveAddress.Text = "";

                    txtSendName.Text = "";
                    txtSendAddress.Text = "";

                    DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                    DataTable dt = new DataTable();

                    string[,] Parameter = new string[,]
                    {
                        {"@TrackId", row.Cells["TrackId"].Value.ToString()},
                        {"@TrackCode", ""},
                        {"@TrackDate", ""},
                        {"@TrackDay", ""},
                        {"@TrackTime", ""},
                        {"@TrackScanLocation", ""},
                        {"@TrackDescription", ""},
                        {"@TrackDeliveryStatus", ""},
                        {"@TrackProduct", ""},
                        //{"@TrackZip", ""},
                        //{"@TrackWeight", "0"},
                        //{"@TrackPrice", "0"},
                        //{"@TrackRegisterIDateTime", ""},
                        //{"@TrackRegisterId", ""},
                        {"@TrackRemark", ""},
                        {"@TrackStatus", ""},
                        {"@TrackSendAddress", "0"},
                        {"@TrackReceiveAddress", "0"},
                    };

                     
                    clsConvert TSSConvert = new clsConvert();

                    db.Get("Spr_S_TblSaveTracking", Parameter, out strErr, out dt);

                    txtId.Text = dt.Rows[0]["TrackId"].ToString();
                    txtCode.Text = dt.Rows[0]["TrackCode"].ToString();
                    dtDate.Text = dt.Rows[0]["TrackDate"].ToString();
                    txtTime.Text = dt.Rows[0]["TrackTime"].ToString();
                    txtDay.Text = dt.Rows[0]["TrackDay"].ToString();
                    cbbDescription.SelectedValue = dt.Rows[0]["TrackDescription"].ToString();
                    cbbLocation.Text = dt.Rows[0]["TrackScanLocation"].ToString();
                    txtProduct.Text = dt.Rows[0]["TrackProduct"].ToString();
                    txtDeliveryStatus.Text = dt.Rows[0]["TrackDeliveryStatus"].ToString();
                    txtRemark.Text = dt.Rows[0]["TrackRemark"].ToString();
                    cbbStatus.SelectedValue = dt.Rows[0]["TrackStatus"].ToString();
                    cbbReceiveAddress.SelectedValue = dt.Rows[0]["ADRCode"].ToString();
                    cbbSendAddress.SelectedValue = dt.Rows[0]["ADSCode"].ToString();
                    pbLicense.Image = TSSConvert.Base64ToImage(dt.Rows[0]["TrackLicense"].ToString());

                    txtPostCode.Text = dt.Rows[0]["TrackZip"].ToString();
                    txtWeight.Text = dt.Rows[0]["TrackWeight"].ToString();
                    txtPrice.Text = dt.Rows[0]["TrackPrice"].ToString();
                    txtEntrance.Text = dt.Rows[0]["TrackRegisterIDateTime"].ToString();
                    txtEntranceId.Text = dt.Rows[0]["TrackRegisterId"].ToString();
                }
            }
            catch
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
                        {"@TrackId", txtId.Text},
                    };

                    clsMessage Message = new clsMessage();

                    Message.MessageConfirmation(strOpe, txtCode.Text, dtDate.Value.ToString("dd MMM yyyy") + " " + cbbDescription.Text);

                    using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                    {
                        var result = Mes.ShowDialog();

                        if (result == DialogResult.Yes)
                        {
                            Mes.Close();
                             
                            db.Operations("Spr_D_TblSaveTracking", Parameter, out strErr);

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
                    clsInsert Insert = new clsInsert();
                    
                    Clear();
                }
            }
            catch (Exception ex)
            {
                
                Log.WriteLogData(programCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                clsConvert TSSConvert = new clsConvert();
                if (txtTime.Text != "")
                {
                    strOpe = "U";

                    string[,] Parameter = new string[,]
                    {
                        {"@User", strUserId},
                        {"@TrackId", txtId.Text},
                        {"@TrackCode", txtCode.Text},
                        {"@TrackDate", Date.GetDate(dtp : dtDate)},
                        {"@TrackDay", txtDay.Text},
                        {"@TrackTime", txtTime.Text},
                        {"@TrackScanLocation", cbbLocation.Text == "อื่นๆ (Null)" ? txtLocation.Text : cbbLocation.Text},
                        {"@TrackDescription", cbbDescription.SelectedValue.ToString()},
                        {"@TrackDeliveryStatus", txtDeliveryStatus.Text},
                        {"@TrackLicense", cbbDescription.SelectedValue.ToString() != "TE" ? "" : txtPath.Text != "" ? TSSConvert.ImageToBase64(txtPath.Text) : ""},
                        {"@TrackProduct", txtProduct.Text},
                        {"@TrackZip", txtPostCode.Text},
                        {"@TrackWeight", txtWeight.Text},
                        {"@TrackPrice", txtPrice.Text},
                        {"@TrackRegisterIDateTime", txtEntrance.Text},
                        {"@TrackRegisterId", txtEntranceId.Text},
                        {"@TrackRemark", txtRemark.Text},
                        {"@TrackStatus", cbbStatus.SelectedValue.ToString()},
                        {"@TrackSendAddress", cbbSendAddress.SelectedValue.ToString()},
                        {"@TrackReceiveAddress", cbbReceiveAddress.SelectedValue.ToString()},
                    };

                    clsMessage Message = new clsMessage();
                    Message.MessageConfirmation(strOpe, txtCode.Text, dtDate.Value.ToString("dd MMM yyyy") + " " + cbbDescription.Text);

                    using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                    {
                        var result = Mes.ShowDialog();

                        if (result == DialogResult.Yes)
                        {
                            Mes.Close();
                             
                            db.Operations("Spr_U_TblSaveTracking", Parameter, out strErr);

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
                        clsInsert Insert = new clsInsert();
                        
                        Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                
                Log.WriteLogData(programCode, strAppName, strUserId, ex.Message);
            }
        }

        public void Search()
        {
            if (txtCodeSearch.Text != "")
            {
                string[,] Parameter = new string[,]
                        {
                            {"@Code", ""},
                            {"@Name", txtCodeSearch.Text},
                            {"@Zipcode", ""},
                            {"@District", ""},
                        };

                 
                db.Get("Spr_S_MaillingAddress", Parameter, out strErr, out dt);
            }
            else
            {
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtCodeSearch.Text != "")
            {
                getDataGrid(getDataTable(txtCodeSearch.Text, "", "", ""));
            }
        }

        private void dtDate_ValueChanged(object sender, EventArgs e)
        {
            clsDate clsDate = new clsDate();
            txtDay.Text = clsDate.GetDayOfWeek(dtDate);
        }

        private void cbbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsDataList List = new clsDataList();
            List.SetNewList(cbbLocation, txtLocation);
        }

        private void txtTime_Leave(object sender, EventArgs e)
        {
            clsFormat Format = new clsFormat();
            txtTime.Text = Format.Time(txtTime);
        }

        private void txtTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsNumber(e.KeyChar) || e.KeyChar == 8)
                {
                }
                else
                {
                    e.Handled = true;
                    return;
                }
            }
            catch
            {
            }
        }

        private void cbbReceiveAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[,] Parameter = new string[,]
                    {
                        {"@TrackId",""},
                        {"@TrackCode", ""},
                        {"@TrackDate", ""},
                        {"@TrackDay", ""},
                        {"@TrackTime", ""},
                        {"@TrackScanLocation", ""},
                        {"@TrackDescription", ""},
                        {"@TrackDeliveryStatus", ""},
                        {"@TrackProduct", ""},
                        //{"@TrackZip", ""},
                        //{"@TrackWeight", "0"},
                        //{"@TrackPrice", "0"},
                        //{"@TrackRegisterIDateTime", ""},
                        //{"@TrackRegisterId", ""},
                        {"@TrackRemark", ""},
                        {"@TrackStatus", ""},
                        {"@TrackSendAddress", "0"},
                        {"@TrackReceiveAddress", cbbReceiveAddress.SelectedValue.ToString()},
                    };

                 
                db.Get("Spr_S_TblSaveTracking", Parameter, out strErr, out dt);

                if (dt.Rows.Count > 0)
                {
                    txtReceiveName.Text = dt.Rows[0]["ADRName"].ToString();
                    txtReceiveAddress.Text = dt.Rows[0]["ADRAddress"].ToString();
                }
                else
                {
                    txtReceiveName.Text = "";
                    txtReceiveAddress.Text = "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbbSendAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[,] Parameter = new string[,]
                    {
                        {"@TrackId",""},
                        {"@TrackCode", ""},
                        {"@TrackDate", ""},
                        {"@TrackDay", ""},
                        {"@TrackTime", ""},
                        {"@TrackScanLocation", ""},
                        {"@TrackDescription", ""},
                        {"@TrackDeliveryStatus", ""},
                        {"@TrackProduct", ""},
                        //{"@TrackZip", ""},
                        //{"@TrackWeight", "0"},
                        //{"@TrackPrice", "0"},
                        //{"@TrackRegisterIDateTime", ""},
                        //{"@TrackRegisterId", ""},
                        {"@TrackRemark", ""},
                        {"@TrackStatus", ""},
                        {"@TrackSendAddress", cbbSendAddress.SelectedValue.ToString()},
                        {"@TrackReceiveAddress", "0"},
                    };

                 
                db.Get("Spr_S_TblSaveTracking", Parameter, out strErr, out dt);

                if (dt.Rows.Count > 0)
                {
                    txtSendName.Text = dt.Rows[0]["ADSName"].ToString();
                    txtSendAddress.Text = dt.Rows[0]["ADSAddress"].ToString();
                }
                else
                {
                    txtSendName.Text = "";
                    txtSendAddress.Text = "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                clsConvert TSSConvert = new clsConvert();
                clsMessage Message = new clsMessage();
                if (txtTime.Text != "")
                {
                    strOpe = "I";

                    string[,] Parameter = new string[,]
                    {
                        {"@User", strUserId},
                        {"@TrackCode", txtCode.Text},
                        {"@TrackDate", Date.GetDate(dtp : dtDate)},
                        {"@TrackDay", txtDay.Text},
                        {"@TrackTime", txtTime.Text},
                        {"@TrackScanLocation", cbbLocation.Text == "อื่นๆ (Null)" ? txtLocation.Text : cbbLocation.Text},
                        {"@TrackDescription", cbbDescription.SelectedValue.ToString()},
                        {"@TrackDeliveryStatus", txtDeliveryStatus.Text},
                        {"@TrackLicense", cbbDescription.SelectedValue.ToString() != "TE" ? "" : txtPath.Text != "" ? TSSConvert.ImageToBase64(txtPath.Text) : ""},
                        {"@TrackProduct", txtProduct.Text},
                        {"@TrackZip", txtPostCode.Text},
                        {"@TrackWeight", txtWeight.Text},
                        {"@TrackPrice", txtPrice.Text},
                        {"@TrackRegisterIDateTime", txtEntrance.Text},
                        {"@TrackRegisterId", txtEntranceId.Text},
                        {"@TrackRemark", txtRemark.Text},
                        {"@TrackStatus", cbbStatus.SelectedValue.ToString()},
                        {"@TrackSendAddress", cbbSendAddress.SelectedValue.ToString()},
                        {"@TrackReceiveAddress", cbbReceiveAddress.SelectedValue.ToString()},
                    };

                    Message.MessageConfirmation(strOpe, txtCode.Text, dtDate.Value.ToString("dd MMM yyyy") + " " + cbbDescription.Text);

                    using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                    {
                        var result = Mes.ShowDialog();

                        if (result == DialogResult.Yes)
                        {
                            Mes.Close();
                             
                            db.Operations("Spr_I_TblSaveTracking", Parameter, out strErr);

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

                        clsInsert Insert = new clsInsert();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                
                Log.WriteLogData(programCode, strAppName, strUserId, ex.Message);
            }
        }

        private void cbbDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbDescription.SelectedValue.ToString() == "TE")
            {
                pbLicense.Visible = true;
                txtPath.Visible = false;
                btnBrowse.Visible = true;
            }
            else
            {
                pbLicense.Visible = false;
                txtPath.Visible = false;
                btnBrowse.Visible = false;
                txtPath.Text = "";
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = new Bitmap(open.FileName);
                fileName = Path.GetFileNameWithoutExtension(open.FileName);
                fileType = Path.GetExtension(open.FileName);
                txtPath.Text = open.FileName;
                open.RestoreDirectory = true;
            }
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pbLicense.Image = Image.FromFile(@"" + txtPath.Text);
            }
            catch (Exception)
            {
            }
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            if (txtCode.Text != "" && txtCode.TextLength == 13)
            {
                DataTable dt = new DataTable();

                string[,] Parameter = new string[,]
                    {
                        {"@TrackId", ""},
                        {"@TrackCode", txtCode.Text},
                        {"@TrackDate", ""},
                        {"@TrackDay", ""},
                        {"@TrackTime", ""},
                        {"@TrackScanLocation", ""},
                        {"@TrackDescription", ""},
                        {"@TrackDeliveryStatus", ""},
                        {"@TrackProduct", ""},
                        //{"@TrackZip", ""},
                        //{"@TrackWeight", "0"},
                        //{"@TrackPrice", "0"},
                        //{"@TrackRegisterIDateTime", ""},
                        //{"@TrackRegisterId", ""},
                        {"@TrackRemark", ""},
                        {"@TrackStatus", ""},
                        {"@TrackSendAddress", "0"},
                        {"@TrackReceiveAddress", "0"},
                    };

                 
                db.Get("Spr_S_TblSaveTracking", Parameter, out strErr, out dt);

                if (dt.Rows.Count > 0)
                {
                    txtRemark.Text = dt.Rows[0]["TrackRemark"].ToString();
                    txtProduct.Text = dt.Rows[0]["TrackProduct"].ToString();
                    cbbReceiveAddress.SelectedValue = dt.Rows[0]["ADRCode"].ToString();
                    cbbSendAddress.SelectedValue = dt.Rows[0]["ADSCode"].ToString();
                    txtTime.Focus();
                }
            }
        }

        private void txtDeliveryStatus_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDeliveryStatus.Text != "")
                {
                    cbbStatus.SelectedIndex = 1;
                }
                else
                {
                    cbbStatus.SelectedIndex = 0;
                }
            }
            catch
            {
                cbbStatus.SelectedIndex = 0;
            }
        }

        private void txtCodeSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtCodeSearch.Text == "")
            {
                gbEMS.Visible = false;
                gbREG.Visible = false;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                Clipboard.SetText(txtCode.Text);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                ExportToFile("PDF", sender, e);
            }
        }

        public void ExportToFile(string fileType, object sender, EventArgs e)
        {
            string ReportName = "";

            try
            {
                string[,] Parameter = new string[,]
                {
                    {"@TrackCode", txtCode.Text},
                };

                 
                clsFunction TSSFN = new clsFunction();
                clsConvert TSSConvert = new clsConvert();

                idTSS = Fn.getTssFileName("STTRE");
                db.Get("Spr_R_TblSaveTracking", Parameter, out strErr, out dt);

                if (strErr == null && dt.Rows.Count > 0)
                {
                    string Path = Fn.getPath("App.Report");
                    ReportName = "RSA-R-TRACK00001";

                    DialogResult result = folderBrowserDialog.ShowDialog();
                    dt.Columns.Add(new DataColumn("TrackBarcode", typeof(byte[])));
                    dt.Columns.Add(new DataColumn("TrackSignatureImage", typeof(byte[])));

                    foreach (DataRow row in dt.Rows)
                    {
                        Zen.Barcode.Code39BarcodeDraw bar = Zen.Barcode.BarcodeDrawFactory.Code39WithoutChecksum;
                        Image imgBar;
                        Bitmap BitBar;
                        string strBar = "";
                        var streamBar = new MemoryStream();
                        byte[] byteBar;

                        strBar = dt.Rows[0]["TrackCode"].ToString();
                        imgBar = bar.Draw(strBar, 20);
                        BitBar = (Bitmap)imgBar;
                        BitBar.Save(streamBar, System.Drawing.Imaging.ImageFormat.Png);
                        byteBar = streamBar.ToArray();
                        row["TrackBarcode"] = byteBar;

                        if (row["TrackSignature"].ToString() != "")
                        {
                            row["TrackSignatureImage"] = Convert.FromBase64String(row["TrackSignature"].ToString());
                        }
                    }

                    rpt = new ReportDocument();
                    rpt.Load(Path + ReportName + ".rpt");
                    rpt.SetDataSource(dt);

                    rpt.SetParameterValue("UserName", strUserName);
                    rpt.SetParameterValue("ReportName", ReportName);

                    crystalReportViewer.ReportSource = rpt;
                    crystalReportViewer.Refresh();

                    if (result == DialogResult.OK)
                    {
                        ExportToPath = folderBrowserDialog.SelectedPath + "\\TSSRSA-SENTT00R" + Fn.getFileLastName();

                        if (fileType == "PDF")
                        {
                            rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, ExportToPath + ".pdf");
                            rpt.Close();
                            System.Diagnostics.Process.Start(@"" + ExportToPath + ".pdf");
                        }
                        else
                        {
                            rpt.ExportToDisk(ExportFormatType.Excel, ExportToPath + ".xls");
                            rpt.Close();
                            System.Diagnostics.Process.Start(@"" + ExportToPath + ".xls");
                        }

                        try
                        {
                            clsInsert Insert = new clsInsert();
                            Insert.TblLogReport(idTSS, ReportName, txtCode.Text, programCode, "STT", "TSSRSA-SENTT00R" + Fn.getFileLastName() + "." + fileType, ExportToPath + "." + fileType, strUserName + " " + strUserSurname);
                        }
                        catch
                        {
                        }
                    }
                    rpt.Close();
                    Clear();
                }
                else
                {
                    Message.MessageResult("N", "SH", strErr);
                    txtCodeSearch.Focus();
                }
            }
            catch (Exception)
            {
                Message.MessageResult("R", "E", strErr);
                Clear();
            }
        }
    }
}