namespace BookStore.Api.Models
{
    public class Rating
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string isbn{ get; set; }
        public int stars { get; set; }
        public string comment { get; set; }
        public DateTime timestamp { get; set; }
       
   
    }
}
