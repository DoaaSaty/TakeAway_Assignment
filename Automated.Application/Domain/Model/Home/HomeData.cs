using Automated.Utilities.Utilities.Parsers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automated.Application.Domain.Model
{
   public class HomeData
    {
        static public string resturantPcName { get; set; } // this is the resturant or post code name 
        


        public void FillData(string sheetToGetDataFrom, string getDataFromRow)
        {

            resturantPcName = ExcelDataParser.GetValueOf(sheetToGetDataFrom, getDataFromRow, "ResturantPcName");
            

        }
        public void Init()
        {
            ExcelDataParser.Init(ConfigurationManager.AppSettings["AutomationDirectory"] + ConfigurationManager.AppSettings["TestDataFile"]);
        }
    }
}
