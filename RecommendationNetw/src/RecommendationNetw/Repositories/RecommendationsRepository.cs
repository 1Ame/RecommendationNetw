using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Repositories
{
    public class RecommendationsRepository : IRepository<Recommendation, string>
    {
        private readonly ApplicationDbContext _context;

        public RecommendationsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Recommendation> GetAllAsync()
        {
            return _context.Recommendations;
        }
        public Recommendation GetAsync(string Id)
        {
            return _context.Recommendations.FirstOrDefault(x => x.Id == Id);
        }
        public bool Create(Recommendation item)
        {
            try
            {
                _context.Recommendations.Add(item);
                _context.SaveChanges();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
        public bool Update(Recommendation item)
        {
            try
            {
                var dbEntry = _context.Recommendations.FirstOrDefault(x => x.Id == item.Id);

                if (dbEntry == null)
                    return false;

                dbEntry.Title = item.Title;
                dbEntry.Description = item.Description;
                dbEntry.ShortDescription = item.ShortDescription;
                dbEntry.Category = item.Category;
                dbEntry.Modified = item.Modified;

                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public Recommendation Remove(string Id)
        {
            var dbEntry = _context.Recommendations.FirstOrDefault(x => x.Id == Id);
            if (dbEntry != null)
            {
                _context.Recommendations.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        

    }
}
