using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NWTrackerAPI.Data;
using NWTrackerAPI.Models;

namespace NWTrackerAPI.Controllers
{
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]/[action]")]
    public class ProjectController : ControllerBase
    {
        public readonly APIContext context;

        public ProjectController(APIContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("/GetProjects")]
        public JsonResult GetProjects()
        {
           var result = context.Projects.ToList();

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

            var new_project = new Project()
            {
                ProjectName = inputtedProjectName.ProjectName
            };

            context.Projects.Add(new_project);
            context.SaveChanges();

            return Ok();
        }
    }
}
