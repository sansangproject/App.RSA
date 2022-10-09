using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SANSANG.Class; 
using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmManageImage : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string strErr = "";
        public string strAppCode = "IMGMAN00";
        public string strAppName = "FrmManageImage";

        private clsImage Image = new clsImage();
        private clsLog Log = new clsLog();

        public FrmManageImage(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();
            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
        }

        private void FrmManageImage_Load(object sender, EventArgs e)
        {

        }

        public void getDataGrid(DataTable dt)
        {
            dataGridView.RowTemplate.Height = 80;
            dataGridView.DataSource = dt;

            //Visible Columns
            dataGridView.Columns["SNo"].Visible = false;
            dataGridView.Columns["ImageId"].Visible = false;
            dataGridView.Columns["ImageCode"].Visible = false;
            dataGridView.Columns["ImageRef"].Visible = false;
            dataGridView.Columns["ImageType"].Visible = false;
            dataGridView.Columns["ImageSize"].Visible = false;
            dataGridView.Columns["ImagePath"].Visible = false;
            dataGridView.Columns["StatusNameEn"].Visible = false;
            dataGridView.Columns["ImageName"].Visible = false;
            dataGridView.Columns["ImageLocation"].Visible = false;
            dataGridView.Columns["ImagePathLocation"].Visible = false;

            //DataGridViewTextBoxColumn
            //DataGridViewCheckBoxColumn
            //DataGridViewImageColumn
            //DataGridViewButtonColumn
            //DataGridViewComboBoxColumn
            //DataGridViewLinkColumn

            //dataGridView.Columns.AddRange(new DataGridViewColumn[] { colImageNo, colImageCode });

            //Set Running Column
            try
            {
                DataGridViewTextBoxColumn ColumnSNo = new DataGridViewTextBoxColumn();
                ColumnSNo.Name = "SNos";
                ColumnSNo.HeaderText = "ลำดับ";
                ColumnSNo.Width = 20;
                dataGridView.Columns.Insert(1, ColumnSNo);

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    row.Cells["SNos"].Value = row.Cells["SNo"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, "{Set SNo Column} " + ex.Message);
            }

            //Set Name Column
            try
            {
                DataGridViewTextBoxColumn ColumnImageCode = new DataGridViewTextBoxColumn();
                ColumnImageCode.Name = "ImageCodes";
                ColumnImageCode.HeaderText = "รหัส";
                ColumnImageCode.Width = 100;
                dataGridView.Columns.Insert(2, ColumnImageCode);

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    row.Cells["ImageCodes"].Value = row.Cells["ImageCode"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, "{Set Image Code Column} " + ex.Message);
            }

            //Set Type Column
            try
            {
                DataGridViewTextBoxColumn ColumnImageType = new DataGridViewTextBoxColumn();
                ColumnImageType.Name = "ImageTypes";
                ColumnImageType.HeaderText = "ประเภท";
                ColumnImageType.Width = 50;
                dataGridView.Columns.Insert(3, ColumnImageType);

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    row.Cells["ImageTypes"].Value = row.Cells["ImageType"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, "{Set Image Type Column} " + ex.Message);
            }

            //Set Image Column
            try
            {
                DataGridViewImageColumn colImage = new DataGridViewImageColumn();
                colImage.Name = "Images";
                colImage.HeaderText = "ภาพ";
                colImage.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dataGridView.Columns.Insert(4, colImage);

                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    //if (row.Cells["ImageType"].Value.ToString() == ".pdf")
                    //{
                    //    row.Cells["Images"].Value = Image.FromFile("\\\\Tss-server\\SERVER-DATA\\Pictures\\Pictures.Program\\I1-8BE417B244-6.jpg");
                    //}
                    //else
                    //{
                    //    if (File.Exists(row.Cells["ImagePathLocation"].Value.ToString()))
                    //    {
                    //        row.Cells["Images"].Value = Image.FromFile(row.Cells["ImagePathLocation"].Value.ToString());
                    //    }
                    //    else
                    //    {
                    //        row.Cells["Images"].Value = Image.FromFile("\\\\Tss-server\\SERVER-DATA\\Pictures\\Pictures.Program\\I1-IMAGE00000-1.jpg");
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, "{Set Image Column} " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}