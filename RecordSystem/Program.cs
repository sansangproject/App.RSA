using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SANSANG.Utilites.App.Global;

namespace SANSANG
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
         {
            GlobalVar.WaterDataList = new List<Utilites.App.Model.WaterModel>();
            GlobalVar.ElectricDataList = new List<Utilites.App.Model.ElectricModel>();
            GlobalVar.ImageDataList = new List<Utilites.App.Model.ImageModel>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmWelcome());
        }
    }
}