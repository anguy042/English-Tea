using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Request
{
    public class CreateWishListRequest
    {
        [Required(AllowEmptyStrings = false)] 
        public int UserId { get; set; }

        [Required(AllowEmptyStrings = false)] 
        public string Name { get; set; }
    }
}
