using Chatbot.Data;
using Chatbot.Dto;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Chatbot.Repository
{
    public class WordClassificationRepository : WordClassification
    {
        private readonly ApplicationDbContext _ctx;

        public WordClassificationRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<ClassificationWithPriortyDto>> GetClassificationInMessageAsync(string msg)
        {
            string[] words = msg.Split(' ');

            var classifications = await _ctx.WordClassifications
                                            .Where(x => words.Contains(x.Word) || 
                                                msg.Contains(x.Word))
                                            .Select(a => new ClassificationWithPriortyDto
                                            {
                                                ClassificationNumber = a.Classification.ClassificationNumber,
                                                PriortyToAnswer = a.Classification.PriorityToAnswer,
                                            }).ToListAsync();

            return classifications;
        }

        public async Task<IEnumerable<string>> ExtractWordsFromMessageInSpecifiedClassification(string msg, int classificationNumber)
        {
            string[] words = msg.Split(' ');
            List<string> matches = new List<string>();

            for (int i = 0; i < words.Length; i++)
            {
                var isMatch = await _ctx.WordClassifications.AnyAsync(x => words.Contains(x.Word) && x.Classification.ClassificationNumber == classificationNumber);

                matches.Add(words[i]);
            }

            return matches;
        }
    }
}
