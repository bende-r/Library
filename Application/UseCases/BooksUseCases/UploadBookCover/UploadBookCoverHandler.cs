using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Exceptions;
using MediatR;

namespace Application.UseCases.BooksUseCases.UploadBookCover
{
  
    public class UploadBookCoverHandler : IRequestHandler<UploadBookCoverRequest, UploadBookCoverResponse>
    {

        public UploadBookCoverHandler()
        {
        }
        public async Task<UploadBookCoverResponse> Handle(UploadBookCoverRequest request, CancellationToken cancellationToken)
        {
            var fileExtension = Path.GetExtension(request.file.FileName);

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", };

            if (allowedExtensions.Contains(fileExtension.ToLowerInvariant()))
            {
                var parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
                var path = Path.Combine(parentDirectory, "web/public/pictures", request.bookId + request.file.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await request.file.CopyToAsync(stream, cancellationToken);
            }
            else
            {
                throw new ImageUploadException("Wrong extension");
            }
            return new UploadBookCoverResponse() { Message = request.bookId + request.file.FileName };
        }
    }
}
