using NWTrackerAPI.Data;
using NWTrackerAPI.Models;

namespace NWTrackerAPI.Processors.Interfaces
{
    public interface ICreateProject
    {
        bool create(APIContext context, Project inputtedProjectName);
    }
}