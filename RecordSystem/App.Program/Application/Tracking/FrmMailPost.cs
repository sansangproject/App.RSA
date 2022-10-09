using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SANSANG.Class; using SANSANG.Database;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmMailPost : Form
    {
        public string strUserId;
        public string strUserName;
        private string strUserSurname;
        public string strUserType;

        public string strAppCode = "SENMA01";
        public string strAppName = "FrmMailPost";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strCode = "";

        public string strOpe = "";
        public string filePath = "";
        public string fileName = "-";
        public string fileType = ".jpg";
        public string strAddress = "";

        private DataTable dt = new DataTable();
        private DataTable dts = new DataTable();
        private clsDelete Delete = new clsDelete();
        private clsInsert Insert = new clsInsert();
        private clsFunction Fn = new clsFunction();
        private clsMessage Message = new clsMessage();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsBarcode Barcode = new clsBarcode();
        private clsLog Log = new clsLog();

        public string[,] Parameter = new string[,] { };

        public FrmMailPost(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            Clear("All");
        }

        public void Clear(string value)
        {
            if (value == "All")
            {
                txtId.Text = "";

                List.GetList(cbbStart, "11", "Status");
                cbbStart.SelectedValue = 0;

                txtName.Text = "ชื่อ - นามสกุล";
                txtMobile.Text = "เบอร์";
                txtAddress1.Text = "เลขที่ / หมู่บ้าน / หมู่ที่ / อาคาร / ชั้น / ห้อง / ซอย";
                txtAddress2.Text = "ถนน";
                txtTambol.Text = "ตำบล";
                lblAumphur.Text = "อำเภอ";
                lblProvince.Text = "จังหวัด";
                txtZip1.Text = "";
                txtZip2.Text = "";
                txtZip3.Text = "";
                txtZip4.Text = "";
                txtZip5.Text = "";
                txtGeo.Text = "";
                txtSearch.Text = "";

                txtPAddress1.Text = "";
                txtPAddress2.Text = "";
                txtPAddress3.Text = "";
                txtPAddress4.Text = "";

                txtPCode1.Text = "";
                txtPCode2.Text = "";
                txtPCode3.Text = "";
                txtPCode4.Text = "";
                txtPCode5.Text = "";

                txtPPhone.Text = "";

                txtTamId.Text = "";
                txtAumId.Text = "";
                txtProId.Text = "";

                txtSearch.Focus();
            }
            else if (value == "Image")
            {
                txtPAddress1.Text = "";
                txtPAddress2.Text = "";
                txtPAddress3.Text = "";
                txtPAddress4.Text = "";

                txtPCode1.Text = "";
                txtPCode2.Text = "";
                txtPCode3.Text = "";
                txtPCode4.Text = "";
                txtPCode5.Text = "";

                txtPPhone.Text = "";
            }

            pbNProductBarcode.Image = null;
        }

        public void getDataGrid(DataTable dt)
        {
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear("All");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtZip1.Text != "")
                {
                    strCode = Fn.getCode("Tbl_Save_Address", "AddressId", "A", DateTime.Now);
                    strOpe = "I";

                    string strNum = "";
                    string strMoo = "";
                    string strBuild = "";
                    string strHome = "";
                    string strRoom = "";
                    string strFloor = "";
                    string strSoi = "";

                    string strAdd = txtAddress1.Text;
                    string[] Address = strAdd.Split(' ');
                    int r = 0;

                    foreach (string a in Address)
                    {
                        strNum = a == "เลขที่" ? Address[r + 1] : strNum == "" ? "" : strNum;
                        strMoo = a == "หมู่ที่" ? Address[r + 1] : strMoo == "" ? "" : strMoo;
                        strRoom = a == "ห้อง" ? Address[r + 1] : strRoom == "" ? "" : strRoom;
                        strFloor = a == "ชั้น" ? Address[r + 1] : strFloor == "" ? "" : strFloor;
                        strSoi = a.Contains("ซอย") == true ? Address[r] : strSoi == "" ? "" : strSoi;
                        strBuild = a.Contains("อาคาร") == true ? Address[r] : strBuild == "" ? "" : strBuild;
                        strHome = a.Contains("หมู่บ้าน") == true ? Address[r] : strHome == "" ? "" : strHome;
                        r++;
                    }

                    string[,] Parameter = new string[,]
                {
                   {"@AddressCode", strCode},
                   {"@AddressPrefix", cbbStart.Text == ":: กรุณาเลือก ::"? "" : cbbStart.Text},
                   {"@AddressName", txtName.Text},
                   {"@Address", txtAddress1.Text},
                   {"@AddressNumber", strNum == ""? "" : strNum},
                   {"@AddressMoo", strMoo == ""? "" : strMoo},
                   {"@AddressBuilding", strBuild == ""? "" : strBuild},
                   {"@AddressHome", strHome == ""? "" : strHome},
                   {"@AddressRoom", strRoom == ""? "" : strRoom},
                   {"@AddressFloor", strFloor == ""? "" : strFloor},
                   {"@AddressSoi", strSoi == ""? "" : strSoi},
                   {"@AddressRoad", txtAddress2.Text == "-" ? "" : txtAddress2.Text.Substring(txtAddress2.Text.IndexOf("ถนน"),txtAddress2.TextLength)},
                   {"@AddressDistrict", txtTambolId.Text},
                   {"@AddressAmphur", txtAmphurId.Text},
                   {"@AddressPrivince", txtProvinceId.Text},
                   {"@AddressGeo", txtGeoId.Text},
                   {"@AddressPostcode", txtZip1.Text + txtZip2.Text + txtZip3.Text + txtZip4.Text + txtZip5.Text},
                   {"@AddressPhone", txtMobile.Text},
                   {"@AddressSend", "2"},
                   {"@AddressStatus", "Y"},
                   {"@User",strUserId},
                };

                    Message.MessageConfirmation(strOpe, strCode, "เพิ่มที่อยู่ " + cbbStart.Text + txtName.Text);

                    using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                    {
                        var result = Mes.ShowDialog();

                        if (result == DialogResult.Yes)
                        {
                            Mes.Close();
                            db.Operations("Spr_I_TblSaveAddress", Parameter, out strErr);
                            if (strErr == null)
                            {
                                Message.MessageResult(strOpe, "C", strErr);
                                Clear("All");
                            }
                            else
                            {
                                Message.MessageResult(strOpe, "E", strErr);
                            }
                        }

                        
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void Search()
        {
            try
            {
                string[,] Parameter = new string[,]
                        {
                            {"@Code", rdbCode.Checked == false? "" : txtSearch.Text},
                            {"@Name", rdbName.Checked == false? "" : txtSearch.Text},
                            {"@Zipcode", rdbZipcode.Checked == false? "" : txtSearch.Text},
                            {"@District", rdbTambol.Checked == false? "" : txtSearch.Text},
                        };

                db.Get("Spr_S_MaillingAddress", Parameter, out strErr, out dt);

                string strSlected = "";
                string strRoad = "";
                int row = dt.Rows.Count;

                if (row > 1)
                {
                    FrmMailAddressSelect Frm = new FrmMailAddressSelect(dt);
                    Frm.ShowDialog();

                    strSlected = Frm.strAddressId;

                    if (strSlected != "")
                    {
                        IEnumerable<DataRow> results = (from MyRows in dt.AsEnumerable() where MyRows.Field<string>("AddressId") == strSlected select MyRows);
                        DataTable dtNew = results.CopyToDataTable();

                        if (dtNew.Rows.Count == 1)
                        {
                            strRoad = dtNew.Rows[0]["AddressRoad"].ToString() == "" ? "" : dtNew.Rows[0]["AddressRoad"].ToString().TrimEnd();

                            cbbStart.Text = dtNew.Rows[0]["AddressPrefix"].ToString() == "" ? ":: กรุณาเลือก ::" : dtNew.Rows[0]["AddressPrefix"].ToString();
                            txtName.Text = dtNew.Rows[0]["AddressName"].ToString() == "" ? "ชื่อ - นามสกุล" : dtNew.Rows[0]["AddressName"].ToString();
                            txtMobile.Text = dtNew.Rows[0]["AddressPhone"].ToString() != "" ? Fn.ConvertPhoneNumber(dtNew.Rows[0]["AddressPhone"].ToString()) : "เบอร์";

                            txtAddress1.Text = "";
                            txtAddress1.Text += dtNew.Rows[0]["AddressNumber"].ToString() == "" ? "" : "เลขที่ " + dtNew.Rows[0]["AddressNumber"].ToString().TrimEnd();
                            txtAddress1.Text += dtNew.Rows[0]["AddressHome"].ToString() == "" ? "" : " " + dtNew.Rows[0]["AddressHome"].ToString().TrimEnd();
                            txtAddress1.Text += dtNew.Rows[0]["AddressMoo"].ToString() == "" ? "" : " หมู่ที่ " + dtNew.Rows[0]["AddressMoo"].ToString().TrimEnd();
                            txtAddress1.Text += dtNew.Rows[0]["AddressBuilding"].ToString() == "" ? "" : " " + dtNew.Rows[0]["AddressBuilding"].ToString().TrimEnd();
                            txtAddress1.Text += dtNew.Rows[0]["AddressFloor"].ToString() == "" ? "" : " ชั้น " + dtNew.Rows[0]["AddressFloor"].ToString().TrimEnd();
                            txtAddress1.Text += dtNew.Rows[0]["AddressRoom"].ToString() == "" ? "" : " ห้อง " + dtNew.Rows[0]["AddressRoom"].ToString().TrimEnd();
                            txtAddress1.Text += dtNew.Rows[0]["AddressSoi"].ToString() == "" ? "" : " " + dtNew.Rows[0]["AddressSoi"].ToString().TrimEnd();

                            txtAddress1.Text = txtAddress1.Text == "" ? "เลขที่ / หมู่บ้าน / หมู่ที่ / อาคาร / ชั้น / ห้อง / ซอย" : txtAddress1.Text.TrimEnd();

                            txtAddress2.Text = "";
                            txtAddress2.Text += strRoad == "" ? "" : strRoad.TrimEnd();

                            txtTambol.Text = "";
                            lblAumphur.Text = "";
                            lblProvince.Text = "";
                            txtZip1.Text = "";

                            if (dt.Rows[0]["ProvinceId"].ToString() == "1")
                            {
                                txtTambol.Text = "แขวง" + dtNew.Rows[0]["DistrictNameTh"].ToString().TrimEnd();
                                lblAumphur.Text = dtNew.Rows[0]["AmphurNameTh"].ToString().TrimEnd();
                            }
                            else
                            {
                                txtTambol.Text = "ตำบล" + dtNew.Rows[0]["DistrictNameTh"].ToString().TrimEnd();
                                lblAumphur.Text = "อำเภอ" + dtNew.Rows[0]["AmphurNameTh"].ToString().TrimEnd();
                            }

                            lblProvince.Text = "จังหวัด" + dtNew.Rows[0]["ProvinceNameTh"].ToString();

                            txtZip1.Text = dtNew.Rows[0]["Postcode"].ToString().Substring(0, 1);
                            txtZip2.Text = "";
                            txtZip2.Text = dtNew.Rows[0]["Postcode"].ToString().Substring(1, 1);
                            txtZip3.Text = "";
                            txtZip3.Text = dtNew.Rows[0]["Postcode"].ToString().Substring(2, 1);
                            txtZip4.Text = "";
                            txtZip4.Text = dtNew.Rows[0]["Postcode"].ToString().Substring(3, 1);
                            txtZip5.Text = "";
                            txtZip5.Text = dtNew.Rows[0]["Postcode"].ToString().Substring(4, 1);

                            txtTambolId.Text = dtNew.Rows[0]["DistrictId"].ToString();
                            txtAmphurId.Text = dtNew.Rows[0]["AmphurId"].ToString();
                            txtProvinceId.Text = dtNew.Rows[0]["ProvinceId"].ToString();
                            txtGeoId.Text = dtNew.Rows[0]["GeographyId"].ToString();

                            txtId.Text = dtNew.Rows[0]["AddressCode"].ToString() == "" ? "" : dtNew.Rows[0]["AddressCode"].ToString();
                        }

                        btnSearch.Focus();
                    }
                }
                else if (row == 1)
                {
                    strRoad = dt.Rows[0]["AddressRoad"].ToString() == "" ? "" : dt.Rows[0]["AddressRoad"].ToString().TrimEnd();

                    cbbStart.Text = dt.Rows[0]["AddressPrefix"].ToString() == "" ? ":: กรุณาเลือก ::" : dt.Rows[0]["AddressPrefix"].ToString();
                    txtName.Text = dt.Rows[0]["AddressName"].ToString() == "" ? "ชื่อ - นามสกุล" : dt.Rows[0]["AddressName"].ToString();
                    txtMobile.Text = dt.Rows[0]["AddressPhone"].ToString() != "" ? Fn.ConvertPhoneNumber(dt.Rows[0]["AddressPhone"].ToString()) : "เบอร์";

                    txtAddress1.Text = "";
                    txtAddress1.Text = dt.Rows[0]["AddressNumber"].ToString().TrimEnd() == "" ? "" : "เลขที่ " + dt.Rows[0]["AddressNumber"].ToString().TrimEnd();
                    txtAddress1.Text += dt.Rows[0]["AddressHome"].ToString().TrimEnd() == "" ? "" : " " + dt.Rows[0]["AddressHome"].ToString().TrimEnd();
                    txtAddress1.Text += dt.Rows[0]["AddressMoo"].ToString().TrimEnd() == "" ? "" : " หมู่ที่ " + dt.Rows[0]["AddressMoo"].ToString().TrimEnd();
                    txtAddress1.Text += dt.Rows[0]["AddressBuilding"].ToString().TrimEnd() == "" ? "" : " " + dt.Rows[0]["AddressBuilding"].ToString().TrimEnd();
                    txtAddress1.Text += dt.Rows[0]["AddressFloor"].ToString().TrimEnd() == "" ? "" : " ชั้น " + dt.Rows[0]["AddressFloor"].ToString().TrimEnd();
                    txtAddress1.Text += dt.Rows[0]["AddressRoom"].ToString().TrimEnd() == "" ? "" : " ห้อง " + dt.Rows[0]["AddressRoom"].ToString().TrimEnd();
                    txtAddress1.Text += dt.Rows[0]["AddressSoi"].ToString().TrimEnd() == "" ? "" : " " + dt.Rows[0]["AddressSoi"].ToString().TrimEnd();

                    txtAddress1.Text = txtAddress1.Text == "" ? "เลขที่ / หมู่บ้าน / หมู่ที่ / อาคาร / ชั้น / ห้อง / ซอย" : txtAddress1.Text;

                    txtAddress2.Text = "";
                    txtAddress2.Text += strRoad == "" ? "" : strRoad.TrimEnd();

                    txtTambol.Text = "";
                    lblAumphur.Text = "";
                    lblProvince.Text = "";
                    txtZip1.Text = "";

                    if (dt.Rows[0]["ProvinceId"].ToString() == "1")
                    {
                        txtTambol.Text = "แขวง" + dt.Rows[0]["DistrictNameTh"].ToString().TrimEnd();
                        lblAumphur.Text = dt.Rows[0]["AmphurNameTh"].ToString().TrimEnd();
                    }
                    else
                    {
                        txtTambol.Text = "ตำบล" + dt.Rows[0]["DistrictNameTh"].ToString().TrimEnd();
                        lblAumphur.Text = "อำเภอ" + dt.Rows[0]["AmphurNameTh"].ToString().TrimEnd();
                    }

                    lblProvince.Text = "จังหวัด" + dt.Rows[0]["ProvinceNameTh"].ToString().TrimEnd();

                    txtZip1.Text = dt.Rows[0]["Postcode"].ToString().Substring(0, 1);
                    txtZip2.Text = "";
                    txtZip2.Text = dt.Rows[0]["Postcode"].ToString().Substring(1, 1);
                    txtZip3.Text = "";
                    txtZip3.Text = dt.Rows[0]["Postcode"].ToString().Substring(2, 1);
                    txtZip4.Text = "";
                    txtZip4.Text = dt.Rows[0]["Postcode"].ToString().Substring(3, 1);
                    txtZip5.Text = "";
                    txtZip5.Text = dt.Rows[0]["Postcode"].ToString().Substring(4, 1);

                    txtTambolId.Text = dt.Rows[0]["DistrictId"].ToString();
                    txtAmphurId.Text = dt.Rows[0]["AmphurId"].ToString();
                    txtProvinceId.Text = dt.Rows[0]["ProvinceId"].ToString();
                    txtGeoId.Text = dt.Rows[0]["GeographyId"].ToString();

                    txtId.Text = dt.Rows[0]["AddressCode"].ToString() == "" ? "" : dt.Rows[0]["AddressCode"].ToString();

                    btnSearch.Focus();
                }
                else
                {
                    string Mes = "ไม่พบ";
                    Mes += rdbCode.Checked == true ? "รหัสอ้างอิง '" + txtSearch.Text + "' กรุณาตรวจสอบอีกครั้ง" : "";
                    Mes += rdbZipcode.Checked == true ? "รหัสไปรษณีย์ '" + txtSearch.Text + "' กรุณาตรวจสอบอีกครั้ง" : "";
                    Mes += rdbName.Checked == true ? "ชื่อ - สกุล '" + txtSearch.Text + "' กรุณาตรวจสอบอีกครั้ง" : "";
                    Mes += rdbTambol.Checked == true ? " ตำบล '" + txtSearch.Text + "' กรุณาตรวจสอบอีกครั้ง" : "";

                    txtAddress2.Text = strRoad;
                    Message.MessageResult("N", "SH", Mes);

                    this.txtZip1.Text = "";
                    this.txtZip2.Text = "";
                    this.txtZip3.Text = "";
                    this.txtZip4.Text = "";
                    this.txtZip5.Text = "";
                    txtTamId.Text = "";
                    txtAumId.Text = "";
                    txtProId.Text = "";
                    lblAumphur.Text = "อำเภอ";
                    lblProvince.Text = "จังหวัด";

                    btnSearch.Focus();
                }
            }
            catch (Exception)
            {
                this.txtZip1.Text = "";
                this.txtZip2.Text = "";
                this.txtZip3.Text = "";
                this.txtZip4.Text = "";
                this.txtZip5.Text = "";
                btnSearch.Focus();
                lblAumphur.Text = "อำเภอ";
                lblProvince.Text = "จังหวัด";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                Clear("Image");
                Search();
            }
        }

        private void txtName_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "ชื่อ - นามสกุล")
            {
                txtName.Text = "";
            }
        }

        private void txtMobile_Click(object sender, EventArgs e)
        {
            if (txtMobile.Text == "เบอร์")
            {
                txtMobile.Text = "";
            }
        }

        private void txtAddress1_Click(object sender, EventArgs e)
        {
            if (txtAddress1.Text == "เลขที่ / หมู่บ้าน / หมู่ที่ / อาคาร / ชั้น / ห้อง / ซอย")
            {
                txtAddress1.Text = "";
            }
        }

        private void txtAddress2_Click(object sender, EventArgs e)
        {
            if (txtAddress2.Text == "ถนน")
            {
                txtAddress2.Text = "";
            }
        }

        private void txtZip1_TextChanged(object sender, EventArgs e)
        {
            txtZip2.Focus();
        }

        private void txtZip2_TextChanged(object sender, EventArgs e)
        {
            txtZip3.Focus();
        }

        private void txtZip3_TextChanged(object sender, EventArgs e)
        {
            txtZip4.Focus();
        }

        private void txtZip4_TextChanged(object sender, EventArgs e)
        {
            txtZip5.Focus();
        }

        private void txtZip5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                txtZip5.Text = "";
                txtZip4.Focus();
            }
            else if (e.KeyChar == (char)Keys.Enter)
            {
                Search();
            }
        }

        private void txtZip4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                txtZip4.Text = "";
                txtZip3.Focus();
            }
        }

        private void txtZip3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                txtZip3.Text = "";
                txtZip2.Focus();
            }
        }

        private void txtZip2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                txtZip2.Text = "";
                txtZip1.Focus();
            }
        }

        private void txtZip1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                txtZip1.Text = "";
                txtZip1.Focus();
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                txtName.Text = "ชื่อ - นามสกุล";
            }
        }

        private void txtMobile_Leave(object sender, EventArgs e)
        {
            if (txtMobile.Text == "")
            {
                txtMobile.Text = "เบอร์";
            }
            else if (txtMobile.Text != "")
            {
                txtMobile.Text = Fn.ConvertPhoneNumber(txtMobile.Text);
            }
            else
            {
            }
        }

        private void txtAddress1_Leave(object sender, EventArgs e)
        {
            if (txtAddress1.Text == "")
            {
                txtAddress1.Text = "เลขที่ / หมู่บ้าน / หมู่ที่ / อาคาร / ชั้น / ห้อง / ซอย";
            }
        }

        private void txtAddress2_Leave(object sender, EventArgs e)
        {
            if (txtAddress2.Text == "")
            {
                txtAddress2.Text = "ถนน";
            }
        }

        private void txtSend_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtName.Text == "ชื่อ - นามสกุล")
                {
                    txtName.Text = "";
                }

                txtName.Focus();
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtMobile.Text == "เบอร์")
                {
                    txtMobile.Text = "";
                }

                txtMobile.Focus();
            }
        }

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtAddress1.Text == "เลขที่ / หมู่บ้าน / หมู่ที่ / อาคาร / ชั้น / ห้อง / ซอย")
                {
                    txtAddress1.Text = "";
                }

                txtAddress1.Focus();
            }
        }

        private void txtAddress1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (txtTambol.Text == "ตำบล")
                {
                    txtTambol.Text = "";
                }

                txtTambol.Focus();
            }
        }

        private void txtAddress2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtZip1.Focus();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                Clear("Image");

                txtPAddress1.Text = "";
                txtPAddress1.Text += cbbStart.Text == ":: กรุณาเลือก ::" ? "" : cbbStart.Text;
                txtPAddress1.Text += txtName.Text == "ชื่อ - นามสกุล" ? "" : txtName.Text;

                txtPAddress2.Text = "";
                txtPAddress2.Text += txtAddress1.Text == "เลขที่ / หมู่บ้าน / หมู่ที่ / อาคาร / ชั้น / ห้อง / ซอย" ? "" : txtAddress1.Text.Replace("\r\n\n", string.Empty);

                txtPAddress3.Text = "";
                txtPAddress3.Text += txtAddress2.Text == "ถนน" ? "" : txtAddress2.Text + " ";
                txtPAddress3.Text += txtTambol.Text == "ตำบล" ? "" : txtTambol.Text.TrimEnd();

                txtPAddress4.Text = "";
                txtPAddress4.Text += lblAumphur.Text == "อำเภอ" ? "" : lblAumphur.Text.TrimEnd();
                txtPAddress4.Text += lblProvince.Text == "จังหวัด" ? "" : " " + lblProvince.Text.TrimEnd();
                txtPAddress4.Text = txtPAddress4.Text.TrimEnd();

                txtPCode1.Text = txtZip1.Text;
                txtPCode2.Text = txtZip2.Text;
                txtPCode3.Text = txtZip3.Text;
                txtPCode4.Text = txtZip4.Text;
                txtPCode5.Text = txtZip5.Text;

                txtPPhone.Text = txtMobile.Text == "เบอร์" ? "" : txtMobile.Text;

                //if (txtPAddress1.Text != "")
                //{
                //    string strDataBarcode = "";
                //    strDataBarcode = "A1-3456789012-3";
                //            //+ "---------------------------"
                //            //+ System.Environment.NewLine + txtPAddress1.Text
                //            //+ System.Environment.NewLine + txtPAddress2.Text
                //            //+ System.Environment.NewLine + txtPAddress3.Text
                //            //+ System.Environment.NewLine + txtPAddress4.Text
                //            //+ System.Environment.NewLine + "โทร " + txtPPhone.Text
                //            //+ System.Environment.NewLine + "---------------------------";
                //    pbNProductBarcode.Image = Barcode.QRCode(strDataBarcode, Color.Black, Color.White, "M", 3);
                //}
            }
            catch (Exception)
            {
            }
        }

        private void txtTambol_Leave(object sender, EventArgs e)
        {
            if (txtTambol.Text == "")
            {
                txtTambol.Text = "ตำบล";
            }
        }

        private void txtTambol_Click(object sender, EventArgs e)
        {
            if (txtTambol.Text == "ตำบล")
            {
                txtTambol.Text = "";
            }
        }

        private void rdbCode_CheckedChanged(object sender, EventArgs e)
        {
            Clear("All");
        }

        private void rdbName_CheckedChanged(object sender, EventArgs e)
        {
            Clear("All");
        }

        private void rdbZipcode_CheckedChanged(object sender, EventArgs e)
        {
            Clear("All");
        }

        private void rdbTambol_CheckedChanged(object sender, EventArgs e)
        {
            Clear("All");
        }

        private void FrmMailAddress_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string keyCode = Fn.keyPress(sender, e);

                if (keyCode == "Ctrl+S")
                {
                    btnAdd_Click(sender, e);
                }
                if (keyCode == "Ctrl+E")
                {
                }
                if (keyCode == "Ctrl+D")
                {
                }
                if (keyCode == "Ctrl+F")
                {
                    Search();
                }
                if (keyCode == "Alt+C")
                {
                    btnClear_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Search();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text != "")
                {
                    strOpe = "U";

                    string strNum = "";
                    string strMoo = "";
                    string strBuild = "";
                    string strHome = "";
                    string strRoom = "";
                    string strFloor = "";
                    string strSoi = "";

                    string strAdd = txtAddress1.Text;
                    string[] Address = strAdd.Split(' ');
                    int r = 0;

                    foreach (string a in Address)
                    {
                        strNum = a == "เลขที่" ? Address[r + 1] : strNum == "" ? "" : strNum;
                        strMoo = a == "หมู่ที่" ? Address[r + 1] : strMoo == "" ? "" : strMoo;
                        strRoom = a == "ห้อง" ? Address[r + 1] : strRoom == "" ? "" : strRoom;
                        strFloor = a == "ชั้น" ? Address[r + 1] : strFloor == "" ? "" : strFloor;
                        strSoi = a.Contains("ซอย") == true ? Address[r] : strSoi == "" ? "" : strSoi;
                        strBuild = a.Contains("อาคาร") == true ? Address[r] : strBuild == "" ? "" : strBuild;
                        strHome = a.Contains("หมู่บ้าน") == true ? Address[r] : strHome == "" ? "" : strHome;
                        r++;
                    }

                    string[,] Parameter = new string[,]
                    {
                       {"@AddressCode", txtId.Text},
                       {"@AddressPrefix", cbbStart.Text == ":: กรุณาเลือก ::"? "" : cbbStart.Text},
                       {"@AddressName", txtName.Text},
                       {"@Address", txtAddress1.Text},
                       {"@AddressNumber", strNum == ""? "" : strNum},
                       {"@AddressMoo", strMoo == ""? "" : strMoo},
                       {"@AddressBuilding", strBuild == ""? "" : strBuild},
                       {"@AddressHome", strHome == ""? "" : strHome},
                       {"@AddressRoom", strRoom == ""? "" : strRoom},
                       {"@AddressFloor", strFloor == ""? "" : strFloor},
                       {"@AddressSoi", strSoi == ""? "" : strSoi},
                       {"@AddressRoad", txtAddress2.Text == "ถนน" ? "" : txtAddress2.Text.Substring(txtAddress2.Text.IndexOf("ถนน"),txtAddress2.TextLength)},
                       {"@AddressDistrict", txtTambolId.Text},
                       {"@AddressAmphur", txtAmphurId.Text},
                       {"@AddressPrivince", txtProvinceId.Text},
                       {"@AddressGeo", txtGeoId.Text},
                       {"@AddressPostcode", txtZip1.Text + txtZip2.Text + txtZip3.Text + txtZip4.Text + txtZip5.Text},
                       {"@AddressPhone", txtMobile.Text},
                       {"@AddressSend", "2"},
                       {"@AddressStatus", "Y"},
                       {"@User",strUserId},
                    };

                    Message.MessageConfirmation(strOpe, txtId.Text, "แก้ไขที่อยู่ " + cbbStart.Text + txtName.Text);

                    using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                    {
                        var result = Mes.ShowDialog();

                        if (result == DialogResult.Yes)
                        {
                            Mes.Close();
                            db.Operations("Spr_U_TblSaveAddress", Parameter, out strErr);

                            if (strErr == null)
                            {
                                Message.MessageResult(strOpe, "C", strErr);
                                Clear("All");
                            }
                            else
                            {
                                Message.MessageResult(strOpe, "E", strErr);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                Log.WriteLogData(strAppCode, strAppName, strUserId, ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text != "")
                {
                    strOpe = "D";

                    string[,] Parameter = new string[,]
                    {
                       {"@AddressCode", txtId.Text},
                    };

                    Message.MessageConfirmation(strOpe, txtId.Text, "ลบที่อยู่ " + cbbStart.Text + txtName.Text);

                    using (var Mes = new FrmMessagesBox(Message.strOperation, Message.strMes, "YES", "NO", Message.strImage))
                    {
                        var result = Mes.ShowDialog();

                        if (result == DialogResult.Yes)
                        {
                            Mes.Close();
                            db.Operations("Spr_D_TblSaveAddress", Parameter, out strErr);

                            if (strErr == null)
                            {
                                Message.MessageResult(strOpe, "C", strErr);
                                Clear("All");
                            }
                            else
                            {
                                Message.MessageResult(strOpe, "E", strErr);
                            }
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