using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SANSANG.Class; 
using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmAPPID00 : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string programCode = "APPID00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        private clsFunction Fn = new clsFunction();
        private clsDataList List = new clsDataList();
        private clsBarcode Barcode = new clsBarcode();
        public string[,] Parameter = new string[,] { };

        public FrmAPPID00(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmAPPID00_Load(object sender, EventArgs e)
        {
            List.GetList(cbbTable, "", "TableWithSchema");
            Clear();
        }

        public void Clear()
        {
            pbQrcode.Image = null;
            cbbTable.SelectedValue = "0";
            txtGetCode.Text = "";

            txtDataCode.Text = "";
            pbImage.Image = null;
            txtCode.Text = "";
            txtCheck.Text = "";
            txtDate.Text = "";
            txtTime.Text = "";
            gbDetail.Visible = true;
            
            btnGen.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtDataCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (txtDataCode.Text == "")
            //{
            //    pbImage.Visible = false;
            //}
            //else
            //{
            //    clsFormat Format = new clsFormat();
            //    Format.TSSId(sender, e, txtDataCode);
            //}
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtDataCode.Text != "")
            {
                var Codes = Fn.GetCodes("", txtDataCode.Text, "Decode");
            }
        }

        private void cbDateCode_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbDateCode.Checked == true)
            //{
            //    dtDateCode.Enabled = true;
            //}
            //else
            //{
            //    dtDateCode.Enabled = false;
            //}
        }

        private void txtProCode_TextChanged(object sender, EventArgs e)
        {
            //if (txtProCode.Text == "")
            //{
            //    btnGen.Enabled = false;
            //}
            //else
            //{
            //    btnGen.Enabled = true;
            //}
        }

        private void txtProCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.KeyChar = Char.ToUpper(e.KeyChar);
            //if (txtProCode.Text == "")
            //{
            //    btnGen.Enabled = false;
            //}
            //else
            //{
            //    btnGen.Enabled = true;
            //}
        }

        private void btnGen_Click(object sender, EventArgs e)
        {
            string Codes = Fn.GetCodes(Fn.getComboboxId(cbbTable), "", "Generated");
            pbQrcode.Image = Barcode.QRCode(Codes, Color.Black, Color.White, "Q", 3, false);
            txtGetCode.Text = Codes;
            btnCopy.Focus();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtGetCode.Text);
        }

        private void cbbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGen.Focus();
        }
    }
}