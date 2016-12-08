using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BankSystem;
using BankSystem.Controllers;
using Telerik.JustMock;
using BankSystem.Repository;
using BankSystem.Models;
using BankSystem.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BankSystem.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_Returning_All_Clients()
        {
            var clientList = ObjectCreator.Clients();

            var Repo = Mock.Create<BankRepo>();
            Mock.Arrange(() => Repo.GetClients()).
                Returns(clientList).MustBeCalled();

            HomeController controller = new HomeController(Repo);
            ViewResult viewResult = controller.Index();
            var model = viewResult.Model as IEnumerable<Client>;

            Assert.IsNotNull(model);
            Assert.AreEqual(model.Count(), clientList.Count());

        }

        [TestMethod]
        public void Accounts_returning_all_active_accounts()
        {
            var accountList = ObjectCreator.Accounts1();

            var Repo = Mock.Create<BankRepo>();
            Mock.Arrange(() => Repo.GetAccounts(1)).
                Returns(accountList).MustBeCalled();

            HomeController controller = new HomeController(Repo);
            ViewResult viewResult = controller.Accounts(1) as ViewResult;
            var model = viewResult.Model as IEnumerable<Account>;

            Assert.IsNotNull(model);
            Assert.AreEqual(model.Count(), accountList.Count()); // test more
        }


        [TestMethod]
        public void CreateClient_creates_Client()
        {
            Client client = new Client { Id = 1, Name = "Testestero" };
            List<Client> clientList = new List<Client>();

            var Repo = Mock.Create<BankRepo>();
            Mock.Arrange(() => Repo.AddClient(client)).DoInstead((Client c) => clientList.Add(c));

            HomeController controller = new HomeController(Repo);
            controller.CreateClient(client);

            Assert.IsTrue(clientList.Contains(client));
        }


        [TestMethod]
        public void CreateAccount_creates_Account()
        {
            Account account = new Account { ClientId = 1, Active = true};
            IList<Account> accountList = new List<Account>();
            
            var Repo = Mock.Create<BankRepo>();
            Mock.Arrange(() => Repo.AddAccount(account)).DoInstead((Account c) => accountList.Add(c));

            HomeController controller = new HomeController(Repo);
            RedirectToRouteResult actionResult = controller.CreateAccount(1);
            
            Assert.IsTrue(actionResult.RouteName != "Index"); // needs changing
        }

        [TestMethod]
        public void Deposit_creates_Deposit()
        {
            Deposit deposit = new Deposit { Id = 1, AccountId = 1, Amount = 500};
            IList<Deposit> depositList = new List<Deposit>();

            var Repo = Mock.Create<BankRepo>();
            Mock.Arrange(() => Repo.Deposit(deposit)).DoInstead((Deposit c) => depositList.Add(c));

            HomeController controller = new HomeController(Repo);
            controller.Deposit(deposit);

            Assert.IsTrue(depositList.Contains(deposit));
        }

        [TestMethod]
        public void Withdraw_creates_Withdraw()
        {
            Withdraw withdraw = new Withdraw { Id = 1, AccountId = 1, Amount = 500 };
            IList<Withdraw> withdrawList = new List<Withdraw>();

            var Repo = Mock.Create<BankRepo>();
            Mock.Arrange(() => Repo.Withdraw(withdraw)).DoInstead((Withdraw c) => withdrawList.Add(c));

            HomeController controller = new HomeController(Repo);
            controller.Withdraw(withdraw);

            Assert.IsTrue(withdrawList.Contains(withdraw));
        }

        [TestMethod]
        public void Transaction_creates_Transaction()
        {
            Transaction transaction = new Transaction { Id = 1, FromId = 1, ToId = 2, Amount = 500 };
            IList<Transaction> transactionList = new List<Transaction>();

            var Repo = Mock.Create<BankRepo>();
            Mock.Arrange(() => Repo.Transaction(transaction)).DoInstead((Transaction c) => transactionList.Add(c));

            HomeController controller = new HomeController(Repo);
            controller.Transaction(transaction);

            Assert.IsTrue(transactionList.Contains(transaction));
        }

        [TestMethod]
        public void History_contains_history()
        {
            List<Deposit> deposits = ObjectCreator.Deposists().ToList();
            List<Withdraw> withdraws = ObjectCreator.Withdraws().ToList();
            List<Transaction> transactions = ObjectCreator.Transactions().ToList();
            HistoryModel history = new HistoryModel
            {
                Deposits = deposits,
                Withdraws = withdraws,
                Transactions = transactions
            };

            bool called = false;

            var Repo = Mock.Create<BankRepo>();
            Mock.Arrange(() => Repo.History(1)).DoInstead(() => { called = true; }).Returns(history);

            HomeController controller = new HomeController(Repo);
            ViewResult viewResult = (ViewResult)controller.History(1);
            var model = viewResult.Model;


            Assert.IsTrue(called);
            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Contact() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
