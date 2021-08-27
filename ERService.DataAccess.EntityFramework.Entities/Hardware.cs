using ERService.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class Hardware : AuditableEntity<int>, IVersionedEntity
    {
        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        [Required]
        [StringLength(80)]
        public string SerialNumber { get; set; }
        
        public int? HardwareTypeID { get; set; }
        public HardwareType HardwareType { get; set; }

        public ICollection<HwCustomItem> HardwareCustomItems { get; set; } = new Collection<HwCustomItem>();

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }
}
