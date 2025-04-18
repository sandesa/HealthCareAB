namespace UserService.Models
{
    public class ValidationResponse
    {
        public string? Email { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? Expires { get; set; }
    }
}
