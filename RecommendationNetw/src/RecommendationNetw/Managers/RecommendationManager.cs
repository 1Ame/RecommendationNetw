using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using RecommendationNetw.Abstracts;
using RecommendationNetw.Models;
using RecommendationNetw.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecommendationNetw.Managers
{
    public class RecommendationManager<T> : RecommendationManager<T, string>
        where T : class, IRecommendation
    {
        public RecommendationManager(IRepository<T> repository, UserManager<ApplicationUser> userManager)
            : base(repository, userManager)
        {
        }
    }

    public class RecommendationManager<T, TKey>
        where T : class, IRecommendation<TKey>
    {

        protected IRepository<T, TKey> Repository { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public RecommendationManager(IRepository<T, TKey> repository, UserManager<ApplicationUser> userManager)
        {
            Repository = repository ;
            UserManager = userManager;
        }

        public async Task<bool> CanGetRecommendations(string userId, Category category)
        {
            var user = await UserManager.FindByIdAsync(userId);

            if (user == null || !user.Answers.Any(x => x.Question.Category == category))
                return false;

            return true;
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await Repository.FindAllAsync(predicate);
        }
        public virtual async Task<T> FindByIdAsync(TKey Id)
        {
            try
            {
                return await Repository.FindByIdAsync(Id);
            }
            catch
            {
                return null;
            }
        }
        public virtual async Task<bool> CreateAsync(T recommendation)
        {
            try
            {
                await Repository.CreateAsync(recommendation);
                return true;
            }
            catch
            {                
                return false;
            }
        }
        public virtual async Task<bool> UpdateAsync(T recommendation)
        {
            try
            {
                await Repository.UpdateAsync(recommendation);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual async Task<bool> DeleteAsync(TKey Id)
        {
            try
            {
                await Repository.DeleteAsync(Id);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
