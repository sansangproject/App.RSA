using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmKnowledgeBanknote : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public clsInsert Insert = new clsInsert();
        public clsFunction Fn = new clsFunction();
        public clsMessage Message = new clsMessage();
        public clsDate Date = new clsDate();
        public clsImage Images = new clsImage();
        public string strSql = "SELECT [BANKNOTE_ID] " +
                                ",[BANKNOTE_NAME] " +
                                ",[BANKNOTE_PRICE] " +
                                ",[BANKNOTE_DATE] " +
                                ",[BANKNOTE_DATE_CHANGE] " +
                                "FROM  [dbo].[TB_BANKNOTE] " +
                                "WHERE 1 = 1 ";

        public FrmKnowledgeBanknote(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmKnowledgeBanknote_Load(object sender, EventArgs e)
        {
            txtSSearch.Focus();
            picFont.Image = null;
            picBack.Image = null;
            txtBrowseFont.Clear();
            txtBrowseBack.Clear();
            txtSSearch.Clear();
            txtName.Clear();
            txtWidth.Clear();
            txtHight.Clear();
            txtPrice.Clear();
            dtDate.Value = DateTime.Now;
            dtPay.Value = DateTime.Now;
            txtChange.Clear();
            txtDetailFont.Clear();
            txtDetailBack.Clear();

            getDataGrid(strSql + " ORDER BY [BANKNOTE_DATE] ");
        }

        public void getDataGrid(string strSql)
        {
            int row = Fn._countRow(strSql).Rows.Count;

            if (row == 0)
            {
                //Fn.ShowImageNull();
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                txtSSearch.Focus();
            }
            else
            {
                Fn._showGridViewDateFormat(dataGridView, strSql, "รหัส", "ชื่อ", "ราคา", "ประกาศใช้", "จ่ายแลก");
                picExcel.Visible = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            if (txtSSearch.Text != "")
            {
                string strSqlWhere = "";
                string valueSearch = txtSSearch.Text;

                try
                {
                    strSqlWhere = "SELECT [BANKNOTE_ID] " +
                                    "      ,[BANKNOTE_NAME] " +
                                    "      ,[BANKNOTE_PRICE] " +
                                    "      ,[BANKNOTE_DATE] " +
                                    "      ,[BANKNOTE_DATE_CHANGE] " +
                                    "  FROM  [dbo].[TB_BANKNOTE] " +
                                    "  WHERE [BANKNOTE_ID] LIKE '%" + valueSearch + "%' " +
                                    "      OR [BANKNOTE_NAME] LIKE '%" + valueSearch + "%' " +
                                    "      OR [BANKNOTE_SIZE_UNIT]LIKE '%" + valueSearch + "%' " +
                                    "      OR [BANKNOTE_SIZE_WIDTH]LIKE '%" + valueSearch + "%' " +
                                    "      OR [BANKNOTE_SIZE_HIGH]LIKE '%" + valueSearch + "%' " +
                                    "      OR [BANKNOTE_PRICE]LIKE '%" + valueSearch + "%' " +
                                    "      OR [BANKNOTE_PRICE_CHANGE]LIKE '%" + valueSearch + "%' " +
                                    "      OR [BANKNOTE_DATE]LIKE '%" + valueSearch + "%' " +
                                    "      OR [BANKNOTE_DATE_CHANGE]LIKE '%" + valueSearch + "%' " +
                                    "      OR [BANKNOTE_PIC_FONT_DETAIL]LIKE '%" + valueSearch + "%' " +
                                    "      OR [BANKNOTE_PIC_BEHIDE_DETAIL]LIKE '%" + valueSearch + "%' " +
                                    "      OR [BANKNOTE_PIC_FONT]LIKE '%" + valueSearch + "%' " +
                                    "      OR [BANKNOTE_PIC_BEHIDE]LIKE '%" + valueSearch + "%' " +
                                    "      ORDER BY [BANKNOTE_DATE] ";

                    lblSearch.Text = "' " + txtSSearch.Text + " '";
                    lblHSearch.Text = "คำค้นหา";
                    getDataGrid(strSqlWhere);
                }
                catch (Exception ex)
                {
                    Message.ShowMesError("Search", ex.ToString());
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Insert._banknote(
                  txtName.Text
                , "มิลลิเมตร"
                , Convert.ToDecimal(txtWidth.Text)
                , Convert.ToDecimal(txtHight.Text)
                , txtPrice.Text, txtChange.Text
                , Date.GetDate(dtp: dtDate)
                , Date.GetDate(dtp: dtPay)
                , txtDetailFont.Text
                , txtDetailBack.Text
                , txtBrowseFont.Text
                , txtBrowseBack.Text
                , strUserId
                );

                FrmKnowledgeBanknote_Load(sender, e);
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];

                string strSql = "SELECT * FROM [TB_BANKNOTE] WHERE [TB_BANKNOTE].[BANKNOTE_ID] = '" + row.Cells["BANKNOTE_ID"].Value.ToString() + "'";

                DataTable dt = new DataTable();
                dt = Fn._setValue(strSql);

                txtId.Text = dt.Rows[0]["BANKNOTE_ID"].ToString();
                txtName.Text = dt.Rows[0]["BANKNOTE_NAME"].ToString();
                txtWidth.Text = dt.Rows[0]["BANKNOTE_SIZE_WIDTH"].ToString();
                txtHight.Text = dt.Rows[0]["BANKNOTE_SIZE_HIGH"].ToString();
                lblUnit.Text = dt.Rows[0]["BANKNOTE_SIZE_UNIT"].ToString();
                txtPrice.Text = dt.Rows[0]["BANKNOTE_PRICE"].ToString();
                txtChange.Text = dt.Rows[0]["BANKNOTE_PRICE_CHANGE"].ToString();
                dtDate.Text = dt.Rows[0]["BANKNOTE_DATE"].ToString();
                dtPay.Text = dt.Rows[0]["BANKNOTE_DATE_CHANGE"].ToString();
                txtBrowseFont.Text = dt.Rows[0]["BANKNOTE_PIC_FONT"].ToString();
                txtBrowseBack.Text = dt.Rows[0]["BANKNOTE_PIC_BEHIDE"].ToString();
                txtDetailFont.Text = dt.Rows[0]["BANKNOTE_PIC_FONT_DETAIL"].ToString();
                txtDetailBack.Text = dt.Rows[0]["BANKNOTE_PIC_BEHIDE_DETAIL"].ToString();

                Images.ShowImage(picFont, Code: dt.Rows[0]["BANKNOTE_PIC_FONT"].ToString());
                Images.ShowImage(picBack, Code: dt.Rows[0]["BANKNOTE_PIC_BEHIDE"].ToString());
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            FrmKnowledgeBanknote_Load(sender, e);
        }

        private void picExcel_Click(object sender, EventArgs e)
        {
            Fn._excel(dataGridView, "");
        }

        private void btnBrowseFont_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = new Bitmap(open.FileName);
                txtBrowseFont.Text = open.FileName;
                open.RestoreDirectory = true;
            }

            Images.ShowImage(picFont, Code: txtBrowseFont.Text);
        }

        private void btnBrowseBack_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Image img = new Bitmap(open.FileName);
                txtBrowseBack.Text = open.FileName;
                open.RestoreDirectory = true;
            }

            Images.ShowImage(picBack, Code: txtBrowseBack.Text);
        }

        private void viewFont_Click(object sender, EventArgs e)
        {
            if (txtBrowseFont.Text != "")
            {
                FrmShowImage FrmShowImage = new FrmShowImage(txtId.Text);
                FrmShowImage.Show();
            }
        }

        private void viewBack_Click(object sender, EventArgs e)
        {
            if (txtBrowseBack.Text != "")
            {
                FrmShowImage FrmShowImage = new FrmShowImage(txtId.Text);
                FrmShowImage.Show();
            }
        }

        private void txtSearchkeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnSearch);
        }
    }
}