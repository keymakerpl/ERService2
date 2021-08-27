using ERService.Contracts.Data;
using System.ComponentModel.DataAnnotations;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class PrintTemplate : AuditableEntity<int>, IVersionedEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Template { get; set; }

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }
}
