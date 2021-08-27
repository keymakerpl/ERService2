using ERService.DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERService.DataAccess.EntityFramework.SqlServer.Configuration
{
    public class HardwareTypeConfiguration : IEntityTypeConfiguration<HardwareType>
    {
        public void Configure(EntityTypeBuilder<HardwareType> builder) => 
            builder.HasData(new HardwareType[]
            {
                new HardwareType() { Id = 1, Name = "Laptop" },
                new HardwareType() { Id = 2, Name = "Komputer PC" },
                new HardwareType() { Id = 3, Name = "Telefon" },
                new HardwareType() { Id = 4, Name = "Drukarka" },
                new HardwareType() { Id = 5, Name = "Konsola" },
                new HardwareType() { Id = 6, Name = "Nawigacja" },
                new HardwareType() { Id = 7, Name = "Aparat" },
                new HardwareType() { Id = 8, Name = "Monitor" },
                new HardwareType() { Id = 9, Name = "Telewizor" }
            });
    }
}