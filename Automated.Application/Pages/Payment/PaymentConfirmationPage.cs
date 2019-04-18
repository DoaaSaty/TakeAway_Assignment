using Automated.Utilities.AutomationAbstractions.Components;
using Automated.Utilities.AutomationAbstractions.CoreActions;
using Automated.Utilities.Utilities.Parsers;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automated.Application.Pages.Home
{
   public class PaymentConfirmationPage
    {
        private IWebDriver driver;
        Dictionary<string, AutomatedElement> PageElements;
        AutomatedElement _cancelBtn, _yesBtn, _saveCookiesBtn, _tryAgainBtn;
        public PaymentConfirmationPage(IWebDriver driver)
        {
            this.driver = driver;
            AutomatedActions.NavigationActions.InitBrowser(driver);

            PageElements = ElementParser.Initialize_Page_Elements(ApplicationConfigs.ObjectRepository + "Payment\\PaymentConfirmationPage.json");
            _cancelBtn = PageElements["cancelBtn"];
            _yesBtn = PageElements["yesBtn"];
            _saveCookiesBtn = PageElements["saveCookiesBtn"];
            _tryAgainBtn = PageElements["tryAgainBtn"];
        }
       
        public void ClickOnCancelBtn()
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_cancelBtn, 500);
            AutomatedActions.ClickActions.ClickOnElement(_cancelBtn);

        }
        public void ClickOnYesBtn()
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_yesBtn, 500);
            AutomatedActions.ClickActions.ClickOnElement(_yesBtn);

        }
        public void ClickOnSaveCookiesBtn()
        {
            AutomatedBrowser.WebDriverInstance.Manage().Cookies.DeleteAllCookies();
            IWebElement saveCookiesBtn = AutomatedBrowser.WebDriverInstance.FindElement(By.CssSelector("a[class='mlf-js-cookie-accept mlf-button mlf-button-action']"));
            TimeSpan waitForSeconds = new TimeSpan(400);
            WebDriverWait wait = new WebDriverWait(AutomatedBrowser.WebDriverInstance, waitForSeconds);
            wait.Until(ExpectedConditions.ElementToBeClickable(saveCookiesBtn));
            saveCookiesBtn.Click();
            //AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_saveCookiesBtn, 500);
            //AutomatedActions.ClickActions.ClickOnElement(_saveCookiesBtn);

        }
        public void ClickOnTryAgainBtn()
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_tryAgainBtn, 500);
            AutomatedActions.ClickActions.ClickOnElement(_tryAgainBtn);

        }

    }
}
