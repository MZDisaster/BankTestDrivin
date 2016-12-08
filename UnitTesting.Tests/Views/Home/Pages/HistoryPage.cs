using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.Seleno.PageObjects;
using TestStack.Seleno.PageObjects.Locators;
using BankSystem.Models;
using BankSystem.ViewModels;

namespace BankSystem.Tests.Views.Home.Pages
{
    public class HistoryPage : Page<HistoryModel>
    {
        public List<Transaction> transactions()
        {
            var TRs = Find.Element(By.Id("TransactionsTable")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
            List<Transaction> transactionsList = new List<Transaction>();

            foreach(var tr in TRs)
            {
                var TDs = tr.FindElements(By.TagName("td"));
                
                transactionsList.Add(new Transaction{
                    Id = int.Parse(TDs[0].Text),
                    Amount = double.Parse(TDs[1].Text),
                    FromId = int.Parse(TDs[2].Text),
                    ToId = int.Parse(TDs[3].Text)
                });
            }
            return new List<Transaction>();
        }

        public List<Deposit> deposits()
        {
            var TRs = Find.Element(By.Id("DepositsTable")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
            List<Deposit> depositsList = new List<Deposit>();

            foreach (var tr in TRs)
            {
                var TDs = tr.FindElements(By.TagName("td"));

                depositsList.Add(new Deposit
                {
                    Id = int.Parse(TDs[0].Text),
                    Amount = double.Parse(TDs[1].Text),
                    AccountId = int.Parse(TDs[2].Text)
                });
            }
            return depositsList;
        }

        public List<Withdraw> withdraws()
        {
            var TRs = Find.Element(By.Id("WithdrawsTable")).FindElement(By.TagName("tbody")).FindElements(By.TagName("tr"));
            List<Withdraw> withdrawsList = new List<Withdraw>();

            foreach (var tr in TRs)
            {
                var TDs = tr.FindElements(By.TagName("td"));

                withdrawsList.Add(new Withdraw
                {
                    Id = int.Parse(TDs[0].Text),
                    Amount = double.Parse(TDs[1].Text),
                    AccountId = int.Parse(TDs[2].Text)
                });
            }
            return withdrawsList;
        }
    }
}
