namespace UserService.Models
{
    public class ValidationRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
