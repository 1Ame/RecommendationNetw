using Microsoft.Data.Entity;
using RecommendationNetw.Abstracts;
using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Repositories
{
    public class SetsRepository<T> : SetsRepository<T, string>, IRepository<T>
           where T : class, IUsersSet
    {
        public SetsRepository(ApplicationDbContext Сontext)
            : base(Сontext)
        {
        }
    }

    public class SetsRepository<T, TKey> : IRepository<T, TKey>
        where T : class, IUsersSet<TKey>
        where TKey : IEquatable<TKey>
    {
        public ApplicationDbContext Context { get; private set; }

        public bool AutoSaveChanges { get; set; }

        public IQueryable<T> Items
        {
            get { return Context.Set<T>(); }
        }

        public SetsRepository(ApplicationDbContext Сontext)
        {
            Context = Сontext;
            AutoSaveChanges = true;
        }

        public virtual Task<T> FindByIdAsync(TKey Id)
        {
            return Items.FirstOrDefaultAsync(x => Id.Equals(x.Id));
        }

        public virtual async Task CreateAsync(T set)
        {
            if (set == null)
                throw new ArgumentNullException("set");

            Context.Entry(set).State = EntityState.Added;

            await SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T set)
        {
            if (set == null)
                throw new ArgumentNullException("set");

            Context.Entry(set).State = EntityState.Modified;

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

        public virtual async Task DeleteUserSets(TKey userId, Category category)
        {
            if (userId == null)
                throw new ArgumentNullException("id");

            Context.Set<T>().RemoveRange(Context.Set<T>().Where(x => x.Category.Equals(category) && (x.OwnerUserId.Equals(userId) || x.TargetUserId.Equals(userId))));

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
