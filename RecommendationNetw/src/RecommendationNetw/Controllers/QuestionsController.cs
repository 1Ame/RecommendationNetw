using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using RecommendationNetw.Models;
using RecommendationNetw.Repositories;
using RecommendationNetw.Services;

namespace RecommendationNetw.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly IRepository<Question, string> repository;

        public QuestionsController(IRepository<Question, string> Repository)
        {
            repository = Repository;    
        }

        // GET: Questions

        //// GET: Questions/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    Question question = await repository.Questions.SingleAsync(m => m.Id == id);
        //    if (question == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(question);
        //}

        // GET: Questions/Create
        public IActionResult Create()
        {
            ViewBag.Action = "Create";
            return View("Edit", new Question());
        }

        //// POST: Questions/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(Question question)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        repository.Questions.Add(question);
        //        await repository.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(question);
        //}

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Question question = await repository.GetAsync(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(question.Id))
                {

                }                
                return RedirectToAction("Index");
            }
            return View(question);
        }

        //// GET: Questions/Delete/5
        //[ActionName("Delete")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    Question question = await repository.Questions.SingleAsync(m => m.Id == id);
        //    if (question == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(question);
        //}

        //// POST: Questions/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    Question question = await repository.Questions.SingleAsync(m => m.Id == id);
        //    repository.Questions.Remove(question);
        //    await repository.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
    }
}
