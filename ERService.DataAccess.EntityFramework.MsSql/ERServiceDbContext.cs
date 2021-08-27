using ERService.DataAccess.EntityFramework.Entities;
using ERService.DataAccess.EntityFramework.SqlServer.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;

namespace ERService.DataAccess.EntityFramework.SqlServer
{
    public class ERServiceDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerAddress> CustomerAddresses { get; set; }

        public DbSet<Hardware> Hardwares { get; set; }

        public DbSet<HwCustomItem> HardwareCustomItems { get; set; }

        public DbSet<CustomItem> CustomItems { get; set; }

        public DbSet<HardwareType> HardwareTypes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<OrderType> OrderTypes { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Numeration> Numeration { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Acl> ACLs { get; set; }

        public DbSet<AclVerb> AclVerbs { get; set; }

        public DbSet<PrintTemplate> PrintTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new NumerationConfiguration())
                        .ApplyConfiguration(new AclVerbConfiguration())
                        .ApplyConfiguration(new RoleConfiguration())
                        .ApplyConfiguration(new AclConfiguration())
                        .ApplyConfiguration(new UserConfiguration())
                        .ApplyConfiguration(new SettingConfiguration())
                        .ApplyConfiguration(new HardwareTypeConfiguration())
                        .ApplyConfiguration(new CustomItemConfiguration())
                        .ApplyConfiguration(new OrderStatusConfiguration())
                        .ApplyConfiguration(new OrderTypeConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-AQ40DA9\SQLEXPRESS;Database=ERService;Trusted_Connection=True;MultipleActiveResultSets=true",
                options => options.MigrationsAssembly("ERService.DataAccess.EntityFramework.Migrations"))
                .LogTo(x => Debug.WriteLine(x));

            base.OnConfiguring(optionsBuilder);
        }
    }
}
