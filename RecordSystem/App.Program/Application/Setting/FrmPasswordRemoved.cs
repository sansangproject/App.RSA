using System;
using System.IO;
using iTextSharp.text.pdf;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace App
{
    public partial class FrmRemovePassword : Form
    {
        public string AppCode = "SETRP00";
        public string AppName = "FrmRemovePassword";
        private string FilePath = "";

        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public FrmRemovePassword(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
        {
            InitializeComponent();
            UserId = UserIdLogin;
            UserName = UserNameLogin;
            UserSurname = UserSurNameLogin;
            UserType = UserTypeLogin;
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

        private void btnRemovePassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                MessageBox.Show("Please select a PDF file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Please enter the PDF password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string fileName = Path.GetFileNameWithoutExtension(FilePath);
            string extension = Path.GetExtension(FilePath);
            string outputFileName = $"{fileName}_Unlocked{extension}";
            string OutputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), outputFileName);
            string Password = txtPassword.Text;

            try
            {
                using (PdfReader reader = new PdfReader(FilePath, new ASCIIEncoding().GetBytes(Password)))
                {
                    using (FileStream fs = new FileStream(OutputPath, FileMode.Create, FileAccess.Write))
                    {
                        PdfStamper Stamper = new PdfStamper(reader, fs);
                        Stamper.Close();
                    }
                }

                MessageBox.Show("Password removed successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}