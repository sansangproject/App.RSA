﻿namespace SANSANG
{
    partial class FrmWelcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWelcome));
            this.pgbWelcome = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pgbWelcome)).BeginInit();
            this.SuspendLayout();
            // 
            // pgbWelcome
            // 
            this.pgbWelcome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgbWelcome.Image = ((System.Drawing.Image)(resources.GetObject("pgbWelcome.Image")));
            this.pgbWelcome.Location = new System.Drawing.Point(0, 0);
            this.pgbWelcome.Name = "pgbWelcome";
            this.pgbWelcome.Size = new System.Drawing.Size(898, 613);
            this.pgbWelcome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pgbWelcome.TabIndex = 0;
            this.pgbWelcome.TabStop = false;
            // 
            // FrmWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 613);
            this.Controls.Add(this.pgbWelcome);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmWelcome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pgbWelcome)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pgbWelcome;
    }
}