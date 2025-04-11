namespace SessionService.Models
{
    public class SessionUpdate
    {
        public string? Token { get; set; }
        public bool? OnlineStatus { get; set; }
        public DateTime? Login { get; set; }
        public DateTime? Logout { get; set; }
    }
}
