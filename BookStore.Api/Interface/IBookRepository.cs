using BookStore.Api.Models;
using System.Threading.Tasks;

namespace BookStore.Api.Interface
{
    public interface IBookRepository
    {
        Task<List<Book>?> GetBooks(string isbn, string genre, string sortBy, string orderBy, int offset, int limit);
        Task<List<Book>?> GetBooksByRating(decimal stars);
        Task<bool> AddBook(Book book);
    }
}
