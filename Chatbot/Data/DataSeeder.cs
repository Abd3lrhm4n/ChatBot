using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Chatbot.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Data
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            var adminUser = new ApplicationUser
            {
                Email = "admin@dxwand.com",
                NormalizedEmail = "ADMIN@DXWAND.COM",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var botUser = new ApplicationUser
            {
                Email = "bot@dxwand.com",
                NormalizedEmail = "BOT@DXWAND.COM",
                UserName = "bot",
                NormalizedUserName = "BOT",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!_context.Users.Any(u => u.UserName == adminUser.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(adminUser, "Admin12!");
                adminUser.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(_context);
                var result = userStore.CreateAsync(adminUser);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Manager"),
                    new Claim(ClaimTypes.NameIdentifier, adminUser.Id)
                };

                userStore.AddClaimsAsync(adminUser, claims);

            }

            if (!_context.Users.Any(u => u.UserName == botUser.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(botUser, "Bot1234!");
                botUser.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(_context);
                var result = userStore.CreateAsync(botUser);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "Bot"),
                    new Claim(ClaimTypes.NameIdentifier, botUser.Id)
                };

                userStore.AddClaimsAsync(botUser, claims);

                BotExtentions.BotId = botUser.Id;
            }
            else
            {
                var bot = _context.Users.FirstOrDefault(x => x.NormalizedUserName == "BOT");
                BotExtentions.BotId = bot.Id;
            }

           _context.SaveChanges();
        }

    }
}
