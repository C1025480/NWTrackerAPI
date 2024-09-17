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
            bool AlreadyExists = context.Projects.Any(x => x.ProjectName == inputtedProjectName.ProjectName);

            if (AlreadyExists == true)
            {
                return BadRequest("Project name already Exists");
            }

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
        [HttpGet]
        [Route("/GetTrackerRecords")]
        public IActionResult GetTrackerRecords(int ProjectPk)
        {
            var result = context.TT_TRACKER
                .Where(tt => tt.TT_NW_FK == ProjectPk)
                .Select(tt => new TrackerRecord
                {
                TT_PK = tt.TT_PK,
                TT_NW_FK = tt.TT_NW_FK, 
                TT_UPRN = tt.TT_UPRN,
                TT_STATUS = tt.TT_STATUS,
                TT_HOUSE_NUMBER = tt.TT_HOUSE_NUMBER,
                TT_STREET = tt.TT_STREET,
                })
            .ToList();

            return new JsonResult(Ok(result));
        }
        [HttpGet]
        [Route("/GetTrackerRecord")]
        public JsonResult GetTrackerRecord(int TrackerRecordPK)
        {
            var result = context.TT_TRACKER
                .Where(tt => tt.TT_PK ==TrackerRecordPK)
                .ToList();

            return new JsonResult(Ok(result));
        }
    }
}
