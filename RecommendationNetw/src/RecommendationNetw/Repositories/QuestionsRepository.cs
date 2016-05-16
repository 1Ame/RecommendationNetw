using Microsoft.Data.Entity;
using RecommendationNetw.Models;
using RecommendationNetw.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecommendationNetw.Repositories
{
    public class QuestionsRepository : IRepository<Question, Guid>
    {
        private readonly ApplicationDbContext _context = null;

        public QuestionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Question> Items
        {
            get { return _context.Questions; }
        }
        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            return await _context.Questions.ToListAsync();
        }
        public async Task<IEnumerable<Question>> GetAllAsync(Expression<Func<Question, bool>> predicate)
        {
            return await _context.Questions.Where(predicate).ToListAsync();
        }
        public async Task<Question> GetAsync(Guid Id)
        {
            return await _context.Questions.FirstOrDefaultAsync(x => x.Id.Equals(Id));
        }
        public async Task<bool> CreateAsync(Question item)
        {
            if (item == null)
                return false;

            try
            {
                _context.Questions.Add(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateAsync(Question item)
        {
            if (item == null)
                return false;

            var dbEntry = await _context.Questions.FirstOrDefaultAsync(x => x.Id == item.Id);

            if (dbEntry == null)
                return false;

            dbEntry.Category = item.Category;
            dbEntry.Text = item.Text;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public async Task<bool> DeleteAsync(Guid Id)
        {
            var dbEntry = _context.Questions.FirstOrDefault(x => x.Id.Equals(Id));

            if (dbEntry == null)
                return false;

            try
            {
                _context.Questions.Remove(dbEntry);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Question>> GenerateQuestionary()
        {
            var result = _context.Questions.GroupBy(x => x.Category)
                .OrderBy(x => Guid.NewGuid())
                .Select(x => x.FirstOrDefault());
            return await result.ToListAsync();
        }
    }
}
