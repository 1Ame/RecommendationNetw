using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Abstracts
{
    public interface IQuestion:IQuestion<string>
    {

    }

    public interface IQuestion<out TKey>
    {
        TKey Id { get; }
        string Text { get; set; }
    }
}
