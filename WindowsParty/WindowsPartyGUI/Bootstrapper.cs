using System.Windows;
using WindowsPartyGUI.ViewModels;
using Caliburn.Micro;

namespace WindowsPartyGUI
{
    public class Bootstrapper: BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
