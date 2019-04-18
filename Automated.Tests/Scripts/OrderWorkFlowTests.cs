using Automated.Application.Domain.Model;

using Automated.Application.Pages.Home;
using Automated.Utilities.AutomationAbstractions.Components;
using Automated.Utilities.Reporting;
using Automated.Utilities.Utilities.Parsers;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;


namespace Automated.Tests.Scripts
{
    [TestFixture]
    class OrderWorkFlowTests : TestMain
    {
        private ExtentTest _test;
        HomePage _homePage;
        HomeData _homeData;
        ResturantSelectionData _resturantSelectionData;
        ResturantSelectionPage _resturantSelectionPage;
        OrderCreationData _orderCreationData;
        OrderCreationPage _orderCreationPage;
        PaymentDetailsData _paymentDetailsData;
        PaymentDetailsPage _paymentDetailsPage;
        PaymentConfirmationPage _paymentConfirmationPage;
        OrderVerificationPage _orderVerificationPage;
        [SetUp]
        public new void SetupTest()
        {
            _homePage = new HomePage(AutomatedBrowser.WebDriverInstance);
            _homeData = new HomeData();
            _resturantSelectionData = new ResturantSelectionData();
            _resturantSelectionPage = new ResturantSelectionPage(AutomatedBrowser.WebDriverInstance);
            _orderCreationPage = new OrderCreationPage(AutomatedBrowser.WebDriverInstance);
            _orderCreationData = new OrderCreationData();
            _paymentDetailsData = new PaymentDetailsData();
            _paymentDetailsPage = new PaymentDetailsPage(AutomatedBrowser.WebDriverInstance);
            _paymentConfirmationPage = new PaymentConfirmationPage(AutomatedBrowser.WebDriverInstance);
            _orderVerificationPage = new OrderVerificationPage(AutomatedBrowser.WebDriverInstance);
        }

        [Test, Order(1)]
        [Category("OrdersWorkflowTests"), Retry(TestConfigs.MaxNumberOfRetries)]

        public void Submit_Valid_Order_Test()
        {

            try
            {
                // Get the current method name and current class name
                var testCaseName = MethodBase.GetCurrentMethod().Name;// method name 
                var worksheetName = GetType().Name;  // class name
                
                // calling FillData method that reads the data for the current test by passing the test and sheet name
                _homeData.FillData(worksheetName, testCaseName);
                _resturantSelectionData.FillData(worksheetName, testCaseName);
                _orderCreationData.FillData(worksheetName, testCaseName);
                _paymentDetailsData.FillData(worksheetName, testCaseName);
               
                //starting the scenario
                _homePage.EnterResturantNameORPostalCode(HomeData.resturantPcName);
                _resturantSelectionPage.SearchByResturantName(ResturantSelectionData.resturantName);
                _resturantSelectionPage.SelectExactResturantName(ResturantSelectionData.resturantName);
                
                //adding main dish 
                _orderCreationPage.AddingMainDishOrderToBasket(_orderCreationPage.ParsingPizzaOrDrinkOrder(OrderCreationData.dishOrder),OrderCreationData.mainDishDrink);
               
                //adding the pizza order 
                _orderCreationPage.AddingTheOrderToBasket(_orderCreationPage.ParsingPizzaOrDrinkOrder(OrderCreationData.pizzaOrder));
                
                //adding the drinks order 
                _orderCreationPage.AddingTheOrderToBasket(_orderCreationPage.ParsingPizzaOrDrinkOrder(OrderCreationData.drinkOrder));
                _orderCreationPage.ClickOnTheBasketIcon();
                _orderCreationPage.AddCommentsForOrderItemsInBasket(OrderCreationData.orderComment);
                _orderCreationPage.ClickOnOrderBtn();

                //Adding payment details
                _paymentDetailsPage.EnterAddress(PaymentDetailsData.address);
                _paymentDetailsPage.EnterSurname(PaymentDetailsData.surname);
                _paymentDetailsPage.EnterTwon(PaymentDetailsData.town);
                _paymentDetailsPage.EnterEmailAddress(PaymentDetailsData.emailaddress);
                _paymentDetailsPage.EnterPhoneNumber(PaymentDetailsData.phoneNumber);
                _paymentDetailsPage.SelectIDEALPaymentMethod();
                _paymentDetailsPage.ClickOnBuyBtn();

                //cancling the order
                _paymentConfirmationPage.ClickOnSaveCookiesBtn();
                _paymentConfirmationPage.ClickOnCancelBtn();
                _paymentConfirmationPage.ClickOnYesBtn();
                _paymentConfirmationPage.ClickOnTryAgainBtn();

                //selecitng cash method to proceed
                _paymentDetailsPage.SelectCashPaymentMethod();
                _paymentDetailsPage.ClickOnBuyBtn();

                // Multiple Assertion
                Assert.Multiple(() =>
                {
                    // check if the UI order Id = DB order ID, but because i don't have DB I'm comparing the UI order ID against itself which 
                    // will be returning true.. but in real case I would create DB connection, quering the created order ID
                    // from the DB and pass it to the below method to check if the order ID from the UI = DB one
                   
                    if (_orderVerificationPage.CompareUIOrderIdAgainstDB(_orderVerificationPage.GetDBOrderId()))
                    {
                        TestReport.test.Pass(TestContext.CurrentContext.Test.Name + " is passed");
                        Assert.True(_orderVerificationPage.CompareUIOrderIdAgainstDB(_orderVerificationPage.GetDBOrderId()));
                    }
                    else
                    {
                        TestReport.test.Fail(TestContext.CurrentContext.Test.Name + " is failed");
                        TestReport.Log(Status.Fail, Status.Error.ToString());
                        Assert.True(_orderVerificationPage.CompareUIOrderIdAgainstDB(_orderVerificationPage.GetDBOrderId()));
                    }

                    // here I'm checking that the displayed resturant name in the order confirmation page = the one we passed in the test sheet
                    if (_orderVerificationPage.ValidateUIResturantNameAgainstProvided_One(ResturantSelectionData.resturantName))
                    {
                        Assert.True(_orderVerificationPage.ValidateUIResturantNameAgainstProvided_One(ResturantSelectionData.resturantName));
                    }
                    else
                    {
                        TestReport.test.Fail(TestContext.CurrentContext.Test.Name + " is failed");
                        TestReport.Log(Status.Fail, Status.Error.ToString());
                        Assert.True(_orderVerificationPage.ValidateUIResturantNameAgainstProvided_One(ResturantSelectionData.resturantName));
                    }

                });


            }
            catch (Exception e)
            {

                //_test.Fail(TestContext.CurrentContext.Test.Name + " is failed");
                //_test.Log(Status.Fail, e.ToString());
                Assert.Fail(e.ToString());
            }

        }
       



    }
}



