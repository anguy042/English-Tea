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
        
        //request to remove a book from a shopping cart, return bool? 
       
        [HttpPost(template: "RemoveBookFromCart/")]
        [ProducesResponseType(200)] // how to check if it actually deleted book, or if it doesnt exist, also check if person has a shopping cart 
        public async Task<bool> Remove(string isbn, int user_id)
        {
            // var cart = await _cartRepository.Get(user_id);

            await _cartRepository.Remove( isbn, user_id);
            return false;
           
        }




    }
}
