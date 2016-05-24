using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using RecommendationNetw.Models;

namespace RecommendationNetw.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasMany(x => x.Answers).WithOne(x => x.Owner).HasForeignKey(x => x.OwnerId);
            builder.Entity<ApplicationUser>().HasMany(x => x.Recommendations).WithOne(x => x.Owner).HasForeignKey(x => x.OwnerId);
            builder.Entity<Question>().HasMany(x => x.Variants).WithOne(x => x.Question).HasForeignKey(x => x.QuestionId);
            builder.Entity<Question>().HasMany(x => x.Answers).WithOne(x => x.Question).HasForeignKey(x => x.QuestionId);

            base.OnModelCreating(builder);
        }

        public DbSet<Recommendation> Recommendations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Variant> Variants { get; set; }        
    }
}
