using System.Threading.Tasks;

namespace BotMonitor.Concerns
{
    public interface IAuthentication
    {
        Task<int> AuthenticateUser(string username, string password);
    }
}
