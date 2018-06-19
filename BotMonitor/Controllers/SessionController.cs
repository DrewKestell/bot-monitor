using BotMonitor.Configuration;
using BotMonitor.Data;
using BotMonitor.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BotMonitor.Controllers
{
    public class SessionController : ControllerBase
    {
        readonly BotContext context;
        readonly IPasswordHasher<User> passwordHasher;

        public SessionController(BotContext context, IPasswordHasher<User> passwordHasher, IOptions<ApiConfiguration> config) : base(config.Value.ApiKey)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
        }

        [HttpGet]   
        public IActionResult Create() => View();

        [HttpPost]
        public async Task Create(string username, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                throw new AuthenticationException("Account not found.");

            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password);

            switch (verificationResult)
            {
                case PasswordVerificationResult.SuccessRehashNeeded:
                    user.HashedPassword = passwordHasher.HashPassword(user, password);
                    await context.SaveChangesAsync();
                    break;
                case PasswordVerificationResult.Failed:
                    throw new AuthenticationException("Invalid password.");
            }

            await SignInAsync(HttpContext, user.Id);
        }

        [HttpDelete]
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
