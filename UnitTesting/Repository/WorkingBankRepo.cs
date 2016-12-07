using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnitTesting.DataAccessLayer;
using UnitTesting.Models;

namespace UnitTesting.Repository
{
    public class WorkingBankRepo : BankRepo
    {
        private BankContext GContext = new BankContext();

        public IEnumerable<Client> GetClients()
        {
            return GContext.Set<Client>().Include("BankAccounts").ToList();
        }

        IEnumerable<Account> BankRepo.GetAccounts(int Id)
        {
            return GContext.Accounts.Where(a => a.ClientId == Id).ToList();
        }

        double BankRepo.GetBalance(int Id)
        {
            return GContext.Accounts.Single(a => a.Id == Id).Balance;
        }

        IEnumerable<Transaction> BankRepo.GetTransActionHistory(int Id)
        {
            return GContext.Transactions.Where(t => t.From.Id == Id || t.To.Id == Id).ToList();
        }

        void BankRepo.Deposit(int Id, double Amount)
        {
            GContext.Deposists.Add(new Deposit() { AccountId = Id, Amount = Amount });
            GContext.Accounts.Single(a => a.Id == Id).Balance += Amount;
            GContext.SaveChanges();
        }

        void BankRepo.Withdraw(int Id, double Amount)
        {
            GContext.Withdraws.Add(new Withdraw() { AccountId = Id, Amount = Amount });
            GContext.Accounts.Single(a => a.Id == Id).Balance -= Amount;
            GContext.SaveChanges();
        }

        void BankRepo.Transfer(int fromId, int toId, double amount)
        {
            GContext.Transactions.Add(new Transaction() { FromId = fromId, ToId = toId, Amout = amount });
            GContext.Accounts.Single(a => a.Id == fromId).Balance -= amount;
            GContext.Accounts.Single(a => a.Id == toId).Balance += amount;
            GContext.SaveChanges();
        }


        public Account GetAccount(int Id)
        {
            return GContext.Accounts.Find(Id);
        }


        public void AddAccount(Account account)
        {
            GContext.Accounts.Add(account);
            GContext.SaveChanges();
        }



        public void EditAccount(Account account)
        {
            Account oldAccount = GContext.Accounts.Find(account.Id);
            oldAccount = account;
            GContext.SaveChanges();
        }


        public void RemoveAccount(Account account)
        {
            GContext.Accounts.Remove(account);
            GContext.SaveChanges();
        }


        public void AddClient(Client client)
        {
            GContext.Clients.Add(client);
            GContext.SaveChanges();
        }
    }
}