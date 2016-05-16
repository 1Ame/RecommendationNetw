using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using RecommendationNetw.Models;
using RecommendationNetw.Repositories;
using System.Threading.Tasks;
using RecommendationNetw.Helpers;
using RecommendationNetw.ViewModels.Recommendations;
using System;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.AspNet.Authorization;

namespace RecommendationNetw.Controllers
{
    [Authorize]
    public class RecommendationsController : Controller
    {
        private readonly IRepository<Recommendation, Guid> _repository = null;

        public RecommendationsController(IRepository<Recommendation, Guid> repository)
        {
            _repository = repository;
        }

        // GET: Recommendations
        public async Task<IActionResult> Index(int page = 1)
        {
            var items = await _repository.GetAllAsync(x => x.OwnerId.Equals(User.GetUserId()));
            var pagingInfo = new PagingInfo(items.Count(), page, 3);

            var model = new ListViewModel()
            {
                Items = items.Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize).Take(pagingInfo.PageSize),
                PagingInfo = pagingInfo
            };

            return View(model);
        }        

        // GET: Recommendations/Details/5
        public async Task<IActionResult> Details(Guid Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }

            var recommendation = await _repository.GetAsync(Id);
            if (recommendation == null)
            {
                return HttpNotFound();
            }

            return View(recommendation);
        }

        // GET: Recommendations/Create
        public IActionResult Create()
        {
            ViewBag.Action = "Create";
            return View("Edit", new Recommendation());
        }

        // GET: Recommendations/Edit/Id
        public async Task<IActionResult> Edit(Guid Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }

            var model = await  _repository.GetAsync(Id);
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.Action = "Edit";
            return View(model);
        }

        // POST: Recommendations/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id, Title, ShortDescription, Description, Category")] Recommendation model)
        {
            if (ModelState.IsValid)
            {
                bool result = false;

                if (model.Id.Equals(Guid.Empty))
                {
                    model.OwnerId = HttpContext.User.GetUserId();
                    result = await _repository.CreateAsync(model);
                }
                else
                {
                    result = await _repository.UpdateAsync(model);
                }

                TempData["opertionResult"] = (result) ? "Recommendation saved." : "Some Error Message";
            }
            return RedirectToAction("Index");
        }

        //POST: Recommendations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid Id)
        {
            var result = await _repository.DeleteAsync(Id);
            TempData["opertionResult"] = (result) ? "Recommendation deleted.": "Some Error Message";
            return RedirectToAction("Index");
        }

        //GET: Recommendations/Delete/5
        public async Task<IActionResult> Delete(Guid Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }

            var recommendation = await _repository.GetAsync(Id);
            if (recommendation == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteConfirm", recommendation);
        }
    }
}
