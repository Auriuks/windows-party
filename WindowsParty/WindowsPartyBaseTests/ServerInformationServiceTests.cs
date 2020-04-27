using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
    public class ServerInformationServiceTests
    {
        private readonly Mock<IRestClientBase> _restClientBase;
        private readonly IMapper _mapper;
        public ServerInformationServiceTests()
        {
            _restClientBase = new Mock<IRestClientBase>();
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
        public async Task GetServers_void_ReturnsServerList()
        {
            var fixture = new Fixture() { RepeatCount = 10 };
            var serverResponse = fixture.CreateMany<ServerResponse>().ToList();
            var restResponse = new RestResponse<List<ServerResponse>>
            {
                Data = serverResponse, StatusCode = HttpStatusCode.OK
            };

            _restClientBase.Setup(met => met.GetAsync<List<ServerResponse>>("v1/servers",false)).ReturnsAsync(() => restResponse);
            var serverInformationService = new ServerInformationService(_restClientBase.Object, _mapper);
            var serverDataList = await serverInformationService.GetServers();

            Assert.True(serverResponse.Count == serverDataList.Count);
            Assert.True(serverResponse.First().Name == serverDataList.First().Name);
        }
    }
}
