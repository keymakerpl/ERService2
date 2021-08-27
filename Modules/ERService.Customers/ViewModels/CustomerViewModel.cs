using ERService.Contracts.Mapping;
using ERService.DataAccess.EntityFramework.Abstractions;
using ERService.DataAccess.EntityFramework.Entities;
using ERService.FunctionalCSharp;
using ERService.Mvvm.Wrappers;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace ERService.Customers.ViewModels
{
    public class CustomerViewModel : BindableBase, INavigationAware, IRegionMemberLifetime
    {
        private readonly IRepositoryFactory repositoryFactory;
        private readonly IMappingProvider mappingProvider;

        public ICommand SaveCommand { get; }

        public CustomerViewModel(IRepositoryFactory repositoryFactory, IMappingProvider mappingProvider)
        {
            this.repositoryFactory = repositoryFactory;
            this.mappingProvider = mappingProvider;

            SaveCommand = new DelegateCommand(OnSaveExecute);
        }

        private async void OnSaveExecute() => 
            await repositoryFactory.GetRepository<ICustomerRepository>().Update(Customer.Model);

        public CustomerWrapper Customer { get; private set; }

        public CustomerAddressWrapper CustomerAddress { get; private set; }
        
        public CustomerAddressWrapper CustomerCompanyAddress { get; private set; }

        public bool KeepAlive => false;

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var id = navigationContext.Parameters.GetValue<int>("id");
            var repository = repositoryFactory.GetRepository<ICustomerRepository>();
            var customerFromRepo = repository.GetById(id);
            
            Customer = mappingProvider.MapTo<CustomerWrapper>(customerFromRepo);

            CustomerAddress = Customer.CustomerAddresses.ToList()
                                                        .DefaultIfEmpty(new CustomerAddressWrapper(new CustomerAddress
                                                        {
                                                            Type = AddressType.Personal
                                                        }))
                                                        .SingleOrDefault(x => x.Type == AddressType.Personal);

            CustomerCompanyAddress = Customer.CustomerAddresses.ToList()
                                                               .DefaultIfEmpty(new CustomerAddressWrapper(new CustomerAddress
                                                               {
                                                                   Type = AddressType.Business
                                                               }))
                                                               .SingleOrDefault(x => x.Type == AddressType.Business);
        }
    }
}
