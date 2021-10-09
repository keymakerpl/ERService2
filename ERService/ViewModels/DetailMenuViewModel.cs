using ERService.Contracts.Events;
using ERService.Contracts.Navigation;
using Prism.Events;
using Prism.Mvvm;
using System.Linq;
using System.Collections.ObjectModel;
using Prism.Regions;
using ERService.Contracts.Constants;
using System.Collections.Generic;
using ERService.Wpf;

namespace ERService.ViewModels
{
    public class DetailMenuViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;

        public ObservableCollection<DetailMenuItem> MenuItems { get; set; } = new ObservableCollection<DetailMenuItem>();

        public DetailMenuViewModel(IEventAggregator eventAggregator, IRegionManager regionManager)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;

            this.eventAggregator.GetEvent<RegisterDetailMenuItemEvent>()
                                .Subscribe(OnAddDetailMenuItem, ThreadOption.UIThread);

            this.eventAggregator.GetEvent<ClearDetailMenuEvent>()
                                .Subscribe(OnClearDetailMenu, ThreadOption.UIThread);

            this.regionManager.Regions[RegionNames.DetailRegion].NavigationService.Navigated += RegisterDetailButtons;
            this.regionManager.Regions[RegionNames.DetailRegion].NavigationService.Navigating += (s, a) =>
            OnClearDetailMenu();
            this.regionManager.Regions[RegionNames.DetailRegion].NavigationService.NavigationFailed += (s, a) =>
            OnClearDetailMenu();
        }

        private void RegisterDetailButtons(object sender, RegionNavigationEventArgs e)
        {
            foreach (var view in e.NavigationContext.NavigationService.Region.ActiveViews)
            {
                var context = view.GetType()?.GetProperty("DataContext")?.GetValue(view, null) as IDetailMenuItems;
                if (context == null)
                    continue;

                OnAddDetailMenuItem(context.DetailMenuItems());
            }
        }

        private void OnClearDetailMenu() => MenuItems.Clear();

        private void OnAddDetailMenuItem(IEnumerable<DetailMenuItem> menuItems)
        {
            Dispatcher.Invoke(() => 
            {
                MenuItems.AddRange(menuItems);
                MenuItems.OrderBy(item => item.Order);
            });
        }
    }
}