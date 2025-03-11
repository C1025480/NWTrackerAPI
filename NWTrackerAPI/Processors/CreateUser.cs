using NWTrackerAPI.Data;
using NWTrackerAPI.Models;
using NWTrackerAPI.Processors.Interfaces;

namespace NWTrackerAPI.Processors
{
    public class CreateUser : ICreateUser
    {
        public readonly APIContext context;
        IHashPassword hashPassword;

        public CreateUser(APIContext context, IHashPassword hashPassword)
        {
            this.context = context;
            this.hashPassword = hashPassword;
        }

        public bool create(APIContext context, LOG_LOGIN inputtedUser)
        {
            var new_User = new LOG_LOGIN()
            {
                LOG_FIRSTNAME = inputtedUser.LOG_FIRSTNAME,
                LOG_SECONDNAME = inputtedUser.LOG_SECONDNAME,
                LOG_EMAILADDRESS = inputtedUser.LOG_EMAILADDRESS,
                LOG_USERNAME = inputtedUser.LOG_USERNAME,
                LOG_PHONENUMBER = inputtedUser.LOG_PHONENUMBER,
                LOG_HASHED_PASSWORD = inputtedUser.LOG_HASHED_PASSWORD
            };

            var password = inputtedUser.LOG_HASHED_PASSWORD;

            var result = hashPassword.PasswordHasher(password);

            new_User.LOG_HASHED_PASSWORD = result.hash;
            new_User.LOG_SALT = result.salt;

            context.LOG_LOGINS.Add(new_User);
            context.SaveChanges();

            return true;
        }
    }
}
