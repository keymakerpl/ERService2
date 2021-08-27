using Prism.Regions;
using Syncfusion.UI.Xaml.NavigationDrawer;
using System;
using System.Collections.Specialized;
using System.Windows;

namespace ERService.PrismExtensions.RegionAdapters
{
    public class SfNavigationDrawerAdapter : RegionAdapterBase<SfNavigationDrawer>
    {
        public SfNavigationDrawerAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, SfNavigationDrawer regionTarget)
        {
            if (region == null)
            {
                throw new ArgumentNullException(nameof(region));
            }

            if (regionTarget == null)
            {
                throw new ArgumentNullException(nameof(regionTarget));
            }

            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (FrameworkElement view in e.NewItems)
                    {
                        regionTarget.ContentView = view;
                    }
                }
            };
        }

        protected override IRegion CreateRegion() => new SingleActiveRegion();
    }
}
