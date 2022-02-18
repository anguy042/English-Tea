namespace BookStore.Api.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int user_id { get; set; }
        public string book_id { get; set; }
        public int stars { get; set; }
        public string Comment { get; set; }
        public DateTime Timestamp { get; set; }
        public object Isbn { get; internal set; }
        public object Name { get; internal set; }
    }
}
