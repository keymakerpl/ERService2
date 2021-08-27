using ERService.Contracts.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class Numeration : IVersionedEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Pattern { get; set; }

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }
}
