using SANSANG.Database;
using Spire.Barcode;
using System.Data;
using System.Drawing;

namespace SANSANG.Class
{
    public class clsBarcode
    {
        private static BarcodeSettings BarcodeSettings = new BarcodeSettings();
        private DataTable dt = new DataTable();

        private clsSearch Search = new clsSearch();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
        private clsInsert Insert = new clsInsert();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();

        public string[,] Parameter = new string[,] { };

        public Image Generate(BarcodeSettings setting)
        {
            BarCodeGenerator generator = new BarCodeGenerator(setting);
            return generator.GenerateImage();
        }

        public Image QRCode(string strData, Color strForeColor, Color strBackColor, string strLevel, int intWidth, bool showText = false)
        {
            BarcodeSettings.Type = BarCodeType.QRCode;
            BarcodeSettings.Unit = GraphicsUnit.Pixel;
            BarcodeSettings.ShowText = showText;
            BarcodeSettings.ShowTextOnBottom = true;
            BarcodeSettings.ResolutionType = ResolutionType.UseDpi;

            BarcodeSettings.Data = strData;
            BarcodeSettings.Data2D = strData;
            BarcodeSettings.ForeColor = strForeColor;
            BarcodeSettings.BackColor = strBackColor;

            BarcodeSettings.X = intWidth;
            BarcodeSettings.LeftMargin = 1;
            BarcodeSettings.RightMargin = 1;
            BarcodeSettings.TopMargin = 1;
            BarcodeSettings.BottomMargin = 1;

            switch (strLevel)
            {
                case "L":
                    BarcodeSettings.QRCodeECL = QRCodeECL.L;
                    break;

                case "M":
                    BarcodeSettings.QRCodeECL = QRCodeECL.M;
                    break;

                case "Q":
                    BarcodeSettings.QRCodeECL = QRCodeECL.Q;
                    break;

                case "H":
                    BarcodeSettings.QRCodeECL = QRCodeECL.H;
                    break;
            }

            BarCodeGenerator generator = new BarCodeGenerator(BarcodeSettings);
            return generator.GenerateImage();
        }

        public Bitmap QRCodeBitmap(string strData, Color strForeColor, Color strBackColor, string strLevel, int intWidth, bool showText = false)
        {
            BarcodeSettings.Type = BarCodeType.QRCode;
            BarcodeSettings.Unit = GraphicsUnit.Pixel;
            BarcodeSettings.ShowText = showText;
            BarcodeSettings.ShowTextOnBottom = true;
            BarcodeSettings.ResolutionType = ResolutionType.UseDpi;

            BarcodeSettings.Data = strData;
            BarcodeSettings.Data2D = strData;
            BarcodeSettings.ForeColor = strForeColor;
            BarcodeSettings.BackColor = strBackColor;

            BarcodeSettings.X = intWidth;
            BarcodeSettings.LeftMargin = 1;
            BarcodeSettings.RightMargin = 1;
            BarcodeSettings.TopMargin = 1;
            BarcodeSettings.BottomMargin = 1;

            switch (strLevel)
            {
                case "L":
                    BarcodeSettings.QRCodeECL = QRCodeECL.L;
                    break;

                case "M":
                    BarcodeSettings.QRCodeECL = QRCodeECL.M;
                    break;

                case "Q":
                    BarcodeSettings.QRCodeECL = QRCodeECL.Q;
                    break;

                case "H":
                    BarcodeSettings.QRCodeECL = QRCodeECL.H;
                    break;
            }

            BarCodeGenerator generator = new BarCodeGenerator(BarcodeSettings);
            return (Bitmap)generator.GenerateImage();
        }

        public Image QRCodeForTag(string strData, Color strForeColor, Color strBackColor, string strLevel, int intWidth, bool showText = false)
        {
            BarcodeSettings.Type = BarCodeType.QRCode;
            BarcodeSettings.Unit = GraphicsUnit.Inch;
            BarcodeSettings.ShowText = false;
            BarcodeSettings.ResolutionType = ResolutionType.UseDpi;

            BarcodeSettings.Data = strData;
            BarcodeSettings.ForeColor = strForeColor;
            BarcodeSettings.BackColor = strBackColor;

            BarcodeSettings.X = intWidth;
            BarcodeSettings.LeftMargin = 1;
            BarcodeSettings.RightMargin = 1;
            BarcodeSettings.TopMargin = 1;
            BarcodeSettings.BottomMargin = 1;

            switch (strLevel)
            {
                case "L":
                    BarcodeSettings.QRCodeECL = QRCodeECL.L;
                    break;

                case "M":
                    BarcodeSettings.QRCodeECL = QRCodeECL.M;
                    break;

                case "Q":
                    BarcodeSettings.QRCodeECL = QRCodeECL.Q;
                    break;

                case "H":
                    BarcodeSettings.QRCodeECL = QRCodeECL.H;
                    break;
            }

            BarCodeGenerator generator = new BarCodeGenerator(BarcodeSettings);
            return generator.GenerateImage();
        }

