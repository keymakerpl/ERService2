using ERService.DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace ERService.DataAccess.EntityFramework.SqlServer.Configuration
{
    public class AclVerbConfiguration : IEntityTypeConfiguration<AclVerb>
    {
        public void Configure(EntityTypeBuilder<AclVerb> builder) => 
            builder.HasData(new AclVerb[]
            {
                new AclVerb() { Id = 1, Name = "Dostęp do konfiguracji aplikacji", DefaultValue = 0 },
                new AclVerb() { Id = 2, Name = "Dostęp do konfiguracji wydruków", DefaultValue = 0 },
                new AclVerb() { Id = 3, Name = "Dostęp do konfiguracji numeracji", DefaultValue = 0 },
                new AclVerb() { Id = 4, Name = "Zarządzanie użytkownikami", DefaultValue = 0 },
                new AclVerb() { Id = 5, Name = "Dodawanie nowych napraw", DefaultValue = 0 },
                new AclVerb() { Id = 6, Name = "Usuwanie napraw", DefaultValue = 0 },
                new AclVerb() { Id = 7, Name = "Edytowanie napraw", DefaultValue = 0 },
                new AclVerb() { Id = 8, Name = "Dodawanie nowych klientów", DefaultValue = 0 },
                new AclVerb() { Id = 9, Name = "Usuwanie klientów", DefaultValue = 0 },
                new AclVerb() { Id = 10, Name = "Edytowanie klientów", DefaultValue = 0 }
            });
    }
}