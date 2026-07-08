using Microsoft.ReportingServices.Interfaces;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Logging;
using Org.BouncyCastle.Ocsp;
using SANSANG;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Utilites.App.Forms;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Telerik.WinControls;
using Tesseract;
using static Telerik.WinControls.VirtualKeyboard.VirtualKeyboardNativeMethods;

namespace App
{
    public partial class FrmStatementConverter : Form
    {
        public string AppCode = "CONST00";
        public string AppName = "FrmStatementConverter";
        private string FilePath = "";


        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;
        public string AbbProvince;
        public bool Start = true;
        private Timer Timer = new Timer();
        private clsDataList List = new clsDataList();
        private clsConvert Convert = new clsConvert();
        private DataListConstant DataList = new DataListConstant();
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(25);
        private clsFunction Function = new clsFunction();
        private clsMessage Message = new clsMessage();

        public FrmStatementConverter(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
        {
            InitializeComponent();
            UserId = UserIdLogin;
            UserName = UserNameLogin;
            UserSurname = UserSurNameLogin;
            UserType = UserTypeLogin;
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            Loading.Show();
            Timer.Interval = (1000);
            Timer.Start();
            Timer.Tick += new EventHandler(LoadList);
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetLists(cbbBank, string.Format(DataList.BankId, "status", "1000"));
            cbbBank.BackColor = Color.WhiteSmoke;

            Start = true;
            AbbProvince = "";
            gbForm.Enabled = true;
            Clear();
            Timer.Stop();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog Dialog = new OpenFileDialog())
            {
                Dialog.Filter = "PDF Files (*.pdf)|*.pdf";
                if (Dialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = Dialog.FileName;
                    txtFilePath.Text = FilePath;
                }
            }
        }

       
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (cbbBank.SelectedIndex <= 0)
            {
                Message.MessageConfirmation("IM","","Please Select a Bank");

                using (var mes = new FrmMessagesBoxOK(
                    Message.strOperation,
                    Message.strMes,
                    "OK",
                    Message.strImage))
                {
                    mes.ShowDialog();
                }
                cbbBank.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFilePath.Text))
            {
                Message.MessageConfirmation("IM", "", "Please select a PDF File");

                using (var mes = new FrmMessagesBoxOK(
                    Message.strOperation,
                    Message.strMes,
                    "OK",
                    Message.strImage))
                {
                    mes.ShowDialog();
                }
                cbbBank.Focus();
                return;
            }

            Convert.ConvertStatement(FilePath, cbbBank.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            Function.ClearAll(gbForm);
            Start = false;
        }
    }
}