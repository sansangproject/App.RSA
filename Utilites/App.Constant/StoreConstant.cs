using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SANSANG.Constant
{
    public class StoreConstant
    {

        public static string MST = "[mst]";
        public static string DBO = "[dbo]";
        public static string SPR = "[spr]";
        public static string FN = "[fn]";

        public static string Stores = "{0}.{1}";

        public static string Path = "[Path]";
        public static string User = "[User]";
        public static string Menu = "[Menu]";
        public static string Images = "[Images]";
        public static string Item = "[Item]";
        public static string Category = "[Category]";
        public static string Expense = "[Expense]";
        public static string Payments = "[Payments]";
        public static string Delete = "[Delete]";
        public static string Tambol = "[Tambol]";
        public static string Postcode = "[Postcode]";
        public static string Address = "[Address]";
        public static string Trackings = "[Trackings]";
        public static string TrackPost = "[TrackPosts]";
        public static string Signatures = "[Signatures]";
        public static string Logo = "[Logo]";
        public static string Shop = "[Shop]";
        public static string Dept = "[Debts]";
        public static string Card = "[Cards]";
        public static string Credit = "[Credits]";
        public static string Statement = "[Statements]";
        public static string Product = "[Products]";
        public static string Stock = "[Stocks]";
        public static string Member = "[Members]";

        public static string Workday = "[Workday]";
        public static string Gold = "[Golds]";
        public static string Money = "[Money]";

        public static string WaterAccount = "[WaterAccount]";
        public static string WaterRates = "[WaterRates]";
        public static string Waters = "[Waters]";

        public static string ElectricityAccount = "[ElectricityAccount]";
        public static string ElectricityRates = "[ElectricityRates]";
        public static string Electricitys = "[Electricitys]";

        public static string GetMasterList = "[GetMasterList]";
        public static string GetList = "[GetList]";

        public static string GetMoneyBalance = "[GetMoneyBalance]";
        public static string GetDataDuplicate = "[GetDataDuplicate]";

        public static string GetBalance = "[GetBalance]";
        public static string GetBalanceSearch = "[GetBalanceSearch]";
        public static string GetPostAddress = "[GetPostAddress]";
        public static string GetBankLogo = "[GetPath]";
        public static string GetBankBalance = "[GetBankBalance]";
        public static string GeneratedId = "[GeneratedId]";
        public static string GetTopId = "[GetTopId]";
        public static string GetItemId = "[GetItemId]";
        public static string GetReportIncomeExpense = "[GetReportIncomeExpense]"; 

        public string ManagePath = string.Format(Stores, SPR, Path);
        public string ManageUser = string.Format(Stores, SPR, User);
        public string ManageMenu = string.Format(Stores, SPR, Menu);
        public string ManageImage = string.Format(Stores, SPR, Images);
        public string ManageItem = string.Format(Stores, SPR, Item);
        public string ManageExpense = string.Format(Stores, SPR, Expense);
        public string ManagePayments = string.Format(Stores, SPR, Payments);
        public string ManageTambol = string.Format(Stores, SPR, Tambol);
        public string ManagePostcode = string.Format(Stores, SPR, Postcode);
        public string ManageAddress = string.Format(Stores, SPR, Address);
        public string ManageTrackings = string.Format(Stores, SPR, Trackings);
        public string ManageTrackPost = string.Format(Stores, SPR, TrackPost);
        public string ManageSignatures = string.Format(Stores, SPR, Signatures);
        public string ManageLogo = string.Format(Stores, SPR, Logo);
        public string ManageShop = string.Format(Stores, SPR, Shop);
        public string ManageDept = string.Format(Stores, SPR, Dept);
        public string ManageCard = string.Format(Stores, SPR, Card);
        public string ManageCredit = string.Format(Stores, SPR, Credit);
        public string ManageStatement = string.Format(Stores, SPR, Statement);
        public string ManageProduct = string.Format(Stores, SPR, Product);
        public string ManageStock = string.Format(Stores, SPR, Stock);
        public string ManageMember = string.Format(Stores, SPR, Member);
        public string ManageCategory = string.Format(Stores, SPR, Category);
        public string ManageMoney = string.Format(Stores, SPR, Money);
        public string ManageWaterAccount = string.Format(Stores, SPR, WaterAccount);
        public string ManageWaterRates = string.Format(Stores, SPR, WaterRates);
        public string ManageWaters = string.Format(Stores, SPR, Waters);

        public string ManageElectricityAccount = string.Format(Stores, SPR, ElectricityAccount);
        public string ManageElectricityRates = string.Format(Stores, SPR, ElectricityRates);
        public string ManageElectricitys = string.Format(Stores, SPR, Electricitys);

        public string ManageWorkday = string.Format(Stores, SPR, Workday);
        public string ManageGold = string.Format(Stores, SPR, Gold);

        public string DeleteData = string.Format(Stores, SPR, Delete);

        public string FnGetMasterList = string.Format(Stores, FN, GetMasterList);
        public string FnGetList = string.Format(Stores, FN, GetList);

        public string FnGetBalance = string.Format(Stores, FN, GetBalance);
        public string FnGetMoneyBalance = string.Format(Stores, FN, GetMoneyBalance);
        public string FnGetDataDuplicate = string.Format(Stores, FN, GetDataDuplicate);
        public string FnGetBalanceSearch = string.Format(Stores, FN, GetBalanceSearch);
        public string FnGetPostAddress = string.Format(Stores, FN, GetPostAddress);
        public string FnGetBankLogo = string.Format(Stores, FN, GetBankLogo);
        public string FnGetBankBalance = string.Format(Stores, FN, GetBankBalance);
        public string FnGeneratedId = string.Format(Stores, FN, GeneratedId);
        public string FnGetTopId = string.Format(Stores, FN, GetTopId);
        public string FnGetItemId = string.Format(Stores, FN, GetItemId);
        public string FnGetReportIncomeExpense = string.Format(Stores, FN, GetReportIncomeExpense);
    }
}
