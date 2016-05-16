using Microsoft.AspNet.Http;
using Microsoft.Data.Entity;
using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecommendationNetw.Repositories
{
    public class RecommendationsRepository : IRepository<Recommendation, Guid>
    {
        private readonly ApplicationDbContext context = null;

        public RecommendationsRepository(ApplicationDbContext Сontext)
        {
            context = Сontext;
        }

        public IQueryable<Recommendation> Items
        {
            get { return context.Recommendations; }
        }            

        public async Task<IEnumerable<Recommendation>> GetAllAsync()
        {
            return await context.Recommendations.ToListAsync();
        }
        public async Task<IEnumerable<Recommendation>> GetAllAsync(Expression<Func<Recommendation, bool>> predicate)
        {
            return await context.Recommendations.Where(predicate).ToListAsync();
        }
        public async Task<Recommendation> GetAsync(Guid Id)
        {
            return await context.Recommendations.FirstOrDefaultAsync(x=>x.Id.Equals(Id));
        }
        public async Task<bool> CreateAsync(Recommendation item)
        {
            if (item == null)
                return false;

            item.PostedOn = DateTime.Now;
            item.ModifiedOn = item.PostedOn;
            context.Entry(item).State = EntityState.Added;

            try
            {
                //context.Recommendations.Add(item);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }            
        }
        public async Task<bool> UpdateAsync(Recommendation item)
        {
            if (item == null)
                return false;
            
            item.ModifiedOn = DateTime.Now;
            context.Entry(item).State = EntityState.Modified;
            context.Entry(item).Property(e => e.PostedOn).IsModified = false;
            context.Entry(item).Property(e => e.OwnerId).IsModified = false;

            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }        
        }
        public async Task<bool> DeleteAsync(Guid Id)
        {
            var dbEntry = context.Recommendations.FirstOrDefault(x => x.Id.Equals(Id));            
            context.Entry(dbEntry).State = EntityState.Deleted;

            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }           
        }        
    }
}
