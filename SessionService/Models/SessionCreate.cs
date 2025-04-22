namespace SessionService.Models
{
    public class SessionCreate
    {
        public string? Email { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? Expires { get; set; }
    }
}
