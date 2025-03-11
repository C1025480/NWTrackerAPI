namespace NWTrackerAPI.Processors.Interfaces
{
    public interface IHashPassword
    {
        (string hash, string salt) PasswordHasher(string password);
    }
}