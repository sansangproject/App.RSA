using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmSaveStamp : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsUpdate TSSUpdate = new clsUpdate();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();

        public string strSql = "SELECT [STAMP_NO] " +
                                "      ,[STAMP_TITLE_TH] " +
                                "      ,[DENOMINATION_DISPLAYTH] " +
                                "      ,[STAMP_DATE]   " +
                                "      ,[STATUS_DISPLAYTH]    " +
                                "      ,[STAMP_ID]    " +
                                "  FROM [STAMPDBMASTER].[dbo].[STP_DATA] " +
                                "  INNER JOIN [STAMPDBMASTER].[dbo].[STP_DENOMINATION] " +
                                "  ON [STAMPDBMASTER].[dbo].[STP_DATA].[STAMP_DENOMINATION] =  " +
                                "  [STAMPDBMASTER].[dbo].[STP_DENOMINATION].[DENOMINATION_ID] " +
                                "  INNER JOIN [STAMPDBMASTER].[dbo].[STP_STATUS] " +
                                "  ON [STAMPDBMASTER].[dbo].[STP_DATA].[STAMP_STATUS] = " +
                                "  [STAMPDBMASTER].[dbo].[STP_STATUS].[STATUS_ID] ";

        public FrmSaveStamp(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmSaveStamp_Load(object sender, EventArgs e)
        {
            txtSSearch.Clear();
            txtSSearch.Focus();
            pbSStamp.Image = null;
            txtSPrint.Clear();
            txtSPrinter.Clear();
            txtSSize.Clear();
            txtSLetterDesingeCompany.Clear();
            txtSLetterDesinge.Clear();
            txtSCoverPrice.Clear();
            txtSNumber.Clear();
            txtSColor.Clear();
            txtSDesingeCompany.Clear();
            txtSDesinge.Clear();
            txtSStatus.Clear();
            txtSEN.Clear();
            txtSTH.Clear();
            txtSPrice.Clear();
            txtSNo.Clear();
            txtSDate.Clear();
            txtSID.Clear();

            getDataGrid(strSql);
        }

        public void getDataGrid(string strSql)
        {
            int row = Fn._countRowST(strSql).Rows.Count;

            if (row == 0)
            {
                //Fn.ShowImageNull();
                dataGridView.DataSource = null;
                picExcel.Visible = false;
            }
            else
            {
                Fn._showGridViewStamp(dataGridView, strSql, "เลขที่่ชุด", "ชื่อชุด", "ชนิดราคา", "วันแรกจำหน่าย", "สถานะ", "");
                picExcel.Visible = true;
            }

            //lblCount.Text = Search._searchForGridST(strSql).Rows.Count.ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSSeachkeyPress(object sender, KeyPressEventArgs e)
        {
            Fn.btnEnter(e, btnSSearch);
        }

        private void btnSSearch_Click(object sender, EventArgs e)
        {
            SSearchData();
        }

        public void SSearchData()
        {
            if (txtSSearch.Text != "")
            {
                string strSqlWhere = "";
                string valueSearch = txtSSearch.Text;

                try
                {
                    strSqlWhere = "";

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

        private void btnClear_Click(object sender, EventArgs e)
        {
            FrmSaveStamp_Load(sender, e);
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];

                string strSql = "SELECT * " +
                                    "  FROM STP_DATA " +
                                    "  INNER JOIN STP_PROGRAM " +
                                    "  ON STP_PROGRAM.PROGRAM_ID = STP_DATA.STAMP_PROGRAM " +
                                    "  INNER JOIN STP_CATEGORY " +
                                    "  ON STP_CATEGORY.CATEGORY_ID = STP_DATA.STAMP_CATEGORY " +
                                    "  INNER JOIN STP_DENOMINATION " +
                                    "  ON STP_DENOMINATION.DENOMINATION_ID = STP_DATA.STAMP_DENOMINATION " +
                                    "  INNER JOIN STP_ORIENTATION " +
                                    "  ON STP_ORIENTATION.ORIENTATION_ID = STP_DATA.STAMP_ORIENTATION " +
                                    "  INNER JOIN  STP_STYLE " +
                                    "  ON STP_STYLE.STYLE_ID = STP_DATA.STAMP_STYLE " +
                                    "  INNER JOIN STP_DESIGNER " +
                                    "  ON STP_DESIGNER.DESIGNER_ID = STP_DATA.STAMP_DESIGNER " +
                                    "  INNER JOIN STP_COMPANY " +
                                    "  ON STP_COMPANY.COMPANY_ID = STP_DATA.STAMP_COVERDESIGNERCOMPANY " +
                                    "  INNER JOIN STP_PRINTER " +
                                    "  ON STP_PRINTER.PRINTER_ID = STP_DATA.STAMP_PRINTER " +
                                    "  INNER JOIN STP_PROCESS " +
                                    "  ON STP_PROCESS.PROCESS_ID = STP_DATA.STAMP_PROCESS " +
                                    "  INNER JOIN STP_TYPE " +
                                    "  ON STP_TYPE.TYPE_ID = STP_DATA.STAMP_TYPE " +
                                    "  INNER JOIN STP_DESIGNTYPE " +
                                    "  ON STP_DESIGNTYPE.DESIGNTYPE_ID = STP_DATA.STAMP_DESIGNTYPE   " +
                                    "  INNER JOIN STP_STATUS " +
                                    "  ON STP_STATUS.STATUS_ID = STP_DATA.STAMP_STATUS " +
                                    " WHERE STP_DATA.STAMP_ID = '" + row.Cells["STAMP_ID"].Value.ToString() + "'";

                DataTable dt = new DataTable();
                dt = Fn._setValueStamp(strSql);

                txtSID.Text = dt.Rows[0]["STAMP_ID"].ToString();
                Fn._showImageByID(pbSStamp, dt.Rows[0]["STAMP_ID"].ToString(), ".jpg", dt.Rows[0]["STAMP_STAMPPICTURE"].ToString());
                txtSPrint.Text = Fn._formatNumber(dt.Rows[0]["STAMP_NUMBEROFPRINT"].ToString(), "Number") + " ดวง";
                txtSSize.Text = dt.Rows[0]["STAMP_HIGH"].ToString() + " x " + dt.Rows[0]["STAMP_WIDTH"].ToString();
                txtSOrientation.Text = dt.Rows[0]["ORIENTATION_DISPLAYTH"].ToString();
                txtSNo.Text = dt.Rows[0]["STAMP_NO"].ToString();
                txtSTH.Text = dt.Rows[0]["STAMP_TITLE_TH"].ToString();
                txtSEN.Text = dt.Rows[0]["STAMP_TITLE_EN"].ToString();
                txtSDate.Text = Fn._formatDate(dt.Rows[0]["STAMP_DATE"].ToString(), "dMMMMyyyy");
                txtSNumber.Text = Fn._formatNumber(dt.Rows[0]["STAMP_SHEETCOMPOSITION"].ToString(), "Number") + " ดวง";
                txtSPrice.Text = dt.Rows[0]["DENOMINATION_DISPLAYTH"].ToString();
                txtSPrinter.Text = dt.Rows[0]["PRINTER_DISPLAYTH"].ToString();
                txtSStatus.Text = dt.Rows[0]["STATUS_DISPLAYTH"].ToString();
                txtSDesinge.Text = dt.Rows[0]["DESIGNER_NAMEDISPLAYTH"].ToString();
                txtSDesingeCompany.Text = dt.Rows[0]["COMPANY_DISPLAYTH"].ToString();
                txtSColor.Text = dt.Rows[0]["PROCESS_DISPLAYTH"].ToString();
                txtSCoverPrice.Text = Fn._formatNumber(dt.Rows[0]["STAMP_FIRSTDAYCOVER"].ToString(), "Baht") + " บาท";
                txtSLetterDesinge.Text = dt.Rows[0]["STAMP_COVERDESIGNER"].ToString();
                txtSLetterDesingeCompany.Text = dt.Rows[0]["STAMP_COVERDESIGNERCOMPANY"].ToString();
                txtSSearch.Focus();
            }
        }

        private void txtSLetterDesinge_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strSql = "SELECT * FROM STP_DESIGNER WHERE STP_DESIGNER.DESIGNER_ID = '" + txtSLetterDesinge.Text + "'";
                DataTable dt = new DataTable();
                dt = Fn._setValueStamp(strSql);
                txtSLetterDesinge.Text = dt.Rows[0]["DESIGNER_NAMEDISPLAYTH"].ToString();
            }
            catch (Exception)
            {
            }
        }

        private void txtSLetterDesingeCompany_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strSql = "SELECT * FROM STP_COMPANY WHERE STP_COMPANY.COMPANY_ID = '" + txtSLetterDesingeCompany.Text + "'";
                DataTable dt = new DataTable();
                dt = Fn._setValueStamp(strSql);
                txtSLetterDesingeCompany.Text = dt.Rows[0]["COMPANY_DISPLAYTH"].ToString();
            }
            catch (Exception)
            {
            }
        }
    }
}