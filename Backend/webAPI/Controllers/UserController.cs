using Microsoft.AspNetCore.Mvc;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    // UserController -> UserService -> UserRepository
    // Controller -> Service -> Repository
    public class UserController : Controller
    {
        [HttpPost]
        public IActionResult Create()
        {
            throw new NotImplementedException();
            //_userService.create()
        }

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
    }
}
 