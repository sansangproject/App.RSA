using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmManageProductType : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string programCode = "MANTY00";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";
        public string strMain = "";

        public string filePath = "-";
        public string fileName = "-";
        public string fileType = ".jpg";

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();

        public string[,] Parameter = new string[,] { };

        public FrmManageProductType(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmManageProductType_Load(object sender, EventArgs e)
        {
            

            List.GetList(cbbStatus, "0", "Status");
            List.GetList(cbbProductTypeMain, "0", "MainProductType");

            Clear();
        }

        public void Clear()
        {
            txtCode.Text = "";
            txtCode.Focus();
            txtNameTh.Text = "";
            txtNameEn.Text = "";
            txtDetail.Text = "";
            txtRemark.Text = "";
            cbbStatus.SelectedValue = "0";
            cbbProductTypeMain.SelectedValue = "0";

            string[,] Parameter = new string[,]
                {
                    {"@MsProductTypeId", ""},
                    {"@MsProductTypeCode", ""},
                    {"@MsProductTypeNameTh", ""},
                    {"@MsProductTypeNameEn", ""},
                    {"@MsProductTypeDetail", ""},
                    {"@MsProductTypeOrder", ""},
                    {"@MsProductTypeMain", "0"},
                    {"@MsProductTypeRemark", ""},
                    {"@MsProductTypeStatus", "0"},
                };

            db.Get("Spr_S_TblMasterProductType", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        public void getDataGrid(DataTable dt)
        {
            try
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
                    dtGrid = dt.DefaultView.ToTable(true, "MsProductTypeCode", "MsProductTypeNameTh", "MsStatusNameTh", "MsProductTypeCreateBy", "Date", "MsProductTypeId");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                    Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                          "ลำดับ", 100, true, mc, mc
                        , "รหัส", 200, true, ml, ml
                        , "ประเภทสินค้า", 250, true, ml, ml
                        , "สถานะ", 150, true, ml, ml
                        , "ผู้สร้าง", 150, true, ml, ml
                        , "ข้อมูล ณ วันที่", 200, true, mc, mc
                        , "", 0, false, mc, mc
                        );

                    picExcel.Visible = true;
                    lblCount.Text = row.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void picExcel_Click(object sender, EventArgs e)
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            strOpe = "D";

            string[,] Parameter = new string[,]
                {
                   {"@MsProductTypeCode", txtCode.Text},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_D_TblMasterProductType", Parameter, out strErr);

                if (strErr == null)
                {
                    Message.MessageResult(strOpe, "C", strErr);
                    Clear();
                }
                else
                {
                    Message.MessageResult(strOpe, "E", strErr);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            strOpe = "U";

            strMain = "1";

            if (cbbProductTypeMain.SelectedValue.ToString() == "0")
            {
                strMain = "0";
            }

            string[,] Parameter = new string[,]
                {
                    {"@MsProductTypeCode", txtCode.Text},
                    {"@MsProductTypeNameTh", txtNameTh.Text},
                    {"@MsProductTypeNameEn", txtNameEn.Text},
                    {"@MsProductTypeDetail", txtDetail.Text},
                    {"@MsProductTypeRemark", txtRemark.Text},
                    {"@MsProductTypeStatus", cbbStatus.SelectedValue.ToString()},
                    {"@User", strUserId },
                    {"@MsProductTypeOrder", strMain},
                    {"@MsProductTypeMain", cbbProductTypeMain.SelectedValue.ToString()},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_U_TblMasterProductType", Parameter, out strErr);

                if (strErr == null)
                {
                    Message.MessageResult(strOpe, "C", strErr);
                    Clear();
                }
                else
                {
                    Message.MessageResult(strOpe, "E", strErr);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            strMain = "1";

            if (cbbProductTypeMain.SelectedValue.ToString() == "0")
            {
                strMain = "0";
            }

            string[,] Parameter = new string[,]
                {
                    {"@MsProductTypeId", ""},
                    {"@MsProductTypeCode", txtCode.Text},
                    {"@MsProductTypeNameTh", txtNameTh.Text},
                    {"@MsProductTypeNameEn", txtNameEn.Text},
                    {"@MsProductTypeDetail", txtDetail.Text},
                    {"@MsProductTypeRemark", txtRemark.Text},
                    {"@MsProductTypeStatus", cbbStatus.SelectedValue.ToString()},
                    {"@MsProductTypeOrder", strMain},
                    {"@MsProductTypeMain", cbbProductTypeMain.SelectedValue.ToString()},
                };

            db.Get("Spr_S_TblMasterProductType", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtCode.Text = Fn.GetCodes("88", "", "Generated");
            strOpe = "I";
            strMain = "1";

            if (cbbProductTypeMain.SelectedValue.ToString() == "0")
            {
                strMain = "0";
            }

            string[,] Parameter = new string[,]
                {
                    {"@MsProductTypeCode", txtCode.Text},
                    {"@MsProductTypeNameTh", txtNameTh.Text},
                    {"@MsProductTypeNameEn", txtNameEn.Text},
                    {"@MsProductTypeDetail", txtDetail.Text},
                    {"@MsProductTypeRemark", txtRemark.Text},
                    {"@MsProductTypeStatus", cbbStatus.SelectedValue.ToString()},
                    {"@User", strUserId },
                    {"@MsProductTypeOrder", strMain},
                    {"@MsProductTypeMain", cbbProductTypeMain.SelectedValue.ToString()},
                };

            bool Action = Message.MessageConfirmation(strOpe, strUserId, "");

            if (Action == true)
            {
                db.Operations("Spr_I_TblMasterProductType", Parameter, out strErr);

                if (strErr == null)
                {
                    Message.MessageResult(strOpe, "C", strErr);
                    Clear();
                }
                else
                {
                    Message.MessageResult(strOpe, "E", strErr);
                }
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                DataTable dt = new DataTable();

                string[,] Parameter = new string[,]
                {
                    {"@MsProductTypeId", row.Cells["MsProductTypeId"].Value.ToString()},
                    {"@MsProductTypeCode", ""},
                    {"@MsProductTypeNameTh", ""},
                    {"@MsProductTypeNameEn", ""},
                    {"@MsProductTypeDetail", ""},
                    {"@MsProductTypeOrder", ""},
                    {"@MsProductTypeMain", "0"},
                    {"@MsProductTypeRemark", ""},
                    {"@MsProductTypeStatus", "0"},
                };

                db.Get("Spr_S_TblMasterProductType", Parameter, out strErr, out dt);

                txtCode.Text = dt.Rows[0]["MsProductTypeCode"].ToString();
                txtNameTh.Text = dt.Rows[0]["MsProductTypeNameTh"].ToString();
                txtNameEn.Text = dt.Rows[0]["MsProductTypeNameEn"].ToString();
                txtDetail.Text = dt.Rows[0]["MsProductTypeDetail"].ToString();
                txtRemark.Text = dt.Rows[0]["MsProductTypeRemark"].ToString();
                cbbStatus.SelectedValue = dt.Rows[0]["MsProductTypeStatus"].ToString();
                cbbProductTypeMain.SelectedValue = dt.Rows[0]["MsProductTypeMain"].ToString();
            }
        }
    }
}