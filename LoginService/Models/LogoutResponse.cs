namespace LoginService.Models
{
    public class LogoutResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool IsLogoutSuccessful { get; set; }
        public bool IsConnectedToService { get; set; }
    }
}
