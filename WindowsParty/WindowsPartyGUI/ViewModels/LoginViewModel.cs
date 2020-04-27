using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WindowsPartyBase.Interfaces;
using WindowsPartyGUI.Models;
using Caliburn.Micro;

namespace WindowsPartyGUI.ViewModels
{
    public class LoginViewModel: Screen
    {
        private string _userName;
        private string _password;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IEventAggregator _eventAggregator;
        private bool _errorLabelIsVisible;

        public string UserName
        {
            get => _userName;
            set
            {
                if (_userName == value) return;
                _userName = value;
                NotifyOfPropertyChange(()=> UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                if (_password == value) return;
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool ErrorLabelIsVisible
        {
            get => _errorLabelIsVisible;
            set
            {
                if (value == _errorLabelIsVisible) return;
                _errorLabelIsVisible = value;
                NotifyOfPropertyChange(() => ErrorLabelIsVisible);
            }
        }

        public LoginViewModel(IAuthenticationService authenticationService, IUserService userService, IEventAggregator eventAggregator)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _eventAggregator = eventAggregator;
        }

        public bool CanLogIn => !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);

        public void LogIn()
        {
            new Thread(async () =>
            {
                await _authenticationService.Login(UserName, Password);
                if (_userService.IsLoggedIn())
                    OnSuccessfulLogin();
                else
                    OnFailedLogin();
            }).Start();

        }

        public void OnSuccessfulLogin()
        {
            _eventAggregator.PublishOnUIThread(new ChangePageMessage(typeof(MainViewModel)));
        }

        public void OnFailedLogin()
        {
            ErrorLabelIsVisible = true;
        }

    }
}
