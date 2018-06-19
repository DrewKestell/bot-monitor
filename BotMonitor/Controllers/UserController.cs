using BotMonitor.Configuration;
using BotMonitor.Data;
using BotMonitor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace BotMonitor.Controllers
{
    public class UserController : Controller
    {
        readonly BotContext context;
        readonly IPasswordHasher<User> passwordHasher;
        readonly ApiConfiguration config;

        public UserController(BotContext context, 
            IPasswordHasher<User> passwordHasher, 
            IOptions<ApiConfiguration> config)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
            this.config = config.Value;
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(string username, string password, string email, string apiKey)
        {
            if (apiKey != config.ApiKey)
                throw new AuthenticationException("Invalid API Key!");

            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null)
                throw new AuthenticationException("Username has already been used.");

            user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
                throw new AuthenticationException("Email has already been used.");
             
            user = new User
            {
                Email = email,
                Username = username
            };
            user.HashedPassword = passwordHasher.HashPassword(user, password);
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return RedirectToAction("Create", "Session");
        }
    }
}
