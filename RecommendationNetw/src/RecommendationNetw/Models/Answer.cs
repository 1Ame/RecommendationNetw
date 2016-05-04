using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecommendationNetw.Models
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [Display(Name ="Your answer")]
        public TextAnswer Value { get; set; }
        
        public string QuestionId { get; set; }
        public Question Question { get; set; }        
    }

    public enum TextAnswer { value1, value2, value3, value4 };
}
