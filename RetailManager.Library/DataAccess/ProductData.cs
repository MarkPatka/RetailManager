using RetailManager.Library.Internal.DataAccess;
using RetailManager.Library.Models;
using System.Collections.Generic;

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
        
    }
}
