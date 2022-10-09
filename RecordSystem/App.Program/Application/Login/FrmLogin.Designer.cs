namespace SANSANG
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.pbLogin = new System.Windows.Forms.PictureBox();
            this.btnLogin = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLogin
            // 
            this.pbLogin.Image = ((System.Drawing.Image)(resources.GetObject("pbLogin.Image")));
            this.pbLogin.Location = new System.Drawing.Point(0, 0);
            this.pbLogin.Name = "pbLogin";
            this.pbLogin.Size = new System.Drawing.Size(572, 395);
            this.pbLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbLogin.TabIndex = 0;
            this.pbLogin.TabStop = false;
            // 
            // btnLogin
            // 
            this.btnLogin.AutoSize = true;
            this.btnLogin.BackColor = System.Drawing.Color.Silver;
            this.btnLogin.Font = new System.Drawing.Font("Mitr", 9F);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(333, 264);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(54, 19);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "  Login ";
            this.btnLogin.Click += new System.EventHandler(this.Login);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.BackColor = System.Drawing.Color.LightGray;
            this.btnClose.Font = new System.Drawing.Font("Mitr", 6F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(445, 66);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(11, 12);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.Close);
            // 
            // txtUser
            // 
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUser.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.Location = new System.Drawing.Point(216, 157);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(170, 21);
            this.txtUser.TabIndex = 1;
            this.txtUser.Text = "SANSANG.ADMIN";
            // 
            // txtPass
            // 
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPass.ForeColor = System.Drawing.Color.DarkGreen;
            this.txtPass.Location = new System.Drawing.Point(216, 200);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(173, 17);
            this.txtPass.TabIndex = 2;
            this.txtPass.Text = "sansangp@$$w0rd";
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 395);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.pbLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogin;
        private System.Windows.Forms.Label btnLogin;
        private System.Windows.Forms.Label btnClose;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
    }
}