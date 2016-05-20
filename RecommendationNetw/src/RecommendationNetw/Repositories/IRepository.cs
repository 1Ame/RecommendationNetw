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
        bool AutoSaveChanges { get; set; }
        IQueryable<T> Items { get; }

        Task<T> FindByIdAsync(Tkey Id);
        Task CreateAsync(T Item);
        Task UpdateAsync(T Item);
        Task DeleteAsync(Tkey Id);
        Task SaveChangesAsync();
    }    
}
