using System.Security.Cryptography;

namespace InsuranceUsers.Utilities
{
    public class PasswordManager : IPasswordManager
    {
        private static int keySize = 256;
        private static int iterations = 1000;
        private static int saltSize = 16;

        public (string hash, string salt) HashPassword(string password)
        {
            byte[] salt = new byte[saltSize];
            RandomNumberGenerator.Fill(salt);
            Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(password, salt, iterations);
            return (Convert.ToBase64String(k1.GetBytes(keySize)), Convert.ToBase64String(salt));
        }

        public bool VerifyPassword(string password, string hash, string salt)
        {
            byte[] bytes = Convert.FromBase64String(salt);
            Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(password, bytes, iterations);
            if (Convert.ToBase64String(k1.GetBytes(keySize)) == hash)
            {
                return true;
            }
            return false;
        }
    }
}
