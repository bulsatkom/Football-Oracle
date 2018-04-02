using FootballOracle.Models;
using FootballOracle.Models.models;
using FootballOracle_Data;
using FootballOracle_DataServices.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballOracle.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMatchService matchService;
        private readonly ITeamService teamService;
        private readonly IUserForecastService userForecastService;
        private readonly IChampionshipService championshipService;

        public MatchController(IMatchService matchService, ITeamService teamService,
            IUserForecastService userForecastService, IChampionshipService championshipService)
        {
            this.matchService = matchService;
            this.teamService = teamService;
            this.userForecastService = userForecastService;
            this.championshipService = championshipService;
        }

        public ActionResult Index()
        {
            var matchesForToday = new List<MatchModel>();
            var matchesForTomorrow = new List<MatchModel>();
            var otherMatches = new List<MatchModel>();
            var UserMatchesId = new List<Guid>();

            var forecastsmodel = new AddForcastViewModel();
            forecastsmodel.forcasts = new List<ForecastModel>();
            var isAuthenticated = false;

            if (User.Identity.IsAuthenticated)
            {
                isAuthenticated = true;
                var accId = Guid.Parse(User.Identity.GetUserId());

                this.userForecastService.GetAllForUser(accId).ToList().ForEach(x =>
                {
                    UserMatchesId.Add(x);
                });

                this.userForecastService.GetAllById(accId).ToList().ForEach(x =>
                    {
                        var match = this.matchService.GetById(x.MatchId);
                        var homeTeam = this.teamService.FindTeamNameById(match.HomeTeam);
                        var awayTeam = this.teamService.FindTeamNameById(match.AwayTeam);

                        forecastsmodel.forcasts.Add(new ForecastModel()
                        {
                            homeTeam = homeTeam,
                            AwayTeam = awayTeam,
                            Coeff = x.Coefficient,
                            Id = x.Id,
                            points = 0,
                            Forecast = x.Forcast
                        });
                    });
            }

            var matches = this.matchService.GetMatchesForForcast().ToList();

            matches.ForEach(x =>
            {
                string homeTeamName = this.teamService.FindTeamNameById(x.HomeTeam);
                string awayTeamName = this.teamService.FindTeamNameById(x.AwayTeam);

                var matchDate = x.Date;

                // in Production must be UtcNow
                var dateNow = DateTime.Now;

                if (matchDate > dateNow)
                {
                    var currentMatchDate = matchDate.Date;
                    var currentDateNowDate = dateNow.Date;
                    bool isDisabled = false;
                    UserMatchesId.ForEach(y =>
                    {
                        if (y == x.Id)
                        {
                            isDisabled = true;
                            return;
                        }
                    });
                    if (currentMatchDate == currentDateNowDate)
                    {
                        matchesForToday.Add(new MatchModel()
                        {
                            Id = x.Id,
                            HomeTeam = homeTeamName,
                            Date = x.Date,
                            AwayTeam = awayTeamName,
                            AwayCoeff = x.AwayCoefficient,
                            HomeCoeff = x.HomeCoefficient,
                            DrawCoeff = x.DrawCoefficient,
                            IsDisabled = isDisabled
                        });
                    }
                    else if (currentMatchDate == currentDateNowDate.AddDays(1))
                    {
                        matchesForTomorrow.Add(new MatchModel()
                        {
                            Id = x.Id,
                            HomeTeam = homeTeamName,
                            Date = x.Date,
                            AwayTeam = awayTeamName,
                            AwayCoeff = x.AwayCoefficient,
                            HomeCoeff = x.HomeCoefficient,
                            DrawCoeff = x.DrawCoefficient,
                            IsDisabled = isDisabled
                        });
                    }
                    else
                    {
                        otherMatches.Add(new MatchModel()
                        {
                            Id = x.Id,
                            HomeTeam = homeTeamName,
                            Date = x.Date,
                            AwayTeam = awayTeamName,
                            AwayCoeff = x.AwayCoefficient,
                            HomeCoeff = x.HomeCoefficient,
                            DrawCoeff = x.DrawCoefficient,
                            IsDisabled = isDisabled
                        });
                    }
                }
            });

            var viewModel = new MatchesViewModel()
            {
                Forecasts = forecastsmodel,
                MatchesForToday = matchesForToday,
                IsAuthenticated = isAuthenticated,
                MatchesForTomorrow = matchesForTomorrow,
                OtherMatches = otherMatches
            };

            return this.View(viewModel);
        }

        [Authorize]
        public ActionResult AddForecast(Guid id, string forecast)
        {
            double coeff = 0;
            bool ContainsId = false;
            var accId = Guid.Parse(User.Identity.GetUserId());

            this.userForecastService.GetAllByIdAndPlayed(accId).ToList().ForEach(x =>
            {
                if (x.MatchId == id)
                {
                    ContainsId = true;
                    return;
                }
            });

            if (!ContainsId)
            {
                var match = this.matchService.GetById(id);

                if (match.Date > DateTime.Now)
                {
                    if (forecast == "1")
                    {
                        coeff = match.HomeCoefficient;
                    }
                    else if (forecast == "2")
                    {
                        coeff = match.AwayCoefficient;
                    }
                    else if (forecast == "X")
                    {
                        coeff = match.DrawCoefficient;
                    }
                    else
                    {
                        throw new ArgumentException("invalid forecast");
                    }

                    var model = new Forecast()
                    {
                        AccountId = Guid.Parse(User.Identity.GetUserId()),
                        AwayGoals = 0,
                        homeGoals = 0,
                        Id = Guid.NewGuid(),
                        Coefficient = coeff,
                        Forcast = forecast,
                        IsOpen = true,
                        IsPlayed = false,
                        IsSuccess = false,
                        MatchId = id,
                        PointsPlayed = 0,
                    };

                    this.userForecastService.Add(model);
                    this.matchService.UpdatePlayedMatchCount(id);
                    this.matchService.UpdatePlayedForForcastCount(id, model.Forcast);
                }
            }

            var forecasts = new List<ForecastModel>();

            this.userForecastService.GetAllById(accId).ToList().ForEach(x =>
            {
                var match = this.matchService.GetById(x.MatchId);
                var homeTeam = this.teamService.FindTeamNameById(match.HomeTeam);
                var awayTeam = this.teamService.FindTeamNameById(match.AwayTeam);

                forecasts.Add(new ForecastModel()
                {
                    homeTeam = homeTeam,
                    AwayTeam = awayTeam,
                    Coeff = x.Coefficient,
                    Id = x.Id,
                    points = 0,
                    Forecast = x.Forcast
                });
            });

            if (Request.IsAjaxRequest())
            {
                return this.View("Index");
            }
            else
            {
                return this.RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddForecasts(ForecastModel matchViewModel)
        {
            if (ModelState.IsValid)
            {
                // Fix this shi* ... 
                var db = ApplicationDbContext.Create();
                var id = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(x => x.Id == id);
                if (matchViewModel.points <= user.Points)
                {
                    user.Points -= matchViewModel.points;
                    db.SaveChanges();
                    this.userForecastService.UpdateForecast(matchViewModel.Id, matchViewModel.points);
                    this.userForecastService.UpgradeUserPoints(Guid.Parse(User.Identity.GetUserId()), matchViewModel.points);
                }

                return this.RedirectToAction("Index", "Match");
            }

            return this.RedirectToAction("Index", matchViewModel);
        }

        public ActionResult Proba()
        {
            var model = new ProbvamModel()
            {
                proba = "4o4o"
            };

            return this.View(model);
        }

        public ActionResult Match(Guid id)
        {
            var match = this.matchService.GetById(id);

            string championshipName = this.championshipService.GetNameById(match.ChampionshipId);

            var teamsDb = this.championshipService.GetTeamsForChampionship(match.ChampionshipId);
            var teams = new List<TeamModel>();

            double homePercent = ((double)match.PlayedFor1 / match.PlayedFrom) * 100;
            double drawPercent = ((double)match.PlayedForX / match.PlayedFrom) * 100;
            double awayPercent = ((double)match.PlayedFor2 / match.PlayedFrom) * 100;

            if(match.PlayedFrom == 0)
            {
                homePercent = 33;
                drawPercent = 33;
                awayPercent = 33;
            }

            string homeTeamName = this.teamService.FindTeamNameById(match.HomeTeam);
            string awayTeamName = this.teamService.FindTeamNameById(match.AwayTeam);

            string homeTeamPic = this.teamService.GetTeamAvatarById(match.HomeTeam);
            string awayTeamPic = this.teamService.GetTeamAvatarById(match.AwayTeam);

            foreach (var team in teamsDb)
            {
                teams.Add(new TeamModel()
                {
                    Draws = team.Draws,
                    GoalConcedered = team.GoalConcedered,
                    GoalScored = team.GoalScored,
                    losses = team.Losses,
                    Matches = team.Matches,
                    Name = team.Name,
                    Picture = team.Picture,
                    Points = team.Points,
                    Wins = team.Wins
                });
            }

            var viewModel = new MatchViewModel()
            {
                id = id,
                AwayCoeff = match.AwayCoefficient,
                AwayTeamName = awayTeamName,
                DrawCoef = match.DrawCoefficient,
                HomeTeamName = homeTeamName,
                HomeCoeff = match.HomeCoefficient,
                PlayedFor1 = (int)match.PlayedFor1,
                PlayedForX = (int)match.PlayedForX,
                PlayedFor2 = (int)match.PlayedFor2,
                PlayedFrom = match.PlayedFrom,
                HomeTeamPic = homeTeamPic,
                AwayTeamPic = awayTeamPic,
                Date = match.Date,
                championshipId = match.ChampionshipId,
                isSuccess = match.IsOpen,
                championship = championshipName,
                PlayedFor1Percent = homePercent,
                PlayedForXPercent = drawPercent,
                PlayedFor2Percent = awayPercent,
                homeGoals = match.HomeGoals,
                awayGoals = match.AwayGoals,
                Teams = teams
            };

            return this.View(viewModel);
        }

        public ActionResult GetHots()
        {
            var matchesDb = this.matchService.GetHots();

            var viewModel = new List<GetHotsViewModel>();

            foreach (var match in matchesDb)
            {
                string homeTeamName = this.teamService.FindTeamNameById(match.HomeTeam);
                string awayTeamName = this.teamService.FindTeamNameById(match.AwayTeam);

                viewModel.Add(new GetHotsViewModel()
                {
                    AwayTeam = awayTeamName,
                    HomeCoef = match.HomeCoefficient,
                    AwayCoef = match.AwayCoefficient,
                    Date = match.Date,
                    DrawCoef = match.DrawCoefficient,
                    HomeTeam = homeTeamName,
                    Id = match.Id
                });
            }

            return this.PartialView("GetHotsPartial", viewModel);
        }

        public ActionResult LastMatches()
        {
            var matchesDb = this.matchService.GetLatest();

            List<LastMatchesViewModel> viewModel = new List<LastMatchesViewModel>();

            foreach (var match in matchesDb)
            {
                string homeTeamName = this.teamService.FindTeamNameById(match.HomeTeam);
                string awayTeamName = this.teamService.FindTeamNameById(match.AwayTeam);

                viewModel.Add(new LastMatchesViewModel()
                {
                    HomeTeam = homeTeamName,
                    AwayTeam = awayTeamName,
                    AwayGoals = match.AwayGoals,
                    HomeGoals = match.HomeGoals,
                    StartTime = match.Date
                });
            }

            return this.PartialView("LastMatchesPartial", viewModel);
        }
    }

    public class ProbvamModel
    {
        public string proba { get; set; }
    }
}
