using FootballOracle_DataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballOracle_Data;
using FootballOracle_Data.interfaces;
using System.Data.Entity;

namespace FootballOracle_DataServices
{
    public class UserForecastService : IUserForecastService
    {
        private readonly IFootballOracleDbContext dbContext;

        public UserForecastService(IFootballOracleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Forecast forecast)
        {
            this.dbContext.Forecast.Add(forecast);

            this.dbContext.SaveChanges();
        }

        public void Create(UserForecast userForecast)
        {
            this.dbContext.UserForecast.Add(userForecast);

            this.dbContext.SaveChanges();
        }

        public Forecast DeleteBet(Guid bet)
        {
            var currentBet = this.dbContext.Forecast.FirstOrDefault(x => x.Id == bet);

            if (currentBet != null)
            {
               return  this.dbContext.Forecast.Remove(currentBet);
            }

            return null;
        }

        public ICollection<Forecast> GetAllById(Guid userId)
        {
            return this.dbContext.Forecast.Where(x => x.AccountId == userId && x.IsPlayed == false).ToList();
        }

        public ICollection<Forecast> GetAllByIdAndPlayed(Guid id)
        {
            return this.dbContext.Forecast.Where(x => x.AccountId == id).ToList();
        }

        public ICollection<Guid> GetAllForUser(Guid userId)
        {
            return this.dbContext.Forecast.Where(x => x.AccountId == userId).Select(x => x.MatchId).ToList();
        }

        public Guid GetIdByAccountId(Guid accId)
        {
            return this.dbContext.UserForecast
                .Where(x => x.AccountId == accId)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        public double GetPointsbyUserId(Guid Id)
        {
            var user = this.dbContext.UserForecast.FirstOrDefault(x => x.AccountId == Id);

            if (user != null)
            {
                return user.Points;
            }

            throw new Exception("Invalid User");
        }

        public int GetSuccessfullForecastCount(Guid userId)
        {
            return this.dbContext.Forecast
                .Where(x => x.AccountId == userId && x.IsSuccess == true)
                .ToList()
                .Count;
        }

        public ICollection<Forecast> GetTopForcast(Guid userId)
        {
            return this.dbContext.Forecast
                .Where(x => x.AccountId == userId && x.IsSuccess == true)
                .OrderByDescending(x => x.Coefficient)
                .ThenByDescending(x => x.PointsPlayed)
                .Take(10)
                .ToList();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateForecast(Guid forecast, int points)
        {
            var currentForecast = this.dbContext.Forecast.FirstOrDefault(x => x.Id == forecast);

            currentForecast.IsPlayed = true;
            currentForecast.PointsPlayed = points;

            this.dbContext.SaveChanges();
        }

        public void UpgradeUserPoints(Guid user, int points)
        {
            var currentUser = this.dbContext.UserForecast.FirstOrDefault(x => x.AccountId == user);

            if (currentUser != null)
            {
                currentUser.Points -= points;

                this.dbContext.SaveChanges();
            }
        }

        public void UpgradeUserPointsAfterMatch(Guid user, double points)
        {
            var currentUser = this.dbContext.UserForecast.FirstOrDefault(x => x.AccountId == user);

            if (currentUser != null)
            {
                currentUser.Points += points;

                this.dbContext.SaveChanges();
            }
        }
    }
}
