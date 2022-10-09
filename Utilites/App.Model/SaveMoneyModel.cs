/// ModifyDate Developmentcompany    Describe
/// (c) Copyright TSS All rights reserved.

using System;

namespace SANSANG.Utilites.App.Model
{
    /// <summary>
    /// Kanban model
    /// </summary>
    public class SaveMoneyModels
    {
        public bool CoinUsed { get; set; }
        public String CoinType { get; set; }
        public String CoinName { get; set; }
        public String CoinValue { get; set; }
        public String CoinDetail { get; set; }
        public DateTime CoinDate { get; set; }
        public String UserId { get; set; }
        public String AppCode { get; set; }
        public String AppName { get; set; }
    }
}