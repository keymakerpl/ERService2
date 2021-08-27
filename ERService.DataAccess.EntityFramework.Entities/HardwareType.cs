using ERService.Contracts.Data;
using System.ComponentModel.DataAnnotations;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class HardwareType : AuditableEntity<int>, IVersionedEntity
    {
        public string Name { get; set; }

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }
}
