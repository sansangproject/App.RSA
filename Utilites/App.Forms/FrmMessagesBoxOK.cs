using System;
using System.Resources;
using System.Windows.Forms;
using SANSANG.Class; 
using SANSANG.Database;

namespace SANSANG.Utilites.App.Forms
{
    public partial class FrmMessagesBoxOK : Form
    {
        public string strtxtMessageHead = "";
        public string strtxtMessageDetail = "";
        public string strbtnOK = "";
        public string strpbMessage = "";
        public string strpbId = "";

        public FrmMessagesBoxOK(string Header, string Detail, string Button, string Code = "", string Id = "")
        {
            InitializeComponent();
            strtxtMessageHead = Header;
            strtxtMessageDetail = Detail;
            strbtnOK = Button;
            strpbMessage = Code;
            strpbId = Id;
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
            btnOK.Text = strbtnOK;
            TSSImage.ShowImage(pbMessage, Id: strpbId, Code: strpbMessage);
        }

        private void FrmMessagesBox_Load(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}