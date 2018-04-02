using FootballOracle.Models;
using FootballOracle.Models.models;
using FootballOracle_DataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballOracle.Controllers
{
    public class ChampionshipController : Controller
    {
        private readonly IChampionshipService championshipService;
        private readonly ITeamService teamService;

        public ChampionshipController(IChampionshipService championshipService, ITeamService teamService)
        {
            this.championshipService = championshipService;
            this.teamService = teamService;
        }

        public ActionResult MenuForChampionshipsPartial(Guid? id)
        {
            var model = new MenuForChampionshipsViewModel()
            {
                ChampionShips = new Dictionary<Guid,string>()
            };

            this.championshipService.GetAll().ToList().ForEach(x =>
            {
                model.ChampionShips.Add(x.Id, x.Name);
            });

            if(id != null)
            {
                model.Selected = id;
            }

            return this.PartialView("MenuForChampionshipsPartial", model);
        }

        public ActionResult champDetails(Guid id)
        {
            var news = new List<NewsModel>();
            var playedMatch = new List<PlayedMatchModel>();
            var table = new List<TeamModel>();
            var upCommingMatches = new List<UpCommingMatchModel>();

            this.championshipService.GetArticlesForChampionship(id).ToList().ForEach(x =>
            {
                news.Add(new NewsModel()
                {
                    Id = x.Id,
                    ImageSrc = x.Image,
                    Title = x.Title
                });
            });

            this.championshipService.GetPlayedMatchByChampionshipId(id).ToList().ForEach(x =>
            {
                var homeTeamName = this.teamService.FindTeamNameById(x.HomeTeam);
                var awayTeamName = this.teamService.FindTeamNameById(x.AwayTeam);

                playedMatch.Add(new PlayedMatchModel()
                {
                    HomeTeam = homeTeamName,
                    AwayTeam = awayTeamName,
                    HomeGoals = x.HomeGoals,
                    AwayGoals = x.AwayGoals,
                    Date = x.Date
                });
            });

            this.championshipService.GetTeamsForChampionship(id).ToList().ForEach(x =>
            {
                table.Add(new TeamModel()
                {
                    Draws = x.Draws,
                    GoalConcedered = x.GoalConcedered,
                    GoalScored = x.GoalScored,
                    losses = x.Losses,
                    Matches = x.Matches,
                    Name = x.Name,
                    Picture = x.Picture,
                    Points = x.Points,
                    Wins = x.Wins
                });
            });

            this.championshipService.GetUpcamingMatchByChampionshipId(id).ToList().ForEach(x =>
            {
                var homeTeamName = this.teamService.FindTeamNameById(x.HomeTeam);
                var awayTeamName = this.teamService.FindTeamNameById(x.AwayTeam);

                upCommingMatches.Add(new UpCommingMatchModel()
                {
                    homeTeam = homeTeamName,
                    awayCoeff = x.AwayCoefficient,
                    awayTeam = awayTeamName,
                    Date = x.Date,
                    drawCoeff = x.DrawCoefficient,
                    homeCoeff = x.HomeCoefficient,
                    Id = x.Id
                });
            });

            var name = this.championshipService.GetNameById(id);

            var model = new ChampDetailsViewModel()
            {
                News = news,
                PlayedMatches = playedMatch,
                Table = table,
                UpcommingMatches = upCommingMatches,
                Name = name
            };

            return this.View(model);
        }

        public ActionResult ChampionshipPartial(ICollection<NewsModel> news, int page)
        {
            var model = new ChampionshipPartialViewModel()
            {
                news = news.ToList(),
                page = page
            };

            if(TempData["news"] != null)
            {
                model.news = (ICollection<NewsModel>)TempData["news"];
            }

            int pages = ((model.news.Count % 10) != 0) ? (model.news.Count / 10 + 1) : model.news.Count / 10;

            model.pages = pages;

            return this.PartialView("ChampionshipPartial", model);
        }
    }
}