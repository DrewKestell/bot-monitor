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

        [Required]
        [MaxLength(16)]
        public string Name { get; set; }

        [Required]
        [MaxLength(32)]
        public string RealmName { get; set; }

        [Required]
        [MaxLength(32)]
        public string CurrentState { get; set; }

        [Required]
        [MaxLength(32)]
        public string AccountUsername { get; set; }

        [Required]
        [MaxLength(32)]
        public string AccountPassword { get; set; }

        public byte Level { get; set; }

        public DateTime LastUpdated { get; set; }

        public User User { get; set; }
    }
}
