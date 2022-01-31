using BookStore.Api.Models;
using System.Threading.Tasks;

namespace BookStore.Api.Interface
{
    public interface IBookRepository
    {
        Task<Book?> GetBook(string isbn);
        Task<bool> AddBook(Book book);
    }
}