        public Image Code128(string strData, Color strForeColor, Color strBackColor, int intHeight, bool showText = false)
        {
            BarcodeSettings.Type = BarCodeType.Code128;
            BarcodeSettings.Unit = GraphicsUnit.Pixel;

            BarcodeSettings.ShowText = showText;
            BarcodeSettings.ShowTextOnBottom = showText;
            BarcodeSettings.ResolutionType = ResolutionType.UseDpi;

            BarcodeSettings.Data = strData;
            BarcodeSettings.ForeColor = strForeColor;
            BarcodeSettings.BackColor = strBackColor;
            BarcodeSettings.X = 1;
            BarcodeSettings.BarHeight = intHeight;

            BarcodeSettings.TextAlignment = StringAlignment.Center;

            BarcodeSettings.TopMargin = 1;
            BarcodeSettings.BottomMargin = 1;
            BarcodeSettings.LeftMargin = 1;
            BarcodeSettings.RightMargin = 1;

            BarCodeGenerator generator = new BarCodeGenerator(BarcodeSettings);
            return generator.GenerateImage();
        }

        public Image Code39(string strData, Color strForeColor, Color strBackColor, int intHeight, bool showText = false)
        {
            BarcodeSettings.Type = BarCodeType.Code39;
            BarcodeSettings.Unit = GraphicsUnit.Pixel;
            BarcodeSettings.ShowText = showText;
            BarcodeSettings.ResolutionType = ResolutionType.UseDpi;

            BarcodeSettings.Data = strData;
            BarcodeSettings.ForeColor = strForeColor;
            BarcodeSettings.BackColor = strBackColor;
            BarcodeSettings.X = 50;
            BarcodeSettings.BarHeight = intHeight;

            BarcodeSettings.TopMargin = 1;
            BarcodeSettings.BottomMargin = 1;
            BarcodeSettings.LeftMargin = 1;
            BarcodeSettings.RightMargin = 1;

            BarCodeGenerator generator = new BarCodeGenerator(BarcodeSettings);
            return generator.GenerateImage();
        }

        public Image Pdf417(string strData, Color strForeColor, Color strBackColor, int intHeight, bool showText = false)
        {
            BarcodeSettings.Type = BarCodeType.Pdf417;
            BarcodeSettings.Unit = GraphicsUnit.Pixel;
            BarcodeSettings.ShowText = true;
            BarcodeSettings.ResolutionType = ResolutionType.UseDpi;

            BarcodeSettings.Data = strData;
            BarcodeSettings.ForeColor = strForeColor;
            BarcodeSettings.BackColor = strBackColor;
            BarcodeSettings.X = 1;
            BarcodeSettings.BarHeight = intHeight;

            BarcodeSettings.TopMargin = 1;
            BarcodeSettings.BottomMargin = 1;
            BarcodeSettings.LeftMargin = 1;
            BarcodeSettings.RightMargin = 1;

            BarCodeGenerator generator = new BarCodeGenerator(BarcodeSettings);
            return generator.GenerateImage();
        }

        public Image EAN13(string strData, Color strForeColor, Color strBackColor, int intHeight, bool ShowText = false)
        {
            BarcodeSettings.Type = BarCodeType.EAN13;
            BarcodeSettings.Unit = GraphicsUnit.Pixel;
            BarcodeSettings.ShowText = ShowText;
            BarcodeSettings.ResolutionType = ResolutionType.UseDpi;
            BarcodeSettings.UseChecksum = CheckSumMode.ForceEnable;

            BarcodeSettings.ShowTextOnBottom = ShowText;
            BarcodeSettings.TextAlignment = StringAlignment.Center;

            BarcodeSettings.Data = strData;
            BarcodeSettings.ForeColor = strForeColor;
            BarcodeSettings.BackColor = strBackColor;
            BarcodeSettings.X = 1;
            BarcodeSettings.BarHeight = intHeight;

            BarcodeSettings.TopMargin = 1;
            BarcodeSettings.BottomMargin = 1;
            BarcodeSettings.LeftMargin = 1;
            BarcodeSettings.RightMargin = 1;

            BarCodeGenerator generator = new BarCodeGenerator(BarcodeSettings);
            Image image = generator.GenerateImage();
            image.Save("EAN-13.png", System.Drawing.Imaging.ImageFormat.Png);

            return image;
        }

        public Image UPCA(string strData, Color strForeColor, Color strBackColor, int intHeight, bool showText = false)
        {
            BarcodeSettings.Type = BarCodeType.UPCA;
            BarcodeSettings.Unit = GraphicsUnit.Pixel;
            BarcodeSettings.ShowText = true;
            BarcodeSettings.ResolutionType = ResolutionType.UseDpi;

            BarcodeSettings.Data = strData;
            BarcodeSettings.ForeColor = strForeColor;
            BarcodeSettings.BackColor = strBackColor;
            BarcodeSettings.X = 1;
            BarcodeSettings.BarHeight = intHeight;

            BarcodeSettings.TopMargin = 1;
            BarcodeSettings.BottomMargin = 1;
            BarcodeSettings.LeftMargin = 1;
            BarcodeSettings.RightMargin = 1;

            BarCodeGenerator generator = new BarCodeGenerator(BarcodeSettings);
            return generator.GenerateImage();
        }
    }
}