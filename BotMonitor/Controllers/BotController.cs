using BotMonitor.Data;
using BotMonitor.FormObjects.Bot;
using BotMonitor.Models;
using BotMonitor.Services;
using BotMonitor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BotMonitor.Controllers
{
    public class BotController : Controller
    {
        readonly IAuthentication authentication;
        readonly BotContext context;

        public BotController(
            IAuthentication authentication,
            BotContext context)
        {
            this.authentication = authentication;
            this.context = context;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index() => View();

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> List()
        {
            var userId = int.Parse(HttpContext.User.Claims.Single(c => c.Type == "UserId").Value);
            
            var bots = await context.Bots
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return Json(bots.Select(b => new BotViewModel(b)));
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create() => View();

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Create formObject)
        {
            if (!formObject.IsValid)
                throw new Exception("Request was not valid.");

            var bot = await context.Bots.FirstOrDefaultAsync(b =>
                b.Name == formObject.Name && b.RealmName == formObject.RealmName
            );

            if (bot != null)
                throw new Exception("Bot already exists.");

            var userId = int.Parse(HttpContext.User.Claims.Single(c => c.Type == "UserId").Value);

            await context.Bots.AddAsync(new Bot
            {
                UserId = userId,
                Name = formObject.Name,
                RealmName = formObject.RealmName,
                CurrentState = "Fresh",
                AccountUsername = formObject.AccountUsername,
                AccountPassword = formObject.AccountPassword,
                Level = 1,
                LastUpdated = DateTime.Now
            });
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public async Task Update(Update formObject)
        {
            if (!formObject.IsValid)
                throw new Exception("Request was not valid.");

            var userID = await authentication.AuthenticateUser(formObject.Username, formObject.Password);

            var bot = await context.Bots.FirstOrDefaultAsync(b => 
                b.Name == formObject.Name && b.RealmName == formObject.RealmName
            );

            if (bot == null)
                throw new Exception("Bot not found. Add the bot to Bot Monitor before playing.");

            bot.CurrentState = formObject.CurrentState;
            bot.Level = formObject.Level;
            bot.LastUpdated = DateTime.Now;
            await context.SaveChangesAsync();
        }
    }
}
