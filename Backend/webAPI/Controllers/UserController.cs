using Microsoft.AspNetCore.Mvc;
using webAPI.Interfaces;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // UserController -> UserService -> UserRepository
    // Controller -> Service -> Repository
    public class UserController : Controller
    {
        private readonly IActivityService _activityService;

        public UserController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        // [HttpPost]
        // public IActionResult Create()
        // {
        //     throw new NotImplementedException();
        //     //_userService.create()
        // }

        [HttpPut("{id}")]
        public IActionResult Update(int id)
        {
            throw new NotImplementedException();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            throw new NotImplementedException();

        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            throw new NotImplementedException();

        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            throw new NotImplementedException();

        }

        [HttpGet("{id}/activities")]
        public IActionResult GetActivitiesForUser(int id)
        {
            var allByUserId = this._activityService.GetAllByUserId(id);
            return Ok(allByUserId);
        }
    }
}
 