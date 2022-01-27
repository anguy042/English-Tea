using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using BookStore.Sample.Function.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace BookStore.Sample.Function
{
    public class GetBookFunction
    {
        private readonly IBookRepository _bookRepository;

        public GetBookFunction(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [FunctionName("GetBook")]
        public async Task<IActionResult> GetBook(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "book/{isbn}")] HttpRequest req,
            string isbn,
            ILogger log)
        {
            log.LogInformation($"Get book isbn: {isbn}");

            var book = await _bookRepository.GetBook(isbn);

            return new OkObjectResult(book);
        }
    }
}
