using BotMonitor.Concerns;
using BotMonitor.Data;
using BotMonitor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BotMonitor.Controllers
{

    public class InstructionController : Controller
    {
        readonly BotContext context;
        readonly IAuthentication authentication;

        public InstructionController(BotContext context, IAuthentication authentication)
        {
            this.context = context;
            this.authentication = authentication;
        }

        [HttpGet]
        public async Task<JsonResult> Show(
            string username, 
            string password, 
            string characterName, 
            string realmName)
        {
            var userID = await authentication.AuthenticateUser(username, password);

            var bot = await context.Bots.FirstOrDefaultAsync(b =>
                b.Name == characterName && b.RealmName == realmName
            );

            if (bot == null)
                throw new Exception("Bot not found. Add the bot to Bot Monitor before playing.");

            var instruction = await context.Instructions.SingleOrDefaultAsync(i => i.BotId == bot.Id);

            if (instruction != null)
            {
                context.Instructions.Remove(instruction);
                await context.SaveChangesAsync();

                return Json(instruction.Command);
            }

            return Json(null);
        }

        [HttpPost]
        [Authorize]
        public async Task IssueInstruction(int botId, string command)
        {
            var instruction = await context.Instructions.FirstOrDefaultAsync(i => i.BotId == botId);

            if (instruction == null)
            {
                instruction = new Instruction
                {
                    BotId = botId
                };
                await context.Instructions.AddAsync(instruction);
            }

            instruction.Command = command;
            await context.SaveChangesAsync();
        }
    }
}
