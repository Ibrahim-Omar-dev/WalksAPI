using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalksAPI.Models.Domain;
using WalksAPI.SeedData;
namespace WalksAPI.ModelConfigration
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasData(RegionSeedData.GetRegions());
        }
    }
}
