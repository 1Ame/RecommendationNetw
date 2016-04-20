using RecommendationNetw.Helpers;
using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.ViewModels.Recommendations
{
    public class IndexViewModel
    {
        public IEnumerable<Recommendation> Items { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
