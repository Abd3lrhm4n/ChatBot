using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chatbot.Data
{
    public class WordCountUse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Word { get; set; }

        [Required]
        public double Count { get; set; }
    }
}
