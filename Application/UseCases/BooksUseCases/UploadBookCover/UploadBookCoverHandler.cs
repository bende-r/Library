using Domain.Interfaces;

using MediatR;

namespace Application.UseCases.BooksUseCases.UploadBookCover
{
    public class UploadBookCoverHandler : IRequestHandler<UploadBookCoverRequest, UploadBookCoverResponse>
    {
        private readonly IImageService _imageService;

        public UploadBookCoverHandler(IImageService imageService)
        {
            _imageService = imageService;
        }

        public async Task<UploadBookCoverResponse> Handle(UploadBookCoverRequest request, CancellationToken cancellationToken)
        {
            var filePath = await _imageService.UploadImageAsync(request.bookId, request.file, cancellationToken);

            return new UploadBookCoverResponse() { Message = filePath };
        }
    }
}