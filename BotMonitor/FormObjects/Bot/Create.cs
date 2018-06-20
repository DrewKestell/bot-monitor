using System;

namespace BotMonitor.FormObjects.Bot
{
    public class Create
    {
        public string Name { get; set; }

        public string RealmName { get; set; }

        public string AccountUsername { get; set; }

        public string AccountPassword { get; set; }

        public string AI { get; set; }

        public bool IsValid =>
            !String.IsNullOrWhiteSpace(Name) &&
            !String.IsNullOrWhiteSpace(RealmName) &&
            !String.IsNullOrWhiteSpace(AccountUsername) &&
            !String.IsNullOrWhiteSpace(AccountPassword) &&
            !String.IsNullOrWhiteSpace(AI);
    }
}
