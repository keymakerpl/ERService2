using ERService.Contracts.Data;
using System.ComponentModel.DataAnnotations;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class CustomItem : AuditableEntity<int>, IVersionedEntity
    {
        [Required]
        [StringLength(50)]
        public string Key { get; set; }
        
        public int HardwareTypeId { get; set; }
        public HardwareType HardwareType { get; set; }

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }
}
