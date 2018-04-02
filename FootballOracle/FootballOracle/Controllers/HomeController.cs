using FootballOracle.Models;
using FootballOracle_DataServices.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web;
using System.Web.Mvc;

namespace FootballOracle.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInQuestionService inQuestionService;
        private readonly IUserForecastService userForecastService;


        public HomeController(IInQuestionService inQuestionService, IUserForecastService userForecastService)
        {
            this.inQuestionService = inQuestionService;
            this.userForecastService = userForecastService;
        }

        public ActionResult _LoginPartial()
        {
            if (User.Identity.IsAuthenticated)
            {
                double points = this.userForecastService.GetPointsbyUserId(Guid.Parse(User.Identity.GetUserId()));

                return this.PartialView("_LoginPartial", points);
            }

            return this.PartialView("_LoginPartial");
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                double points = this.userForecastService.GetPointsbyUserId(Guid.Parse(User.Identity.GetUserId()));

                return View("index", points);
            }

            return this.View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult InQuestionPartial()
        {
            var model = new InQuestionViewModel();
            model.Answers = new List<SelectListItem>();
            model.PlayedForEach = new List<int>();
            var activeInQuestion = this.inQuestionService.GetIdOfActiveInQuestion();

            if (activeInQuestion != null)
            {
                model.AllPlayedCount = activeInQuestion.PlayersCount;
                model.Id = activeInQuestion.Id;

                var answers = this.inQuestionService.GetAnswersById(activeInQuestion.Id);

                foreach (var answer in answers)
                {
                    model.Answers.Add(new SelectListItem()
                    {
                        Text = answer.Text,
                        Value = answer.Id.ToString()
                    });

                    model.PlayedForEach.Add(answer.playedFrom);
                }

                model.Title = activeInQuestion.Question;

                if (User.Identity.IsAuthenticated)
                {
                    var accId = Guid.Parse(User.Identity.GetUserId());
                    if (accId != null)
                    {
                        model.CanAnswer = this.inQuestionService.CanAnswer(activeInQuestion.Id, accId);
                    }
                    else
                    {
                        model.CanAnswer = false;
                    }
                }
            }

            return this.PartialView("InQuestionPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InQuestionPartial(InQuestionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userid = Guid.Parse(User.Identity.GetUserId());

                var cananswer = this.inQuestionService.CanAnswer(model.Id, userid);

                if (cananswer)
                {
                    this.inQuestionService.UpgradeVotesForAnswer(model.Answer);
                    this.inQuestionService.UpgradeVotesForInquestion(model.Id);
                    this.inQuestionService.UpgrareAnswerUserTable(model.Id, userid);
                }

                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Index");
        }
    }
}