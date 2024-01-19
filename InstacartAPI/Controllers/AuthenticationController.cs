using Microsoft.AspNetCore.Mvc;

namespace InstacartAPI.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
