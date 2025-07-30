using WalksAPI.Data;
using WalksAPI.RepositoryUntionOfWork.IRepositoryInterfaces;
using WalksAPI.RepositoryUntionOfWork.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace WalksAPI.RepositoryUntionOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IwalksRepositry WalksRepository { get; }
        public IRegionRepository RegionRepository { get; }
        public IImageRepository ImageRepository { get; }

        public UnitOfWork(AppDbContext context, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            WalksRepository = new WalksRepository(_context);
            RegionRepository = new RegionRepository(_context);
            ImageRepository = new LocalImageUpload(
                webHostEnvironment: webHostEnvironment,
                contextAccessor: httpContextAccessor,
                DbContext: _context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
