using ChatWebServer.Models;
using ChatWebServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly Service _service;
        private static Dictionary<string, string> usersTokensDic=new Dictionary<string, string>();  

        public static Dictionary<string, string> getUsersTokensDic() { return usersTokensDic; }

        public LoginController(Service service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Id,Password")] User user)
        {
            User claim = await _service.UserValidation(user);
            if (claim == null)
                return NotFound();
            return Ok(claim);
        }


        [HttpPost("token")]
        public async Task<IActionResult> UpdateToken([Bind("Id,Password")] User user)
        {
            usersTokensDic[user.Id] = user.Password;
            return Ok(user.Id);
        }

    }
}
