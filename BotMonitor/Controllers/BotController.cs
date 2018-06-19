using BotMonitor.Configuration;
using Microsoft.Extensions.Options;

namespace BotMonitor.Controllers
{
    public class BotController : ControllerBase
    {
        public BotController(IOptions<ApiConfiguration> config)
            : base(config.Value.ApiKey) { }
    }
}
