using System.Threading.Tasks;

namespace WindowsPartyBase.Interfaces
{
    public interface IAuthenticationService
    {
        Task Login(string userName, string password);
        void Logout();
    }
}