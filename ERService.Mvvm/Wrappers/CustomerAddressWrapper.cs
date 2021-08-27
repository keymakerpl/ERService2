using ERService.DataAccess.EntityFramework.Entities;
using ERService.Mvvm.Base;

namespace ERService.Mvvm.Wrappers
{
    public class CustomerAddressWrapper : AuditableWrapper<int, CustomerAddress>, IWrappable
    {
        private string _city;
        private string _houseNumber;
        private string _postcode;
        private string _street;

        public CustomerAddressWrapper(CustomerAddress model) : base(model)
        {
        }

        public string City
        {
            get => GetProperty<string>();
            set => SetProperty(ref _city, value);
        }

        public string HouseNumber
        {
            get => GetProperty<string>();
            set => SetProperty(ref _houseNumber, value);
        }

        public string Postcode
        {
            get => GetProperty<string>();
            set => SetProperty(ref _postcode, value);
        }

        public string Street
        {
            get => GetProperty<string>();
            set => SetProperty(ref _street, value);
        }

        public AddressType Type { get; set; }
    }
}