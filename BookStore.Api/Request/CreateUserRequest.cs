namespace BookStore.Api.Request
{
    public class CreateUserRequest
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? home_address { get; set; }

    }
}
