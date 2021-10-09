﻿using ERService.Contracts.Models;
using ERService.Mvvm.Base;
using System.ComponentModel.DataAnnotations;

namespace ERService.Customers.Models
{
    public class CustomerLookupItem<TId> : AuditableLookupItem<TId>, ILookupItem
    {
        [Display(Name = "Imię", Description = "Imię klienta")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko", Description = "Nazwisko klienta")]
        public string LastName { get; set; }

        [Display(Name = "Pełna nazwa", Description = "Imię oraz nazwisko klienta")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        [Display(Name = "Firma", Description = "Firma klienta")]
        public string CompanyName { get; set; }

        [Display(Name = "NIP", Description = "NIP firmy klienta")]
        public string NIP { get; set; }

        [Display(Name = "Email", Description = "Adres email klienta")]
        public string Email { get; set; }

        [Display(Name = "Email 2", Description = "Alternatywny adres email klienta")]
        public string Email2 { get; set; }

        [Display(Name = "Telefon", Description = "Numer telefonu klienta")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Telefon 2", Description = "Alternatywny numer telefonu klienta")]
        public string PhoneNumber2 { get; set; }

        [Display(Name = "Opis", Description = "Dodatkowe informacje o kliencie")]
        public string Description { get; set; }
    }
}
