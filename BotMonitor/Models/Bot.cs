using System;
using System.ComponentModel.DataAnnotations;

namespace BotMonitor.Models
{
    public class Bot
    {
        public byte Id { get; set; }

        [MaxLength(16)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string CurrentState { get; set; }

        public byte Level { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
