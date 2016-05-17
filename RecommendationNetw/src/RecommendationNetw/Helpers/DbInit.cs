using RecommendationNetw.Models;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace RecommendationNetw.Helpers
{
    public static class DbInit
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (!roleManager.Roles.Any())
            {
                var role = new IdentityRole();
                role.Name = "moder";
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser { UserName = "aaa", Email = "aaa@aaa.com" };
                var result = userManager.CreateAsync(user, "123Aa/").Result;
                if (result.Succeeded)
                {
                    var dbEntry = userManager.FindByNameAsync(user.UserName).Result;
                    userManager.AddToRoleAsync(dbEntry, "moder");
                }
            }
            
            if (!context.Questions.Any())
            {
                var question = new Question {Id = Guid.NewGuid().ToString(), Category = Category.Art, Text = "Why?" };
                var variant = new Variant { Id = Guid.NewGuid().ToString(), NumericValue = 1, TextValue = "Odin", QuestionId = question.Id };
                context.Questions.Add(question);
                context.Variants.Add(variant);
                context.SaveChanges();
            }
        }
    }
}
