namespace BotMonitor.ViewModels
{
    public class BotViewModel
    {
        public BotViewModel(Models.Bot bot)
        {
            Id = bot.Id;
            Name = bot.Name;
            CurrentState = bot.CurrentState;
            Level = bot.Level;
            LastUpdated = $"{bot.LastUpdated:M/d/yyyy H:mm:ss tt}";
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string CurrentState { get; set; }

        public byte Level { get; set; }

        public string LastUpdated { get; set; }
    }
}
