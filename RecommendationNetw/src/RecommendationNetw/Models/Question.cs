using RecommendationNetw.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecommendationNetw.Models
{
    public class Question : Question<string>, IQuestion
    {
        public Question()
        {
            Id = Guid.NewGuid().ToString();
        }
    }

    public class Question<TKey> : IQuestion<TKey>
    {        

        [Key]
        public TKey Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public Category Category { get; set; }

        public virtual ICollection<Variant> Variants { get; set; } = new List<Variant>();
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}

