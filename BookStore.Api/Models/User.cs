using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Models
{
    public class User
    {
        public int? id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string? username { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string? password { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string? name { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string? email { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string? home_address { get; set; }
    }
}
