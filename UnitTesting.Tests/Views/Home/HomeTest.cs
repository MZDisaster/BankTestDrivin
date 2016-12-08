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
        public void Index_Contains_Clients()
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
        public void CreateClient_page_test()
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
    }
}
