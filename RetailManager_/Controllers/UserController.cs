using RetailManager.Library.DataAccess;
using System.Web.Http;
using RetailManager.Library.Models;
using Microsoft.AspNet.Identity;
using System.Linq;

namespace RetailManager_.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        [HttpGet]
        public UserModel GetById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();

            return data.GetUserById(userId).First();
        }
    }
}
