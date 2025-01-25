using NWTrackerAPI.Data;
using NWTrackerAPI.Models;
using NWTrackerAPI.Processors.Interfaces;

namespace NWTrackerAPI.Processors
{
    public class DeleteProject : IDeleteProject
    {
        public readonly APIContext context;

        public DeleteProject(APIContext context)
        {
            this.context = context;
        }

        public bool delete(APIContext context, Project inputtedProject)
        {
            bool Exists = context.Projects.Any(x => x.NW_PK == inputtedProject.NW_PK);

            if (Exists == true)
            {
                context.Projects.Remove(inputtedProject);
                context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
