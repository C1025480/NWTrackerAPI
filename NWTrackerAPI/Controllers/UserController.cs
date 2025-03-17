using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NWTrackerAPI.Data;
using NWTrackerAPI.Models;
using NWTrackerAPI.Processors.Interfaces;

namespace NWTrackerAPI.Controllers
{
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        public readonly APIContext context;
        ICreateUser createUser;
        IGetUser getUser;

        public UserController(APIContext context, ICreateUser createUser, IGetUser getUser)
        {
            this.context = context;
            this.createUser = createUser;   
            this.getUser = getUser;
        }

        [HttpGet]
        [Route("/GetLogin")]
        public JsonResult GetLogin()
        {
            var result = context.LOG_LOGINS
                 .OrderByDescending(x => x.LOG_ID)
                 .ToList();

            return new JsonResult(Ok(result));
        }

        [HttpPost]
        [Route("/CreateUser")]
        public IActionResult CreateUser([FromBody] LOG_LOGIN inputtedUser)
        {
            createUser.create(context, inputtedUser);
            
            return Ok();
        }

        [HttpPost]
        [Route("/UserLogin")]
        public IActionResult UserLogin([FromBody] LoginModel login)
        {
            LOG_LOGIN User = getUser.Validate(context, login.username, login.password);

            if (User == null) {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            else
            {
                User.LOG_HASHED_PASSWORD = null;
                User.LOG_SALT = null;
                return Ok(new
                {
                    message = "Login successful",
                    user = User,
                });
            }
        }


    }
}
