using System.Threading.Tasks;

namespace WindowsPartyBase.Interfaces
{
    public interface IRestClientBase
    {
        Task<T> GetAsync<T>(string uri, bool noLogging = false) where T : class, new();
        Task<T> PostAsync<T>(string uri, object value, bool noLogging = false) where T : class, new();
    }
}