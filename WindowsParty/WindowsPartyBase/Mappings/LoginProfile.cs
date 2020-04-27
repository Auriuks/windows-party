using WindowsPartyBase.Models;
using AutoMapper;

namespace WindowsPartyBase.Mappings
{
    public class LoginProfile: Profile
    {
        public LoginProfile()
        {
            CreateMap<LoginResponse, UserData>();
        }
    }
}
