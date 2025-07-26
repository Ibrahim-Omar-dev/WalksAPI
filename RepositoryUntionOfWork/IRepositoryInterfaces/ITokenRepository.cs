using Microsoft.AspNetCore.Identity;

namespace WalksAPI.RepositoryUntionOfWork.IRepositoryInterfaces
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }

}
