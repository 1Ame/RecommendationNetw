using System.Linq;
using Microsoft.AspNet.Mvc;
using RecommendationNetw.Models;
using System.Threading.Tasks;
using RecommendationNetw.Helpers;
using RecommendationNetw.ViewModels.Recommendations;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;
using RecommendationNetw.Managers;
using Microsoft.Data.Entity;
using RecommendationNetw.Services;

namespace RecommendationNetw.Controllers
{
    [Authorize]
    public class RecommendationsController : Controller
    {
        private readonly RecommendationManager<Recommendation> _recomManager;
        private readonly RecommendationService<ApplicationUser,Answer,Set> _service = null;

        private readonly int PageSize = 3;

        public RecommendationsController(RecommendationManager<Recommendation> manager, RecommendationService<ApplicationUser, Answer, Set> service)
        {
            _recomManager = manager;
            _service = service;            
        }

        // GET: Recommendations
        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = HttpContext.User.GetUserId();

            var items = _recomManager.FindAllAsync(x => userId.Equals(x.OwnerId));            

            var model = new ListViewModel()
            {
                PagingInfo = new PagingInfo(await items.CountAsync(), page, PageSize),

                Items = await items.Skip((page - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync(),                
            };

            return View(model);
        }        

        // GET: Recommendations/Details/5
        public async Task<IActionResult> Details(string Id)
        {           
            if (Id == null)
            {
                return HttpNotFound();
            }
            var recommendation = await _recomManager.FindByIdAsync(Id);

            if (recommendation == null)
            {
                return HttpNotFound();
            }
            return View(recommendation);
        }

        // GET: Recommendations/Create
        public IActionResult Create()
        {
            return View(new Recommendation());
        }

        // POST: Recommendations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, ShortDescription, Description, Category")] Recommendation model)
        {
            if (ModelState.IsValid)
            {
                model.OwnerId = HttpContext.User.GetUserId();
                var result = await _recomManager.CreateAsync(model);
                TempData["opertionResult"] = (result) ? "Recommendation saved." : "Some Error Message";
            }
            return RedirectToAction("Index");
        }
        
        // GET: Recommendations/Edit/Id
        public async Task<IActionResult> Edit(string Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }

            var userId = HttpContext.User.GetUserId();
            var model = await _recomManager.FindByIdAsync(Id);

            if (model == null || !model.OwnerId.Equals(userId))
            {
                return HttpNotFound();
            } 
                       
            return View(model);
        }

        // POST: Recommendations/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id, Title, ShortDescription, Description, Category")] Recommendation model)
        {
            if (ModelState.IsValid)
            {
                model.OwnerId = HttpContext.User.GetUserId();
                var result = await _recomManager.UpdateAsync(model);
                TempData["opertionResult"] = (result) ? "Recommendation saved." : "Some Error Message";
            }
            return RedirectToAction("Index");
        }

        //POST: Recommendations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string Id)
        {
            var result = await _recomManager.DeleteAsync(Id);
            TempData["opertionResult"] = result ? "Recommendation deleted.": "Some Error Message";
            return RedirectToAction("Index");
        }

        //GET: Recommendations/Delete/5
        public async Task<IActionResult> Delete(string Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }

            var userId = HttpContext.User.GetUserId();
            var model = await _recomManager.FindByIdAsync(Id);

            if (model == null || !model.OwnerId.Equals(userId))
            {
                return HttpNotFound();
            }

            return PartialView("_DeleteConfirm", model);
        }

        public async Task<IActionResult> GetRecommendations(Category category)
        {
            if (category == 0)
            {
                return HttpNotFound();
            }
           
            var recommendations2 = await _service.GetSimilarUsers2(User.GetUserId(), category);



            return View();
        }
    }
}
