using BookStore.Api.Models;

namespace BookStore.Api.Response
{
    public class GetBooksResponse : ApiBaseResponse
    {
        public List<Book>? Books { get; set; }
    }
}
