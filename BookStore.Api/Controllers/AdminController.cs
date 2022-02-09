using BookStore.Api.Interface;
using BookStore.Api.Models;
using BookStore.Api.Request;
using BookStore.Api.Response;
using BookStore.Sample.Function.Response;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminRepository _bookRepository;

        public AdminController(ILogger<AdminController> logger,
            IAdminRepository bookRepository)
        {
            _logger = logger;
            _bookRepository = bookRepository;
        }

        //Endpoint to get book from book table
        [HttpGet(template: "GetBook")]
        public async Task<GetBookResponse> Get(string isbn)
        {
            _logger.LogInformation($"Get book isbn: {isbn}");

            var book = await _bookRepository.GetBook(isbn);

            var response = new GetBookResponse { Book = book };
            return response;
        }

        //Endpoint to add a new book to book table
        [HttpPost(template: "AddNewBook")]
        public async Task<AddBookResponse> Add([FromBody] Book book)
        {
            _logger.LogInformation($"Attempting to add book");

            var isSuccess = await _bookRepository.AddNewBook(book);

            //Create the response
            var response = new AddBookResponse
            {
                Message = isSuccess ? "Success" : "Failed" //this is just a fancy if statement
            };

            return response;
        }

        //Endpoint to add a new author to author table
        [HttpPost(template: "AddNewAuthor")]
        public async Task<AddAuthorResponse> Add([FromBody] Author author)
        {
            _logger.LogInformation($"Attempting to add author");

            var isSuccess = await _bookRepository.AddNewAuthor(author);

            //Create the response
            var response = new AddAuthorResponse
            {
                Message = isSuccess ? "Success" : "Failed" //this is just a fancy if statement
            };

            return response;
        }

        //Endpoint to add a new book to an author to book_author table.
        [HttpPost(template: "AddBookToAuthor")]
        public async Task Add([FromBody] AddBookToAuthorRequest request)
        {
            await _bookRepository.AddBookToAuthor(request.Author_id, request.Isbn);
        }

        //Endpoint to get books written by a given author
        [HttpGet(template: "GetBooksRelatedToAuthor")]
        public async Task<IEnumerable<AuthorBook>> Get(int id)
        {
            var books = await _bookRepository.GetBooksRelatedToAuthor(id);
            return books;
        }
    }
}