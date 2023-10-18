﻿using System;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using SANSANG.Class;
using SANSANG.Constant;
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;

namespace SANSANG
{
    public partial class FrmManageMoney : Form
    {
        public string UserId;
        public string UserName;
        public string UserSurname;
        public string UserType;

        public string AppCode = "MANMN00";
        public string AppName = "FrmManageMoney";
        public string Error = "";

        private DataTable dt = new DataTable();
        private DataSet ds = new DataSet();
        private StoreConstant Store = new StoreConstant();
        private OperationConstant Operation = new OperationConstant();
        private DataListConstant DataList = new DataListConstant();
        private clsDelete Delete = new clsDelete();
        private clsEdit Edit = new clsEdit();
        private clsInsert Insert = new clsInsert();
        private clsFunction Function = new clsFunction();
        private clsMessage Message = new clsMessage();
        private dbConnection db = new dbConnection();
        private clsDataList List = new clsDataList();
        private clsLog Log = new clsLog();
        private clsImage Images = new clsImage();
        private TableConstant Table = new TableConstant();
        private ColumnConstant Column = new ColumnConstant();
        private FrmAnimatedProgress Loading = new FrmAnimatedProgress(25);
        private clsHelpper Helper = new clsHelpper();
        private Timer Timer = new Timer();
        public string[,] Parameter = new string[,] { };

        public FrmManageMoney(string UserIdLogin, string UserNameLogin, string UserSurNameLogin, string UserTypeLogin)
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
            Timer.Start();
            Timer.Tick += new EventHandler(LoadList);
        }

        private void LoadList(object sender, EventArgs e)
        {
            List.GetLists(cbbStatus, string.Format(DataList.StatusId, "0"));

            pb_Thai_True.Hide();
            pb_Thai_False.Show();
            gbForm.Enabled = true;
            Clear();
            Timer.Stop();
        }

        public void Clear()
        {
            Function.ClearAll(gbForm);
            Search(false);
        }

