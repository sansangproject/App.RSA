namespace SANSANG
{
    partial class FrmCalLoan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCalLoan));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtTime = new System.Windows.Forms.DateTimePicker();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtM = new System.Windows.Forms.TextBox();
            this.txtD = new System.Windows.Forms.TextBox();
            this.txtBaht = new System.Windows.Forms.TextBox();
            this.txtMonth = new System.Windows.Forms.TextBox();
            this.txtInterest = new System.Windows.Forms.TextBox();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.btnShowXLS = new System.Windows.Forms.Button();
            this.btnShowPDF = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbPayType = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 90);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(1325, 527);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.dtTime);
            this.panel1.Controls.Add(this.txtY);
            this.panel1.Controls.Add(this.txtM);
            this.panel1.Controls.Add(this.txtD);
            this.panel1.Controls.Add(this.txtBaht);
            this.panel1.Controls.Add(this.txtMonth);
            this.panel1.Controls.Add(this.txtInterest);
            this.panel1.Controls.Add(this.txtNumber);
            this.panel1.Controls.Add(this.btnShowXLS);
            this.panel1.Controls.Add(this.btnShowPDF);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblID);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbbPayType);
            this.panel1.Location = new System.Drawing.Point(23, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1291, 473);
            this.panel1.TabIndex = 9;
            // 
            // dtTime
            // 
            this.dtTime.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtTime.Location = new System.Drawing.Point(329, 194);
            this.dtTime.Name = "dtTime";
            this.dtTime.Size = new System.Drawing.Size(15, 25);
            this.dtTime.TabIndex = 23;
            this.dtTime.TabStop = false;
            this.dtTime.ValueChanged += new System.EventHandler(this.dtTime_ValueChanged);
            // 
            // txtY
            // 
            this.txtY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtY.Location = new System.Drawing.Point(270, 194);
            this.txtY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(53, 25);
            this.txtY.TabIndex = 7;
            this.txtY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumberkeyPress);
            // 
            // txtM
            // 
            this.txtM.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtM.Location = new System.Drawing.Point(211, 194);
            this.txtM.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtM.Name = "txtM";
            this.txtM.Size = new System.Drawing.Size(53, 25);
            this.txtM.TabIndex = 6;
            this.txtM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumberkeyPress);
            // 
            // txtD
            // 
            this.txtD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtD.Location = new System.Drawing.Point(152, 194);
            this.txtD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtD.Name = "txtD";
            this.txtD.Size = new System.Drawing.Size(53, 25);
            this.txtD.TabIndex = 5;
            this.txtD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumberkeyPress);
            // 
            // txtBaht
            // 
            this.txtBaht.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtBaht.Location = new System.Drawing.Point(152, 152);
            this.txtBaht.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBaht.Name = "txtBaht";
            this.txtBaht.Size = new System.Drawing.Size(112, 25);
            this.txtBaht.TabIndex = 4;
            this.txtBaht.TextChanged += new System.EventHandler(this.txtBaht_TextChanged);
            this.txtBaht.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumberkeyPress);
            // 
            // txtMonth
            // 
            this.txtMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtMonth.Location = new System.Drawing.Point(152, 110);
            this.txtMonth.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(53, 25);
            this.txtMonth.TabIndex = 3;
            this.txtMonth.TextChanged += new System.EventHandler(this.txtMonth_TextChanged);
            this.txtMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumberkeyPress);
            // 
            // txtInterest
            // 
            this.txtInterest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtInterest.Location = new System.Drawing.Point(152, 68);
            this.txtInterest.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtInterest.Name = "txtInterest";
            this.txtInterest.Size = new System.Drawing.Size(57, 25);
            this.txtInterest.TabIndex = 2;
            this.txtInterest.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumberkeyPress);
            // 
            // txtNumber
            // 
            this.txtNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.txtNumber.Location = new System.Drawing.Point(152, 26);
            this.txtNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(112, 25);
            this.txtNumber.TabIndex = 1;
            this.txtNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumberkeyPress);
            // 
            // btnShowXLS
            // 
            this.btnShowXLS.BackColor = System.Drawing.Color.Transparent;
            this.btnShowXLS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowXLS.ForeColor = System.Drawing.Color.White;
            this.btnShowXLS.Image = ((System.Drawing.Image)(resources.GetObject("btnShowXLS.Image")));
            this.btnShowXLS.Location = new System.Drawing.Point(385, 68);
            this.btnShowXLS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShowXLS.Name = "btnShowXLS";
            this.btnShowXLS.Size = new System.Drawing.Size(50, 60);
            this.btnShowXLS.TabIndex = 21;
            this.btnShowXLS.UseVisualStyleBackColor = false;
            this.btnShowXLS.Click += new System.EventHandler(this.btnShowXLS_Click);
            // 
            // btnShowPDF
            // 
            this.btnShowPDF.BackColor = System.Drawing.Color.Transparent;
            this.btnShowPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowPDF.ForeColor = System.Drawing.Color.White;
            this.btnShowPDF.Image = ((System.Drawing.Image)(resources.GetObject("btnShowPDF.Image")));
            this.btnShowPDF.Location = new System.Drawing.Point(329, 68);
            this.btnShowPDF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnShowPDF.Name = "btnShowPDF";
            this.btnShowPDF.Size = new System.Drawing.Size(50, 60);
            this.btnShowPDF.TabIndex = 21;
            this.btnShowPDF.UseVisualStyleBackColor = false;
            this.btnShowPDF.Click += new System.EventHandler(this.btnShowPDF_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(22, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "อัตราดอกเบี้ย (% ต่อปี) ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(82, 234);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "วิธีการชำระ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(76, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "วันที่เริ่มชำระ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(40, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "จำนวนเงินผ่อนชำระ";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblID.Location = new System.Drawing.Point(54, 29);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(92, 17);
            this.lblID.TabIndex = 10;
            this.lblID.Text = "ยอดเงินกู้ (บาท) :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(31, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "จำนวนเดือนผ่อนชำระ";
            // 
            // cbbPayType
            // 
            this.cbbPayType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.cbbPayType.FormattingEnabled = true;
            this.cbbPayType.Location = new System.Drawing.Point(152, 236);
            this.cbbPayType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbbPayType.Name = "cbbPayType";
            this.cbbPayType.Size = new System.Drawing.Size(138, 25);
            this.cbbPayType.TabIndex = 20;
            this.cbbPayType.TabStop = false;
            this.cbbPayType.SelectedIndexChanged += new System.EventHandler(this.cbbPayType_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Josefin Sans", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(239, 48);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(412, 37);
            this.textBox1.TabIndex = 44;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "| PROGRAM CALCULATOR LOAN AUTO";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Location = new System.Drawing.Point(-2, -10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1355, 102);
            this.groupBox3.TabIndex = 45;
            this.groupBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, -57);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(243, 270);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.btnAdd);
            this.groupBox5.Controls.Add(this.btnExit);
            this.groupBox5.Controls.Add(this.btnEdit);
            this.groupBox5.Controls.Add(this.btnClear);
            this.groupBox5.Controls.Add(this.btnDelete);
            this.groupBox5.Location = new System.Drawing.Point(1108, 17);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(229, 74);
            this.groupBox5.TabIndex = 45;
            this.groupBox5.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.Transparent;
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(14, 23);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 36);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.Transparent;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(182, 23);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(36, 36);
            this.btnExit.TabIndex = 11;
            this.btnExit.Tag = "ออก";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.ForeColor = System.Drawing.Color.Transparent;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(56, 23);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(36, 36);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.Transparent;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.Location = new System.Drawing.Point(140, 23);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(36, 36);
            this.btnClear.TabIndex = 10;
            this.btnClear.Tag = "เคลียร์";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.Transparent;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(98, 23);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 36);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Mitr ExtraLight", 14F);
            this.textBox2.Location = new System.Drawing.Point(259, 75);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(199, 26);
            this.textBox2.TabIndex = 44;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "โปรแกรมคำนวณดอกเบี้ย";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Location = new System.Drawing.Point(2, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1346, 626);
            this.groupBox4.TabIndex = 46;
            this.groupBox4.TabStop = false;
            // 
            // FrmCalLoan
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
            this.Name = "FrmCalLoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmCalLoan_Load);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Button btnShowPDF;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.ComboBox cbbPayType;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtInterest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.TextBox txtM;
        private System.Windows.Forms.TextBox txtD;
        private System.Windows.Forms.TextBox txtBaht;
        private System.Windows.Forms.TextBox txtMonth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtTime;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnShowXLS;
    }
}