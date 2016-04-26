using Microsoft.AspNet.Mvc;
using RecommendationNetw.Helpers;
using RecommendationNetw.Models;
using RecommendationNetw.Repositories;
using RecommendationNetw.ViewModels.Manage;
using RecommendationNetw.ViewModels.Recommendations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecommendationNetw.ViewComponents
{
    public class RecomList : ViewComponent
    {
        public readonly IRepository<Recommendation, string> repository = null;

        public RecomList(IRepository<Recommendation, string> Repository)
        {
            repository = Repository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page = 1)
        {
            var items = await repository.GetAllAsync(x => x.OwnerId == HttpContext.User.GetUserId());
            var pagingInfo = new PagingInfo(items.Count(), page, 1);

            var model = new ListViewModel()
            {
                Items = items.Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize).Take(pagingInfo.PageSize),
                PagingInfo = pagingInfo
            };

            return View(model);
        }
    }
}
