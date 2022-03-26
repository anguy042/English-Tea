using BookStore.Api.Models;

namespace BookStore.Api.Repository
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart?>> Get(string user_id);

        Task Create(int user_id, string Isbn, int quantity);
         Task Remove(string Isbn, int user_id);
    }
}