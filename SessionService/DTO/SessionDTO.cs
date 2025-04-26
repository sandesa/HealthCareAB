namespace SessionService.DTO
{
    public class SessionDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? Login { get; set; }
        public DateTime? Logout { get; set; }
    }
}
