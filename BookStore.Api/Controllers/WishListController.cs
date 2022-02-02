using BookStore.Api.Interface;
using BookStore.Api.Models;
using BookStore.Api.Request;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WishListController : ControllerBase
    {
        private readonly IWishListRepository _wishListRepository;

        public WishListController (IWishListRepository wishListRepository)
        {
            _wishListRepository = wishListRepository;
        }

        [HttpGet()]
        public async Task<IEnumerable<WishList>> Get(string user_id)
        {
            var wishlist = await _wishListRepository.Get(user_id);
            return wishlist;
        }

        [HttpGet(template: "GetBooks")]
        public async Task<IEnumerable<WishListBook>> GetBooks(int wish_list_id)
        {
            var books = await _wishListRepository.GetBooks(wish_list_id);
            return books;
        }

        [HttpPost(template: "Create")]
        public async Task<IActionResult> Create([FromBody] CreateWishListRequest request)
        {
            await _wishListRepository.Create(request.User_Id, request.Name);
            return Ok();
        }

        [HttpPost(template: "AddBook")]
        public async Task AddBook([FromBody] AddBookToWishListRequest request)
        {
            await _wishListRepository.AddBook(request.Wish_List_Id, request.Book_Id);
        }
    }
}
