using ERService.Contracts.Constants;
using ERService.Contracts.Events;
using ERService.Contracts.Mapping;
using ERService.Contracts.Messages;
using ERService.Contracts.Navigation;
using ERService.DataAccess.EntityFramework.Abstractions;
using ERService.DataAccess.EntityFramework.Entities;
using ERService.FunctionalCSharp;
using ERService.Mvvm.Base;
using ERService.Mvvm.Wrappers;
using ERService.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ERService.Customers.ViewModels
{
    public class CustomerViewModel : DetailViewModelBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IMappingProvider mappingProvider;
        private readonly INotificationProvider notificationProvider;
        private CustomerWrapper customer;
        private CustomerAddressWrapper customerAddress;
        private CustomerAddressWrapper customerCompanyAddress;
        private readonly IEventAggregator eventAggregator;
        private readonly IRegionManager regionManager;
        private readonly ResourceDictionary resourceDictionary;

        public CustomerViewModel(IRepositoryFactory repositoryFactory,
                                 IMappingProvider mappingProvider,
                                 INotificationProvider notificationProvider,
                                 IEventAggregator eventAggregator,
                                 IRegionManager regionManager,
                                 ResourceDictionary resourceDictionary)
        {
            this.repositoryFactory = repositoryFactory;
            this.mappingProvider = mappingProvider;
            this.notificationProvider = notificationProvider;
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.resourceDictionary = resourceDictionary;
        }

        public CustomerWrapper Customer
        {
            get => customer;
            private set => SetProperty(ref customer, value);
        }

        public CustomerAddressWrapper CustomerAddress
        {
            get => customerAddress;
            private set => SetProperty(ref customerAddress, value);
        }

        public CustomerAddressWrapper CustomerCompanyAddress
        {
            get => customerCompanyAddress;
            private set => SetProperty(ref customerCompanyAddress, value);
        }

        public bool KeepAlive => false;

        protected override Action<object> OnSaveExecute => async (args) =>
            await Result.Try(async () => await repositoryFactory.GetRepository<ICustomerRepository>()
                                                                .Update(Customer.Model))
                        .Tap(() => notificationProvider.ShowSuccess("Zapisano element...", "Zmiany zostały zapisane"))
                        .OnFailure(_ => notificationProvider.ShowError("Błąd...", "Wystąpił błąd podczas zapisu"))
                        .Tap(() => OnGoBackExecute(null));

        protected override Func<object, bool> OnSaveCanExecute => (object args) => 
            Customer is not null && Customer.HasErrors is false;

        protected override Action<object> OnGoBackExecute =>
            (args) => Dispatcher.Invoke(() => 
            regionManager.Regions[RegionNames.DetailRegion].NavigationService.Journal.GoBack());

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public async void OnNavigatedTo(NavigationContext navigationContext) =>
            await Result.Success(navigationContext.Parameters.GetValue<int>("id"))
                        .Map(async id => await GetCustomer(id))
                        .Map(customer => mappingProvider.MapTo<CustomerWrapper>(customer))
                        .Tap(customer => InitializeCustomer(customer))
                        .Tap(customer => InitializeCustomerAddress(customer))
                        .Tap(() => InitializeButtons());

        private void InitializeButtons() =>
            Dispatcher.Invoke(() => 
            eventAggregator
                .GetEvent<RegisterDetailMenuItemEvent>()
                .Publish(new DetailMenuItem[]
                {
                    new DetailMenuItem 
                    { 
                        Text = "Zapisz", 
                        Command = SaveCommand, 
                        Order = 1, 
                        Icon = resourceDictionary["Save"] 
                    },
                    new DetailMenuItem 
                    { 
                        Text = "Anuluj", 
                        Order = 2, 
                        Command = GoBackCommand, 
                        Icon = resourceDictionary["Backward"] 
                    }
                }));

        private void InitializeCustomerAddress(CustomerWrapper customer)
        {
            CustomerAddress = customer.CustomerAddresses.SingleOrDefault(address => address.Type == AddressType.Personal);
            CustomerCompanyAddress = customer.CustomerAddresses.SingleOrDefault(address => address.Type == AddressType.Business);
            CustomerAddress.PropertyChanged += (s, a) => ((DelegateCommand<object>)SaveCommand).RaiseCanExecuteChanged();
            CustomerCompanyAddress.PropertyChanged += (s, a) => ((DelegateCommand<object>)SaveCommand).RaiseCanExecuteChanged();
        }

        private void InitializeCustomer(CustomerWrapper customer)
        {
            customer.PropertyChanged += (s, a) => ((DelegateCommand<object>)SaveCommand).RaiseCanExecuteChanged();
            Customer = customer;
        }

        private async Task<Customer> GetCustomer(int id) => 
            await Result.Try(() => repositoryFactory.GetRepository<ICustomerRepository>())
                        .Bind(async repository => await repository.GetByIdAsync(id)
                                                                  .ToResult($"No customer with provided id: {id}"))
                        .Match(onFailure: _ => CreateCustomer(), onSuccess: customer => customer);

        private Customer CreateCustomer() => 
            new Customer()
            {
                CustomerAddresses = new CustomerAddress[]
                {
                    new CustomerAddress { Type = AddressType.Personal },
                    new CustomerAddress { Type = AddressType.Business }
                }
            };
    }
}
