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

        public async Task<IEnumerable<WishList>> GetByUserId(int userId)
        {
            var results = await _connection.QueryAsync<WishList>($"SELECT * FROM public.wish_list " +
                                                                 $"WHERE user_id = '{userId}'");
            return results;
        }

        public async Task<WishList> GetByWishListId(int wishListId)
        {
            var results = await _connection.QueryFirstOrDefaultAsync<WishList>($"SELECT * FROM public.wish_list " +
                                                                               $"WHERE id = '{wishListId}'");
            return results;
        }

        public async Task<IEnumerable<WishListBook>> GetBooks(int wishListId)
        {
            var results = await _connection.QueryAsync<WishListBook>($"SELECT book_id as Isbn FROM public.wish_list_book " +
                                                                     $"WHERE wishlist_id = {wishListId}");
            return results;
        }

        public async Task<int> GetWishListCount(int userId)
        {
            return await _connection.QueryFirstOrDefaultAsync<int>($"SELECT COUNT(1) FROM public.wish_list " +
                                                                   $"WHERE user_id = {userId}");
        }

        public async Task<WishList> Create(int user_id, string name)
        {
            return await _connection.QueryFirstOrDefaultAsync($"INSERT INTO public.wish_list " +
                                                              $"VALUES(DEFAULT, {user_id}, '{name}')");
        }

        public async Task MoveToCart(int wishListId, int userId, string isbn)
        {
            await _connection.QueryAsync($"INSERT INTO public.cart " +
                                         $"VALUES(DEFAULT, {userId}, '{isbn}', 1)");
        }

        public async Task RemoveBookFromList(int wishListId, string isbn)
        {
            await _connection.QueryAsync($"DELETE FROM public.wish_list_book " +
                             $"WHERE wishlist_id = '{wishListId}' AND book_id = '{isbn}'");
        }

        public async Task AddBook(int wishListId, string book_id)
        {
            await _connection.QueryAsync($"INSERT INTO public.wish_list_book " +
                                         $"VALUES({wishListId}, {book_id})");
        }
    }
}
