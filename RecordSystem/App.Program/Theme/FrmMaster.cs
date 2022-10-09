using TSS.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RSA
{
    public partial class FrmMasterForm : Form
    {
        public string strUserId;
                public string strUserName;        public string strUserSurname;
        public string strUserType;

        public string programCode = "FRMCODE";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string strAppCode = "APPCO00";
        public string strAppName = "FrmMaste";

        public string filePath = "-";
        public string fileName = "-";
        public string fileType = ".jpg";

        DataTable dt = new DataTable();
        clsSearch TSSSearch = new clsSearch();
        clsDelete TSSDelete = new clsDelete();
        clsInsert TSSInsert = new clsInsert();
        clsFunction TSSFunction = new clsFunction();
        clsMessage TSSMessage = new clsMessage();
        clsDatabase TSSDatabase = new clsDatabase();
        clsLog TSSLog = new clsLog();
        public string[,] parameter = new string[,] { };
        CultureInfo En = new CultureInfo("en-US");

        public FrmMasterForm(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void Frm_Load(object sender, EventArgs e)
        {
            TSSFunction.getMenu(programCode, lblProTH, lblProEN);
            Clear();

        }

         public void Clear()
        {
            txtId.Enabled = true;
            txtId.Text = "";
            txtId.Focus();

            string[,] parameter = new string[,]
	            {
                    {"@Parameter","Value"},
                    
	            };

            TSSDatabase.getData("Spr_S", parameter, parameter.Length / 2, out strErr, out dt, "dbRSA");
            getDataGrid(dt);
        }

        public void getDataGrid(DataTable dt)
        {
            int row = dt.Rows.Count;

            if (row == 0)
            {
                dataGridView.DataSource = null;
                picExcel.Visible = false;
                lblCount.Text = "0";
            }
            else
            {
                DataTable dtGrid = new DataTable();
                dtGrid = dt.DefaultView.ToTable(true, "Code", "NameTh", "Detail", "MsStatusNameTh", "Date", "Id");

                DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
                DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                TSSFunction.showGridViewFormatFromStore(dtGrid, dataGridView,
                      "ลำดับ", 100, true, mc, mr
                    , "รหัส", 200, true, ml, ml
                    , "รายการ", 150, true, ml, ml
                    , "รายละเอียด", 150, true, ml, ml
                    , "สถานะ", 150, true, ml, ml
                    , "ข้อมูล ณ วันที่ ", 200, true, mc, mc
                    , "", 0, false, mc, mc
                    );

                picExcel.Visible = true;
                lblCount.Text = row.ToString();
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
            try
            {
                parameter = new string[,]
	            {
                    {"@Parameter", "Value"},
	            };

                TSSDatabase.getData("Spr_S", parameter, parameter.Length / 2, out strErr, out dt, "dbRSA");
                getDataGrid(dt);

            }
            catch (Exception)
            {
                
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtId.Text = TSSFunction.getId("Tbl", "Id", "CODE");
            strUserId = txtId.Text;
            strOpe = "I";

            string[,] parameter = new string[,]
	            {
	                {"@Parameter", "Value"},
	            };

            bool Action = TSSMessage.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                TSSDatabase.operationData("Spr_I", parameter, parameter.Length / 2, out strErr, "dbRSA");

                if (strErr == null)
                {
                    TSSMessage.MessageResult(strOpe, "C", strErr);
                    Clear();
                }
                else
                {
                    TSSMessage.MessageResult(strOpe, "E", strErr);
                }
            }

            TSSInsert.TblLogMenu(strUserId, programCode, "Add");
            TSSLog.WriteLogData(strAppCode, strAppName, strUserId, "Insert Data Successfully");
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                DataTable dt = new DataTable();

                string[,] parameter = new string[,]
	            {
                    {"@Id", row.Cells["Id"].Value.ToString()},
	            };

                TSSDatabase.getData("Spr_S", parameter, parameter.Length / 2, out strErr, out dt, "dbRSA");

                txtId.Text = dt.Rows[0]["Code"].ToString();

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            strUserId = txtId.Text;
            strOpe = "D";

            string[,] parameter = new string[,]
	            {
	                {"@Parameter", "Value"},
	            };

            bool Action = TSSMessage.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                TSSDatabase.operationData("Spr_D", parameter, parameter.Length / 2, out strErr, "dbRSA");

                if (strErr == null)
                {
                    TSSMessage.MessageResult(strOpe, "C", strErr);
                    Clear();
                }
                else
                {
                    TSSMessage.MessageResult(strOpe, "E", strErr);
                }
            }

            TSSInsert.TblLogMenu(strUserId, programCode, "Delete");
            TSSLog.WriteLogData(strAppCode, strAppName, strUserId, "Delete Data Successfully");
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            strUserId = txtId.Text;
            strOpe = "U";

            string[,] parameter = new string[,]
	            {
	                {"@Parameter", "Value"},
	            };

            bool Action = TSSMessage.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                TSSDatabase.operationData("Spr_U", parameter, parameter.Length / 2, out strErr, "dbRSA");

                if (strErr == null)
                {
                    TSSMessage.MessageResult(strOpe, "C", strErr);
                    Clear();
                }
                else
                {
                    TSSMessage.MessageResult(strOpe, "E", strErr);
                }
            }

            TSSInsert.TblLogMenu(strUserId, programCode, "Edit");
            TSSLog.WriteLogData(strAppCode, strAppName, strUserId, "Update Data Successfully");
        }

        private void picExcel_Click(object sender, EventArgs e)
        {

        }

        private void Frm_KeyDown(object sender, KeyEventArgs e)
        {
            string keyCode = TSSFunction.keyPress(sender, e);

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
            if (keyCode == "Altl+C")
            {
                btnClear_Click(sender, e);
            }
            if (keyCode == "Ctrl+P")
            {
                btnPrint_Click(sender, e);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

        }

    }
}