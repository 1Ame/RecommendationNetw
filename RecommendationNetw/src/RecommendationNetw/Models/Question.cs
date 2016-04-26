using System.ComponentModel.DataAnnotations;

namespace RecommendationNetw.Models
{
    public class Question
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Text { get; set; }

        public Aspect Aspect { get; set; }
    }

    public enum Aspect { aspect1, aspect2, aspect3, asect4};
}
