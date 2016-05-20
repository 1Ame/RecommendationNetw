using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Abstracts
{

    public interface IAnswer : IAnswer<string>
    {

    }

    // Minimal Answer model
    public interface IAnswer<TKey>
    {
       TKey Id { get; }

       Category Category { get; set; }
    }

}
