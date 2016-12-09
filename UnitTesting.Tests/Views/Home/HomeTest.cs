using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.Seleno.PageObjects.Locators;
using BankSystem.Controllers;
using BankSystem.Tests.Views.Home.Pages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankSystem.Models;

namespace BankSystem.Tests.Views.Home
{
    [TestClass]
    public class HomeTest
    {
        [TestMethod]
        public void a_CreateClient_page_test()
        {
            var browser = BrowserHost.Instance.Application.Browser;
            var indexPage = BrowserHost.Instance.NavigateToInitialPage<HomeController, IndexPage>(x => x.Index());

            var newClientPage = indexPage.gotoCreateClientPage();

            indexPage = newClientPage.Submit<IndexPage>(new Client
            {
                Id = 1,
                Name = "tester"
            });

            Client newClientFromIndexList = indexPage.Table().Last();

            Assert.AreEqual(newClientFromIndexList.Name, "tester");
        }

        [TestMethod]
        public void b_Index_Contains_Clients()
        {
            var browser = BrowserHost.Instance.Application.Browser;
            var indexPage = BrowserHost.Instance.NavigateToInitialPage<HomeController, IndexPage>(x => x.Index());

            var createClientButton = browser.FindElement(By.LinkText("Create Client")).Text;
            var Table = browser.FindElement(By.TagName("Table")).Text;

            // Assert
            // Table exists with this header
            Assert.IsTrue(!string.IsNullOrEmpty(Table));
            Assert.IsTrue(!string.IsNullOrEmpty(createClientButton));
            Assert.IsNotNull(indexPage.Table());
        }

        [TestMethod]
        public void c_CreateAccount_should_create_new_account()
        {
            var browser = BrowserHost.Instance.Application.Browser;
            var indexPage = BrowserHost.Instance.NavigateToInitialPage<HomeController, IndexPage>(x => x.Index());
            Client newClientFromIndexList = indexPage.Table().Last();

            var accountPage = indexPage.gotoAccountsPage(newClientFromIndexList.Id);


            IEnumerable<Account> accountsList = accountPage.Table();
            int oldCount = accountsList.Count();

            accountPage = accountPage.CreateAccount();

            Assert.AreEqual(oldCount + 1, accountPage.Table().Count());

        }

        [TestMethod]
        public void d_Accounts_should_contain_list_of_account()
        {
            var browser = BrowserHost.Instance.Application.Browser;
            var indexPage = BrowserHost.Instance.NavigateToInitialPage<HomeController, IndexPage>(x => x.Index());

            Client newClientFromIndexList = indexPage.Table().Last();

            var accountPage = indexPage.gotoAccountsPage(newClientFromIndexList.Id);

            Assert.IsNotNull(accountPage.Table());
        }

        [TestMethod]
        public void e_Deposit_should_add_balance_to_account()
        {
            var browser = BrowserHost.Instance.Application.Browser;
            var indexPage = BrowserHost.Instance.NavigateToInitialPage<HomeController, IndexPage>(x => x.Index());

            Client newClientFromIndexList = indexPage.Table().Last();

            var accountPage = indexPage.gotoAccountsPage(newClientFromIndexList.Id);

            Account newAccountFromAccountPage = accountPage.Table().Last();
            double oldAccountBalance = newAccountFromAccountPage.Balance;


            var depositPage = accountPage.gotoDeposit(newAccountFromAccountPage.Id);

            accountPage = depositPage.Submit<AccountsPage>(
                    new Deposit
                    {
                        AccountId = newAccountFromAccountPage.Id,
                        Amount = 500
                    }
                );

            newAccountFromAccountPage = accountPage.Table().Last();

            Assert.AreEqual(oldAccountBalance + 500, newAccountFromAccountPage.Balance);
        }

