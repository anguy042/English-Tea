using BookStore.Api.Models;
using Dapper;
using Npgsql;

namespace BookStore.Api.Repository
{

    public class CartRepository : ICartRepository
    {

        private readonly NpgsqlConnection _connection;

        public CartRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }


        public async Task<Cart?> Get(string user_id)
        {
            var results = await _connection.QueryAsync<Cart>($"SELECT * FROM public.cart " +
                                                             $"WHERE user_id = '{user_id}'");
            Console.WriteLine("This is C#");
            return results.FirstOrDefault();
        }


    }
}
