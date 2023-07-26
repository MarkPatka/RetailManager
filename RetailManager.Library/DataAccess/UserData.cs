using System.Collections.Generic;
using RetailManager.Library.Models;
using RetailManager.Library.Internal.DataAccess;

namespace RetailManager.Library.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();
            var parameters = new { Id = Id };
            
            var output = sql.LoadData<UserModel, dynamic>(
                "[dbo].[spUserLookup]", 
                parameters,
                "RMData");

            return output;
        }


    }
}
