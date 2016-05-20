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
    public class AnswerManager<T> : AnswerManager<T, string>
       where T : Answer
    {
        public AnswerManager(IRepository<T> repository)
            : base(repository)
        {
        }        
    }

    public class AnswerManager<TAnswer, TKey>
        where TAnswer : Answer<TKey>
        where TKey : IEquatable<TKey>
    {
        protected IRepository<TAnswer, TKey> repository { get; set; }

        public IQueryable<TAnswer> Answers { get; }

        public AnswerManager(IRepository<TAnswer, TKey> Repository)
        {
            repository = Repository;
            Answers = repository.Items;
        }

        public virtual IQueryable<TAnswer> FindAllAsync(Expression<Func<TAnswer, bool>> predicate)
        {
            return repository.Items.Where(predicate);
        }
        public virtual IQueryable<TAnswer> FindAllWithRefAsync(Expression<Func<TAnswer, bool>> predicate)
        {             
            return repository.Items.Include(x=>x.Question.Variants).Where(predicate);
        }

        public virtual Task<TAnswer> FindByIdAsync(TKey Id)
        {
            try
            {
                return repository.FindByIdAsync(Id);
            }
            catch
            {
                return null;
            }
        }
        public virtual async Task<bool> CreateAsync(TAnswer answer)
        {
            try
            {
                await repository.CreateAsync(answer);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual async Task<bool> CreateRangeAsync(IEnumerable<TAnswer> answers)
        {
            try
            {
                repository.AutoSaveChanges = false;

                foreach (var answer in answers)
                    await repository.CreateAsync(answer);

                repository.AutoSaveChanges = true;

                await repository.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual async Task<bool> EditRangeAsync(IEnumerable<TAnswer> answers)
        {
            try
            {
                repository.AutoSaveChanges = false;

                foreach (var answer in answers)
                    await repository.UpdateAsync(answer);

                repository.AutoSaveChanges = true;

                await repository.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual async Task<bool> UpdateAsync(TAnswer answer)
        {
            try
            {
                await repository.UpdateAsync(answer);
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
                await repository.DeleteAsync(Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual Task<bool> HaveAnswersInCategory(TKey userId, Category category)
        {
            return repository.Items.Include(x => x.Question).AnyAsync(x => userId.Equals(x.OwnerId) && category.Equals(x.Question.Category));
        }
    }
}
