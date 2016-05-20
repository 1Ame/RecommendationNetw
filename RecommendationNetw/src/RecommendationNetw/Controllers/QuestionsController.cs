using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RecommendationNetw.Repositories;
using Microsoft.AspNet.Identity;
using RecommendationNetw.Models;
using System.Security.Claims;
using RecommendationNetw.Managers;
using Microsoft.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RecommendationNetw.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly QuestionManager<Question> _questionManager;
        private readonly AnswerManager<Answer> _answerManager;


        public QuestionsController(QuestionManager<Question> questionManager,
            AnswerManager<Answer> answerManager)
        {
            _questionManager = questionManager;
            _answerManager = answerManager;
        }    

        public async Task<IActionResult> Index(Category category)
        {
            if (category == 0)
                return HttpNotFound();

            var haveAnswers = await _answerManager.HaveAnswersInCategory(UserId, category);

            if (haveAnswers)
                return RedirectToAction("Edit", new { Category = category });

            return RedirectToAction("Create", new { Category = category});
        }

        public async Task<IActionResult> Create(Category category)
        {
            if (category == 0)            
                return HttpNotFound();            

            var questionList = await _questionManager.FindAllWithRefAsync(x => x.Category == category).ToListAsync();

            if (questionList == null)
                return HttpNotFound();

            var model = questionList.Select(x => new Answer()
            {
                Category = category,
                Question = x,
                QuestionId = x.Id
            }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IEnumerable<Answer> model)
        {            
            if (ModelState.IsValid)
            {
                foreach (var item in model)
                    item.OwnerId = UserId;
                 
                var result = await _answerManager.CreateRangeAsync(model);
                TempData["opertionResult"] = (result) ? "Answers saved." : "Some Error Message";
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit(Category category)
        {
            if (category == 0)            
                return HttpNotFound();

            var model = await _answerManager.FindAllWithRefAsync(x => category.Equals(x.Category) && UserId.Equals(x.OwnerId)).ToListAsync();

            if (model == null)            
                return HttpNotFound();            

            ViewBag.Action = "Edit";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IEnumerable<Answer> model)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in model)
                    item.OwnerId = UserId;

                var result = await _answerManager.EditRangeAsync(model);
                TempData["opertionResult"] = (result) ? "Answers saved." : "Some Error Message";
            }
            return RedirectToAction("Index", "Home");
        }

        private string UserId
        {
            get { return HttpContext.User.GetUserId(); }
        }
    }
}
