using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecommendationNetw.Models
{
    public class Answer
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public int Value { get; set; }

        public string QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public string OwnerId { get; set; }
        public virtual ApplicationUser Owner { get; set; }     
    } 

}
