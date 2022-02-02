namespace BookStore.Api.Request
{
    public class AddBookToWishListRequest
    {
        public int Wish_List_Id { get; set; }
        public string Book_Id { get; set; }
    }
}
