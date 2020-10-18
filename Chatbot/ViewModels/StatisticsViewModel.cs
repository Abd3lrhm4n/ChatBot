using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatbot.ViewModels
{
    public class StatisticsViewModel
    {
        public Dictionary<string, int> UsersCount { get; set; }

        public Dictionary<string, double> TopUsedWords { get; set; }
    }
}
