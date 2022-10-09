using SANSANG.Constant;
using SANSANG.Database;
using System;
using System.Data;

namespace SANSANG.Class
{
    public class clsSearch
    {
        private dbConnection db = new dbConnection();
        private StoreConstant Store = new StoreConstant();
        public OperationConstant Operation = new OperationConstant();
        private clsFunction Function = new clsFunction();
        public string Error = "";
        private DataTable dt = new DataTable();
        string[,] Parameter = new string[,] { };

        public string Trackings(string Barcode)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", ""},
                    {"@Code", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", ""},
                    {"@IsDelete", ""},
                    {"@Operation", Operation.SelectAbbr},
                    {"@Barcode", Barcode},
                    {"@Provider", "0"},
                    {"@Date", ""},
                    {"@Time", ""},
                    {"@Sender", "0"},
                    {"@Receiver", "0"},
                    {"@Product", ""},
                    {"@Weight", "0.000"},
                    {"@Price", "0.00"},
                    {"@Detail", ""},
                    {"@Remark", ""},
                };

                db.Get(Store.ManageTrackings, Parameter, out Error, out dt);

                if (Function.GetRows(dt) == 1)
                {
                    return dt.Rows[0]["Id"].ToString();
                }
                else
                {
                    return "";
                }
            }
            catch (Exception)
            {
                return "";
            }
        }



























        public void GetExpensePaySub(string strPaySubName, string strPaymentCode, out string strCode, out string strType)
        {
            strCode = "";
            strType = "";

            string[,] Parameter = new string[,]
                    {
                    {"@MsPaymentSubCode",""},
                    {"@MsPaymentSubNameTh",""},
                    {"@MsPaymentSubNameEn", strPaySubName},
                    {"@MsPaymentSubDetail",""},
                    {"@MsPaymentCode",strPaymentCode},
                    {"@MsPaymentSubType",""},
                    {"@MsPaymentSubStatus","0"},
                    };

            db.Get("Spr_S_TblMasterPaymentSub", Parameter, out Error, out dt);

            int row = 0;
            row = dt.Rows.Count;

            if (row == 1)
            {
                strCode = dt.Rows[0]["MsPaymentSubCode"].ToString();
                strType = dt.Rows[0]["MsPaymentSubType"].ToString();
            }
        }

        public DataTable getAddressForPrint(string code)
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

            string[,] Parameter = new string[,]
            {
                {"@Code", code}
            };

            db.Get("Spr_S_PostAddress", Parameter, out Error, out dt);

            if (string.IsNullOrEmpty(Error))
            {
                AddressRow = dtAddress.NewRow();
                AddressRow["Name"] = dt.Rows[0]["AddressPrefix"].ToString() + dt.Rows[0]["AddressName"].ToString();
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
    }
}