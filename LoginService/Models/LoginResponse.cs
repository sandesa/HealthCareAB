namespace LoginService.Models
{
    public class LoginResponse
    {
        public bool IsConnectedToService { get; set; }
        public bool IsLoginSuccessful { get; set; }
        public string? Message { get; set; }
    }
}
