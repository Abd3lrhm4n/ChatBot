using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chatbot.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Message> Messages { get; set; }

        public DbSet<WordClassification> WordClassifications { get; set; }

        public DbSet<Classification> Classifications { get; set; }

        public DbSet<BotSpeech> BotSpeeches { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<WordCountUse> WordCountUses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Message>()
                .HasOne(m => m.Bot)
                .WithMany()
                .HasForeignKey(m => m.BotId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Message>()
                .HasOne(m => m.Human)
                .WithMany()
                .HasForeignKey(m => m.HumanId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<BotSpeech>()
                .HasOne(c => c.Classification)
                .WithMany()
                .HasForeignKey(c => c.ClassificationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Message>().HasIndex(h => h.HumanId).HasName("HumanIdIndex");
            builder.Entity<Message>().HasIndex(m => m.MessageBody).HasName("MessageBodyIndex");

            builder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18, 3)");
            builder.Entity<Product>().HasIndex(n => n.NameAr).HasName("ProductNameArIndex");
            builder.Entity<Product>().HasIndex(n => n.NameEn).HasName("ProductNameEnIndex");
            builder.Entity<Product>()
                .HasIndex(n => n.Price)
                .HasName("ProductPriceIndex").
                IncludeProperties(n => new { n.NameAr, n.NameEn });

            builder.Entity<WordClassification>()
                .HasIndex(w => w.ClassificationId)
                .HasName("WordClassificationIdIndex")
                .IncludeProperties(w => w.Word);

            builder.Entity<WordCountUse>()
                .HasIndex(w => w.Word)
                .HasName("WordCountUseNameIndex")
                .IncludeProperties(c => c.Count);
        }
    }
}
