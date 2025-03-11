using NWTrackerAPI.Data;
using NWTrackerAPI.Models;
using NWTrackerAPI.Processors.Interfaces;
using System.Security.Cryptography;
using System;
using System.Text;

namespace NWTrackerAPI.Processors
{
    public class ValidatePassword : IValidatePassword
    {
        public readonly APIContext context;

        public ValidatePassword(APIContext context)
        {
            this.context = context;
        }
        public bool Validator(string enteredPassword, string storedHashedPassword, string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000))
            {
                byte[] enteredHash = pbkdf2.GetBytes(32);

                string enteredHashString = Convert.ToBase64String(enteredHash);
                return enteredHashString == storedHashedPassword;
            }
        }
    }
}
