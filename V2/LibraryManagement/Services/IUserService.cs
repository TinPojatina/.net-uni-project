using System.Security.Claims;

namespace LibraryManagement.Services
{
    public interface IUserService
    {
        string GetUserId();
        string GetUserName();
        bool IsAuthenticated();
    }
}