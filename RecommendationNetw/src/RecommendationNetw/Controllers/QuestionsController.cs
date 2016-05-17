using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RecommendationNetw.Repositories;
using Microsoft.AspNet.Identity;
using RecommendationNetw.Models;
using System.Security.Claims;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RecommendationNetw.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Question, Guid> _repository;

        public QuestionsController(UserManager<ApplicationUser> userManager, 
            IRepository<Question, Guid> repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<IActionResult> Index(Category category)
        {
            var user = await GetCurrentUserAsync();

            if (!user.Answers.Any(x => x.Question.Category == category))
                RedirectToAction("Create", category);

            return RedirectToAction("Edit", category);
        }

        public async Task<IActionResult> Create(Category? category)
        {
            if (category == null)
            {
                return HttpNotFound();
            }

            var user = await GetCurrentUserAsync();
            var questionList = await _repository.FindAllAsync(x => x.Category == category);

            var model = questionList.Select(x => new Answer()
                {
                    Question = x,
                    QuestionId = x.Id                  
                });

            return View("Edit", model);
        }

        public async Task<IActionResult> Edit(Category? category)
        {
            if (category == null)
            {
                return HttpNotFound();
            }

            var user = await GetCurrentUserAsync();
            var model = user.Answers;

            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.Action = "Edit";
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ICollection<Answer> answers)
        {
            return View();
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
        }
    }
}
