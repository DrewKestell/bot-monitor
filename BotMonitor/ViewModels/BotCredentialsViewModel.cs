using BotMonitor.Models;

namespace BotMonitor.ViewModels
{
    public class BotCredentialsViewModel
    {
        public BotCredentialsViewModel(Bot bot)
        {
            CharacterName = bot.Name;
            Username = bot.AccountUsername;
            Password = bot.AccountPassword;
        }

        public string CharacterName { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
