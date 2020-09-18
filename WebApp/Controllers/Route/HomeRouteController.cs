using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Route
{
    // [Authorize]
    [Route("")]
    public class HomeRouteController : Controller
    {

        public HomeRouteController()
        {
        }
        [AllowAnonymous]
        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet("GuestRegister")]
        public IActionResult GuestRegister()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }
        
    }
}