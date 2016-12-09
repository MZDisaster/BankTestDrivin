using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankSystem.DataAccessLayer;
using BankSystem.Models;
using BankSystem.ViewModels;

namespace BankSystem.Repository
{
    public class WorkingBankRepo : BankRepo
    {
        private IBankContext BContext;

        public WorkingBankRepo()
        {
            this.BContext = new BankContext();
        }

        public WorkingBankRepo(IBankContext context)
        {
            this.BContext = context;
        }

        public IEnumerable<Client> GetClients()
        {
            return BContext.Clients.Include("BankAccounts").Where(c => c.Active == true).ToList();
        }

        public Client GetClient(int Id)
        {
            return BContext.Clients.Include("BankAccounts").Single(c => c.Active == true && c.Id == Id);
        }

        public IEnumerable<Account> GetAccounts(int Id)
        {
            return BContext.Accounts.Where(a => a.ClientId == Id && a.Active == true).ToList();
        }

        public double GetBalance(int Id)
        {
            return BContext.Accounts.Single(a => a.Id == Id).Balance;
        }

        public IEnumerable<Transaction> GetTransActionHistory(int Id)
        {
            return BContext.Transactions.Where(t => t.FromId == Id || t.ToId == Id);
        }

        public void Deposit(Deposit deposit)
        {
            BContext.Deposists.Add(deposit);
            BContext.Accounts.Single(a => a.Id == deposit.AccountId).Balance += deposit.Amount;
            BContext.SaveChanges();
        }

        public void Withdraw(Withdraw withdraw)
        {
            if(!BContext.Accounts.Find(withdraw.AccountId).isLocked)
            {
                if (BContext.Accounts.Single(a => a.Id == withdraw.AccountId).Balance >= withdraw.Amount)
                {
                    BContext.Withdraws.Add(withdraw);
                    BContext.Accounts.Single(a => a.Id == withdraw.AccountId).Balance -= withdraw.Amount;
                    BContext.SaveChanges();
                }
            }
        }

        public void Transaction(Transaction transaction)
        {
            if (BContext.Accounts.Single(a => a.Id == transaction.FromId).Balance >= transaction.Amount)
            {
                if (BContext.Accounts.Where(a => a.Id == transaction.ToId).Count() > 0)
                {
                    BContext.Transactions.Add(transaction);
                    BContext.Accounts.Single(a => a.Id == transaction.FromId).Balance -= transaction.Amount;
                    BContext.Accounts.Single(a => a.Id == transaction.ToId).Balance += transaction.Amount;
                    BContext.SaveChanges();
                }
            }
        }


        public Account GetAccount(int Id)
        {
            return BContext.Accounts.Find(Id);
        }


        public void AddAccount(Account account)
        {
            BContext.Accounts.Add(account);
            BContext.SaveChanges();
        }



        public void EditAccount(Account account)
        {
            Account oldAccount = BContext.Accounts.Find(account.Id);
            oldAccount = account;
            BContext.SaveChanges();
        }


        public void RemoveAccount(Account account)
        {
            BContext.Accounts.Find(account.Id).Active = false;
            BContext.SaveChanges();
        }


        public void AddClient(Client client)
        {
            BContext.Clients.Add(client);
            BContext.SaveChanges();
        }


        public HistoryModel History(int Id)
        {
            HistoryModel history = new HistoryModel()
            {
                Deposits = BContext.Deposists.Where(d => d.AccountId == Id).ToList(),
                Withdraws = BContext.Withdraws.Where(w => w.AccountId == Id).ToList(),
                Transactions = BContext.Transactions.Where(t => t.FromId == Id || t.ToId == Id).ToList()
            };

            return history;
        }


        public int GetClientId(Transaction transaction)
        {
            return BContext.Accounts.First(a => a.Id == transaction.FromId).ClientId;
        }

        public int GetClientId(Withdraw withdraw)
        {
            return BContext.Accounts.First(a => a.Id == withdraw.AccountId).ClientId;
        }

        public int GetClientId(Deposit deposit)
        {
            return BContext.Accounts.First(a => a.Id == deposit.AccountId).ClientId;
        }

        public int GetClientId(int AccountId)
        {
            return BContext.Accounts.First(a => a.Id == AccountId).ClientId;
        }


        public void RemoveClient(int Id)
        {
            BContext.Clients.Find(Id).Active = false;
            BContext.SaveChanges();
        }

        public void LockUnlockAccount(int Id)
        {
            BContext.Accounts.Find(Id).isLocked = !BContext.Accounts.Find(Id).isLocked;
            BContext.SaveChanges();
        }
    }
}