using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Model;

namespace SANSANG.Class
{
    public class clsDataList
    {
        public string Error = "";

        public dbConnection db = new dbConnection();
        public DataTable dt = new DataTable();
        public DataSet ds = new DataSet();
        public clsFunction Fn = new clsFunction();
        public DataListConstant List = new DataListConstant();
        public StoreConstant Store = new StoreConstant();
        public string[,] Parameter = new string[,] { };

        public void GetList(ComboBox ComboBox, string Table, string Value = "")
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Table", Table},
                    {"@Value", Value},
                };

                db.Get(Store.FnGetList, Parameter, out Error, out dt);

                ComboBox.DataSource = dt;
                ComboBox.DisplayMember = "Name";
                ComboBox.ValueMember = "Id";
                ComboBox.SelectedValue = "0";
            }
            catch
            {

            }
        }

        public void GetLists(ComboBox ComboBox, string List)
        {
            string[] Value = List.Split(new char[0]);

            try
           {
                string[,] Parameter = new string[,]
                {
                    {"@Table", Value[0]},
                    {"@Column", Value[1]},
                    {"@Type", Value[2]},
                    {"@Where", Value[3]},
                    {"@Value", Value[4]},
                };

                db.GetList(Store.FnGetMasterList, Parameter, out Error, out ds);
                dt = ds.Tables[0];

                ComboBox.DataSource = dt;
                ComboBox.DisplayMember = "Name";
                ComboBox.ValueMember = Value[2];
                ComboBox.SelectedValue = "0";
            }
            catch
            {

            }
        }

        public void GetDebitCreditList(ComboBox ComboBoxs)
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr;

                dt.Columns.Add("Id");
                dt.Columns.Add("Name");

                dr = dt.NewRow();
                dr["Name"] = ":: กรุณาเลือก ::";
                dr["Id"] = "9";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["Name"] = "รายรับ";
                dr["Id"] = "1";
                dt.Rows.Add(dr);

                dr = dt.NewRow();
                dr["Name"] = "รายจ่าย";
                dr["Id"] = "0";
                dt.Rows.Add(dr);

                ComboBoxs.DataSource = dt;
                ComboBoxs.DisplayMember = "Name";
                ComboBoxs.ValueMember = "Id";
                ComboBoxs.SelectedIndex = 0;
            }
            catch
            {

            }
        }

        public void GetMonthList(ComboBox ComboBoxs)
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("Id");
            dt.Columns.Add("Value");

            dr = dt.NewRow();
            dr["Id"] = "0";
            dr["Value"] = ":: กรุณาเลือก ::";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = "1";
            dr["Value"] = "มกราคม";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = "2";
            dr["Value"] = "กุมภาพันธ์";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = "3";
            dr["Value"] = "มีนาคม";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = "4";
            dr["Value"] = "เมษายน";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = "5";
            dr["Value"] = "พฤษภาคม";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = "6";
            dr["Value"] = "มิถุนายน";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = "7";
            dr["Value"] = "กรกฎาคม";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = "8";
            dr["Value"] = "สิงหาคม";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = "9";
            dr["Value"] = "กันยายน";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = "10";
            dr["Value"] = "ตุลาคม";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = "11";
            dr["Value"] = "พฤศจิกายน";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Id"] = "12";
            dr["Value"] = "ธันวาคม";
            dt.Rows.Add(dr);

            ComboBoxs.DataSource = dt;
            ComboBoxs.DisplayMember = "Value";
            ComboBoxs.ValueMember = "Id";
            ComboBoxs.SelectedIndex = 0;
        }

        public void GetYearList(ComboBox ComboBoxs)
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("Id");
            dt.Columns.Add("Value");

            dr = dt.NewRow();
            dr["Id"] = "0";
            dr["Value"] = ":: กรุณาเลือก ::";
            dt.Rows.Add(dr);

            Int64 years = Convert.ToInt64(DateTime.Now.Year.ToString()) + 543;

            dr = dt.NewRow();
            dr["Id"] = years.ToString();
            dr["Value"] = years.ToString();
            dt.Rows.Add(dr);

            for (int i = 12; i >= 1; i--)
            {
                dr = dt.NewRow();
                dr["Id"] = Convert.ToString(years - i);
                dr["Value"] = Convert.ToString(years - i);
                dt.Rows.Add(dr);
            }

            for (int i = 1; i <= 2; i++)
            {
                dr = dt.NewRow();
                dr["Id"] = Convert.ToString(years + i);
                dr["Value"] = Convert.ToString(years + i);
                dt.Rows.Add(dr);
            }

            ComboBoxs.DataSource = dt;
            ComboBoxs.DisplayMember = "Value";
            ComboBoxs.ValueMember = "Id";
            ComboBoxs.SelectedIndex = 0;
        }








































        //public void GetList(ComboBox ccbName, string strValue, string strCbb, int format = 0)
        //{
        //    try
        //    {
        //        //if (strCbb == List.Status)
        //        //{
        //        //    string[,] Parameter = new string[,]
        //        //    {
        //        //        {"@MsStatusType", strValue},
        //        //    };

        //        //    db.GetList("Spr_L_TblMasterStatus", Parameter, out Error, out ds);
        //        //}

        //        if (strCbb == List.StatusAll)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@MsStatusType", strValue},
        //            };

        //            db.GetList("Spr_L_TblMasterStatusAll", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Type)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@MsStatusType", strValue},
        //            };

        //            db.GetList("Spr_L_TblMasterStatus", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.CreditBank)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@Status", strValue},
        //            };

        //            db.GetList("Spr_L_TblMasterCreditBank", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Water)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@Type", strValue},
        //                {"@Authority", "MWA"},
        //            };

        //            db.GetList("mst.Spr_L_MetropolitanAuthority", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Electricity)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@Type", strValue},
        //                {"@Authority", "MEA"},
        //            };

        //            db.GetList("mst.Spr_L_MetropolitanAuthority", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.HospitalId)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@MsHospitalStatus", strValue}
        //            };

        //            db.GetList("Spr_L_TblMasterHospital", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.PatientId)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@MsPatientStatus", strValue}
        //            };

        //            db.GetList("Spr_L_TblMasterPatient", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Path)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@Status", strValue}
        //            };

        //            db.GetList("Store.ListPath", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Credit)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@MsCardStatus", strValue}
        //            };

        //            db.GetList("Spr_L_TblMasterCard", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Logo)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@Status", strValue}
        //            };

        //            db.GetList("Store.ListLogo", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Money)
        //        {
        //            string[,] Parameter = new string[,] { };

        //            if (format == 2)
        //            {
        //                Parameter = new string[,]
        //                {
        //                    {"@Format", format.ToString()},
        //                    {"@MsMoneyStatus", strValue},
        //                };
        //            }
        //            else
        //            {
        //                Parameter = new string[,]
        //                {
        //                    {"@Format", "0"},
        //                    {"@MsMoneyStatus", strValue},
        //                };
        //            }

        //            db.GetList("Spr_L_TblMasterMoney", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.UserType)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@MsUserTypeStatus", strValue}
        //            };

        //            db.GetList("Spr_L_TblMasterUserType", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Payment)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@MsPaymentStatus", strValue}
        //            };

        //            db.GetList("Spr_L_TblMasterPayment", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Transactions)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                 {"@MsPaymentCode", strValue}
        //                ,{"@MsPaymentSubStatus", "Y"}
        //            };

        //            db.GetList("Spr_L_TblMasterTransactions", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.PaymentSub)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@MsPaymentCode", Fn.SubstringBefore(strValue, ",")}
        //               ,{"@MsPaymentSubStatus", Fn.SubstringAfter(strValue, ",")}
        //            };

        //            db.GetList("Spr_L_TblMasterPaymentSub", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Coin)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@MsCoinStatus", strValue}
        //            };

        //            db.GetList("Spr_L_TblMasterCoin", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Member)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@MsLogoStatus", strValue.Substring(0, 1)},
        //                {"@MsLogoType", strValue.Substring(1, 2)}
        //            };

        //            db.GetList("Spr_L_TblMasterMemLogo", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.MainProductType)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@Status", strValue}
        //            };

        //            db.GetList("Spr_L_ProductType", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Account)
        //        {
        //            string[,] Parameter = new string[,] { };

        //            if (format != 0)
        //            {
        //                Parameter = new string[,]
        //                {
        //                    {"@AccountStatus", strValue},
        //                    {"@Format", format.ToString()}
        //                };
        //            }
        //            else
        //            {
        //                Parameter = new string[,]
        //                {
        //                    {"@Format", "0"},
        //                    {"@AccountStatus", strValue}
        //                };
        //            }

        //            db.GetList("Spr_L_TblSaveAccount", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.AccountType)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@MsAccountTypeStatus", strValue}
        //            };

        //            db.GetList("Spr_L_TblMasterAccountType", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Branch)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@MsBranchBank", strValue.Substring(0, 3)},
        //                {"@MsBranchStatus", strValue.Substring(4, 1)}
        //            };

        //            db.GetList("Spr_L_TblMasterBranch", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Owner)
        //        {
        //            string[,] Parameter = new string[,]
        //        {
        //            {"@MsLogoStatus", strValue},
        //            {"@MsLogoType", ""}
        //        };

        //            db.GetList("Spr_L_TblMasterMemLogo", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Geography)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@GeographyStatus", strValue},
        //            };

        //            db.GetList("Spr_L_TblMasterGeography", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.ElecOffice)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@MsElecOfficeStatus", strValue},
        //            };

        //            db.GetList("Spr_L_TblMasterElectricityOffice", Parameter, out Error, out ds);
        //        }
        //        if (strCbb == List.Address)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@AddressStatus", strValue},
        //            };

        //            db.GetList("Spr_L_Address", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.GeographyAll)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@GeographyStatus", strValue},
        //            };

        //            db.GetList("Spr_L_TblMasterGeographyAll", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.ProvinceAll)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@ProvinceId", ""},
        //                {"@ProvinceGeoId", ""},
        //                {"@ProvinceStatus", "0"},
        //            };

        //            db.GetList("Spr_L_TblMasterProvinceAll", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Table)
        //        {
        //            string[,] Parameter = new string[,] { };

        //            db.GetList("Spr_L_Table", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.TableWithSchema)
        //        {
        //            string[,] Parameter = new string[,] { };

        //            db.GetList("Spr_L_TableWithSchema", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Column)
        //        {
        //            if (strValue != "")
        //            {
        //                string[,] Parameter = new string[,]
        //                {
        //                    {"@TableName", strValue},
        //                };

        //                db.GetList("Spr_L_Column", Parameter, out Error, out ds);
        //            }
        //        }

        //        if (strCbb == List.ProductType)
        //        {
        //            if (strValue != "")
        //            {
        //                Parameter = new string[,]
        //                {
        //                    {"@Status", strValue},
        //                };

        //                db.GetList("Store.ListProductType", Parameter, out Error, out ds);
        //            }
        //        }

        //        if (strCbb == List.Unit)
        //        {
        //            if (strValue != "")
        //            {
        //                Parameter = new string[,]
        //                {
        //                    {"@Status", strValue},
        //                };

        //                db.GetReport("Store.ListUnit", Parameter, out Error, out ds);
        //            }
        //        }

        //        if (strCbb == List.Shop)
        //        {
        //            if (strValue != "")
        //            {
        //                string[,] Parameter = new string[,]
        //                {
        //                    {"@Status", strValue},
        //                };

        //                db.GetList("Spr_L_TblMasterShop", Parameter, out Error, out ds);
        //            }
        //        }

        //        if (strCbb == List.MonthDebt)
        //        {
        //            if (strValue != "")
        //            {
        //                string[,] Parameter = new string[,]
        //                {
        //                    {"@Status", strValue},
        //                };

        //                db.GetList("Spr_L_MonthAndYearOfData", Parameter, out Error, out ds);
        //            }
        //        }

        //        if (strCbb == List.PostLocation)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@Status", strValue},
        //            };

        //            db.GetList("Spr_L_TrackScanLocation", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.CreditCard)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@Status", strValue},
        //            };

        //            db.GetList("Spr_L_TblMasterCreditCard", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.PostAddress)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@AddressStatus", strValue},
        //            };

        //            db.GetList("Spr_L_PostAddress", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.AmphoeAll)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                {"@AmphoeId", ""},
        //                {"@AmphoeStatus", "Y"},
        //                {"@AmphoeProvinceId", ""}
        //            };

        //            db.GetList("Spr_L_TblMasterAmphoeAll", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.TambolAll)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                 {"@TambolId", ""},
        //                 {"@TambolStatus", "Y"},
        //                 {"@TambolAmphoeId", ""}
        //            };

        //            db.GetList("Spr_L_TblMasterTambolAll", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Receiver || strCbb == List.Sender)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                 {"@Type", ""},
        //            };

        //            db.GetList("Spr_L_TblSaveAddress", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Receivers)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                 {"@Type", strValue},
        //            };

        //            db.GetList("Spr_L_TblSaveAddress", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.TrackStatus)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                 {"@TrackStatusNumber", ""}
        //            };

        //            db.GetList("Spr_L_TblMasterTrackStatus", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.TrackCode)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                 {"@TrackPostCode", ""}
        //            };

        //            db.GetList("Spr_L_TblSaveTrackPost", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.StatusType)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                 {"@Status", ""}
        //            };

        //            db.GetReport("Store.ListStatusType", Parameter, out Error, out ds);
        //        }

        //        if (strCbb == List.Vicinity)
        //        {
        //            string[,] Parameter = new string[,]
        //            {
        //                 {"@MsStatusType", "15"}
        //            };

        //            db.GetList("Spr_L_TblMasterStatus", Parameter, out Error, out ds);
        //        }

        //        dt = ds.Tables[0];

        //        ccbName.DataSource = dt;
        //        ccbName.DisplayMember = "Name";
        //        ccbName.ValueMember = "Code";
        //    }
        //    catch
        //    {

        //    }
        //}

        public void GetListId(ComboBox ComboBox, string Table, string Value = "")
        {
            try
            {
                string[,] Parameter = new string[,]
                {
                        {"@Table", Table},
                        {"@Value", Value},
                };

                db.GetList("[spr].[L_Id]", Parameter, out Error, out ds);


                dt = ds.Tables[0];

                ComboBox.DataSource = dt;
                ComboBox.DisplayMember = "Name";
                ComboBox.ValueMember = "Id";
            }
            catch
            {

            }
        }

        public void getWhereList(ComboBox ccbName, string strCode, string strLevel, string strStatus, string strCbb)
        {
            try
            {
                if (strCbb == List.Province)
                {
                    string[,] Parameter = new string[,]
                    {
                        {"@ProvinceId", strCode},
                        {"@ProvinceStatus", strStatus},
                        {"@ProvinceGeoId", strLevel}
                    };

                    db.GetList("Spr_L_TblMasterProvince", Parameter, out Error, out ds);
                }

                if (strCbb == List.ProvinceAll)
                {
                    string[,] Parameter = new string[,]
                    {
                        {"@ProvinceId", strCode},
                        {"@ProvinceStatus", strStatus},
                        {"@ProvinceGeoId", strLevel}
                    };

                    db.GetList("Spr_L_TblMasterProvinceAll", Parameter, out Error, out ds);
                }

                if (strCbb == List.Amphoe)
                {
                    string[,] Parameter = new string[,]
                    {
                        {"@AmphoeId", strCode},
                        {"@AmphoeStatus", strStatus},
                        {"@AmphoeProvinceId", strLevel}
                    };

                    db.GetList("Spr_L_TblMasterAmphoe", Parameter, out Error, out ds);
                }

                if (strCbb == List.AmphoeAll)
                {
                    string[,] Parameter = new string[,]
                    {
                        {"@AmphoeId", strCode},
                        {"@AmphoeStatus", strStatus},
                        {"@AmphoeProvinceId", strLevel}
                    };

                    db.GetList("Spr_L_TblMasterAmphoeAll", Parameter, out Error, out ds);
                }

                if (strCbb == List.Tambol)
                {
                    string[,] Parameter = new string[,]
                    {
                         {"@TambolId", strCode},
                         {"@TambolStatus", strStatus},
                         {"@TambolAmphoeId", strLevel}
                    };

                    db.GetList("Spr_L_TblMasterTambol", Parameter, out Error, out ds);
                }

                if (strCbb == List.TambolAll)
                {
                    string[,] Parameter = new string[,]
                    {
                         {"@TambolId", strCode},
                         {"@TambolStatus", strStatus},
                         {"@TambolAmphoeId", strLevel}
                    };

                    db.GetList("Spr_L_TblMasterTambolAll", Parameter, out Error, out ds);
                }

                dt = ds.Tables[0];
                ccbName.DataSource = dt;
                ccbName.DisplayMember = "Name";
                ccbName.ValueMember = "Code";
            }
            catch (Exception)
            {
            }
        }

        public DataTable getPayTypeList()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("index");
            dt.Columns.Add("value");

            dr = dt.NewRow();
            dr["index"] = "0";
            dr["value"] = ":: กรุณาเลือก ::";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "1";
            dr["value"] = "เงินต้นคงที่";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "2";
            dr["value"] = "เงินต้นและดอกเบี้ยคงที่";
            dt.Rows.Add(dr);

            return dt;
        }

        public DataTable getDentalItemList()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("index");
            dt.Columns.Add("value");

            dr = dt.NewRow();
            dr["index"] = "0";
            dr["value"] = ":: กรุณาเลือก ::";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "ปรับเครื่องมือ";
            dr["value"] = "1000";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "ค่าบริการคลินิกปลอดเชื้อ";
            dr["value"] = "50";
            dt.Rows.Add(dr);

            return dt;
        }

        public DataTable getTextStartList()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("index");
            dt.Columns.Add("value");

            dr = dt.NewRow();
            dr["index"] = "0";
            dr["value"] = ":: กรุณาเลือก ::";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "1";
            dr["value"] = "กรุณาส่ง";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "2";
            dr["value"] = "ชื่อและที่อยู่ผู้รับ";
            dt.Rows.Add(dr);

            return dt;
        }

        public DataTable GetStatusId()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("id");
            dt.Columns.Add("value");

            for (int i = 0; i < 4; i++)
            {
                dr = dt.NewRow();
                dr["id"] = i;
                dr["value"] = "";
                dt.Rows.Add(dr);
            }

            return dt;
        }

        public DataTable GetPersonalGroupList()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("index");
            dt.Columns.Add("value");

            dr = dt.NewRow();
            dr["index"] = "0";
            dr["value"] = ":: กรุณาเลือก ::";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "1";
            dr["value"] = "พ่อ";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "2";
            dr["value"] = "แม่";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "3";
            dr["value"] = "ลูก";
            dt.Rows.Add(dr);

            return dt;
        }

        public DataTable getRankList()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("index");
            dt.Columns.Add("value");

            dr = dt.NewRow();
            dr["index"] = "0";
            dr["value"] = ":: กรุณาเลือก ::";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "1";
            dr["value"] = "ครู/อาจารย์";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "2";
            dr["value"] = "ชวด";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "3";
            dr["value"] = "ญาติ";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "4";
            dr["value"] = "ตา";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "5";
            dr["value"] = "ทวด";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "6";
            dr["value"] = "น้อง";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "7";
            dr["value"] = "น้า";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "8";
            dr["value"] = "ป้า";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "9";
            dr["value"] = "ปู่";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "10";
            dr["value"] = "พี่";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "11";
            dr["value"] = "เพื่อนบ้าน";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "12";
            dr["value"] = "ย่า";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "13";
            dr["value"] = "ยาย";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "14";
            dr["value"] = "ลุง";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "15";
            dr["value"] = "หลาน";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "16";
            dr["value"] = "เหลน";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "17";
            dr["value"] = "อา";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "18";
            dr["value"] = "เพื่อน";
            dt.Rows.Add(dr);

            return dt;
        }

        public DataTable GetJoinList()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("index");
            dt.Columns.Add("value");

            dr = dt.NewRow();
            dr["index"] = "0";
            dr["value"] = ":: กรุณาเลือก ::";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "11";
            dr["value"] = "มาร่วมงาน-มีซอง";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "10";
            dr["value"] = "มาร่วมงาน-ไม่มีซอง";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "01";
            dr["value"] = "ไม่มาร่วมงาน-มีซอง";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "00";
            dr["value"] = "ไม่มาร่วมงาน-ไม่มีซอง";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["index"] = "99";
            dr["value"] = "ไม่ระบุ";
            dt.Rows.Add(dr);

            return dt;
        }

        public DataTable getLedgerReportTypeItemList()
        {
            DataTable dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("id");
            dt.Columns.Add("value");

            dr = dt.NewRow();
            dr["id"] = "รายรับรายจ่าย";
            dr["value"] = "00";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["id"] = ":: กรุณาเลือก ::";
            //dr["value"] = "00";
            //dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["id"] = "รายรับ";
            dr["value"] = "1";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["id"] = "รายจ่าย";
            dr["value"] = "0";
            dt.Rows.Add(dr);

            return dt;
        }

        public void SetNewList(ComboBox ccb, TextBox tb)
        {
            try
            {
                string strValue = "";
                strValue = ccb.SelectedValue.ToString();

                if (strValue == "99")
                {
                    tb.Enabled = true;
                    tb.Text = "";
                    tb.Focus();
                }
                else
                {
                    tb.Enabled = false;
                    tb.Text = "";
                }
            }
            catch (Exception)
            {
            }
        }
    }
}