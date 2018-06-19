using System.ComponentModel.DataAnnotations;

namespace BotMonitor.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(32)]
        public string Username { get; set; }

        [Required]
        [MaxLength(128)]
        public string HashedPassword { get; set; }

        [Required]
        [MaxLength(32)]
        public string Email { get; set; }

        public bool ProfilePrivate { get; set; }
    }
}
