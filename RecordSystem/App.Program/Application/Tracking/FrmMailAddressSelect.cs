using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;

namespace SANSANG
{
    public partial class FrmMailAddressSelect : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserSurName;
        public string strUserType;

        public string programCode = "";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string filePath = "";
        public string fileName = "-";
        public string fileType = ".jpg";

        public string strAddressId, strDataCode, strImageCode, strprogramPath, strprogramName, strIdCode = "";

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();

        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
       private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsImage TSSImage = new clsImage();

        public string[,] Parameter = new string[,] { };

        public FrmMailAddressSelect(DataTable dtAddress)
        {
            InitializeComponent();
            getDataGrid(dtAddress);
        }

        private void Frm_Load(object sender, EventArgs e)
        {
        }

        public void getDataGrid(DataTable dt)
        {
            
            dataGridView.RowTemplate.Height = 35;
            dataGridView.DataSource = dt;

            //Visible Columns

            //dataGridView.Columns["SNo"].HeaderText = "ลำดับ";
            dataGridView.Columns["SNo"].Visible = false;

            dataGridView.Columns["AddressCode"].Visible = false;
            dataGridView.Columns["AddressId"].Visible = false;

            dataGridView.Columns["DistrictId"].Visible = false;
            dataGridView.Columns["AddressCode"].Visible = false;
            dataGridView.Columns["Address"].Visible = false;
            dataGridView.Columns["AddressPrefix"].Visible = false;

            dataGridView.Columns["AddressNumber"].Visible = false;
            dataGridView.Columns["AddressMoo"].Visible = false;
            dataGridView.Columns["AddressBuilding"].Visible = false;
            dataGridView.Columns["AddressHome"].Visible = false;
            dataGridView.Columns["AddressRoom"].Visible = false;
            dataGridView.Columns["AddressFloor"].Visible = false;
            dataGridView.Columns["AddressSoi"].Visible = false;
            dataGridView.Columns["AddressRoad"].Visible = false;
            dataGridView.Columns["DistrictId"].Visible = false;

            dataGridView.Columns["DistrictNameTh"].HeaderText = "ตำบล";

            dataGridView.Columns["DistrictNameEn"].Visible = false;
            dataGridView.Columns["AmphurId"].Visible = false;

            dataGridView.Columns["AmphurNameTh"].HeaderText = "อำเภอ";

            dataGridView.Columns["AmphurNameEn"].Visible = false;
            dataGridView.Columns["ProvinceId"].Visible = false;

            dataGridView.Columns["ProvinceNameTh"].HeaderText = "จังหวัด";

            dataGridView.Columns["ProvinceNameEn"].Visible = false;
            dataGridView.Columns["GeographyId"].Visible = false;
            dataGridView.Columns["GeographyNameTh"].Visible = false;

            dataGridView.Columns["Postcode"].HeaderText = "รหัสไปรษณีย์";
            dataGridView.Columns["AddressName"].HeaderText = "หมายเหตุ";
            dataGridView.Columns["AddressPhone"].Visible = false;
            dataGridView.Columns["StatusNameTh"].Visible = false;
            dataGridView.Columns["StatusNameEn"].Visible = false;
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            clear();
            DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
            txtName.Text = row.Cells["AddressName"].Value.ToString();
            txtTambol.Text = row.Cells["DistrictNameTh"].Value.ToString();
            txtAmphur.Text = row.Cells["AmphurNameTh"].Value.ToString();
            txtProvince.Text = row.Cells["ProvinceNameTh"].Value.ToString();
            txtZipcode.Text = row.Cells["Postcode"].Value.ToString();
            txtId.Text = row.Cells["AddressId"].Value.ToString();
        }

        private void clear()
        {
            txtName.Text = "";
            txtTambol.Text = "";
            txtAmphur.Text = "";
            txtProvince.Text = "";
            txtZipcode.Text = "";
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            strAddressId = txtId.Text;
            this.Close();
        }
    }
}