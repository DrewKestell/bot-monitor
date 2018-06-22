using Microsoft.AspNetCore.Mvc;

namespace BotMonitor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        
        [HttpGet]
        public IActionResult AccessDenied() => View();
    }
}
