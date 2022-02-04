using BookStore.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;

        public RatingController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<Rating> Get(string isbn)
        {

        }
    }
}
