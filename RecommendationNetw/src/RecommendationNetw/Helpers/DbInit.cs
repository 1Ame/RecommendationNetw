using RecommendationNetw.Models;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace RecommendationNetw.Helpers
{
    public static class DbInit
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (!roleManager.Roles.Any())
            {
                var role = new IdentityRole();
                role.Name = "moder";
                var result = await roleManager.CreateAsync(role);
            }

            if (!userManager.Users.Any())
            {
                for(int i = 0; i < 100; i++)
                {
                    var user = new ApplicationUser { UserName = "aaa" + i, Email = "aaa" + i + "@aaa.com" };
                    var result = await userManager.CreateAsync(user, "123Aa/");
                    if (result.Succeeded)
                    {
                        Debug.WriteLine("User {0} added.", i);                        
                    }
                }
                await context.SaveChangesAsync();
            }


            if (!context.Questions.Any())
            {
                for (int i = 0; i < 20; i++)
                {
                    var question = new Question() { Category = Category.Art, Text = "Question" + i };
                    context.Questions.Add(question);

                    var variants = new List<Variant>()
                    {
                        new Variant() { NumericValue = 1, TextValue = "FirstVariant"+i, QuestionId = question.Id },
                        new Variant() { NumericValue = 2, TextValue = "SecondVariant"+i, QuestionId = question.Id },                   
                        new Variant() { NumericValue = 3, TextValue = "ThirdVariant" + i, QuestionId = question.Id }
                    };
                    context.Variants.AddRange(variants);
                }
                await context.SaveChangesAsync();
            }


            if (!context.Answers.Any())
            {
                Random rnd = new Random();
                foreach (var user in context.Users)
                {
                    foreach (var question in context.Questions)
                        context.Answers.Add(new Answer()
                        {
                            Category = Category.Art,
                            QuestionId = question.Id,
                            OwnerId = user.Id,
                            Value = rnd.Next(1, 10)
                        });
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
