using RecommendationNetw.Abstracts;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace RecommendationNetw.Models
{
    public class Answer : Answer<string>, IAnswer
    {
        public Answer()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
    public class Answer<TKey> : IAnswer<TKey>
    { 
        [Key]
        public TKey Id { get; set; }

        [Required]
        public int Value { get; set; }

        [Required]
        public Category Category { get; set; }

        public string QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }

        
    } 

}
