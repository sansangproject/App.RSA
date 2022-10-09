namespace SANSANG
{
    partial class FrmFlights
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFlights));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.picAirline = new System.Windows.Forms.PictureBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblHSearch = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picExcel = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbbLogo = new System.Windows.Forms.ComboBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.txtETKT_M = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.picAirline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // picAirline
            // 
            this.picAirline.Image = ((System.Drawing.Image)(resources.GetObject("picAirline.Image")));
            this.picAirline.Location = new System.Drawing.Point(1162, 116);
            this.picAirline.Name = "picAirline";
            this.picAirline.Size = new System.Drawing.Size(154, 206);
            this.picAirline.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAirline.TabIndex = 22;
            this.picAirline.TabStop = false;
            // 
            // txtSearch
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtSearch.Location = new System.Drawing.Point(195, 173);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(168, 18);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TabStop = false;
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
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSeaGreen;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dataGridView.Location = new System.Drawing.Point(19, 46);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1289, 187);
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
            this.groupBox2.Location = new System.Drawing.Point(10, 366);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1325, 249);
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
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Josefin Sans", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(243, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(300, 37);
            this.textBox1.TabIndex = 44;
            this.textBox1.Text = "| PROGRAM SAVE FLIGHT";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Controls.Add(this.btnExit);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Location = new System.Drawing.Point(-2, -10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1355, 102);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " ";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Mitr ExtraLight", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.textBox2.ForeColor = System.Drawing.Color.DimGray;
            this.textBox2.Location = new System.Drawing.Point(264, 67);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(199, 27);
            this.textBox2.TabIndex = 44;
            this.textBox2.Text = "โปรแกรมบันทึกเที่ยวบิน";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, -47);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(244, 226);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 45;
            this.pictureBox1.TabStop = false;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.Transparent;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(1295, 41);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(36, 36);
            this.btnExit.TabIndex = 11;
            this.btnExit.Tag = "ออก";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.Transparent;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(185, 209);
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
            this.btnEdit.Location = new System.Drawing.Point(227, 209);
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
            this.btnClear.Location = new System.Drawing.Point(311, 209);
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
            this.btnDelete.Location = new System.Drawing.Point(269, 209);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 36);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.cbbLogo);
            this.groupBox4.Controls.Add(this.textBox12);
            this.groupBox4.Controls.Add(this.textBox21);
            this.groupBox4.Controls.Add(this.textBox18);
            this.groupBox4.Controls.Add(this.textBox23);
            this.groupBox4.Controls.Add(this.textBox20);
            this.groupBox4.Controls.Add(this.textBox22);
            this.groupBox4.Controls.Add(this.textBox19);
            this.groupBox4.Controls.Add(this.textBox13);
            this.groupBox4.Controls.Add(this.textBox11);
            this.groupBox4.Controls.Add(this.textBox6);
            this.groupBox4.Controls.Add(this.textBox16);
            this.groupBox4.Controls.Add(this.textBox5);
            this.groupBox4.Controls.Add(this.textBox15);
            this.groupBox4.Controls.Add(this.textBox4);
            this.groupBox4.Controls.Add(this.textBox9);
            this.groupBox4.Controls.Add(this.textBox8);
            this.groupBox4.Controls.Add(this.textBox17);
            this.groupBox4.Controls.Add(this.txtETKT_M);
            this.groupBox4.Controls.Add(this.textBox14);
            this.groupBox4.Controls.Add(this.textBox10);
            this.groupBox4.Controls.Add(this.textBox3);
            this.groupBox4.Controls.Add(this.txtSearch);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Controls.Add(this.btnAdd);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.btnClear);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.picAirline);
            this.groupBox4.Controls.Add(this.btnEdit);
            this.groupBox4.Controls.Add(this.pictureBox2);
            this.groupBox4.Location = new System.Drawing.Point(2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1346, 626);
            this.groupBox4.TabIndex = 46;
            this.groupBox4.TabStop = false;
            // 
            // cbbLogo
            // 
            this.cbbLogo.FormattingEnabled = true;
            this.cbbLogo.Location = new System.Drawing.Point(1162, 327);
            this.cbbLogo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbLogo.Name = "cbbLogo";
            this.cbbLogo.Size = new System.Drawing.Size(154, 25);
            this.cbbLogo.TabIndex = 47;
            this.cbbLogo.SelectedIndexChanged += new System.EventHandler(this.cbbLogo_SelectedIndexChanged);
            // 
            // textBox12
            // 
            this.textBox12.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox12.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox12.ForeColor = System.Drawing.Color.Black;
            this.textBox12.Location = new System.Drawing.Point(885, 165);
            this.textBox12.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(45, 22);
            this.textBox12.TabIndex = 0;
            this.textBox12.TabStop = false;
            this.textBox12.Text = "35B";
            this.textBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox21
            // 
            this.textBox21.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox21.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.textBox21.ForeColor = System.Drawing.Color.Black;
            this.textBox21.Location = new System.Drawing.Point(945, 270);
            this.textBox21.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new System.Drawing.Size(62, 15);
            this.textBox21.TabIndex = 0;
            this.textBox21.TabStop = false;
            this.textBox21.Text = "C25";
            // 
            // textBox18
            // 
            this.textBox18.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox18.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.textBox18.ForeColor = System.Drawing.Color.Black;
            this.textBox18.Location = new System.Drawing.Point(946, 241);
            this.textBox18.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new System.Drawing.Size(62, 15);
            this.textBox18.TabIndex = 0;
            this.textBox18.TabStop = false;
            this.textBox18.Text = "TG 5012";
            // 
            // textBox23
            // 
            this.textBox23.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox23.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.textBox23.ForeColor = System.Drawing.Color.Black;
            this.textBox23.Location = new System.Drawing.Point(1065, 270);
            this.textBox23.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new System.Drawing.Size(62, 15);
            this.textBox23.TabIndex = 0;
            this.textBox23.TabStop = false;
            this.textBox23.Text = "35B";
            this.textBox23.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox20
            // 
            this.textBox20.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox20.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.textBox20.ForeColor = System.Drawing.Color.Black;
            this.textBox20.Location = new System.Drawing.Point(1065, 241);
            this.textBox20.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new System.Drawing.Size(62, 15);
            this.textBox20.TabIndex = 0;
            this.textBox20.TabStop = false;
            this.textBox20.Text = "22";
            this.textBox20.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox22
            // 
            this.textBox22.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox22.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.textBox22.ForeColor = System.Drawing.Color.Black;
            this.textBox22.Location = new System.Drawing.Point(1007, 270);
            this.textBox22.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new System.Drawing.Size(62, 15);
            this.textBox22.TabIndex = 0;
            this.textBox22.TabStop = false;
            this.textBox22.Text = "11:45";
            this.textBox22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox19
            // 
            this.textBox19.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox19.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.textBox19.ForeColor = System.Drawing.Color.Black;
            this.textBox19.Location = new System.Drawing.Point(1007, 241);
            this.textBox19.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new System.Drawing.Size(62, 15);
            this.textBox19.TabIndex = 0;
            this.textBox19.TabStop = false;
            this.textBox19.Text = "23NOV";
            this.textBox19.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox13
            // 
            this.textBox13.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox13.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold);
            this.textBox13.ForeColor = System.Drawing.Color.Black;
            this.textBox13.Location = new System.Drawing.Point(814, 171);
            this.textBox13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(62, 15);
            this.textBox13.TabIndex = 0;
            this.textBox13.TabStop = false;
            this.textBox13.Text = "23NOV";
            this.textBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox11
            // 
            this.textBox11.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox11.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox11.ForeColor = System.Drawing.Color.Black;
            this.textBox11.Location = new System.Drawing.Point(731, 165);
            this.textBox11.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(79, 22);
            this.textBox11.TabIndex = 0;
            this.textBox11.TabStop = false;
            this.textBox11.Text = "TG5012";
            this.textBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.ForeColor = System.Drawing.Color.Black;
            this.textBox6.Location = new System.Drawing.Point(603, 223);
            this.textBox6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(49, 22);
            this.textBox6.TabIndex = 0;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "C25";
            // 
            // textBox16
            // 
            this.textBox16.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox16.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox16.ForeColor = System.Drawing.Color.Black;
            this.textBox16.Location = new System.Drawing.Point(967, 196);
            this.textBox16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(173, 15);
            this.textBox16.TabIndex = 0;
            this.textBox16.TabStop = false;
            this.textBox16.Text = "TOKYO / HND";
            // 
            // textBox5
            // 
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.ForeColor = System.Drawing.Color.Black;
            this.textBox5.Location = new System.Drawing.Point(584, 204);
            this.textBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(166, 15);
            this.textBox5.TabIndex = 0;
            this.textBox5.TabStop = false;
            this.textBox5.Text = "TOKYO / HND";
            // 
            // textBox15
            // 
            this.textBox15.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox15.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox15.ForeColor = System.Drawing.Color.Black;
            this.textBox15.Location = new System.Drawing.Point(982, 181);
            this.textBox15.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(158, 15);
            this.textBox15.TabIndex = 0;
            this.textBox15.TabStop = false;
            this.textBox15.Text = "BANGKOK / BKK";
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.ForeColor = System.Drawing.Color.Black;
            this.textBox4.Location = new System.Drawing.Point(603, 187);
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(166, 15);
            this.textBox4.TabIndex = 0;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "BANGKOK / BKK";
            // 
            // textBox9
            // 
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox9.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox9.ForeColor = System.Drawing.Color.Black;
            this.textBox9.Location = new System.Drawing.Point(773, 229);
            this.textBox9.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(50, 15);
            this.textBox9.TabIndex = 0;
            this.textBox9.TabStop = false;
            this.textBox9.Text = "11:45";
            // 
            // textBox8
            // 
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.ForeColor = System.Drawing.Color.Black;
            this.textBox8.Location = new System.Drawing.Point(773, 255);
            this.textBox8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(50, 15);
            this.textBox8.TabIndex = 0;
            this.textBox8.TabStop = false;
            this.textBox8.Text = "22";
            // 
            // textBox17
            // 
            this.textBox17.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox17.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox17.ForeColor = System.Drawing.Color.Black;
            this.textBox17.Location = new System.Drawing.Point(982, 211);
            this.textBox17.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new System.Drawing.Size(158, 15);
            this.textBox17.TabIndex = 0;
            this.textBox17.TabStop = false;
            this.textBox17.Text = "2170000000000";
            // 
            // txtETKT_M
            // 
            this.txtETKT_M.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtETKT_M.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtETKT_M.ForeColor = System.Drawing.Color.Black;
            this.txtETKT_M.Location = new System.Drawing.Point(603, 256);
            this.txtETKT_M.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtETKT_M.Name = "txtETKT_M";
            this.txtETKT_M.Size = new System.Drawing.Size(125, 15);
            this.txtETKT_M.TabIndex = 0;
            this.txtETKT_M.TabStop = false;
            // 
            // textBox14
            // 
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox14.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.ForeColor = System.Drawing.Color.Black;
            this.textBox14.Location = new System.Drawing.Point(748, 121);
            this.textBox14.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(172, 15);
            this.textBox14.TabIndex = 0;
            this.textBox14.TabStop = false;
            this.textBox14.Text = "ECONOMY";
            this.textBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox10
            // 
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox10.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox10.ForeColor = System.Drawing.Color.Black;
            this.textBox10.Location = new System.Drawing.Point(945, 159);
            this.textBox10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(195, 18);
            this.textBox10.TabIndex = 0;
            this.textBox10.TabStop = false;
            this.textBox10.Text = "PHANYUDOAT / SARAWUT";
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.Black;
            this.textBox3.Location = new System.Drawing.Point(562, 171);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(166, 15);
            this.textBox3.TabIndex = 0;
            this.textBox3.TabStop = false;
            this.textBox3.Text = "PHANYUDOAT / SARAWUT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(121, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "คำค้นหา :";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(6, 95);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1334, 279);
            this.pictureBox2.TabIndex = 46;
            this.pictureBox2.TabStop = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // FrmSaveFlight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1350, 630);
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmSaveFlight";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmSaveFlight_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSaveMember_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picAirline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
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
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.PictureBox picAirline;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbbLogo;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox txtETKT_M;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox10;
    }
}