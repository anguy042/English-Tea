using BookStore.Api.Interface;
using BookStore.Api.Models;
using BookStore.Api.Response;
using BookStore.Sample.Function.Response;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookRepository _bookRepository;

        public BookController(ILogger<BookController> logger,
            IBookRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        [HttpGet(Name = "Get")]
        public async Task<GetBookResponse> Get(string isbn)
        {
            _logger.LogInformation($"Get book isbn: {isbn}");

            var book = await _bookRepository.GetBook(isbn);

            var response = new GetBookResponse { Book = book };
            return response;
        }

        [HttpPost(Name = "Add")]
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