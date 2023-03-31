using CloudinaryDotNet.Actions;

namespace BlogSite_API.Repository.IRepository
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string publicUrl);
    }
}
