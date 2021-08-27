using ERService.Contracts.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class User : AuditableEntity<int>, IVersionedEntity
    {
        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName ?? Login} {LastName ?? ""}";

        [NotMapped]
        public string Initials => FirstName?.Substring(0, 1) + LastName?.Substring(0, 1);

        [Phone]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsSystem { get; set; }

        public int? RoleId { get; set; }

        public Role Role { get; set; }

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }
}