using ERService.Contracts.Constants;
using ERService.Contracts.Events;
using ERService.Contracts.Navigation;
using ERService.Customers.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Syncfusion.Windows.Shared;

namespace ERService.Customers
{
    public class CustomersModule : IModule
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;

        public CustomersModule(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            eventAggregator.GetEvent<RegisterSideMenuItemEvent>().Publish(new MenuItem
            {
                Name = "Klienci",
                Command = new DelegateCommand<object>(args =>
                regionManager.Regions[RegionNames.ContentRegion].RequestNavigate(ViewNames.CustomerListView))
            });
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CustomersList>(ViewNames.CustomerListView);
            containerRegistry.RegisterForNavigation<Customer>(ViewNames.CustomerView);
        }
    }
}