using ERService.FunctionalCSharp;
using ERService.Contracts.Constants;
using ERService.Contracts.Mapping;
using ERService.Customers.Models;
using ERService.DataAccess.EntityFramework.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ERService.Customers.ViewModels
{
    public class CustomersListViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IMappingProvider mappingProvider;
        private readonly IRegionManager regionManager;

        public ICommand DataGridMouseDoubleClick { get; }
        public IncrementalList<CustomerLookupItem<int>> Customers { get; }

        public bool KeepAlive => false;

        public CustomersListViewModel(IRepositoryFactory repositoryFactory, IMappingProvider mappingProvider, IRegionManager regionManager) 
        {
            this.repositoryFactory = repositoryFactory;
            this.mappingProvider = mappingProvider;
            this.regionManager = regionManager;

            DataGridMouseDoubleClick = new DelegateCommand<MouseButtonEventArgs>(OnMouseDoubleClick);

            Customers = new IncrementalList<CustomerLookupItem<int>>(async (count, baseIndex) =>
                await LoadMoreItems(count, baseIndex)) { MaxItemCount = 100 };
        }

        private void OnMouseDoubleClick(MouseButtonEventArgs args) => 
            Maybe.From(args.Source as SfDataGrid)
                 .ToResult("Expected SfDataGrid")
                 .Ensure(grid => grid.SelectedItem != null, "Nothing selected")
                 .Ensure(grid => grid.SelectedItem is CustomerLookupItem<int>, "Expected CustomerLookupItem")
                 .Map(grid => grid.SelectedItem as CustomerLookupItem<int>)
                 .Tap(selectedElement =>
                 {
                     var parameters = new NavigationParameters();
                     parameters.Add("id", selectedElement.Id);
                     regionManager.Regions[RegionNames.ContentRegion]
                                  .RequestNavigate(ViewNames.CustomerView, parameters);
                 });

        private async Task LoadMoreItems(uint count, int baseIndex) 
        {
            var repository = repositoryFactory.GetRepository<ICustomerRepository>();
            var customersToAdd = await repository.FindByAsync(x => true, baseIndex, (int)count);
            Customers.LoadItems(mappingProvider.MapTo<IEnumerable<CustomerLookupItem<int>>>(customersToAdd));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}
