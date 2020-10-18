using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chatbot.ViewModels
{
    public class MessageViewModel
    {
        [Required]
        public string MessageBody { get; set; }
    }
}
