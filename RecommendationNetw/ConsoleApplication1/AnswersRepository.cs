using Microsoft.Data.Entity;
using RecommendationNetw.Abstracts;
using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Repositories
{
    public class AnswersRepository<T> : AnswersRepository<T, string>, IRepository<T>
           where T : class, IAnswer
    {
        public AnswersRepository(ApplicationDbContext Сontext)
            : base(Сontext)
        {
        }        
    }

    public class AnswersRepository<T, TKey> : IRepository<T, TKey>
        where T : class, IAnswer<TKey>
        where TKey: IEquatable<TKey>
    {
        public ApplicationDbContext Context { get; private set; }
        public bool AutoSaveChanges { get; set; }
        public IQueryable<T> Items
        {
            get { return Context.Set<T>(); }
        }

        public AnswersRepository(ApplicationDbContext Сontext)
        {
            Context = Сontext;
            AutoSaveChanges = true;
        }

        public virtual Task<T> FindByIdAsync(TKey Id)
        {
            return Items.FirstOrDefaultAsync(x => Id.Equals(x.Id));
        }
        public virtual async Task CreateAsync(T answer)
        {
            if (answer == null)
                throw new ArgumentNullException("answer");
            
            Context.Entry(answer).State = EntityState.Added;

            await SaveChangesAsync();
        }
        public virtual async Task UpdateAsync(T answer)
        {
            if (answer == null)
                throw new ArgumentNullException("answer");

            Context.Entry(answer).State = EntityState.Modified;

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
