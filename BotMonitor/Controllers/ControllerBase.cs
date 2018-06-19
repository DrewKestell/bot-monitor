using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Authentication;

namespace BotMonitor.Controllers
{
    public class ControllerBase : Controller
    {
        readonly string apiKey;
        
        public ControllerBase(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Query["apiKey"] != apiKey)
                throw new AuthenticationException("STOP!");

            base.OnActionExecuting(context);
        }
    }
}
