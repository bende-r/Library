using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(string bookId, IFormFile file, CancellationToken cancellationToken);
    }
}