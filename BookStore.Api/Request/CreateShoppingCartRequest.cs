namespace BookStore.Api.Request
{
    public class CreateShoppingCartRequest
    {
        public int User_Id { get; set; }
        public string Isbn { get; set; }

        public int Quantity { get; set; }

    }
}
