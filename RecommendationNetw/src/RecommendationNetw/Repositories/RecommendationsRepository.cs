using Microsoft.Data.Entity;
using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecommendationNetw.Repositories
{
    public class RecommendationsRepository : IRepository<Recommendation, string>
    {
        private readonly ApplicationDbContext context = null;

        public RecommendationsRepository(ApplicationDbContext Сontext)
        {
            context = Сontext;
        }

        public async Task<IEnumerable<Recommendation>> GetAllAsync()
        {
            return await context.Recommendations.ToListAsync();
        }
        public async Task<IEnumerable<Recommendation>> GetAllAsync(Expression<Func<Recommendation, bool>> predicate)
        {
            return await context.Recommendations.Where(predicate).ToListAsync();
        }
        public async Task<Recommendation> GetAsync(string Id)
        {
            return await context.Recommendations.FirstOrDefaultAsync(x=>x.Id == Id);
        }
        public async Task<Recommendation> CreateAsync(Recommendation item)
        {            
            context.Recommendations.Add(item);
            await context.SaveChangesAsync();
            return item;            
        }
        public async Task<Recommendation> UpdateAsync(Recommendation item)
        {
            
            if (item == null)
                return null;
                 
            var dbEntry = await context.Recommendations.FirstOrDefaultAsync(x => x.Id == item.Id);

            if (dbEntry != null)
            {                
                dbEntry.Title = item.Title;
                dbEntry.Description = item.Description;
                dbEntry.ShortDescription = item.ShortDescription;
                dbEntry.Category = item.Category;
                dbEntry.ModifiedOn = DateTime.Now;

                await context.SaveChangesAsync();
            }
            return dbEntry;            
        }
        public async Task<Recommendation> DeleteAsync(string Id)
        {
            var dbEntry = context.Recommendations.FirstOrDefault(x => x.Id == Id);
            if (dbEntry != null)
            {
                context.Recommendations.Remove(dbEntry);
                await context.SaveChangesAsync();
            }
            return dbEntry;
        }
        public async Task<int> CountAsync()
        {
            return await context.Recommendations.CountAsync();
        }



    }
}
