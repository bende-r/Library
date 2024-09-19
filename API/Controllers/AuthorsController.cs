using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
