using RMDesktopUI.Library.Models;

namespace RMDesktopUI.Library.Api
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserinfo(string token);
        HttpClient Client { get; }
    }
}