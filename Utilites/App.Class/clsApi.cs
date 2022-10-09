using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using SANSANG.Constant;
using SANSANG.Utilites.App.Forms;
using SANSANG.Utilites.App.Model;
using SANSANG.Database;

namespace SANSANG.Class
{
    public class clsApi
    {
        public string Error = "";
        private clsSetting Setting = new clsSetting();
        private TrackConstant Trace = new TrackConstant();
        private ApiConstant Api = new ApiConstant();
        private clsFunction Function = new clsFunction();
        private dbConnection db = new dbConnection();
        private clsInsert Insert = new clsInsert();
        private clsSearch Search = new clsSearch();
        private clsDelete Delete = new clsDelete();
        private clsConvert Converts = new clsConvert();
        private StoreConstant Store = new StoreConstant();
        private OperationConstant Operation = new OperationConstant();
        private TableConstant Table = new TableConstant();

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        public string[,] Parameter = new string[,] { };

        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public bool CheckNet()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }

        public TokenModel CallWebClient()
        {
            string url = Setting.GetUrlAPIToken();
            string token = Setting.GetTrackToken();
            string method = "POST";
            string value;

            using (WebClient client = new WebClient())
            {
                client.Headers[HttpRequestHeader.Authorization] = token;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                value = client.UploadString(url, method);
            }

            TokenModel Token = new TokenModel(value);
            return Token;
        }

        public List<TrackModel> GetTrackItem(string token, string code)
        {
            string url = Setting.GetUrlAPITrack();
            List<string> barcodeList = new List<string>();
            barcodeList.Add(code);

            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Encoding = Encoding.UTF8;
                    client.Headers[HttpRequestHeader.Authorization] = token;
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";

                    var clientBody = new
                    {
                        status = Trace.StatusAll,
                        language = Trace.LangTh,
                        barcode = barcodeList
                    };

                    var json = JsonConvert.SerializeObject(clientBody);
                    var value = client.UploadString(url, Api.MethodPost, json);

                    JObject jObject = JObject.Parse(value);
                    JToken jResponse = jObject["response"];
                    JToken jItems = jResponse["items"];
                    JToken jTrack = jItems[code];

                    var data = JsonConvert.DeserializeObject<List<TrackModel>>(jTrack.ToString());
                    return data;
                }
                catch
                {
                    return null;
                }
            }
        }

        public void SearchItem(string Barcode)
        {
            try
            {
                if (CheckNet())
                {
                    int Number = Function.NumberDuplicate(Table.TrackPosts, Barcode);
                    string TrackingId = Search.Trackings(Barcode);

                    if (TrackingId != "")
                    {
                        var Token = "Token " + CallWebClient().Token;
                        var Data = GetTrackItem(Token, Barcode);

                        if (Number == 0)
                        {
                            Insert.TrackPost(Data, TrackingId);
                        }
                        else if (Data.Count > Number)
                        {
                            if (Delete.TrackPost(TrackingId))
                            {
                                Insert.TrackPost(Data, TrackingId);
                            }
                        }
                    }
                    else
                    {
                        var Message = new FrmMessagesBoxOK("Please Register Tracking First", "Tracking number not registered", "OK", Id: "1028");
                        Message.ShowDialog();
                    }
                }
            }
            catch
            {

            }
        }

        public void UpdateTracking()
        {
            try
            {
                if (CheckNet())
                {
                    Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", ""},
                        {"@Status", "1000"},
                        {"@User", ""},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.SelectAbbr},
                        {"@Barcode", ""},
                        {"@Provider", ""},
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

                    db.Gets(Store.ManageTrackings, Parameter, out Error, out ds);

                    List<string> Data = ds.Tables[1].AsEnumerable().Select(r => r.Field<string>("Barcode")).ToList();

                    foreach (var Item in Data)
                    {
                        SearchItem(Item);
                    }

                    var Message = new FrmMessagesBoxOK("Update Data Complete", "Tracking is up to date.", "OK", Id: "1021");
                    Message.ShowDialog();
                }
                else
                {
                    var Message = new FrmMessagesBoxOK("Please Check Network Connection", "Internet Not Connected", "OK", Id: "1034");
                    Message.ShowDialog();
                }
            }
            catch
            {

            }
        }
    }
}