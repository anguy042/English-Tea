using Npgsql;

namespace BookStore.Api.Repository
{
    public class RatingRepository
    {
        private readonly NpgsqlConnection _connection;

        public RatingRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }
    }
}
