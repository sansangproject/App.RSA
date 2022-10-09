namespace SANSANG
{
    partial class FrmDashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDashboard));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslName = new System.Windows.Forms.ToolStripStatusLabel();
            this.MenuItem11 = new Telerik.WinControls.UI.RadMenuItem();
            this.MenuItem12 = new Telerik.WinControls.UI.RadMenuItem();
            this.MenuItem13 = new Telerik.WinControls.UI.RadMenuItem();
            this.MenuItem14 = new Telerik.WinControls.UI.RadMenuItem();
            this.RD = new Telerik.WinControls.UI.RadMenuItem();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.tsslName});
            this.statusStrip.Location = new System.Drawing.Point(0, 677);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 15, 0);
            this.statusStrip.Size = new System.Drawing.Size(1354, 25);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // StatusLabel
            // 
            this.StatusLabel.BackColor = System.Drawing.Color.Black;
            this.StatusLabel.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.StatusLabel.ForeColor = System.Drawing.Color.White;
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(48, 20);
            this.StatusLabel.Text = "Ready";
            // 
            // tsslName
            // 
            this.tsslName.BackColor = System.Drawing.Color.White;
            this.tsslName.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tsslName.ForeColor = System.Drawing.Color.Black;
            this.tsslName.Name = "tsslName";
            this.tsslName.Size = new System.Drawing.Size(38, 20);
            this.tsslName.Text = "User";
            // 
            // MenuItem11
            // 
            this.MenuItem11.AccessibleDescription = "radMenuItem1";
            this.MenuItem11.AccessibleName = "radMenuItem1";
            this.MenuItem11.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.MenuItem11.Name = "MenuItem11";
            this.MenuItem11.Text = "";
            this.MenuItem11.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // MenuItem12
            // 
            this.MenuItem12.AccessibleDescription = "radMenuItem2";
            this.MenuItem12.AccessibleName = "radMenuItem2";
            this.MenuItem12.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.MenuItem12.Name = "MenuItem12";
            this.MenuItem12.Text = "";
            this.MenuItem12.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // MenuItem13
            // 
            this.MenuItem13.AccessibleDescription = "radMenuItem3";
            this.MenuItem13.AccessibleName = "radMenuItem3";
            this.MenuItem13.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.MenuItem13.Name = "MenuItem13";
            this.MenuItem13.Text = "";
            this.MenuItem13.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // MenuItem14
            // 
            this.MenuItem14.AccessibleDescription = "radMenuItem4";
            this.MenuItem14.AccessibleName = "radMenuItem4";
            this.MenuItem14.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.MenuItem14.Name = "MenuItem14";
            this.MenuItem14.Text = "";
            this.MenuItem14.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // RD
            // 
            this.RD.AccessibleDescription = "RD";
            this.RD.AccessibleName = "RD";
            this.RD.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.RD.Name = "RD";
            this.RD.Text = "";
            this.RD.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1354, 702);
            this.Controls.Add(this.statusStrip);
            this.Font = new System.Drawing.Font("Mitr Light", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmDashboard_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel tsslName;
        private Telerik.WinControls.UI.RadMenuItem MenuItem11;
        private Telerik.WinControls.UI.RadMenuItem MenuItem12;
        private Telerik.WinControls.UI.RadMenuItem MenuItem13;
        private Telerik.WinControls.UI.RadMenuItem MenuItem14;
        private Telerik.WinControls.UI.RadMenuItem RD;
    }
}



