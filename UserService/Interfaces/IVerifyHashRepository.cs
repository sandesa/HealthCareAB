namespace UserService.Interfaces
{
    public interface IVerifyHashRepository
    {
        bool VerifyPassword(string password, string passwordHash);
        bool CompareByteArrays(byte[] a, byte[] b);
    }
}
