using RecommendationNetw.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Models
{
    public class Set : Set<string>, IUsersSet
    {
        public Set()
        {
            Id = Guid.NewGuid().ToString();            
        }
    }

    public class Set<TKey> : IUsersSet<TKey>
    {
        public TKey Id { get; set; }

        [Required]
        public TKey OwnerUserId { get; set; }

        [Required]
        public TKey TargetUserId { get; set; }

        [Required]
        public double Coeficient { get; set; }

        [Required]
        public Category Category { get; set; }

        //owner answers last modification
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OwnerModifiedOn { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TargetModifiedOn { get; set; }
    }
}
