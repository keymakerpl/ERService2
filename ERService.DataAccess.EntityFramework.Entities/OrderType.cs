using ERService.Contracts.Data;
using System.ComponentModel.DataAnnotations;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class OrderType : AuditableEntity<int>, IVersionedEntity
    {
        [Required]
        public string Name { get; set; }

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }
}
