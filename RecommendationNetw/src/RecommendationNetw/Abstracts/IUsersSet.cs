using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Abstracts
{
    public interface IUsersSet : IUsersSet<string>
    {

    }

    public interface IUsersSet<TKey>
    {
        TKey Id { get; set; }

        TKey OwnerUserId { get; set; }

        TKey TargetUserId { get; set; }

        Category Category { get; set; }
    }
}
