using FootballOracle.Areas.Admin.Models;
using FootballOracle.Models;
using FootballOracle_Data;
using FootballOracle_DataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace FootballOracle.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IChampionshipService championshipService;
        private readonly ITeamService teamService;
        private readonly IMatchService matchService;
        private readonly IArticleService articleService;
        private readonly ITagService tagService;
        private readonly IInQuestionService inQuestionService;
        private readonly IUserForecastService userForecast;


        public AdminController(IChampionshipService championshipService, ITeamService teamService,
            IMatchService matchService, IArticleService articleService, ITagService tagService,
            IInQuestionService inQuestionService, IUserForecastService userForecast)
        {
            this.championshipService = championshipService;
            this.teamService = teamService;
            this.matchService = matchService;
            this.articleService = articleService;
            this.tagService = tagService;
            this.inQuestionService = inQuestionService;
            this.userForecast = userForecast;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult AddArticle()
        {
            var teamsDb = this.teamService.GetAll();
            var championshipsDb = this.championshipService.GetAll();

            var teams = new List<SelectListItem>();
            var championships = new List<SelectListItem>();

            foreach (var team in teamsDb)
            {
                teams.Add(new SelectListItem()
                {
                    Value = team.Id.ToString(),
                    Text = team.Name
                });
            }

            foreach (var championship in championshipsDb)
            {
                championships.Add(new SelectListItem()
                {
                    Text = championship.Name,
                    Value = championship.Id.ToString()
                });
            }

            var viewModel = new AddArticleViewModel()
            {
                Championships = championships,
                Teams = teams
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArticle(AddArticleViewModel addArticleViewModel)
        {
            if (ModelState.IsValid)
            {
                List<Guid> teams = addArticleViewModel.TeamsId.ToList();
                List<Guid> teamsId = new List<Guid>();
                List<Tag> TagsId = new List<Tag>();

                teamsId.Add(teams[0]);

                if (teams[0] != teams[1])
                {
                    teamsId.Add(teams[1]);
                }

                foreach (var teamId in teamsId)
                {
                    TagsId.Add(this.tagService.GetTagByMatchId(teamId));
                }

                var article = new Article()
                {
                    Id = Guid.NewGuid(),
                    ChampionshipId = addArticleViewModel.ChampionshipId,
                    Content = addArticleViewModel.Content,
                    Date = DateTime.UtcNow,
                    Image = addArticleViewModel.Image,
                    Viewing = 0,
                    Title = addArticleViewModel.Title,
                    Tags = TagsId
                };

                this.articleService.Add(article);

                return this.View("index");
            }

            return this.View(addArticleViewModel);
        }

        [HttpGet]
        public ActionResult AddMatch()
        {
            var teams = this.teamService.GetAll().ToList();
            var championships = this.championshipService.GetAll();

            List<SelectListItem> teamsResult = new List<SelectListItem>();
            List<SelectListItem> championshipsResult = new List<SelectListItem>();
            var dictionary = new Dictionary<string, SelectListGroup>();

            foreach (var championship in championships)
            {
                championshipsResult.Add(new SelectListItem()
                {
                    Text = championship.Name,
                    Value = championship.Id.ToString()
                });

                dictionary.Add(championship.Name, new SelectListGroup()
                {
                    Name = championship.Name
                });
            }

            foreach (var team in teams)
            {
                var name = this.championshipService.GetNameById(team.ChampionshipId);
                teamsResult.Add(new SelectListItem()
                {
                    Text = team.Name,
                    Value = team.Id.ToString(),
                    Group = dictionary[name]
                });
            }

            var viewModel = new AddMatchViewModel()
            {
                Teams = teamsResult,
                Championships = championshipsResult
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMatch(AddMatchViewModel addMatchViewModel)
        {
            if (ModelState.IsValid)
            {
                var match = new Match()
                {
                    Id = Guid.NewGuid(),
                    AwayCoefficient = addMatchViewModel.AwayCoefficient,
                    AwayGoals = 0,
                    AwayTeam = addMatchViewModel.AwayTeam,
                    Date = addMatchViewModel.Date,
                    DrawCoefficient = addMatchViewModel.DrawCoefficient,
                    HomeCoefficient = addMatchViewModel.HomeCoefficient,
                    HomeGoals = 0,
                    HomeTeam = addMatchViewModel.HomeTeam,
                    IsOpen = true,
                    PlayedFrom = 0,
                    SuccessMatch = 0,
                    SuccessResult = 0,
                    ChampionshipId = addMatchViewModel.ChampionshipId,
                    PlayedFor1 = 0,
                    PlayedFor2 = 0,
                    PlayedForX = 0
                };

                this.matchService.Add(match);

                return this.View("index");
            }

            return this.View(addMatchViewModel);
        }

        public ActionResult AddResult()
        {
            var viewModel = new AddResultViewModel();

            var matches = this.matchService.GetAllOpen();
            var result = new List<ViewTeam>();

            foreach (var match in matches)
            {
                string homeTeam = this.teamService.FindTeamNameById(match.HomeTeam);
                string awayTeam = this.teamService.FindTeamNameById(match.AwayTeam);
                result.Add(new ViewTeam()
                {
                    Id = match.Id,
                    homeTeam = homeTeam,
                    awayTeam = awayTeam,
                    homeGoals = 0,
                    awayGoals = 0
                });
            }

            viewModel.Matches = result;

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Result(Guid id, string homeTeam, string awayTeam)
        {
            var team = new ViewTeam()
            {
                homeGoals = 0,
                awayGoals = 0,
                Id = id,
                homeTeam = homeTeam,
                awayTeam = awayTeam
            };

            return this.View(team);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Result(ViewTeam Team)
        {
            if (ModelState.IsValid)
            {
                this.matchService.AddResult(Team.Id, Team.homeGoals, Team.awayGoals);

                var result = this.matchService.UpdateAllForecasts(Team.Id, Team.homeGoals, Team.awayGoals);

                // fix this shi* :@
                var db = ApplicationDbContext.Create();
                foreach (var res in result)
                {
                    string currentId = res.Item1.ToString();
                    var user = db.Users.FirstOrDefault(x => x.Id == currentId);
                    user.Points += res.Item2 * res.Item3;
                    db.SaveChanges();
                }

                foreach (var res in result)
                {
                    string currentId = res.Item1.ToString();
                    this.userForecast.UpgradeUserPointsAfterMatch(res.Item1, res.Item2 * res.Item3);
                }

                var match = this.matchService.GetById(Team.Id);

                this.teamService.UpdateTeams(match.HomeTeam, match.AwayTeam, Team.homeGoals, Team.awayGoals);

                return this.View("index");
            }

            return this.View("AddResult");
        }

        public ActionResult AddThread()
        {
            return this.View();
        }

        public ActionResult AddForumArticle()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult AddTeam()
        {
            List<Championship> championships = this.championshipService.GetAll().ToList();
            var viewModel = new AddTeamViewModel();
            foreach (var championship in championships)
            {
                viewModel.Championships.Add(new SelectListItem()
                {
                    Text = championship.Name,
                    Value = championship.Id.ToString()
                });
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTeam(AddTeamViewModel AddTeamViewModel)
        {
            if (ModelState.IsValid)
            {
                var team = new Team()
                {
                    Id = Guid.NewGuid(),
                    ChampionshipId = AddTeamViewModel.ChampionshipId,
                    Draws = 0,
                    GoalConcedered = 0,
                    GoalScored = 0,
                    Losses = 0,
                    Matches = 0,
                    Name = AddTeamViewModel.Name,
                    Picture = AddTeamViewModel.Picture,
                    Points = 0,
                    Wins = 0
                };

                var tag = new Tag()
                {
                    Id = Guid.NewGuid(),
                    TeamId = team.Id
                };

                this.teamService.Add(team);
                this.tagService.add(tag);

                return this.View("Index");
            }

            return this.View(AddTeamViewModel);
        }

        [HttpGet]
        public ActionResult AddChampionship()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddChampionship(AddChampionshipViewModel addChampionshipViewModel)
        {
            if (ModelState.IsValid)
            {
                var championship = new Championship()
                {
                    Id = Guid.NewGuid(),
                    Name = addChampionshipViewModel.name
                };

                this.championshipService.Add(championship);

                return this.View("index");
            }

            return this.View(addChampionshipViewModel);
        }

        [HttpGet]
        public ActionResult AddInQuestion(AddInquestionViewModel addInquestionViewModel)
        {
            if (addInquestionViewModel == null)
            {
                addInquestionViewModel = new AddInquestionViewModel()
                {
                    AnswersCount = 0,
                    Answers = new List<string>(),
                };
            }

            return this.View(addInquestionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInQuestions(AddInquestionViewModel addInquestionViewModel)
        {
            if (ModelState.IsValid)
            {
                addInquestionViewModel.Answers = new List<string>();

                for (int i = 0; i < addInquestionViewModel.AnswersCount; i++)
                {
                    addInquestionViewModel.Answers.Add("");
                }
                return this.View("AddInQuestion", addInquestionViewModel);
            }

            return this.RedirectToAction("AddInQuestion");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddInQuestionsDb(AddInquestionViewModel addInquestionViewModel)
        {
            if (ModelState.IsValid)
            {
                List<string> answers = new List<string>();

                addInquestionViewModel.Answers.ToList().ForEach(x =>
             {
                 answers.Add(x);
             });

                var inquestion = new Inquestion()
                {
                    Id = Guid.NewGuid(),
                    PlayersCount = 0,
                    Question = addInquestionViewModel.Question,
                    IsActive = false
                };

                var answersforDb = new List<Answer>();

                foreach (var answer in answers)
                {
                    answersforDb.Add(new Answer()
                    {
                        Id = Guid.NewGuid(),
                        Text = answer,
                        InquestionId = inquestion.Id,
                        playedFrom = 0,
                        
                    });
                }

                this.inQuestionService.AddInquestion(inquestion);
                this.inQuestionService.AddAnswers(answersforDb);

                return this.View("Index");
            }

            return this.RedirectToAction("AddInQuestion");
        }
    }
}