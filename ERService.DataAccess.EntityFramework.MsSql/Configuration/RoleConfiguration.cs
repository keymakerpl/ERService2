using ERService.DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERService.DataAccess.EntityFramework.SqlServer.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder) {
            builder.HasData(new Role[] 
            {
                new Role()
                {
                    Id = 1,
                    Name = "default",
                    IsSystem = true
                }
            });
        }
    }
}