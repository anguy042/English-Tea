using BookStore.Api.Models;

namespace BookStore.Api.Interface
{
    public interface IWishListRepository
    {
        Task<IEnumerable<WishList>> Get(string user_id);
        Task<IEnumerable<WishListBook>> GetBooks(int wish_list_id);
        Task Create(int user_id, string name);
        Task AddBook(int wish_list_id, string book_id);
    }
}