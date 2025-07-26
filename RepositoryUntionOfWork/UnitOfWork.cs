using WalksAPI.Data;
using WalksAPI.RepositoryUntionOfWork.IRepositoryInterfaces;
using WalksAPI.RepositoryUntionOfWork.Repository;

namespace WalksAPI.RepositoryUntionOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IwalksRepositry WalksRepository { get; }
        public IRegionRepository RegionRepository { get; }


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            WalksRepository = new WalksRepository(_context);
            RegionRepository = new RegionRepository(_context);

        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
