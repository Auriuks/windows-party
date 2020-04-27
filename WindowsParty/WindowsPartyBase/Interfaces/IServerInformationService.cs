using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsPartyBase.Models;

namespace WindowsPartyBase.Interfaces
{
    public interface IServerInformationService
    {
        Task<List<ServerData>> GetServers();
    }
}