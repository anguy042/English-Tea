namespace BookStore.Api.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int user_id { get; set; }
        public string book_id { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
