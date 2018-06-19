using System.ComponentModel.DataAnnotations.Schema;

namespace BotMonitor.Models
{
    public class Instruction
    {
        public int Id { get; set; }
        
        [ForeignKey("Bot")]
        public int BotId { get; set; }

        public Bot Bot { get; set; }

        public string Command { get; set; }
    }
}
