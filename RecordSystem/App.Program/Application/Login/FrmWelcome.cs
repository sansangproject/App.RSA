using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SANSANG.Class;

namespace SANSANG
{
    public partial class FrmWelcome : Form
    {
        public FrmWelcome()
        {
            InitializeComponent();
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            var Task = new TaskCompletionSource<bool>();
            System.Timers.Timer Timer = new System.Timers.Timer();

            Timer.Elapsed += (obj, args) =>
            {
                Task.TrySetResult(true);
            };

            Timer.Interval = 5000000;
            Timer.AutoReset = false;
            Timer.Start();

            FrmLogin frm = new FrmLogin();
            frm.Activate();
            frm.Show();

        }

        private void FrmKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                clsFunction Fn = new clsFunction();
                FrmLogin frm = new FrmLogin();

                string keyCode = Fn.keyPress(sender, e);

                if (keyCode == "Enter")
                {
                    frm.Login();
                }
            }
            catch (Exception ex)
            {
                clsLog Log = new clsLog();
                Log.WriteLogData("Welcome", "Welcome", "Loging", ex.Message);
            }
        }
    }
}