using NWTrackerAPI.Data;
using NWTrackerAPI.Models;
using NWTrackerAPI.Processors.Interfaces;

namespace NWTrackerAPI.Processors
{
    public class CreateProject : ICreateProject
    {
        public readonly APIContext context;

        public CreateProject(APIContext context)
        {
            this.context = context;
        }

        public bool create(APIContext context, Project inputtedProjectName)
        {
            var new_project = new Project()
            {
                ProjectName = inputtedProjectName.ProjectName
            };

            context.Projects.Add(new_project);
            context.SaveChanges();

            return true;
        }
    }
}
