using ERService.Contracts.Constants;
using ERService.Contracts.Events;
using ERService.Contracts.Navigation;
using ERService.Orders.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System.Windows;

namespace ERService.Orders
{
    public class OrdersModule : IModule
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;
        private readonly ResourceDictionary resourceDictionary;

        public OrdersModule(IEventAggregator eventAggregator,
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
                               Text = "Zlecenia",
                               Icon = resourceDictionary["Orders"],
                               Command = new DelegateCommand<object>(args =>
                               regionManager.Regions[RegionNames.DetailRegion]
                                            .RequestNavigate(ViewNames.OrderListView))
                           });

        public void RegisterTypes(IContainerRegistry containerRegistry) => 
            containerRegistry.RegisterForNavigation<OrdersList>(ViewNames.OrderListView);
    }
}
