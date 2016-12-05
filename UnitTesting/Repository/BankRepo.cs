using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnitTesting.DataAccessLayer;
using UnitTesting.Models;

namespace UnitTesting.Repository
{
    public interface BankRepo
    {
        IEnumerable<Client> GetClients();
        IEnumerable<Account> GetAccounts(int Id);
        double GetBalance(int Id);
        IEnumerable<Transaction> GetTransActionHistory(int Id);
        void Deposit(int Id, double Amount);
        void Withdraw(int Id, double Amount);
        void Transfer(int fromId, int toId, double amount);

    }
}