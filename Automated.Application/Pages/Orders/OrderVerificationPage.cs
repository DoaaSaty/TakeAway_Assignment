using Automated.Utilities.AutomationAbstractions.Components;
using Automated.Utilities.AutomationAbstractions.CoreActions;
using Automated.Utilities.Utilities.Parsers;
using AventStack.ExtentReports.Model;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TAMM.Automation.Application.Utilities;

namespace Automated.Application.Pages.Home
{
   public class OrderVerificationPage
    {
        private IWebDriver driver;
        Dictionary<string, AutomatedElement> PageElements;
        AutomatedElement _orderId, _resturantName;



        public OrderVerificationPage(IWebDriver driver)
        {
            this.driver = driver;
            AutomatedActions.NavigationActions.InitBrowser(driver);

            PageElements = ElementParser.Initialize_Page_Elements(ApplicationConfigs.ObjectRepository + "Orders\\OrderVerificationPage.json");

            _orderId = PageElements["orderId"];
            _resturantName = PageElements["resturantName"];
        }
       
       public bool CompareUIOrderIdAgainstDB(string orderIdDB)
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_orderId,500);
            string orderIdUi= AutomatedActions.ElementActions.GetTextOfElement(_orderId);
            orderIdUi = orderIdUi.Trim();
            if (orderIdDB.Equals(orderIdUi))
                return true;
            else return false;
        }

        /// <summary>
        ///  Here I'm returning the order Id from the UI just because I don't have DB to validate against
        ///  But in real case we should compare againt DB by creating entity framework connection to DB
        ///  write down a query that returns the created Order Id from the DB
        ///  And that DB order Id I should be sending to the method above to compare both
        ///  orderID from the UI and DB
        /// </summary>
        /// <returns></returns>
        public string GetDBOrderId()
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_orderId, 500);
            return  AutomatedActions.ElementActions.GetTextOfElement(_orderId).Trim();
        }
        public bool ValidateUIResturantNameAgainstProvided_One(string resturnat)
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_resturantName, 500);
            string resturnatName=  AutomatedActions.ElementActions.GetTextOfElement(_resturantName);
            resturnatName = resturnatName.Trim();
            if (resturnatName.Equals(resturnat))
                return true;
            else return false;
        }

    }
}
