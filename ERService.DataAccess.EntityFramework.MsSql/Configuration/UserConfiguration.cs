using ERService.DataAccess.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERService.DataAccess.EntityFramework.SqlServer.Configuration
{

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.HasData(new User[] 
            {
                new User()
                {
                    Id = 1,
                    Login = "administrator",
                    IsActive = true,
                    IsAdmin = true,
                    IsSystem = true,
                    RoleId = 1,
                    PasswordHash = "TxLVWrN0l5eCTgSgWzu+9DD0hjm9GHUQFke/ixgRhXG5fL6GqohNNRUozIuQnpMQ/AMUgo5O7Sm9XPExBvK5fyULJUVdIMOT/mupzdeDDP6L/5Zlc8IBBOIwXmRszQq7VjPxff6rvMMscS3KCvk7B3LYHZmdkpYWnndqsPwaCmlb8UdUvsZYbfT4ycUr4SqO2lrhVzy5decN8PtlCMKM9dAoYwqKppkN5Bw5Ge9Rt61dCNkefgmWkMMnXJI3mmpTTSOzTPjdIqaSmV4jFnpih6oSPwgTXWjsqGJprpL7y8fztD/hSCjluLGgfXBAsYiqcgDD2gKsmjGqbHVLT+6dcg==",
                    Salt = "h1WROAWPPhJjGE9FWtKit3rolD9Sobb5BDNaO9k+TvBJpEooFM8kIRRyizkrEZ8JGm/zfbncrcePUMKhFXN8tQ=="
                }
            });
        }
    }
}