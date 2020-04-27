using System;
using System.Threading.Tasks;
using WindowsPartyBase.Interfaces;
using WindowsPartyBase.Models;
using AutoMapper;

namespace WindowsPartyBase.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRestClientBase _restClientBase;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AuthenticationService(IRestClientBase restClientBase, IUserService userService, IMapper mapper)
        {
            _restClientBase = restClientBase;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task Login(string userName, string password)
        {
            UserData userData = null;
            try
            {
                var response = await _restClientBase.PostAsync<LoginResponse>("v1/tokens", new {UserName = userName, Password = password}, true);
                userData = _mapper.Map<UserData>(response);
                userData.UserName = userName;
            }
            catch (Exception)
            {
                userData = null;
            }
            finally
            {
                _userService.SetUserData(userData);
            }
           
        }

        public void Logout()
        {
            _userService.SetUserData(null);
        }
    }
}
