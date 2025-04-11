namespace SessionService.Models
{
    public class Session
    {
        public int Id { get; set; }
        public required string Token { get; set; }
        public bool OnlineStatus { get; set; } = false;
        public DateTime? Login { get; set; }
        public DateTime? Logout { get; set; }
    }
}
