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
   public class OrderCreationPage
    {
        private IWebDriver driver;
        Dictionary<string, AutomatedElement> PageElements;
        AutomatedElement _mealsList, _mealNameList, _addOneBtn, _drinkDdl, _addMainDishBtn, _basketBtn, _commentBtn,
            _commentText, _OrderBtn;
        IReadOnlyCollection<IWebElement> allMeals, mealNames;
        
        public OrderCreationPage(IWebDriver driver)
        {
            this.driver = driver;
            AutomatedActions.NavigationActions.InitBrowser(driver);

            PageElements = ElementParser.Initialize_Page_Elements(ApplicationConfigs.ObjectRepository + "Orders\\OrderCreationPage.json");
            
            _mealsList = PageElements["mealsList"];
            _mealNameList = PageElements["mealNameList"];
            _addOneBtn = PageElements["addOneBtn"];
            _drinkDdl = PageElements["drinkDdl"];
            _addMainDishBtn = PageElements["addMainDishBtn"];
            _basketBtn = PageElements["basketBtn"];
            _commentBtn = PageElements["commentBtn"];
            _commentText = PageElements["commentText"];
            _OrderBtn = PageElements["OrderBtn"];
        }
       
        
        /// <summary>
       /// Looping over all the main dishes, pizzas and drinks comparing against the provided order from the excel sheet
       /// and adding the desired order
       /// </summary>
       /// <param name="dishes"></param>
        public void AddingTheOrderToBasket(Dictionary<string,int> orderList)
        {
            Thread.Sleep(400);
            allMeals = _mealsList.GetActualWebElements();
            mealNames = _mealNameList.GetActualWebElements();
            
            foreach (var orderName in orderList) //looping over either Drinks or Pizza dictonary 
            {
                for (int j = 0; j <allMeals.Count; j++)
                {
                    //getting the order name and checking if it's the current selected on the UI
                    // here "Duck Breast" is special case as it comes with customized options
                    if (allMeals.ElementAt(j).Text.ToLower().Equals(orderName.Key.ToLower()) )
                    {
                        //looping over the desired quantities to add the item accordingly
                        for (int k = 0; k < orderName.Value; k++)
                            mealNames.ElementAt(j).Click();
                        
                        break;
                    }    
                }
            }
           
        }

        public void AddingMainDishOrderToBasket(Dictionary<string, int> orderList,string drink)
        {
            Thread.Sleep(400);
            TimeSpan waitForSeconds = new TimeSpan(400);
            WebDriverWait wait = new WebDriverWait(AutomatedBrowser.WebDriverInstance, waitForSeconds);
            
            allMeals = _mealsList.GetActualWebElements();
            mealNames = _mealNameList.GetActualWebElements();

            foreach (var orderName in orderList) 
            {
                for (int j = 0; j < allMeals.Count; j++)
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(mealNames.ElementAt(j)));
                    //getting the order name and checking if it's the current selected on the UI
                    // here "Duck Breast" is special case as it comes with customized options
                    if (allMeals.ElementAt(j).Text.ToLower().Equals(orderName.Key.ToLower()) && orderName.Key.ToLower() == "Duck Breast".ToLower())
                    {
                        mealNames.ElementAt(j).Click();
                        AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_addOneBtn, 400);
                        // need to add the tomato by checking the checkbox
                        for (int k = 1; k < orderName.Value; k++) // as the initial value of the meals in the UI is 1
                            AutomatedActions.ClickActions.ClickOnElement(_addOneBtn);
                        break;
                    }
                }
                //select the drink
                AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_drinkDdl, 400);
                AutomatedActions.SelectActions.SelectOptionByText(_drinkDdl, drink);

                //adding the dish to the basket
                AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_addMainDishBtn, 400);
                AutomatedActions.ClickActions.ClickOnElement(_addMainDishBtn);
            }
        }
        public Dictionary<string,int> ParsingPizzaOrDrinkOrder(string order)
        {
            Thread.Sleep(400);
          
            string[] orderRows = order.Split('\n');
            string[] orderQuantity = new string[] { };
            Dictionary<string, int> orderWithQuantity = new Dictionary<string, int>();

            for (int i=0;i< orderRows.Length;i++)
            {
                if (orderRows[i] != "")
                {
                    orderQuantity = orderRows[i].Split(',');
                    orderWithQuantity.Add(orderQuantity[0].ToString(), int.Parse(orderQuantity[1]));
                }
            }
            return orderWithQuantity;
        }
        public void ClickOnTheBasketIcon()
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_basketBtn, 400);
            AutomatedActions.ClickActions.ClickOnElement(_basketBtn);
        }/// <summary>
        /// basically here I'm looping over the items in the Basket, clicking on their comment btn and 
        /// entering the "No sugar" comment for each item
        /// </summary>
        /// <param name="comment"></param>
        public void AddCommentsForOrderItemsInBasket(string comment)
        {
            TimeSpan waitForSeconds = new TimeSpan(400);
            WebDriverWait wait = new WebDriverWait(AutomatedBrowser.WebDriverInstance, waitForSeconds);
            int btnsCount  = AutomatedBrowser.WebDriverInstance.FindElements(By.XPath("//div[@class='cart-meal-edit-buttons']/div[3]")).Count;
            for (int i=0;i< btnsCount-1;i++)
            {
                IReadOnlyCollection<IWebElement> afterEditBtns = AutomatedBrowser.WebDriverInstance.FindElements(By.XPath("//div[@class='cart-meal-edit-buttons']/div[3]"));
                IReadOnlyCollection<IWebElement> itemscommentTexts = AutomatedBrowser.WebDriverInstance.FindElements(By.ClassName("cart-meal-textarea"));
                wait.Until(ExpectedConditions.ElementToBeClickable(afterEditBtns.ElementAt(i)));
                IWebElement element = afterEditBtns.ElementAt(i);
                afterEditBtns.ElementAt(i).Click();
                itemscommentTexts.ElementAt(i).SendKeys(comment);
                afterEditBtns.ElementAt(i).Click(); // to close the comment text
                    

                }
        }

        public void ClickOnOrderBtn()
        {
            AutomatedActions.WaitActions.WaitForWebElementToBeClickable(_OrderBtn, 400);
            AutomatedActions.ClickActions.ClickOnElement(_OrderBtn);
        }
        
        

    }
}
