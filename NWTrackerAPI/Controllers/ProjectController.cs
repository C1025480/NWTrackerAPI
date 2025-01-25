using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NWTrackerAPI.Data;
using NWTrackerAPI.Models;
using NWTrackerAPI.Processors.Interfaces;

namespace NWTrackerAPI.Controllers
{
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]/[action]")]
    public class ProjectController : ControllerBase
    {
        public readonly APIContext context;
        ICreateProject createProject;
        ICheckForAlreadyExistingProjects checkForAlreadyExistingProjects;
        IDeleteProject deleteProject;

        public ProjectController(APIContext context,
            ICreateProject createProject,
            ICheckForAlreadyExistingProjects checkForAlreadyExistingProjects,
            IDeleteProject deleteProject)
        {
            this.context = context;
            this.createProject = createProject;
            this.checkForAlreadyExistingProjects = checkForAlreadyExistingProjects;
            this.deleteProject = deleteProject;
        }

        [HttpGet]
        [Route("/GetProjects")]
        public JsonResult GetProjects()
        {
            var result = context.Projects
                 .OrderByDescending(x => x.NW_PK)
                 .ToList();

            return new JsonResult(Ok(result));
        }
        [HttpPost]
        [Route("/NewProject")]
        public IActionResult NewProject([FromBody] Project inputtedProjectName)
        {
            if (inputtedProjectName == null || string.IsNullOrWhiteSpace(inputtedProjectName.ProjectName))
            {
                return BadRequest("Project name is required.");
            }

            bool AlreadyExists = checkForAlreadyExistingProjects.check(context, inputtedProjectName);

            if (AlreadyExists == true)
            {
                return BadRequest("Project name already Exists");
            }

            bool success = createProject.create(context, inputtedProjectName);

            if (success == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Project creation failed");
            }
        }
        [HttpPost]
        [Route("/DeleteProject")]
        public IActionResult DeleteProject([FromBody] Project inputtedProject)
        {
            if (inputtedProject == null || string.IsNullOrWhiteSpace(inputtedProject.ProjectName))
            {
                return BadRequest("Project name is required." + inputtedProject);
            }

            bool Success = deleteProject.delete(context, inputtedProject);
            if (Success == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Error deleting Project: " + inputtedProject);
            }
        }

    }
}
