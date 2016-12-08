using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankSystem.Models;

namespace BankSystem.ViewModels
{
    public class HistoryModel
    {
        public List<Transaction> Transactions { get; set; }
        public List<Deposit> Deposits { get; set; }
        public List<Withdraw> Withdraws { get; set; }
    }
}