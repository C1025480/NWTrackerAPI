using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NWTrackerAPI.Data;
using NWTrackerAPI.Models;
using NWTrackerAPI.Processors.Interfaces;

namespace NWTrackerAPI.Processors
{
    public class GetUser : IGetUser
    {
        public readonly APIContext context;
        IValidatePassword validatePassword;

        public GetUser(APIContext context, IValidatePassword validatePassword)
        {
            this.context = context;
            this.validatePassword = validatePassword;
        }

        public LOG_LOGIN Validate(APIContext context, string username, string password)
        {
            LOG_LOGIN User = context.LOG_LOGINS.FirstOrDefault(x => x.LOG_USERNAME == username);

            if (User != null)
            {
                string storedhash = User.LOG_HASHED_PASSWORD;
                string storedSalt = User.LOG_SALT;

                bool isPasswordValid = validatePassword.Validator(password, storedhash, storedSalt);
                if (isPasswordValid)
                {
                    return User;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Couldn't find user associated with username");
                return null;
            }
        }
    }
}
