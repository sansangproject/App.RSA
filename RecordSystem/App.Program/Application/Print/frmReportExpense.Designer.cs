namespace RecordSystemApplication.App.Program.Application.Print
{
    partial class frmReportExpense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReportExpense));
            this.btnPrint = new System.Windows.Forms.Button();
            this.label39 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbbCategory = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.ForeColor = System.Drawing.Color.Transparent;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.Location = new System.Drawing.Point(405, 77);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(36, 36);
            this.btnPrint.TabIndex = 93;
            this.btnPrint.TabStop = false;
            this.btnPrint.Tag = "เคลียร์";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.BackColor = System.Drawing.Color.Transparent;
            this.label39.Font = new System.Drawing.Font("DB HeaventRounded", 14.25F);
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label39.Location = new System.Drawing.Point(63, 77);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(40, 23);
            this.label39.TabIndex = 100;
            this.label39.Text = "Date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("DB HeaventRounded", 14.25F);
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(70, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 23);
            this.label3.TabIndex = 98;
            this.label3.Text = "วันที่";
            // 
            // dtDate
            // 
            this.dtDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtDate.CalendarForeColor = System.Drawing.Color.Black;
            this.dtDate.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtDate.CalendarTitleBackColor = System.Drawing.Color.White;
            this.dtDate.CalendarTitleForeColor = System.Drawing.Color.White;
            this.dtDate.CalendarTrailingForeColor = System.Drawing.Color.White;
            this.dtDate.CustomFormat = "dd MMM yyyy";
            this.dtDate.Font = new System.Drawing.Font("Mitr Light", 9.75F);
            this.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDate.Location = new System.Drawing.Point(147, 64);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(177, 28);
            this.dtDate.TabIndex = 99;
            this.dtDate.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("DB HeaventRounded", 14.25F);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label14.Location = new System.Drawing.Point(38, 127);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 23);
            this.label14.TabIndex = 103;
            this.label14.Text = "Category:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("DB HeaventRounded", 14.25F);
            this.label6.ForeColor = System.Drawing.Color.Gray;
            this.label6.Location = new System.Drawing.Point(55, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 23);
            this.label6.TabIndex = 101;
            this.label6.Text = "ประเภท";
            // 
            // cbbCategory
            // 
            this.cbbCategory.Enabled = false;
            this.cbbCategory.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbbCategory.Font = new System.Drawing.Font("Mitr Light", 9.75F);
            this.cbbCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbbCategory.FormattingEnabled = true;
            this.cbbCategory.Location = new System.Drawing.Point(147, 114);
            this.cbbCategory.Name = "cbbCategory";
            this.cbbCategory.Size = new System.Drawing.Size(177, 28);
            this.cbbCategory.TabIndex = 102;
            this.cbbCategory.Text = ":: Loading ::";
            // 
            // frmReportExpense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 306);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbbCategory);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtDate);
            this.Controls.Add(this.btnPrint);
            this.Font = new System.Drawing.Font("DB HeaventRounded", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmReportExpense";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbbCategory;
    }
}