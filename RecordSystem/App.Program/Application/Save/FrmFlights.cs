using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmFlights : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        private clsSearch Search = new clsSearch();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
        private clsDataList List = new clsDataList();
        private clsImage Images = new clsImage();
        private dbConnection db = new dbConnection();
        private clsDesign TSSDesign = new clsDesign();
        private clsFormat TSSFormat = new clsFormat();

        public string strSql = "SELECT [FLIGHT_ID]" +
            "      ,[FLIGHT_AIRLINE]" +
                                "      ,[FLIGHT_FROM]" +
                                "      ,[FLIGHT_TO]" +
                                "      ,[FLIGHT_DATE]" +
                                "  FROM [TB_FLIGHT] INNER JOIN [TB_STATUS]" +
                                "  ON [TB_FLIGHT].[FLIGHT_STATUS] = [TB_STATUS].[STATUS_ID] " +
                                "  ORDER BY [TB_FLIGHT].[FLIGHT_DATE]";

        public FrmFlights(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmSaveFlight_Load(object sender, EventArgs e)
        {
            Fn._listlogo(cbbLogo, "LOGO_NAME", "LOGO_ID", "WHERE LOGO_STATUS = 'F' ORDER BY LOGO_NAME");
            cbbLogo.SelectedValue = 0;

            Images.ShowDefault(picAirline);

            txtSearch.Text = "";
            lblSearch.Text = "";
            getDataGrid(strSql);
        }

        public void getDataGrid(string strSql)
        {
            int row = Fn._countRow(strSql).Rows.Count;

            if (row == 0)
            {
                Images.ShowDefault(picAirline);
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                txtSearch.Focus();
            }
            else
            {
                Fn._showGridViewDateFormat(dataGridView, strSql, "รหัส", "สายการบิน", "จาก", "ถึง", "วันเดินทาง");
                picExcel.Visible = true;
            }

            ////lblCount.Text = Search._searchForGrid(strSql).Rows.Count.ToString("#,#");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SearchData()
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                lblSearch.Text = "";
                lblSearch.Visible = false;

                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];

                string strSql = "SELECT * FROM [TB_FLIGHT] WHERE [FLIGHT_ID] = '" + row.Cells["FLIGHT_ID"].Value.ToString() + "'";

                DataTable dt = new DataTable();
                dt = Fn._setValue(strSql);

                txtETKT_M.Text = dt.Rows[0]["FLIGHT_TRICKET_NUMBER"].ToString();

                cbbLogo.SelectedValue = dt.Rows[0]["FLIGHT_LOGO"].ToString();
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
            FrmSaveFlight_Load(sender, e);
        }

        private void picExcel_Click(object sender, EventArgs e)
        {
            Fn._excel(dataGridView, "FLIGHT");
        }

        private void FrmSaveMember_KeyDown(object sender, KeyEventArgs e)
        {
            string keyCode = Fn.keyPress(sender, e);

            if (keyCode == "Ctrl+S")
            {
                btnAdd_Click(sender, e);
            }
            if (keyCode == "Ctrl+E")
            {
                btnEdit_Click(sender, e);
            }
            if (keyCode == "Ctrl+D")
            {
                btnDelete_Click(sender, e);
            }
            if (keyCode == "Ctrl+F")
            {
                //btnSeach_Click(sender, e);
            }
            if (keyCode == "Ctrl+C")
            {
                //btnClear_Click(sender, e);
            }
        }

        private void cbbLogo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strSql = "SELECT * " +
                                "FROM TB_LOGO " +
                                "INNER JOIN TB_PATH " +
                                "ON TB_LOGO.LOGO_FILELOCATION = TB_PATH.PATH_ID " +
                                "WHERE [LOGO_ID] = '" + cbbLogo.SelectedValue + "'";
                DataTable dt = Fn._setValue(strSql);
                Images.ShowImage(picAirline, dt.Rows[0]["PATH_LOCATION"].ToString() + dt.Rows[0]["LOGO_FILENAME"].ToString() + dt.Rows[0]["LOGO_FILETYPE"].ToString());
            }
            catch (Exception)
            {
            }
        }
    }
}