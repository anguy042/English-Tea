using BookStore.Api.Models;
using System.Threading.Tasks;

namespace BookStore.Api.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User?>> Get(string username);
        Task<IEnumerable<CreditCard?>> GetCard(int user_id);
        Task Create(string username, string password, string name, string email, string home_address);
        Task Update(string username, string password, string name, string email, string home_address);
        Task CreateCard(int user_id, string name, string number, string expire_date, int pin);
    }
}