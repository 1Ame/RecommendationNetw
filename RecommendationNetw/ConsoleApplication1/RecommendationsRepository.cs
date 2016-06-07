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
    public class RecommendationsRepository<T> : RecommendationsRepository<T, string>, IRepository<T>
        where T : class, IRecommendation
    {
        public RecommendationsRepository(ApplicationDbContext Сontext)
            :base(Сontext)
        {
        }       
    }

    public class RecommendationsRepository<T, TKey> : IRepository<T, TKey> 
        where T : class, IRecommendation<TKey>
        where TKey: IEquatable<TKey>
    {
        private ApplicationDbContext Context { get; }
        public bool AutoSaveChanges { get; set; }
        public IQueryable<T> Items
        {
            get { return Context.Set<T>(); }
        }

        public RecommendationsRepository(ApplicationDbContext Сontext)
        {
            Context = Сontext;
            AutoSaveChanges = true;
        }

        public virtual Task<T> FindByIdAsync(TKey Id)
        {
            return Items.FirstOrDefaultAsync(x=>x.Id.Equals(Id));
        }
        public virtual async Task CreateAsync(T recommendation)
        {
            if (recommendation == null)
                throw new ArgumentNullException("recommendation");

            recommendation.PostedOn = DateTime.Now;
            recommendation.ModifiedOn = recommendation.PostedOn;
            Context.Entry(recommendation).State = EntityState.Added;

            await SaveChangesAsync();
        }
        public virtual async Task UpdateAsync(T recommendation)
        {
            if (recommendation == null)
                throw new ArgumentNullException("recommendation");

            recommendation.ModifiedOn = DateTime.Now;
            Context.Entry(recommendation).State = EntityState.Modified;
            Context.Entry(recommendation).Property(e => e.PostedOn).IsModified = false;

            await SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(TKey id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            var dbEntry = FindByIdAsync(id);

            if (dbEntry != null)
                Context.Entry(dbEntry).State = EntityState.Deleted;

            await SaveChangesAsync();
        }
       
        public async Task SaveChangesAsync()
        {
            if (AutoSaveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }        
    }
}
