using Caliburn.Micro;

namespace WindowsPartyGUI.ViewModels
{
    public class ShellViewModel: Conductor<object>
    {
        public ShellViewModel()
        {
            // LoadLogin();
            LoadMain();
        }

        public void LoadLogin()
        {
            ActivateItem(new LoginViewModel ());
        }

        public void LoadMain()
        {
            ActivateItem(new MainViewModel());
        }
    }
}
