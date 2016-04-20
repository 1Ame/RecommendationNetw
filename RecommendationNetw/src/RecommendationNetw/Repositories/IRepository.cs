using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Repositories
{
    public interface IRepository<T, in Tkey> where T : class
    {
        IEnumerable<T> GetAllAsync();
        T GetAsync(Tkey Id);
        bool Create(T item);
        bool Update(T item);
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
