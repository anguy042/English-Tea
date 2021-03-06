using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Request
{
    public class AddBookToWishListRequest
    {
        [Required(AllowEmptyStrings = false)] 
        public int WishListId { get; set; }
        
        [Required(AllowEmptyStrings = false), StringLength(13, MinimumLength = 13)] 
        public string Isbn { get; set; }
    }
}
