namespace SANSANG
{
    partial class FrmShowImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShowImage));
            this.pbShowPic = new System.Windows.Forms.PictureBox();
            this.pbZoom = new System.Windows.Forms.PictureBox();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtImageCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trbZoom = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbZoom)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // pbShowPic
            // 
            this.pbShowPic.BackColor = System.Drawing.Color.White;
            this.pbShowPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbShowPic.Image = ((System.Drawing.Image)(resources.GetObject("pbShowPic.Image")));
            this.pbShowPic.Location = new System.Drawing.Point(0, 0);
            this.pbShowPic.Name = "pbShowPic";
            this.pbShowPic.Size = new System.Drawing.Size(788, 440);
            this.pbShowPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbShowPic.TabIndex = 0;
            this.pbShowPic.TabStop = false;
            // 
            // pbZoom
            // 
            this.pbZoom.Location = new System.Drawing.Point(15, 15);
            this.pbZoom.Name = "pbZoom";
            this.pbZoom.Size = new System.Drawing.Size(760, 408);
            this.pbZoom.TabIndex = 1;
            this.pbZoom.TabStop = false;
            this.pbZoom.Visible = false;
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.BackColor = System.Drawing.Color.Transparent;
            this.btnZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomIn.ForeColor = System.Drawing.Color.Transparent;
            this.btnZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomIn.Image")));
            this.btnZoomIn.Location = new System.Drawing.Point(717, 531);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(48, 45);
            this.btnZoomIn.TabIndex = 2;
            this.btnZoomIn.UseVisualStyleBackColor = false;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.BackColor = System.Drawing.Color.Transparent;
            this.btnZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnZoomOut.ForeColor = System.Drawing.Color.White;
            this.btnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomOut.Image")));
            this.btnZoomOut.Location = new System.Drawing.Point(780, 531);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(48, 45);
            this.btnZoomOut.TabIndex = 2;
            this.btnZoomOut.UseVisualStyleBackColor = false;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.White;
            this.panel.Controls.Add(this.pbShowPic);
            this.panel.Controls.Add(this.pbZoom);
            this.panel.Location = new System.Drawing.Point(51, 85);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(788, 440);
            this.panel.TabIndex = 3;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(654, 531);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(48, 45);
            this.btnBack.TabIndex = 2;
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(886, 618);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(597, 531);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(41, 45);
            this.btnSave.TabIndex = 2;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(291, 550);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(36, 31);
            this.btnSearch.TabIndex = 107;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtImageCode
            // 
            this.txtImageCode.BackColor = System.Drawing.Color.White;
            this.txtImageCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtImageCode.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.txtImageCode.ForeColor = System.Drawing.Color.DimGray;
            this.txtImageCode.Location = new System.Drawing.Point(66, 555);
            this.txtImageCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtImageCode.Name = "txtImageCode";
            this.txtImageCode.Size = new System.Drawing.Size(211, 21);
            this.txtImageCode.TabIndex = 106;
            this.txtImageCode.TextChanged += new System.EventHandler(this.txtImageCode_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(60, 550);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 31);
            this.label1.TabIndex = 108;
            this.label1.Text = "22222222222222";
            // 
            // trbZoom
            // 
            this.trbZoom.BackColor = System.Drawing.Color.White;
            this.trbZoom.LargeChange = 1;
            this.trbZoom.Location = new System.Drawing.Point(344, 546);
            this.trbZoom.Maximum = 20;
            this.trbZoom.Name = "trbZoom";
            this.trbZoom.Size = new System.Drawing.Size(225, 45);
            this.trbZoom.TabIndex = 109;
            this.trbZoom.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trbZoom.Scroll += new System.EventHandler(this.trbZoom_Scroll);
            // 
            // FrmShowImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(886, 611);
            this.Controls.Add(this.trbZoom);
            this.Controls.Add(this.txtImageCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnZoomIn);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmShowImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmShowImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbShowPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbZoom)).EndInit();
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbShowPic;
        private System.Windows.Forms.PictureBox pbZoom;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtImageCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trbZoom;
    }
}