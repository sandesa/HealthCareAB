namespace SessionService.Models
{
    public class SessionUpdate
    {
        public int? UserId { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? Expires { get; set; }
        public DateTime? Login { get; set; }
        public DateTime? Logout { get; set; }
    }
}
