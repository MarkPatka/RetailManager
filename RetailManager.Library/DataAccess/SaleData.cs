using RetailManager.Library.Internal.DataAccess;
using RetailManager.Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace RetailManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            ProductData products = new ProductData();
            decimal taxRate = ConfigHelper.GetTaxRate() / 100m;

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                
                var productInfo = products.GetProductById(detail.ProductId);

                if (productInfo == null)
                {
                    throw new System.ArgumentNullException($"The product Id of {detail.ProductId } could not be dound in the database.");
                }

                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);
                
                if (productInfo.IsTaxable)
                {
                    detail.Tax = detail.PurchasePrice * taxRate;
                }

                details.Add(detail);
            }

            SaleDBModel sale = new SaleDBModel()
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId,
            };

            sale.Total = sale.SubTotal + sale.Tax;

            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData<SaleDBModel>("dbo.spSale_Insert", sale, "RMData");

            sale.Id = sql.LoadData<int, dynamic>(
                "spSale_Lookup", 
                new { sale.CashierId, sale.SaleDate }, 
                "RMData")
                .FirstOrDefault();


            foreach (var item in details)
            {
                item.SaleId = sale.Id;
                sql.SaveData("dbo.spSaleDetail_Insert", item, "RMData");

            }


        }
    }
}
