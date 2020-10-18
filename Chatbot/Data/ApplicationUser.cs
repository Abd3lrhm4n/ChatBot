using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chatbot.Data
{
    public class ApplicationUser : IdentityUser
    {
        public int Age { get; set; }

        public int Gender { get; set; }
    }

    
}
