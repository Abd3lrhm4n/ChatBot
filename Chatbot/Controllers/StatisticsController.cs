using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Chatbot.Repository;
using Chatbot.ViewModels;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;

namespace Chatbot.Controllers
{
    [Authorize(Roles = "Manager")]
    public class StatisticsController : Controller
    {
        private readonly Statistics _statistics;

        public StatisticsController(Statistics statistics, WordCountUseRepository wordCountUse)
        {
            _statistics = statistics;
        }

        public async Task<IActionResult> Index()
        {
            StatisticsViewModel model = new StatisticsViewModel();

            model.UsersCount = await _statistics.GetGendersCount();
            model.TopUsedWords = await _statistics.GetTopUsedWordsCount(10);

            return View(model);
        }

        
    }
}
