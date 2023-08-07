
using System.Configuration;

namespace RMDesktopUI.Library.Helpers
{
    /// <summary>
    /// TODO: Move this from config to the API
    /// </summary>
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
