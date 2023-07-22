using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;
using SANSANG.Constant;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmManageUnit : Form
    {
        public string strUserId;
        public string strUserName; public string strUserSurname;
        public string strUserType;

        public string strAppCode = "MANUN00";
        public string strAppName = "FrmMangeUnit";
        public string strErr = "";
        public string strOpe = "";
        public string strStatus = "";

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();

        private TransactionConstant Transaction = new TransactionConstant();
        private StoreConstant Store = new StoreConstant();
        private StatusConstant Status = new StatusConstant();

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();

        public string[,] Parameter = new string[,] { };

        public FrmManageUnit(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmMangeUnit_Load(object sender, EventArgs e)
        {
            List.GetList(cbbStatus, "0", "Status");
            Clear();
        }

        public void Clear()
        {
            txtId.Text = "";
            txtCode.Text = "";
            txtCode.Focus();
            txtNameTh.Text = "";
            txtNameEn.Text = "";
            cbbStatus.SelectedValue = "0";

            string[,] Parameter = new string[,]
                {
                    {"@MsUnitId", ""},
                    {"@MsUnitCode", ""},
                    {"@MsUnitNameTh", ""},
                    {"@MsUnitNameEn", ""},
                    {"@MsUnitStatus", "0"},
                };

            db.Get("Store.SelectUnit", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        public void getDataGrid(DataTable dt)
        {
            if (dt != null)
            {
                int row = dt.Rows.Count;

                if (row == 0)
                {
                    dataGridView.DataSource = null;
                    picExcel.Visible = false;
                    lblResult.Text = Fn.ShowResult(0);
                }
                else
                {
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "MsUnitCode", "MsUnitNameTh", "MsStatusNameTh", "MsUnitCreateBy", "Date", "MsUnitId");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                    Fn.showGridViewFormatFromStore(dtGrid, dataGridView,
                          "ลำดับ", 50, true, mc, mc
                        , "รหัส", 150, true, ml, ml
                        , "หน่วยนับ", 300, true, ml, ml
                        , "สถานะ", 150, true, ml, ml
                        , "ผู้สร้าง", 100, true, ml, ml
                        , "ข้อมูล ณ วันที่", 150, true, mc, mc
                        , "", 0, false, mc, mc
                        );

                    picExcel.Visible = true;
                    lblResult.Text = Fn.ShowResult(row);
                }
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
            Operation(Status.Delete, txtId.Text);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Operation(Status.Update, txtId.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtCode.Text = Fn.GetCodes("101", "", "Generated");
            Operation(Status.Insert, txtId.Text);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        public void SearchData()
        {
            string[,] Parameter = new string[,]
               {
                    {"@MsUnitId", ""},
                    {"@MsUnitCode", txtCode.Text},
                    {"@MsUnitNameTh", txtNameTh.Text},
                    {"@MsUnitNameEn", txtNameEn.Text},
                    {"@MsUnitStatus", cbbStatus.SelectedValue.ToString()},
               };

            db.Get("Store.SelectUnit", Parameter, out strErr, out dt);
            getDataGrid(dt);
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                DataTable dt = new DataTable();

                string[,] Parameter = new string[,]
                {
                    {"@MsUnitId", row.Cells["MsUnitId"].Value.ToString()},
                    {"@MsUnitCode", "" },
                    {"@MsUnitNameTh", ""},
                    {"@MsUnitNameEn", ""},
                    {"@MsUnitStatus", "0"},
                };

                db.Get("Store.SelectUnit", Parameter, out strErr, out dt);

                txtId.Text = dt.Rows[0]["MsUnitId"].ToString();
                txtCode.Text = dt.Rows[0]["MsUnitCode"].ToString();
                txtNameTh.Text = dt.Rows[0]["MsUnitNameTh"].ToString();
                cbbStatus.SelectedValue = dt.Rows[0]["MsUnitStatus"].ToString();
                txtNameEn.Text = dt.Rows[0]["MsUnitNameEn"].ToString();
            }
        }

        private void Operation(string Operation, string Id = "")
        {
            try
            {
                if (txtCode.Text != "")
                {
                    string strCode = Operation == Status.Insert ? txtCode.Text : txtId.Text != "" ? txtCode.Text + " (" + txtId.Text + ")" : txtCode.Text;
                    string strText = txtNameTh.Text;
                    string strStoreName = Operation == "I" ? "Store.InsertUnit" : Operation == "U" ? "Store.UpdateUnit" : "Store.DeleteUnit";
                    string strTransactionName = Operation == "I" ? Transaction.Add : Operation == "U" ? Transaction.Update : Transaction.Delete;

                    string[,] Parameter = new string[,]
                    {
                        {"@MsUnitId", txtId.Text},
                        {"@MsUnitCode", txtCode.Text},
                        {"@MsUnitNameTh", txtNameTh.Text},
                        {"@MsUnitNameEn", txtNameEn.Text},
                        {"@MsUnitStatus",cbbStatus.SelectedValue.ToString()},
                        {"@User",strUserId },
                        {"@DeleteType", "0"},
                    };

                    Message.MessageConfirmation(Operation, strCode, strText);

                    using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, Status.Yes, Status.No, Message.strImage))
                    {
                        var result = Mes.ShowDialog();

                        if (result == DialogResult.Yes)
                        {
                            Mes.Close();
                            db.Operations(strStoreName, Parameter, out strErr);
                            strStatus = strErr == null ? Transaction.Complete : Transaction.Error;
                            Message.MessageResult(Operation, strStatus, strErr);
                            Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }
    }
}