using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Services
{
    public interface IRecommendationManager
    {
        IEnumerable<Recommendation> GetRecommendations(string Id);
    }
}
