using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.Models
{
    public class Answer
    {
        public string Id { get; set; }

        public int Mark { get; set; }

        public Question Question { get; set; }

        public ApplicationUser Owner { get; set; }
    }
}
