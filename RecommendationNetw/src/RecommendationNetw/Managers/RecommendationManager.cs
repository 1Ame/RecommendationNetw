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
    public class RecommendationManager<T> : RecommendationManager<T, string>
        where T : Recommendation
    {
        public RecommendationManager(IRepository<T> repository)
            : base(repository)
        {
        }
    }

    public class RecommendationManager<TRecom, TKey>
        where TRecom :  Recommendation<TKey>
        where TKey : IEquatable<TKey>
    {
        protected IRepository<TRecom, TKey> _repository { get; }
        public IQueryable<TRecom> Recommendations { get; }

        public RecommendationManager(IRepository<TRecom, TKey> repository)
        {
            _repository = repository;
            Recommendations = repository.Items;
            
        }

        public virtual IQueryable<TRecom> FindAllAsync(Expression<Func<TRecom, bool>> predicate)
        {     
            return _repository.Items.Where(predicate);
        }
        public virtual Task<TRecom> FindByIdAsync(TKey Id)
        {
            try
            {
                return _repository.FindByIdAsync(Id);
            }
            catch
            {
                return null;
            }
        }        

        public virtual async Task<bool> CreateAsync(TRecom recommendation)
        {
            try
            {                
                await _repository.CreateAsync(recommendation);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual async Task<bool> UpdateAsync(TRecom recommendation)
        {
            try
            {
                await _repository.UpdateAsync(recommendation);
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
            catch(Exception e)
            {
                return false;
            }
        }

        public virtual async Task<bool> SetModerationValue(TKey Id, bool Value)
        {
            try
            {
                var dbEntry = await _repository.FindByIdAsync(Id);

                if (dbEntry == null)
                    return false;

                dbEntry.IsModerated = Value;

                await _repository.UpdateAsync(dbEntry);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}

