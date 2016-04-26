using Microsoft.Data.Entity;
using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Services
{
    public class QuestionaryManager : IQuestionary
    {
        private readonly IQueryable<Question> questions = null;

        public QuestionaryManager(IQueryable<Question> Questions)
        {
            questions = Questions;
        }

        public async Task<IEnumerable<Question>> GenerateQuestionary()
        {
            Random rnd = new Random();
            var result = questions.GroupBy(x => x.Aspect).OrderBy(x=>Guid.NewGuid()).Select(x=>x.FirstOrDefault());
            return await result.ToListAsync();
        }
    }
}
