namespace SANSANG.Utilites.App.Forms
{
    partial class FrmMessagesBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMessagesBox));
            this.btnNo = new DevComponents.DotNetBar.ButtonX();
            this.btnYes = new DevComponents.DotNetBar.ButtonX();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.pbMessage = new System.Windows.Forms.PictureBox();
            this.txtMessageHead = new System.Windows.Forms.TextBox();
            this.txtMessageDetail = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMessage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNo
            // 
            this.btnNo.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.btnNo.BackColor = System.Drawing.SystemColors.Control;
            this.btnNo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNo.Font = new System.Drawing.Font("Mitr", 8.249999F);
            this.btnNo.Location = new System.Drawing.Point(333, 147);
            this.btnNo.Name = "btnNo";
            this.btnNo.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(0, 15, 15, 0);
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnNo.TabIndex = 0;
            this.btnNo.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnYes
            // 
            this.btnYes.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.btnYes.BackColor = System.Drawing.SystemColors.Control;
            this.btnYes.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnYes.Font = new System.Drawing.Font("Mitr", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.Location = new System.Drawing.Point(252, 147);
            this.btnYes.Name = "btnYes";
            this.btnYes.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(0, 15, 15, 0);
            this.btnYes.Size = new System.Drawing.Size(75, 23);
            this.btnYes.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.btnYes.TabIndex = 0;
            this.btnYes.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
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
            this.pbMessage.Location = new System.Drawing.Point(28, 63);
            this.pbMessage.Name = "pbMessage";
            this.pbMessage.Size = new System.Drawing.Size(73, 69);
            this.pbMessage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbMessage.TabIndex = 2;
            this.pbMessage.TabStop = false;
            // 
            // txtMessageHead
            // 
            this.txtMessageHead.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessageHead.Font = new System.Drawing.Font("Mitr", 10F);
            this.txtMessageHead.Location = new System.Drawing.Point(107, 26);
            this.txtMessageHead.Multiline = true;
            this.txtMessageHead.Name = "txtMessageHead";
            this.txtMessageHead.Size = new System.Drawing.Size(284, 47);
            this.txtMessageHead.TabIndex = 4;
            // 
            // txtMessageDetail
            // 
            this.txtMessageDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMessageDetail.Font = new System.Drawing.Font("Mitr Light", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessageDetail.Location = new System.Drawing.Point(107, 64);
            this.txtMessageDetail.Multiline = true;
            this.txtMessageDetail.Name = "txtMessageDetail";
            this.txtMessageDetail.Size = new System.Drawing.Size(301, 68);
            this.txtMessageDetail.TabIndex = 5;
            // 
            // FrmMessagesBox
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
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.btnNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMessagesBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmMessagesBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbMessage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnNo;
        private DevComponents.DotNetBar.ButtonX btnYes;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private System.Windows.Forms.PictureBox pbMessage;
        private System.Windows.Forms.TextBox txtMessageHead;
        private System.Windows.Forms.TextBox txtMessageDetail;
    }
}