using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BookStore.Sample.Function.Request;
using BookStore.Sample.Function.Response;
using BookStore.Sample.Function.Interface;
using BookStore.Sample.Function.Models;

namespace BookStore.Sample.Function
{
    public class PostBookFunction
    {
        private readonly IBookRepository _bookRepository;

        public PostBookFunction(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [FunctionName("AddBook")]
        public async Task<IActionResult> AddBook(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "book/add")] HttpRequest req,
            ILogger log)
        {
            //Deserialize the request
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var request = JsonConvert.DeserializeObject<AddBookRequest>(requestBody);

            //Map the request to a book model
            var newBook = new Book { Isbn = request.Isbn, Name = request.Name };

            log.LogInformation($"Attempting to add book");
            log.LogInformation($"With parameters: \n {requestBody}");

            //Try to add the book to the database
            var isSuccess = await _bookRepository.AddBook(newBook);

            //Create the response
            var response = new AddBookResponse
            {
                Message = isSuccess ? "Success" : "Failed" //this is just a fancy if statement
            };

            return new OkObjectResult(response);
        }
    }
}
