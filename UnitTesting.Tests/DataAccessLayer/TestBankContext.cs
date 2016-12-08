using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BankSystem.Models;
using BankSystem.Tests.DataAccessLayer;
using BankSystem.DataAccessLayer;

namespace BankSystem.Tests.DataAccessLayer
{
    public class TestBankContext : IBankContext
    {

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Withdraw> Withdraws { get; set; }
        public DbSet<Deposit> Deposists { get; set; }

        public TestBankContext()
        {
            this.Accounts = new TestAccountDbSet();
            this.Clients = new TestClientDbSet();
            this.Transactions = new TestTransactionDbSet();
            this.Withdraws = new TestWithdrawDbSet();
            this.Deposists = new TestDepositDbSet();
        }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Client client)
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
