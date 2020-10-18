using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Chatbot.Data
{
    public class WordClassification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Word { get; set; }

        public string Language { get; set; }

        public Classification Classification { get; set; }

        [ForeignKey("Classification")]
        public int ClassificationId { get; set; }
    }
}
