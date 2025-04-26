namespace LoginService.Models
{
    public class SessionRequest
    {
        public int? UserId { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? Expires { get; set; }
    }
}
