using ERService.DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERService.DataAccess.EntityFramework.SqlServer.Configuration
{
    public class OrderTypeConfiguration : IEntityTypeConfiguration<OrderType>
    {
        public void Configure(EntityTypeBuilder<OrderType> builder) => 
            builder.HasData(new OrderType[]
            {
                new OrderType() { Id = 1, Name = "Gwarancyjna" },
                new OrderType() { Id = 2, Name = "Niegwarancyjna" },
                new OrderType() { Id = 3, Name = "Pogwarancyjna" }
            });
    }
}