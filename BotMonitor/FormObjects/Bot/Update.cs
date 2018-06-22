namespace BotMonitor.FormObjects.Bot
{
    public class Update
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string RealmName { get; set; }
        
        public string AccountUsername { get; set; }
        
        public string AccountPassword { get; set; }
        
        public string CurrentState { get; set; }

        public byte Level { get; set; }

        public string CurrentZone { get; set; }

        public string Class { get; set; }
    }
}
