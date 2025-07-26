using WalksAPI.Models.Domain;

namespace WalksAPI.SeedData
{
    public static class WalkSeedData
    {
        public static List<Walks> GetWalks()
        {
            return new List<Walks>
            {
                new Walks
                {
                    Id = Guid.Parse("a3df276e-b47e-4f68-bd62-ea3fefdb2452"),
                    Name = "Coastal Breeze",
                    Description = "A peaceful walk along the coast with sea views.",
                    LengthInKM = 3.5,
                    WalkImageUrl = "https://images.pexels.com/photos/1005417/pexels-photo-1005417.jpeg",
                    RegionId = Guid.Parse("F7248FC3-2585-4EFB-8D1D-1C555F4087F6"), // Auckland
                    DifficultyId = Guid.Parse("54466f17-02af-48e7-8ed3-5a4a8bfacf6f") // Easy
                },
                new Walks
                {
                    Id = Guid.Parse("b963b6a6-07a0-4a2c-8c8b-efab3eaedf2d"),
                    Name = "Forest Trek",
                    Description = "Moderate trek through thick native forest.",
                    LengthInKM = 8.1,
                    WalkImageUrl = "https://images.pexels.com/photos/5825582/pexels-photo-5825582.jpeg",
                    RegionId = Guid.Parse("14CEBA71-4B51-4777-9B17-46602CF66153"), // Bay Of Plenty
                    DifficultyId = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c") // Medium
                },
                new Walks
                {
                    Id = Guid.Parse("d74e2d9a-c0f5-4f06-b4f1-f47914ef0de5"),
                    Name = "Summit Challenge",
                    Description = "A tough climb to the summit with rewarding views.",
                    LengthInKM = 15.6,
                    WalkImageUrl = "https://images.pexels.com/photos/460621/pexels-photo-460621.jpeg",
                    RegionId = Guid.Parse("14CEBA71-4B51-4777-9B17-46602CF66153"), // Wellington
                    DifficultyId = Guid.Parse("f808ddcd-b5e5-4d80-b732-1ca523e48434") // Hard
                }
            };
        }
    }
}
