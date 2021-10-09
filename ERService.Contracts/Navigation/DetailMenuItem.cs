using System.Collections.ObjectModel;

namespace ERService.Contracts.Navigation
{
    public class DetailMenuItem : MenuItem
    {
        public int Order { get; set; }
        public ObservableCollection<DetailMenuItem> SubItems { get; } = new ObservableCollection<DetailMenuItem>();
    }
}