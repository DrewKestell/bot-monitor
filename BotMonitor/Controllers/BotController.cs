using BotMonitor.Concerns;
using BotMonitor.Configuration;
using BotMonitor.Data;
using BotMonitor.FormObjects.Bot;
using BotMonitor.Models;
using BotMonitor.ViewModels;
using DSharpPlus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BotMonitor.Controllers
{
    public class BotController : Controller
    {
        readonly IAuthentication authentication;
        readonly BotContext context;
        readonly ApiConfiguration config;

        public BotController(
            IAuthentication authentication,
            BotContext context,
            IOptions<ApiConfiguration> config)
        {
            this.authentication = authentication;
            this.context = context;
            this.config = config.Value;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index() => View();

        [Authorize]
        [HttpGet]
        public async Task<JsonResult> List(string sortOrder)
        {
            var userId = int.Parse(HttpContext.User.Claims.Single(c => c.Type == "UserId").Value);

            var bots = context.Bots
                .Where(b => b.UserId == userId);

            switch(sortOrder)
            {
                case "Name":
                    bots = bots.OrderBy(b => b.Name);
                    break;
                case "Realm Name":
                    bots = bots.OrderBy(b => b.RealmName).ThenBy(b => b.Name).ThenBy(b => b.Level);
                    break;
                case "Active":
                    bots = bots.OrderBy(b => b.CurrentState != "Idle" ? 1 : 2);
                    break;
                default:
                    bots = bots.OrderByDescending(b => DateTime.Parse(b.LastUpdated.ToString("g"))).ThenBy(b => b.RealmName).ThenBy(b => b.Name).ThenBy(b => b.Level);
                    break;
            }
            
            return Json((await bots.ToListAsync()).Select(b => new BotViewModel(b)));
        }

        [HttpGet]
        public async Task<JsonResult> ListCredentials(string username, string password, string realmName)
        {
            var userId = await authentication.AuthenticateUser(username, password);

            var bots = await context.Bots
                .Where(b => b.RealmName == realmName)
                .Where(b => b.UserId == userId)
                .OrderBy(b => b.Name)
                .ToListAsync();

            return Json(bots.Select(b => new BotDetailViewModel(b)));
        }

        [HttpGet]
        public async Task<JsonResult> Show(string username, string password, string realmName, string name)
        {
            var userId = await authentication.AuthenticateUser(username, password);

            var bot = await context.Bots.FirstOrDefaultAsync(b =>
                b.Name == name && b.RealmName == realmName
            );

            if (bot == null)
                throw new Exception("Bot not found. Add the bot to Bot Monitor before playing.");

            return Json(new BotDetailViewModel(bot));
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
                AccountUsername = formObject.AccountUsername,
                AccountPassword = formObject.AccountPassword,
                AI = formObject.AI,
                CurrentState = "Fresh",
                CurrentZone = "Fresh",
                Level = 1,
                Class = formObject.Class,
                LastUpdated = DateTime.Now
            });
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        [HttpPost]
        public async Task Update(Update formObject)
        {
            var userID = await authentication.AuthenticateUser(formObject.Username, formObject.Password);

            var bot = await context.Bots.FirstOrDefaultAsync(b => 
                b.Name == formObject.Name && b.RealmName == formObject.RealmName
            );

            if (bot == null)
                throw new Exception("Bot not found. Add the bot to Bot Monitor before playing.");

            if (formObject.Level > bot.Level)
                await DispatchDiscordMessage($"{bot.Name} the {bot.Class} just reached level {formObject.Level}!");

            bot.CurrentState = formObject.CurrentState;
            bot.Level = formObject.Level;
            bot.LastUpdated = DateTime.Now;
            bot.CurrentZone = formObject.CurrentZone;
            bot.Class = formObject.Class;
            await context.SaveChangesAsync();
        }

        [HttpPost]
        public async Task UpdateSettings(UpdateSettings formObject)
        {
            var userID = await authentication.AuthenticateUser(formObject.Username, formObject.Password);

            var bot = await context.Bots.FirstOrDefaultAsync(b =>
                b.Name == formObject.Name && b.RealmName == formObject.RealmName
            );

            if (bot == null)
                throw new Exception("Bot not found. Add the bot to Bot Monitor before playing.");

            bot.AI = formObject.AI;
            bot.HotSpot = formObject.HotSpot;
            bot.Food = formObject.Food;
            bot.Drink = formObject.Drink;
            bot.Ammo = formObject.Ammo;
            bot.ExcludedMobs = formObject.ExcludedMobs;
            bot.MinLevel = formObject.MinLevel;
            bot.MaxLevel = formObject.MaxLevel;
            await context.SaveChangesAsync();
        }

        [NonAction]
        public async Task DispatchDiscordMessage(string message)
        {
            var client = new DiscordClient(new DiscordConfiguration
            {
                Token = config.BotToken,
                TokenType = TokenType.Bot
            });
            await client.ConnectAsync();
            var channel = await client.GetChannelAsync(config.ChannelId);
            await channel.SendMessageAsync(message);
        }
    }
}
