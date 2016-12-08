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
    public class DeleteAccountPage : Page<Account>
    {
        public T Submit<T>(Account accountModel)
           where T : UiComponent, new()
        {
            Input.Model(accountModel);
            return Navigate.To<T>(By.CssSelector("input[value='Delete'][type='submit']"));
        }
    }
}
