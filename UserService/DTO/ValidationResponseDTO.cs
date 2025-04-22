namespace UserService.DTO
{
    public class ValidationResponseDTO
    {
        public int UserId { get; set; }
        public string? UserAccountType { get; set; }
        public string? Email { get; set; }
        public string? UserType { get; set; }
        public bool IsValid { get; set; } = false;
    }
}
