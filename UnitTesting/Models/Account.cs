using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BankSystem.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        public double Balance { get; set; }

        public bool Active { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual List<Transaction> TransActionHistory { get; set; }

        public virtual List<Deposit> DepositHistory { get; set; }

        public virtual List<Withdraw> WithdrawHistory { get; set; }

        public Account()
        {
            this.Active = true;
        }
    }
}