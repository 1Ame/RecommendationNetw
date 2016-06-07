using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
using RecommendationNetw.Managers;
using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Services
{
    
    public class RecommendationService<TUser>
        where TUser : ApplicationUser
    {
        public UserManager<ApplicationUser> userManager { get; set; }
        public ISimilarityMeasure measure { get; set; }

        public RecommendationService(UserManager<ApplicationUser> UserManager, ISimilarityMeasure Measure)
        {
            userManager = UserManager;
            measure = Measure;
        }

        public async Task<List<string>> GetSimilarUsers(string userId, Category category)
        {
            var result = new List<string>();

            var currentUser = await userManager.FindByIdWithRefAsync(userId);

            if (currentUser == null)
                return result;

            var currentAnswers = currentUser.Answers.Where(x => category.Equals(x.Category))
                .ToDictionary(x => x.QuestionId, x => x.Value);

            if (currentAnswers.Count() == 0)
                return result;

            var users = userManager.Users.Include(x => x.Answers)
                .Where(x => x.Answers.Any(y => y.Category.Equals(category)));

            await users.ForEachAsync(user =>
            {
                if (result.Count >= 1)
                    return;

                var answers = currentUser.Answers.Where(x => category.Equals(x.Category))
                .ToDictionary(x => x.QuestionId, x => x.Value);

                var coef = measure.Calculate(currentAnswers, answers);
                if  (coef >= measure.SimilarityLimit)
                    result.Add(user.Id);                
            });

            return result;
        }
    }
}
