namespace LoginService.Models
{
    public class LoginResponse
    {
        public int? UserId { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? Expires { get; set; }
        public string? Message { get; set; }
        public bool IsConnectedToService { get; set; }
        public bool IsLoginSuccessful { get; set; }
    }
}
