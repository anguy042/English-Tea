using BookStore.Api.Interface;
using BookStore.Api.Models;
using BookStore.Api.Request;
using BookStore.Api.Response;
using BookStore.Sample.Function.Response;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WishListController : ControllerBase
    {
        private readonly ILogger<WishListController> _logger;
        private readonly IWishListRepository _wishListRepository;
        private readonly IBookRepository _bookRepository;

        public WishListController (ILogger<WishListController> logger, IWishListRepository wishListRepository,
            IBookRepository bookRepository)
        {
            _logger = logger;
            _wishListRepository = wishListRepository;
            _bookRepository = bookRepository;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(GetWishListsResponse), 200)]
        [ProducesResponseType(typeof(GetWishListsResponse), 404)]
        public async Task<IActionResult> Get(int userId)
        {
            _logger.LogInformation($"Getting wish lists for user: {userId}");

            var response = new GetWishListsResponse();
            var wishlists = await _wishListRepository.GetByUserId(userId);

            if (wishlists.Count() == 0)
            {
                response.Errors.Add("NotFound", "There is no wish list for this user.");
                return NotFound(response);
            }

            response.WishLists = wishlists;
            return new OkObjectResult(response);
        }

        [HttpGet(template: "GetBooks")]
        [ProducesResponseType(typeof(GetWishListBooksResponse), 200)]
        [ProducesResponseType(typeof(GetWishListBooksResponse), 404)]
        public async Task<IActionResult> GetBooks(int wishListId)
        {
            _logger.LogInformation($"Getting books for wish list id: {wishListId}");

            var response = new GetWishListBooksResponse();

            var books = await _wishListRepository.GetBooks(wishListId);
            if (books.Count() == 0)
            {
                response.Errors.Add("NotFound", $"There is no wish list with id {wishListId} or there are no books");
                return new NotFoundObjectResult(response);
            }

            response.Books = books;
            return new OkObjectResult(response);
        }

        [HttpPost(template: "Create")]
        [ProducesResponseType(typeof(WishList), 201)]
        [ProducesResponseType(typeof(CreateWishListResponse), 400)]
        [ProducesResponseType(typeof(CreateWishListResponse), 409)]
        public async Task<IActionResult> Create([FromBody] CreateWishListRequest request)
        {
            _logger.LogInformation($"Request to create wish list with parameters: ");
            _logger.LogInformation($"{JsonSerializer.Serialize(request)}");

            var response = new CreateWishListResponse();
            WishList createWishlist;

            try
            {
                //TODO: Make sure the user exists

                //Only allow 3 wishlists
                var totalWishlists = await _wishListRepository.GetWishListCount(request.UserId);
                if (totalWishlists > 3)
                {
                    var message = "You can only have up to 3 wishlist at a time";
                    _logger.LogInformation(message);

                    response.Errors.Add("Wishlist", message);
                    return new BadRequestObjectResult(response);
                }

                //Make sure there is not already a wishlist for this user with this name
                var wishlists = await _wishListRepository.GetByUserId(request.UserId);
                if(wishlists.Any(e => e.Name == request.Name))
                {
                    var message = "You already have a wishlist with this name.";
                    _logger.LogInformation($"Conflict: {message}");

                    response.Errors.Add("Conflict", message);
                    return new ConflictObjectResult(response);
                }

                createWishlist = await _wishListRepository.Create(request.UserId, request.Name);
            }
            catch (Exception e)
            {
                _logger.LogError(e.GetBaseException().Message);
                response.Errors.Add("Exception", $"{e.GetBaseException().Message}");
                return new ObjectResult(response);
            }

            return Created($"/wishList?user_id={request.UserId}", createWishlist);
        }

        [HttpPost(template: "AddToCart")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(AddFromWishListToCartResponse), 400)]
        public async Task<IActionResult> AddToCart([FromBody] WishlistAddToCartRequest request)
        {
            _logger.LogInformation($"Requesting to add {request.Isbn} to cart with parameters: ");
            _logger.LogInformation($"{JsonSerializer.Serialize(request)}");

            var response = new AddFromWishListToCartResponse();

            //Make sure the user and wishlist are valid
            var wishlists = await _wishListRepository.GetByUserId(request.UserId);
            if (wishlists.FirstOrDefault(e => e.Id == request.WishListId && e.User_Id == request.UserId) == null)
            {
                var message = $"Wish list id and/or user id do not corralate.";
                _logger.LogInformation($"BadRequest: {message}");

                response.Errors.Add("BadRequest", message);
                return new BadRequestObjectResult(response);
            }

            //Make sure that this request isbn in the wish list first
            var books = await _wishListRepository.GetBooks(request.WishListId);
            if (books.Where(e => e.Isbn == request.Isbn).Count() == 0)
            {
                var message = $"Wish list does not contain book {request.Isbn}";
                _logger.LogInformation($"BadRequest: {message}");

                response.Errors.Add("BadRequest", message);
                return new BadRequestObjectResult(response);
            }

            await _wishListRepository.MoveToCart(request.WishListId, request.UserId, request.Isbn);
            await _wishListRepository.RomoveBookFromList(request.WishListId, request.Isbn);

            return Ok();
        }

        [HttpPost(template: "AddBook")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(AddBookToWishListResponse), 400)]
        public async Task<IActionResult> AddBook([FromBody] AddBookToWishListRequest request)
        {
            _logger.LogInformation($"Requesting to add book {request.Isbn} to wish list {request.WishListId}");

            //Dont allow duplicate book for a wish list
            var books = await _wishListRepository.GetBooks(request.WishListId);
            if (books.Where(e => e.Isbn == request.Isbn).Count() > 0)
            {
                var message = $"Wish list already contains book {request.Isbn}";
                _logger.LogInformation($"BadRequest: {message}");

                var response = new AddBookToWishListResponse();
                response.Errors.Add("BadRequest", message);
                return new BadRequestObjectResult(response);
            }

            //Make sure wish list exist
            var wishlist = await _wishListRepository.GetByWishListId(request.WishListId);
            if (wishlist is null)
            {
                var message = $"Wish list with id {request.WishListId} that does not exist";
                _logger.LogInformation($"BadRequest: {message}");

                var response = new AddBookToWishListResponse();
                response.Errors.Add("BadRequest", message);
                return new BadRequestObjectResult(response);
            }

            //Make sure this book exist in the db
            var book = await _bookRepository.GetBook(request.Isbn);
            if (book is null)
            {
                var message = $"Failed to add book. {request.Isbn} does not exist in the database";
                _logger.LogInformation($"NotFound: {message}");

                var response = new AddBookToWishListResponse();
                response.Errors.Add("BadRequest", message);
                return new BadRequestObjectResult(response);
            }

            await _wishListRepository.AddBook(request.WishListId, request.Isbn);
            
            return Ok();
        }
    }
}