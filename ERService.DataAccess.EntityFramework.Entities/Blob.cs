using ERService.Contracts.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class Blob : IEntity<int>, IVersionedEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string FileName { get; set; }

        public string Description { get; set; }

        public string Checksum { get; set; }

        public int Size { get; set; }

        [Required]
        public byte[] Data { get; set; }

        public long RowVersion { get; set; }
    }
}
