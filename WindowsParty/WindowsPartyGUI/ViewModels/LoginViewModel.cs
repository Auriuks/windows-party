using System.Threading;
using WindowsPartyBase.Helpers;
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
        private string _errorLabel;

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

        public string ErrorLabel
        {
            get => _errorLabel;
            set
            {
                if (value == _errorLabel) return;
                _errorLabel = value;
                NotifyOfPropertyChange(() => ErrorLabel);
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
                var loginResponse = await _authenticationService.Login(UserName, Password);
                if (_userService.IsLoggedIn() && loginResponse == LoginResponses.Success)
                    OnSuccessfulLogin();
                else
                    OnFailedLogin(loginResponse);
            }).Start();

        }

        public void OnSuccessfulLogin()
        {
            _eventAggregator.PublishOnUIThread(new ChangePageMessage(typeof(MainViewModel)));
        }

        public void OnFailedLogin(LoginResponses loginResponse)
        {
            ErrorLabel = loginResponse == LoginResponses.BadCredentials ? "Wrong user name or password" : "Failed to login";
            ErrorLabelIsVisible = true;
        }

    }
}
