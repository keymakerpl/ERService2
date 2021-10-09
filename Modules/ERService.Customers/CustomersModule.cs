using ERService.Contracts.Constants;
using ERService.Contracts.Events;
using ERService.Contracts.Navigation;
using ERService.Customers.Views;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Syncfusion.Windows.Shared;
using System.Windows;

namespace ERService.Customers
{
    public class CustomersModule : IModule
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;
        private readonly ResourceDictionary resourceDictionary;

        public CustomersModule(IEventAggregator eventAggregator,
                               IRegionManager regionManager,
                               ResourceDictionary resourceDictionary)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.resourceDictionary = resourceDictionary;
        }

        public void OnInitialized(IContainerProvider containerProvider) => 
            eventAggregator.GetEvent<RegisterMainMenuItemEvent>()
                           .Publish(new MainMenuItem
                           {
                               Text = "Klienci",
                               Icon = resourceDictionary["Customers"],
                               Command = new DelegateCommand<object>(args => 
                               regionManager.Regions[RegionNames.DetailRegion]
                                            .RequestNavigate(ViewNames.CustomerListView))
                           });

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CustomersList>(ViewNames.CustomerListView);
            containerRegistry.RegisterForNavigation<Customer>(ViewNames.CustomerView);
        }
    }
}