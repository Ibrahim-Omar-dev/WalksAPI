namespace WalksAPI.Model.DTOs.Image
{
    public class ImageUploadRequest
    {
        public IFormFile File { get; set; }
        public string? FileDescription { get; set; }
        public string FileName { get; set; }

    }
}
