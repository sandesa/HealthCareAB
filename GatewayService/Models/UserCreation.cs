namespace GatewayService.Models
{
    public class UserCreation
    {
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
