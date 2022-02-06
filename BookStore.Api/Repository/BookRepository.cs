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

            //For now return true (success)
            return true;
        }

        public async Task<List<Book>?> GetBooks(string isbn, string genre, string sortBy, string orderBy, int offset, int limit)
        {
            //Get book from database
            StringBuilder queryBuilder = new StringBuilder("SELECT * FROM book b ");
            StringBuilder filterBuilder = new StringBuilder();
            
            Dictionary<string, string> filter = new Dictionary<string, string>();
            filter.Add("isbn", isbn);
            filter.Add("genre", genre);
            foreach (KeyValuePair<string, string> entry in filter)
            {
                if(entry.Value != null)
                {
                    if(filterBuilder.Length == 0)
                    {
                        filterBuilder.Append("WHERE ");
                    }
                    else
                    {
                        filterBuilder.Append("AND ");
                    }
                    if(!String.IsNullOrEmpty(entry.Value))
                    {
                        filterBuilder.Append($"{entry.Key} = '{entry.Value}' ");
                    }
                }
            }

            

            StringBuilder sortByStringBuilder = new StringBuilder();
            if (sortBy != null)
            {
                sortByStringBuilder.Append($"ORDER BY {sortBy} ");
                if (String.IsNullOrEmpty(orderBy))
                {
                    sortByStringBuilder.Append("ASC ");
                } 
                else
                {
                    sortByStringBuilder.Append($"{orderBy} ");
                }
            }

            StringBuilder offsetBuilder = new StringBuilder();
            if(offset != 0)
            {
                offsetBuilder.Append($"OFFSET {offset} ");
            }

            queryBuilder.Append(filterBuilder).Append(sortByStringBuilder).Append(offsetBuilder);
            if (limit != 0)
            {
                queryBuilder.Append($"LIMIT {limit} ");
            }

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
