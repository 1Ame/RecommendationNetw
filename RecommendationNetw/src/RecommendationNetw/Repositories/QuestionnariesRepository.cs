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
    public class QuestionnariesRepository : IRepository<Questionary, Guid>, IQuestionary<Question>
    {
        private readonly ApplicationDbContext _context = null;

        public QuestionnariesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Questionary> Items
        {
            get { return _context.Questionnaires; }
        }
        public async Task<IEnumerable<Questionary>> GetAllAsync()
        {
            return await _context.Questionnaires.ToListAsync();
        }
        public async Task<IEnumerable<Questionary>> GetAllAsync(Expression<Func<Questionary, bool>> predicate)
        {
            return await _context.Questionnaires.Where(predicate).ToListAsync();
        }
        public async Task<Questionary> GetAsync(Guid Id)
        {
            return await _context.Questionnaires.FirstOrDefaultAsync(x => x.Id.Equals(Id));
        }
        public async Task<bool> CreateAsync(Questionary item)
        {
            if (item == null)
                return false;                

            try
            {
                _context.Questionnaires.Add(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateAsync(Questionary item)
        {
            if (item == null)
                return false;

            var dbEntry = await _context.Questionnaires.FirstOrDefaultAsync(x => x.Id.Equals(item.Id));

            if (dbEntry == null)
                return false;

            dbEntry.Answers = item.Answers;           

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
            var dbEntry = _context.Questionnaires.FirstOrDefault(x => x.Id.Equals(Id));

            if (dbEntry == null)
                return false;

            try
            {
                _context.Questionnaires.Remove(dbEntry);
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
            var result = _context.Questions.GroupBy(x => x.Aspect)
                .OrderBy(x => Guid.NewGuid())
                .Select(x => x.FirstOrDefault());
            return await result.ToListAsync();
        }
    }
}
