using Chatbot.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatbot.Repository
{
    public class WordCountUseRepository : WordCountUse
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ILogger<WordCountUseRepository> _logger;
        public WordCountUseRepository(ApplicationDbContext ctx, ILogger<WordCountUseRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public async Task Insert(string[] words)
        {
            try
            {
                for (int i = 0; i < words.Length; i++)
                {
                    string word = words[i].Trim().ToLower();

                    if (string.IsNullOrEmpty(word))
                    {
                        continue;
                    }

                    var dbWord = await _ctx.WordCountUses.Where(x => x.Word == word).FirstOrDefaultAsync();

                    if (dbWord != null)
                    {
                        dbWord.Count += 1;
                        _ctx.Entry(dbWord).State = EntityState.Modified;
                    }
                    else
                    {
                        var newWord = new WordCountUse
                        {
                            Count = 1,
                            Word = word
                        };

                        _ctx.WordCountUses.Add(newWord);
                    }
                    await _ctx.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                _logger.LogTrace(ex, ex.Message);
            }
        }
    }
}
