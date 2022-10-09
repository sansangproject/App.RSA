using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SANSANG
{
    public partial class FrmAnimatedProgress : Form
    {
        private bool simulateError = false;
        private int Steps = 1;
        public FrmAnimatedProgress(int Step)
        {
            InitializeComponent();
            Steps = Step;
            this.pictureBox.Image = null;// global::RecordSystemApplication.Properties.Resources.Animation;
            this.backgroundWorker.RunWorkerAsync();
        }

        private void OnStartClick(object sender, EventArgs e)
        {
            this.pictureBox.Image = null;// global::RecordSystemApplication.Properties.Resources.Animation;
            this.backgroundWorker.RunWorkerAsync();
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            this.backgroundWorker.CancelAsync();
        }

        private void OnSimulateErrorClick(object sender, EventArgs e)
        {
            this.simulateError = true;
        }

        private void OnDoWork(object sender, DoWorkEventArgs e)
        {
            Random rand = new Random();
            for (int i = 0; i < 101; i += Steps)
            {
                if (this.backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }

                this.backgroundWorker.ReportProgress(-1, string.Format("{0}%", i));

                System.Threading.Thread.Sleep(rand.Next(100, 1000));
                if (this.simulateError)
                {
                }
            }
        }

        private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is String)
            {
                this.labelProgress.Text = (String)e.UserState;
            }
        }

        private void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.pictureBox.Image = null;
            if (e.Cancelled)
            {
            }
            else
            {
                if (e.Error != null)
                {
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}