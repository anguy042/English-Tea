using BookStore.Api.Interface;
using BookStore.Api.Models;
using Dapper;
using Npgsql;
using System.Text;

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

           
            return true;
        }

        public async Task<List<Book>?> GetBooks(string? isbn, string? genre, string? sortBy, string? orderBy, int offset, int limit)
        {
            //Get book from database
            StringBuilder queryBuilder = new StringBuilder("SELECT * FROM book b ");
            var filters = new Dictionary<string, string>
            {
                { "isbn", isbn },
                { "genre", genre }
            };

            StringBuilder filterBuilder = new StringBuilder();
            if (isbn != null || genre != null)
                filterBuilder.Append("WHERE ");

            var firstWhereCondition = true;
            foreach (var filter in filters.Where(e => e.Value != null))
            {
                if (firstWhereCondition == false)
                    filterBuilder.Append("AND ");

                filterBuilder.Append($"{filter.Key} = '{filter.Value}' ");
                firstWhereCondition = false;
            }

            StringBuilder sortByStringBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(sortBy))
            {
                sortByStringBuilder.Append($"ORDER BY {sortBy} ");

                if (string.IsNullOrEmpty(orderBy))
                    sortByStringBuilder.Append("ASC ");
                else
                    sortByStringBuilder.Append($"{orderBy} ");
            }

            StringBuilder offsetBuilder = new StringBuilder();
            if (offset != 0)
                offsetBuilder.Append($"OFFSET {offset} ");

            queryBuilder.Append(filterBuilder).Append(sortByStringBuilder).Append(offsetBuilder);

            if (limit != 0)
                queryBuilder.Append($"LIMIT {limit} ");

            var results = await _connection.QueryAsync<Book>(queryBuilder.ToString());
            return results.ToList();
        }

        public async Task<List<Book>?> GetBooksByRating(decimal stars)
        {
            var results = await _connection.QueryAsync<Book>(
                "SELECT b.*, AVG(r.stars) as rating FROM book b " +
                "JOIN RATING r ON r.isbn = b.isbn GROUP BY b.isbn " +
                $"HAVING AVG(r.stars) >= {stars} " +
                "ORDER BY rating ASC "
                );
            return results.ToList();
        }
    }
}
