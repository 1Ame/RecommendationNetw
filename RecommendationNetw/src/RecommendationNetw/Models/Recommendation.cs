using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Models
{
    public class Recommendation
    {        
        [Key]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Full description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Short description")]
        [DataType(DataType.MultilineText)]
        [StringLength(100)]
        public string ShortDescription { get; set; }

        [Required]
        [Display(Name = "Category")]
        public Category Category { get; set; }

        public DateTime PostedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public bool IsModerated { get; set; }
        
        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }        

        //public virtual List<Tag> Tags { get; set; }
    }

    public enum Category { Music, Films, Books, Art, Other };
}
