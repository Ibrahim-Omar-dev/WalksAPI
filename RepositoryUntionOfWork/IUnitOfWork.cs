using WalksAPI.RepositoryUntionOfWork.IRepositoryInterfaces;
using WalksAPI.RepositoryUntionOfWork.Repository;

namespace WalksAPI.RepositoryUntionOfWork
{
    public interface IUnitOfWork
    {
        IwalksRepositry WalksRepository { get; }
        IRegionRepository RegionRepository { get; }
        Task SaveAsync();
    }
}
