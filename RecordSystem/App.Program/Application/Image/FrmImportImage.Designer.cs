namespace SANSANG
{
    partial class FrmImportImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImportImage));
            this.btnBrowser = new System.Windows.Forms.Button();
            this.txtBrowser = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbbStatus = new System.Windows.Forms.ComboBox();
            this.cbDeleteImageFrom = new System.Windows.Forms.CheckBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.txtImageFromSize = new System.Windows.Forms.TextBox();
            this.txtImageFromName = new System.Windows.Forms.TextBox();
            this.txtImageFromType = new System.Windows.Forms.TextBox();
            this.pbImageStart = new System.Windows.Forms.PictureBox();
            this.cbbImageTo = new System.Windows.Forms.ComboBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtImageToRef = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtImageToLocations = new System.Windows.Forms.TextBox();
            this.txtImageToCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtImageToSize = new System.Windows.Forms.TextBox();
            this.txtImageToName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtImageToType = new System.Windows.Forms.TextBox();
            this.pbImageEnd = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.gbBefor = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnMove = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.gbAfter = new System.Windows.Forms.GroupBox();
            this.pbSProductBarcode = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbImageStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImageEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbBefor.SuspendLayout();
            this.gbAfter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSProductBarcode)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBrowser
            // 
            this.btnBrowser.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowser.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnBrowser.Image = ((System.Drawing.Image)(resources.GetObject("btnBrowser.Image")));
            this.btnBrowser.Location = new System.Drawing.Point(260, 21);
            this.btnBrowser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(27, 33);
            this.btnBrowser.TabIndex = 3;
            this.btnBrowser.UseVisualStyleBackColor = false;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // txtBrowser
            // 
            this.txtBrowser.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtBrowser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBrowser.Enabled = false;
            this.txtBrowser.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.txtBrowser.ForeColor = System.Drawing.Color.OliveDrab;
            this.txtBrowser.Location = new System.Drawing.Point(10, 154);
            this.txtBrowser.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBrowser.Name = "txtBrowser";
            this.txtBrowser.Size = new System.Drawing.Size(133, 21);
            this.txtBrowser.TabIndex = 2;
            this.txtBrowser.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label8.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.label8.Location = new System.Drawing.Point(11, 141);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 19);
            this.label8.TabIndex = 110;
            this.label8.Text = "Status :";
            // 
            // cbbStatus
            // 
            this.cbbStatus.BackColor = System.Drawing.Color.Gainsboro;
            this.cbbStatus.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.cbbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.cbbStatus.FormattingEnabled = true;
            this.cbbStatus.Location = new System.Drawing.Point(82, 139);
            this.cbbStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbbStatus.Name = "cbbStatus";
            this.cbbStatus.Size = new System.Drawing.Size(167, 28);
            this.cbbStatus.TabIndex = 4;
            // 
            // cbDeleteImageFrom
            // 
            this.cbDeleteImageFrom.AutoSize = true;
            this.cbDeleteImageFrom.BackColor = System.Drawing.Color.Transparent;
            this.cbDeleteImageFrom.Checked = true;
            this.cbDeleteImageFrom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDeleteImageFrom.ForeColor = System.Drawing.Color.DimGray;
            this.cbDeleteImageFrom.Location = new System.Drawing.Point(83, 181);
            this.cbDeleteImageFrom.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDeleteImageFrom.Name = "cbDeleteImageFrom";
            this.cbDeleteImageFrom.Size = new System.Drawing.Size(166, 24);
            this.cbDeleteImageFrom.TabIndex = 109;
            this.cbDeleteImageFrom.Text = "Delete Image Master ";
            this.cbDeleteImageFrom.UseVisualStyleBackColor = false;
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.Transparent;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.Location = new System.Drawing.Point(316, 21);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(27, 33);
            this.btnImport.TabIndex = 4;
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Visible = false;
            this.btnImport.Click += new System.EventHandler(this.btnImageFromSubmit_Click);
            // 
            // txtImageFromSize
            // 
            this.txtImageFromSize.BackColor = System.Drawing.Color.Gainsboro;
            this.txtImageFromSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtImageFromSize.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.txtImageFromSize.ForeColor = System.Drawing.Color.DimGray;
            this.txtImageFromSize.Location = new System.Drawing.Point(83, 107);
            this.txtImageFromSize.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtImageFromSize.Name = "txtImageFromSize";
            this.txtImageFromSize.Size = new System.Drawing.Size(161, 21);
            this.txtImageFromSize.TabIndex = 107;
            // 
            // txtImageFromName
            // 
            this.txtImageFromName.BackColor = System.Drawing.Color.Gainsboro;
            this.txtImageFromName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtImageFromName.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.txtImageFromName.ForeColor = System.Drawing.Color.DimGray;
            this.txtImageFromName.Location = new System.Drawing.Point(83, 33);
            this.txtImageFromName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtImageFromName.Name = "txtImageFromName";
            this.txtImageFromName.Size = new System.Drawing.Size(161, 21);
            this.txtImageFromName.TabIndex = 103;
            this.txtImageFromName.TextChanged += new System.EventHandler(this.txtImageFromName_TextChanged);
            // 
            // txtImageFromType
            // 
            this.txtImageFromType.BackColor = System.Drawing.Color.Gainsboro;
            this.txtImageFromType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtImageFromType.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.txtImageFromType.ForeColor = System.Drawing.Color.DimGray;
            this.txtImageFromType.Location = new System.Drawing.Point(83, 71);
            this.txtImageFromType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtImageFromType.Name = "txtImageFromType";
            this.txtImageFromType.Size = new System.Drawing.Size(161, 21);
            this.txtImageFromType.TabIndex = 105;
            // 
            // pbImageStart
            // 
            this.pbImageStart.BackColor = System.Drawing.SystemColors.Window;
            this.pbImageStart.Image = ((System.Drawing.Image)(resources.GetObject("pbImageStart.Image")));
            this.pbImageStart.Location = new System.Drawing.Point(100, 12);
            this.pbImageStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbImageStart.Name = "pbImageStart";
            this.pbImageStart.Size = new System.Drawing.Size(217, 164);
            this.pbImageStart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImageStart.TabIndex = 0;
            this.pbImageStart.TabStop = false;
            // 
            // cbbImageTo
            // 
            this.cbbImageTo.BackColor = System.Drawing.Color.Gainsboro;
            this.cbbImageTo.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.cbbImageTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.cbbImageTo.FormattingEnabled = true;
            this.cbbImageTo.Location = new System.Drawing.Point(89, 22);
            this.cbbImageTo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbbImageTo.Name = "cbbImageTo";
            this.cbbImageTo.Size = new System.Drawing.Size(247, 28);
            this.cbbImageTo.TabIndex = 3;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.White;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.Transparent;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(684, 6);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(33, 33);
            this.btnExit.TabIndex = 107;
            this.btnExit.Tag = "ออก";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Transparent;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnOk.Image = ((System.Drawing.Image)(resources.GetObject("btnOk.Image")));
            this.btnOk.Location = new System.Drawing.Point(313, 61);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(23, 36);
            this.btnOk.TabIndex = 106;
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(286, 61);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(26, 37);
            this.btnSearch.TabIndex = 105;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label7.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.label7.Location = new System.Drawing.Point(11, 208);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 19);
            this.label7.TabIndex = 104;
            this.label7.Text = "Ref Id:";
            // 
            // txtImageToRef
            // 
            this.txtImageToRef.BackColor = System.Drawing.Color.Gainsboro;
            this.txtImageToRef.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtImageToRef.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.txtImageToRef.ForeColor = System.Drawing.Color.DimGray;
            this.txtImageToRef.Location = new System.Drawing.Point(89, 225);
            this.txtImageToRef.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtImageToRef.Name = "txtImageToRef";
            this.txtImageToRef.Size = new System.Drawing.Size(136, 21);
            this.txtImageToRef.TabIndex = 103;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label18.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.label18.Location = new System.Drawing.Point(11, 24);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(65, 19);
            this.label18.TabIndex = 102;
            this.label18.Text = "Location :";
            // 
            // txtImageToLocations
            // 
            this.txtImageToLocations.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtImageToLocations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtImageToLocations.Enabled = false;
            this.txtImageToLocations.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.txtImageToLocations.ForeColor = System.Drawing.Color.OliveDrab;
            this.txtImageToLocations.Location = new System.Drawing.Point(372, 159);
            this.txtImageToLocations.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtImageToLocations.Name = "txtImageToLocations";
            this.txtImageToLocations.Size = new System.Drawing.Size(160, 21);
            this.txtImageToLocations.TabIndex = 101;
            this.txtImageToLocations.TabStop = false;
            // 
            // txtImageToCode
            // 
            this.txtImageToCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtImageToCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtImageToCode.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.txtImageToCode.ForeColor = System.Drawing.Color.DimGray;
            this.txtImageToCode.Location = new System.Drawing.Point(89, 68);
            this.txtImageToCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtImageToCode.Name = "txtImageToCode";
            this.txtImageToCode.Size = new System.Drawing.Size(196, 21);
            this.txtImageToCode.TabIndex = 93;
            this.txtImageToCode.TextChanged += new System.EventHandler(this.txtImageToCode_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label1.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.label1.Location = new System.Drawing.Point(11, 171);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 19);
            this.label1.TabIndex = 100;
            this.label1.Text = "Size :";
            // 
            // txtImageToSize
            // 
            this.txtImageToSize.BackColor = System.Drawing.Color.Gainsboro;
            this.txtImageToSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtImageToSize.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.txtImageToSize.ForeColor = System.Drawing.Color.DimGray;
            this.txtImageToSize.Location = new System.Drawing.Point(89, 185);
            this.txtImageToSize.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtImageToSize.Name = "txtImageToSize";
            this.txtImageToSize.Size = new System.Drawing.Size(136, 21);
            this.txtImageToSize.TabIndex = 99;
            // 
            // txtImageToName
            // 
            this.txtImageToName.BackColor = System.Drawing.Color.Gainsboro;
            this.txtImageToName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtImageToName.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.txtImageToName.ForeColor = System.Drawing.Color.DimGray;
            this.txtImageToName.Location = new System.Drawing.Point(89, 108);
            this.txtImageToName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtImageToName.Name = "txtImageToName";
            this.txtImageToName.Size = new System.Drawing.Size(242, 21);
            this.txtImageToName.TabIndex = 95;
            this.txtImageToName.TextChanged += new System.EventHandler(this.txtImageToName_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.lblName.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.lblName.Location = new System.Drawing.Point(11, 129);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(43, 19);
            this.lblName.TabIndex = 98;
            this.lblName.Text = "Type :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label2.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.label2.Location = new System.Drawing.Point(11, 94);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 19);
            this.label2.TabIndex = 96;
            this.label2.Text = "Name :";
            // 
            // txtImageToType
            // 
            this.txtImageToType.BackColor = System.Drawing.Color.Gainsboro;
            this.txtImageToType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtImageToType.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.txtImageToType.ForeColor = System.Drawing.Color.DimGray;
            this.txtImageToType.Location = new System.Drawing.Point(89, 145);
            this.txtImageToType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtImageToType.Name = "txtImageToType";
            this.txtImageToType.Size = new System.Drawing.Size(136, 21);
            this.txtImageToType.TabIndex = 97;
            // 
            // pbImageEnd
            // 
            this.pbImageEnd.BackColor = System.Drawing.SystemColors.Window;
            this.pbImageEnd.Image = ((System.Drawing.Image)(resources.GetObject("pbImageEnd.Image")));
            this.pbImageEnd.Location = new System.Drawing.Point(457, 14);
            this.pbImageEnd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pbImageEnd.Name = "pbImageEnd";
            this.pbImageEnd.Size = new System.Drawing.Size(215, 164);
            this.pbImageEnd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImageEnd.TabIndex = 1;
            this.pbImageEnd.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-29, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(786, 507);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // gbBefor
            // 
            this.gbBefor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gbBefor.Controls.Add(this.label3);
            this.gbBefor.Controls.Add(this.label5);
            this.gbBefor.Controls.Add(this.label13);
            this.gbBefor.Controls.Add(this.label6);
            this.gbBefor.Controls.Add(this.cbbStatus);
            this.gbBefor.Controls.Add(this.btnMove);
            this.gbBefor.Controls.Add(this.btnBrowser);
            this.gbBefor.Controls.Add(this.label14);
            this.gbBefor.Controls.Add(this.txtImageFromName);
            this.gbBefor.Controls.Add(this.label15);
            this.gbBefor.Controls.Add(this.txtImageFromType);
            this.gbBefor.Controls.Add(this.label16);
            this.gbBefor.Controls.Add(this.txtImageFromSize);
            this.gbBefor.Controls.Add(this.label8);
            this.gbBefor.Controls.Add(this.cbDeleteImageFrom);
            this.gbBefor.Controls.Add(this.btnImport);
            this.gbBefor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.gbBefor.Location = new System.Drawing.Point(10, 186);
            this.gbBefor.Name = "gbBefor";
            this.gbBefor.Size = new System.Drawing.Size(349, 260);
            this.gbBefor.TabIndex = 111;
            this.gbBefor.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(12, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 19);
            this.label3.TabIndex = 115;
            this.label3.Text = "ชื่อภาพ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(12, 72);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 19);
            this.label5.TabIndex = 114;
            this.label5.Text = "ประเภทภาพ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label13.ForeColor = System.Drawing.Color.Gray;
            this.label13.Location = new System.Drawing.Point(11, 157);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 19);
            this.label13.TabIndex = 110;
            this.label13.Text = "สถานะ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(12, 111);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 19);
            this.label6.TabIndex = 113;
            this.label6.Text = "ขนาด";
            // 
            // btnMove
            // 
            this.btnMove.BackColor = System.Drawing.Color.Transparent;
            this.btnMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMove.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnMove.Image = ((System.Drawing.Image)(resources.GetObject("btnMove.Image")));
            this.btnMove.Location = new System.Drawing.Point(285, 21);
            this.btnMove.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(28, 33);
            this.btnMove.TabIndex = 3;
            this.btnMove.UseVisualStyleBackColor = false;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label14.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.label14.Location = new System.Drawing.Point(12, 17);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 19);
            this.label14.TabIndex = 110;
            this.label14.Text = "Name :";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label15.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.label15.Location = new System.Drawing.Point(12, 94);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(40, 19);
            this.label15.TabIndex = 112;
            this.label15.Text = "Size :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label16.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.label16.Location = new System.Drawing.Point(12, 52);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(43, 19);
            this.label16.TabIndex = 111;
            this.label16.Text = "Type :";
            // 
            // gbAfter
            // 
            this.gbAfter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gbAfter.Controls.Add(this.pbSProductBarcode);
            this.gbAfter.Controls.Add(this.label17);
            this.gbAfter.Controls.Add(this.label19);
            this.gbAfter.Controls.Add(this.label12);
            this.gbAfter.Controls.Add(this.label11);
            this.gbAfter.Controls.Add(this.btnSearch);
            this.gbAfter.Controls.Add(this.btnOk);
            this.gbAfter.Controls.Add(this.label10);
            this.gbAfter.Controls.Add(this.txtImageToCode);
            this.gbAfter.Controls.Add(this.label9);
            this.gbAfter.Controls.Add(this.label4);
            this.gbAfter.Controls.Add(this.txtImageToRef);
            this.gbAfter.Controls.Add(this.label7);
            this.gbAfter.Controls.Add(this.cbbImageTo);
            this.gbAfter.Controls.Add(this.label18);
            this.gbAfter.Controls.Add(this.label2);
            this.gbAfter.Controls.Add(this.txtImageToType);
            this.gbAfter.Controls.Add(this.label1);
            this.gbAfter.Controls.Add(this.txtImageToName);
            this.gbAfter.Controls.Add(this.txtImageToSize);
            this.gbAfter.Controls.Add(this.lblName);
            this.gbAfter.Location = new System.Drawing.Point(368, 186);
            this.gbAfter.Name = "gbAfter";
            this.gbAfter.Size = new System.Drawing.Size(342, 260);
            this.gbAfter.TabIndex = 112;
            this.gbAfter.TabStop = false;
            // 
            // pbSProductBarcode
            // 
            this.pbSProductBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSProductBarcode.BackColor = System.Drawing.Color.Transparent;
            this.pbSProductBarcode.Location = new System.Drawing.Point(250, 171);
            this.pbSProductBarcode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbSProductBarcode.Name = "pbSProductBarcode";
            this.pbSProductBarcode.Size = new System.Drawing.Size(81, 79);
            this.pbSProductBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSProductBarcode.TabIndex = 113;
            this.pbSProductBarcode.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label17.ForeColor = System.Drawing.Color.Gray;
            this.label17.Location = new System.Drawing.Point(11, 77);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(50, 19);
            this.label17.TabIndex = 111;
            this.label17.Text = "รหัสภาพ";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label19.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.label19.Location = new System.Drawing.Point(11, 59);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(44, 19);
            this.label19.TabIndex = 110;
            this.label19.Text = "Code :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label12.ForeColor = System.Drawing.Color.Gray;
            this.label12.Location = new System.Drawing.Point(11, 40);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 19);
            this.label12.TabIndex = 109;
            this.label12.Text = "ตำแหน่ง";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label11.ForeColor = System.Drawing.Color.Gray;
            this.label11.Location = new System.Drawing.Point(11, 111);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 19);
            this.label11.TabIndex = 108;
            this.label11.Text = "ชื่อภาพ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label10.ForeColor = System.Drawing.Color.Gray;
            this.label10.Location = new System.Drawing.Point(11, 149);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 19);
            this.label10.TabIndex = 107;
            this.label10.Text = "ประเภทภาพ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(11, 188);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 19);
            this.label9.TabIndex = 106;
            this.label9.Text = "ขนาด";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(11, 226);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 19);
            this.label4.TabIndex = 105;
            this.label4.Text = "รหัสอ้างอิง";
            // 
            // FrmImportImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(721, 458);
            this.Controls.Add(this.gbAfter);
            this.Controls.Add(this.gbBefor);
            this.Controls.Add(this.pbImageEnd);
            this.Controls.Add(this.pbImageStart);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtImageToLocations);
            this.Controls.Add(this.txtBrowser);
            this.Controls.Add(this.btnExit);
            this.Font = new System.Drawing.Font("Mitr Light", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmImportImage";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmImportImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImageStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbImageEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbBefor.ResumeLayout(false);
            this.gbBefor.PerformLayout();
            this.gbAfter.ResumeLayout(false);
            this.gbAfter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSProductBarcode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox txtBrowser;
        private System.Windows.Forms.PictureBox pbImageStart;
        private System.Windows.Forms.ComboBox cbbImageTo;
        private System.Windows.Forms.PictureBox pbImageEnd;
        private System.Windows.Forms.TextBox txtImageToCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtImageToName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtImageToType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtImageToSize;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtImageToLocations;
        private System.Windows.Forms.CheckBox cbDeleteImageFrom;
        private System.Windows.Forms.TextBox txtImageFromSize;
        private System.Windows.Forms.TextBox txtImageFromName;
        private System.Windows.Forms.TextBox txtImageFromType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtImageToRef;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbbStatus;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox gbBefor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox gbAfter;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.PictureBox pbSProductBarcode;
    }
}