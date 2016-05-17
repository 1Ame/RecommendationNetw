using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecommendationNetw.Repositories
{
    public interface IRepository<T> : IRepository<T, string> where T : class
    {
    }

    public interface IRepository<T, in Tkey> where T : class
    {
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> prediacte);
        Task<T> FindByIdAsync(Tkey Id);
        Task CreateAsync(T Item);
        Task UpdateAsync(T Item);
        Task DeleteAsync(Tkey Id);
    }    
}
