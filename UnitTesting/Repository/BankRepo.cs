using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BankSystem.DataAccessLayer;
using BankSystem.Models;
using BankSystem.ViewModels;

namespace BankSystem.Repository
{
    public interface BankRepo
    {
        IEnumerable<Client> GetClients();
        int GetClientId(Transaction transaction);
        int GetClientId(Withdraw withdraw);
        int GetClientId(Deposit deposit);
        int GetClientId(int AccountId);
        void RemoveClient(int Id);
        IEnumerable<Account> GetAccounts(int Id);
        Account GetAccount(int Id);
        double GetBalance(int Id);
        IEnumerable<Transaction> GetTransActionHistory(int Id);
        void Deposit(Deposit deposit);
        void Withdraw(Withdraw withdraw);
        void Transaction(Transaction transaction);
        void AddAccount(Account account);
        void EditAccount(Account account);
        void RemoveAccount(Account account);
        void AddClient(Client client);
        HistoryModel History(int Id);
    }
}