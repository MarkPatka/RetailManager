
using Microsoft.AspNet.Identity;
using RetailManager.Library.DataAccess;
using RetailManager.Library.Models;
using RetailManager_.Models;
using System.Web.Http;

namespace RetailManager_.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {
        public void Post(SaleModel sale)
        {
            SaleData data = new SaleData();
            string userId = RequestContext.Principal.Identity.GetUserId();

            data.SaveSale(sale, userId);
        }
    }
}
