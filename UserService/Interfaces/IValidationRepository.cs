namespace UserService.Interfaces
{
    public interface IValidationRepository
    {
        bool VerifyPassword(string password, string passwordHash);
        bool CompareByteArrays(byte[] a, byte[] b);
    }
}
