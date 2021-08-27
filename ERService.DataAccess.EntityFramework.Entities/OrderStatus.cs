using ERService.Contracts.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class OrderStatus : AuditableEntity<int>, IVersionedEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]        
        public StatusGroup Group { get; set; }

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }
    
    public enum StatusGroup
    {
        [Description("Otwarte")]
        Open,
        [Description("W trakcie")]
        InProgress,
        [Description("Zamknięte")]
        Finished
    }
}
