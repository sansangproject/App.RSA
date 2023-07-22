/// ModifyDate Developmentcompany    Describe
/// (c) Copyright TSS All rights reserved.

using System;

namespace SANSANG.Utilites.App.Model
{
    /// <summary>
    /// Kanban model
    /// </summary>
    public class ExpensesModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string User { get; set; }
        public string IsActive { get; set; }
        public string IsDelete { get; set; }
        public string Operation { get; set; }
        public string List { get; set; }
        public string MoneyId { get; set; }
        public string CategoryId { get; set; }
        public string ItemId { get; set; }
        public string IsDebit { get; set; }
        public string Item { get; set; }
        public string Detail { get; set; }
        public string Amount { get; set; }
        public string UnitId { get; set; }
        public string Unit { get; set; }
        public string Date { get; set; }
        public string Receipt { get; set; }

        public string ExpenseCode { get; set; }
        public string ExpenseMoney { get; set; }
        public string ExpensePay { get; set; }
        public string ExpensePaySub { get; set; }
        public string ExpenseDetails { get; set; }
        public string ExpenseItem { get; set; }
        public string ExpenseAmount { get; set; }
        public string ExpenseUnitId { get; set; }
        public string ExpenseUnit { get; set; }
        public string ExpenseIncome { get; set; }
        public string ExpenseStatus { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ExpenseRef { get; set; }
        public string UserId { get; set; }
        public string AppCode { get; set; }
        public string AppName { get; set; }
        public string ExpenseReciept { get; set; }
    }
}