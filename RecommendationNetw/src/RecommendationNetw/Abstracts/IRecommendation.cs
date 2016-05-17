using Microsoft.AspNet.Identity.EntityFramework;
using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Abstracts
{
    public interface IRecommendation : IRecommendation<string>
    {

    }

    public interface IRecommendation<out TKey>        
    {
        TKey Id { get; }

        string Title { get; set; }

        DateTime PostedOn { get; set; }

        DateTime ModifiedOn { get; set; }        
    }
}
