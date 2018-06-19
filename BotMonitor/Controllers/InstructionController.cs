using BotMonitor.Data;
using BotMonitor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BotMonitor.Controllers
{
    [Authorize]
    public class InstructionController : Controller
    {
        readonly BotContext context;

        public InstructionController(BotContext context)
        {
            this.context = context;
        }

        [HttpPost]
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
