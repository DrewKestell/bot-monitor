using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotMonitor.Models
{
    public class Bot
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        // set these when creating the bot

        [Required]
        [MaxLength(16)]
        public string Name { get; set; }

        [Required]
        [MaxLength(32)]
        public string RealmName { get; set; }
        
        [Required]
        [MaxLength(32)]
        public string AccountUsername { get; set; }

        [Required]
        [MaxLength(32)]
        public string AccountPassword { get; set; }

        [Required]
        [MaxLength(32)]
        public string AI { get; set; }
        
        // post these with every update

        [Required]
        [MaxLength(32)]
        public string CurrentState { get; set; }

        [MaxLength(32)]
        public string HotSpot { get; set; }
        
        [MaxLength(32)]
        public string Food { get; set; }
        
        [MaxLength(32)]
        public string Drink { get; set; }
        
        [MaxLength(32)]
        public string Ammo { get; set; }

        [MaxLength(128)]
        public string ExcludedMobs { get; set; }
        
        public byte MinLevel { get; set; }

        public byte MaxLevel { get; set; }

        public byte Level { get; set; }

        public DateTime LastUpdated { get; set; }

        public User User { get; set; }
    }
}
