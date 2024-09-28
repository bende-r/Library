using MediatR;

using Microsoft.AspNetCore.Http;

namespace Application.UseCases.BooksUseCases.UploadBookCover
{
    public sealed record UploadBookCoverRequest(IFormFile file, string bookId) : IRequest<UploadBookCoverResponse>
    {
    }
}