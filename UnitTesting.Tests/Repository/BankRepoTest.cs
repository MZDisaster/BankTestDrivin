using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Models;
using BankSystem.Repository;
using BankSystem.Tests.DataAccessLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankSystem.Tests.Repository
{
    [TestClass]
    public class BankRepoTest
    {
        [TestMethod]
        public void GetClients_should_return_clients()
        {
            IEnumerable<Client> clients = ObjectCreator.Clients();
            var context = new TestBankContext();
            context.Clients.AddRange(clients);

            var Repo = new WorkingBankRepo(context);

            var result = Repo.GetClients();

            Assert.IsNotNull(result);

            var e1 = clients.GetEnumerator();
            var e2 = result.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
            {
                Assert.AreEqual(e1.Current, e2.Current);
            }
        }

        [TestMethod]
        public void GetAccounts_should_return_accounts()
        {
            IEnumerable<Account> accounts = ObjectCreator.Accounts1().Where(a => a.Active).Select(a => a);
            var context = new TestBankContext();
            context.Accounts.AddRange(accounts);

            var Repo = new WorkingBankRepo(context);

            var result = Repo.GetAccounts(1);

            Assert.IsNotNull(result);

            var e1 = accounts.GetEnumerator();
            var e2 = result.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
            {
                Assert.AreEqual(e1.Current, e2.Current);
            }
        }

        [TestMethod]
        public void GetBalance_should_return_balance()
        {
            IEnumerable<Account> accounts = ObjectCreator.Accounts1();
            var context = new TestBankContext();
            context.Accounts.AddRange(accounts);

            var Repo = new WorkingBankRepo(context);

            var result = Repo.GetBalance(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(double));
            Assert.AreEqual(result, 5000);
        }

        [TestMethod]
        public void GetTransactionHistory_should_return_ienumerable_of_transaction()
        {
            IEnumerable<Transaction> transactions = ObjectCreator.Transactions();
            var context = new TestBankContext();
            context.Transactions.AddRange(transactions);

            var Repo = new WorkingBankRepo(context);

            var result = Repo.GetTransActionHistory(1);
            var result2 = result.ToList();
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 5);
        }

        [TestMethod]
        public void Deposit_should_create_new_deposit()
        {
            var context = new TestBankContext();
            context.Accounts.AddRange(ObjectCreator.Accounts1());

            var Repo = new WorkingBankRepo(context);

            var oldCount = context.Deposists.Count();

            Repo.Deposit(new Deposit { Id = 1, AccountId = 1, Amount = 500 });

            var newCount = context.Deposists.Count();

            Assert.IsTrue(newCount == 1);
            Assert.AreEqual(oldCount + 1, newCount);
        }

        [TestMethod]
        public void withdraw_should_create_new_withdraw()
        {
            var context = new TestBankContext();
            context.Accounts.AddRange(ObjectCreator.Accounts1());

            var Repo = new WorkingBankRepo(context);
            var oldCount = context.Withdraws.Count();

            Repo.Withdraw(new Withdraw { Id = 1, AccountId = 1, Amount = 500 });

            var newCount = context.Withdraws.Count();

            Assert.IsTrue(newCount == 1);
            Assert.AreEqual(oldCount + 1, newCount);
        }


        [TestMethod]
        public void Transaction_should_create_new_transaction()
        {
            var context = new TestBankContext();
            context.Accounts.AddRange(ObjectCreator.Accounts1());

            var Repo = new WorkingBankRepo(context);
            var oldCount = context.Transactions.Count();

            Repo.Transaction(new Transaction { Id = 1, FromId = 1, ToId = 2, Amount = 500 });

            var newCount = context.Transactions.Count();

            Assert.IsTrue(newCount == 1);
            Assert.AreEqual(oldCount + 1, newCount);
        }

        [TestMethod]
        public void GetAccount_should_return_the_specified_account()
        {
            IEnumerable<Account> accounts = ObjectCreator.Accounts1();
            var context = new TestBankContext();
            context.Accounts.AddRange(accounts);
            var Repo = new WorkingBankRepo(context);

            var result = Repo.GetAccount(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
        }

        [TestMethod]
        public void AddAccount_should_create_new_account()
        {
            var context = new TestBankContext();
            var Repo = new WorkingBankRepo(context);

            Repo.AddAccount(new Account { Id = 1, ClientId = 1, Balance = 500 });

            var result = context.Accounts.Single(a => a.Id == 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, 1);
        }

        [TestMethod]
        public void RemoveAccount_should_set_account_Active_to_false()
        {
            var context = new TestBankContext();
            context.Accounts.AddRange(ObjectCreator.Accounts1());
            var Repo = new WorkingBankRepo(context);

            var account = context.Accounts.Single(a => a.Id == 1);

            Repo.RemoveAccount(account);

            Assert.IsFalse(account.Active);
        }

        [TestMethod]
        public void AddClient_should_create_new_client()
        {
            var context = new TestBankContext();
            var Repo = new WorkingBankRepo(context);

            Repo.AddClient(new Client { Id = 1, Name = "tester" });

            var result = context.Clients.Single(c => c.Id == 1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, "tester");
            Assert.AreEqual(result.Id, 1);
        }

        [TestMethod]
        public void History_should_return_HistoryModel()
        {
            var context = new TestBankContext();

            var Repo = new WorkingBankRepo(context);

            var result = Repo.History(1);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Deposits.Count() == 0);
            Assert.IsTrue(result.Transactions.Count() == 0);
            Assert.IsTrue(result.Withdraws.Count() == 0);
        }

        [TestMethod]
        public void GetClientId_should_return_int_id()
        {
            var context = new TestBankContext();
            context.Clients.AddRange(ObjectCreator.Clients());
            context.Accounts.AddRange(ObjectCreator.Accounts1());
            var Repo = new WorkingBankRepo(context);

            var result1 = Repo.GetClientId(new Deposit { Id = 1, AccountId = 1, Amount = 1 });
            var result2 = Repo.GetClientId(new Withdraw { Id = 1, AccountId = 1, Amount = 1 });
            var result3 = Repo.GetClientId(new Transaction { Id = 1, FromId = 1, ToId = 2 });
            var result4 = Repo.GetClientId(1);

            Assert.AreEqual(result1, 1);
            Assert.AreEqual(result2, 1);
            Assert.AreEqual(result3, 1);
            Assert.AreEqual(result4, 1);
        }

        [TestMethod]
        public void lock_should_lock_withdraw()
        {
            var context = new TestBankContext();
            context.Accounts.AddRange(ObjectCreator.Accounts1());

            var Repo = new WorkingBankRepo(context);
            Repo.LockUnlockAccount(1);

            var result = Repo.GetAccount(1);

            Assert.IsTrue(result.isLocked);
        }
    }
}
