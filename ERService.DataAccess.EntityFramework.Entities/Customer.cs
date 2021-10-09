using ERService.Contracts.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class Customer : AuditableEntity<int>, IVersionedEntity, ISoftDeletable
    {
        [StringLength(50, ErrorMessage = "Maksymalnie 50 znakow")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [StringLength(50, ErrorMessage = "Maksymalnie 50 znakow")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        [StringLength(50, ErrorMessage = "Maksymalnie 50 znakow")]
        public string CompanyName { get; set; }

        [StringLength(50, ErrorMessage = "Maksymalnie 50 znakow")]
        public string NIP { get; set; }

        [StringLength(50, ErrorMessage = "Maksymalnie 50 znakow")]
        [EmailAddress(ErrorMessage = "Adres email ma nieprawidłowy format")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "Maksymalnie 50 znakow")]
        [EmailAddress(ErrorMessage = "Adres email ma nieprawidłowy format")]
        public string Email2 { get; set; }

        [Phone(ErrorMessage = "Numer telefonu ma nieprawidłowy format")]
        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [MaxLength(20, ErrorMessage = "Maksymalnie 20 znaków")]
        public string PhoneNumber { get; set; }

        [Phone(ErrorMessage = "Numer telefonu ma nieprawidłowy format")]
        [MaxLength(20, ErrorMessage = "Maksymalnie 20 znaków")]
        public string PhoneNumber2 { get; set; }

        [StringLength(500, ErrorMessage = "Maksymalnie 500 znaków")]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        [ConcurrencyCheck]
        public long RowVersion { get; set; }

        #region Relations

        public ICollection<CustomerAddress> CustomerAddresses { get; set; } = new Collection<CustomerAddress>();
        public ICollection<Order> Orders { get; set; } = new Collection<Order>();        

        #endregion Relations
    }
}