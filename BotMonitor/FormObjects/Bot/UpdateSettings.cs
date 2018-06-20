namespace BotMonitor.FormObjects.Bot
{
    public class UpdateSettings
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string RealmName { get; set; }
        
        public string AI { get; set; }

        public string HotSpot { get; set; }

        public string Food { get; set; }

        public string Drink { get; set; }

        public string Ammo { get; set; }

        public string ExcludedMobs { get; set; }

        public byte MinLevel { get; set; }

        public byte MaxLevel { get; set; }
    }
}
