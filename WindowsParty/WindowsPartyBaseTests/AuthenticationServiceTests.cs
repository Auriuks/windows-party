using System.Net;
using System.Threading.Tasks;
using WindowsPartyBase.Helpers;
using WindowsPartyBase.Interfaces;
using WindowsPartyBase.Models;
using WindowsPartyBase.Services;
using AutoFixture;
using AutoMapper;
using Moq;
using RestSharp;
using Xunit;

namespace WindowsPartyBaseTests
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IRestClientBase> _restClientBase;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AuthenticationServiceTests()
        {
            _restClientBase = new Mock<IRestClientBase>();
            _userService = new UserService();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(new[]
                {
                    "WindowsPartyBase"
                });
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task Login_UserNameAndPassword_ReturnsSuccess()
        {
            var fixture = new Fixture() { RepeatCount = 10 };
            var userName = fixture.Create<string>();
            var password = fixture.Create<string>();
            var loginResponse = fixture.Create<LoginResponse>();

            var restResponse = new RestResponse<LoginResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Data = loginResponse
            };

            _restClientBase.Setup(met => met.PostAsync<LoginResponse>("v1/tokens", It.IsAny<object>(),true))
                .ReturnsAsync(() => restResponse)
                .Verifiable();

            var serverInformationService = new AuthenticationService(_restClientBase.Object, _userService, _mapper);
            var serverDataList = await serverInformationService.Login(userName, password);

            _restClientBase.Verify();
            Assert.True(serverDataList == LoginResponses.Success);
            Assert.True(_userService.GetToken() == loginResponse.Token);
        }

        [Fact]
        public async Task Login_UserNameAndPassword_ReturnsBadUserName()
        {
            var fixture = new Fixture() { RepeatCount = 10 };
            var userName = fixture.Create<string>();
            var password = fixture.Create<string>();
            var loginResponse = fixture.Create<LoginResponse>();

            var restResponse = new RestResponse<LoginResponse>
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Data = loginResponse
            };

            _restClientBase.Setup(met => met.PostAsync<LoginResponse>("v1/tokens", It.IsAny<object>(), true))
                .ReturnsAsync(() => restResponse)
                .Verifiable();

            var serverInformationService = new AuthenticationService(_restClientBase.Object, _userService, _mapper);
            var serverDataList = await serverInformationService.Login(userName, password);

            _restClientBase.Verify();
            Assert.True(serverDataList == LoginResponses.BadCredentials);
            Assert.True(_userService.GetToken() == null);
        }

        [Fact]
        public async Task Login_UserNameAndPassword_ReturnsFailedToLogin()
        {
            var fixture = new Fixture() { RepeatCount = 10 };
            var userName = fixture.Create<string>();
            var password = fixture.Create<string>();
            var loginResponse = fixture.Create<LoginResponse>();

            var restResponse = new RestResponse<LoginResponse>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Data = loginResponse
            };

            _restClientBase.Setup(met => met.PostAsync<LoginResponse>("v1/tokens", It.IsAny<object>(), true))
                .ReturnsAsync(() => restResponse)
                .Verifiable();

            var serverInformationService = new AuthenticationService(_restClientBase.Object, _userService, _mapper);
            var serverDataList = await serverInformationService.Login(userName, password);

            _restClientBase.Verify();
            Assert.True(serverDataList == LoginResponses.FailedToLogin);
            Assert.True(_userService.GetToken() == null);
        }
    }
}
