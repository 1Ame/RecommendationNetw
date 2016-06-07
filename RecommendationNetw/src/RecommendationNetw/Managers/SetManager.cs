using RecommendationNetw.Models;
using RecommendationNetw.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Managers
{
    public class SetManager<T> : SetManager<T, string>
        where T : Set
    {
        public SetManager(IRepository<T> repository)
            : base(repository)
        {
        }
    }

    public class SetManager<TSet, TKey>
        where TSet:Set<TKey>
        where TKey:IEquatable<TKey>
    {
        protected IRepository<TSet, TKey> repository { get; }
        public IQueryable<TSet> Sets { get; }

        public SetManager(IRepository<TSet, TKey> Repository)
        {
            repository = Repository;
            Sets = repository.Items;
        }

        public virtual async Task<bool> DeleteUserSets(TKey userId, Category category)
        {
            try
            {
                await (repository as SetsRepository<TSet,TKey>).DeleteUserSets(userId, category);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
