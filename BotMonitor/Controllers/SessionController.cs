using BotMonitor.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BotMonitor.Controllers
{
    public class SessionController : Controller
    {
        readonly IAuthentication authentication;

        public SessionController(IAuthentication authentication)
        {
            this.authentication = authentication;
        }

        [HttpGet]   
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string username, string password)
        {
            var userId = await authentication.AuthenticateUser(username, password);

            await SignInAsync(HttpContext, userId);

            return RedirectToAction("Index", "Bot");
        }

        // this should be HttpDestroy
        [HttpGet]
        public async Task<IActionResult> Destroy()
        {
            await SignOutAsync(HttpContext);
            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        public async Task SignInAsync(HttpContext httpContext, int userId) =>
            await httpContext.SignInAsync("BloogBotAuthenticationScheme", BuildClaimsPrincipal(userId));

        [NonAction]
        public async Task SignOutAsync(HttpContext httpContext) =>
            await httpContext.SignOutAsync("BloogBotAuthenticationScheme");

        static ClaimsPrincipal BuildClaimsPrincipal(int userId)
        {
            var claimsIdentity = new ClaimsIdentity("BloogBotAuthenticationScheme");
            claimsIdentity.AddClaim(new Claim("UserId", userId.ToString()));
            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
