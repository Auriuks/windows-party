using System.Threading.Tasks;
using WindowsPartyBase.Helpers;

namespace WindowsPartyBase.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponses> Login(string userName, string password);
        void Logout();
    }
}