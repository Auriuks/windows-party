using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using WindowsPartyBase.Interfaces;
using WindowsPartyGUI.Models;
using AutoMapper;
using Caliburn.Micro;

namespace WindowsPartyGUI.ViewModels
{
    public class MainViewModel : Screen
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IServerInformationService _serverInformationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IMapper _mapper;

        public MainViewModel(IAuthenticationService authenticationService,
            IServerInformationService serverInformationService,
            IEventAggregator eventAggregator,
            IMapper mapper)
        {
            _authenticationService = authenticationService;
            _serverInformationService = serverInformationService;
            _mapper = mapper;
            _eventAggregator = eventAggregator;
             LoadServers();
        }
        public ObservableCollection<ServerModel> Servers { get; set; }


        public void LoadServers()
        {
            new Thread(async () =>
            {
                var servers = await _serverInformationService.GetServers();
                Servers = _mapper.Map<ObservableCollection<ServerModel>>(servers);
                NotifyOfPropertyChange(()=>Servers);
            }).Start();
        }

        public void Logout()
        {
            _authenticationService.Logout();
            _eventAggregator.PublishOnUIThread(new ChangePageMessage(typeof(LoginViewModel)));
        }
    }
}
