using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecommendationNetw.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public Category Category { get; set; }

        public ICollection<Variant> Variants { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}

