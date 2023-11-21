using Microsoft.AspNetCore.Mvc;

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpPost]
        public IActionResult Create()
        {

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id)
        {

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {

        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {

        }
    }
}
 