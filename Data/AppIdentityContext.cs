using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WalksAPI.Data
{
    public class AppIdentityContext : IdentityDbContext
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = "c85c6b59-72fa-4db0-9de9-69e462384f7f";
            var user = "07b68a9b-4c46-4235-b84a-1d8a0690c6ef";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = admin,
                    ConcurrencyStamp = admin,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = user,
                    ConcurrencyStamp = user,
                    Name = "User",
                    NormalizedName = "USER"
                }
            };


            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
