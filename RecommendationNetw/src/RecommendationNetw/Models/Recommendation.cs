using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Models
{
    public class Recommendation
    {        
        public string Id { get; set; }
       
        public string Title { get; set; }
        
        public string Description { get; set; }
       
        public string ShortDescription { get; set; }

        public Category Category { get; set; }

        public DateTime PostedOn { get; set; }

        public DateTime Modified { get; set; }

        public bool IsModerated { get; set; }

        public string OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }        

        //public virtual List<Tag> Tags { get; set; }
    }

    public enum Category { Music, Films, Books, Art, Other };
}
