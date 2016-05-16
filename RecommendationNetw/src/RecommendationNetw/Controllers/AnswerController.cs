using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RecommendationNetw.Models;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Authorization;

namespace RecommendationNetw.Controllers
{
    [Authorize]
    public class AnswerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AnswerController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(Category category)
        {
            var user = await GetCurrentUserAsync();

            if (!user.Answers.Any(x => x.Question.Category == category))
                RedirectToAction("Create", category);

            return RedirectToAction("Edit", category);
        }

        public async Task<IActionResult> Create(Category category)
        {
            return View();
        }

        public async Task<IActionResult> Edit(Category? category)
        {
            if (category == null)
            {
                return HttpNotFound();
            }

            var model = await GetCurrentUserAsync();
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.Action = "Edit";
            return View(model);
        }

        private async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await _userManager.FindByIdAsync(HttpContext.User.GetUserId());
        }
    }
}
