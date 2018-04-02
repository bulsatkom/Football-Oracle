using FootballOracle.Models;
using FootballOracle.Models.models;
using FootballOracle_DataServices.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballOracle.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserForecastService userForecastService;
        private readonly IMatchService matchService;
        private readonly ITeamService teamService;



        public ProfileController(IUserForecastService userForecastService, IMatchService matchService,
            ITeamService teamService)
        {
            this.userForecastService = userForecastService;
            this.matchService = matchService;
            this.teamService = teamService;
        }

        public ActionResult TopTenUsersPartial()
        {
            var model = new List<TopTenUsersViewModel>();
            var db = new ApplicationDbContext();
            db.Users.OrderByDescending(x => x.Points).ToList().ForEach(x =>
            {
                model.Add(new TopTenUsersViewModel()
                {
                    Id = Guid.Parse(x.Id),
                    Name = x.UserName,
                    Points = x.Points
                });
            });

            return this.PartialView("TopTenUsersPartial", model);
        }

        public ActionResult Bets(Guid id)
        {
            var bets = this.userForecastService.GetAllByIdAndPlayed(id);

            var PlayedMatches = new List<PlayedBetModel>();
            var NotPlayedMatches = new List<NotPlayedBetModel>();

            foreach (var bet in bets)
            {
                var currentMatch = this.matchService.GetById(bet.MatchId);
                var homeTeamName = this.teamService.FindTeamNameById(currentMatch.HomeTeam);
                var awayTeamName = this.teamService.FindTeamNameById(currentMatch.AwayTeam);
                var homeTeamPic = this.teamService.GetTeamAvatarById(currentMatch.HomeTeam);
                var awayTeamPic = this.teamService.GetTeamAvatarById(currentMatch.AwayTeam);


                if (bet.IsOpen == true)
                {
                    NotPlayedMatches.Add(new NotPlayedBetModel()
                    {
                        AwayPic = awayTeamPic,
                        AwayTeam = awayTeamName,
                        HomeTeam = homeTeamName,
                        Coeff = bet.Coefficient,
                        Date = currentMatch.Date,
                        Forcast = bet.Forcast,
                        MatchId = bet.MatchId,
                        HomePic = homeTeamPic,
                        PlayedPoints = bet.PointsPlayed,
                        IsStarted = DateTime.Now > currentMatch.Date,
                        ForecastId = bet.Id
                    });
                }
                else
                {
                    PlayedMatches.Add(new PlayedBetModel()
                    {
                        awayGoals = currentMatch.AwayGoals,
                        HomeTeam = homeTeamName,
                        Date = currentMatch.Date,
                        AwayPic = awayTeamPic,
                        AwayTeam = awayTeamName,
                        Coeff = bet.Coefficient,
                        Forcast = bet.Forcast,
                        homeGoals = currentMatch.HomeGoals,
                        HomePic = homeTeamPic,
                        IsSuccessFull = bet.IsSuccess,
                        PlayedPoints = bet.PointsPlayed,

                    });
                }
            }

            var model = new BetsViewModel()
            {
                NotPlayedMatches = NotPlayedMatches,
                PlayedMatches = PlayedMatches,
                UserId = id
            };

            return this.View(model);
        }

        public ActionResult DeleteBet(Guid id)
        {
            var forecast = this.userForecastService.DeleteBet(id);
            var userId = User.Identity.GetUserId();

            if (forecast != null)
            {
                
                this.matchService.RemovePlayedMatchCount(forecast.MatchId);
                this.matchService.RemovePlayedForForcastCount(forecast.MatchId, forecast.Forcast);
                this.userForecastService.UpgradeUserPoints(Guid.Parse(userId), -forecast.PointsPlayed);

                //FIX IN OTHER PROJECT :@ :D :X
                var db = new ApplicationDbContext();

                var user = db.Users.FirstOrDefault(x => x.Id == userId);
                user.Points += forecast.PointsPlayed;

                db.SaveChanges();
            }

            return this.RedirectToAction("Bets", new { id = Guid.Parse(userId) });
        }

        public ActionResult Account(Guid id)
        {
            // fix this shi... :@
            var db = ApplicationDbContext.Create();
            var model = new AccountViewModel();
            string idString = id.ToString();
            var user = db.Users.FirstOrDefault(x => x.Id == idString);
            if (user != null)
            {
                var favouriteTeam = this.teamService.FindTeamNameById(user.FavouriteTeamId);
                var favouriteTeamPic = this.teamService.GetTeamAvatarById(user.FavouriteTeamId);
                int allForecast = this.userForecastService.GetAllForUser(id).ToList().Count;
                int successForecast = this.userForecastService.GetSuccessfullForecastCount(id);
                var matches = new List<PlayedBetModel>();
                this.userForecastService.GetTopForcast(id).ToList().ForEach(x => 
                {
                    var currentMatch = this.matchService.GetById(x.MatchId);
                    var homeTeamName = this.teamService.FindTeamNameById(currentMatch.HomeTeam);
                    var awayTeamName = this.teamService.FindTeamNameById(currentMatch.AwayTeam);
                    var date = currentMatch.Date;
                    var homeTeamPic = this.teamService.GetTeamAvatarById(currentMatch.HomeTeam);
                    var awayTeamPic = this.teamService.GetTeamAvatarById(currentMatch.AwayTeam);

                    matches.Add(new PlayedBetModel()
                    {
                        awayGoals = x.AwayGoals,
                        Coeff = x.Coefficient,
                        Forcast = x.Forcast,
                        homeGoals = x.homeGoals,
                        IsSuccessFull = x.IsSuccess,
                        PlayedPoints = x.PointsPlayed,
                        Date = date,
                        AwayTeam = awayTeamName,
                        AwayPic = awayTeamPic,
                        HomePic = homeTeamPic,
                        HomeTeam = homeTeamName
                    });
                });

                model.userName = user.UserName;
                model.AccId = id;
                model.City = user.City;
                model.FullName = user.FirstName + " " + user.MiddleName + " " + user.LastName;
                model.Points = user.Points;
                model.RegisteredOn = user.Register;
                model.Gender = user.Gender;
                model.FavouriteTeam = favouriteTeam;
                model.FavouriteTeamPic = favouriteTeamPic;
                model.ForecastCount = allForecast;
                model.SuccessForecastCount = successForecast;
                model.TopForcasts = matches;
            }

            return this.View(model);
        }
    }
}