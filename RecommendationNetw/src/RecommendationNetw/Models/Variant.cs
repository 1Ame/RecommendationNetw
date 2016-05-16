using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Models
{
    public class Variant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public int NumericValue { get; set; }

        [Required]
        public string TextValue { get; set; }

        public string QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
