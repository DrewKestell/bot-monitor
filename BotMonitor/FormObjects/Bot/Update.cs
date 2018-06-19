using System;

namespace BotMonitor.FormObjects.Bot
{
    public class Update
    {
        public string Username { get; set; }
        
        public string Password { get; set; }

        public string Name { get; set; }

        public string RealmName { get; set; }

        public string CurrentState { get; set; }

        public byte Level { get; set; }

        public DateTime LastUpdated { get; set; }

        public bool IsValid =>
            !String.IsNullOrWhiteSpace(Username) &&
            !String.IsNullOrWhiteSpace(Password) &&
            !String.IsNullOrWhiteSpace(Name) &&
            !String.IsNullOrWhiteSpace(RealmName) &&
            !String.IsNullOrWhiteSpace(CurrentState);
    }
}
