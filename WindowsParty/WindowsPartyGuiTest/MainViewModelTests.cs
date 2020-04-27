using System.Linq;
using System.Threading;
using WindowsPartyBase.Interfaces;
using WindowsPartyBase.Models;
using WindowsPartyGUI.ViewModels;
using AutoFixture;
using AutoMapper;
using Caliburn.Micro;
using Moq;
using Xunit;

namespace WindowsPartyGuiTest
{
    public class MainViewModelTests
    {
        readonly Mock<IAuthenticationService> _authenticationService;
        readonly Mock<IServerInformationService> _serverInformationService;
        readonly Mock<IEventAggregator> _eventAggregator;
        readonly IMapper _mapper;

        public MainViewModelTests()
        {
            _authenticationService = new Mock<IAuthenticationService>();
            _serverInformationService = new Mock<IServerInformationService>();
            _eventAggregator = new Mock<IEventAggregator>();


            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(new[]
                {
                    "WindowsParty",
                    "WindowsPartyBase"
                });
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void LoadServers_ServerListResponse_ServerListLoaded()
        {
            var fixture = new Fixture() {RepeatCount = 10};
            var serverList = fixture.CreateMany<ServerData>().ToList();

            _serverInformationService.Setup(met => met.GetServers()).ReturnsAsync(() => serverList);
            var mainViewModel = new MainViewModel(_authenticationService.Object, _serverInformationService.Object,
                _eventAggregator.Object, _mapper);

            Thread.Sleep(500); //because LogIn method runs fire and forget thread
            Assert.True(mainViewModel.Servers.Count == serverList.Count);
            Assert.True(mainViewModel.Servers.First().Name == serverList.First().Name);
        }

        [Fact]
        public void Logout_Void_UserDataResets()
        {
            _authenticationService.Setup(met => met.Logout()).Verifiable();
            var mainViewModel = new MainViewModel(_authenticationService.Object, _serverInformationService.Object,
                _eventAggregator.Object, _mapper);
            mainViewModel.Logout();
            _authenticationService.Verify();
        }
    }
}
