using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Services
{
    public interface IMeasure : IMeasure<IDictionary<string, int>>
    {
        
    }

    public interface IMeasure<in TMassive>
    {
        double Calculate(TMassive x, TMassive y);
    }
}
