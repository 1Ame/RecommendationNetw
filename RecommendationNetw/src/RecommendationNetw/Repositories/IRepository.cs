using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecommendationNetw.Repositories
{
    interface IRepository<T, in Tkey> where T : class
    {
        IQueryable<T> Items { get; }

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Tkey Id);
        T Create(T item);
        T Update(T item);
        T Remove(Tkey Id);

        //maybe some extencions
        //Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);

        //realisation like repos.GetSingleAsync(x=>x.Category = someCategory)
        //Task<IEnumerable<T>> GetSingleAsync(Expression<Func<T, bool>> predicate);        
        //{
        //    var result = Items.FirstOrDefault(predicate);
        //    return await result.ToAsyncEnumerable();
        //}

    }
}
