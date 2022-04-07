namespace BookStore.Api.Models
{
    public class Book
    {
        public string? Isbn { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public string? Genre { get; set; }
        public string? Publisher { get; set; }
        public DateTime? Published_Date { get; set; }
        public int? Copies_Sold { get; set; }
        public string? Seller { get; set; }
    }
}