        [TestMethod]
        public void f_Transaction_should_send_money()
        {
            var browser = BrowserHost.Instance.Application.Browser;
            var indexPage = BrowserHost.Instance.NavigateToInitialPage<HomeController, IndexPage>(x => x.Index());

            Client newClientFromIndexList = indexPage.Table().Last();

            var accountPage = indexPage.gotoAccountsPage(newClientFromIndexList.Id).CreateAccount();

            var table = accountPage.Table();

            Account newAccountFromAccountPage = table.Last();
            Account oldAccountFromAccountPage = table.ToList()[table.ToList().IndexOf(table.Last()) - 1];
            double oldnewAccountBalance = newAccountFromAccountPage.Balance;
            double oldoldAccountBalance = oldAccountFromAccountPage.Balance;

            var transactionPage = accountPage.gotoTransaction(newAccountFromAccountPage.Id);

            accountPage = transactionPage.Submit<AccountsPage>(
                    new Transaction
                    {
                        FromId = oldAccountFromAccountPage.Id,
                        ToId = newAccountFromAccountPage.Id,
                        Amount = 500
                    }
                );

            table = accountPage.Table();

            newAccountFromAccountPage = table.Last();
            oldAccountFromAccountPage = table.ToList()[table.ToList().IndexOf(table.Last()) - 1];

            Assert.AreEqual(oldnewAccountBalance + 500, newAccountFromAccountPage.Balance);
            Assert.AreEqual(oldoldAccountBalance - 500, oldAccountFromAccountPage.Balance);
        }

        [TestMethod]
        public void g_Withdraw_should_withdraw_money()
        {
            var browser = BrowserHost.Instance.Application.Browser;
            var indexPage = BrowserHost.Instance.NavigateToInitialPage<HomeController, IndexPage>(x => x.Index());

            Client newClientFromIndexList = indexPage.Table().Last();

            var accountPage = indexPage.gotoAccountsPage(newClientFromIndexList.Id);

            var table = accountPage.Table();

            Account newAccountFromAccountPage = table.Last();
            double oldBalance = newAccountFromAccountPage.Balance;

            var withdrawPage = accountPage.gotoWithdraw(newAccountFromAccountPage.Id);

            accountPage = withdrawPage.Submit<AccountsPage>(
                    new Withdraw
                    {
                        AccountId = newAccountFromAccountPage.Id,
                        Amount = 500
                    }
                );

            newAccountFromAccountPage = accountPage.Table().Last();

            Assert.AreEqual(oldBalance - 500, newAccountFromAccountPage.Balance);
        }

        [TestMethod]
        public void h_Delete_should_delete_accounts()
        {
            var browser = BrowserHost.Instance.Application.Browser;
            var indexPage = BrowserHost.Instance.NavigateToInitialPage<HomeController, IndexPage>(x => x.Index());

            Client newClientFromIndexList = indexPage.Table().Last();

            var accountPage = indexPage.gotoAccountsPage(newClientFromIndexList.Id);

            var table = accountPage.Table();

            Account newAccountFromAccountPage = table.Last();
            Account newAccountFromAccountPage2 = table.ToList()[table.ToList().IndexOf(table.Last()) - 1];

            accountPage = accountPage
                .DeleteAccount(newAccountFromAccountPage.Id)
                .Submit<AccountsPage>(newAccountFromAccountPage)
                .DeleteAccount(newAccountFromAccountPage2.Id)
                .Submit<AccountsPage>(newAccountFromAccountPage2);

            Assert.AreEqual(table.Count() - 2, accountPage.Table().Count());
        }

        [TestMethod]
        public void i_DeleteClient_should_delete_client()
        {
            var browser = BrowserHost.Instance.Application.Browser;
            var indexPage = BrowserHost.Instance.NavigateToInitialPage<HomeController, IndexPage>(x => x.Index());

            Client newClientFromIndexList = indexPage.Table().Last();
            int oldclientsCount = indexPage.Table().Count();

            indexPage = indexPage.deleteAccount(newClientFromIndexList.Id);

            Assert.AreEqual(oldclientsCount - 1, indexPage.Table().Count());
        }
    }
}
