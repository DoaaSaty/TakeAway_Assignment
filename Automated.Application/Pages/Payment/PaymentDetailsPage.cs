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
   public class PaymentDetailsPage
    {
        private IWebDriver driver;
        Dictionary<string, AutomatedElement> PageElements;
        AutomatedElement _idealMethodBtn, _cashMethodBtn, _addressText, _townText, _surNameText, _emailAddressText,
            _phoneNumberText, _buyNowBtn;
        public PaymentDetailsPage(IWebDriver driver)
        {
            this.driver = driver;
            AutomatedActions.NavigationActions.InitBrowser(driver);

            PageElements = ElementParser.Initialize_Page_Elements(ApplicationConfigs.ObjectRepository + "Payment\\PaymentDetailsPage.json");
            _idealMethodBtn = PageElements["idealMethodBtn"];
            _cashMethodBtn = PageElements["cashMethodBtn"];
            _addressText = PageElements["addressText"];
            _townText = PageElements["townText"];
            _surNameText = PageElements["surNameText"];
            _emailAddressText = PageElements["emailAddressText"];
            _phoneNumberText = PageElements["phoneNumberText"];
            _buyNowBtn = PageElements["buyNowBtn"];
        }
       
        public void SelectIDEALPaymentMethod()
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_idealMethodBtn, 500);
            AutomatedActions.ClickActions.ClickOnElement(_idealMethodBtn);

        }
        public void SelectCashPaymentMethod()
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_cashMethodBtn, 500);
            AutomatedActions.ClickActions.ClickOnElement(_cashMethodBtn);

        }
        public void EnterAddress(string address)
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_addressText, 500);
            AutomatedActions.TextActions.EnterTextInField(_addressText, address);
        }
        public void EnterTwon(string town)
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_townText, 500);
            AutomatedActions.TextActions.EnterTextInField(_townText, town);
        }
        public void EnterSurname(string surname)
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_surNameText, 500);
            AutomatedActions.TextActions.EnterTextInField(_surNameText, surname);
        }
        public void EnterPhoneNumber(string phoneNumber)
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_phoneNumberText, 500);
            AutomatedActions.TextActions.EnterTextInField(_phoneNumberText, phoneNumber);
        }
        public void EnterEmailAddress(string emailAddress)
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_emailAddressText, 500);
            AutomatedActions.TextActions.EnterTextInField(_emailAddressText, emailAddress);
        }
        public void ClickOnBuyBtn()
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeVisible(_buyNowBtn, 500);
            AutomatedActions.ClickActions.ClickOnElement(_buyNowBtn);
        }
    }
}
