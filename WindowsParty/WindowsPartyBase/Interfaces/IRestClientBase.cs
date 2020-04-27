using System.Threading.Tasks;
using RestSharp;

namespace WindowsPartyBase.Interfaces
{
    public interface IRestClientBase
    {
        Task<IRestResponse<T>> GetAsync<T>(string uri, bool noLogging = false) where T : class, new();
        Task<IRestResponse<T>> PostAsync<T>(string uri, object value, bool noLogging = false) where T : class, new();
    }
}