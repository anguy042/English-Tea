namespace BookStore.Api.Models
{
    public class Cart
    {

        public int? Id { get; set; }
        public int? User_Id { get; set; }

        public string? Book_Isbn { get; set; }
        public int? Quantity { get; set; }
     
       

    }
}
