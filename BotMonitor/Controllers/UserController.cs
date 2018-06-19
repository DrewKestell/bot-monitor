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
    public class UserController : ControllerBase
    {
        readonly BotContext context;
        readonly IPasswordHasher<User> passwordHasher;

        public UserController(BotContext context, IPasswordHasher<User> passwordHasher, IOptions<ApiConfiguration> config) : base(config.Value.ApiKey) { }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task Create(string username, string password, string email, string apiKey)
        {
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
        }
    }
}
