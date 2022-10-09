using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SANSANG.Class; 
using SANSANG.Database;
using SANSANG.Utilites.App.Forms;
using SANSANG.Utilites.App.Model;

namespace RecordSystemApplication.App.Program.Application.Post
{
    public partial class FrmApiTrace : Form
    {
        public string strUserId;
        public string strUserName;
        public string strUserSurname;
        public string strUserType;

        public string strAppCode = "SENAP00";
        public string strAppName = "FrmApiTrace";
        public string strErr = "";
        public string strLaguage = "TH";
        public string strOpe = "";

        private clsApi TSSAPI = new clsApi();
        private clsFunction Fn = new clsFunction();

        public FrmApiTrace(string userIdLogin, string userNameLogin, string userSurNameLogin, string userTypeLogin)
        {
            InitializeComponent();

            strUserId = userIdLogin;
            strUserName = userNameLogin;
            strUserSurname = userSurNameLogin;
            strUserType = userTypeLogin;
        }

        private void FrmApiTrace_Load(object sender, EventArgs e)
        {

        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            TSSAPI.SearchItem(txtTrackCode.Text, strUserId);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            TSSAPI.UpdateTracking(strUserId);
        }
    }
}
