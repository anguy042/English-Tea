using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Models
{
    public class CreditCard
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string name { get; set; }
        public string number{ get; set; }
        public string expire_date { get; set; }
        public int pin { get; set; }
    }
}
