namespace LoginService.Models
{
    public class ValidationResponse
    {
        public string? Token { get; set; }
        public string? Message { get; set; }
        public bool IsValid { get; set; }
        public bool IsConnectedToService { get; set; }
    }
}
