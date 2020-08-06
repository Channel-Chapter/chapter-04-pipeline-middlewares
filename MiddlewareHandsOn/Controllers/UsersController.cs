using Microsoft.AspNetCore.Mvc;
using MiddlewareHandsOn.Api.Models;
using System.Collections.Generic;

namespace MiddlewareHandsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return new List<User>
            {
                new User("Gabriel", "Brazil"),
                new User("John", "Canada"),
                new User("Mike", "USA")
            };
        }
    }

}
