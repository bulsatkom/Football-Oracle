using FootballOracle_Data;
using FootballOracle_Data.interfaces;
using FootballOracle_DataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballOracle_DataServices
{
    public class MatchService : IMatchService
    {
        private IFootballOracleDbContext dbContext;

        public MatchService(IFootballOracleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Match match)
        {
            this.dbContext.Match.Add(match);
            this.dbContext.SaveChanges();
        }

        public void AddResult(Guid id, int homeGoals, int awayGoals)
        {
            var match = this.dbContext.Match.FirstOrDefault(x => x.Id == id);

            match.HomeGoals = homeGoals;
            match.AwayGoals = awayGoals;
            match.IsOpen = false;

            this.dbContext.SaveChanges();
        }

        public ICollection<Match> GetAllOpen()
        {
            return this.dbContext.Match.Where(x => x.IsOpen == true).ToList();
        }

        public Match GetById(Guid id)
        {
            return this.dbContext.Match.FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Match> GetHots()
        {
            return this.dbContext.Match
                .Where(x => x.IsOpen == true)
                .OrderByDescending(x => x.PlayedFrom)
                .Take(10)
                .ToList();
        }

        public ICollection<Match> GetLatest()
        {
            return this.dbContext.Match
                .Where(x => x.IsOpen == false)
                .OrderByDescending(x => x.Date)
                .Take(10)
                .ToList();
        }

        public ICollection<Match> GetMatchesForForcast()
        {
            return this.dbContext.Match
                .Where(x => x.IsOpen == true && x.Date > DateTime.Now)
                .OrderBy(x => x.Date)
                .ToList();
        }

        public void RemovePlayedForForcastCount(Guid MatchId, string forcast)
        {
            var match = this.GetById(MatchId);

            if(match != null)
            {
                if (forcast == "1")
                {
                    match.PlayedFor1 -= 1;
                }
                else if (forcast == "X")
                {
                    match.PlayedForX -= 1;
                }
                else
                {
                    match.PlayedFor2 -= 1;
                }

                this.dbContext.SaveChanges();
            }
        }

        public void RemovePlayedMatchCount(Guid id)
        {
            var match = this.GetById(id);

            if(match != null)
            {
                match.PlayedFrom -= 1;

                this.dbContext.SaveChanges();
            }
        }

        public ICollection<Tuple<Guid, double, int>> UpdateAllForecasts(Guid MatchId, int homeGoals, int awayGoals)
        {
            var result = new List<Tuple<Guid, double, int>>();

            string forecast = "";

            if (homeGoals > awayGoals)
            {
                forecast = "1";
            }
            else if (homeGoals == awayGoals)
            {
                forecast = "X";
            }
            else
            {
                forecast = "2";
            }

            this.dbContext.Forecast.Where(x => x.MatchId == MatchId).ToList().ForEach(x =>
            {
                bool isSuccess = x.Forcast == forecast;
                x.IsOpen = false;
                x.IsSuccess = isSuccess;
                x.homeGoals = homeGoals;
                x.AwayGoals = awayGoals;

                if (isSuccess)
                {
                    result.Add(new Tuple<Guid, double, int>(x.AccountId, x.Coefficient, x.PointsPlayed));
                }

                this.dbContext.SaveChanges();
            });

            return result;
        }

        public void UpdatePlayedForForcastCount(Guid MatchId, string forcast)
        {
            var match = this.GetById(MatchId);
            if (match != null)
            {
                if (forcast == "1")
                {
                    match.PlayedFor1 += 1;
                }
                else if (forcast == "X")
                {
                    match.PlayedForX += 1;
                }
                else
                {
                    match.PlayedFor2 += 1;
                }

                this.dbContext.SaveChanges();
            }
        }

        public void UpdatePlayedMatchCount(Guid id)
        {
            var match = this.GetById(id);

            if (match != null)
            {
                match.PlayedFrom += 1;

                this.dbContext.SaveChanges();
            }
        }

        public void UpgradeUserPointsForSuccessMatch(Guid UserId, int points, double coeff)
        {
            var user = this.dbContext.UserForecast.FirstOrDefault(x => x.Id == UserId);

            if (user != null)
            {
                user.Points += (points * coeff);

                this.dbContext.SaveChanges();
            }
        }
    }
}
