using ERService.Contracts.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class Acl : IEntity<int>, IVersionedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int AclVerbId { get; set; }

        public AclVerb AclVerb { get; set; }

        public int RoleId { get; set; }

        public int Value { get; set; }

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }
}