using System.Linq;
using BotMonitor.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BotMonitor.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using BotMonitor.Configuration;

namespace BotMonitor.Controllers
{
    public class HomeController : ControllerBase
    {
        public HomeController(IOptions<ApiConfiguration> config) : base(config.Value.ApiKey) { }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task IssueInstruction(byte botId, string command)
        {
            using (var db = new BotContext())
            {
                var instruction = await db.Instructions.FirstOrDefaultAsync(i => i.BotId == botId);

                if (instruction == null)
                {
                    instruction = new Instruction
                    {
                        BotId = botId
                    };
                    await db.Instructions.AddAsync(instruction);
                }

                instruction.Command = command;
                await db.SaveChangesAsync();
            }
        }

        [HttpPost]
        public async Task UpdateBotStatus()
        {

        }

        [HttpGet]
        public async Task<JsonResult> GetData()
        {
            using (var db = new BotContext())
            {
                var bots = await db.Bots.ToListAsync();
                return Json(bots.Select(b => new ViewModels.Bot(b)));
            }
        }
    }
}
