using WindowsPartyBase.Interfaces;
using WindowsPartyBase.Models;

namespace WindowsPartyBase.Services
{
    public class UserService : IUserService
    {
        private UserData _userData;
        public void SetUserData(UserData userData)
        {
            _userData = userData;
        }

        public bool IsLoggedIn()
        {
            return !string.IsNullOrWhiteSpace(_userData?.Token);
        }

        public string GetToken()
        {
            return _userData?.Token;
        }
    }
}
