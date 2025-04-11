namespace LoginService.Models
{
    public class LoginResponse
    {
        public string? Token { get; set; }
        public string? Message { get; set; }
        public bool IsConnectedToService { get; set; }
        public bool IsLoginSuccessful { get; set; }
    }
}
