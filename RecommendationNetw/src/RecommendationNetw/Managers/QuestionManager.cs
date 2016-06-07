using Microsoft.Data.Entity;
using RecommendationNetw.Models;
using RecommendationNetw.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RecommendationNetw.Managers
{
    public class QuestionManager<TQuestion> : QuestionManager<TQuestion, string>
        where TQuestion : Question
    {
        public QuestionManager(IRepository<TQuestion> repository)
            : base(repository)
        {
        }
    }

    public class QuestionManager<TQuestion, TKey>
        where TQuestion : Question<TKey>
        where TKey : IEquatable<TKey>
    {

        protected IRepository<TQuestion, TKey> _repository { get; set; }
        public IQueryable<TQuestion> Questions { get; }

        public QuestionManager(IRepository<TQuestion, TKey> repository)
        {
            _repository = repository;
            Questions = repository.Items;
        }

        public virtual IQueryable<TQuestion> FindAllAsync(Expression<Func<TQuestion, bool>> predicate)
        {
            return _repository.Items.Where(predicate);
        }
        public virtual IQueryable<TQuestion> FindAllWithRefAsync(Expression<Func<TQuestion, bool>> predicate)
        {
            return _repository.Items.Include(x=>x.Variants).Where(predicate);
        }

        public virtual async Task<TQuestion> FindByIdAsync(TKey Id)
        {
            try
            {
                return await _repository.FindByIdAsync(Id);
            }
            catch
            {
                return null;
            }
        }

        public virtual async Task<bool> CreateAsync(TQuestion question)
        {
            try
            {
                await _repository.CreateAsync(question);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual async Task<bool> UpdateAsync(TQuestion question)
        {
            try
            {
                await _repository.UpdateAsync(question);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual async Task<bool> DeleteAsync(TKey Id)
        {
            try
            {
                await _repository.DeleteAsync(Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual async Task<IEnumerable<TQuestion>> FindByCategory(Category category)
        {
            return await _repository.Items.Where(x => x.Category.Equals(category)).ToListAsync();
        }
    }
}
