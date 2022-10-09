using Spire.Barcode;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmBarQRCode : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "APPBCO00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";
        public string filePath = "";
        public string fileName = "-";
        public string fileType = ".jpg";
        public string strAddress = "";

        public short Widths;
        public short Heights;

        public short LeftMargin;
        public short RightMargin;
        public short TopMargin;
        public short BottomMargin;

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();

        private clsBarcode Barcode = new clsBarcode();
        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsImage TSSImage = new clsImage();

        public string[,] Parameter = new string[,] { };

        public FrmBarQRCode(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmBarQRCode_Load(object sender, EventArgs e)
        {
            
            Clear();
        }

        public void Clear()
        {
            txtText.Text = "";

            cbbType.Text = "";
            cbbLevel.Text = "";
            cbbGraphics.Text = "";
            cbbForeColor.Text = "";
            cbbBackColor.Text = "";
            cbbResolution.Text = "";
            cbbShowData.Text = "";

            txtWidth.Text = "";
            txtHeight.Text = "";
            txtLeftMargin.Text = "";
            txtRightMargin.Text = "";
            txtTopMargin.Text = "";
            txtBottomMargin.Text = "";

            pbImageCode.Image = null;
            txtText.Focus();
        }

        public void getDataGrid(DataTable dt)
        {
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            BarcodeSettings settings = new BarcodeSettings();

            try
            {
                if (this.txtText.Text != null && this.txtText.Text.Length > 0)
                {
                    settings.Data = txtText.Text;
                    settings.Data2D = txtText.Text;
                }

                if (this.txtWidth.Text != null && this.txtWidth.Text.Length > 0 && Int16.TryParse(txtWidth.Text, out Widths))
                {
                    settings.X = Width;
                }

                if (this.txtHeight.Text != null && this.txtHeight.Text.Length > 0 && Int16.TryParse(txtHeight.Text, out Heights))
                {
                    settings.Y = Height;
                }

                if (this.txtHeight.Text != null && this.txtHeight.Text.Length > 0 && Int16.TryParse(txtHeight.Text, out Heights))
                {
                    settings.BarHeight = Height;
                }

                if (this.txtLeftMargin.Text != null && this.txtLeftMargin.Text.Length > 0 && Int16.TryParse(txtLeftMargin.Text, out LeftMargin))
                {
                    settings.LeftMargin = LeftMargin;
                }

                if (this.txtRightMargin.Text != null && this.txtRightMargin.Text.Length > 0 && Int16.TryParse(txtRightMargin.Text, out RightMargin))
                {
                    settings.RightMargin = RightMargin;
                }

                if (this.txtTopMargin.Text != null && this.txtTopMargin.Text.Length > 0 && Int16.TryParse(txtTopMargin.Text, out TopMargin))
                {
                    settings.TopMargin = TopMargin;
                }

                if (this.txtBottomMargin.Text != null && this.txtBottomMargin.Text.Length > 0 && Int16.TryParse(txtBottomMargin.Text, out BottomMargin))
                {
                    settings.BottomMargin = BottomMargin;
                }

                if (this.cbbType.SelectedItem != null)
                {
                    switch (cbbType.Text)
                    {
                        case "EAN13":
                            settings.Type = BarCodeType.EAN13;
                            break;

                        case "EAN8":
                            settings.Type = BarCodeType.EAN8;
                            break;

                        case "Code128":
                            settings.Type = BarCodeType.Code128;
                            break;

                        case "Code93Extended":
                            settings.Type = BarCodeType.Code93Extended;
                            break;

                        case "Code93":
                            settings.Type = BarCodeType.Code93;
                            break;

                        case "Code39Extended":
                            settings.Type = BarCodeType.Code39Extended;
                            break;

                        case "Code39":
                            settings.Type = BarCodeType.Code39;
                            break;

                        case "Interleaved25":
                            settings.Type = BarCodeType.Interleaved25;
                            break;

                        case "Code25":
                            settings.Type = BarCodeType.Code25;
                            break;

                        case "Code11":
                            settings.Type = BarCodeType.Code11;
                            break;

                        case "Codabar":
                            settings.Type = BarCodeType.Codabar;
                            break;

                        case "EAN128":
                            settings.Type = BarCodeType.EAN128;
                            break;

                        case "SCC14":
                            settings.Type = BarCodeType.SCC14;
                            break;

                        case "EAN14":
                            settings.Type = BarCodeType.EAN14;
                            break;

                        case "SSCC18":
                            settings.Type = BarCodeType.SSCC18;
                            break;

                        case "ITF14":
                            settings.Type = BarCodeType.ITF14;
                            break;

                        case "ITF6":
                            settings.Type = BarCodeType.ITF6;
                            break;

                        case "UPCA":
                            settings.Type = BarCodeType.UPCA;
                            break;

                        case "Planet":
                            settings.Type = BarCodeType.Planet;
                            break;

                        case "MSI":
                            settings.Type = BarCodeType.MSI;
                            break;

                        case "DataMatrix":
                            settings.Type = BarCodeType.DataMatrix;
                            break;

                        case "QRCode":
                            settings.Type = BarCodeType.QRCode;
                            break;

                        case "Pdf417":
                            settings.Type = BarCodeType.Pdf417;
                            break;

                        case "Pdf417Macro":
                            settings.Type = BarCodeType.Pdf417Macro;
                            break;

                        case "RSS14":
                            settings.Type = BarCodeType.RSS14;
                            break;

                        case "RSS14Truncated":
                            settings.Type = BarCodeType.RSS14Truncated;
                            break;

                        case "PostNet":
                            settings.Type = BarCodeType.PostNet;
                            break;

                        case "UPCE":
                            settings.Type = BarCodeType.UPCE;
                            break;

                        case "RoyalMail4State":
                            settings.Type = BarCodeType.RoyalMail4State;
                            break;

                        case "DeutschePostLeitcode":
                            settings.Type = BarCodeType.DeutschePostLeitcode;
                            break;

                        case "DeutschePostIdentcode":
                            settings.Type = BarCodeType.DeutschePostIdentcode;
                            break;

                        case "OPC":
                            settings.Type = BarCodeType.OPC;
                            break;

                        case "PZN":
                            settings.Type = BarCodeType.PZN;
                            break;

                        case "SwissPostParcel":
                            settings.Type = BarCodeType.SwissPostParcel;
                            break;

                        case "USPS":
                            settings.Type = BarCodeType.USPS;
                            break;

                        case "RSSExpanded":
                            settings.Type = BarCodeType.RSSExpanded;
                            break;

                        case "RSSLimited":
                            settings.Type = BarCodeType.RSSLimited;
                            break;

                        case "SingaporePost4State":
                            settings.Type = BarCodeType.SingaporePost4State;
                            break;
                    }
                }

                if (this.cbbLevel.SelectedItem != null)
                {
                    switch (cbbLevel.Text)
                    {
                        case "L":
                            settings.QRCodeECL = QRCodeECL.L;
                            break;

                        case "M":
                            settings.QRCodeECL = QRCodeECL.M;
                            break;

                        case "Q":
                            settings.QRCodeECL = QRCodeECL.Q;
                            break;

                        case "H":
                            settings.QRCodeECL = QRCodeECL.H;
                            break;
                    }
                }

                if (this.cbbForeColor.SelectedItem != null)
                {
                    settings.ForeColor = Color.FromName(this.cbbForeColor.SelectedItem.ToString());
                }

                if (this.cbbBackColor.SelectedItem != null)
                {
                    settings.BackColor = Color.FromName(this.cbbBackColor.SelectedItem.ToString());
                }

                if (this.cbbShowData.SelectedItem != null)
                {
                    settings.ShowText = Convert.ToBoolean(cbbShowData.Text);
                }

                if (this.cbbHasBorder.SelectedItem != null)
                {
                    settings.HasBorder = Convert.ToBoolean(cbbHasBorder.Text);
                    settings.BorderWidth = 0.5F;
                }

                if (this.cbbResolution.SelectedItem != null)
                {
                    switch (cbbResolution.Text)
                    {
                        case "Graphics":
                            settings.ResolutionType = ResolutionType.Graphics;
                            break;

                        case "Printer":
                            settings.ResolutionType = ResolutionType.Printer;
                            break;

                        case "UseDpi":
                            settings.ResolutionType = ResolutionType.UseDpi;
                            break;
                    }
                }

                if (this.cbbGraphics.SelectedItem != null)
                {
                    switch (cbbGraphics.Text)
                    {
                        case "Display":
                            settings.Unit = GraphicsUnit.Display;
                            break;

                        case "Document":
                            settings.Unit = GraphicsUnit.Document;
                            break;

                        case "Inch":
                            settings.Unit = GraphicsUnit.Inch;
                            break;

                        case "Millimeter":
                            settings.Unit = GraphicsUnit.Millimeter;
                            break;

                        case "Pixel":
                            settings.Unit = GraphicsUnit.Pixel;
                            break;

                        case "Point":
                            settings.Unit = GraphicsUnit.Point;
                            break;

                        case "World":
                            settings.Unit = GraphicsUnit.World;
                            break;
                    }
                }

                pbImageCode.Image = Barcode.Generate(settings);
            }
            catch (Exception)
            {
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|GIF Files (*.gif)|*.gif|ICON Files (*.icon)|*.icon";

            if (DialogResult.OK == save.ShowDialog())
            {
            }
        }
    }
}