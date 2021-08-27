using ERService.DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERService.DataAccess.EntityFramework.SqlServer.Configuration
{
    public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
    {
        public void Configure(EntityTypeBuilder<OrderStatus> builder) => 
            builder.HasData(new OrderStatus[]
            {
                new OrderStatus() { Id = 1, Name = "Nowa naprawa" },
                new OrderStatus() { Id = 2, Name = "W trakcie naprawy" },
                new OrderStatus() { Id = 3, Name = "Oczekuje na części" },
                new OrderStatus() { Id = 4, Name = "Naprawa zakończona" },
                new OrderStatus() { Id = 5, Name = "Naprawa niewykonana" }
            });
    }
}