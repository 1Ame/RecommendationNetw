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
using RecommendationNetw.Managers;
using Microsoft.AspNet.Authorization;

namespace RecommendationNetw.Controllers
{
    [Authorize(Roles = "moderator")]
    public class ModerController : Controller
    {
        private  readonly RecommendationManager<Recommendation> _recomManager;
        private readonly int PageSize = 3;

        public ModerController(RecommendationManager<Recommendation> manager)
        {
            _recomManager = manager;    
        }
                
        public async Task<IActionResult> Index(int page = 1)
        {
            var items = _recomManager.FindAllAsync(x => x.IsModerated.Equals(false));            

            var model = new ListViewModel()
            {
                PagingInfo = new PagingInfo(await items.CountAsync(), page, PageSize),

                Items = await items.Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync(),                
            };
            return View(model);
        }         
    }
}
