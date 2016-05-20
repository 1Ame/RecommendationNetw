using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Abstracts
{
    public interface IVariant : IVariant<string>
    {

    }

    // Minimal Variant model
    public interface IVariant<TKey>
    {
        TKey Id { get; }

        int NumericValue { get; set; }

        string TextValue { get; set; }
    }
}
