using FootballOracle_DataServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballOracle_Data;
using FootballOracle_Data.interfaces;

namespace FootballOracle_DataServices
{
    public class ForecastService : IForecastService
    {
        private IFootballOracleDbContext dbContext;

        public ForecastService(IFootballOracleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(Forecast forecast)
        {
            this.dbContext.Forecast.Add(forecast);
            this.dbContext.SaveChanges();
        }

        public ICollection<Forecast> GetAllById(Guid userId)
        {
            return this.dbContext.Forecast
                .Where(x => x.AccountId == userId && x.IsPlayed == false)
                .ToList();
        }

        public void Remove(Guid id)
        {
            var forecast = this.dbContext.Forecast.FirstOrDefault(x => x.Id == id);

            this.dbContext.Forecast.Remove(forecast);
        }
    }
}
