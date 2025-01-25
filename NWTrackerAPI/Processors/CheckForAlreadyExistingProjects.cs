using NWTrackerAPI.Data;
using NWTrackerAPI.Models;
using NWTrackerAPI.Processors.Interfaces;

namespace NWTrackerAPI.Processors
{
    public class CheckForAlreadyExistingProjects : ICheckForAlreadyExistingProjects
    {
        public readonly APIContext context;

        public CheckForAlreadyExistingProjects(APIContext context)
        {
            this.context = context;
        }

        public bool check(APIContext context, Project inputtedProjectName)
        {
            bool AlreadyExists = context.Projects.Any(x => x.ProjectName == inputtedProjectName.ProjectName);

            if (AlreadyExists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
