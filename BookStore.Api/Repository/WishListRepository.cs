using BookStore.Api.Interface;
using BookStore.Api.Models;
using Dapper;
using Npgsql;

namespace BookStore.Api.Repository
{
    public class WishListRepository : IWishListRepository
    {
        private readonly NpgsqlConnection _connection;

        public WishListRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<WishList>> Get(string user_id)
        {
            var results = await _connection.QueryAsync<WishList>($"SELECT * FROM public.wish_list " +
                                                                 $"WHERE user_id = '{user_id}'");
            return results;
        }

        public async Task<IEnumerable<WishListBook>> GetBooks(int wish_list_id)
        {
            var results = await _connection.QueryAsync<WishListBook>($"SELECT book_id as Isbn FROM public.wish_list_book " +
                                                                     $"WHERE wishlist_id = {wish_list_id}");
            return results;
        }

        public async Task Create(int user_id, string name)
        {
            var results = await _connection.QueryAsync($"INSERT INTO public.wish_list " +
                                                       $"VALUES(DEFAULT, {user_id}, '{name}')");
        }

        public async Task AddBook(int wish_list_id, string book_id)
        {
            var results = await _connection.QueryAsync($"INSERT INTO public.wish_list_book " +
                                                       $"VALUES({wish_list_id}, {book_id})");
        }
    }
}
