using Caliburn.Micro;

namespace WindowsPartyGUI.ViewModels
{
    public class ShellViewModel: Conductor<object>
    {
        public ShellViewModel()
        {
            LoadLogin();
        }

        public void LoadLogin()
        {
            ActivateItem(new LoginViewModel ());
        }
    }
}
