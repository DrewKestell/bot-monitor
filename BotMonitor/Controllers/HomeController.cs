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
    public class HomeController : ControllerBase
    {
        readonly BotContext context;

        public HomeController(BotContext context, IOptions<ApiConfiguration> config)
            : base(config.Value.ApiKey)
        {
            this.context = context;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task IssueInstruction(byte botId, string command)
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

        [HttpPost]
        public async Task UpdateBotStatus(BotUpdate formObject)
        {

        }

        [HttpGet]
        public async Task<JsonResult> GetData()
        {
            var bots = await context.Bots.ToListAsync();
            return Json(bots.Select(b => new ViewModels.Bot(b)));
        }

        [HttpGet]
        public IActionResult AccessDenied() => View();
    }
}
