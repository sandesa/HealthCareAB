namespace SessionService.Models
{
    public class SessionCreate
    {
        public int? UserId { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? Expires { get; set; }
    }
}
