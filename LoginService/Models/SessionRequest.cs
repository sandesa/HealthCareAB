namespace LoginService.Models
{
    public class SessionRequest
    {
        public string? Email { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? Expires { get; set; }
    }
}
