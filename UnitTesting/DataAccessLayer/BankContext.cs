using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BankSystem.Models;

namespace BankSystem.DataAccessLayer
{
    public class BankContext : DbContext, IBankContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Withdraw> Withdraws { get; set; }
        public DbSet<Deposit> Deposists { get; set; }

        public BankContext() : base("DefaultConnection")
        {

        }

    }
}