using Automated.Utilities.Utilities.Parsers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automated.Application.Domain.Model
{
   public class PaymentDetailsData
    {
        static public string address { get; set; }
        static public string town { get; set; }
        static public string surname{ get; set; }
        static public string emailaddress { get; set; }
        static public string phoneNumber { get; set; }

        public void FillData(string sheetToGetDataFrom, string getDataFromRow)
        {

            address = ExcelDataParser.GetValueOf(sheetToGetDataFrom, getDataFromRow, "Address");
            town = ExcelDataParser.GetValueOf(sheetToGetDataFrom, getDataFromRow, "Town");
            surname = ExcelDataParser.GetValueOf(sheetToGetDataFrom, getDataFromRow, "Surname");
            emailaddress = ExcelDataParser.GetValueOf(sheetToGetDataFrom, getDataFromRow, "EmailAddress");
            phoneNumber = ExcelDataParser.GetValueOf(sheetToGetDataFrom, getDataFromRow, "PhoneNumber");

        }
        public void Init()
        {
            ExcelDataParser.Init(ConfigurationManager.AppSettings["AutomationDirectory"] + ConfigurationManager.AppSettings["TestDataFile"]);
        }
    }
}
