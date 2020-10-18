using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chatbot.Data
{
    public class BotSpeech
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Phrase { get; set; }

        public Classification Classification { get; set; }

        public string Language { get; set; }

        [ForeignKey("Classification")]
        public int ClassificationId { get; set; }
    }
}
