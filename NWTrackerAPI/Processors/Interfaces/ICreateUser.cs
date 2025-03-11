using NWTrackerAPI.Data;
using NWTrackerAPI.Models;

namespace NWTrackerAPI.Processors.Interfaces
{
    public interface ICreateUser
    {
        bool create(APIContext context, LOG_LOGIN inputtedUser);
    }
}