using WalksAPI.Models.DTOs.Difficultys;
using WalksAPI.Models.DTOs.Regions;

namespace WalksAPI.Models.DTOs.WalksDTO
{
    public class WalksDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKM { get; set; }
        public string WalkImageUrl { get; set; }

        public RegionDTO Region { get; set; }
        public DifficultyDTO Difficulty { get; set; }
    }
}
