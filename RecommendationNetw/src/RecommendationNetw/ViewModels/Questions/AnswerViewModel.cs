using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.ViewModels.Questions
{
    public class AnswerViewModel
    {   
        public string AnswerId { get; set; }

        public Category Category { get; set; }

        public string Value { get; set; }

        public string QuestionText { get; set; }

        public string QuestionId { get; set; }
    }
}
