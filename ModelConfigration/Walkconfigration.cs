using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalksAPI.Models.Domain;
using WalksAPI.SeedData;

namespace WalksAPI.ModelConfigration
{
    public class Walkconfigration : IEntityTypeConfiguration<Walks>
    {
        public void Configure(EntityTypeBuilder<Walks> builder)
        {
            builder.HasData(WalkSeedData.GetWalks());
        }
    }

}

