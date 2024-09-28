namespace Application.UseCases.BooksUseCases
{
    public class BookResponse
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public bool IsBorrowed { get; set; }

        public string? ImageUrl { get; set; }
    }
}