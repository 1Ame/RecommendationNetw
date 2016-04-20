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
        private IRepository<Recommendation, string> _repository;

        public RecommendationsController(IRepository<Recommendation, string> repository)
        {
            _repository = repository;
        }

        // GET: Recommendations
        public IActionResult Index(int? page)
        {
            var pagingInfo = new PagingInfo(_repository.GetAllAsync().Count(), page);
            return View();
        }

        // GET: Recommendations/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Recommendation recommendation = _repository.GetAsync(id);
            if (recommendation == null)
            {
                return HttpNotFound();
            }

            return View(recommendation);
        }

        // GET: Recommendations/Create
        public IActionResult Create()
        {
            return View("Edit", new Recommendation());
        }

        // GET: Recommendations/Edit/Id
        public async Task<IActionResult> Edit(string Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }

            var model =  _repository.GetAsync(Id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Recommendations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind(include: "Id, Title, ShortDescription, Description, Category")] Recommendation model)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                if (string.IsNullOrEmpty(model.Id))
                {
                    model.Id = Guid.NewGuid().ToString();
                    model.PostedOn = DateTime.Now;
                    model.Modified = DateTime.Now;
                    model.OwnerId = HttpContext.User.GetUserId();
                    result = _repository.Create(model);
                }
                else
                {
                    model.Modified = DateTime.Now;
                    result = _repository.Update(model);
                }
                TempData["opertionResult"] = result ? "Your recommendation saved" : "Error";
            }
            return View();
        }

        //GET: Recommendations/Edit/5
        //public IActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    Recommendation recommendation = _context.Recommendation.Single(m => m.Id == id);
        //    if (recommendation == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(recommendation);
        //}

        //POST: Recommendations/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Recommendation recommendation)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Update(recommendation);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(recommendation);
        //}

        //GET: Recommendations/Delete/5
        //[ActionName("Delete")]
        //public IActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    Recommendation recommendation = _context.Recommendation.Single(m => m.Id == id);
        //    if (recommendation == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(recommendation);
        //}

        //POST: Recommendations/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(string id)
        //{
        //    Recommendation recommendation = _context.Recommendation.Single(m => m.Id == id);
        //    _context.Recommendation.Remove(recommendation);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}       
    }
}
