using Application.Exceptions;

using Domain.Interfaces;

using Microsoft.AspNetCore.Http;

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

        public async Task<string> UploadImageAsync(string bookId, IFormFile file, CancellationToken cancellationToken)
        {
            var fileExtension = Path.GetExtension(file.FileName);

            if (!allowedExtensions.Contains(fileExtension.ToLowerInvariant()))
            {
                throw new ImageUploadException("Wrong extension");
            }

            var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            var path = Path.Combine(parentDirectory, "web/public/pictures", bookId + file.FileName);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);

            return bookId + file.FileName; // Возвращаем название файла
        }
    }
}
