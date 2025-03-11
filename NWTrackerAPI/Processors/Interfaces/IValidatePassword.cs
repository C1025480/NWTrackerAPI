namespace NWTrackerAPI.Processors.Interfaces
{
    public interface IValidatePassword
    {
        bool Validator(string enteredPassword, string storedHashedPassword, string storedSalt);
    }
}