using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Request
{
    public class CreateCardRequest
    {
        [Required(AllowEmptyStrings = false)]
        public int user_id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string name { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string number { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string expire_date { get; set; }
        [Required(AllowEmptyStrings = false)]
        public int pin { get; set; }
    }
}
