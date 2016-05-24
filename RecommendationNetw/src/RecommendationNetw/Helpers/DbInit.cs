using RecommendationNetw.Models;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Collections.Generic;

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
                var user = new ApplicationUser { UserName = "aaa", Email = "aaa@aaa.com" };
                var user1 = new ApplicationUser { UserName = "bbb", Email = "bbb@aaa.com" };
                var result = await userManager.CreateAsync(user, "123Aa/");
                result = await userManager.CreateAsync(user1, "123Aa/");
                if (result.Succeeded)
                {
                    var dbEntry = await userManager.FindByNameAsync(user.UserName);
                    await userManager.AddToRoleAsync(dbEntry, "moder");
                }
            }
            
            if (!context.Questions.Any())
            {
                var question1 = new Question() {Category = Category.Art, Text = "Why?" };
                var question2 = new Question() {Category = Category.Art, Text = "Who?" };
                var question3 = new Question() {Category = Category.Art, Text = "When?" };

                var variants = new List<Variant>() {
                    new Variant() { NumericValue = 1, TextValue = "WhyNot", QuestionId = question1.Id },
                    new Variant() { NumericValue = 2, TextValue = "NotWhy", QuestionId = question1.Id },

                    new Variant() { NumericValue = 1, TextValue = "He", QuestionId = question2.Id },
                    new Variant() { NumericValue = 2, TextValue = "She", QuestionId = question2.Id },

                    new Variant() { NumericValue = 1, TextValue = "In 2015", QuestionId = question3.Id },
                    new Variant() { NumericValue = 2, TextValue = "In 2016", QuestionId = question3.Id }
                };

                context.Questions.Add(question1);
                context.Questions.Add(question2);
                context.Questions.Add(question3);
                context.Variants.AddRange(variants);
            }
            await context.SaveChangesAsync();
        }
    }
}
