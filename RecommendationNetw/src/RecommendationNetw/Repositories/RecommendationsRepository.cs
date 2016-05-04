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
            item.ModifiedOn = DateTime.Now;

            try
            {
                context.Recommendations.Add(item);
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
                 
            var dbEntry = await context.Recommendations.FirstOrDefaultAsync(x => x.Id.Equals(item.Id));

            if (dbEntry == null)
                return false;
                                       
            dbEntry.Title = item.Title;
            dbEntry.Description = item.Description;
            dbEntry.ShortDescription = item.ShortDescription;
            dbEntry.Category = item.Category;
            dbEntry.ModifiedOn = DateTime.Now;

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

            if (dbEntry == null)
                return false;

            try
            {
                context.Recommendations.Remove(dbEntry);
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
