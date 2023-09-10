namespace InsuranceUsers.Utilities
{
    public interface IPasswordManager
    {
        (string hash, string salt) HashPassword(string password);
        bool VerifyPassword(string password, string hash, string salt);
    }
}
