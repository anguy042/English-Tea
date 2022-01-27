using BookStore.Sample.Function.Models;
using System.Threading.Tasks;

namespace BookStore.Sample.Function.Interface
{
    public interface IBookRepository
    {
        Task<Book> GetBook(string isbn);
        Task<bool> AddBook(Book book);
    }
}
