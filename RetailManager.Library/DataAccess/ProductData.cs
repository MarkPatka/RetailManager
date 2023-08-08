using RetailManager.Library.Internal.DataAccess;
using RetailManager.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace RetailManager.Library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();
            
            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMData");
            
            return output;
        }

        public ProductModel GetProductById(int id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = id }, "RMData").FirstOrDefault();

            return output;
        }
        
    }
}
