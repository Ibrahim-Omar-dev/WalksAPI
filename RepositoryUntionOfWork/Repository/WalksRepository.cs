using Microsoft.EntityFrameworkCore;
using WalksAPI.Data;
using WalksAPI.Models.Domain;
using WalksAPI.RepositoryUntionOfWork.IRepositoryInterfaces;

namespace WalksAPI.RepositoryUntionOfWork.Repository
{
    public class WalksRepository : IwalksRepositry
    {
        private readonly AppDbContext context;

        public WalksRepository(AppDbContext appDbContext) {
            this.context = appDbContext;
        }
        public async Task<Walks> CreateAsync(Walks walks)
        {
             await context.Walks.AddAsync(walks);
            return walks;
        }

        public async Task<Walks?> DeleteAsync(Guid id)
        {
            var existingRegion = await context.Walks.FindAsync(id);

            if (existingRegion == null)
            {
                return null;
            }

            context.Walks.Remove(existingRegion);
            return existingRegion;
        }

        public async Task<List<Walks>> GetAllAsync(string? filterOn = null, string? filterQuery = null
            , string? sortBy = null, bool IsAscending = true,
            int pageNumber = 1,int pageSize=20)
        {
            IQueryable<Walks> query =context.Walks.Include("Region").Include("Difficulty").AsQueryable();
            //Filter on
            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(x => x.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.Where(x => x.Description.Contains(filterQuery));
                }
            }
            //sort on
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if(sortBy.Equals("Name",StringComparison.OrdinalIgnoreCase))
                {
                    query = IsAscending? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
                }
                else if (sortBy.Equals("LengthInKM", StringComparison.OrdinalIgnoreCase))
                {
                    query = IsAscending ? query.OrderBy(x => x.LengthInKM) : query.OrderByDescending(x => x.LengthInKM);
                }
            }
            int skipResult = (pageNumber - 1) * pageSize;

            return await query.Skip(skipResult).Take(pageSize).ToListAsync();
        }



        public async Task<Walks?> GetByIdAsync(Guid id)
        {
            var walks =await context.Walks.FirstOrDefaultAsync(x => x.Id == id);
             return walks;
        }

        public async Task<Walks?> UpdateAsync(Guid id, Walks walks)
        {
            var existingRegion = await context.Walks.FindAsync(id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Description = walks.Description;
            existingRegion.Name = walks.Name;
            existingRegion.WalkImageUrl = walks.WalkImageUrl;
            existingRegion.LengthInKM = walks.LengthInKM;
            existingRegion.RegionId = walks.RegionId;
            existingRegion.DifficultyId = walks.DifficultyId;


            return existingRegion;
        }
    }
}