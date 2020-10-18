using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatbot.Dto
{
    public class ClassificationWithPriortyDto
    {
        public int ClassificationNumber { get; set; }

        public int PriortyToAnswer { get; set; }
    }
}
