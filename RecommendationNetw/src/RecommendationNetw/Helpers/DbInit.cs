using RecommendationNetw.Models;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace RecommendationNetw.Helpers
{
    public static class DbInit
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser { UserName = "aaa", Email = "aaa@aaa.com" };
                var result = userManager.CreateAsync(user, "123Aa/");
            }
            
            if (!context.Questions.Any())
            {
                var variants1 = new Variant[]
                {
                    new Variant { NumericValue = 1, TextValue = "Odin"},
                    new Variant { NumericValue = 2, TextValue = "Dwa"},
                    new Variant { NumericValue = 3, TextValue = "Tri"},
                };

                context.Questions.Add(new Question { Category = Category.Art, Text = "Why?", Variants = variants1});
                context.Questions.Add(new Question { Category = Category.Books, Text = "Who?" });
                context.Questions.Add(new Question { Category = Category.Films, Text = "When?"});
                context.Questions.Add(new Question { Category = Category.Music, Text = "What?" });
                context.SaveChanges();
            }
        }
    }
}
