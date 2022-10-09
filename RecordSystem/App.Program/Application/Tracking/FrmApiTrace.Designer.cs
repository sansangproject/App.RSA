namespace RecordSystemApplication.App.Program.Application.Post
{
    partial class FrmApiTrace
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
            this.txtTrackCode = new System.Windows.Forms.TextBox();
            this.btnGetData = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTrackCode
            // 
            this.txtTrackCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.txtTrackCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtTrackCode.Location = new System.Drawing.Point(86, 39);
            this.txtTrackCode.Name = "txtTrackCode";
            this.txtTrackCode.Size = new System.Drawing.Size(169, 26);
            this.txtTrackCode.TabIndex = 29;
            this.txtTrackCode.Text = "RX273519273TH";
            this.txtTrackCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(270, 39);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(79, 28);
            this.btnGetData.TabIndex = 26;
            this.btnGetData.Text = "Search";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(470, 48);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(79, 28);
            this.btnUpdate.TabIndex = 30;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // FrmApiTrace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtTrackCode);
            this.Controls.Add(this.btnGetData);
            this.Name = "FrmApiTrace";
            this.Text = "FrmApiTrace";
            this.Load += new System.EventHandler(this.FrmApiTrace_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTrackCode;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.Button btnUpdate;
    }
}