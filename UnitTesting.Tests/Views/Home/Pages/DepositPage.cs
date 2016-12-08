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
    public class DepositPage : Page<Deposit>
    {
        public T Submit<T>(Deposit depositModel)
           where T : UiComponent, new()
        {
            Input.Model(depositModel);
            return Navigate.To<T>(By.CssSelector("input[value='Create'][type='submit']"));
        }
    }
}
