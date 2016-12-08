using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.Seleno;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Locators;
using BankSystem.Models;

namespace BankSystem.Tests.Views.Home.Pages
{
    public class AccountsPage : Page
    {
        public IEnumerable<Account> Table()
        {
            var TRs = Find.Element(By.TagName("tbody")).FindElements(By.TagName("tr"));

            List<Account> accountList = new List<Account>();

            foreach (var tr in TRs)
            {
                var TDs = tr.FindElements(By.TagName("td"));

                var link = TDs[1].FindElement(By.TagName("a")).GetAttribute("href");
                int routePosInLink = link.LastIndexOf('/') + 1;
                int id = int.Parse(link.Substring(routePosInLink));

                accountList.Add(new Account
                {
                    Id = 1,
                    ClientId = 1,
                    Balance = 500
                });
            }

            return accountList;
        }

        public DepositPage gotoDeposit(int id)
        {
            return Navigate.To<DepositPage>(By.CssSelector("a[href='/Home/Deposit/"+ id + "']"));
        }

        public WithdrawPage gotoWithdraw(int id)
        {
            return Navigate.To<WithdrawPage>(By.CssSelector("a[href='/Home/Withdraw/" + id + "']"));
        }

        public TransactionPage gotoTransaction(int id)
        {
            return Navigate.To<TransactionPage>(By.CssSelector("a[href='/Home/Transaction/" + id + "']"));
        }

        public HistoryPage gotoHistory(int id)
        {
            return Navigate.To<HistoryPage>(By.CssSelector("a[href='/Home/History/" + id + "']"));
        }

        public void DeleteAccount(int id)
        {
            Find.Element(By.CssSelector("a[href='/Home/Delete/" + id + "']")).Click();
        }
    }
}
