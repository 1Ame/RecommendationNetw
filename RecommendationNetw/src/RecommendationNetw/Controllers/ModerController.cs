using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using RecommendationNetw.Models;
using RecommendationNetw.Repositories;
using RecommendationNetw.Helpers;
using RecommendationNetw.ViewModels.Recommendations;
using System.Security.Claims;

namespace RecommendationNetw.Controllers
{
    
    public class ModerController : Controller
    {
        private  readonly IRepository<Recommendation, string> repository = null ;

        public ModerController(IRepository<Recommendation, string> Repository)
        {
            repository = Repository;    
        }

        // GET: Recommendations1
        public async Task<IActionResult> Index(int? page)
        {
            var items = (await repository.GetAllAsync(x => x.IsModerated == false)).OrderBy(x => x.Owner);
            var pagingInfo = new PagingInfo(items.Count(), page, 1);

            var model = new ListViewModel()
            {
                Items = items.Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize).Take(pagingInfo.PageSize),
                PagingInfo = pagingInfo
            };
            return View(model);
        }

        // GET: Recommendations1/Details/5
        
    }
}
