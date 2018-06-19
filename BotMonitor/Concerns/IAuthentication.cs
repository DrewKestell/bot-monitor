using System.Threading.Tasks;

namespace BotMonitor.Services
{
    public interface IAuthentication
    {
        Task<int> AuthenticateUser(string username, string password);
    }
}
