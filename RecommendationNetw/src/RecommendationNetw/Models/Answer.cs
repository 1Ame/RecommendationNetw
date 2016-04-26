using System.ComponentModel.DataAnnotations;

namespace RecommendationNetw.Models
{
    public class Answer
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name ="Your answer")]
        public int Value { get; set; }

        public string QuestionId { get; set; }
        public Question Question { get; set; }

        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
    }
}
