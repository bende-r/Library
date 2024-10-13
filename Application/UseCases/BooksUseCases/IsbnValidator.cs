using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.UseCases.BooksUseCases
{
    public static class IsbnValidator
    {
        private static readonly Regex Isbn10Regex = new Regex(@"^\d{9}[\d|X]$", RegexOptions.Compiled);
        private static readonly Regex Isbn13Regex = new Regex(@"^\d{13}$", RegexOptions.Compiled);

        public static bool IsValidIsbn(string isbn)
        {
            return Isbn10Regex.IsMatch(isbn) || Isbn13Regex.IsMatch(isbn);
        }
    }
}
