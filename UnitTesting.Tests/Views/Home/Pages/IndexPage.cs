using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Locators;
using BankSystem.Models;

namespace BankSystem.Tests.Views.Home.Pages
{
    public class IndexPage : Page
    {
        public IEnumerable<Client> Table()
        {
            var TRs = Find.Element(By.TagName("tbody")).FindElements(By.TagName("tr"));

            List<Client> clientList = new List<Client>();

            foreach(var tr in TRs)
            {
                var TDs = tr.FindElements(By.TagName("td"));

                var link = TDs.Last().FindElement(By.TagName("a")).GetAttribute("href");
                int routePosInLink = link.LastIndexOf('/') + 1;
                int id = int.Parse(link.Substring(routePosInLink));

                clientList.Add(new Client
                {
                    Name = TDs[0].Text,
                    Id = id
                });
            }

            return clientList;
        }

        public CreateClientPage gotoCreateClientPage()
        {
            return Navigate.To<CreateClientPage>(By.LinkText("Create Client"));
        }

        public AccountsPage gotoAccountsPage(int id)
        {
            return Navigate.To<AccountsPage>(By.CssSelector("a[href='/Home/Accounts/"+ id + "']"));
        }

        public IndexPage deleteAccount(int id)
        {
            return Navigate.To<IndexPage>(By.CssSelector("a[href='/Home/DeleteClient/" + id + "']"));
        }
    }
}
