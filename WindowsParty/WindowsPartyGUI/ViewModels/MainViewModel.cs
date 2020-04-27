using System.Collections.ObjectModel;
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
        private readonly IMapper _mapper;

        public MainViewModel(IAuthenticationService authenticationService, 
            IServerInformationService serverInformationService, 
            IMapper mapper)
        {
            _authenticationService = authenticationService;
            _serverInformationService = serverInformationService;
            _mapper = mapper;
            LoadServers();
        }
        public ObservableCollection<ServerModel> Servers { get; set; }


        public void LoadServers()
        {
            Task.Run(async () =>
            {
                var servers = await _serverInformationService.GetServers();
                Servers = _mapper.Map<ObservableCollection<ServerModel>>(servers);
                NotifyOfPropertyChange(()=>Servers);
            });
        }

        public void Logout()
        {
            _authenticationService.Logout();
        }
    }
}
