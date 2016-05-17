using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecommendationNetw.Models
{
    public class Question
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public Category Category { get; set; }

        public virtual ICollection<Variant> Variants { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}

