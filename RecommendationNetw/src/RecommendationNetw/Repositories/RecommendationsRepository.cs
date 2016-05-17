using Microsoft.AspNet.Http;
using Microsoft.Data.Entity;
using RecommendationNetw.Abstracts;
using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecommendationNetw.Repositories
{
    public class RecommendationsRepository<T> : IRepository<T> 
        where T : class, IRecommendation
    {
        public ApplicationDbContext Context { get; private set; }
        public bool AutoSaveChanges { get; set; }
        public IQueryable<T> Recommendations
        {
            get { return Context.Set<T>(); }
        }

        public RecommendationsRepository(ApplicationDbContext Сontext)
        {
            Context = Сontext;
            AutoSaveChanges = true;
        }
        
        public virtual Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return Recommendations.Where(predicate).ToListAsync();
        }
        public virtual Task<T> FindByIdAsync(string Id)
        {
            if(Id == null)
                throw new ArgumentNullException("recommendation");

            return Recommendations.FirstOrDefaultAsync(x=>x.Id.Equals(Id));
        }
        public virtual async Task CreateAsync(T recommendation)
        {
            if (recommendation == null)
                throw new ArgumentNullException("recommendation");

            recommendation.PostedOn = DateTime.Now;
            recommendation.ModifiedOn = recommendation.PostedOn;
            Context.Entry(recommendation).State = EntityState.Added;

            await SaveChanges();
        }
        public virtual async Task UpdateAsync(T recommendation)
        {
            if (recommendation == null)
                throw new ArgumentNullException("recommendation");

            recommendation.ModifiedOn = DateTime.Now;

            Context.Entry(recommendation).State = EntityState.Modified;
            Context.Entry(recommendation).Property(e => e.PostedOn).IsModified = false;

            await SaveChanges();
        }
        public virtual async Task DeleteAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            var dbEntry = FindByIdAsync(id);

            if (dbEntry != null)
                Context.Entry(dbEntry).State = EntityState.Deleted;

            await SaveChanges();
        }

        private async Task SaveChanges()
        {
            if (AutoSaveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }
    }
}
