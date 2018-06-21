using BotMonitor.Models;

namespace BotMonitor.ViewModels
{
    public class BotDetailViewModel
    {
        public BotDetailViewModel(Bot bot)
        {
            CharacterName = bot.Name;
            AccountUsername = bot.AccountUsername;
            AccountPassword = bot.AccountPassword;
            AI = bot.AI;
            HotSpot = bot.HotSpot;
            Food = bot.Food;
            Drink = bot.Drink;
            Ammo = bot.Ammo;
            ExcludedMobs = bot.ExcludedMobs;
            MinLevel = bot.MinLevel;
            MaxLevel = bot.MaxLevel;
            Level = bot.Level;
        }

        public string CharacterName { get; set; }

        public string AccountUsername { get; set; }

        public string AccountPassword { get; set; }

        public string AI { get; set; }
        
        public string HotSpot { get; set; }

        public string Food { get; set; }

        public string Drink { get; set; }

        public string Ammo { get; set; }

        public string ExcludedMobs { get; set; }

        public byte MinLevel { get; set; }

        public byte MaxLevel { get; set; }

        public byte Level { get; set; }
    }
}
