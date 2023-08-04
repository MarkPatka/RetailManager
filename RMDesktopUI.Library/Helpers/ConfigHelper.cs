
using System.Configuration;

namespace RMDesktopUI.Library.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        public decimal GetTaxRate()
        {
            string? rateText = ConfigurationManager.AppSettings["taxRate"];

            if (!Decimal.TryParse(rateText, out decimal output))
                throw new InvalidCastException("The Tax Rate is unrecognized");

            return output;
            
        }
    }
}
