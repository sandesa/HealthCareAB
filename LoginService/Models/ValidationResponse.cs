namespace LoginService.Models
{
    public class ValidationResponse
    {
        public string? Email { get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string? Message { get; set; }
        public bool? IsConnectedToService { get; set; }
        public bool? IsValid { get; set; }
    }
}
