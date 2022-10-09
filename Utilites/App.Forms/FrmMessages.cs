using System;
using System.Resources;
using System.Windows.Forms;
using SANSANG.Class; 
using SANSANG.Database;

namespace SANSANG.Utilites.App.Forms
{
    public partial class FrmMessages : Form
    {
        public string strtxtMessageHead = "";
        public string strtxtMessageDetail = "";
        public string strbtnOK = "";
        public string strpbMessage = "";

        public FrmMessages(string MesHeader, string MesDetail, string CodePb)
        {
            InitializeComponent();
            strtxtMessageHead = MesHeader;
            strtxtMessageDetail = MesDetail;
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
            clsImage TSSImage = new clsImage();
            txtMessageHead.Text = strtxtMessageHead;
            txtMessageDetail.Text = strtxtMessageDetail;
            TSSImage.ShowImage(pbMessage, Code: strpbMessage);
        }

        private void FrmMessagesBox_Load(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.No;
        }
    }
}