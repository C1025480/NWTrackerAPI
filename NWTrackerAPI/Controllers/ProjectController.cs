using Microsoft.AspNetCore.Mvc;
using NWTrackerAPI.Data;

namespace NWTrackerAPI.Controllers
{
    [ApiController]
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
    }
}
