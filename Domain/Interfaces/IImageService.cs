using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(string bookId, IFormFile file, CancellationToken cancellationToken);
    }
}
