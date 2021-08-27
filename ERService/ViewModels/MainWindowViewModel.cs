using ERService.Contracts.Events;
using ERService.Contracts.Navigation;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace ERService.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator) {
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;

            eventAggregator.GetEvent<RegisterSideMenuItemEvent>().Subscribe(AddMenuItem);
        }

        private void AddMenuItem(MenuItem menuItem) => 
            MenuItems.Add(menuItem);

        public ObservableCollection<MenuItem> MenuItems { get; set; } = new ObservableCollection<MenuItem>();
    }
}
