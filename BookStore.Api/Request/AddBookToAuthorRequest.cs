namespace BookStore.Api.Request
{
    public class AddBookToAuthorRequest
    {
        public int Author_id { get; set; }
        public string Isbn { get; set; }
    }
}
