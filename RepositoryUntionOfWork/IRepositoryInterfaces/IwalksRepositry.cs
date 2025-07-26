using WalksAPI.Models.Domain;

namespace WalksAPI.RepositoryUntionOfWork.IRepositoryInterfaces
{
    public interface IwalksRepositry
    {
        Task<List<Walks>> GetAllAsync(string? filterOn = null, string? filterQuery = null
            , string? sortBy = null, bool IsAscending = true,
            int pageNumber = 1,int pageSize=20);
        Task<Walks?> GetByIdAsync(Guid id);
        Task<Walks> CreateAsync(Walks region);
        Task<Walks?> UpdateAsync(Guid id, Walks region);
        Task<Walks?> DeleteAsync(Guid id);

    }
}
