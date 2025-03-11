using NWTrackerAPI.Data;
using NWTrackerAPI.Models;

namespace NWTrackerAPI.Processors.Interfaces
{
    public interface IGetUser
    {
        LOG_LOGIN Validate(APIContext context, string username, string password);
    }
}