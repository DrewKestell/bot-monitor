using BotMonitor.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace BotMonitor.Controllers
{
    public class UserController : ControllerBase
    {
        public UserController(IOptions<ApiConfiguration> config) : base(config.Value.ApiKey) { }

        [HttpPost]
        public async Task Create(string username, string password, string email, string apiKey)
        {

        }
    }
}
