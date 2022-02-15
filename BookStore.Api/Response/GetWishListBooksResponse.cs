using BookStore.Api.Models;

namespace BookStore.Api.Response
{
    public class GetWishListBooksResponse : ApiBaseResponse
    {
        public IEnumerable<WishListBook> Books { get; set; }
    }
}
