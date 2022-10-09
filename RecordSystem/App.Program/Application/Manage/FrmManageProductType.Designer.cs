namespace SANSANG
{
    partial class FrmManageProductType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManageProductType));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtCode = new System.Windows.Forms.TextBox();
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
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picExcel = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtDetail = new System.Windows.Forms.TextBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtNameEn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbbProductTypeMain = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSearchs = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblProTH = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblProEN = new System.Windows.Forms.TextBox();
            this.pbBanner = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCode
            // 
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtCode.Location = new System.Drawing.Point(225, 174);
            this.txtCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(177, 18);
            this.txtCode.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.Transparent;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(409, 170);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 27);
            this.btnSearch.TabIndex = 21;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtNameTh
            // 
            this.txtNameTh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNameTh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtNameTh.Location = new System.Drawing.Point(259, 209);
            this.txtNameTh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNameTh.Name = "txtNameTh";
            this.txtNameTh.Size = new System.Drawing.Size(174, 18);
            this.txtNameTh.TabIndex = 15;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.ForeColor = System.Drawing.Color.Gray;
            this.lblName.Location = new System.Drawing.Point(144, 209);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(66, 17);
            this.lblName.TabIndex = 16;
            this.lblName.Text = "ชื่อประเภท :";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.ForeColor = System.Drawing.Color.Gray;
            this.lblID.Location = new System.Drawing.Point(174, 175);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(36, 17);
            this.lblID.TabIndex = 10;
            this.lblID.Text = "รหัส :";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(618, 243);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 17);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.Text = "สถานะ :";
            // 
            // cbbStatus
            // 
            this.cbbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.cbbStatus.FormattingEnabled = true;
            this.cbbStatus.Location = new System.Drawing.Point(711, 240);
            this.cbbStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbStatus.Name = "cbbStatus";
            this.cbbStatus.Size = new System.Drawing.Size(175, 25);
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
            this.lblHSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblHSearch.Location = new System.Drawing.Point(16, 19);
            this.lblHSearch.Name = "lblHSearch";
            this.lblHSearch.Size = new System.Drawing.Size(50, 17);
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveBorder;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dataGridView.Location = new System.Drawing.Point(19, 46);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1289, 205);
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
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(1262, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 17);
            this.label6.TabIndex = 41;
            this.label6.Text = "รายการ";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(1151, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 17);
            this.label7.TabIndex = 41;
            this.label7.Text = "ข้อมูลทั้งหมด";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblCount.Location = new System.Drawing.Point(1235, 19);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(15, 17);
            this.lblCount.TabIndex = 41;
            this.lblCount.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblHSearch);
            this.groupBox2.Controls.Add(this.picExcel);
            this.groupBox2.Controls.Add(this.dataGridView);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblSearch);
            this.groupBox2.Controls.Add(this.lblCount);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.ForeColor = System.Drawing.Color.Gray;
            this.groupBox2.Location = new System.Drawing.Point(10, 353);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1325, 267);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            // 
            // picExcel
            // 
            this.picExcel.Image = ((System.Drawing.Image)(resources.GetObject("picExcel.Image")));
            this.picExcel.Location = new System.Drawing.Point(23, 51);
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
            this.groupBox4.Controls.Add(this.txtDetail);
            this.groupBox4.Controls.Add(this.txtRemark);
            this.groupBox4.Controls.Add(this.txtNameEn);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.lblName);
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.lblID);
            this.groupBox4.Controls.Add(this.lblStatus);
            this.groupBox4.Controls.Add(this.txtCode);
            this.groupBox4.Controls.Add(this.txtNameTh);
            this.groupBox4.Controls.Add(this.cbbProductTypeMain);
            this.groupBox4.Controls.Add(this.cbbStatus);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.pictureBox2);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.btnEdit);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.btnClear);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnSearchs);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.lblProTH);
            this.groupBox4.Controls.Add(this.btnAdd);
            this.groupBox4.Controls.Add(this.lblProEN);
            this.groupBox4.Controls.Add(this.pbBanner);
            this.groupBox4.Location = new System.Drawing.Point(2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1346, 626);
            this.groupBox4.TabIndex = 46;
            this.groupBox4.TabStop = false;
            // 
            // txtDetail
            // 
            this.txtDetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtDetail.Location = new System.Drawing.Point(259, 277);
            this.txtDetail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDetail.Multiline = true;
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.Size = new System.Drawing.Size(250, 57);
            this.txtDetail.TabIndex = 128;
            // 
            // txtRemark
            // 
            this.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRemark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtRemark.Location = new System.Drawing.Point(711, 209);
            this.txtRemark.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(174, 18);
            this.txtRemark.TabIndex = 127;
            // 
            // txtNameEn
            // 
            this.txtNameEn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNameEn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtNameEn.Location = new System.Drawing.Point(259, 243);
            this.txtNameEn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNameEn.Name = "txtNameEn";
            this.txtNameEn.Size = new System.Drawing.Size(174, 18);
            this.txtNameEn.TabIndex = 126;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(141, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "รายละเอียด :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(614, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "ประเภท :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(604, 209);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "หมายเหตุ :";
            // 
            // cbbProductTypeMain
            // 
            this.cbbProductTypeMain.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbProductTypeMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.cbbProductTypeMain.FormattingEnabled = true;
            this.cbbProductTypeMain.Location = new System.Drawing.Point(711, 171);
            this.cbbProductTypeMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbProductTypeMain.Name = "cbbProductTypeMain";
            this.cbbProductTypeMain.Size = new System.Drawing.Size(175, 25);
            this.cbbProductTypeMain.TabIndex = 20;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(5, 83);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1336, 275);
            this.pictureBox2.TabIndex = 46;
            this.pictureBox2.TabStop = false;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.DarkRed;
            this.label25.Location = new System.Drawing.Point(1236, 55);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(36, 13);
            this.label25.TabIndex = 125;
            this.label25.Text = "Alt+C";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.DarkRed;
            this.label24.Location = new System.Drawing.Point(1068, 55);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(35, 13);
            this.label24.TabIndex = 121;
            this.label24.Text = "Alt+F";
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.DarkRed;
            this.label23.Location = new System.Drawing.Point(1274, 56);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(39, 13);
            this.label23.TabIndex = 124;
            this.label23.Text = "Ctrl+X";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.DarkRed;
            this.label22.Location = new System.Drawing.Point(1192, 56);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 13);
            this.label22.TabIndex = 123;
            this.label22.Text = "Ctrl+D";
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(1151, 17);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(36, 36);
            this.btnEdit.TabIndex = 112;
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.DarkRed;
            this.label21.Location = new System.Drawing.Point(1151, 55);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(39, 13);
            this.label21.TabIndex = 122;
            this.label21.Text = "Ctrl+E";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(1235, 17);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(36, 36);
            this.btnClear.TabIndex = 114;
            this.btnClear.Tag = "เคลียร์";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.DarkRed;
            this.label20.Location = new System.Drawing.Point(1108, 55);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(39, 13);
            this.label20.TabIndex = 120;
            this.label20.Text = "Ctrl+S";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(1277, 17);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(36, 36);
            this.btnExit.TabIndex = 115;
            this.btnExit.Tag = "ออก";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSearchs
            // 
            this.btnSearchs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchs.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearchs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchs.ForeColor = System.Drawing.Color.White;
            this.btnSearchs.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchs.Image")));
            this.btnSearchs.Location = new System.Drawing.Point(1067, 16);
            this.btnSearchs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearchs.Name = "btnSearchs";
            this.btnSearchs.Size = new System.Drawing.Size(36, 36);
            this.btnSearchs.TabIndex = 116;
            this.btnSearchs.UseVisualStyleBackColor = false;
            this.btnSearchs.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(1193, 17);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 36);
            this.btnDelete.TabIndex = 113;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lblProTH
            // 
            this.lblProTH.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblProTH.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblProTH.Font = new System.Drawing.Font("Mitr ExtraLight", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lblProTH.ForeColor = System.Drawing.Color.DimGray;
            this.lblProTH.Location = new System.Drawing.Point(388, 50);
            this.lblProTH.Name = "lblProTH";
            this.lblProTH.Size = new System.Drawing.Size(312, 30);
            this.lblProTH.TabIndex = 119;
            this.lblProTH.Text = "โปรแกรม";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(1109, 17);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 36);
            this.btnAdd.TabIndex = 111;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblProEN
            // 
            this.lblProEN.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblProEN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblProEN.Font = new System.Drawing.Font("Josefin Sans", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lblProEN.ForeColor = System.Drawing.Color.IndianRed;
            this.lblProEN.Location = new System.Drawing.Point(388, 15);
            this.lblProEN.Name = "lblProEN";
            this.lblProEN.Size = new System.Drawing.Size(312, 28);
            this.lblProEN.TabIndex = 118;
            this.lblProEN.Text = "PROGRAM";
            // 
            // pbBanner
            // 
            this.pbBanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBanner.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pbBanner.Image = ((System.Drawing.Image)(resources.GetObject("pbBanner.Image")));
            this.pbBanner.Location = new System.Drawing.Point(5, 4);
            this.pbBanner.Name = "pbBanner";
            this.pbBanner.Size = new System.Drawing.Size(1336, 93);
            this.pbBanner.TabIndex = 117;
            this.pbBanner.TabStop = false;
            // 
            // FrmManageProductType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1350, 630);
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmManageProductType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmManageProductType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblHSearch;
        private System.Windows.Forms.PictureBox picExcel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtNameTh;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cbbStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSearchs;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox lblProTH;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox lblProEN;
        private System.Windows.Forms.PictureBox pbBanner;
        private System.Windows.Forms.TextBox txtNameEn;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbProductTypeMain;
    }
}