using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmShowImage : Form
    {
        public string imageCode, Locations = "";
        public int zoomValue = 1;
        private Image imgOriginal;

        private clsFunction Fn = new clsFunction();
        private clsImage TSSImage = new clsImage();
        private clsMessage Message = new clsMessage();

        public FrmShowImage(string strReceptFileId)
        {
            InitializeComponent();
            imageCode = strReceptFileId;
        }

        private void FrmShowImage_Load(object sender, EventArgs e)
        {
           
        }

        public Image PictureBoxZoom(Image img, Size size)
        {
            int imgSizeDiff = 0;
            Bitmap bm;

            if (img.Width > img.Height)
            {
                imgSizeDiff = img.Width - img.Height;
                bm = new Bitmap(img, Convert.ToInt32(800 + (size.Width * 100) + imgSizeDiff), Convert.ToInt32((800 + (size.Height * 100))));
            }
            else
            {
                imgSizeDiff = img.Height - img.Width;
                bm = new Bitmap(img, Convert.ToInt32(800 + (size.Width + 100)), Convert.ToInt32((800 + (size.Height * 100)) + imgSizeDiff));
            }

            Graphics grap = Graphics.FromImage(bm);
            grap.InterpolationMode = InterpolationMode.HighQualityBicubic;
            return bm;
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            //zoomValue = zoomValue + 1;
            //pbShowPic.Visible = false;

            //if ((zoomValue > 1) && (imgOriginal != null))
            //{
            //    pbZoom.Image = null;
            //    pbZoom.Image = PictureBoxZoom(imgOriginal, new Size(zoomValue, zoomValue));
            //    pbZoom.Visible = true;

            //    this.panel.AutoScroll = true;
            //    this.pbZoom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            //}
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            //zoomValue = zoomValue - 1;
            //pbShowPic.Visible = false;

            //if ((zoomValue > 1) && (imgOriginal != null))
            //{
            //    pbZoom.Image = null;
            //    pbZoom.Image = PictureBoxZoom(imgOriginal, new Size(zoomValue, zoomValue));
            //    pbZoom.Visible = true;

            //    this.panel.AutoScroll = true;
            //    this.pbZoom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            //}
            //else
            //{
            //    pbShowPic.Visible = true;
            //    pbZoom.Visible = false ;
            //    zoomValue = 1;
            //}
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            pbShowPic.Visible = true;
            pbZoom.Visible = false;
            zoomValue = 1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
        }

        private void txtImageCode_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
        }

        private void trbZoom_Scroll(object sender, EventArgs e)
        {
            imgOriginal = pbShowPic.Image;

            if (trbZoom.Value > 0)
            {
                pbShowPic.Image = Zoom(imgOriginal, new Size(trbZoom.Value, trbZoom.Value));
            }
        }

        private Image Zoom(Image img, Size size)
        {
            Bitmap bmp = new Bitmap(img, img.Width + (img.Width * size.Width / 100), img.Height + (img.Height * size.Height / 100));
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }
    }
}