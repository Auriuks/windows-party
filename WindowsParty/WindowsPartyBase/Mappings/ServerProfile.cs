using WindowsPartyBase.Models;
using AutoMapper;

namespace WindowsPartyBase.Mappings
{
    public class ServerProfile: Profile
    {
        public ServerProfile()
        {
            CreateMap<ServerResponse, ServerData>();
        }
    }
}
