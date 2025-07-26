using Microsoft.EntityFrameworkCore;
using WalksAPI.Data;
using WalksAPI.Models.Domain;
using WalksAPI.RepositoryUntionOfWork.IRepositoryInterfaces;

namespace WalksAPI.RepositoryUntionOfWork.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly AppDbContext context;

        public RegionRepository(AppDbContext appDbContext) {
            this.context = appDbContext;
        }
        public async Task<Region> CreateAsync(Region region)
        {
             await context.Regions.AddAsync(region);
            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await context.Regions.FindAsync(id);

            if (existingRegion == null)
            {
                return null;
            }

            context.Regions.Remove(existingRegion);
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await context.Regions.ToListAsync();
        }



        public async Task<Region?> GetByIdAsync(Guid id)
        {
            var regions =await context.Regions.FirstOrDefaultAsync(x => x.Id == id);
             return regions;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await context.Regions.FindAsync(id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            return existingRegion;
        }
    }
}
