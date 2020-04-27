using Caliburn.Micro;

namespace WindowsPartyGUI.ViewModels
{
    public class ShellViewModel: Conductor<object>
    {
        public ShellViewModel(MainViewModel main)
        {
            LoadLogin(main);
        }

        public void LoadLogin(MainViewModel main)
        {
            ActivateItem(main);
        }
    }
}
