using ERService.DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERService.DataAccess.EntityFramework.SqlServer.Configuration
{

    public class NumerationConfiguration : IEntityTypeConfiguration<Numeration>
    {
        public void Configure(EntityTypeBuilder<Numeration> builder) => 
            builder.HasData(new Numeration[]
            {
                new Numeration() { Id = 1, Name = "default", Pattern = "[MM][RRRR]" }
            });
    }
}