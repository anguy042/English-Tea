using Microsoft.AspNetCore.Mvc;
using BookStore.Api.Interface;
using BookStore.Api.Models;
using BookStore.Api.Request;

namespace BookStore.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly Repository.ICartRepository _cartRepository;

        public ShoppingCartController(Repository.ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet(Name = "GetCart/")]
        public async Task<IEnumerable<Cart?>> Get(string user_id) //make this function return a list of cart, refer to bookrepo code 
        {
           var cart = await _cartRepository.Get(user_id); 

            return cart;
        }

        [HttpPost(Name = "CreateCart/")]
        public async Task<bool> Create([FromBody] CreateShoppingCartRequest request )
        {
            // var cart = await _cartRepository.Get(user_id);

           await _cartRepository.Create(request.User_Id, request.Isbn, request.Quantity);
            return false;
        }


    }
}
