using ERService.Contracts.Data;
using ERService.DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERService.DataAccess.EntityFramework.SqlServer.Configuration
{

    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasQueryFilter(customer => !customer.IsDeleted);
        }
    }
}