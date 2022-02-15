namespace BookStore.Api.Models
{
    public class Cart
    {

        public int? Cart_Id { get; set; }
        public int? User_Id { get; set; }

        public double? Price { get; set; }
        public int? Quantity { get; set; }
     
       

    }
}
