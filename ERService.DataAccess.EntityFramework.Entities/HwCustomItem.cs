using ERService.Contracts.Data;
using System.ComponentModel.DataAnnotations;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class HwCustomItem : AuditableEntity<int>, IVersionedEntity
    {
        public int CustomItemId { get; set; }
        public CustomItem CustomItem { get; set; }

        [StringLength(200)]
        public string Value { get; set; }

        public int HardwareId { get; set; }
        public Hardware Hardware { get; set; }

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }
}
