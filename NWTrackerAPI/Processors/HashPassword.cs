using NWTrackerAPI.Data;
using NWTrackerAPI.Models;
using NWTrackerAPI.Processors.Interfaces;
using System.Security.Cryptography;
using System;
using System.Text;

namespace NWTrackerAPI.Processors
{
    public class HashPassword : IHashPassword
    {
        public readonly APIContext context;

        public HashPassword(APIContext context)
        {
            this.context = context;
        }
        public (string hash, string salt) PasswordHasher(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] saltBytes = new byte[16];
                rng.GetBytes(saltBytes);
                string salt = Convert.ToBase64String(saltBytes);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000))
                {
                    byte[] hash = pbkdf2.GetBytes(32);
                    return (Convert.ToBase64String(hash), salt);
                }
            }
        }
    }
}
