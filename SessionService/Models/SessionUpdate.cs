namespace SessionService.Models
{
    public class SessionUpdate
    {
        public string? Email { get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public DateTime? Login { get; set; }
        public DateTime? Logout { get; set; }
    }
}
