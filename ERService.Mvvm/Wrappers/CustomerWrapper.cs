using ERService.DataAccess.EntityFramework.Entities;
using ERService.Mvvm.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ERService.Mvvm.Wrappers
{
    public class CustomerWrapper : AuditableWrapper<int, Customer>, IWrappable
    {
        private string _companyName;
        private string _description;
        private string _email;
        private string _email2;
        private string _firstName;
        private string _lastName;
        private string _nip;
        private string _phoneNumber;
        private string _phoneNumber2;

        public CustomerWrapper(Customer model) : base(model)
        {
        }

        public string CompanyName
        {
            get => GetProperty<string>();
            set => SetProperty(ref _companyName, value);
        }

        public string Description
        {
            get => GetProperty<string>();
            set => SetProperty(ref _description, value);
        }

        public string Email
        {
            get => GetProperty<string>();
            set => SetProperty(ref _email, value is "" ? null : value);
        }

        public string Email2
        {
            get => GetProperty<string>();
            set => SetProperty(ref _email2, value is "" ? null : value);
        }

        public string FirstName
        {
            get => GetProperty<string>();
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => GetProperty<string>();
            set => SetProperty(ref _lastName, value);
        }

        public string FullName
        {
            get => GetProperty<string>();
        }

        public string NIP
        {
            get => GetProperty<string>();
            set => SetProperty(ref _nip, value);
        }

        public string PhoneNumber
        {
            get => GetProperty<string>();
            set => SetProperty(ref _phoneNumber, value is "" ? null : value);
        }

        public string PhoneNumber2
        {
            get => GetProperty<string>();
            set => SetProperty(ref _phoneNumber2, value is "" ? null : value);
        }

        #region Relations

        public ICollection<CustomerAddressWrapper> CustomerAddresses { get; set; } = new Collection<CustomerAddressWrapper>();
        public ICollection<OrderWrapper> Orders { get; set; } = new Collection<OrderWrapper>();

        #endregion Relations
    }
}