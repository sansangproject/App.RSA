using System;
using System.Data;
using System.Windows.Forms;
using SANSANG.Class;
using SANSANG.Database;
using SANSANG.Constant;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmTracks : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "SAVTK00";
        public string AppName = "FrmTracks";
        public string Error;
        public string Laguage;
        public bool Start = false;
        public int Row = 0;

        private DataTable dt = new DataTable();
        private DataTable ds = new DataTable();
        private clsDate Date = new clsDate();
        private clsInsert Insert = new clsInsert();
        private clsFunction Function = new clsFunction();
        private clsConvert Converts = new clsConvert();
        private clsMessage Message = new clsMessage();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private CharacterConstant CharType = new CharacterConstant();
        private DataListConstant DataList = new DataListConstant();
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(20);
        private OperationConstant Operation = new OperationConstant();
        private StoreConstant Store = new StoreConstant();
        private clsDelete Delete = new clsDelete();
        private clsEdit Edit = new clsEdit();
        private TableConstant Table = new TableConstant();
        private clsLog Log = new clsLog();
        private clsApi TSSAPI = new clsApi();
        private Timer Timer = new Timer();
        public string[,] Parameter = new string[,] { };

        public FrmTracks(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
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
            Timer.Interval = (2000);
            Timer.Tick += new EventHandler(LoadList);
            Timer.Start();
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "0"));
            List.GetList(cbbProvider, DataList.Transportation);

            List.GetList(cbbReceiver, DataList.ReceiverId);
            List.GetList(cbbSender, DataList.SenderId);

            Laguage = clsSetting.ReadLanguageSetting();
            Start = true;
            gbFrm.Enabled = true;

            Clear();
            Timer.Stop();
        }

        public void Clear()
        {
            List.GetList(cbbReceiver, DataList.ReceiverId);
            List.GetList(cbbSender, DataList.SenderId);

            cbbStatus.SelectedValue = "0";
            cbbReceiver.SelectedValue = "0";
            cbbSender.SelectedValue = "0";
            cbbProvider.SelectedValue = "0";

            dtTime.Text = "00:00:00";
            dtDate.Text = DateTime.Now.ToString();

            txtPrice.Text = "";
            txtWeight.Text = "";
            txtDetail.Text = "";
            txtRemark.Text = "";
            txtProduct.Text = "";
            txtBarcode.Text = "";
            txtCode.Text = "";
            txtReference.Text = "";

            SearchData(false);
        }

        public void ShowGridView(DataTable dt)
        {
            try
            {
                GridView.DataSource = null;

                if (Function.GetRows(dt) == 0)
                {
                    txtCount.Text = Function.ShowNumberOfData(0);
                    txtCode.Focus();

                    Message.MessageConfirmation("F", "", Laguage == "th" ? "ไม่พบข้อมูลที่ค้นหา" : "No data found");

                    using (var Mes = new FrmMessagesBoxOK(Message.strOperation, Message.strMes, "OK", Message.strImage))
                    {
                        var result = Mes.ShowDialog();
                    }
                }
                else
                {
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "SNo", "Transportation", "Barcode", "Product", "AddressSender", "AddressReceiver", "NameReceiver", "TrackStatusName", "Id");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                    Function.ShowGridViewFormatFromStore(dtGrid, GridView,
                          " ลำดับ", 50, true, mc, mc
                        , "ขนส่ง", 80, true, ml, ml
                        , "รหัสพัสดุ", 80, true, ml, ml
                        , "สินค้า", 150, true, ml, ml
                        , "ต้นทาง", 100, true, ml, ml
                        , "ปลายทาง", 100, true, ml, ml
                        , "ผู้รับ", 100, true, ml, ml
                        , "สถานะ", 100, true, ml, ml
                        , "", 0, false, mc, mc
                    );

                    txtCount.Text = Function.ShowNumberOfData(Function.GetRows(dt));
                    GridView.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void Exit(object sender, EventArgs e)
        {
            Close();
        }

        private void ClearData(object sender, EventArgs e)
        {
            Clear();
        }

        private void DeleteData(object sender, EventArgs e)
        {
            try
            {
                if (Delete.Drop(AppCode, AppName, UserId, 0, Table.Trackings, txtCode, Details: cbbProvider.Text + " (" + txtBarcode.Text + ")", true))
                {
                    Clear();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void EditData(object sender, EventArgs e)
        {
            try
            {
                if (txtId.Text != "")
                {
                    Parameter = new string[,]
                    {
                        {"@Id", txtId.Text},
                        {"@Code", txtCode.Text},
                        {"@Status", Function.GetComboId(cbbStatus)},
                        {"@User", UserId},
                        {"@IsActive", Function.GetComboId(cbbStatus) == "1000"? "1" : "0"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.UpdateAbbr},
                        {"@Barcode", txtBarcode.Text},
                        {"@Provider", Function.GetComboId(cbbProvider)},
                        {"@Date", Date.GetDate(dtp : dtDate)},
                        {"@Time", dtTime.Text},
                        {"@Sender", Function.GetComboId(cbbSender)},
                        {"@Receiver", Function.GetComboId(cbbReceiver)},
                        {"@Product", txtProduct.Text},
                        {"@Weight", txtWeight.Text},
                        {"@Price", txtPrice.Text},
                        {"@Detail", txtDetail.Text},
                        {"@Reference", txtReference.Text},
                        {"@Remark", txtRemark.Text},
                    };

                    if (Edit.Update(AppCode, AppName, UserId, Store.ManageTrackings, Parameter, txtCode.Text, Details: cbbProvider.Text + " (" + txtBarcode.Text + ")"))
                    {
                        Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void Search(object sender, EventArgs e)
        {
            SearchData(true);
        }

        public void SearchData(bool Search)
        {
            Parameter = new string[,]
            {
                {"@Id", Search? "" : ""},
                {"@Code", Search? txtCode.Text : ""},
                {"@Status", Search? Function.GetComboId(cbbStatus) : "0"},
                {"@User", Search? "" : ""},
                {"@IsActive", Search? "" : ""},
                {"@IsDelete", Search? "" : ""},
                {"@Operation", Operation.SelectAbbr},
                {"@Barcode", Search? txtBarcode.Text : ""},
                {"@Provider", Search? Function.GetComboId(cbbProvider) : "0"},
                {"@Date", Search? "" : ""},
                {"@Time", Search? "" : ""},
                {"@Sender", Search? Function.GetComboId(cbbSender) : "0"},
                {"@Receiver", Search? Function.GetComboId(cbbReceiver) : "0"},
                {"@Product", Search? txtProduct.Text : ""},
                {"@Weight", Search? txtWeight.Text : "0.000"},
                {"@Price", Search? txtPrice.Text : "0.00"},
                {"@Detail", Search? txtDetail.Text : ""},
                {"@Reference", txtReference.Text},
                {"@Remark", Search? txtRemark.Text : ""},
            };

            string Condition = Function.ShowConditons(GetCondition());
            lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;

            db.Get(Store.ManageTrackings, Parameter, out Error, out dt);
            ShowGridView(dt);
        }

        public void ShowData(DataTable Data)
        {
            try
            {
                if (Function.GetRows(Data) > 0)
                {
                    cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                    cbbSender.SelectedValue = dt.Rows[0]["Sender"].ToString();
                    cbbReceiver.SelectedValue = dt.Rows[0]["Receiver"].ToString();
                    cbbProvider.SelectedValue = dt.Rows[0]["Provider"].ToString();

                    txtId.Text = dt.Rows[0]["Id"].ToString();
                    txtCode.Text = dt.Rows[0]["Code"].ToString();
                    txtBarcode.Text = dt.Rows[0]["Barcode"].ToString();
                    txtProduct.Text = dt.Rows[0]["Product"].ToString();
                    txtDetail.Text = dt.Rows[0]["Detail"].ToString();
                    txtReference.Text = dt.Rows[0]["Reference"].ToString();
                    txtRemark.Text = dt.Rows[0]["Remark"].ToString();
                    txtPrice.Text = dt.Rows[0]["Price"].ToString();
                    txtWeight.Text = dt.Rows[0]["Weight"].ToString();
                    dtTime.Text = dt.Rows[0]["Time"].ToString();
                    dtDate.Text = dt.Rows[0]["Date"].ToString();

                    GridView.Focus();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }
        private void AddData(object sender, EventArgs e)
        {
            try
            {
                if ((!string.IsNullOrEmpty(txtBarcode.Text) && cbbSender.SelectedValue.ToString() != "0" && cbbProvider.SelectedValue.ToString() != "0"))
                {
                    if (!Function.IsDuplicates(Table.Trackings, txtBarcode.Text, cbbProvider.SelectedValue.ToString(), Detail: cbbProvider.Text + " (" + txtBarcode.Text + ")"))
                    {
                        txtCode.Text = Function.GetCodes(Table.TrackingsId, "", "Generated");

                        Parameter = new string[,]
                        {
                            {"@Id", ""},
                            {"@Code", txtCode.Text},
                            {"@Status", Function.GetComboId(cbbStatus)},
                            {"@User", UserId},
                            {"@IsActive", "1"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                            {"@Barcode", txtBarcode.Text},
                            {"@Provider", Function.GetComboId(cbbProvider)},
                            {"@Date", Date.GetDate(dtp : dtDate)},
                            {"@Time", dtTime.Text},
                            {"@Sender", Function.GetComboId(cbbSender)},
                            {"@Receiver", Function.GetComboId(cbbReceiver)},
                            {"@Product", txtProduct.Text},
                            {"@Weight", string.IsNullOrEmpty(txtWeight.Text)? "0.000" : txtWeight.Text},
                            {"@Price", string.IsNullOrEmpty(txtPrice.Text)? "0.00" : txtPrice.Text},
                            {"@Detail", txtDetail.Text},
                            {"@Reference", txtReference.Text},
                            {"@Remark", txtRemark.Text},
                        };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageTrackings, Parameter, txtCode.Text, Details: cbbProvider.Text + " (" + txtBarcode.Text + ")"))
                        {
                            Clear();
                        }
                    }
                }
                else
                {
                    Message.ShowRequestData();
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

                strCondition += txtCode.Text != "" ? ", รหัสอ้างอิง: " + txtCode.Text : "";
                strCondition += cbbProvider.Text != ":: กรุณาเลือก ::" ? ", บริษัทขนส่ง: " + cbbProvider.Text : "";
                strCondition += txtBarcode.Text != "" ? ", Tracking: " + txtBarcode.Text : "";
                strCondition += cbbSender.Text != ":: กรุณาเลือก ::" ? ", ผู้ส่ง: " + cbbSender.Text : "";
                strCondition += cbbReceiver.Text != ":: กรุณาเลือก ::" ? ", ผู้รับ: " + cbbReceiver.Text : "";
                strCondition += txtProduct.Text != "" ? ", สิ่งของ: " + txtProduct.Text : "";
                strCondition += txtPrice.Text != "" ? ", ราคา: " + txtPrice.Text : "";
                strCondition += txtWeight.Text != "" ? ", น้ำหนัก: " + txtWeight.Text : "";
                strCondition += txtDetail.Text != "" ? ", รายละเอียด: " + txtDetail.Text : "";
                strCondition += txtReference.Text != "" ? ", เลขอ้างอิง: " + txtReference.Text : "";
                strCondition += txtRemark.Text != "" ? ", หมายเหตุ: " + txtRemark.Text : "";
                strCondition += cbbStatus.Text != ":: กรุณาเลือก ::" ? ", สถานะ: " + cbbStatus.Text : "";

                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return "";
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow Row = this.GridView.Rows[e.RowIndex];

                string[,] Parameter = new string[,]
                {
                    {"@Id", Row.Cells["Id"].Value.ToString()},
                    {"@Code", ""},
                    {"@Status", "0"},
                    {"@User", ""},
                    {"@IsActive", ""},
                    {"@IsDelete", ""},
                    {"@Operation", Operation.SelectAbbr},
                    {"@Barcode", ""},
                    {"@Provider", "0"},
                    {"@Date", ""},
                    {"@Time", ""},
                    {"@Sender", "0"},
                    {"@Receiver", "0"},
                    {"@Product", ""},
                    {"@Weight", "0.000"},
                    {"@Price", "0.00"},
                    {"@Detail", ""},
                    {"@Reference", ""},
                    {"@Remark", ""},
                };

                db.Get(Store.ManageTrackings, Parameter, out Error, out dt);
                ShowData(dt);
            }
        }

        private void Print(object sender, EventArgs e)
        {
        }

        private void FrmKeyDown(object sender, KeyEventArgs e)
        {
            string keyCode = Function.keyPress(sender, e);

            if (keyCode == "Ctrl+S")
            {
                AddData(sender, e);
            }
            if (keyCode == "Ctrl+E")
            {
                EditData(sender, e);
            }
            if (keyCode == "Ctrl+D")
            {
                DeleteData(sender, e);
            }
            if (keyCode == "Altl+C")
            {
                ClearData(sender, e);
            }
            if (keyCode == "Ctrl+P")
            {
                Print(sender, e);
            }
            if (keyCode == "Enter")
            {
                SearchData(true);
            }
        }

        private void cbbReceiver_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbReceiver.SelectedValue.ToString() != "0")
            {
                cbbStatus.SelectedValue = "1000";
                txtProduct.Focus();
            }
        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
            Function.SetComma(txtPrice);
        }

        private void pbUpdate_Click(object sender, EventArgs e)
        {
            TSSAPI.UpdateTracking();
            Clear();
        }

        private void Copy(object sender, EventArgs e)
        {
            try
            {
                if (txtBarcode.Text != "")
                {
                    Clipboard.SetText(txtBarcode.Text);
                }
            }
            catch
            {

            }
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Function.IsCharacter(e.KeyChar, CharType.Tracking))
            {
                e.Handled = true;
            }
            else
            {
                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (!string.IsNullOrEmpty(txtBarcode.Text))
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
                            {"@Barcode", txtBarcode.Text},
                            {"@Provider", "0"},
                            {"@Date", ""},
                            {"@Time", ""},
                            {"@Sender", "0"},
                            {"@Receiver", "0"},
                            {"@Product", ""},
                            {"@Weight", "0.000"},
                            {"@Price", "0.00"},
                            {"@Detail", ""},
                            {"@Reference", ""},
                            {"@Remark", ""},
                        };

                        db.Get(Store.ManageTrackings, Parameter, out Error, out dt);
                        ShowData(dt);
                    }
                }
                else
                {
                    e.KeyChar = char.ToUpper(e.KeyChar);
                }
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            txtWeight.Text = txtWeight.Text == "" ? "0.000" : txtWeight.Text;
        }

        private void btnSeller_Click(object sender, EventArgs e)
        {
            cbbSender.SelectedValue = "100123";
            List.GetList(cbbReceiver, DataList.ReceiverId, "1090");
            txtProduct.Focus();
        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            List.GetList(cbbReceiver, DataList.ReceiverId);
            List.GetList(cbbSender, DataList.SenderId);

            cbbReceiver.SelectedValue = "100022";

        }

        private void cbbSender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbSender.SelectedValue.ToString() != "0")
            {
                cbbStatus.SelectedValue = "1000";
                txtProduct.Focus();
            }
        }

        private void txtProduct_Leave(object sender, EventArgs e)
        {
            Converts.TitleCase(txtProduct);
        }
    }
}