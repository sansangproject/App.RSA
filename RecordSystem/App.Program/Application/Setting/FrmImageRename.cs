using Microsoft.VisualBasic.Logging;
using Org.BouncyCastle.Ocsp;
using SANSANG.Class;
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tesseract;
using static Telerik.WinControls.VirtualKeyboard.VirtualKeyboardNativeMethods;

namespace App
{
    public partial class FrmImageRename : Form
    {
        public string AppCode = "SETRN00";
        public string AppName = "FrmImageRename";

        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;
        private clsLog Log = new clsLog();

        public FrmImageRename(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
        {
            InitializeComponent();
            UserId = UserIdLogin;
            UserName = UserNameLogin;
            UserSurname = UserSurNameLogin;
            UserType = UserTypeLogin;
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a Folder";
                dialog.ShowNewFolderButton = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string FolderPath = dialog.SelectedPath;
                    txtFilePath.Text = FolderPath;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                string TessDataPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\App.Library\OCR\tessdata"));
                string FolderPath = txtFilePath.Text;

                if (!Directory.Exists(FolderPath))
                {
                    MessageBox.Show("Folder does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string[] ImageFiles = Directory.GetFiles(FolderPath, "*.*")
                                        .Where(file => file.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                               file.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                                               file.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                                               file.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                                               file.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) ||
                                               file.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase))
                                        .ToArray();

                using (var Engine = new TesseractEngine(TessDataPath, "eng+tha", EngineMode.Default))
                {
                    foreach (string ImageFile in ImageFiles)
                    {
                        try
                        {
                            using (var Image = Pix.LoadFromFile(ImageFile))
                            {
                                using (var Page = Engine.Process(Image))
                                {
                                    string Text = Page.GetText();
                                    string Code = "";
                                    
                                    Code = new string(Text.Where(char.IsLetterOrDigit).ToArray()).ToUpper();

                                    if (!string.IsNullOrEmpty(Code))
                                    {
                                        string Directory = Path.GetDirectoryName(ImageFile);
                                        string Extension = Path.GetExtension(ImageFile);
                                        string NewImagePath = Path.Combine(Directory, Code + Extension);

                                        if (!File.Exists(NewImagePath))
                                        {
                                            File.Move(ImageFile, NewImagePath);
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                        }
                    }
                }

                RefreshFolder(FolderPath);
                txtFilePath.Text = "";
                MessageBox.Show("Renaming process completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        [DllImport("shell32.dll")]
        public static extern void SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

        public static void RefreshFolder(string folderPath)
        {
            SHChangeNotify(0x8000000, 0x1000, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine("Folder refreshed.");
        }
    }
}