using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Services
{
    public interface IMeasure<in T>
    {
        double Calculate(T[] x, T[] y);
    }
}
