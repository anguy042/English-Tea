using BookStore.Api.Interface;
using BookStore.Api.Models;
using Dapper;
using Npgsql;

namespace BookStore.Api.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly NpgsqlConnection _connection;

        public AdminRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> AddNewBook(Book book)
        {
            var results = await _connection.QueryAsync<Book>($"INSERT INTO Book " +
                                                             $"VALUES('{book.Isbn}', '{book.Name}'," +
                                                             $"'{book.Description}', '{book.Price}', '{book.Genre}'," +
                                                             $"'{book.Publisher}', '{book.Published_Date}', {book.Copies_Sold}, " +
                                                             $"'{book.Seller}')");

            //For now return true (success)
            return true;
        }

        public async Task<Book?> GetBook(string isbn)
        {
            //Get book from database
            var results = await _connection.QueryAsync<Book>($"SELECT * FROM book b " +
                                                             $"WHERE b.isbn = '{isbn}' ");
            return results.FirstOrDefault();
        }

        public async Task<bool> AddNewAuthor(Author author)
        {
            var results = await _connection.QueryAsync<Author>($"INSERT INTO Author " +
                                                               $"VALUES(DEFAULT, '{author.FirstName}', " +
                                                               $"'{author.LastName}', '{author.Biography}', " +
                                                               $"'{author.Publisher}')");

            //For now return true (success)
            return true;
        }

        public async Task AddBookToAuthor(int author_id, string isbn)
        {

            var results = await _connection.QueryAsync($"INSERT INTO public.author_book (author_id, book_isbn) " +
                                                       $"SELECT a.id, b.isbn " +
                                                       $"FROM author a, book b " +
                                                       $"WHERE a.id = {author_id} and b.isbn = '{isbn}'");
        }

        public async Task<IEnumerable<AuthorBook>> GetBooksRelatedToAuthor(int Id)
        {
            //Get book from database
            var results = await _connection.QueryAsync<AuthorBook>($"SELECT * FROM public.author_book " +
                                                                   $"WHERE author_id = {Id}");
            return results;
        }
    }
}
