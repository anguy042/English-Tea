using BookStore.Api.Models;

namespace BookStore.Api.Response
{
    public class GetBookResponse : ApiBaseResponse
    {
        public Book? Book { get; set; }
    }
}
