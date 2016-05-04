using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using RecommendationNetw.Models;
using RecommendationNetw.Repositories;
using RecommendationNetw.Services;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;
using RecommendationNetw.ViewModels.Questions;
using System;

namespace RecommendationNetw.Controllers
{
    [Authorize]
    public class QuestionaryController : Controller
    {
        private readonly IRepository<Questionary, Guid> _repository;
        private readonly UserManager<ApplicationUser> _userManager;


        public QuestionaryController(IRepository<Questionary, Guid> repository, 
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _repository = repository;    
        } 

        public async Task<IActionResult> Index()
        {
            var a = User.GetUserId();
            
            var user = await _userManager.FindByIdAsync(User.GetUserId());
            if (user.Questionary == null)
                return RedirectToAction("Create");                        
            return RedirectToAction("Edit", user.Questionary.Id);
        }

        // GET: Questions/Create
        public async Task<IActionResult> Create()
        {
            var questionsList = await((IQuestionary<Question>)_repository).GenerateQuestionary();
            var model = new Questionary()
            {
                Answers = questionsList.Select(x => new Answer()
                {
                    Question = x,
                    QuestionId =x.Id.ToString()
                }).ToList()
            };
            ViewBag.Action = "Create";
            return View("Edit", model);
        }

        public async Task<IActionResult> Edit(Guid Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }

            var model = await _repository.GetAsync(Id);

            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.Action = "Edit";
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Questionary model)
        {
            if (ModelState.IsValid)
            {                
                bool result = false;
                if (model.Id.Equals(Guid.Empty))
                {
                    model.OwnerId = User.GetUserId();
                    result = await _repository.CreateAsync(model);
                }
                else
                {
                    result = await _repository.UpdateAsync(model);
                }

                TempData["opertionResult"] = (result) ? "Questionary saved." : "Some Error Message";
            }
            return RedirectToAction("Index");
        }
    }
}
