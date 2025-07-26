using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalksAPI.Models.Domain;
using WalksAPI.SeedData;

namespace WalksAPI.ModelConfigration
{
    public class DifficulltyConfigration : IEntityTypeConfiguration<Difficulty>
    {
        public void Configure(EntityTypeBuilder<Difficulty> builder)
        {
            builder.HasData(DifficultySeedData.GetData());
        }
    }
}
