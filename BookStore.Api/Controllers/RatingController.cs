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


        [HttpPost(template: "CreateRating/")]
        public async Task<IActionResult> Create(string isbn, int user_id, int stars, string comment)
        {
            await ratingRespository.Create(isbn, user_id, stars, comment);
     
            return Ok();
        }
        
        [HttpGet(template: "RetrieveRatings/")]
        public async Task<IActionResult> GetByIsbn(string isbn)
        {
            var rating = await ratingRespository.GetByIsbn(isbn);

            return Ok(rating);

        }

        [HttpGet(template: "RetrieveAverageRating/")]
        public async Task<IActionResult> GetAverage(string isbn)
        {
           var average = await ratingRespository.GetAverage(isbn);

            return Ok(new {average = average});

        }

    }
}


