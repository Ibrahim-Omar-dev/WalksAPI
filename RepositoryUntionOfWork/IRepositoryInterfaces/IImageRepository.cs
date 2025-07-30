using WalksAPI.Models.Domain;

namespace WalksAPI.RepositoryUntionOfWork.IRepositoryInterfaces
{
    public interface IImageRepository 
    {
        Task<Image> UploadImage(Image image);

    }
}
