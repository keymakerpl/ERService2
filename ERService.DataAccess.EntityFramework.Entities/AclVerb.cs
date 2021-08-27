using ERService.Contracts.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class AclVerb : IEntity<int>, IVersionedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int DefaultValue { get; set; }
        
        [MaxLength(50)]
        public string Description { get; set; }

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }
}
