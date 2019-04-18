using Automated.Utilities.AutomationAbstractions.Components;
using Automated.Utilities.AutomationAbstractions.CoreActions;
using Automated.Utilities.Utilities.Parsers;
using AventStack.ExtentReports.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Automated.Application.Pages.Home
{
   public class ResturantSelectionPage
    {
        private IWebDriver driver;
        Dictionary<string, AutomatedElement> PageElements;
        AutomatedElement _resturantSearchBox, _resturantList;
        public ResturantSelectionPage(IWebDriver driver)
        {
            this.driver = driver;
            AutomatedActions.NavigationActions.InitBrowser(driver);

            PageElements = ElementParser.Initialize_Page_Elements(ApplicationConfigs.ObjectRepository + "Orders\\ResturantSelectionPage.json");
            _resturantSearchBox = PageElements["resturantSearchBox"];
            _resturantList = PageElements["resturantList"];
        }
       
        public void SearchByResturantName(string resturantName)
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_resturantSearchBox, 500);
            AutomatedActions.TextActions.EnterTextInField(_resturantSearchBox, resturantName);
            Thread.Sleep(3000); // just to give it time to load the relevant resturants names
        }
        /// <summary>
        ///In case that multiple resturants names were returned as a result of your search, here I'm looping on them all 
        /// to select the exact one
        /// </summary>
       public void SelectExactResturantName(string resturantName)
        {
            TimeSpan waitForSeconds = new TimeSpan(400);
            WebDriverWait wait = new WebDriverWait(AutomatedBrowser.WebDriverInstance, waitForSeconds);

            IReadOnlyCollection<IWebElement> resturantList = AutomatedBrowser.WebDriverInstance.FindElements(By.XPath("//*[@class='restaurant']/div[2]/h2/a"));
            string resturantText = string.Empty;
            IWebElement resturantElement = null; 

            for (int i=1;i<resturantList.Count;i++)//ignoring the first element
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(resturantList.ElementAt(i)));
                resturantElement = resturantList.ElementAt(i);
                resturantText = resturantElement.Text;
                resturantText = resturantText.Trim();
                if (resturantText.Equals(resturantName))
                    resturantElement.Click();

            }
        }
       
    }
}
