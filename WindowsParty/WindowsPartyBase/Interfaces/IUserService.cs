using WindowsPartyBase.Models;

namespace WindowsPartyBase.Interfaces
{
    public interface IUserService
    {
        void SetUserData(UserData userData);
        bool IsLoggedIn();
        string GetToken();
    }
}