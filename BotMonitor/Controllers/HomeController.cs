using BotMonitor.Configuration;
using BotMonitor.Data;
using BotMonitor.FormObjects;
using BotMonitor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace BotMonitor.Controllers
{
    public class HomeController : Controller
    {
        //readonly BotContext context;

        //public HomeController(BotContext context, IOptions<ApiConfiguration> config)
        //    : base(config.Value.ApiKey)
        //{
        //    this.context = context;
        //}

        public IActionResult Index() => View();


        [HttpGet]
        public IActionResult AccessDenied() => View();
    }
}
