using ERService.Contracts.Constants;
using ERService.Contracts.Events;
using ERService.Contracts.Navigation;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace ERService.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;

        public MainWindowViewModel(IRegionManager regionManager,
                                   IEventAggregator eventAggregator) 
        {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;

            this.regionManager.RequestNavigate(RegionNames.DetailMenuRegion, ViewNames.DetailMenuView);
            this.eventAggregator.GetEvent<RegisterMainMenuItemEvent>().Subscribe(AddMenuItem);
        }

        private void AddMenuItem(MainMenuItem menuItem) => MenuItems.Add(menuItem);

        public ObservableCollection<MainMenuItem> MenuItems { get; set; } = new ObservableCollection<MainMenuItem>();
    }
}
