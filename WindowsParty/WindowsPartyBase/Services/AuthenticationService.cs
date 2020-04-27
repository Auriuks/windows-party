using System;
using System.Net;
using System.Threading.Tasks;
using WindowsPartyBase.Helpers;
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

        public async Task<LoginResponses> Login(string userName, string password)
        {
            UserData userData;
            try
            {
                var response = await _restClientBase.PostAsync<LoginResponse>("v1/tokens", new {UserName = userName, Password = password}, true);
                
                if(response.StatusCode == HttpStatusCode.Unauthorized)
                    return LoginResponses.BadCredentials;

                if (response.StatusCode != HttpStatusCode.OK || response.Data == null)
                    return LoginResponses.FailedToLogin;

                userData = _mapper.Map<UserData>(response.Data);
                userData.UserName = userName;
            }
            catch (Exception)
            {
                userData = null;
            }

            _userService.SetUserData(userData);
            return LoginResponses.Success;
        }

        public void Logout()
        {
            _userService.SetUserData(null);
        }
    }
}
