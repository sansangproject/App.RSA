namespace SANSANG
{
    partial class FrmMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMaster));
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblHSearch = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.gbData = new System.Windows.Forms.GroupBox();
            this.lblCondition = new System.Windows.Forms.Label();
            this.picExcel = new System.Windows.Forms.PictureBox();
            this.pbBanner = new System.Windows.Forms.PictureBox();
            this.gbForm = new System.Windows.Forms.GroupBox();
            this.pnButton = new System.Windows.Forms.Panel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.pnMain = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.gbData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).BeginInit();
            this.gbForm.SuspendLayout();
            this.pnButton.SuspendLayout();
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
            this.lblHSearch.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblHSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
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
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dataGridView.Location = new System.Drawing.Point(19, 46);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.SteelBlue;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1297, 198);
            this.dataGridView.TabIndex = 40;
            this.dataGridView.TabStop = false;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // No
            // 
            this.No.FillWeight = 50F;
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(1270, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 20);
            this.label6.TabIndex = 41;
            this.label6.Text = "รายการ";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(1159, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 20);
            this.label7.TabIndex = 41;
            this.label7.Text = "ข้อมูลทั้งหมด";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblCount.Location = new System.Drawing.Point(1246, 19);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(15, 17);
            this.lblCount.TabIndex = 41;
            this.lblCount.Text = "0";
            // 
            // gbData
            // 
            this.gbData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbData.Controls.Add(this.lblCondition);
            this.gbData.Controls.Add(this.lblHSearch);
            this.gbData.Controls.Add(this.picExcel);
            this.gbData.Controls.Add(this.dataGridView);
            this.gbData.Controls.Add(this.label6);
            this.gbData.Controls.Add(this.lblSearch);
            this.gbData.Controls.Add(this.lblCount);
            this.gbData.Controls.Add(this.label7);
            this.gbData.Font = new System.Drawing.Font("Mitr Light", 9.75F);
            this.gbData.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.gbData.Location = new System.Drawing.Point(6, 360);
            this.gbData.Name = "gbData";
            this.gbData.Size = new System.Drawing.Size(1333, 260);
            this.gbData.TabIndex = 43;
            this.gbData.TabStop = false;
            // 
            // lblCondition
            // 
            this.lblCondition.AutoSize = true;
            this.lblCondition.Font = new System.Drawing.Font("Mitr Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.lblCondition.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblCondition.Location = new System.Drawing.Point(89, 19);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(53, 20);
            this.lblCondition.TabIndex = 41;
            this.lblCondition.Text = "คำค้นหา";
            // 
            // picExcel
            // 
            this.picExcel.Image = ((System.Drawing.Image)(resources.GetObject("picExcel.Image")));
            this.picExcel.Location = new System.Drawing.Point(26, 48);
            this.picExcel.Name = "picExcel";
            this.picExcel.Size = new System.Drawing.Size(28, 21);
            this.picExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picExcel.TabIndex = 42;
            this.picExcel.TabStop = false;
            this.picExcel.Visible = false;
            this.picExcel.Click += new System.EventHandler(this.picExcel_Click);
            // 
            // pbBanner
            // 
            this.pbBanner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbBanner.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pbBanner.Location = new System.Drawing.Point(6, 10);
            this.pbBanner.Name = "pbBanner";
            this.pbBanner.Size = new System.Drawing.Size(1331, 93);
            this.pbBanner.TabIndex = 23;
            this.pbBanner.TabStop = false;
            // 
            // gbForm
            // 
            this.gbForm.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbForm.BackColor = System.Drawing.Color.White;
            this.gbForm.Controls.Add(this.pnMain);
            this.gbForm.Controls.Add(this.pnButton);
            this.gbForm.Controls.Add(this.gbData);
            this.gbForm.Controls.Add(this.pbBanner);
            this.gbForm.Location = new System.Drawing.Point(2, 2);
            this.gbForm.Name = "gbForm";
            this.gbForm.Size = new System.Drawing.Size(1343, 626);
            this.gbForm.TabIndex = 46;
            this.gbForm.TabStop = false;
            // 
            // pnButton
            // 
            this.pnButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnButton.Controls.Add(this.btnSearch);
            this.pnButton.Controls.Add(this.label1);
            this.pnButton.Controls.Add(this.label23);
            this.pnButton.Controls.Add(this.btnAdd);
            this.pnButton.Controls.Add(this.btnExit);
            this.pnButton.Controls.Add(this.label20);
            this.pnButton.Controls.Add(this.label24);
            this.pnButton.Controls.Add(this.btnEdit);
            this.pnButton.Controls.Add(this.btnSend);
            this.pnButton.Controls.Add(this.label21);
            this.pnButton.Controls.Add(this.label25);
            this.pnButton.Controls.Add(this.btnDelete);
            this.pnButton.Controls.Add(this.btnClear);
            this.pnButton.Controls.Add(this.label22);
            this.pnButton.Location = new System.Drawing.Point(1017, 24);
            this.pnButton.Name = "pnButton";
            this.pnButton.Size = new System.Drawing.Size(310, 66);
            this.pnButton.TabIndex = 224;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(7, 6);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(36, 36);
            this.btnSearch.TabIndex = 90;
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(8, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 89;
            this.label1.Text = "Alt+F";
            // 
            // label23
            // 
            this.label23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.DarkRed;
            this.label23.Location = new System.Drawing.Point(263, 44);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(39, 13);
            this.label23.TabIndex = 74;
            this.label23.Text = "Ctrl+X";
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.Transparent;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(49, 6);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 36);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(264, 6);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(36, 36);
            this.btnExit.TabIndex = 11;
            this.btnExit.Tag = "ออก";
            this.btnExit.UseVisualStyleBackColor = false;
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.DarkRed;
            this.label20.Location = new System.Drawing.Point(48, 44);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(39, 13);
            this.label20.TabIndex = 70;
            this.label20.Text = "Ctrl+S";
            // 
            // label24
            // 
            this.label24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.DarkRed;
            this.label24.Location = new System.Drawing.Point(222, 44);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(37, 13);
            this.label24.TabIndex = 71;
            this.label24.Text = "Alt+N";
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(91, 6);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(36, 36);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Image = ((System.Drawing.Image)(resources.GetObject("btnSend.Image")));
            this.btnSend.Location = new System.Drawing.Point(222, 6);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(36, 36);
            this.btnSend.TabIndex = 10;
            this.btnSend.Tag = "ยกยอด";
            this.btnSend.UseVisualStyleBackColor = false;
            // 
            // label21
            // 
            this.label21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.DarkRed;
            this.label21.Location = new System.Drawing.Point(90, 44);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(39, 13);
            this.label21.TabIndex = 72;
            this.label21.Text = "Ctrl+E";
            // 
            // label25
            // 
            this.label25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.Color.DarkRed;
            this.label25.Location = new System.Drawing.Point(180, 44);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(36, 13);
            this.label25.TabIndex = 75;
            this.label25.Text = "Alt+C";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(135, 6);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 36);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(180, 6);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(36, 36);
            this.btnClear.TabIndex = 10;
            this.btnClear.Tag = "เคลียร์";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.DarkRed;
            this.label22.Location = new System.Drawing.Point(133, 44);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 13);
            this.label22.TabIndex = 73;
            this.label22.Text = "Ctrl+D";
            // 
            // pnMain
            // 
            this.pnMain.Location = new System.Drawing.Point(6, 109);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(1331, 255);
            this.pnMain.TabIndex = 225;
            // 
            // FrmMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(1347, 630);
            this.Controls.Add(this.gbForm);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Frm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.gbData.ResumeLayout(false);
            this.gbData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBanner)).EndInit();
            this.gbForm.ResumeLayout(false);
            this.pnButton.ResumeLayout(false);
            this.pnButton.PerformLayout();
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
        private System.Windows.Forms.GroupBox gbData;
        private System.Windows.Forms.GroupBox gbForm;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.PictureBox pbBanner;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.Panel pnMain;
        private System.Windows.Forms.Panel pnButton;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label22;
    }
}