namespace LoginService.Models
{
    public class LoginResponse
    {
        public string? Email { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? Expires { get; set; }
        public string? Message { get; set; }
        public bool IsConnectedToService { get; set; }
        public bool IsLoginSuccessful { get; set; }
    }
}
