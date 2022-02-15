using BookStore.Api.Models;
using System.Collections;
using System.Threading.Tasks;

namespace BookStore.Api.Interface
{
    public interface IAdminRepository
    {
        Task<Book?> GetBook(string isbn);
        Task<bool> AddNewBook(Book book);
        Task<bool> AddNewAuthor(Author author);
        Task AddBookToAuthor(int Id, string Isbn);
        Task<IEnumerable<AuthorBook>> GetBooksRelatedToAuthor(int Id);
        
    }
}
