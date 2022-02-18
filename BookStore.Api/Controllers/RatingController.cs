using BookStore.Api.Interface;
using BookStore.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository ratingRespository;
        private readonly ILogger<RatingController> logger;
       

        public RatingController(ILogger<RatingController> logger, IRatingRepository ratingRepository)
        {
            logger = logger;
            ratingRespository = ratingRepository;
        }


        [HttpPost(Name = "CreateRating/")]
        public async Task<IActionResult> Create(string isbn, int user_id, int stars, string comment)
        {
            await ratingRespository.Create(isbn, user_id, stars, comment);
     
            return Ok();
        }
        
    }
}


