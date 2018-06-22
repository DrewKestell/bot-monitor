using BotMonitor.Models;

namespace BotMonitor.ViewModels
{
    public class BotViewModel
    {
        public BotViewModel(Bot bot)
        {
            Id = bot.Id;
            Name = bot.Name;
            RealmName = bot.RealmName;
            CurrentState = bot.CurrentState;
            CurrentZone = bot.CurrentZone;
            Class = bot.Class;
            Level = bot.Level;
            LastUpdated = $"{bot.LastUpdated:g}";
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string RealmName { get; set; }
        
        public string CurrentState { get; set; }

        public string CurrentZone { get; set; }

        public string Class { get; set; }

        public byte Level { get; set; }

        public string LastUpdated { get; set; }
    }
}
