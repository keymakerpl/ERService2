using ERService.Customers;
using ERService.DataAccess.EntityFramework.SqlServer;
using ERService.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using Syncfusion.UI.Xaml.NavigationDrawer;
using System.Windows;
using ERService.PrismExtensions.RegionAdapters;
using ERService.Core;
using ERService.MappingProvider;
using ERService.Contracts.Constants;
using System;
using ERService.MessageProvider;
using ERService.Orders;
using System.Windows.Navigation;

namespace ERService
{
    public partial class App : PrismApplication
    {
        public App() => Syncfusion.Licensing.SyncfusionLicenseProvider
            .RegisterLicense("NDg2NDY0QDMxMzkyZTMyMmUzMG1XTER6MFFGMjRqNTY2cXFmWkNzNVNhMDdZQTlsWTFLbUxzS2V1QmFxQ0E9");

        protected override Window CreateShell() => Container.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry) 
        {
            containerRegistry.RegisterForNavigation<DetailMenu>(ViewNames.DetailMenuView);
            containerRegistry.RegisterSingleton<ResourceDictionary>(() => new ResourceDictionary
            {
                Source = new Uri("/ERService.Wpf;component/Assets/Menu/Icons.xaml", UriKind.RelativeOrAbsolute)
            });
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog) {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule<CoreModule>()
                         .AddModule<MappingProviderModule>()
                         .AddModule<MessageProviderModule>()
                         .AddModule<DataAccessModule>()
                         .AddModule<CustomersModule>()
                         .AddModule<OrdersModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings) {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(SfNavigationDrawer), Container.Resolve<SfNavigationDrawerAdapter>());
        }
    }
}
