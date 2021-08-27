using ERService.Contracts.Data;
using System.ComponentModel.DataAnnotations;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class Setting : AuditableEntity<int>, IVersionedEntity
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string ValueType { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }
}
