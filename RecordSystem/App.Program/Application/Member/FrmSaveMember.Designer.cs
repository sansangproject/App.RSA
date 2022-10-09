namespace SANSANG
{
    partial class FrmSaveMember
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSaveMember));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cbbStatus = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblHSearch = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblCondition = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.picExcel = new System.Windows.Forms.PictureBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.gbMain = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbbUser = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbbCard = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.cbShowPass = new System.Windows.Forms.CheckBox();
            this.rdbGMail = new System.Windows.Forms.RadioButton();
            this.rdbHMail = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.cbbShop = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pbBanner = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).BeginInit();
            this.gbMain.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).BeginInit();
            this.SuspendLayout();
            // 
            // pbImage
            // 
            this.pbImage.Image = ((System.Drawing.Image)(resources.GetObject("pbImage.Image")));
            this.pbImage.Location = new System.Drawing.Point(167, 158);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(221, 190);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbImage.TabIndex = 22;
            this.pbImage.TabStop = false;
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtName.Location = new System.Drawing.Point(929, 150);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(173, 21);
            this.txtName.TabIndex = 1;
            this.txtName.TabStop = false;
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblName.Location = new System.Drawing.Point(474, 187);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(60, 20);
            this.lblName.TabIndex = 16;
            this.lblName.Text = "หมายเลข:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblStatus.Location = new System.Drawing.Point(489, 260);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(45, 20);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.Text = "สถานะ:";
            // 
            // cbbStatus
            // 
            this.cbbStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.cbbStatus.FormattingEnabled = true;
            this.cbbStatus.Location = new System.Drawing.Point(575, 260);
            this.cbbStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbStatus.Name = "cbbStatus";
            this.cbbStatus.Size = new System.Drawing.Size(178, 28);
            this.cbbStatus.TabIndex = 0;
            this.cbbStatus.TabStop = false;
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
            this.lblHSearch.Font = new System.Drawing.Font("Mitr Light", 9.75F);
            this.lblHSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblHSearch.Location = new System.Drawing.Point(16, 19);
            this.lblHSearch.Name = "lblHSearch";
            this.lblHSearch.Size = new System.Drawing.Size(53, 20);
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Mitr Light", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dataGridView.Location = new System.Drawing.Point(19, 46);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1234, 259);
            this.dataGridView.TabIndex = 40;
            this.dataGridView.TabStop = false;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // No
            // 
            this.No.FillWeight = 50F;
            this.No.HeaderText = "ลำดับ";
            this.No.MinimumWidth = 6;
            this.No.Name = "No";
            this.No.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblCondition);
            this.groupBox2.Controls.Add(this.txtCount);
            this.groupBox2.Controls.Add(this.lblHSearch);
            this.groupBox2.Controls.Add(this.picExcel);
            this.groupBox2.Controls.Add(this.dataGridView);
            this.groupBox2.Controls.Add(this.lblSearch);
            this.groupBox2.ForeColor = System.Drawing.Color.Gray;
            this.groupBox2.Location = new System.Drawing.Point(10, 366);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1270, 340);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCondition.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblCondition.Location = new System.Drawing.Point(75, 19);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(49, 20);
            this.lblCondition.TabIndex = 1002;
            this.lblCondition.Text = "ทั้งหมด";
            // 
            // txtCount
            // 
            this.txtCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCount.ForeColor = System.Drawing.Color.RoyalBlue;
            this.txtCount.Location = new System.Drawing.Point(943, 19);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(310, 21);
            this.txtCount.TabIndex = 1001;
            this.txtCount.TabStop = false;
            this.txtCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.Transparent;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(53, 4);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 36);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.ForeColor = System.Drawing.Color.Transparent;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(95, 4);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(36, 36);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClear
            // 
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.Transparent;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(178, 4);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(36, 36);
            this.btnClear.TabIndex = 12;
            this.btnClear.Tag = "เคลียร์";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.Transparent;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(137, 4);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 36);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // gbMain
            // 
            this.gbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMain.BackColor = System.Drawing.Color.White;
            this.gbMain.Controls.Add(this.label10);
            this.gbMain.Controls.Add(this.cbbUser);
            this.gbMain.Controls.Add(this.label7);
            this.gbMain.Controls.Add(this.cbbCard);
            this.gbMain.Controls.Add(this.label6);
            this.gbMain.Controls.Add(this.label29);
            this.gbMain.Controls.Add(this.panel1);
            this.gbMain.Controls.Add(this.groupBox2);
            this.gbMain.Controls.Add(this.cbShowPass);
            this.gbMain.Controls.Add(this.rdbGMail);
            this.gbMain.Controls.Add(this.rdbHMail);
            this.gbMain.Controls.Add(this.label1);
            this.gbMain.Controls.Add(this.label9);
            this.gbMain.Controls.Add(this.label8);
            this.gbMain.Controls.Add(this.label5);
            this.gbMain.Controls.Add(this.label4);
            this.gbMain.Controls.Add(this.label3);
            this.gbMain.Controls.Add(this.label2);
            this.gbMain.Controls.Add(this.lblName);
            this.gbMain.Controls.Add(this.lblStatus);
            this.gbMain.Controls.Add(this.txtPassword);
            this.gbMain.Controls.Add(this.txtUser);
            this.gbMain.Controls.Add(this.txtPhone);
            this.gbMain.Controls.Add(this.txtEmail);
            this.gbMain.Controls.Add(this.txtSurname);
            this.gbMain.Controls.Add(this.txtCode);
            this.gbMain.Controls.Add(this.txtName);
            this.gbMain.Controls.Add(this.cbbStatus);
            this.gbMain.Controls.Add(this.pbImage);
            this.gbMain.Controls.Add(this.cbbShop);
            this.gbMain.Controls.Add(this.pictureBox2);
            this.gbMain.Controls.Add(this.pbBanner);
            this.gbMain.Enabled = false;
            this.gbMain.Font = new System.Drawing.Font("Mitr Light", 9.75F);
            this.gbMain.Location = new System.Drawing.Point(-3, -12);
            this.gbMain.Name = "gbMain";
            this.gbMain.Size = new System.Drawing.Size(1357, 735);
            this.gbMain.TabIndex = 46;
            this.gbMain.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Mitr Light", 8.5F);
            this.label10.ForeColor = System.Drawing.Color.Gray;
            this.label10.Location = new System.Drawing.Point(480, 238);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(54, 19);
            this.label10.TabIndex = 109;
            this.label10.Text = "Member";
            // 
            // cbbUser
            // 
            this.cbbUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbUser.FormattingEnabled = true;
            this.cbbUser.Location = new System.Drawing.Point(577, 222);
            this.cbbUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbUser.Name = "cbbUser";
            this.cbbUser.Size = new System.Drawing.Size(197, 28);
            this.cbbUser.TabIndex = 108;
            this.cbbUser.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Mitr Light", 8.5F);
            this.label7.ForeColor = System.Drawing.Color.Gray;
            this.label7.Location = new System.Drawing.Point(499, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 19);
            this.label7.TabIndex = 107;
            this.label7.Text = "Card";
            // 
            // cbbCard
            // 
            this.cbbCard.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbCard.FormattingEnabled = true;
            this.cbbCard.Location = new System.Drawing.Point(577, 185);
            this.cbbCard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbCard.Name = "cbbCard";
            this.cbbCard.Size = new System.Drawing.Size(197, 28);
            this.cbbCard.TabIndex = 106;
            this.cbbCard.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Mitr Light", 8.5F);
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(488, 275);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 19);
            this.label6.TabIndex = 105;
            this.label6.Text = "Status";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Mitr Light", 8.5F);
            this.label29.ForeColor = System.Drawing.Color.Gray;
            this.label29.Location = new System.Drawing.Point(497, 163);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(37, 19);
            this.label29.TabIndex = 104;
            this.label29.Text = "Shop";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.label25);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Controls.Add(this.btnEdit);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Location = new System.Drawing.Point(1015, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(266, 64);
            this.panel1.TabIndex = 103;
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label25.Font = new System.Drawing.Font("Mitr Light", 8F);
            this.label25.ForeColor = System.Drawing.Color.DarkRed;
            this.label25.Location = new System.Drawing.Point(182, 42);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(35, 18);
            this.label25.TabIndex = 99;
            this.label25.Text = "Alt+C";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label24.Font = new System.Drawing.Font("Mitr Light", 8F);
            this.label24.ForeColor = System.Drawing.Color.DarkRed;
            this.label24.Location = new System.Drawing.Point(14, 42);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(34, 18);
            this.label24.TabIndex = 95;
            this.label24.Text = "Alt+F";
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label23.Font = new System.Drawing.Font("Mitr Light", 8F);
            this.label23.ForeColor = System.Drawing.Color.DarkRed;
            this.label23.Location = new System.Drawing.Point(220, 43);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(39, 18);
            this.label23.TabIndex = 98;
            this.label23.Text = "Ctrl+X";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label22.Font = new System.Drawing.Font("Mitr Light", 8F);
            this.label22.ForeColor = System.Drawing.Color.DarkRed;
            this.label22.Location = new System.Drawing.Point(138, 43);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(38, 18);
            this.label22.TabIndex = 97;
            this.label22.Text = "Ctrl+D";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label21.Font = new System.Drawing.Font("Mitr Light", 8F);
            this.label21.ForeColor = System.Drawing.Color.DarkRed;
            this.label21.Location = new System.Drawing.Point(97, 42);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(37, 18);
            this.label21.TabIndex = 96;
            this.label21.Text = "Ctrl+E";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(10, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(36, 36);
            this.btnSearch.TabIndex = 92;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label20.Font = new System.Drawing.Font("Mitr Light", 8F);
            this.label20.ForeColor = System.Drawing.Color.DarkRed;
            this.label20.Location = new System.Drawing.Point(54, 42);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(37, 18);
            this.label20.TabIndex = 93;
            this.label20.Text = "Ctrl+S";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(220, 4);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(36, 36);
            this.btnExit.TabIndex = 91;
            this.btnExit.Tag = "ออก";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // cbShowPass
            // 
            this.cbShowPass.AutoSize = true;
            this.cbShowPass.ForeColor = System.Drawing.Color.Gray;
            this.cbShowPass.Location = new System.Drawing.Point(1124, 302);
            this.cbShowPass.Name = "cbShowPass";
            this.cbShowPass.Size = new System.Drawing.Size(82, 24);
            this.cbShowPass.TabIndex = 102;
            this.cbShowPass.Text = "แสดงรหัส";
            this.cbShowPass.UseVisualStyleBackColor = true;
            this.cbShowPass.CheckedChanged += new System.EventHandler(this.cbShowPass_CheckedChanged);
            // 
            // rdbGMail
            // 
            this.rdbGMail.AccessibleName = "rdbMail";
            this.rdbGMail.AutoSize = true;
            this.rdbGMail.ForeColor = System.Drawing.Color.Gray;
            this.rdbGMail.Location = new System.Drawing.Point(1211, 183);
            this.rdbGMail.Name = "rdbGMail";
            this.rdbGMail.Size = new System.Drawing.Size(60, 24);
            this.rdbGMail.TabIndex = 5;
            this.rdbGMail.Text = "Gmail";
            this.rdbGMail.UseVisualStyleBackColor = true;
            // 
            // rdbHMail
            // 
            this.rdbHMail.AccessibleName = "rdbMail";
            this.rdbHMail.AutoSize = true;
            this.rdbHMail.Checked = true;
            this.rdbHMail.ForeColor = System.Drawing.Color.Gray;
            this.rdbHMail.Location = new System.Drawing.Point(1132, 183);
            this.rdbHMail.Name = "rdbHMail";
            this.rdbHMail.Size = new System.Drawing.Size(74, 24);
            this.rdbHMail.TabIndex = 4;
            this.rdbHMail.TabStop = true;
            this.rdbHMail.Text = "Hotmail";
            this.rdbHMail.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(486, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "สมาชิก:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(806, 301);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 20);
            this.label9.TabIndex = 16;
            this.label9.Text = "Password :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(835, 262);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 20);
            this.label8.TabIndex = 16;
            this.label8.Text = "User :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(820, 223);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "เบอร์โทร :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(838, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 20);
            this.label4.TabIndex = 16;
            this.label4.Text = "อีเมล์ :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(821, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "ชื่อ-สกุล :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Location = new System.Drawing.Point(487, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 16;
            this.label2.Text = "บริการ:";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.txtPassword.Location = new System.Drawing.Point(929, 298);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(173, 22);
            this.txtPassword.TabIndex = 8;
            // 
            // txtUser
            // 
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUser.ForeColor = System.Drawing.Color.Navy;
            this.txtUser.Location = new System.Drawing.Point(929, 262);
            this.txtUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(173, 21);
            this.txtUser.TabIndex = 7;
            // 
            // txtPhone
            // 
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtPhone.Location = new System.Drawing.Point(929, 223);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPhone.MaxLength = 1000;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(173, 21);
            this.txtPhone.TabIndex = 6;
            this.txtPhone.TextChanged += new System.EventHandler(this.txtPhone_TextChanged);
            this.txtPhone.Leave += new System.EventHandler(this.txtPhone_Leave);
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtEmail.Location = new System.Drawing.Point(929, 186);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(173, 21);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.Leave += new System.EventHandler(this.txtEmail_Leave);
            // 
            // txtSurname
            // 
            this.txtSurname.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSurname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtSurname.Location = new System.Drawing.Point(1133, 150);
            this.txtSurname.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(137, 21);
            this.txtSurname.TabIndex = 2;
            this.txtSurname.Leave += new System.EventHandler(this.txtSurname_Leave);
            // 
            // txtCode
            // 
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCode.Enabled = false;
            this.txtCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtCode.Location = new System.Drawing.Point(205, 109);
            this.txtCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(223, 21);
            this.txtCode.TabIndex = 0;
            this.txtCode.TabStop = false;
            this.txtCode.Visible = false;
            // 
            // cbbShop
            // 
            this.cbbShop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbShop.FormattingEnabled = true;
            this.cbbShop.Location = new System.Drawing.Point(577, 148);
            this.cbbShop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbShop.Name = "cbbShop";
            this.cbbShop.Size = new System.Drawing.Size(197, 28);
            this.cbbShop.TabIndex = 0;
            this.cbbShop.TabStop = false;
            this.cbbShop.SelectedIndexChanged += new System.EventHandler(this.cbbShop_SelectedIndexChanged);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 93);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1277, 289);
            this.pictureBox2.TabIndex = 46;
            this.pictureBox2.TabStop = false;
            // 
            // pbBanner
            // 
            this.pbBanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBanner.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pbBanner.Image = ((System.Drawing.Image)(resources.GetObject("pbBanner.Image")));
            this.pbBanner.Location = new System.Drawing.Point(3, 13);
            this.pbBanner.Name = "pbBanner";
            this.pbBanner.Size = new System.Drawing.Size(1287, 74);
            this.pbBanner.TabIndex = 94;
            this.pbBanner.TabStop = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // FrmSaveMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1284, 702);
            this.Controls.Add(this.gbMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmSaveMember";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmSaveMember_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSaveMember_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).EndInit();
            this.gbMain.ResumeLayout(false);
            this.gbMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblHSearch;
        private System.Windows.Forms.PictureBox picExcel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox gbMain;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cbbStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ComboBox cbbShop;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.RadioButton rdbGMail;
        private System.Windows.Forms.RadioButton rdbHMail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox pbBanner;
        private System.Windows.Forms.CheckBox cbShowPass;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbbCard;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbbUser;
    }
}