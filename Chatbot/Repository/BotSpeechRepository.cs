using Chatbot.Data;
using Chatbot.Dto;
using Chatbot.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Chatbot.Repository
{
    public class BotSpeechRepository : BotSpeech
    {
        private readonly ApplicationDbContext _ctx;

        public BotSpeechRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<string> GenerateBotSpeech(string message, IEnumerable<ClassificationWithPriortyDto> classifications)
        {
            string BotSpeech = "";

            if (message.Any(a => a >= 0x600 && a <= 0x6ff) && message.ToLower().Any(a => a >= 97 && a <= 122))
            {
                BotSpeech = "من فضلك تحدث بلغة واحده في الرساله حتي لا يختلط علي الامر فأنا آلي";
            }
            else if (message.Any(a => a >= 0x600 && a <= 0x6ff))
            {
                if (classifications == null || classifications.Count() < 1)
                {
                    return "لا افهم لم يتم تدريبي جيدا";
                }

                var orderedClassifications = classifications.OrderBy(x => x.PriortyToAnswer).ToList();

                for (int i = 0; i < orderedClassifications.Count(); i++)
                {
                    var possiblesSpeeches = await _ctx.BotSpeeches.Where(x => x.Classification.ClassificationNumber == orderedClassifications[i].ClassificationNumber &&
                                                                x.Language == LanguageEnum.Arabic)
                                                            .OrderBy(speech => Guid.NewGuid())
                                                            .Select(p => p.Phrase)
                                                            .FirstOrDefaultAsync();

                    BotSpeech += possiblesSpeeches + "\n";

                    if (orderedClassifications[i].ClassificationNumber == 5)
                    {
                        var matches = await this.ExtractWordsFromMessageInSpecifiedClassification(message, 5);

                        List<Product> foundProducts = new List<Product>();

                        for (int j = 0; j < matches.Count(); j++)
                        {
                            var products = await _ctx.Products.Where(x => x.NameAr.Contains(matches.ElementAt(j)))
                                                        .ToListAsync();

                            foundProducts.AddRange(products);
                        }

                        if (foundProducts.Count() > 0)
                        {
                            foundProducts = foundProducts.Distinct().ToList();

                            var TopMatchesProducts = foundProducts.SelectMany(p =>
                            {
                                return matches.Select(a => new
                                {
                                    Percentage = p.NameAr.CalculateSimilarity(a),
                                    Product = p
                                });
                            }).Where(x => x.Percentage > 0).OrderByDescending(x => x.Percentage).Take(3).ToList();

                            if (TopMatchesProducts.Count() < 1)
                            {
                                BotSpeech += "Opps! We Did not Find What You Looking For\n";
                            }
                            else
                            {
                                foreach (var prod in TopMatchesProducts)
                                {
                                    BotSpeech += $"{prod.Product.NameAr} السعر: {prod.Product.Price}\n";
                                }
                            }
                        }
                        else
                        {
                            BotSpeech += "معذرة لما نجد ما تبحث عنه\n";
                        }
                    }

                }

            }
            else if (message.ToLower().Any(a => a >= 97 && a <= 122))
            {
                if (classifications == null || classifications.Count() < 1)
                {
                    return "Sorry Did Not Understand You My Creator Did Not Teach Me Well";
                }

                var orderedClassifications = classifications.OrderBy(x => x.PriortyToAnswer).ToList();

                for (int i = 0; i < orderedClassifications.Count(); i++)
                {
                    var possiblesSpeeches = await _ctx.BotSpeeches.Where(x => x.Classification.ClassificationNumber == orderedClassifications[i].ClassificationNumber &&
                                                                x.Language == LanguageEnum.English)
                                                            .OrderBy(speech => Guid.NewGuid())
                                                            .Select(p => p.Phrase)
                                                            .FirstOrDefaultAsync();

                    BotSpeech += possiblesSpeeches + "\n";

                    if (orderedClassifications[i].ClassificationNumber == 5)
                    {
                        var matches = await this.ExtractWordsFromMessageInSpecifiedClassification(message, 5);

                        List<Product> foundProducts = new List<Product>();

                        for (int j = 0; j < matches.Count(); j++)
                        {
                            var products = await _ctx.Products.Where(x => x.NameEn.Contains(matches.ElementAt(j)))
                                                        .ToListAsync();

                            foundProducts.AddRange(products);
                        }

                        if (foundProducts.Count() > 0)
                        {
                            foundProducts = foundProducts.Distinct().ToList();

                            var TopMatchesProducts = foundProducts.SelectMany(p =>
                            {
                                return matches.Select(a => new
                                {
                                    Percentage = p.NameEn.CalculateSimilarity(a),
                                    Product = p
                                });
                            }).Where(x => x.Percentage > 0).OrderByDescending(x => x.Percentage).Take(3).ToList();

                            if (TopMatchesProducts.Count() < 1)
                            {
                                BotSpeech += "Opps! We Did not Find What You Looking For\n";
                            }
                            else
                            {
                                foreach (var prod in TopMatchesProducts)
                                {
                                    BotSpeech += $"{prod.Product.NameEn} Price: {prod.Product.Price}\n";
                                }
                            }
                        }
                        else
                        {
                            BotSpeech += "Opps! We Did not Find What You Looking For\n";
                        }
                    }
                }

            }
            else
            {
                BotSpeech = "Cannot Recognize The Language\nلم يتم التعرف علي اللغه";
            }

            return BotSpeech;
        }

        public async Task<IEnumerable<string>> ExtractWordsFromMessageInSpecifiedClassification(string msg, int classificationNumber)
        {
            string[] words = msg.Split(' ');
            List<string> matches = new List<string>();

            for (int i = 0; i < words.Length; i++)
            {
                var isMatch = await _ctx.WordClassifications.AnyAsync(x => x.Word.Contains(words[i]) && x.Classification.ClassificationNumber == classificationNumber);

                if (isMatch && words[i].Length > 2)
                {
                    matches.Add(words[i]);
                }
            }

            return matches;
        }
    }
}
