using WalksAPI.Models.Domain;

namespace WalksAPI.SeedData
{
    public static class DifficultySeedData
    {
        public static List<Difficulty> GetData()
        {
            return new List<Difficulty>
            {
                new Difficulty
                {
                    Id = Guid.Parse("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("f808ddcd-b5e5-4d80-b732-1ca523e48434"),
                    Name = "Hard"
                }
            };
        }
    }
}
