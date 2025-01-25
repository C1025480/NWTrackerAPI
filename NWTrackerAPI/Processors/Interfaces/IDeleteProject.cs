using NWTrackerAPI.Data;
using NWTrackerAPI.Models;

namespace NWTrackerAPI.Processors.Interfaces
{
    public interface IDeleteProject
    {
        bool delete(APIContext context, Project inputtedProject);
    }
}