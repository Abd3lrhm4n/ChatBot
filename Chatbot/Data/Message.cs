using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chatbot.Data
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string MessageBody { get; set; }

        [Required]
        public DateTime SentAt { get; set; }

        [Required]
        public bool SenderIsBot { get; set; } = true;

        public ApplicationUser Human { get; set; }

        public ApplicationUser Bot { get; set; }

        [ForeignKey("Human")]
        [Required]
        public string HumanId { get; set; }

        [ForeignKey("Bot")]
        [Required]
        public string BotId { get; set; }
    }
}
