using ERService.Contracts.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class Role : AuditableEntity<int>, IVersionedEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public bool IsSystem { get; set; }

        public ICollection<Acl> ACLs { get; set; } = new Collection<Acl>();

        public ICollection<User> Users { get; set; } = new Collection<User>();

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }
}
