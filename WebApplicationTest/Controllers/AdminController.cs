using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Controllers
{
    //[Authorize(Roles ="admin,manager")]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
