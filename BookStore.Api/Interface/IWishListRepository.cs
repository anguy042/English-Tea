using BookStore.Api.Models;

namespace BookStore.Api.Interface
{
    public interface IWishListRepository
    {
        Task<IEnumerable<WishList>> GetByUserId(int user_id);
        Task<WishList> GetByWishListId(int user_id);
        Task<IEnumerable<WishListBook>> GetBooks(int wish_list_id);
        Task<int> GetWishListCount(int userId);
        Task<WishList> Create(int user_id, string name);
        Task MoveToCart(int wishListId, int user_id, string isbn);
        Task RemoveBookFromList(int wishListId, string isbn);
        Task AddBook(int wish_list_id, string book_id);
    }
}
