using mandiri_project.Interfaces;
using System.Security.Claims;
using System.Security.Cryptography;

namespace mandiri_project.Services
{
    public class ApplicationUserService : IApplicationUser
    {      

        public ApplicationUserService()
        {
            
        }

        public string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32); // 32 bytes for a 256-bit key
                byte[] combinedBytes = new byte[salt.Length + hash.Length];
                Array.Copy(salt, 0, combinedBytes, 0, salt.Length);
                Array.Copy(hash, 0, combinedBytes, salt.Length, hash.Length);
                return Convert.ToBase64String(combinedBytes);
            }
        }

        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            byte[] combinedBytes = Convert.FromBase64String(storedHash);
            byte[] salt = new byte[16];
            Array.Copy(combinedBytes, 0, salt, 0, salt.Length);

            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32); // 32 bytes for a 256-bit key
                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != combinedBytes[i + salt.Length])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }

}
