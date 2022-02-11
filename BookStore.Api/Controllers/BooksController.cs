using BookStore.Api.Interface;
using BookStore.Api.Models;
using BookStore.Api.Response;
using BookStore.Sample.Function.Response;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookRepository _bookRepository;

        public BooksController(ILogger<BooksController> logger,
            IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        [HttpGet(Name = "GetBooks")]
        public async Task<GetBooksResponse> Get(string? isbn, string? genre, string? sortBy, string? orderBy, int offset, int limit)
        {
            if(!String.IsNullOrEmpty(isbn))
            {
                _logger.LogInformation($"Get book isbn: {isbn}");
            }

            var books = await _bookRepository.GetBooks(isbn, genre, sortBy, orderBy, offset, limit);
            var response = new GetBooksResponse { Books = books };
            return response;
        }

        [HttpGet]
        [Route("rating")]
        public async Task<GetBooksResponse> GetByRating(int stars = 0)
        {
            var books = await _bookRepository.GetBooksByRating(stars);
            var response = new GetBooksResponse { Books = books };
            return response;
        }

        [HttpPost(Name = "AddBook")]
        public async Task<AddBookResponse> Add([FromBody] Book book)
        {
            _logger.LogInformation($"Attempting to add book");

            var isSuccess = await _bookRepository.AddBook(book);

            //Create the response
            var response = new AddBookResponse
            {
                Message = isSuccess ? "Success" : "Failed" //this is just a fancy if statement
            };

            return response;
        }
    }
}