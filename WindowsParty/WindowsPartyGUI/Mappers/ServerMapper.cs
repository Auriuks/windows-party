using WindowsPartyBase.Models;
using WindowsPartyGUI.Models;
using AutoMapper;

namespace WindowsPartyGUI.Mappers
{
    public class ServerMapper: Profile
    {
        public ServerMapper()
        {
            CreateMap<ServerData, ServerModel>();
        }
    }
}
