using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using WindowsPartyBase.Interfaces;
using WindowsPartyBase.Services;
using WindowsPartyGUI.Helpers;
using WindowsPartyGUI.ViewModels;
using AutoMapper;
using Caliburn.Micro;
using log4net;

namespace WindowsPartyGUI
{
    public class Bootstrapper: BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();
            ConventionManager.AddElementConvention<PasswordBox>(
                PasswordBoxHelper.BoundPasswordProperty,
                "Password",
                "PasswordChanged");
        }

        protected override void Configure()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddMaps(new[] {
                    "WindowsParty",
                    "WindowsPartyBase"
                });
            });

            log4net.Config.XmlConfigurator.Configure();

            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()

                //WindowsPartyBase initialization
                .PerRequest<IRestClientBase, RestClientBase>()
                .Singleton<IUserService, UserService>()
                .Singleton<IServerInformationService, ServerInformationService>()
                .PerRequest<IAuthenticationService, AuthenticationService>();

            _container.RegisterInstance(
                typeof(IMapper),
                "automapper",
                config.CreateMapper()
            );

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
