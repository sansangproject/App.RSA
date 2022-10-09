namespace SANSANG
{
    partial class FrmRebates
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRebates));
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblHSearch = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.picExcel = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.cbbJoin = new System.Windows.Forms.ComboBox();
            this.cbbRank = new System.Windows.Forms.ComboBox();
            this.cbbUsed = new System.Windows.Forms.ComboBox();
            this.cbbGroup = new System.Windows.Forms.ComboBox();
            this.cbbAmphur = new System.Windows.Forms.ComboBox();
            this.cbbProvince = new System.Windows.Forms.ComboBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblProTH = new System.Windows.Forms.TextBox();
            this.lblProEN = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
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
            this.lblHSearch.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.lblHSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblHSearch.Location = new System.Drawing.Point(18, 18);
            this.lblHSearch.Name = "lblHSearch";
            this.lblHSearch.Size = new System.Drawing.Size(71, 22);
            this.lblHSearch.TabIndex = 41;
            this.lblHSearch.Text = "จำนวนเงิน";
            this.lblHSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Mitr Light", 10F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Teal;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dataGridView.Location = new System.Drawing.Point(19, 46);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1292, 170);
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
            this.label6.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(1264, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 22);
            this.label6.TabIndex = 41;
            this.label6.Text = "รายการ";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(1126, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 22);
            this.label7.TabIndex = 41;
            this.label7.Text = "ข้อมูลทั้งหมด";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblCount.Location = new System.Drawing.Point(1225, 18);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(20, 22);
            this.lblCount.TabIndex = 41;
            this.lblCount.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtTotal);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.lblHSearch);
            this.groupBox2.Controls.Add(this.picExcel);
            this.groupBox2.Controls.Add(this.dataGridView);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblSearch);
            this.groupBox2.Controls.Add(this.lblCount);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.groupBox2.ForeColor = System.Drawing.Color.Gray;
            this.groupBox2.Location = new System.Drawing.Point(10, 366);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1325, 254);
            this.groupBox2.TabIndex = 43;
            this.groupBox2.TabStop = false;
            // 
            // txtTotal
            // 
            this.txtTotal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtTotal.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.txtTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.txtTotal.Location = new System.Drawing.Point(103, 18);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(66, 21);
            this.txtTotal.TabIndex = 89;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Mitr Light", 10F);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label14.Location = new System.Drawing.Point(175, 18);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(34, 22);
            this.label14.TabIndex = 41;
            this.label14.Text = "บาท";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.txtRemark);
            this.groupBox4.Controls.Add(this.txtAmount);
            this.groupBox4.Controls.Add(this.txtCity);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtName);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtCode);
            this.groupBox4.Controls.Add(this.lblID);
            this.groupBox4.Controls.Add(this.txtNumber);
            this.groupBox4.Controls.Add(this.cbbJoin);
            this.groupBox4.Controls.Add(this.cbbRank);
            this.groupBox4.Controls.Add(this.cbbUsed);
            this.groupBox4.Controls.Add(this.cbbGroup);
            this.groupBox4.Controls.Add(this.cbbAmphur);
            this.groupBox4.Controls.Add(this.cbbProvince);
            this.groupBox4.Controls.Add(this.btnFind);
            this.groupBox4.Controls.Add(this.pictureBox2);
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.lblProTH);
            this.groupBox4.Controls.Add(this.lblProEN);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.btnAdd);
            this.groupBox4.Controls.Add(this.btnExit);
            this.groupBox4.Controls.Add(this.btnClear);
            this.groupBox4.Controls.Add(this.btnEdit);
            this.groupBox4.Controls.Add(this.pictureBox1);
            this.groupBox4.Location = new System.Drawing.Point(2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1346, 626);
            this.groupBox4.TabIndex = 46;
            this.groupBox4.TabStop = false;
            // 
            // txtRemark
            // 
            this.txtRemark.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRemark.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.txtRemark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtRemark.Location = new System.Drawing.Point(594, 308);
            this.txtRemark.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(306, 24);
            this.txtRemark.TabIndex = 47;
            this.txtRemark.Text = "-";
            this.txtRemark.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRemark_KeyPress);
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAmount.Font = new System.Drawing.Font("Mitr Light", 20F);
            this.txtAmount.ForeColor = System.Drawing.Color.Red;
            this.txtAmount.Location = new System.Drawing.Point(594, 147);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(117, 42);
            this.txtAmount.TabIndex = 47;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // txtCity
            // 
            this.txtCity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCity.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.txtCity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCity.Location = new System.Drawing.Point(197, 194);
            this.txtCity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(258, 24);
            this.txtCity.TabIndex = 47;
            this.txtCity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCity_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label11.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.label11.ForeColor = System.Drawing.Color.Gray;
            this.label11.Location = new System.Drawing.Point(538, 202);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 23);
            this.label11.TabIndex = 48;
            this.label11.Text = "สถานะ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label8.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.label8.ForeColor = System.Drawing.Color.Gray;
            this.label8.Location = new System.Drawing.Point(550, 240);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 23);
            this.label8.TabIndex = 48;
            this.label8.Text = "ฝ่าย";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label12.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.label12.ForeColor = System.Drawing.Color.Gray;
            this.label12.Location = new System.Drawing.Point(133, 307);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 23);
            this.label12.TabIndex = 48;
            this.label12.Text = "ร่วมงาน";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(549, 278);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 23);
            this.label5.TabIndex = 48;
            this.label5.Text = "ศักดิ์";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(145, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 23);
            this.label4.TabIndex = 48;
            this.label4.Text = "อำเภอ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.label10.ForeColor = System.Drawing.Color.Gray;
            this.label10.Location = new System.Drawing.Point(521, 307);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 23);
            this.label10.TabIndex = 48;
            this.label10.Text = "หมายเหตุ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label9.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.label9.ForeColor = System.Drawing.Color.Gray;
            this.label9.Location = new System.Drawing.Point(514, 164);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 23);
            this.label9.TabIndex = 48;
            this.label9.Text = "จำนวนเงิน";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(137, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 23);
            this.label3.TabIndex = 48;
            this.label3.Text = "จังหวัด";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(155, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 23);
            this.label2.TabIndex = 48;
            this.label2.Text = "ที่อยู่";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtName.Location = new System.Drawing.Point(197, 161);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(258, 24);
            this.txtName.TabIndex = 47;
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(164, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 23);
            this.label1.TabIndex = 48;
            this.label1.Text = "ชื่อ";
            // 
            // txtCode
            // 
            this.txtCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCode.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.txtCode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCode.Location = new System.Drawing.Point(197, 128);
            this.txtCode.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(218, 24);
            this.txtCode.TabIndex = 47;
            this.txtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCode_KeyPress);
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblID.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.lblID.ForeColor = System.Drawing.Color.Gray;
            this.lblID.Location = new System.Drawing.Point(156, 128);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(36, 23);
            this.lblID.TabIndex = 48;
            this.lblID.Text = "รหัส";
            // 
            // txtNumber
            // 
            this.txtNumber.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNumber.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.txtNumber.ForeColor = System.Drawing.Color.White;
            this.txtNumber.Location = new System.Drawing.Point(29, 113);
            this.txtNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(50, 24);
            this.txtNumber.TabIndex = 49;
            this.txtNumber.Text = "0";
            this.txtNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbbJoin
            // 
            this.cbbJoin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbJoin.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.cbbJoin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbbJoin.FormattingEnabled = true;
            this.cbbJoin.Location = new System.Drawing.Point(197, 305);
            this.cbbJoin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbJoin.Name = "cbbJoin";
            this.cbbJoin.Size = new System.Drawing.Size(218, 31);
            this.cbbJoin.TabIndex = 50;
            this.cbbJoin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbbJoin_KeyPress);
            // 
            // cbbRank
            // 
            this.cbbRank.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbRank.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.cbbRank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbbRank.FormattingEnabled = true;
            this.cbbRank.Location = new System.Drawing.Point(594, 271);
            this.cbbRank.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbRank.Name = "cbbRank";
            this.cbbRank.Size = new System.Drawing.Size(218, 31);
            this.cbbRank.TabIndex = 50;
            this.cbbRank.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbbRank_KeyPress);
            // 
            // cbbUsed
            // 
            this.cbbUsed.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbUsed.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.cbbUsed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbbUsed.FormattingEnabled = true;
            this.cbbUsed.Location = new System.Drawing.Point(594, 195);
            this.cbbUsed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbUsed.Name = "cbbUsed";
            this.cbbUsed.Size = new System.Drawing.Size(218, 31);
            this.cbbUsed.TabIndex = 50;
            this.cbbUsed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbbStatus_KeyPress);
            // 
            // cbbGroup
            // 
            this.cbbGroup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbGroup.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.cbbGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbbGroup.FormattingEnabled = true;
            this.cbbGroup.Location = new System.Drawing.Point(594, 233);
            this.cbbGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbGroup.Name = "cbbGroup";
            this.cbbGroup.Size = new System.Drawing.Size(218, 31);
            this.cbbGroup.TabIndex = 50;
            this.cbbGroup.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbbGroup_KeyPress);
            // 
            // cbbAmphur
            // 
            this.cbbAmphur.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbAmphur.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.cbbAmphur.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbbAmphur.FormattingEnabled = true;
            this.cbbAmphur.Location = new System.Drawing.Point(197, 267);
            this.cbbAmphur.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbAmphur.Name = "cbbAmphur";
            this.cbbAmphur.Size = new System.Drawing.Size(218, 31);
            this.cbbAmphur.TabIndex = 50;
            this.cbbAmphur.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbbAmphur_KeyPress);
            // 
            // cbbProvince
            // 
            this.cbbProvince.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbProvince.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.cbbProvince.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbbProvince.FormattingEnabled = true;
            this.cbbProvince.Location = new System.Drawing.Point(197, 227);
            this.cbbProvince.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbProvince.Name = "cbbProvince";
            this.cbbProvince.Size = new System.Drawing.Size(218, 31);
            this.cbbProvince.TabIndex = 50;
            this.cbbProvince.SelectedIndexChanged += new System.EventHandler(this.cbbProvince_SelectedIndexChanged);
            this.cbbProvince.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbbProvince_KeyPress);
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.Color.Transparent;
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.Font = new System.Drawing.Font("Mitr Light", 11F);
            this.btnFind.ForeColor = System.Drawing.Color.Transparent;
            this.btnFind.Image = ((System.Drawing.Image)(resources.GetObject("btnFind.Image")));
            this.btnFind.Location = new System.Drawing.Point(421, 128);
            this.btnFind.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(34, 24);
            this.btnFind.TabIndex = 51;
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox2.Location = new System.Drawing.Point(5, 94);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1337, 278);
            this.pictureBox2.TabIndex = 46;
            this.pictureBox2.TabStop = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(1082, 33);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(36, 36);
            this.btnSearch.TabIndex = 88;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblProTH
            // 
            this.lblProTH.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblProTH.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblProTH.Font = new System.Drawing.Font("Mitr Light", 9.75F);
            this.lblProTH.ForeColor = System.Drawing.Color.DimGray;
            this.lblProTH.Location = new System.Drawing.Point(397, 50);
            this.lblProTH.Name = "lblProTH";
            this.lblProTH.Size = new System.Drawing.Size(322, 21);
            this.lblProTH.TabIndex = 86;
            this.lblProTH.TabStop = false;
            this.lblProTH.Text = "โปรแกรม";
            // 
            // lblProEN
            // 
            this.lblProEN.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblProEN.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblProEN.Font = new System.Drawing.Font("Mitr Light", 9.75F);
            this.lblProEN.ForeColor = System.Drawing.Color.IndianRed;
            this.lblProEN.Location = new System.Drawing.Point(359, 31);
            this.lblProEN.Name = "lblProEN";
            this.lblProEN.Size = new System.Drawing.Size(322, 21);
            this.lblProEN.TabIndex = 85;
            this.lblProEN.TabStop = false;
            this.lblProEN.Text = "PROGRAM";
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.DarkRed;
            this.label25.Location = new System.Drawing.Point(1254, 71);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(36, 13);
            this.label25.TabIndex = 84;
            this.label25.Text = "Alt+C";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.DarkRed;
            this.label24.Location = new System.Drawing.Point(1086, 71);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(35, 13);
            this.label24.TabIndex = 80;
            this.label24.Text = "Alt+F";
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.DarkRed;
            this.label23.Location = new System.Drawing.Point(1292, 72);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(39, 13);
            this.label23.TabIndex = 83;
            this.label23.Text = "Ctrl+X";
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.DarkRed;
            this.label22.Location = new System.Drawing.Point(1210, 72);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 13);
            this.label22.TabIndex = 82;
            this.label22.Text = "Ctrl+D";
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.DarkRed;
            this.label21.Location = new System.Drawing.Point(1169, 71);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(39, 13);
            this.label21.TabIndex = 81;
            this.label21.Text = "Ctrl+E";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.DarkRed;
            this.label20.Location = new System.Drawing.Point(1126, 71);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(39, 13);
            this.label20.TabIndex = 78;
            this.label20.Text = "Ctrl+S";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(1208, 33);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 36);
            this.btnDelete.TabIndex = 74;
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
            this.btnAdd.Location = new System.Drawing.Point(1124, 33);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 36);
            this.btnAdd.TabIndex = 72;
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
            this.btnExit.Location = new System.Drawing.Point(1292, 33);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(36, 36);
            this.btnExit.TabIndex = 76;
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
            this.btnClear.Location = new System.Drawing.Point(1250, 33);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(36, 36);
            this.btnClear.TabIndex = 75;
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
            this.btnEdit.Location = new System.Drawing.Point(1166, 33);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(36, 36);
            this.btnEdit.TabIndex = 73;
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(5, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1334, 93);
            this.pictureBox1.TabIndex = 87;
            this.pictureBox1.TabStop = false;
            // 
            // FrmSaveRebate
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
            this.Name = "FrmSaveRebate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSaveRebate_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.TextBox lblProTH;
        private System.Windows.Forms.TextBox lblProEN;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.ComboBox cbbProvince;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbbRank;
        private System.Windows.Forms.ComboBox cbbUsed;
        private System.Windows.Forms.ComboBox cbbGroup;
        private System.Windows.Forms.ComboBox cbbAmphur;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbbJoin;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtTotal;
    }
}