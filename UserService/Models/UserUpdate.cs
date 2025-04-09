namespace UserService.Models
{
    public class UserUpdate
    {
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public List<string>? UserType { get; set; }
        public List<string>? UserAccountType { get; set; }
    }
}
