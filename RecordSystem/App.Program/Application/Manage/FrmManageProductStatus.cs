using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmManageProductStatus : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsUpdate TSSUpdate = new clsUpdate();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();

        public string strSql = "SELECT [PRODUCT_STATUS_ID]" +
                                "      ,[PRODUCT_STATUS_NAME]" +
                                "      ,[STATUS_NAME_TH]" +
                                "      ,[PRODUCT_STATUS_CREATE_DATE]" +
                                "      ,[PRODUCT_STATUS_UPDATE_DATE]" +
                                "  FROM [TB_PRODUCT_STATUS]INNER JOIN [TB_STATUS]" +
                                "  ON [TB_PRODUCT_STATUS].[PRODUCT_STATUS_STATUS] = [TB_STATUS].[STATUS_ID]";

        public FrmManageProductStatus(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmManageProductStatus_Load(object sender, EventArgs e)
        {
            txtID.Enabled = true;
            txtID.Text = "";
            txtID.Focus();
            txtName.Text = "";
            Fn._listStatus(cbbStatus, "STATUS_NAME_TH", "STATUS_ID");
            cbbStatus.SelectedValue = 1;
            lblSearch.Text = "";

            getDataGrid(strSql);
        }

        public void getDataGrid(string strSql)
        {
            int row = Fn._countRow(strSql).Rows.Count;

            if (row == 0)
            {
                //Fn.ShowImageNull();
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                txtID.Focus();
            }
            else
            {
                Fn._showGridViewDateFormat(dataGridView, strSql, "รหัส", "ชื่อสถานะสินค้า", "สถานะ", "วันที่สร้าง", "วันที่แก้ไข");
                picExcel.Visible = true;
            }

            //lblCount.Text = Search._searchForGrid(strSql).Rows.Count.ToString("#,#");
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
            try
            {
                string txtSearch = "";
                string strSql = "SELECT [PRODUCT_STATUS_ID]" +
                                "      ,[PRODUCT_STATUS_NAME]" +
                                "      ,[STATUS_NAME_TH]" +
                                "      ,[PRODUCT_STATUS_CREATE_DATE]" +
                                "      ,[PRODUCT_STATUS_UPDATE_DATE]" +
                                "  FROM [TB_PRODUCT_STATUS]INNER JOIN [TB_STATUS]" +
                                "  ON [TB_PRODUCT_STATUS].[PRODUCT_STATUS_STATUS] = [TB_STATUS].[STATUS_ID] WHERE 1 = 1";

                if (txtID.Text != "")
                {
                    strSql += Fn._where("AND", txtID.Text, "[TB_PRODUCT_STATUS].[PRODUCT_STATUS_ID]");
                    txtSearch += Fn._textSearch(txtID.Text, "รหัส");
                }

                if (txtName.Text != "")
                {
                    strSql += Fn._where("AND", txtName.Text, "[TB_PRODUCT_STATUS].[PRODUCT_STATUS_NAME]");
                    txtSearch += Fn._textSearch(txtName.Text, "ชื่อสถานะสินค้า");
                }
                if (cbbStatus.SelectedIndex < -1)
                {
                    strSql += Fn._where("AND", cbbStatus.SelectedValue.ToString(), "[TB_PRODUCT_STATUS].[PRODUCT_STATUS_STATUS]");
                    txtSearch += Fn._textSearch(cbbStatus.Text, "สถานะ");
                }

                strSql += " ORDER BY [TB_PRODUCT_STATUS].[PRODUCT_STATUS_NAME]";

                lblSearch.Text = txtSearch;
                lblHSearch.Text = "คำค้นหา";
                getDataGrid(strSql);
            }
            catch (Exception ex)
            {
                Message.ShowMesError("Search", ex.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Fn._countRow("TB_PRODUCT_STATUS", "PRODUCT_STATUS_ID", txtID.Text).Rows.Count == 0)
            {
                Insert._productStatus
                    (
                    txtID.Text,
                    txtName.Text,
                    Fn.getComboboxId(cbbStatus),
                    strUserId
                    );
                FrmManageProductStatus_Load(sender, e);
            }
            else
            {
                Fn._showDuplicate();
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtID.Enabled = false;

                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];

                string strSql = "SELECT * FROM [TB_PRODUCT_STATUS] WHERE [PRODUCT_STATUS_ID] = '" + row.Cells["PRODUCT_STATUS_ID"].Value.ToString() + "'";

                DataTable dt = new DataTable();
                dt = Fn._setValue(strSql);

                txtID.Text = dt.Rows[0]["PRODUCT_STATUS_ID"].ToString();
                txtName.Text = dt.Rows[0]["PRODUCT_STATUS_NAME"].ToString();
                cbbStatus.SelectedValue = dt.Rows[0]["PRODUCT_STATUS_STATUS"].ToString();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete._delete("TB_PRODUCT_STATUS", "PRODUCT_STATUS_ID", txtID.Text);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            FrmManageProductStatus_Load(sender, e);
        }

        private void picExcel_Click(object sender, EventArgs e)
        {
            Fn._excel(dataGridView, "PRODUCT_STATUS");
        }
    }
}