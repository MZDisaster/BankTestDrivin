using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BankSystem.Models;

namespace BankSystem.DataAccessLayer
{
    public interface IBankContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Withdraw> Withdraws { get; set; }
        DbSet<Deposit> Deposists { get; set; }
        int SaveChanges();
    }
}