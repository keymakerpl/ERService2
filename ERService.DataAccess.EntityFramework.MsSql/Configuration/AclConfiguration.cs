using ERService.DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;

namespace ERService.DataAccess.EntityFramework.SqlServer.Configuration
{

    public class AclConfiguration : IEntityTypeConfiguration<Acl>
    {
        public void Configure(EntityTypeBuilder<Acl> builder) {
            var aclsToAdd = new List<Acl>();
            Enumerable.Range(1, 10).ToList().ForEach(x => aclsToAdd.Add(new Acl() { Id = x, AclVerbId = x, Value = 1, RoleId = 1 }));
            builder.HasData(aclsToAdd.ToArray());
        }
    }
}