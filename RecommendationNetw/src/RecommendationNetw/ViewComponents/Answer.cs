using Microsoft.AspNet.Mvc;
using RecommendationNetw.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecommendationNetw.ViewComponents
{
    public class Answer : ViewComponent
    {
        public IViewComponentResult Invoke(Question question)
        {            
            return View(question);
        }
    }
}
