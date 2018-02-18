using System.ComponentModel.DataAnnotations.Schema;

namespace BotMonitor.Models
{
    public class Instruction
    {
        public byte Id { get; set; }
        
        [ForeignKey("Bot")]
        public byte BotId { get; set; }

        public Bot Bot { get; set; }

        public string Command { get; set; }
    }
}
