using Chatbot.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chatbot.Repository
{
    public class Statistics
    {
        private readonly ApplicationDbContext _ctx;

        public Statistics(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Dictionary<string, int>> GetGendersCount()
        {
            int MalesCount =  await _ctx.Users
                                        .Join(_ctx.UserClaims, user => user.Id, claim => claim.UserId, (user, claim) => new { User = user, Claim = claim })
                                        .Where(x => x.User.Gender == (int)Enums.Gender.Male && x.Claim.ClaimValue == "Human")
                                        .CountAsync();

            int FemalesCount = await _ctx.Users
                                        .Join(_ctx.UserClaims, user => user.Id, claim => claim.UserId, (user, claim) => new { User = user, Claim = claim })
                                        .Where(x => x.User.Gender == (int)Enums.Gender.Female && x.Claim.ClaimValue == "Human")
                                        .CountAsync();

            return new Dictionary<string, int>
            {
                {"Males", MalesCount },
                {"Females", FemalesCount }
            };
        }

        public async Task<Dictionary<string, double>> GetTopUsedWordsCount(int take)
        {
            return await _ctx.WordCountUses
                                .OrderByDescending(o => o.Count)
                                .Take(take)
                                .ToDictionaryAsync(p => p.Word, v => v.Count);
        } 
        
    }
}
