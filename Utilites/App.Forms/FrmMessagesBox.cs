using System;
using System.Resources;
using System.Windows.Forms;
using SANSANG.Class; 
using SANSANG.Database;

namespace SANSANG.Utilites.App.Forms
{
    public partial class FrmMessagesBox : Form
    {
        public String strtxtMessageHead = "";
        public String strtxtMessageDetail = "";
        public String strbtnYes = "";
        public String strbtnNo = "";
        public String strpbMessage = "";

        public FrmMessagesBox(string MesHeader, string MesDetail, string MesBtnYes, string MesBtnNo, string CodePb)
        {
            InitializeComponent();
            strtxtMessageHead = MesHeader;
            strtxtMessageDetail = MesDetail;
            strbtnYes = MesBtnYes;
            strbtnNo = MesBtnNo;
            strpbMessage = CodePb;
            Display();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.No;
        }

        private void Display()
        {
            clsImage Images = new clsImage();
            txtMessageHead.Text = strtxtMessageHead.TrimStart();
            txtMessageDetail.Text = strtxtMessageDetail.TrimStart();
            btnYes.Text = strbtnYes;
            btnNo.Text = strbtnNo;
            Images.ShowImage(pbMessage, Code: strpbMessage);
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void FrmMessagesBox_Load(object sender, EventArgs e)
        {
        }
    }
}