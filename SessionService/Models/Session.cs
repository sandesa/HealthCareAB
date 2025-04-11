namespace SessionService.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public bool? OnlineStatus { get; set; }
        public DateTime? Login { get; set; }
        public DateTime? Logout { get; set; }
    }
}
