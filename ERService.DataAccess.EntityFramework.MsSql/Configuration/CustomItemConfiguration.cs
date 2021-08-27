using ERService.DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace ERService.DataAccess.EntityFramework.SqlServer.Configuration
{

    public class CustomItemConfiguration : IEntityTypeConfiguration<CustomItem>
    {
        public void Configure(EntityTypeBuilder<CustomItem> builder) {
            var hardwareTypes = new HardwareType[]
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
            };

            var customItemsToSeed = new List<CustomItem>();
            foreach (var hwType in hardwareTypes) {
                switch (hwType.Name) {
                    case "Komputer PC":
                        customItemsToSeed.Add(new CustomItem() { Id = 1, HardwareTypeId = hwType.Id, Key = "Procesor" });
                        customItemsToSeed.Add(new CustomItem() { Id = 2, HardwareTypeId = hwType.Id, Key = "RAM" });
                        customItemsToSeed.Add(new CustomItem() { Id = 3, HardwareTypeId = hwType.Id, Key = "HDD" });
                        customItemsToSeed.Add(new CustomItem() { Id = 4, HardwareTypeId = hwType.Id, Key = "Grafika" });
                        customItemsToSeed.Add(new CustomItem() { Id = 5, HardwareTypeId = hwType.Id, Key = "Napęd" });
                        customItemsToSeed.Add(new CustomItem() { Id = 6, HardwareTypeId = hwType.Id, Key = "Bateria" });
                        customItemsToSeed.Add(new CustomItem() { Id = 7, HardwareTypeId = hwType.Id, Key = "Zasilacz" });
                        customItemsToSeed.Add(new CustomItem() { Id = 8, HardwareTypeId = hwType.Id, Key = "Stan" });
                        break;
                    case "Laptop":
                        customItemsToSeed.Add(new CustomItem() { Id = 9, HardwareTypeId = hwType.Id, Key = "Procesor" });
                        customItemsToSeed.Add(new CustomItem() { Id = 10, HardwareTypeId = hwType.Id, Key = "RAM" });
                        customItemsToSeed.Add(new CustomItem() { Id = 11, HardwareTypeId = hwType.Id, Key = "HDD" });
                        customItemsToSeed.Add(new CustomItem() { Id = 12, HardwareTypeId = hwType.Id, Key = "Grafika" });
                        customItemsToSeed.Add(new CustomItem() { Id = 13, HardwareTypeId = hwType.Id, Key = "Napęd" });
                        customItemsToSeed.Add(new CustomItem() { Id = 14, HardwareTypeId = hwType.Id, Key = "Bateria" });
                        customItemsToSeed.Add(new CustomItem() { Id = 15, HardwareTypeId = hwType.Id, Key = "Zasilacz" });
                        customItemsToSeed.Add(new CustomItem() { Id = 16, HardwareTypeId = hwType.Id, Key = "Stan" });
                        break;
                    case "Monitor":
                        customItemsToSeed.Add(new CustomItem() { Id = 17, HardwareTypeId = hwType.Id, Key = "Przekątna ekranu" });
                        customItemsToSeed.Add(new CustomItem() { Id = 18, HardwareTypeId = hwType.Id, Key = "Akcesoria" });
                        customItemsToSeed.Add(new CustomItem() { Id = 19, HardwareTypeId = hwType.Id, Key = "Stan" });
                        break;
                    case "Telewizor":
                        customItemsToSeed.Add(new CustomItem() { Id = 20, HardwareTypeId = hwType.Id, Key = "Przekątna ekranu" });
                        customItemsToSeed.Add(new CustomItem() { Id = 21, HardwareTypeId = hwType.Id, Key = "Akcesoria" });
                        customItemsToSeed.Add(new CustomItem() { Id = 22, HardwareTypeId = hwType.Id, Key = "Stan" });
                        break;
                    //case "Telefon":
                    //case "Drukarka":
                    //case "Nawigacja":
                    //case "Konsola":
                    case "Aparat":
                        customItemsToSeed.Add(new CustomItem() { Id = 23, HardwareTypeId = hwType.Id, Key = "Stan" });
                        customItemsToSeed.Add(new CustomItem() { Id = 24, HardwareTypeId = hwType.Id, Key = "Akcesoria" });
                        break;
                }
            }

            builder.HasData(customItemsToSeed.ToArray());
        }
    }
}