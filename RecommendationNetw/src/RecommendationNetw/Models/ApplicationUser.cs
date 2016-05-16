using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecommendationNetw.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {        
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Recommendation> Recommendations { get; set; }
    }
}
