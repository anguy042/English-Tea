using BookStore.Api.Models;

namespace BookStore.Api.Response
{
    public class GetWishListsResponse : ApiBaseResponse
    {
        public IEnumerable<WishList> WishLists { get; set; }
    }
}
