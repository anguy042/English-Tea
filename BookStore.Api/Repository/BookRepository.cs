using BookStore.Api.Interface;
using BookStore.Api.Models;
using Dapper;
using Npgsql;

namespace BookStore.Api.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly NpgsqlConnection _connection;

        public BookRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> AddBook(Book book)
        {
            var results = await _connection.QueryAsync<Book>($"INSERT INTO Book " +
                                                             $"VALUES('{book.Isbn}', '{book.Name}');");

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
    }
}
