using ERService.Contracts.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class Order : AuditableEntity<int>, IVersionedEntity
    {
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        [StringLength(50)]
        public string Number { get; set; }

        [NotMapped]
        public string OrderNumber { get { return $"{Id}/{Number}"; } }

        [Column(TypeName = "DateTime")]
        public DateTime DateRegistered { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime? DateEnded { get; set; }

        public int? OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public int? OrderTypeId { get; set; }
        public OrderType OrderType { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        [StringLength(50)]
        public string Cost { get; set; }

        [StringLength(1000)]
        public string Fault { get; set; }

        [StringLength(1000)]
        public string Solution { get; set; }

        [StringLength(1000)]
        public string Comment { get; set; }

        [StringLength(50)]
        public string ExternalNumber { get; set; }
        
        public int Progress { get; set; }

        public ICollection<Hardware> Hardwares { get; set; } = new Collection<Hardware>();

        public ICollection<Blob> Attachments { get; set; } = new Collection<Blob>();

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }
}
