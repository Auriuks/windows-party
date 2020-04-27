using WindowsPartyGUI.Models;
using Caliburn.Micro;

namespace WindowsPartyGUI.ViewModels
{
    public class ShellViewModel: Conductor<object>, IHandle<ChangePageMessage>
    {
        public ShellViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
            Handle(new ChangePageMessage(typeof(LoginViewModel)));
        }

        public void Handle(ChangePageMessage message)
        {
            var instance = IoC.GetInstance(message.ViewModelType, message.ViewModelType.ToString());
            ActivateItem(instance);
        }
    }
}
