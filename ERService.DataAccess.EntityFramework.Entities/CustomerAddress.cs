using ERService.Contracts.Data;
using System.ComponentModel.DataAnnotations;

namespace ERService.DataAccess.EntityFramework.Entities
{
    public class CustomerAddress : AuditableEntity<int>, IVersionedEntity
    {
        public int CustomerId { get; set; }
        
        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string City { get; set; }

        public string Postcode { get; set; }

        public Customer Customer { get; set; }

        public AddressType Type { get; set; }

        [ConcurrencyCheck]
        public long RowVersion { get; set; }
    }

    public enum AddressType
    {
        Personal,
        Business
    }
}