using System;

namespace WindowsPartyGUI.Models
{
    public class ChangePageMessage
    {
        public readonly Type ViewModelType;

        public ChangePageMessage(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }
    }
}
