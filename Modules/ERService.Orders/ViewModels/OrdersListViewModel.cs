using ERService.Contracts.Mapping;
using ERService.Contracts.Messages;
using ERService.DataAccess.EntityFramework.Abstractions;
using ERService.FunctionalCSharp;
using ERService.Orders.Models;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ERService.Orders.ViewModels
{
    public class OrdersListViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IEventAggregator eventAggregator;
        private readonly INotificationProvider notificationProvider;
        private readonly IMappingProvider mappingProvider;
        private readonly IRegionManager regionManager;
        private readonly ResourceDictionary resourceDictionary;

        public IncrementalList<OrderLookupItem<int>> Orders { get; }

        public OrdersListViewModel(IRepositoryFactory repositoryFactory,
                                   IEventAggregator eventAggregator,
                                   INotificationProvider notificationProvider,
                                   IMappingProvider mappingProvider,
                                   IRegionManager regionManager,
                                   ResourceDictionary resourceDictionary)
        {
            this.repositoryFactory = repositoryFactory;
            this.eventAggregator = eventAggregator;
            this.notificationProvider = notificationProvider;
            this.mappingProvider = mappingProvider;
            this.regionManager = regionManager;
            this.resourceDictionary = resourceDictionary;

            Orders = new IncrementalList<OrderLookupItem<int>>(async (count, baseIndex) =>
                await LoadMoreItems(count, baseIndex))
            { MaxItemCount = 100 };
        }

        private async Task LoadMoreItems(uint count, int baseIndex) =>
            await Result.Try(() => repositoryFactory.GetRepository<IOrderRepository>())
                        .Map(async repository => await repository.FindByAsync(x => true, baseIndex, (int)count))
                        .OnFailure(error => notificationProvider.ShowError("Błąd", error))
                        .Tap(ordersToAdd =>
                            Orders.LoadItems(mappingProvider.MapTo<IEnumerable<OrderLookupItem<int>>>(ordersToAdd)));

        public bool KeepAlive => false;

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }
    }
}
