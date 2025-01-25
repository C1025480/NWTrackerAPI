using NWTrackerAPI.Data;
using NWTrackerAPI.Models;

namespace NWTrackerAPI.Processors.Interfaces
{
    public interface ICheckForAlreadyExistingProjects
    {
        bool check(APIContext context, Project inputtedProjectName);
    }
}