        public void ShowGridView(DataTable dt)
        {
            try
            {
                if (Function.GetRows(dt) == 0)
                {
                    GridView.DataSource = null;
                    txtCount.Text = Function.ShowNumberOfData(0);
                }
                else
                {
                    DataTable dtGrid = new DataTable();
                    dtGrid = dt.DefaultView.ToTable(true, "SNo", "Code", "Display", "Name", "NameEn", "StatusName", "Dates", "Id");

                    DataGridViewContentAlignment mc = DataGridViewContentAlignment.MiddleCenter;
                    DataGridViewContentAlignment ml = DataGridViewContentAlignment.MiddleLeft;

                    Function.showGridViewFormatFromStore(dtGrid, GridView,
                          "ลำดับ", 50, true, mc, mc
                        , "รหัส", 100, true, ml, ml
                        , "การชำระเงิน", 150, true, ml, ml
                        , "ภาษาไทย", 150, true, ml, ml
                        , "ภาษาอังกฤษ", 150, true, ml, ml
                        , "สถานะ", 50, true, ml, ml
                        , "ข้อมูล ณ วันที่", 150, true, mc, mc
                        , "", 0, false, mc, mc
                        );

                    txtCount.Text = Function.ShowNumberOfData(dt.Rows.Count);
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
            this.Close();
        }

        private void SearchData(object sender, EventArgs e)
        {
            Search(true);
        }

        public void Search(bool Search)
        {
            try
            {
                Parameter = new string[,]
                {
                    {"@Id", Search ? txtId.Text : ""},
                    {"@Code", Search ? txtCode.Text : ""},
                    {"@Name", Search ? txtName.Text : ""},
                    {"@NameEn", Search ? txtNameEn.Text : ""},
                    {"@Status", Search ? Function.GetComboId(cbbStatus) : "0"},
                    {"@Display", Search ? txtDisplay.Text : ""},
                    {"@User", ""},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.SelectAbbr},
                };

                string Condition = Function.ShowConditons(GetCondition());
                lblCondition.Text = Condition == "" ? "ทั้งหมด" : Condition;
                db.Get(Store.ManageMoney, Parameter, out Error, out dt);
                ShowGridView(dt);
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
                strCondition += txtName.Text != "" ? ", การชำระเงิน: " + txtName.Text : "";
                strCondition += txtNameEn.Text != "" ? ", ชื่ออังกฤษ: " + txtNameEn.Text : "";
                strCondition += txtDisplay.Text != "" ? ", ชื่อที่แสดง: " + txtDisplay.Text : "";
                strCondition += cbbStatus.Text != ":: กรุณาเลือก ::" ? ", สถานะ: " + cbbStatus.Text : "";
                return strCondition;
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
                return "";
            }
        }

        private void ShowData(DataTable dt)
        {
            try
            {
                if (Function.GetRows(dt) > 0)
                {

                    txtId.Text = dt.Rows[0]["Id"].ToString();
                    txtCode.Text = dt.Rows[0]["Code"].ToString();
                    txtName.Text = dt.Rows[0]["Name"].ToString();
                    txtNameEn.Text = dt.Rows[0]["NameEn"].ToString();
                    txtDisplay.Text = dt.Rows[0]["Display"].ToString();

                    lblDisplay.Text = dt.Rows[0]["Display"].ToString();

                    cbbStatus.SelectedValue = dt.Rows[0]["Status"].ToString();

                    if (txtDisplay.Text == txtNameEn.Text)
                    {
                        cb_Thai.Checked = true;
                        pb_Thai_True.Show();
                        pb_Thai_False.Hide();
                    }
                    else
                    {
                        pb_Thai_True.Hide();
                        pb_Thai_False.Show();
                        cb_Thai.Checked = false;
                    }

                    GridView.Focus();
                }
            }
            catch
            {

            }
        }

        private void EditData(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtCode.Text))
                {
                    Parameter = new string[,]
                    {
                        {"@Id", txtId.Text},
                        {"@Code", txtCode.Text},
                        {"@Name", txtName.Text},
                        {"@NameEn", txtNameEn.Text},
                        {"@Status", Function.GetComboId(cbbStatus)},
                        {"@Display", txtDisplay.Text},
                        {"@User", UserId},
                        {"@IsActive", "1"},
                        {"@IsDelete", "0"},
                        {"@Operation", Operation.UpdateAbbr},
                    };

                    if (Edit.Update(AppCode, AppName, UserId, Store.ManageMoney, Parameter, txtCode.Text, Details: txtName.Text))
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

        private void DeleteData(object sender, EventArgs e)
        {
            try
            {
                if (Delete.DropId(AppCode, AppName, UserId, 0, Table.Money, txtId, txtCode, Details: txtName.Text))
                {
                    Clear();
                }
            }
            catch (Exception ex)
            {
                Log.WriteLogData(AppCode, AppName, UserId, ex.Message);
            }
        }

        private void ClearData(object sender, EventArgs e)
        {
            Clear();
        }

        private void AddData(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtCode.Text))
                {
                    if (!Function.IsDuplicates(Table.Money, txtCode.Text, txtName.Text, Detail: string.Concat(txtCode.Text, " | ", txtName.Text)))
                    {
                        Parameter = new string[,]
                        {
                            {"@Id", txtId.Text},
                            {"@Code", txtCode.Text},
                            {"@Name", txtName.Text},
                            {"@NameEn", txtNameEn.Text},
                            {"@Status", Function.GetComboId(cbbStatus)},
                            {"@Display", txtDisplay.Text},
                            {"@User", UserId},
                            {"@IsActive", "1"},
                            {"@IsDelete", "0"},
                            {"@Operation", Operation.InsertAbbr},
                        };

                        if (Insert.Add(AppCode, AppName, UserId, Store.ManageMoney, Parameter, txtCode.Text, Details: txtName.Text))
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

        private void CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow RowIndex = this.GridView.Rows[e.RowIndex];

                Parameter = new string[,]
                {
                    {"@Id", RowIndex.Cells["Id"].Value.ToString()},
                    {"@Code",  ""},
                    {"@Name",  ""},
                    {"@NameEn", ""},
                    {"@Status", "0"},
                    {"@Display", ""},
                    {"@User", ""},
                    {"@IsActive", "1"},
                    {"@IsDelete", "0"},
                    {"@Operation", Operation.SelectAbbr},
                };

                db.Get(Store.ManageMoney, Parameter, out Error, out dt);
                ShowData(dt);
            }
        }

        private void FrmKeyDown(object sender, KeyEventArgs e)
        {
            string keyCode = Function.KeyPress(sender, e);

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
            if (keyCode == "Enter")
            {
                Search(true);
            }
        }

        private void Ticker(object sender, EventArgs e)
        {
            lblDisplay.Text = string.Empty;
            txtDisplay.Text = string.Empty;

            if (Helper.CheckboxTicker(sender, this))
            {
                txtDisplay.Text = txtNameEn.Text;
                lblDisplay.Text = txtNameEn.Text;
            }
            else
            {
                txtDisplay.Text = txtName.Text;
                lblDisplay.Text = txtName.Text;
            }
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCode.Text) && txtCode.Text.Count() == 3)
            {
                string Code = txtCode.Text;
                string NewCode = Function.GetRunningId(Table.Money, Column.MoneyId);
                txtCode.Text = NewCode == "" ? Code : string.Concat(Code, NewCode);
            }
        }
    }
}