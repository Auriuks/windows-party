using System.Collections.ObjectModel;
using WindowsPartyGUI.Models;

namespace WindowsPartyGUI.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Servers = new ObservableCollection<ServerModel>()
            {
                new ServerModel(){Name = "Canada", Distance = 4073},
                new ServerModel(){Name = "Latvia", Distance = 10},
                new ServerModel(){Name = "Lithuania", Distance = 5}
            };
        }
        public ObservableCollection<ServerModel> Servers { get; set; }
    }
}
