using Automated.Utilities.Utilities.Parsers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automated.Application.Domain.Model
{
   public class OrderCreationData
    {
        static public string dishOrder { get; set; }
        static public string pizzaOrder { get; set; }
        static public string drinkOrder{ get; set; }
        static public string mainDishDrink { get; set; }
        static public string orderComment { get; set; }

        public void FillData(string sheetToGetDataFrom, string getDataFromRow)
        {

            dishOrder = ExcelDataParser.GetValueOf(sheetToGetDataFrom, getDataFromRow, "Dishes");
            pizzaOrder = ExcelDataParser.GetValueOf(sheetToGetDataFrom, getDataFromRow, "Pizza");
            drinkOrder = ExcelDataParser.GetValueOf(sheetToGetDataFrom, getDataFromRow, "Drinks");
            mainDishDrink = ExcelDataParser.GetValueOf(sheetToGetDataFrom, getDataFromRow, "MainDishDrink");
            orderComment = ExcelDataParser.GetValueOf(sheetToGetDataFrom, getDataFromRow, "OrderComment");
        }
        public void Init()
        {
            ExcelDataParser.Init(ConfigurationManager.AppSettings["AutomationDirectory"] + ConfigurationManager.AppSettings["TestDataFile"]);
        }
    }
}
