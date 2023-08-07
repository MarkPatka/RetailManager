using System;
using System.Configuration;

namespace RetailManager.Library
{
    public class ConfigHelper
    {
        public static decimal GetTaxRate()
        {
            string rateText = ConfigurationManager.AppSettings["taxRate"];

            if (!Decimal.TryParse(rateText, out decimal output))
                throw new InvalidCastException("The Tax Rate is unrecognized");

            return output;
        }
    }
}
