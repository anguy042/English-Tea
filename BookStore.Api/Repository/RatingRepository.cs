using BookStore.Api.Interface;
using BookStore.Api.Models;
using Dapper;
using Npgsql;

namespace BookStore.Api.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly NpgsqlConnection _connection;

        public RatingRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public Task<bool> AddRating(Rating rating)
        {
            throw new NotImplementedException();
        }

        public async Task Create(string isbn, int user_id, int stars, string comment)
        {
            var results = await _connection.QueryAsync($"INSERT INTO rating VALUES " +
                                                        $"(Default, {user_id}, '{isbn}', {stars},'{comment}', " +
                                                        $"CURRENT_TIMESTAMP)");
        }

        public async Task<IEnumerable<RatingSummary>> GetByIsbn(string isbn)
        {
            var results = await _connection.QueryAsync<RatingSummary>($"SELECT stars, comment FROM public.rating " +
                                                       $"WHERE isbn='{isbn}' " +
                                                       $"ORDER BY stars DESC");
            return results;
        }

        public async Task<int> GetAverage(string isbn)
        {
            var results = await _connection.QueryAsync<int>($"SELECT AVG(stars) FROM public.rating " +
                                                       $"WHERE isbn='{isbn}'");

            return results.FirstOrDefault();
        }


    }
}
