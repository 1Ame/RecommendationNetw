using Microsoft.Data.Entity;
using RecommendationNetw.Abstracts;
using RecommendationNetw.Models;
using RecommendationNetw.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecommendationNetw.Repositories
{
    public class QuestionsRepository<T> : IRepository<T>
        where T : class, IQuestion
    {
        public ApplicationDbContext Context { get; private set; }
        public bool AutoSaveChanges { get; set; }
        public IQueryable<T> Questions
        {
            get { return Context.Set<T>(); }
        }

        public QuestionsRepository(ApplicationDbContext Сontext)
        {
            Context = Сontext;
            AutoSaveChanges = true;
        }

        public virtual Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return Questions.Where(predicate).ToListAsync();
        }
        public virtual Task<T> FindByIdAsync(string Id)
        {
            return Questions.FirstOrDefaultAsync(x => x.Id.Equals(Id));
        }

        public virtual async Task CreateAsync(T question)
        {
            if (question == null)
                throw new ArgumentNullException("question");

            Context.Entry(question).State = EntityState.Added;

            await SaveChanges();
        }
        public virtual async Task UpdateAsync(T question)
        {
            if (question == null)
                throw new ArgumentNullException("question");           

            Context.Entry(question).State = EntityState.Modified;            

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
