using System.Threading;
using WindowsPartyBase.Helpers;
using WindowsPartyBase.Interfaces;
using WindowsPartyGUI.ViewModels;
using Caliburn.Micro;
using Moq;
using Xunit;

namespace WindowsPartyGuiTest
{
    public class LoginTests
    {
        private readonly Mock<IAuthenticationService> _authenticationService;
        private readonly Mock<IUserService> _userService;
        private readonly Mock<IEventAggregator> _eventAggregator;

        public LoginTests()
        {
            _authenticationService = new Mock<IAuthenticationService>();
            _userService = new Mock<IUserService>();
            _eventAggregator = new Mock<IEventAggregator>();
        }

        [Fact]
        public void Login_FailedToLoginResponse_ErrorLabelIsShowed()
        {
            _authenticationService
                .Setup(met => met.Login(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => LoginResponses.FailedToLogin)
                .Verifiable();

            var loginViewModel = new LoginViewModel(_authenticationService.Object, _userService.Object,_eventAggregator.Object);
            loginViewModel.LogIn();

            Thread.Sleep(100); //because LogIn method runs fire and forget thread

            Assert.True(loginViewModel.ErrorLabelIsVisible);
            Assert.True(loginViewModel.ErrorLabel == "Failed to login");
        }

        [Fact]
        public void Login_BadCredentialsResponse_ErrorLabelIsShowed()
        {
            _authenticationService
                .Setup(met => met.Login(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => LoginResponses.BadCredentials)
                .Verifiable();

            var loginViewModel = new LoginViewModel(_authenticationService.Object, _userService.Object, _eventAggregator.Object);
            loginViewModel.LogIn();

            Thread.Sleep(100); //because LogIn method runs fire and forget thread
            Assert.True(loginViewModel.ErrorLabelIsVisible);
            Assert.True(loginViewModel.ErrorLabel == "Wrong user name or password");
        }
    }
}
