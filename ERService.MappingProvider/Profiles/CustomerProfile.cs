using AutoMapper;
using ERService.DataAccess.EntityFramework.Entities;
using ERService.Mvvm.Wrappers;

namespace ERService.MappingProvider.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            //CreateMap<CustomerWrapper, Customer>().ConvertUsing(wrapper => wrapper.Model);
            //CreateMap<CustomerAddressWrapper, CustomerAddress>().ConvertUsing(wrapper => wrapper.Model);
        }
    }

    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            //CreateMap<OrderWrapper, Order>().ConvertUsing(wrapper => wrapper.Model);
        }
    }
}
