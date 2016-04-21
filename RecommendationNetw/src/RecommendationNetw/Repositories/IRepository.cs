using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecommendationNetw.Repositories
{
    public interface IRepository<T, in Tkey> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> prediacte);
        Task<T> GetAsync(Tkey Id);
        Task<T> CreateAsync(T item);
        Task<T> UpdateAsync(T item);
        Task<T> DeleteAsync(Tkey Id);
        Task<int> CountAsync();

    }
}
