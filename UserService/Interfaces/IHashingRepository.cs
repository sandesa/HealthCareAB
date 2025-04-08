namespace UserService.Interfaces
{
    public interface IHashingRepository
    {
        string HashPassword(string password);
    }
}
