namespace BookStore.Api.Response
{
    public class ApiBaseResponse
    {
        public Dictionary<string, string> Errors { get; set; } = new Dictionary<string, string>();
    }
}