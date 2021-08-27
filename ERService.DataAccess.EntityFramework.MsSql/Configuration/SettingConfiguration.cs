using ERService.DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERService.DataAccess.EntityFramework.SqlServer.Configuration
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder) => 
            builder.HasData(new Setting[]
            {
                new Setting()
                {
                    Id = 1,
                    Key = "CompanyName",
                    Category = "CompanyInfo",
                    Value = "Test",
                    ValueType = typeof(string).FullName,
                    Description = "Nazwa firmy"
                },
                new Setting()
                {
                    Id = 2,
                    Key = "CompanyStreet",
                    Category = "CompanyInfo",
                    Value = "",
                    ValueType = typeof(string).FullName,
                    Description = "Ulica przy jakiej prowadzona jest działalność"
                },
                new Setting()
                {
                    Id = 3,
                    Key = "CompanyNumber",
                    Category = "CompanyInfo",
                    Value = "",
                    ValueType = typeof(string).FullName,
                    Description = "Numer budynku"
                },
                new Setting()
                {
                    Id = 4,
                    Key = "CompanyCity",
                    Category = "CompanyInfo",
                    Value = "",
                    ValueType = typeof(string).FullName,
                    Description = "Miasto prowadzenia działalności"
                },
                new Setting()
                {
                    Id = 5,
                    Key = "CompanyPostCode",
                    Category = "CompanyInfo",
                    Value = "",
                    ValueType = typeof(string).FullName,
                    Description = "Kod pocztowy"
                },
                new Setting()
                {
                    Id = 6,
                    Key = "CompanyNIP",
                    Category = "CompanyInfo",
                    Value = "",
                    ValueType = typeof(string).FullName,
                    Description = "NIP"
                }
            });
    }
}