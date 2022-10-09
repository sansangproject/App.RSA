namespace SANSANG
{
    partial class FrmManageBranch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManageBranch));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNameEn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbBank = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtNameTh = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cbbStatus = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblHSearch = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblCount = new System.Windows.Forms.TextBox();
            this.picExcel = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.gbFrm = new System.Windows.Forms.GroupBox();
            this.pbBarcode = new System.Windows.Forms.PictureBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtDay = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.pbBanner = new System.Windows.Forms.PictureBox();
            this.lblProTH = new System.Windows.Forms.TextBox();
            this.lblProEN = new System.Windows.Forms.TextBox();
            this.cbbProvince = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.gbFrm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBarcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.txtAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtAddress.Location = new System.Drawing.Point(774, 59);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(373, 91);
            this.txtAddress.TabIndex = 25;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label2.ForeColor = System.Drawing.Color.OliveDrab;
            this.label2.Location = new System.Drawing.Point(681, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 19);
            this.label2.TabIndex = 27;
            this.label2.Text = "ที่อยู่:";
            // 
            // txtNameEn
            // 
            this.txtNameEn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNameEn.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.txtNameEn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNameEn.Location = new System.Drawing.Point(262, 200);
            this.txtNameEn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNameEn.Name = "txtNameEn";
            this.txtNameEn.Size = new System.Drawing.Size(273, 19);
            this.txtNameEn.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label3.ForeColor = System.Drawing.Color.OliveDrab;
            this.label3.Location = new System.Drawing.Point(161, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 19);
            this.label3.TabIndex = 23;
            this.label3.Text = "ธนาคาร:";
            // 
            // cbbBank
            // 
            this.cbbBank.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbBank.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.cbbBank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbbBank.FormattingEnabled = true;
            this.cbbBank.Location = new System.Drawing.Point(260, 91);
            this.cbbBank.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbBank.Name = "cbbBank";
            this.cbbBank.Size = new System.Drawing.Size(273, 27);
            this.cbbBank.TabIndex = 24;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.btnSearch.ForeColor = System.Drawing.Color.Transparent;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(408, 56);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(20, 22);
            this.btnSearch.TabIndex = 21;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtNameTh
            // 
            this.txtNameTh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNameTh.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.txtNameTh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNameTh.Location = new System.Drawing.Point(262, 174);
            this.txtNameTh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNameTh.Name = "txtNameTh";
            this.txtNameTh.Size = new System.Drawing.Size(273, 19);
            this.txtNameTh.TabIndex = 15;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.lblName.ForeColor = System.Drawing.Color.OliveDrab;
            this.lblName.Location = new System.Drawing.Point(160, 168);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(49, 19);
            this.lblName.TabIndex = 16;
            this.lblName.Text = "ชื่อสาขา:";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.lblID.ForeColor = System.Drawing.Color.OliveDrab;
            this.lblID.Location = new System.Drawing.Point(154, 58);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(55, 19);
            this.lblID.TabIndex = 10;
            this.lblID.Text = "รหัสสาขา:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.OliveDrab;
            this.lblStatus.Location = new System.Drawing.Point(670, 200);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(41, 19);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.Text = "สถานะ:";
            // 
            // cbbStatus
            // 
            this.cbbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbStatus.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.cbbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbbStatus.FormattingEnabled = true;
            this.cbbStatus.Location = new System.Drawing.Point(774, 191);
            this.cbbStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbStatus.Name = "cbbStatus";
            this.cbbStatus.Size = new System.Drawing.Size(174, 27);
            this.cbbStatus.TabIndex = 20;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblSearch.Location = new System.Drawing.Point(71, 19);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(12, 17);
            this.lblSearch.TabIndex = 41;
            this.lblSearch.Text = " ";
            // 
            // lblHSearch
            // 
            this.lblHSearch.AutoSize = true;
            this.lblHSearch.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.lblHSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblHSearch.Location = new System.Drawing.Point(29, 82);
            this.lblHSearch.Name = "lblHSearch";
            this.lblHSearch.Size = new System.Drawing.Size(47, 19);
            this.lblHSearch.TabIndex = 41;
            this.lblHSearch.Text = "คำค้นหา";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Mitr Light", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DarkSeaGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dataGridView.Location = new System.Drawing.Point(19, 46);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1225, 210);
            this.dataGridView.TabIndex = 40;
            this.dataGridView.TabStop = false;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // No
            // 
            this.No.FillWeight = 50F;
            this.No.HeaderText = "ลำดับ";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(1060, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 19);
            this.label7.TabIndex = 41;
            this.label7.Text = "ข้อมูลทั้งหมด";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblCount);
            this.groupBox2.Controls.Add(this.picExcel);
            this.groupBox2.Controls.Add(this.dataGridView);
            this.groupBox2.Controls.Add(this.lblSearch);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lblHSearch);
            this.groupBox2.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.groupBox2.ForeColor = System.Drawing.Color.Gray;
            this.groupBox2.Location = new System.Drawing.Point(3, 354);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1275, 270);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            // 
            // lblCount
            // 
            this.lblCount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblCount.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblCount.Location = new System.Drawing.Point(1138, 19);
            this.lblCount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(102, 19);
            this.lblCount.TabIndex = 130;
            this.lblCount.TabStop = false;
            this.lblCount.Text = "0 รายการ";
            this.lblCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // picExcel
            // 
            this.picExcel.Image = ((System.Drawing.Image)(resources.GetObject("picExcel.Image")));
            this.picExcel.Location = new System.Drawing.Point(24, 48);
            this.picExcel.Name = "picExcel";
            this.picExcel.Size = new System.Drawing.Size(32, 25);
            this.picExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picExcel.TabIndex = 42;
            this.picExcel.TabStop = false;
            this.picExcel.Visible = false;
            this.picExcel.Click += new System.EventHandler(this.picExcel_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.gbFrm);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.btnPrint);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.btnAdd);
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnClear);
            this.groupBox4.Controls.Add(this.btnEdit);
            this.groupBox4.Controls.Add(this.pbBanner);
            this.groupBox4.Controls.Add(this.lblProTH);
            this.groupBox4.Controls.Add(this.lblProEN);
            this.groupBox4.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.groupBox4.Location = new System.Drawing.Point(2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1346, 626);
            this.groupBox4.TabIndex = 46;
            this.groupBox4.TabStop = false;
            // 
            // gbFrm
            // 
            this.gbFrm.Controls.Add(this.cbbProvince);
            this.gbFrm.Controls.Add(this.label5);
            this.gbFrm.Controls.Add(this.pbBarcode);
            this.gbFrm.Controls.Add(this.txtId);
            this.gbFrm.Controls.Add(this.label4);
            this.gbFrm.Controls.Add(this.txtTime);
            this.gbFrm.Controls.Add(this.txtCode);
            this.gbFrm.Controls.Add(this.txtDay);
            this.gbFrm.Controls.Add(this.txtNameTh);
            this.gbFrm.Controls.Add(this.label1);
            this.gbFrm.Controls.Add(this.btnSearch);
            this.gbFrm.Controls.Add(this.cbbStatus);
            this.gbFrm.Controls.Add(this.cbbBank);
            this.gbFrm.Controls.Add(this.lblStatus);
            this.gbFrm.Controls.Add(this.txtNameEn);
            this.gbFrm.Controls.Add(this.lblID);
            this.gbFrm.Controls.Add(this.txtAddress);
            this.gbFrm.Controls.Add(this.label3);
            this.gbFrm.Controls.Add(this.lblName);
            this.gbFrm.Controls.Add(this.label2);
            this.gbFrm.Enabled = false;
            this.gbFrm.Location = new System.Drawing.Point(3, 84);
            this.gbFrm.Name = "gbFrm";
            this.gbFrm.Size = new System.Drawing.Size(1271, 270);
            this.gbFrm.TabIndex = 130;
            this.gbFrm.TabStop = false;
            // 
            // pbBarcode
            // 
            this.pbBarcode.BackColor = System.Drawing.Color.White;
            this.pbBarcode.Location = new System.Drawing.Point(1138, 142);
            this.pbBarcode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pbBarcode.Name = "pbBarcode";
            this.pbBarcode.Size = new System.Drawing.Size(118, 114);
            this.pbBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbBarcode.TabIndex = 207;
            this.pbBarcode.TabStop = false;
            // 
            // txtId
            // 
            this.txtId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtId.Enabled = false;
            this.txtId.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.txtId.ForeColor = System.Drawing.Color.Gainsboro;
            this.txtId.Location = new System.Drawing.Point(262, 24);
            this.txtId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(110, 19);
            this.txtId.TabIndex = 130;
            this.txtId.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtId.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label4.ForeColor = System.Drawing.Color.OliveDrab;
            this.label4.Location = new System.Drawing.Point(389, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 19);
            this.label4.TabIndex = 129;
            this.label4.Text = "เวลา:";
            // 
            // txtTime
            // 
            this.txtTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTime.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.txtTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTime.Location = new System.Drawing.Point(433, 131);
            this.txtTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(102, 19);
            this.txtTime.TabIndex = 128;
            // 
            // txtCode
            // 
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCode.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.txtCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCode.Location = new System.Drawing.Point(262, 59);
            this.txtCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(140, 19);
            this.txtCode.TabIndex = 15;
            // 
            // txtDay
            // 
            this.txtDay.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDay.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.txtDay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDay.Location = new System.Drawing.Point(262, 131);
            this.txtDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDay.Name = "txtDay";
            this.txtDay.Size = new System.Drawing.Size(85, 19);
            this.txtDay.TabIndex = 127;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label1.ForeColor = System.Drawing.Color.OliveDrab;
            this.label1.Location = new System.Drawing.Point(155, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 19);
            this.label1.TabIndex = 126;
            this.label1.Text = "วันทำการ:";
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.ForeColor = System.Drawing.Color.Transparent;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(980, 19);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(36, 36);
            this.btnPrint.TabIndex = 109;
            this.btnPrint.TabStop = false;
            this.btnPrint.Tag = "เคลียร์";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.DarkRed;
            this.label25.Location = new System.Drawing.Point(1192, 57);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(36, 15);
            this.label25.TabIndex = 123;
            this.label25.Text = "Alt+C";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.DarkRed;
            this.label24.Location = new System.Drawing.Point(1024, 57);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(34, 15);
            this.label24.TabIndex = 119;
            this.label24.Text = "Alt+F";
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.DarkRed;
            this.label23.Location = new System.Drawing.Point(1230, 58);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(39, 15);
            this.label23.TabIndex = 122;
            this.label23.Text = "Ctrl+X";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.DarkRed;
            this.label22.Location = new System.Drawing.Point(1148, 58);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 15);
            this.label22.TabIndex = 121;
            this.label22.Text = "Ctrl+D";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.DarkRed;
            this.label21.Location = new System.Drawing.Point(1107, 57);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(39, 15);
            this.label21.TabIndex = 120;
            this.label21.Text = "Ctrl+E";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(1020, 19);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 36);
            this.button1.TabIndex = 115;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.DarkRed;
            this.label11.Location = new System.Drawing.Point(979, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 15);
            this.label11.TabIndex = 116;
            this.label11.Text = "Ctrl+P";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.DarkRed;
            this.label20.Location = new System.Drawing.Point(1064, 57);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(39, 15);
            this.label20.TabIndex = 117;
            this.label20.Text = "Ctrl+S";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(1146, 19);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 36);
            this.btnDelete.TabIndex = 112;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(1062, 19);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 36);
            this.btnAdd.TabIndex = 110;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(1230, 19);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(36, 36);
            this.btnExit.TabIndex = 114;
            this.btnExit.Tag = "ออก";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(1188, 19);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(36, 36);
            this.btnClear.TabIndex = 113;
            this.btnClear.Tag = "เคลียร์";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(1104, 19);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(36, 36);
            this.btnEdit.TabIndex = 111;
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // pbBanner
            // 
            this.pbBanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBanner.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pbBanner.Image = ((System.Drawing.Image)(resources.GetObject("pbBanner.Image")));
            this.pbBanner.Location = new System.Drawing.Point(3, 3);
            this.pbBanner.Name = "pbBanner";
            this.pbBanner.Size = new System.Drawing.Size(1275, 93);
            this.pbBanner.TabIndex = 118;
            this.pbBanner.TabStop = false;
            // 
            // lblProTH
            // 
            this.lblProTH.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblProTH.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblProTH.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.lblProTH.ForeColor = System.Drawing.Color.DimGray;
            this.lblProTH.Location = new System.Drawing.Point(388, 63);
            this.lblProTH.Name = "lblProTH";
            this.lblProTH.Size = new System.Drawing.Size(322, 19);
            this.lblProTH.TabIndex = 125;
            this.lblProTH.Text = "โปรแกรม";
            // 
            // lblProEN
            // 
            this.lblProEN.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblProEN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblProEN.Font = new System.Drawing.Font("Mitr ExtraLight", 18F);
            this.lblProEN.ForeColor = System.Drawing.Color.IndianRed;
            this.lblProEN.Location = new System.Drawing.Point(388, 23);
            this.lblProEN.Name = "lblProEN";
            this.lblProEN.Size = new System.Drawing.Size(322, 38);
            this.lblProEN.TabIndex = 124;
            this.lblProEN.Text = "PROGRAM";
            // 
            // cbbProvince
            // 
            this.cbbProvince.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbProvince.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.cbbProvince.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbbProvince.FormattingEnabled = true;
            this.cbbProvince.Location = new System.Drawing.Point(774, 157);
            this.cbbProvince.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbProvince.Name = "cbbProvince";
            this.cbbProvince.Size = new System.Drawing.Size(174, 27);
            this.cbbProvince.TabIndex = 209;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Mitr Light", 9F);
            this.label5.ForeColor = System.Drawing.Color.OliveDrab;
            this.label5.Location = new System.Drawing.Point(670, 166);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 19);
            this.label5.TabIndex = 208;
            this.label5.Text = "จังหวัด:";
            // 
            // FrmManageBranch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1284, 630);
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmManageBranch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmManageBranch_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmManageBranch_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gbFrm.ResumeLayout(false);
            this.gbFrm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBarcode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblHSearch;
        private System.Windows.Forms.PictureBox picExcel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtNameTh;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cbbStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbBank;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNameEn;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TextBox lblProTH;
        private System.Windows.Forms.TextBox lblProEN;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.PictureBox pbBanner;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TextBox txtDay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox lblCount;
        private System.Windows.Forms.GroupBox gbFrm;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.PictureBox pbBarcode;
        private System.Windows.Forms.ComboBox cbbProvince;
        private System.Windows.Forms.Label label5;
    }
}