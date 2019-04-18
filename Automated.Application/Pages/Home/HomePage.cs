using Automated.Utilities.AutomationAbstractions.Components;
using Automated.Utilities.AutomationAbstractions.CoreActions;
using Automated.Utilities.Utilities.Parsers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automated.Application.Pages.Home
{
   public class HomePage
    {
        private IWebDriver driver;
        Dictionary<string, AutomatedElement> PageElements;
        AutomatedElement _searchBox, _searchbBtn;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            AutomatedActions.NavigationActions.InitBrowser(driver);

            PageElements = ElementParser.Initialize_Page_Elements(ApplicationConfigs.ObjectRepository + "Home\\HomePage.json");
            _searchBox = PageElements["searchBox"];
            _searchbBtn = PageElements["searchbBtn"];
        }
       
        public void EnterResturantNameORPostalCode(string name)
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_searchBox, 500);
            AutomatedActions.TextActions.EnterTextInField(_searchBox, name);
            IWebElement searchbox = AutomatedBrowser.WebDriverInstance.FindElement(By.Id("imysearchstring"));
            Actions action = new Actions(AutomatedBrowser.WebDriverInstance);
            action.MoveToElement(searchbox).SendKeys(Keys.Enter).Build().Perform();

        }
        public void ClickOnSearchBtn()
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_searchbBtn, 500);
            AutomatedActions.ClickActions.ClickOnElement(_searchbBtn);
        }
       
    }
}
