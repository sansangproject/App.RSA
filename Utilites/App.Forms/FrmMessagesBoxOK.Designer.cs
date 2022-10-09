namespace SANSANG.Utilites.App.Forms
{
    partial class FrmMessagesBoxOK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMessagesBoxOK));
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.pbMessage = new System.Windows.Forms.PictureBox();
            this.txtMessageHead = new System.Windows.Forms.TextBox();
            this.txtMessageDetail = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Font = new System.Drawing.Font("Mitr", 8.249999F);
            this.btnOK.Location = new System.Drawing.Point(333, 147);
            this.btnOK.Name = "btnOK";
            this.btnOK.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(0, 15, 15, 0);
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnOK.TabIndex = 0;
            this.btnOK.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnClose.Location = new System.Drawing.Point(397, 24);
            this.btnClose.Name = "btnClose";
            this.btnClose.Shape = new DevComponents.DotNetBar.EllipticalShapeDescriptor();
            this.btnClose.Size = new System.Drawing.Size(20, 19);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "x";
            this.btnClose.TextColor = System.Drawing.Color.Gray;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pbMessage
            // 
            this.pbMessage.BackColor = System.Drawing.Color.Transparent;
            this.pbMessage.Location = new System.Drawing.Point(28, 22);
            this.pbMessage.Name = "pbMessage";
            this.pbMessage.Size = new System.Drawing.Size(73, 69);
            this.pbMessage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMessage.TabIndex = 2;
            this.pbMessage.TabStop = false;
            // 
            // txtMessageHead
            // 
            this.txtMessageHead.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessageHead.Font = new System.Drawing.Font("Mitr", 10F);
            this.txtMessageHead.Location = new System.Drawing.Point(119, 28);
            this.txtMessageHead.Multiline = true;
            this.txtMessageHead.Name = "txtMessageHead";
            this.txtMessageHead.Size = new System.Drawing.Size(272, 19);
            this.txtMessageHead.TabIndex = 4;
            // 
            // txtMessageDetail
            // 
            this.txtMessageDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessageDetail.Font = new System.Drawing.Font("Mitr Light", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessageDetail.Location = new System.Drawing.Point(119, 53);
            this.txtMessageDetail.Multiline = true;
            this.txtMessageDetail.Name = "txtMessageDetail";
            this.txtMessageDetail.Size = new System.Drawing.Size(289, 73);
            this.txtMessageDetail.TabIndex = 5;
            // 
            // FrmMessagesBoxOK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(439, 198);
            this.Controls.Add(this.txtMessageDetail);
            this.Controls.Add(this.txtMessageHead);
            this.Controls.Add(this.pbMessage);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMessagesBoxOK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmMessagesBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbMessage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private System.Windows.Forms.PictureBox pbMessage;
        private System.Windows.Forms.TextBox txtMessageHead;
        private System.Windows.Forms.TextBox txtMessageDetail;
    }
}