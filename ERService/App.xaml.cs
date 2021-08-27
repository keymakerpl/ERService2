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

namespace ERService
{
    public partial class App : PrismApplication
    {
        public App() => 
            Syncfusion.Licensing.SyncfusionLicenseProvider
            .RegisterLicense("NDg2NDY0QDMxMzkyZTMyMmUzMG1XTER6MFFGMjRqNTY2cXFmWkNzNVNhMDdZQTlsWTFLbUxzS2V1QmFxQ0E9");

        protected override Window CreateShell() => Container.Resolve<MainWindow>();

        protected override void RegisterTypes(IContainerRegistry containerRegistry) { }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog) {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule<CoreModule>()
                         .AddModule<MappingProviderModule>()
                         .AddModule<DataAccessModule>()
                         .AddModule<CustomersModule>();
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings) {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
            regionAdapterMappings.RegisterMapping(typeof(SfNavigationDrawer), Container.Resolve<SfNavigationDrawerAdapter>());
        }
    }
}
