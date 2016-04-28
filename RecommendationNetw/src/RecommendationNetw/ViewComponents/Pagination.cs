using Microsoft.AspNet.Mvc;
using RecommendationNetw.Helpers;
using RecommendationNetw.Models;
using RecommendationNetw.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecommendationNetw.ViewComponents
{
    public class Pagination : ViewComponent
    {
        public IViewComponentResult Invoke(string Action, PagingInfo pagingInfo)
        {
            ViewBag.Action = Action;
            return View(pagingInfo);
        }
    }
}
