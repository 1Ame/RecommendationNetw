using Microsoft.AspNet.Identity;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using RecommendationNetw.Managers;
using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Services
{
    
    public class RecommendationService<TUser, TAnswer,TSet>
        where TUser : ApplicationUser
        where TAnswer : Answer
        where TSet: Set
    {
        
        private IQueryable<TUser> Users { get;}
        private IQueryable<TAnswer> Answers { get; }
        private ISimilarityMeasure measure { get; }
        private ApplicationDbContext context { get; }

        public RecommendationService(ApplicationDbContext Context, ISimilarityMeasure Measure)
        {
            context = Context;
            Users = Context.Set<TUser>().AsNoTracking();
            Answers = Context.Set<TAnswer>().AsNoTracking();
            measure = Measure;
            
        }

        public async Task<List<string>> GetSimilarUsers2(string userId, Category category)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var result = new List<string>();

            var currentAnswers = await Answers.Where(x => userId.Equals(x.OwnerId) && category.Equals(x.Category))
                .ToDictionaryAsync(x => x.QuestionId, x => x.Value);

            if (currentAnswers.Count() == 0)
                return result;

            var otherUsers = Users.Include(x => x.Answers).AsParallel()
                .Where(x => x.Answers.Any(y => y.Category.Equals(category)))
                .Select(x => new
                {
                    Id = x.Id,
                    Answers = x.Answers
                });

            otherUsers.ForAll(user =>
            {
                if (result.Count >= 20)
                    return;

                var otherAnswers = user.Answers.Where(y => category.Equals(y.Category)).ToDictionary(y => y.QuestionId, y => y.Value);

                if (measure.Calculate(currentAnswers, otherAnswers) >= measure.SimilarityLimit)                
                    result.Add(user.Id);
                
            });

            sw.Stop();
            var resultTime = sw.Elapsed.TotalSeconds;

            return result;
        }
        
        private async Task AddSetAsync(string ownerId, string targetId, double coef, Category category)
        {
            //var set = new Set()
            //{
            //    OwnerUserId = ownerId,
            //    TargetUserId = targetId,
            //    Coeficient = coef,
            //    Category = category
            //};
            
            //context.Entry(set).State = EntityState.Added;

            //await context.SaveChangesAsync();
        }         
    }
}
