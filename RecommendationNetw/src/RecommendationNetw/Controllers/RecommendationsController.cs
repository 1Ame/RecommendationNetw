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

namespace RecommendationNetw.Controllers
{
    public class RecommendationsController : Controller
    {
        private readonly IRepository<Recommendation, string> repository = null;

        public RecommendationsController(IRepository<Recommendation, string> Repository)
        {
            repository = Repository;
        }

        // GET: Recommendations
        public async Task<IActionResult> Index(int? page)
        {
            var items = await repository.GetAllAsync(x => x.OwnerId == HttpContext.User.GetUserId());
            var pagingInfo = new PagingInfo(items.Count(), page,1);

            var model = new IndexViewModel()
            {
                Items = items.Skip((pagingInfo.CurrentPage - 1) * pagingInfo.PageSize).Take(pagingInfo.PageSize),
                PagingInfo = pagingInfo
            };
            return View(model);
        }

        // GET: Recommendations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Recommendation recommendation = await repository.GetAsync(id);
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
        public async Task<IActionResult> Edit(string Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }

            var model = await  repository.GetAsync(Id);
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
        public async Task<IActionResult> Edit([Bind(include: "Id, Title, ShortDescription, Description, Category")] Recommendation model)
        {
            if (ModelState.IsValid)
            {
                Recommendation result = null;
                if (string.IsNullOrEmpty(model.Id))
                {                    
                    model.OwnerId = HttpContext.User.GetUserId();
                    model.Id = Guid.NewGuid().ToString();
                    model.PostedOn = DateTime.Now;
                    model.ModifiedOn = DateTime.Now;
                    result = await repository.CreateAsync(model);
                }
                else
                {
                    result = await repository.UpdateAsync(model);
                }
            }
            return RedirectToAction("Index");
        }

        //POST: Recommendations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string Id)
        {
            var result = await repository.DeleteAsync(Id);
            TempData["opertionResult"] = (result != null) ? string.Format("Recommendation \"{0}\" deleted.", result.Title) : "Error";
            return RedirectToAction("Index");
        }

        //GET: Recommendations/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(string Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }

            var recommendation = await repository.GetAsync(Id);
            if (recommendation == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteConfirmPartial", recommendation);
        }
    }
}
