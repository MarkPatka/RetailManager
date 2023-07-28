using RetailManager.Library.DataAccess;
using RetailManager.Library.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace RetailManager_.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        public List<ProductModel> Get()
        {
            ProductData data = new ProductData();

            data.GetProducts();

            return data.GetProducts();
        }
    }
}
