using ERService.Contracts.Models;
using ERService.Mvvm.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERService.Orders.Models
{
    public class OrderLookupItem<TId>: AuditableLookupItem<TId>, ILookupItem
    {
        [Display(AutoGenerateField = false, Description = "Number field is not generated in UI")]
        public string Number { get; set; }

        [Display(Name = "Numer", Description = "Numer zlecenia")]
        public string OrderNumber { get { return $"{Id}/{Number}"; } }

        [Display(Name = "Data rejestracji", Description = "Data dodania zlecenia do systemu")]
        public DateTime DateRegistered { get; set; }

        [Display(Name = "Data zakończenia", Description = "Data zakończenia zlecenia")]
        public DateTime? DateEnded { get; set; }

        [Display(Name = "Koszt", Description = "Koszt zlecenia")]
        public string Cost { get; set; }

        [Display(Name = "Opis", Description = "Opis zlecenia")]
        public string Fault { get; set; }

        [Display(Name = "Rozwiązanie", Description = "Rozwiązanie do zgłoszenia")]
        public string Solution { get; set; }

        [Display(Name = "Komentarz", Description = "Komentarz do zlecenia")]
        public string Comment { get; set; }

        [Display(Name = "Numer zewnętrzny", Description = "Numer zewnętrzny zlecenia")]
        public string ExternalNumber { get; set; }

        [Display(Name = "Postęp", Description = "Postęp zlecenia")]
        public int Progress { get; set; }
    }
}
