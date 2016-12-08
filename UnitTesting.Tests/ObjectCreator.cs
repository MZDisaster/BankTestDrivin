using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Models;

namespace BankSystem.Tests
{
    public static class ObjectCreator
    {
        public static IEnumerable<Client> Clients(){
            Client[] clients = new Client[]{
                new Client(){Id = 1, Name = "Test 1", BankAccounts = ObjectCreator.Accounts1().ToList()},
                new Client(){Id = 2, Name = "Test 2", BankAccounts = ObjectCreator.Accounts2().ToList()},
                new Client(){Id = 3, Name = "Test 3", BankAccounts = ObjectCreator.Accounts3().ToList()},
                new Client(){Id = 4, Name = "Test 4"},
                new Client(){Id = 5, Name = "Test 5"},
                new Client(){Id = 6, Name = "Test 6"}
            };

            return clients;
        }

        public static IEnumerable<Account> Accounts1()
        {
            Account[] accounts = new Account[]{
                new Account{Balance = 5000, ClientId = 1, Id = 1},
                new Account{Balance = 55000, ClientId = 1, Id = 2},
                new Account{Balance = 6000, ClientId = 1, Id = 3},
                new Account{Balance = 88000, ClientId = 1, Id = 4},
                new Account{Balance = 74000, ClientId = 1, Id = 5},
                new Account{Balance = 0, ClientId = 1, Id = 6, Active = false},
                new Account{Balance = 44000, ClientId = 1, Id = 7}
            };

            return accounts;
        }

        public static IEnumerable<Account> Accounts2()
        {
            Account[] accounts = new Account[]{
                new Account{Balance = 5000, ClientId = 1, Id = 8},
                new Account{Balance = 55000, ClientId = 1, Id = 9},
                new Account{Balance = 6000, ClientId = 1, Id = 10},
                new Account{Balance = 88000, ClientId = 1, Id = 11},
                new Account{Balance = 74000, ClientId = 1, Id = 12},
                new Account{Balance = 2000, ClientId = 1, Id = 13},
                new Account{Balance = 44000, ClientId = 1, Id = 14}
            };

            return accounts;
        }

        public static IEnumerable<Account> Accounts3()
        {
            Account[] accounts = new Account[]{
                new Account{Balance = 5000, ClientId = 1, Id = 15},
                new Account{Balance = 55000, ClientId = 1, Id = 16},
                new Account{Balance = 6000, ClientId = 1, Id = 17},
                new Account{Balance = 88000, ClientId = 1, Id = 18},
                new Account{Balance = 74000, ClientId = 1, Id = 19},
                new Account{Balance = 2000, ClientId = 1, Id = 20},
                new Account{Balance = 44000, ClientId = 1, Id = 21}
            };

            return accounts;
        }

        public static IEnumerable<Transaction> Transactions()
        {
            Transaction[] transactions = new Transaction[]{
                new Transaction{ Id = 1, Amount = 500, FromId = 1, ToId = 2},
                new Transaction{ Id = 2, Amount = 500, FromId = 2, ToId = 2},
                new Transaction{ Id = 3, Amount = 500, FromId = 3, ToId = 2},
                new Transaction{ Id = 4, Amount = 500, FromId = 2, ToId = 1},
                new Transaction{ Id = 5, Amount = 500, FromId = 2, ToId = 1},
                new Transaction{ Id = 6, Amount = 500, FromId = 1, ToId = 2},
                new Transaction{ Id = 7, Amount = 500, FromId = 1, ToId = 2}
            };

            return transactions;
        }

        public static IEnumerable<Deposit> Deposists()
        {
            Deposit[] deposists = new Deposit[]{
                new Deposit{Id = 1, AccountId = 1, Amount = 500},
                new Deposit{Id = 2, AccountId = 2, Amount = 500},
                new Deposit{Id = 3, AccountId = 3, Amount = 500},
                new Deposit{Id = 4, AccountId = 4, Amount = 500},
                new Deposit{Id = 5, AccountId = 5, Amount = 500},
                new Deposit{Id = 6, AccountId = 6, Amount = 500},
                new Deposit{Id = 7, AccountId = 7, Amount = 500},
                new Deposit{Id = 8, AccountId = 1, Amount = 2200}
            };

            return deposists;
        }

        public static IEnumerable<Withdraw> Withdraws()
        {
            Withdraw[] withdraws = new Withdraw[]{
                new Withdraw{ Id = 1, AccountId = 1, Amount = 500},
                new Withdraw{ Id = 2, AccountId = 2, Amount = 500},
                new Withdraw{ Id = 3, AccountId = 3, Amount = 500},
                new Withdraw{ Id = 4, AccountId = 4, Amount = 500},
                new Withdraw{ Id = 5, AccountId = 5, Amount = 500},
                new Withdraw{ Id = 6, AccountId = 6, Amount = 500},
                new Withdraw{ Id = 7, AccountId = 7, Amount = 500},
                new Withdraw{ Id = 8, AccountId = 1, Amount = 5500}
            };

            return withdraws;
        }
    }
}
