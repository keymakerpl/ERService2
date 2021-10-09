using ERService.Contracts.Constants;
using ERService.Contracts.Mapping;
using ERService.Contracts.Messages;
using ERService.Contracts.Navigation;
using ERService.Customers.Models;
using ERService.DataAccess.EntityFramework.Abstractions;
using ERService.DataAccess.EntityFramework.Entities;
using ERService.FunctionalCSharp;
using ERService.Wpf;
using Prism.Events;
using Prism.Regions;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;

namespace ERService.Customers.ViewModels
{
    public partial class CustomersListViewModel : GenericListViewModel<CustomerLookupItem<int>, Customer>, IDetailMenuItems
    {
        private readonly IMappingProvider mappingProvider;
        private readonly IRegionManager regionManager;
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IEventAggregator eventAggregator;
        private readonly INotificationProvider notificationProvider;
        private readonly ResourceDictionary resourceDictionary;

        public CustomersListViewModel(IRepositoryFactory repositoryFactory,
                                      IEventAggregator eventAggregator,
                                      INotificationProvider notificationProvider,
                                      IMappingProvider mappingProvider,
                                      IRegionManager regionManager,
                                      ResourceDictionary resourceDictionary)
        {
            this.repositoryFactory = repositoryFactory;
            this.mappingProvider = mappingProvider;
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.notificationProvider = notificationProvider;
            this.resourceDictionary = resourceDictionary;

            Items = new IncrementalList<CustomerLookupItem<int>>((count, baseIndex) =>
            LoadItems((count, baseIndex, Filter)))
            { MaxItemCount = 100 };
        }

        protected override Func<Result> AddItem => () =>
            Result.Try(() =>
            Dispatcher.Invoke(() =>
            regionManager.Regions[RegionNames.DetailRegion]
                         .RequestNavigate(ViewNames.CustomerView))
            );

        protected override Func<CustomerLookupItem<int>, Task<Result>> RemoveItem =>
            async selectedCustomer => await repositoryFactory.GetRepository<ICustomerRepository>()
                                                             .RemoveById(selectedCustomer.Id);

        protected override Action<CustomerLookupItem<int>> OpenItem => 
            selectedCustomer => 
            {
                var parameters = new NavigationParameters();
                parameters.Add("id", selectedCustomer.Id);
                Dispatcher.Invoke(() =>
                regionManager.Regions[RegionNames.DetailRegion]
                             .RequestNavigate(ViewNames.CustomerView, parameters));
            };

        protected override Action<LoadItemsParameters<Customer>> LoadItems =>
            async parameters =>
            await Result.Try(() => repositoryFactory.GetRepository<ICustomerRepository>())
                        .Map(async repository => await repository.FindByAsync(parameters.FilterPredicate,
                                                                              parameters.BaseIndex,
                                                                              (int)parameters.Count).ConfigureAwait(false))
                        .Map(customersFromRepo => mappingProvider.MapTo<IEnumerable<CustomerLookupItem<int>>>(customersFromRepo))
                        .Tap(customersToAdd => ((IncrementalList<CustomerLookupItem<int>>)Items).LoadItems(customersToAdd));

        protected override Action OnItemRemoved =>
            async () =>
            {
                await Result.Try(() => (Items as IncrementalList<CustomerLookupItem<int>>)?.Remove(SelectedItem))
                            .Ensure(isRemoved => isRemoved.HasValue, "Cannot remove item from list")
                            .Ensure(isRemoved => isRemoved.Value, "Cannot remove item from list")
                            .Tap(async () =>
                            await notificationProvider.ShowSuccess("Usunięto element...", "Element został usunięty z bazy danych"));
            };

        protected override Action OnItemAdded =>
            () => notificationProvider.ShowSuccess("Dodano klienta...", "Element został dodany do bazy danych");

        protected override Action<Expression<Func<Customer, bool>>> OnFilterChanged =>
            async _ => await (Items as IncrementalList<CustomerLookupItem<int>>)?.LoadMoreItemsAsync(100);

        public IEnumerable<DetailMenuItem> DetailMenuItems()
        {
            yield return new DetailMenuItem
            {
                Text = "Dodaj",
                Command = AddItemCommand,
                Order = 1,
                Icon = resourceDictionary["PlusCircle"]
            };
            yield return new DetailMenuItem
            {
                Text = "Usuń",
                Command = RemoveItemCommand,
                Order = 2,
                Icon = resourceDictionary["MinusCircle"]
            };
        }
    }
}