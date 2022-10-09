using CrystalDecisions.CrystalReports.Engine;
using SANSANG.Class;
using SANSANG.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SANSANG
{
    public partial class FrmEnvelopes : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string programCode = "SENEL01";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        public string filePath = "";
        public string fileName = "-";
        public string fileType = ".jpg";
        public string strAddress = "";
        public string ExportToPath = "";

        private DataTable dt = new DataTable();
        private clsFunction Fn = new clsFunction();
        private dbConnection db = new dbConnection();
        private ReportDocument rpt;

        public string[,] Parameter = new string[,] { };

        public FrmEnvelopes(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmLoad(object sender, EventArgs e)
        {

            Clear();
        }

        public void Clear()
        {
            getDataGrid(getDataTable("", "", "", ""));

            txtPost1.Text = "";
            txtPost2.Text = "";
            txtPost3.Text = "";
            txtPost4.Text = "";
            txtPost5.Text = "";
            txtPost6.Text = "";

            txtPostName1.Text = "";
            txtPostName2.Text = "";
            txtPostName3.Text = "";
            txtPostName4.Text = "";
            txtPostName5.Text = "";
            txtPostName6.Text = "";
            txtPostName7.Text = "";
            txtPostName8.Text = "";

            pbNum1.Visible = false;
            pbNum2.Visible = false;
            pbNum3.Visible = false;
            pbNum4.Visible = false;
            pbNum5.Visible = false;
            pbNum6.Visible = false;
            pbNum7.Visible = false;
            pbNum8.Visible = false;

            txtPostName1.Visible = false;
            txtPostName2.Visible = false;
            txtPostName3.Visible = false;
            txtPostName4.Visible = false;
            txtPostName5.Visible = false;
            txtPostName6.Visible = false;
            txtPostName7.Visible = false;
            txtPostName8.Visible = false;

            txtPPhone.Text = "";
            txtCode.Text = "";
            txtPAddress1.Text = "";
            txtPAddress2.Text = "";
            txtPAddress3.Text = "";
            txtPAddress4.Text = "";

            txtPCode1.Text = "";
            txtPCode2.Text = "";
            txtPCode3.Text = "";
            txtPCode4.Text = "";
            txtPCode5.Text = "";

            txtSearch.Text = "";
            txtSearch.Focus();
        }

        public DataTable getDataTable(String strCode, String strName, String strZip, String strDistric)
        {
            string[,] Parameter = new string[,]
                       {
                            {"@Code", strCode == ""? "" : strCode},
                            {"@Name", strName == ""? "" : strName},
                            {"@Zipcode", strZip == ""? "" : strZip},
                            {"@District", strDistric == ""? "" : strDistric},
                       };

            db.Get("Spr_S_MaillingAddress", Parameter, out strErr, out dt);

            IEnumerable<DataRow> results = (from MyRows in dt.AsEnumerable() where MyRows.Field<string>("AddressCode") != "" select MyRows);
            DataTable dtNew = results.CopyToDataTable();
            return dtNew;
        }

        public void getDataGrid(DataTable dt)
        {

            dataGridView.RowTemplate.Height = 35;
            dataGridView.DataSource = dt;

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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dataGridView.Rows[e.RowIndex];
                String strRoad = "";

                string[,] Parameter = new string[,]
                        {
                            {"@Code", row.Cells["AddressCode"].Value.ToString()},
                            {"@Name", ""},
                            {"@Zipcode", ""},
                            {"@District", ""},
                        };

                db.Get("Spr_S_MaillingAddress", Parameter, out strErr, out dt);

                txtPAddress1.Text = "";
                txtPAddress2.Text = "";
                txtPAddress3.Text = "";
                txtPAddress4.Text = "";
                txtPCode1.Text = "";

                strRoad = dt.Rows[0]["AddressRoad"].ToString() == "" ? "" : dt.Rows[0]["AddressRoad"].ToString();
                txtPPhone.Text = dt.Rows[0]["AddressPhone"].ToString() != "" ? Fn.ConvertPhoneNumber(dt.Rows[0]["AddressPhone"].ToString()) : "";
                txtPAddress1.Text = dt.Rows[0]["AddressPrefix"].ToString() + dt.Rows[0]["AddressName"].ToString() == "" ? "ชื่อ - นามสกุล" : dt.Rows[0]["AddressName"].ToString();

                txtPAddress2.Text += dt.Rows[0]["AddressNumber"].ToString() == "" ? "" : "เลขที่ " + dt.Rows[0]["AddressNumber"].ToString();
                txtPAddress2.Text += dt.Rows[0]["AddressHome"].ToString() == "" ? "" : " " + dt.Rows[0]["AddressHome"].ToString();
                txtPAddress2.Text += dt.Rows[0]["AddressMoo"].ToString() == "" ? "" : " หมู่ที่ " + dt.Rows[0]["AddressMoo"].ToString() + " ";
                txtPAddress2.Text += dt.Rows[0]["AddressBuilding"].ToString() == "" ? "" : dt.Rows[0]["AddressBuilding"].ToString();
                txtPAddress2.Text += dt.Rows[0]["AddressFloor"].ToString() == "" ? "" : " ชั้น " + dt.Rows[0]["AddressFloor"].ToString();
                txtPAddress2.Text += dt.Rows[0]["AddressRoom"].ToString() == "" ? "" : " ห้อง " + dt.Rows[0]["AddressRoom"].ToString();

                txtPAddress3.Text = strRoad == "" ? "" : strRoad.TrimEnd() + " ";
                txtPAddress3.Text += dt.Rows[0]["AddressSoi"].ToString() == "" ? "" : dt.Rows[0]["AddressSoi"].ToString() + " ";

                if (dt.Rows[0]["ProvinceId"].ToString() == "1")
                {
                    txtPAddress4.Text = dt.Rows[0]["AmphurNameTh"].ToString().TrimEnd().TrimEnd() + " จังหวัด" + dt.Rows[0]["ProvinceNameTh"].ToString().TrimEnd();
                    txtPAddress3.Text += dt.Rows[0]["DistrictNameTh"].ToString() == "" ? "" : "แขวง" + dt.Rows[0]["DistrictNameTh"].ToString().TrimEnd();
                }
                else
                {
                    txtPAddress4.Text = "อำเภอ" + dt.Rows[0]["AmphurNameTh"].ToString().TrimEnd().TrimEnd() + " จังหวัด" + dt.Rows[0]["ProvinceNameTh"].ToString().TrimEnd();
                    txtPAddress3.Text += dt.Rows[0]["DistrictNameTh"].ToString() == "" ? "" : "ตำบล" + dt.Rows[0]["DistrictNameTh"].ToString().TrimEnd();
                }

                txtPCode1.Text = dt.Rows[0]["Postcode"].ToString().Substring(0, 1);
                txtPCode2.Text = "";
                txtPCode2.Text = dt.Rows[0]["Postcode"].ToString().Substring(1, 1);
                txtPCode3.Text = "";
                txtPCode3.Text = dt.Rows[0]["Postcode"].ToString().Substring(2, 1);
                txtPCode4.Text = "";
                txtPCode4.Text = dt.Rows[0]["Postcode"].ToString().Substring(3, 1);
                txtPCode5.Text = "";
                txtPCode5.Text = dt.Rows[0]["Postcode"].ToString().Substring(4, 1);

                txtCode.Text = dt.Rows[0]["AddressCode"].ToString() == "" ? "" : dt.Rows[0]["AddressCode"].ToString();
            }
            catch (Exception)
            {
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
        }

        public void Search()
        {
            if (txtSearch.Text != "")
            {
                string[,] Parameter = new string[,]
                        {
                            {"@Code", ""},
                            {"@Name", txtSearch.Text},
                            {"@Zipcode", ""},
                            {"@District", ""},
                        };

                db.Get("Spr_S_MaillingAddress", Parameter, out strErr, out dt);
            }
            else
            {
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                getDataGrid(getDataTable("", txtSearch.Text, "", ""));
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtCode.Text != "")
            {
                try
                {
                    if (txtPost1.Text != "")
                    {
                        if (txtPost2.Text != "")
                        {
                            if (txtPost3.Text != "")
                            {
                                if (txtPost4.Text != "")
                                {
                                    if (txtPost5.Text != "")
                                    {
                                        if (txtPost6.Text != "")
                                        {
                                            if (txtPost7.Text != "")
                                            {
                                                txtPostName8.Text = txtPAddress1.Text;
                                                txtPost8.Text = txtCode.Text;
                                                txtPostName8.Visible = true;
                                                pbNum8.Visible = true;
                                            }
                                            else
                                            {
                                                txtPostName7.Text = txtPAddress1.Text;
                                                txtPost7.Text = txtCode.Text;
                                                txtPostName7.Visible = true;
                                                pbNum7.Visible = true;
                                            }
                                        }
                                        else
                                        {
                                            txtPostName6.Text = txtPAddress1.Text;
                                            txtPost6.Text = txtCode.Text;
                                            txtPostName6.Visible = true;
                                            pbNum6.Visible = true;
                                        }
                                    }
                                    else
                                    {
                                        txtPostName5.Text = txtPAddress1.Text;
                                        txtPost5.Text = txtCode.Text;
                                        txtPostName5.Visible = true;
                                        pbNum5.Visible = true;
                                    }
                                }
                                else
                                {
                                    txtPostName4.Text = txtPAddress1.Text;
                                    txtPost4.Text = txtCode.Text;
                                    txtPostName4.Visible = true;
                                    pbNum4.Visible = true;
                                }
                            }
                            else
                            {
                                txtPostName3.Text = txtPAddress1.Text;
                                txtPost3.Text = txtCode.Text;
                                txtPostName3.Visible = true;
                                pbNum3.Visible = true;
                            }
                        }
                        else
                        {
                            txtPostName2.Text = txtPAddress1.Text;
                            txtPost2.Text = txtCode.Text;
                            txtPostName2.Visible = true;
                            pbNum2.Visible = true;
                        }
                    }
                    else
                    {
                        txtPostName1.Text = txtPAddress1.Text;
                        txtPost1.Text = txtCode.Text;
                        txtPostName1.Visible = true;
                        pbNum1.Visible = true;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPost8.Text == "")
                {
                    if (txtPost7.Text == "")
                    {
                        if (txtPost6.Text == "")
                        {
                            if (txtPost5.Text == "")
                            {
                                if (txtPost4.Text == "")
                                {
                                    if (txtPost3.Text == "")
                                    {
                                        if (txtPost2.Text == "")
                                        {
                                            txtPostName1.Text = "";
                                            txtPost1.Text = "";
                                            txtPostName1.Visible = false;
                                            pbNum1.Visible = false;
                                        }
                                        else
                                        {
                                            txtPostName2.Text = "";
                                            txtPost2.Text = "";
                                            txtPostName2.Visible = false;
                                            pbNum2.Visible = false;
                                        }
                                    }
                                    else
                                    {
                                        txtPostName3.Text = "";
                                        txtPost3.Text = "";
                                        txtPostName3.Visible = false;
                                        pbNum3.Visible = false;
                                    }
                                }
                                else
                                {
                                    txtPostName4.Text = "";
                                    txtPost4.Text = "";
                                    txtPostName4.Visible = false;
                                    pbNum4.Visible = false;
                                }
                            }
                            else
                            {
                                txtPostName5.Text = "";
                                txtPost5.Text = "";
                                txtPostName5.Visible = false;
                                pbNum5.Visible = false;
                            }
                        }
                        else
                        {
                            txtPostName6.Text = "";
                            txtPost6.Text = "";
                            txtPostName6.Visible = false;
                            pbNum6.Visible = false;
                        }
                    }
                    else
                    {
                        txtPostName7.Text = "";
                        txtPost7.Text = "";
                        txtPostName7.Visible = false;
                        pbNum7.Visible = false;
                    }
                }
                else
                {
                    txtPostName8.Text = "";
                    txtPost8.Text = "";
                    txtPostName8.Visible = false;
                    pbNum8.Visible = false;
                }
            }
            catch (Exception)
            {
            }
        }

        public DataTable getDataForPrint()
        {
            DataTable dtAddress = new DataTable();
            dtAddress.Columns.Add(new DataColumn("AddressLine1", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("AddressLine2", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("AddressLine3", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("Mobile", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("Name", typeof(string)));

            dtAddress.Columns.Add(new DataColumn("Postcode1", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("Postcode2", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("Postcode3", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("Postcode4", typeof(string)));
            dtAddress.Columns.Add(new DataColumn("Postcode5", typeof(string)));
            DataRow AddressRow;
            int length = txtPostName8.Text != "" ? 8 : txtPostName7.Text != "" ? 7 : txtPostName6.Text != "" ? 6 : txtPostName5.Text != "" ? 5 : txtPostName4.Text != "" ? 4 : txtPostName3.Text != "" ? 3 : txtPostName2.Text != "" ? 2 : txtPostName1.Text != "" ? 1 : 0;

            for (int i = 0; i < length; i++)
            {
                String strValue = "";
                strValue = i == 0 ? txtPost1.Text : i == 1 ? txtPost2.Text : i == 2 ? txtPost3.Text : i == 3 ? txtPost4.Text : i == 4 ? txtPost5.Text : i == 5 ? txtPost6.Text : i == 6 ? txtPost7.Text : i == 7 ? txtPost8.Text : "";

                string[,] Parameter = new string[,]
                {
                    {"@Code", strValue},
                    {"@Name", ""},
                    {"@Zipcode", ""},
                    {"@District", ""},
                };

                db.Get("Spr_R_TblSaveAddress", Parameter, out strErr, out dt);

                AddressRow = dtAddress.NewRow();
                AddressRow["Name"] = "คุณ" + dt.Rows[0]["AddressName"].ToString();
                AddressRow["Mobile"] = dt.Rows[0]["AddressPhone"].ToString();

                AddressRow["AddressLine1"] = dt.Rows[0]["AddressLine1"].ToString();
                AddressRow["AddressLine2"] = dt.Rows[0]["AddressLine2"].ToString();
                AddressRow["AddressLine3"] = dt.Rows[0]["AddressLine3"].ToString();

                AddressRow["Postcode1"] = dt.Rows[0]["Postcode1"].ToString();
                AddressRow["Postcode2"] = dt.Rows[0]["Postcode2"].ToString();
                AddressRow["Postcode3"] = dt.Rows[0]["Postcode3"].ToString();
                AddressRow["Postcode4"] = dt.Rows[0]["Postcode4"].ToString();
                AddressRow["Postcode5"] = dt.Rows[0]["Postcode5"].ToString();
                dtAddress.Rows.Add(AddressRow);
            }

            return dtAddress;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            FrmAnimatedProgress frm = new FrmAnimatedProgress(10);
            string ReportName = "";
            DataTable dtData = getDataForPrint();

            try
            {
                if (dtData.Rows.Count > 0)
                {
                    string Path = Fn.getPath("App.Report");
                    ReportName = "RSA-R-SENEL00001";

                    DialogResult result = folderBrowserDialog.ShowDialog();

                    rpt = new ReportDocument();
                    rpt.Load(Path + ReportName + ".rpt");
                    rpt.SetDataSource(dtData);

                    crystalReportViewer.ReportSource = rpt;
                    crystalReportViewer.Refresh();

                    rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, ExportToPath + ".pdf");
                    rpt.Close();
                    System.Diagnostics.Process.Start(@"" + ExportToPath + ".pdf");

                    if (result == DialogResult.OK)
                    {
                        ExportToPath = folderBrowserDialog.SelectedPath + "\\TSSRSA-MWARE00R" + Fn.getFileLastName();
                        rpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, ExportToPath + ".pdf");
                        rpt.Close();
                    }

                    rpt.Close();
                    Clear();
                }
                else
                {
                }
            }
            catch (Exception)
            {
                Clear();
            }
        }
    }
}