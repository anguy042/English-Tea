using BookStore.Api.Models;

namespace BookStore.Api.Repository
{
    public interface ICartRepository
    {
        Task<Cart?> Get(string user_id);

        Task Create(int user_id, string Isbn, int quantity);
    }
}