using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SANSANG.Class; 
using SANSANG.Database;
using SANSANG.Constant;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmTrackPost : Form
    {
        public string UserId;
        public string UserName; 
        public string UserSurname;
        public string UserType;

        public string Error = "";
        public string AppCode = "SAVTP00";
        public string AppName = "FrmTrackPost";
        public string Ope = "";

        public bool Start = false;

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        private clsDate Date = new clsDate();
        private clsFunction Function = new clsFunction();
        private clsMessage Message = new clsMessage();
        private dbConnection db = new dbConnection();
        private CharacterConstant CharType = new CharacterConstant();
        private clsDataList List = new clsDataList();
        private DataListConstant DataList = new DataListConstant();
        private clsLog Log = new clsLog();
        private clsConvert Converts = new clsConvert();
        private clsBarcode Barcode = new clsBarcode();
        private PathConstant Paths = new PathConstant();
        private clsImage Images = new clsImage();
        private ImageConstant Pictures = new ImageConstant();
        private clsSearch Search = new clsSearch();
        private StoreConstant Store = new StoreConstant();
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(25);
        private OperationConstant Operation = new OperationConstant();

        private clsApi Api = new clsApi();

        private Timer Timer = new Timer();
        public string[,] Parameter = new string[,] { };

        public FrmTrackPost(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
        {
            InitializeComponent();

            UserId = UserIdLogin;
            UserName = UserNameLogin;
            UserSurname = UserSurNameLogin;
            UserType = UserTypeLogin;
        }

        private void FrmLoad(object sender, EventArgs e)
        {
            Loading.Show();
            Timer.Interval = (1000);
            Timer.Tick += new EventHandler(LoadList);
            Timer.Start();
        }

        private void LoadList(object sender, EventArgs e)
        {
            Start = true;
            gbFrm.Enabled = true;

            List.GetLists(cbbStatus, DataList.StatusTrackId);
            List.GetList(cbbBarcode, DataList.BarcodeId);

            Clear();
            Timer.Stop();
        }

        public void Clear()
        {
            txtLocation.Text = "";
            txtBarcode.Text = "";

            cbbStatus.SelectedIndex = 0;
            cbbBarcode.SelectedIndex = 0;
            cbbBarcode.Text = "";

            lblBuddha.Text = "2500";
            lblDate.Text = "01";
            lblDayName.Text = "จันทร์";
            lblMonthName.Text = "ม.ค.";
            lblTime.Text = "00:00 น.";
            pbBarcode.Image = null;
            GridView.DataSource = null;
            txtCount.Text = "0";
            txtStatus.Text = "";
            txtName.Text = "";

            SetStatus("Clear");

            gbTable.Visible = false;
            pbSignature.Visible = false;
            gbLastStatus.Text = "นำจ่ายสำเร็จ";
            txtBarcode.Focus();
        }

        public void ShowGridView(DataTable dt)
        {
            try
            {
                if (Function.GetRows(dt) == 0)
                {
                    GridView.DataSource = null;
                    txtCount.Text = Function.ShowNumberOfData(0);
                    btnExit.Focus();
                }
                else
                {
                    GridView.DataSource = null;
                    gbTable.Visible = true;
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "SNo", "DateTimes", "Location", "StatusName", "Barcode", "Postcode", "Number", "Id");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;
                    DataGridViewContentAlignment mr = DataGridViewContentAlignment.MiddleRight;

                    Function.showGridViewFormatFromStore(dtGrid, GridView,
                          " ลำดับ", 50, true, mc, mc
                        , "วัน/เวลา", 100, true, ml, ml
                        , "ตำแหน่ง", 100, true, ml, ml
                        , "สถานะ", 150, true, ml, ml
                        , "", 100, false, ml, ml
                        , "", 100, false, mc, mr
                        , "", 100, false, ml, ml
                        , "", 0, false, mc, mc
                    );

                    txtCount.Text = Function.ShowNumberOfData(Function.GetRows(dt));
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtBarcode.TextLength == 13)
            {
                SearchData(txtBarcode.Text);
            }
        }

        public void SearchData(string Barcodes)
        {
            try
            {
                if (Barcodes != "")
                {
                    Parameter = new string[,]
                    {
                        {"@Id", ""},
                        {"@Code", ""},
                        {"@Status", ""},
                        {"@User", ""},
                        {"@IsActive", ""},
                        {"@IsDelete", ""},
                        {"@Operation", Operation.SelectAbbr},
                        {"@TrackingId", ""},
                        {"@Barcode", Barcodes},
                        {"@Number", ""},
                        {"@Date", ""},
                        {"@Location", ""},
                        {"@Postcode", ""},
                        {"@Description", ""},
                        {"@DeliveryDate", ""},
                        {"@Receiver", ""},
                        {"@SignatureId", ""},
                    };

                    string Condition = GetCondition();
                    lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;

                    db.Get(Store.ManageTrackPost, Parameter, out Error, out dt);
                    ShowGridView(dt);
                    ShowData(dt);
                }
            }
            catch
            {

            }
        }

        public void ShowData(DataTable Data)
        {
            try
            {
                if (Function.GetRows(Data) != 0)
                {
                    pbBarcode.Image = Barcode.Code128(dt.Rows[0]["Barcode"].ToString(), Color.Black, Color.White, 35, true);

                    txtLocation.Text = dt.Rows[0]["Locations"].ToString();
                    cbbStatus.SelectedValue = dt.Rows[0]["Number"].ToString();
                    lblBuddha.Text = dt.Rows[0]["Buddha"].ToString();
                    lblDayName.Text = dt.Rows[0]["Days"].ToString();
                    lblDate.Text = dt.Rows[0]["DayNumber"].ToString();
                    lblMonthName.Text = dt.Rows[0]["Months"].ToString();
                    lblTime.Text = dt.Rows[0]["Time"].ToString();
                    txtStatus.Text = dt.Rows[0]["TrackStatus"].ToString();
                    txtName.Text = dt.Rows[0]["Receivers"].ToString();

                    var Signature = dt.Rows[0]["Signature"].ToString();

                    ShowSignature(Signature);
                    SetStatus(dt.Rows[0]["Number"].ToString());
                }
                else
                {
                    var Mes = new FrmMessagesBoxOK("Please Register Tracking First", "Tracking number not registered", "OK", "I4-8BEE9535E5-1");
                    Mes.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private string GetCondition()
        {
            try
            {
                string strCondition = "";

                strCondition += txtBarcode.Text != "" ? txtBarcode.Text : "";
                strCondition += cbbBarcode.Text != ":: กรุณาเลือก ::" ? cbbBarcode.Text : "";

                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return "";
            }
        }


        private void cbbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch.Focus();
        }

        private void Update(object sender, EventArgs e)
        {
            Clear();
            Api.UpdateTracking();
        }


        private void Enters(object sender, EventArgs e)
        {
            if (txtBarcode.TextLength == 13)
            {
                SearchData(txtBarcode.Text);
            }
        }

        private void KeyDowns(object sender, KeyEventArgs e)
        {
            string keyCode = Function.keyPress(sender, e);

            if (keyCode == "Enter")
            {
                SearchData(txtBarcode.Text);
            }
        }

        private void SetStatus(string Status)
        {
            Bitmap ImgBox       = new Bitmap(Images.GetLocationImage(Id: Pictures.Box));
            Bitmap ImgTruck     = new Bitmap(Images.GetLocationImage(Id: Pictures.Truck));
            Bitmap ImgSend      = new Bitmap(Images.GetLocationImage(Id: Pictures.Send));
            Bitmap ImgSuccess   = new Bitmap(Images.GetLocationImage(Id: Pictures.Success));
            Bitmap ImgUnSuccess = new Bitmap(Images.GetLocationImage(Id: Pictures.Unsuccess));

            if (Status.Contains("101") || Status.Contains("102") || Status.Contains("103"))
            {
                pbBox.Image = ImgBox;
                pbTruck.Image = Converts.ImageGrayScale(ImgTruck);
                pbSend.Image = Converts.ImageGrayScale(ImgSend);
                pbSuccess.Image = Converts.ImageGrayScale(ImgSuccess);
                txtStatus.ForeColor = Color.DarkGray;
            }
            else if (Status.Contains("201") || Status.Contains("202") || Status.Contains("203") ||
                     Status.Contains("204") || Status.Contains("205") || Status.Contains("206") ||
                     Status.Contains("207"))
            {
                pbBox.Image = ImgBox;
                pbTruck.Image = ImgTruck;
                pbSend.Image = Converts.ImageGrayScale(ImgSend);
                pbSuccess.Image = Converts.ImageGrayScale(ImgSuccess);
                txtStatus.ForeColor = Color.Red;
            }
            else if (Status.Contains("301") || Status.Contains("302"))
            {
                pbBox.Image = ImgBox;
                pbTruck.Image = ImgTruck;
                pbSend.Image = ImgSend;
                pbSuccess.Image = Converts.ImageGrayScale(ImgSuccess);
                txtStatus.ForeColor = Color.Orange;
            }
            else if (Status.Contains("401"))
            {
                pbBox.Image = ImgBox;
                pbTruck.Image = ImgTruck;
                pbSend.Image = ImgSend;
                pbSuccess.Image = ImgUnSuccess;
                txtStatus.ForeColor = Color.Orange;
                gbLastStatus.Text = "นำจ่ายไม่สำเร็จ";
            }
            else if (Status.Contains("501"))
            {
                pbBox.Image = ImgBox;
                pbTruck.Image = ImgTruck;
                pbSend.Image = ImgSend;
                pbSuccess.Image = ImgSuccess;
                txtStatus.ForeColor = Color.Green;
                gbLastStatus.Text = "นำจ่ายสำเร็จ";
            }
            else
            {
                pbBox.Image = Converts.ImageGrayScale(ImgBox);
                pbTruck.Image = Converts.ImageGrayScale(ImgTruck);
                pbSend.Image = Converts.ImageGrayScale(ImgSend);
                pbSuccess.Image = Converts.ImageGrayScale(ImgSuccess);
                gbLastStatus.Text = "นำจ่ายสำเร็จ";
                txtStatus.Text = "";
            }
        }

        private void ShowSignature(string Signature)
        {
            if (Signature.ToString() != "")
            {
                byte[] bytes = Convert.FromBase64String(Signature);

                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    pbSignature.Visible = true;
                    pbSignature.Image = Image.FromStream(ms);
                }
            }
            else
            {
                pbSignature.Visible = false;
            }
        }

        private void pbList_Click(object sender, EventArgs e)
        {
            if (txtBarcode.Visible)
            {
                Clear();
                cbbBarcode.Visible = true;
                txtBarcode.Visible = false;
                pbUpdate.Focus();
            }
            else
            {
                Clear();
                cbbBarcode.Visible = false;
                txtBarcode.Visible = true;
                txtBarcode.Focus();
            }
            
        }

        private void cbbBarcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbBarcode.SelectedIndex != 0)
            {
                txtBarcode.Text = "";
                SearchData(cbbBarcode.Text);
            }

            btnExit.Focus();
        }

        private void KeyPresss(object sender, KeyPressEventArgs e)
        {
            if (!Function.IsCharacter(e.KeyChar, CharType.Tracking))
            {
                e.Handled = true;
            }
        }
    }
}