using BookStore.Api.Interface;
using BookStore.Api.Models;
using Dapper;
using Npgsql;

namespace BookStore.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly NpgsqlConnection _connection;

        public UserRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<User?>> Get(string username)
        {
            var results = await _connection.QueryAsync<User>($"SELECT * FROM public.user " +
                                                             $"WHERE username = '{username}'");

            return results;
        }

        public async Task<IEnumerable<CreditCard?>> GetCard(int user_id)
        {
            var results = await _connection.QueryAsync<CreditCard>($"SELECT * FROM public.credit_card " +
                                                                   $"WHERE user_id = '{user_id}'");
            return results;
        }

        public async Task Create(string username, string password, string email, string name, string home_address)
        {
            var results = await _connection.QueryAsync<User>($"INSERT INTO public.user " +
                                                             $"VALUES(DEFAULT, '{username}', '{password}', '{email}', '{name}', '{home_address}')");
        }

        public async Task Update(string username, string password, string email, string name, string home_address)
        {
            var results = await _connection.QueryAsync<User>($"UPDATE public.user " +
                                                             $"SET password = '{password}', name = '{name}', email = '{email}', home_address = '{home_address}' " +
                                                             $"WHERE username = '{username}';");
        }

        public async Task CreateCard(int user_id, string name, string number, string expire_date, int pin)
        {
            var results = await _connection.QueryAsync<CreditCard>($"INSERT INTO public.credit_card " +
                                                                   $"VALUES(DEFAULT, '{user_id}', '{name}', '{number}', '{expire_date}', '{pin}')");
        }
    }
}